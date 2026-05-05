#!/usr/bin/env python3
"""
F4 — Snapshot operativo cross-cliente (capabilities-aware).

Esquema heterogéneo cross-cliente:
  - tipo_documento VARCHAR existe solo en MERCOPAN, MERHONSA, KILLIOS, KILLIOS_2026
  - log_error_wms_{oc,pe,pick,reab,rec,ubic} solo en BECOFARMA, BYB, KILLIOS_2026, MAMPA
  - dañado_picking, dañado_verificacion, Enviado_A_ERP existen en TODAS

Cada query corre con try/except independiente. Capabilities se detectan al inicio.
Read-only.
"""
import os
import json
import pymssql
import datetime
from pathlib import Path
from concurrent.futures import ThreadPoolExecutor

HOST = "52.41.114.122"
PORT = 1437
USER = "sa"
PWD = os.environ["WMS_KILLIOS_DB_PASSWORD"]
ROOT = Path(__file__).resolve().parent.parent.parent
OUT = ROOT / "brain" / "data-deep-dive"
TODAY = datetime.date.today().isoformat()

DBS = [
    ("IMS4MB_BECOFARMA_PRD",       "becofarma",      "PRD"),
    ("IMS4MB_BYB_PRD",             "byb",            "PRD"),
    ("IMS4MB_CEALSA_QAS",          "cealsa",         "QAS"),
    ("IMS4MB_MERCOPAN_PRD",        "mercopan",       "PRD"),
    ("IMS4MB_MERHONSA_PRD",        "merhonsa",       "PRD"),
    ("TOMWMS_KILLIOS_PRD",         "killios",        "PRD"),
    ("TOMWMS_KILLIOS_PRD_2026",    "killios_2026",   "PRD"),
    ("TOMWMS_MAMPA_QA",            "mampa",          "QA"),
]


def conn(db):
    return pymssql.connect(server=HOST, port=PORT, user=USER, password=PWD,
                           database=db, login_timeout=15, timeout=180)


def safe(c, sql, default=None):
    """Ejecuta query, devuelve filas como list[dict] o default si falla."""
    try:
        cur = c.cursor()
        cur.execute(sql)
        rows = cur.fetchall()
        cols = [d[0] for d in cur.description] if cur.description else []
        return [dict(zip(cols, r)) for r in rows]
    except Exception as e:
        if default is None:
            return [{"_error": f"{type(e).__name__}: {str(e)[:200]}"}]
        return default


def detect_caps(c):
    """Detecta capabilities del esquema en esta BD."""
    caps = {}
    cols = safe(c, "SELECT name FROM sys.columns WHERE object_id=OBJECT_ID('i_nav_transacciones_out')", default=[])
    cnames = {r["name"] for r in cols}
    caps["has_tipo_doc_str"] = "tipo_documento" in cnames
    caps["has_idtipodoc"] = "IdTipoDocumento" in cnames

    log_tables = safe(c, "SELECT name FROM sys.tables WHERE name LIKE 'log_error_wms%'", default=[])
    caps["log_tables"] = sorted({r["name"] for r in log_tables}, key=str.lower)
    return caps


# ============= Queries =============

def q_bodegas(c):
    return safe(c, """
        SELECT IdBodega, codigo, nombre, activo, IdEmpresa
        FROM bodega ORDER BY IdBodega
    """)


def q_config_heatmap(c):
    return safe(c, """
        SELECT
            ce.idbodega,
            ISNULL(b.nombre, '?') as nombre_bodega,
            ce.idPropietario,
            ce.rechazar_pedido_incompleto,
            ce.despachar_existencia_parcial,
            ce.convertir_decimales_a_umbas,
            ce.control_lote,
            ce.control_vencimiento,
            ce.genera_lp,
            ce.crear_recepcion_de_transferencia_nav,
            ce.equiparar_cliente_con_propietario_en_doc_salida,
            ce.control_peso
        FROM i_nav_config_enc ce
        LEFT JOIN bodega b ON b.IdBodega = ce.idbodega
        ORDER BY ce.idbodega
    """)


def q_top_sps(c):
    return safe(c, """
        SELECT TOP 30
            OBJECT_NAME(object_id, database_id) as sp_name,
            execution_count,
            total_elapsed_time / 1000 as ms_total,
            total_elapsed_time / NULLIF(execution_count,0) / 1000 as ms_avg,
            last_execution_time
        FROM sys.dm_exec_procedure_stats
        WHERE database_id = DB_ID()
        ORDER BY execution_count DESC
    """)


def q_outbox_estado(c):
    return safe(c, """
        SELECT enviado, COUNT(*) as cant,
               MIN(fec_agr) as primer, MAX(fec_agr) as ultimo
        FROM i_nav_transacciones_out
        GROUP BY enviado
        ORDER BY enviado
    """)


def q_outbox_tipos(c, caps):
    if caps["has_tipo_doc_str"]:
        return safe(c, """
            SELECT TOP 25
                ISNULL(tipo_documento, '(NULL)') as clave,
                COUNT(*) as cant,
                SUM(CASE WHEN enviado = 1 THEN 1 ELSE 0 END) as enviados,
                SUM(CASE WHEN enviado = 0 THEN 1 ELSE 0 END) as pendientes
            FROM i_nav_transacciones_out
            GROUP BY tipo_documento
            ORDER BY COUNT(*) DESC
        """)
    return safe(c, """
        SELECT TOP 25
            CONCAT('IdTipoDocumento=', ISNULL(CAST(IdTipoDocumento as varchar(10)), '(NULL)')) as clave,
            COUNT(*) as cant,
            SUM(CASE WHEN enviado = 1 THEN 1 ELSE 0 END) as enviados,
            SUM(CASE WHEN enviado = 0 THEN 1 ELSE 0 END) as pendientes
        FROM i_nav_transacciones_out
        GROUP BY IdTipoDocumento
        ORDER BY COUNT(*) DESC
    """)


def q_bug001(c, caps):
    """BUG-001: dañado_picking sin ajuste a ERP. Universal."""
    parts = [
        "(SELECT COUNT(*) FROM trans_picking_ubic WHERE dañado_picking = 1) as danados_picking",
        "(SELECT COUNT(*) FROM trans_picking_ubic WHERE dañado_verificacion = 1) as danados_verif",
        "(SELECT COUNT(*) FROM trans_ajuste_enc) as ajustes_locales",
        "(SELECT COUNT(*) FROM trans_ajuste_enc WHERE Enviado_A_ERP = 1) as ajustes_enviados_erp",
        "(SELECT COUNT(*) FROM trans_ajuste_enc WHERE Enviado_A_ERP = 0 OR Enviado_A_ERP IS NULL) as ajustes_pendientes_erp",
    ]
    if caps["has_tipo_doc_str"]:
        parts.append("(SELECT COUNT(*) FROM i_nav_transacciones_out WHERE tipo_documento = 'AJCANTN') as ajcantn")
        parts.append("(SELECT COUNT(*) FROM i_nav_transacciones_out WHERE tipo_documento LIKE 'AJ%') as todos_aj")
    else:
        parts.append("NULL as ajcantn")
        parts.append("NULL as todos_aj")

    sql = "SELECT " + ", ".join(parts)
    rows = safe(c, sql, default=[{}])
    return rows[0] if rows else {}


def q_heatmap_bodega(c, caps):
    """Heat-map por bodega: queries por log_error sólo si la tabla existe."""
    log_pick = "log_error_wms_pick" if "log_error_wms_pick" in caps["log_tables"] else None
    log_rec = "log_error_wms_rec" if "log_error_wms_rec" in caps["log_tables"] else None
    log_oc = "log_error_wms_oc" if "log_error_wms_oc" in caps["log_tables"] else None

    parts = [
        "b.IdBodega",
        "b.codigo",
        "b.nombre",
        """(SELECT COUNT(*) FROM trans_picking_enc WHERE IdBodega=b.IdBodega
             AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as picks_30d""",
        """(SELECT COUNT(*) FROM trans_ajuste_enc WHERE idbodega=b.IdBodega
             AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as ajustes_30d""",
        """(SELECT COUNT(*) FROM trans_picking_ubic
             WHERE IdBodega=b.IdBodega AND dañado_picking=1
             AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as danados_30d""",
    ]
    if log_pick:
        parts.append(f"(SELECT COUNT(*) FROM {log_pick} WHERE IdBodega=b.IdBodega AND Fec_agr >= DATEADD(MONTH,-1,GETDATE())) as errores_pick_30d")
    else:
        parts.append("NULL as errores_pick_30d")
    if log_rec:
        parts.append(f"(SELECT COUNT(*) FROM {log_rec} WHERE IdBodega=b.IdBodega AND Fec_agr >= DATEADD(MONTH,-1,GETDATE())) as errores_rec_30d")
    else:
        parts.append("NULL as errores_rec_30d")
    if log_oc:
        parts.append(f"(SELECT COUNT(*) FROM {log_oc} WHERE IdBodega=b.IdBodega AND Fec_agr >= DATEADD(MONTH,-1,GETDATE())) as errores_oc_30d")
    else:
        parts.append("NULL as errores_oc_30d")

    sql = "SELECT " + ",\n".join(parts) + """
        FROM bodega b
        WHERE (b.activo = 1 OR b.activo IS NULL)
        ORDER BY b.IdBodega
    """
    return safe(c, sql)


def q_log_totals(c, caps):
    """Sólo cuenta tablas que existen."""
    parts = []
    for tbl in ["log_error_wms", "log_error_wms_pe", "log_error_wms_pick",
                "log_error_wms_rec", "log_error_wms_reab", "log_error_wms_oc",
                "log_error_wms_ubic"]:
        # Match case-insensitive con caps
        tbl_lower = tbl.lower()
        match = next((t for t in caps["log_tables"] if t.lower() == tbl_lower), None)
        if match:
            parts.append(f"(SELECT COUNT(*) FROM {match}) as {tbl_lower}")
        else:
            parts.append(f"NULL as {tbl_lower}")
    sql = "SELECT " + ", ".join(parts)
    rows = safe(c, sql, default=[{}])
    return rows[0] if rows else {}


def q_log_recientes(c, caps):
    """10 errores más recientes en log_error_wms general (universal)."""
    if not any(t.lower() == "log_error_wms" for t in caps["log_tables"]):
        return [{"_error": "tabla log_error_wms no existe en esta BD"}]
    return safe(c, """
        SELECT TOP 10
            ISNULL(MensajeError, '?') as MensajeError,
            ISNULL(Fec_agr, '?') as Fec_agr,
            ISNULL(IdBodega, 0) as IdBodega
        FROM log_error_wms
        ORDER BY Fec_agr DESC
    """)


# ============= Run =============

def run_db(db_name, cliente, env):
    out = {"db": db_name, "cliente": cliente, "env": env, "ts": TODAY, "ok": False}
    try:
        c = conn(db_name)
        caps = detect_caps(c)
        out["caps"] = caps
        out["bodegas"] = q_bodegas(c)
        out["config_heatmap"] = q_config_heatmap(c)
        out["top_sps"] = q_top_sps(c)
        out["outbox_estado"] = q_outbox_estado(c)
        out["outbox_tipos"] = q_outbox_tipos(c, caps)
        out["bug001"] = q_bug001(c, caps)
        out["heatmap_bodega"] = q_heatmap_bodega(c, caps)
        out["log_totals"] = q_log_totals(c, caps)
        out["log_recientes"] = q_log_recientes(c, caps)
        c.close()
        out["ok"] = True
    except Exception as e:
        out["error"] = f"{type(e).__name__}: {str(e)[:300]}"
    return out


# ============= Render markdown =============

def fmt_table(rows, cols=None, max_rows=50):
    if not rows:
        return "_(sin filas)_"
    if isinstance(rows[0], dict) and "_error" in rows[0]:
        return f"_ERROR: {rows[0]['_error']}_"
    if cols is None:
        cols = list(rows[0].keys())
    head = "| " + " | ".join(cols) + " |"
    sep = "|" + "|".join(["---"] * len(cols)) + "|"
    body = []
    for r in rows[:max_rows]:
        cells = []
        for col in cols:
            v = r.get(col)
            if v is None:
                cells.append("—")
            elif isinstance(v, bool):
                cells.append("✓" if v else "·")
            elif isinstance(v, int):
                cells.append(f"{v:,}" if abs(v) >= 1000 else str(v))
            elif isinstance(v, float):
                cells.append(f"{v:,.2f}")
            elif isinstance(v, datetime.datetime):
                cells.append(v.strftime("%Y-%m-%d %H:%M"))
            elif isinstance(v, datetime.date):
                cells.append(v.isoformat())
            else:
                s = str(v).replace("|", "\\|").replace("\n", " ")
                cells.append(s[:80])
        body.append("| " + " | ".join(cells) + " |")
    out = "\n".join([head, sep] + body)
    if len(rows) > max_rows:
        out += f"\n_(... y {len(rows) - max_rows} filas más)_"
    return out


def render_snapshot(d):
    cliente = d["cliente"]
    db = d["db"]
    env = d["env"]
    md = []

    md.append("---")
    md.append(f"id: SNAPSHOT-{cliente.upper()}-{TODAY}")
    md.append("tipo: snapshot-operativo")
    md.append("estado: vigente")
    md.append(f"cliente: {cliente}")
    md.append(f"clientes: [{cliente}]")
    md.append(f"db_name: {db}")
    md.append(f"environment: {env}")
    md.append(f"fecha: {TODAY}")
    md.append("autores: [agente-brain-replit]")
    md.append(f"tags: [snapshot, datos-productivos, cliente/{cliente}, bug-001]")
    md.append("relacionado_con: [BUG-001, F4]")
    md.append("---\n")

    md.append(f"# Snapshot operativo — {cliente.upper()} ({env})")
    md.append(f"\n> **BD**: `{db}` · **Host**: `{HOST}:{PORT}` · **Fecha**: {TODAY}")
    md.append("> Generado por `tools/wms-deep-dive/snapshot_runner.py` (read-only).\n")

    if not d.get("ok"):
        md.append(f"## ERROR DE CONEXIÓN\n\n```\n{d.get('error', 'unknown')}\n```\n")
        return "\n".join(md)

    caps = d.get("caps", {})
    md.append("## 0. Capabilities detectadas\n")
    md.append(f"- `i_nav_transacciones_out.tipo_documento` (varchar): **{'sí' if caps.get('has_tipo_doc_str') else 'NO'}**")
    md.append(f"- `i_nav_transacciones_out.IdTipoDocumento` (int): **{'sí' if caps.get('has_idtipodoc') else 'NO'}**")
    md.append(f"- Tablas `log_error_wms_*` disponibles: `{', '.join(caps.get('log_tables', [])) or '(ninguna)'}`\n")

    bug = d["bug001"]
    md.append("## 1. BUG-001: dañado_picking sin ajuste a ERP\n")
    danados = bug.get("danados_picking", 0) or 0
    ajcantn = bug.get("ajcantn")
    aj_local = bug.get("ajustes_locales", 0) or 0
    aj_env = bug.get("ajustes_enviados_erp", 0) or 0
    aj_pen = bug.get("ajustes_pendientes_erp", 0) or 0

    md.append("| Métrica | Valor |")
    md.append("|---|---:|")
    md.append(f"| Marcas `dañado_picking = 1` (histórico) | **{danados:,}** |")
    md.append(f"| Marcas `dañado_verificacion = 1` (histórico) | {(bug.get('danados_verif', 0) or 0):,} |")
    if ajcantn is not None:
        md.append(f"| Outbox con `tipo_documento = 'AJCANTN'` | **{ajcantn:,}** |")
        md.append(f"| Outbox con `tipo_documento LIKE 'AJ%'` | {(bug.get('todos_aj', 0) or 0):,} |")
    else:
        md.append(f"| Outbox con `tipo_documento = 'AJCANTN'` | _no aplica (BD sin tipo_documento varchar)_ |")
    md.append(f"| Ajustes locales (`trans_ajuste_enc`) | {aj_local:,} |")
    md.append(f"| Ajustes con `Enviado_A_ERP = 1` | {aj_env:,} |")
    md.append(f"| Ajustes con `Enviado_A_ERP = 0 ó NULL` (**pendientes a ERP**) | **{aj_pen:,}** |")

    md.append("\n**Interpretación:**\n")
    if danados > 0 and aj_local == 0:
        md.append(f"- {danados:,} daños registrados en HH y **0 ajustes locales** → la marca de daño NO genera ajuste de inventario (BUG-001 root cause).")
    elif danados > 0 and ajcantn is not None and ajcantn == 0:
        md.append(f"- {danados:,} daños registrados, {aj_local:,} ajustes locales, pero **0 documentos AJCANTN en outbox** → los ajustes locales no se enrutan a NAV (gap NAV out).")
    elif danados > 0 and aj_pen > 0:
        pct = aj_pen / aj_local * 100 if aj_local else 0
        md.append(f"- {aj_pen:,} ajustes locales con `Enviado_A_ERP=0` ({pct:.1f}% del total) → backlog de envío al ERP.")
    elif danados == 0 and aj_local == 0:
        md.append("- Sin marcas de daño y sin ajustes locales (BD vacía o sin tráfico).")
    else:
        md.append(f"- Comportamiento mixto: {danados:,} daños, {aj_local:,} ajustes locales, {aj_pen:,} pendientes ERP.")

    md.append("\n## 2. Heat-map por bodega (último mes)\n")
    md.append(fmt_table(d["heatmap_bodega"]))

    md.append("\n## 3. Outbox `i_nav_transacciones_out`\n")
    md.append("### 3.1 Por estado de envío\n")
    md.append(fmt_table(d["outbox_estado"]))
    md.append("\n### 3.2 Por tipo de documento (top 25)\n")
    md.append(fmt_table(d["outbox_tipos"]))

    md.append("\n## 4. Top 30 SPs ejecutados (sys.dm_exec_procedure_stats)\n")
    md.append("> Nota: `sys.dm_exec_procedure_stats` se resetea al reiniciar SQL Server.\n")
    md.append(fmt_table(d["top_sps"],
        cols=["sp_name","execution_count","ms_total","ms_avg","last_execution_time"]))

    md.append("\n## 5. Heat-map de configuración (i_nav_config_enc)\n")
    md.append(fmt_table(d["config_heatmap"]))

    md.append("\n## 6. Volumen total log_error_wms_*\n")
    if d["log_totals"] and "_error" not in d["log_totals"]:
        md.append("| Log | Filas |")
        md.append("|---|---:|")
        for k, v in d["log_totals"].items():
            shown = f"{v:,}" if v is not None else "_(no existe)_"
            md.append(f"| `{k}` | {shown} |")

    md.append("\n### 6.1 Errores recientes (top 10 log general)\n")
    md.append(fmt_table(d["log_recientes"]))

    md.append("\n## 7. Bodegas configuradas\n")
    md.append(fmt_table(d["bodegas"]))

    return "\n".join(md)


def render_cross(results):
    md = []
    md.append("---")
    md.append(f"id: CROSS-COMPARATIVA-{TODAY}")
    md.append("tipo: cross-comparativa")
    md.append("estado: vigente")
    md.append(f"fecha: {TODAY}")
    md.append("autores: [agente-brain-replit]")
    md.append("clientes: [becofarma, byb, cealsa, mercopan, merhonsa, killios, mampa]")
    md.append("ramas: [dev_2028_merge]")
    md.append("tags: [snapshot, cross-cliente, bug-001, heat-map]")
    md.append("relacionado_con: [BUG-001, F4]")
    md.append("---\n")
    md.append(f"# CROSS-COMPARATIVA WMS — snapshot {TODAY}\n")
    md.append("> 8 BDs analizadas en EC2 `52.41.114.122:1437` (excluidos LIVE/mpos/POD_BETA).")
    md.append("> Generado por `tools/wms-deep-dive/snapshot_runner.py`.\n")

    md.append("## 1. Inventario general\n")
    md.append("| Cliente | Env | DB | Bodegas | Outbox total | Daños hist. | Ajustes locales | tipo_documento varchar | Logs especializados |")
    md.append("|---|---|---|---:|---:|---:|---:|:---:|---|")
    for d in results:
        if not d.get("ok"):
            md.append(f"| {d['cliente']} | {d['env']} | `{d['db']}` | ERR | ERR | ERR | ERR | — | — |")
            continue
        bod = len(d["bodegas"])
        ob = sum((r.get("cant", 0) or 0) for r in d["outbox_estado"])
        bug = d["bug001"]
        danos_total = (bug.get('danados_picking', 0) or 0) + (bug.get('danados_verif', 0) or 0)
        caps = d.get("caps", {})
        has_str = "✓" if caps.get("has_tipo_doc_str") else "·"
        log_specs = [t.replace("log_error_wms_", "") for t in caps.get("log_tables", []) if t != "log_error_wms"]
        log_str = ", ".join(log_specs) if log_specs else "_solo general_"
        md.append(f"| {d['cliente']} | {d['env']} | `{d['db']}` | {bod} | {ob:,} | {danos_total:,} | {bug.get('ajustes_locales',0):,} | {has_str} | {log_str} |")

    md.append("\n## 2. BUG-001 cross-cliente\n")
    md.append("> Métrica universal: **dañado_picking en HH** vs **ajustes locales con Enviado_A_ERP=0**.")
    md.append("> Métrica solo donde aplica: **AJCANTN en outbox** (4 BDs con tipo_documento varchar).\n")
    md.append("| Cliente | Daños picking | Daños verif | Ajustes locales | Enviados ERP | Pendientes ERP | AJCANTN outbox | AJ% outbox | Verdict |")
    md.append("|---|---:|---:|---:|---:|---:|---:|---:|---|")
    for d in results:
        if not d.get("ok"):
            continue
        bug = d["bug001"]
        dp = bug.get("danados_picking", 0) or 0
        dv = bug.get("danados_verif", 0) or 0
        al = bug.get("ajustes_locales", 0) or 0
        ae = bug.get("ajustes_enviados_erp", 0) or 0
        ap = bug.get("ajustes_pendientes_erp", 0) or 0
        aj = bug.get("ajcantn")
        all_aj = bug.get("todos_aj")
        aj_str = f"{aj:,}" if aj is not None else "n/a"
        all_aj_str = f"{all_aj:,}" if all_aj is not None else "n/a"

        if dp == 0 and al == 0:
            verdict = "sin tráfico"
        elif dp > 100 and al == 0:
            verdict = "**BUG ROOT (sin ajuste local)**"
        elif aj is not None and dp > 100 and aj == 0:
            verdict = "**BUG GAP NAV (sin AJCANTN)**"
        elif al > 0 and ap / al > 0.5:
            verdict = f"BACKLOG ERP ({ap/al*100:.0f}% pend)"
        else:
            verdict = "OK / parcial"
        md.append(f"| {d['cliente']} | {dp:,} | {dv:,} | {al:,} | {ae:,} | {ap:,} | {aj_str} | {all_aj_str} | {verdict} |")

    md.append("\n## 3. Volumen log_error_wms_* por cliente\n")
    md.append("| Cliente | general | pedido | picking | recepción | reabasto | OC | ubicación |")
    md.append("|---|---:|---:|---:|---:|---:|---:|---:|")
    for d in results:
        if not d.get("ok"):
            continue
        lt = d.get("log_totals", {})
        def cell(k):
            v = lt.get(k)
            return f"{v:,}" if v is not None else "—"
        md.append(f"| {d['cliente']} | {cell('log_error_wms')} | {cell('log_error_wms_pe')} | "
                  f"{cell('log_error_wms_pick')} | {cell('log_error_wms_rec')} | "
                  f"{cell('log_error_wms_reab')} | {cell('log_error_wms_oc')} | "
                  f"{cell('log_error_wms_ubic')} |")

    md.append("\n## 4. Notas distintivas por cliente\n")
    for d in results:
        if not d.get("ok"):
            md.append(f"- **{d['cliente']}**: ERROR de conexión")
            continue
        bug = d["bug001"]
        bod = len(d["bodegas"])
        notas = []
        if bod >= 30: notas.append(f"multi-bodega ({bod} bodegas)")
        if d["env"] != "PRD": notas.append(f"entorno {d['env']}")
        ob = sum((r.get("cant", 0) or 0) for r in d["outbox_estado"])
        if ob == 0: notas.append("outbox vacío")
        if (bug.get("danados_picking", 0) or 0) > 1000 and (bug.get("ajustes_locales", 0) or 0) == 0:
            notas.append(f"BUG-001 root: {bug['danados_picking']:,} daños / 0 ajustes")
        if (bug.get("ajcantn") == 0) and (bug.get("danados_picking", 0) or 0) > 1000:
            notas.append(f"BUG-001 gap NAV: {bug['danados_picking']:,} daños / 0 AJCANTN")
        if not d.get("caps", {}).get("has_tipo_doc_str"):
            notas.append("schema legacy IMS4MB (sin tipo_documento varchar)")
        if not notas: notas.append("perfil estándar")
        md.append(f"- **{d['cliente']}**: " + "; ".join(notas))

    md.append(f"\n## 5. Snapshots individuales\n")
    for d in results:
        md.append(f"- [`{d['cliente']}/snapshot-{TODAY}.md`](./{d['cliente']}/snapshot-{TODAY}.md)")

    return "\n".join(md)


def main():
    print(f"Ejecutando query suite en {len(DBS)} BDs (paralelo, 8 workers)...")
    OUT.mkdir(parents=True, exist_ok=True)

    with ThreadPoolExecutor(max_workers=8) as ex:
        results = list(ex.map(lambda x: run_db(*x), DBS))

    for d in results:
        status = "OK" if d.get("ok") else f"ERROR: {d.get('error', '?')[:80]}"
        bug = d.get("bug001", {})
        dp = bug.get("danados_picking", 0) or 0
        aj = bug.get("ajcantn")
        aj_str = f"{aj:,}" if aj is not None else "  n/a"
        al = bug.get("ajustes_locales", 0) or 0
        ap = bug.get("ajustes_pendientes_erp", 0) or 0
        print(f"  [{d['cliente']:<14}] {status:<8}  daños_pick={dp:>7,}  AJCANTN={aj_str:>7}  ajustes_loc={al:>5,}  pend_erp={ap:>5,}")

        client_dir = OUT / d["cliente"]
        client_dir.mkdir(parents=True, exist_ok=True)
        (client_dir / f"snapshot-{TODAY}.md").write_text(render_snapshot(d), encoding="utf-8")
        (client_dir / f"snapshot-{TODAY}-raw.json").write_text(
            json.dumps(d, indent=2, ensure_ascii=False, default=str), encoding="utf-8")

    cross_path = OUT / "CROSS-COMPARATIVA.md"
    cross_path.write_text(render_cross(results), encoding="utf-8")
    print(f"\nCross-comparativa: {cross_path.relative_to(ROOT)}")
    print("Listo.")


if __name__ == "__main__":
    main()
