#!/usr/bin/env python3
"""
F5 — Atlas consolidado cliente-aware.

Genera:
  - _index/ATLAS.md  (punto de entrada navegable)
  - clients/<cliente>-atlas-2026-05-05.md  (8 fichas append-style)

Cada ficha cruza:
  - Snapshot operativo F4 (lo más fresco)
  - Cross-refs a clients/<x>.md, ADRs, learnings, BUG-001
  - Diagnostic queries listas para copy-paste
  - Capability flags relevantes desde i_nav_config_enc

NO modifica las fichas clients/<x>.md existentes para evitar perder
contenido densamente curado (cada una tiene >250 líneas).
"""
import json
import datetime
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent.parent
DATA = ROOT / "brain" / "data-deep-dive"
CLIENTS = ROOT / "brain" / "clients"
INDEX = ROOT / "brain" / "_index"
TODAY = "2026-05-05"

# Catálogo cliente -> metadatos
CLIENTES = {
    "becofarma": {
        "db": "IMS4MB_BECOFARMA_PRD",
        "env": "PRD",
        "rubro": "Farmacia",
        "erp": "SAP B1 (DI-API)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "PRODUCT-CENTRIC",
        "ficha_existente": "clients/becofarma.md",
        "fingerprint": "BECOFARMA_CLIENT_FARMA-VERIF-COMPLETO_APPLIED",
        "adrs": [],
        "learnings_relevantes": ["L-013-sapbosync-exe", "L-014-becofarma-bd-diagnostica", "L-016-log-segmentado"],
        "bug001_severidad": "no afectado (compensa con AJCANTN intensivo)",
        "casos": [],
        "notas_atlas": "BD diagnóstica en EC2, no productiva real; AJCANTN intensivo es diseño operativo, no compensación de bug.",
    },
    "byb": {
        "db": "IMS4MB_BYB_PRD",
        "env": "PRD",
        "rubro": "Alimentos / consumo masivo",
        "erp": "NAV (Microsoft Dynamics)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "PRODUCT-CENTRIC NULL",
        "ficha_existente": "clients/byb.md",
        "fingerprint": "BYB_CLIENT_NAV-OPERACION-PARADA-2024_APPLIED",
        "adrs": ["ADR-008-byb-replenishment-module"],
        "learnings_relevantes": ["L-010-navsync-bb-no-procesa-ingresos", "L-022-patron-naming-sincronizador",
                                  "L-023-byb-corte-operativo-2024", "L-024-byb-verificacion-half-implemented"],
        "bug001_severidad": "MEDIA (484 líneas; outlier 21% via HH)",
        "casos": [],
        "notas_atlas": "Único cliente NAV. Outbox parado entre dic-2023 y oct-2025 (L-023). Verificación half-implemented (L-024). 21% del bug entra por HH (vs 1-3% en el resto) — investigar HH específico.",
    },
    "cealsa": {
        "db": "IMS4MB_CEALSA_QAS",
        "env": "QAS",
        "rubro": "Distribución general",
        "erp": "Propio (CEALSASync.exe)",
        "rama_prod": "dev_2028_merge",
        "modelo_config": "PRODUCT-CENTRIC heterogéneo",
        "ficha_existente": "clients/cealsa.md",
        "fingerprint": "CEALSA_CLIENT_QAS-CEALSASYNC-PROPIO_APPLIED",
        "adrs": ["ADR-009-cealsa-3pl-jornada-prefactura"],
        "learnings_relevantes": [],
        "bug001_severidad": "no afectado (QAS sin operación real)",
        "casos": [],
        "notas_atlas": "Entorno QAS, outbox vacío. Tiene módulos exclusivos (Pólizas, Kits, fiscal-warehouse). 80% backlog Enviado_A_ERP=0 (368/460) — coherente con QAS no operativo.",
    },
    "killios": {
        "db": "TOMWMS_KILLIOS_PRD",
        "env": "PRD",
        "rubro": "Gastronómico",
        "erp": "SAP B1 (DI-API)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "MIXTO",
        "ficha_existente": "clients/killios.md",
        "fingerprint": "KILLIOS_CLIENT_GASTRONOMICO-SAP-B1_APPLIED",
        "adrs": ["ADR-007-killios-sap-b1-integration"],
        "learnings_relevantes": ["L-009-sapsync-killios-solo-enteros", "L-014-becofarma-bd-diagnostica"],
        "bug001_severidad": "CRÍTICA — snapshot histórico (3 meses)",
        "casos": ["CP-013-killios-wms164"],
        "notas_atlas": "Snapshot histórico 2025-06 → 2025-08 (BD vieja). La activa es KILLIOS_PRD_2026.",
    },
    "killios_2026": {
        "db": "TOMWMS_KILLIOS_PRD_2026",
        "env": "PRD",
        "rubro": "Gastronómico",
        "erp": "SAP B1 (DI-API)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "MIXTO",
        "ficha_existente": "clients/killios.md",
        "fingerprint": "KILLIOS_CLIENT_GASTRONOMICO-SAP-B1_APPLIED",
        "adrs": ["ADR-007-killios-sap-b1-integration"],
        "learnings_relevantes": ["L-009-sapsync-killios-solo-enteros"],
        "bug001_severidad": "CRÍTICA — cliente reportante, política prioritaria",
        "casos": ["CP-013-killios-wms164"],
        "notas_atlas": "BD activa de Killios. 10,565 daños / 318,191 UM fantasma en 11 meses. Mes pico abril 2026 (1,904 líneas, 62k UM). Heidi Lopez + Kevin Ramos = 71% del bug.",
    },
    "mampa": {
        "db": "TOMWMS_MAMPA_QA",
        "env": "QA",
        "rubro": "Zapatería (talla y color)",
        "erp": "SAP B1 (SAPBOSyncMampa.exe)",
        "rama_prod": "dev_2028_merge",
        "modelo_config": "BODEGA-CENTRIC",
        "ficha_existente": "(falta — generar en Wave 2)",
        "fingerprint": "MAMPA_CLIENT_TALLA-COLOR-ZAPATERIA_APPLIED",
        "adrs": [],
        "learnings_relevantes": ["L-017-fk-sentinela-cero", "L-018-verificacion-etiquetas"],
        "bug001_severidad": "no afectado (no usa la feature)",
        "casos": [],
        "notas_atlas": "Multi-bodega real (33 bodegas en QA). Catálogo masivo 31,397 productos por talla x color. NO usa lote ni vencimiento. IdTipoRotacion=1 (FIFO puro). Pick por voz + control talla/color en TODAS las bodegas.",
    },
    "mercopan": {
        "db": "IMS4MB_MERCOPAN_PRD",
        "env": "PRD",
        "rubro": "Consumo masivo (aceites, detergentes)",
        "erp": "NAV (probable)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "PRODUCT-CENTRIC",
        "ficha_existente": "clients/mercopan.md",
        "fingerprint": "(pendiente fingerprint formal)",
        "adrs": [],
        "learnings_relevantes": [],
        "bug001_severidad": "CRÍTICA — el más grave histórico (574k UM en 29 meses)",
        "casos": [],
        "notas_atlas": "El más grave en volumen absoluto histórico. 19,607 daños / 574k UM en 29 meses. Felix Mariscal + Abel Castillo = 88%. Estado actual UNKNOWN (backup termina jul-2024). 3,094 AJCANTN históricos = máxima compensación manual del bug.",
    },
    "merhonsa": {
        "db": "IMS4MB_MERHONSA_PRD",
        "env": "PRD",
        "rubro": "(rubro a confirmar)",
        "erp": "(a confirmar)",
        "rama_prod": "dev_2023_estable",
        "modelo_config": "(pendiente análisis)",
        "ficha_existente": "clients/merhonsa.md",
        "fingerprint": "(pendiente)",
        "adrs": [],
        "learnings_relevantes": [],
        "bug001_severidad": "n/a (backup vacío en CLIENTES-AFECTADOS, refresh 2026-04 muestra 0 daños / 1.014M outbox)",
        "casos": [],
        "notas_atlas": "ATÍPICO: 0 trans_picking_ubic / 0 trans_picking_det / 1.014M filas en outbox. Ratio inusual sugiere uso solo de cabeceras o flujo distinto al estándar TOM.",
    },
}


def load_snapshot(cliente):
    p = DATA / cliente / f"snapshot-{TODAY}-raw.json"
    if not p.exists():
        return None
    return json.loads(p.read_text())


# ============= Diagnostic queries por cliente =============

def diagnostic_queries(cliente, snap, meta):
    """Genera bloque de queries SQL listas para copy-paste."""
    db = meta["db"]
    has_str = snap.get("caps", {}).get("has_tipo_doc_str", False) if snap else False

    out = ["### Q1. Daños activos sin ajuste (BUG-001)"]
    out.append(f"```sql\nUSE [{db}];\n")
    out.append("-- Lineas con daño marcado pero stock sin descontar")
    out.append("SELECT TOP 100")
    out.append("    pu.IdPickingUbic, pu.IdPickingEnc, pu.IdBodega,")
    out.append("    pu.IdProductoBodega, pu.cantidad_solicitada, pu.cantidad_despachada,")
    out.append("    pu.dañado_picking, pu.dañado_verificacion,")
    out.append("    pu.fec_agr, pu.user_agr,")
    out.append("    pe.estado as estado_picking_enc")
    out.append("FROM trans_picking_ubic pu")
    out.append("LEFT JOIN trans_picking_enc pe ON pe.IdPickingEnc = pu.IdPickingEnc")
    out.append("WHERE pu.dañado_picking = 1")
    out.append("  AND pu.cantidad_despachada = 0")
    out.append("  AND pu.fec_agr >= DATEADD(MONTH, -3, GETDATE())")
    out.append("ORDER BY pu.fec_agr DESC;")
    out.append("```\n")

    out.append("### Q2. Backlog Enviado_A_ERP=0 (ajustes locales sin sincronizar)")
    out.append(f"```sql\nUSE [{db}];\n")
    out.append("SELECT")
    out.append("    ae.idajusteenc, ae.idbodega, ae.fecha,")
    out.append("    ae.referencia, ae.idusuario,")
    out.append("    ae.fec_agr, ae.user_agr,")
    out.append("    ae.IdPropietarioBodega, ae.IdCentroCosto,")
    out.append("    (SELECT COUNT(*) FROM trans_ajuste_det ad WHERE ad.idajusteenc = ae.idajusteenc) as lineas")
    out.append("FROM trans_ajuste_enc ae")
    out.append("WHERE (ae.Enviado_A_ERP = 0 OR ae.Enviado_A_ERP IS NULL)")
    out.append("  AND ae.fec_agr >= DATEADD(MONTH, -1, GETDATE())")
    out.append("ORDER BY ae.fec_agr DESC;")
    out.append("```\n")

    out.append("### Q3. Outbox pendiente por tipo de documento")
    out.append(f"```sql\nUSE [{db}];\n")
    if has_str:
        out.append("SELECT tipo_documento, IdTipoDocumento,")
        out.append("       COUNT(*) as total,")
        out.append("       SUM(CASE WHEN enviado=0 THEN 1 ELSE 0 END) as pendientes,")
        out.append("       MIN(fec_agr) as primer_pendiente, MAX(fec_agr) as ultimo")
        out.append("FROM i_nav_transacciones_out")
        out.append("WHERE enviado = 0")
        out.append("GROUP BY tipo_documento, IdTipoDocumento")
        out.append("ORDER BY pendientes DESC;")
    else:
        out.append("-- Esta BD usa solo IdTipoDocumento (int 1/2/3), sin string varchar")
        out.append("SELECT IdTipoDocumento,")
        out.append("       COUNT(*) as total,")
        out.append("       SUM(CASE WHEN enviado=0 THEN 1 ELSE 0 END) as pendientes,")
        out.append("       MIN(fec_agr) as primer_pendiente, MAX(fec_agr) as ultimo")
        out.append("FROM i_nav_transacciones_out")
        out.append("GROUP BY IdTipoDocumento")
        out.append("ORDER BY pendientes DESC;")
    out.append("```\n")

    out.append("### Q4. Heat-map operativo último mes por bodega")
    out.append(f"```sql\nUSE [{db}];\n")
    out.append("SELECT b.IdBodega, b.codigo, b.nombre,")
    out.append("    (SELECT COUNT(*) FROM trans_picking_enc")
    out.append("        WHERE IdBodega=b.IdBodega")
    out.append("        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as picks_30d,")
    out.append("    (SELECT COUNT(*) FROM trans_ajuste_enc")
    out.append("        WHERE idbodega=b.IdBodega")
    out.append("        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as ajustes_30d,")
    out.append("    (SELECT COUNT(*) FROM trans_picking_ubic")
    out.append("        WHERE IdBodega=b.IdBodega AND dañado_picking=1")
    out.append("        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as danados_30d")
    out.append("FROM bodega b")
    out.append("WHERE b.activo = 1")
    out.append("ORDER BY b.IdBodega;")
    out.append("```\n")

    out.append("### Q5. Top SPs ejecutados (resetea con reinicio SQL)")
    out.append(f"```sql\nUSE [{db}];\n")
    out.append("SELECT TOP 30")
    out.append("    OBJECT_NAME(object_id, database_id) as sp,")
    out.append("    execution_count,")
    out.append("    total_elapsed_time/1000 as ms_total,")
    out.append("    total_elapsed_time/NULLIF(execution_count,0)/1000 as ms_avg,")
    out.append("    last_execution_time")
    out.append("FROM sys.dm_exec_procedure_stats")
    out.append("WHERE database_id = DB_ID()")
    out.append("ORDER BY execution_count DESC;")
    out.append("```\n")

    return "\n".join(out)


# ============= Render ficha por cliente =============

def render_ficha_atlas(cliente, meta, snap):
    md = []
    md.append("---")
    md.append(f"id: ATLAS-{cliente.upper()}-{TODAY}")
    md.append("tipo: ficha-atlas-cliente")
    md.append("estado: vigente")
    md.append(f"cliente: {cliente}")
    md.append(f"clientes: [{cliente}]")
    md.append(f"db_name: {meta['db']}")
    md.append(f"environment: {meta['env']}")
    md.append(f"rama_productiva: {meta['rama_prod']}")
    md.append(f"erp: \"{meta['erp']}\"")
    md.append(f"rubro: \"{meta['rubro']}\"")
    md.append(f"modelo_config: \"{meta['modelo_config']}\"")
    md.append(f"fecha: {TODAY}")
    md.append("autores: [agente-brain-replit]")
    md.append(f"tags: [atlas, ficha-cliente, cliente/{cliente}, F5]")
    md.append("relacionado_con: [BUG-001, F4, F5, ATLAS]")
    md.append("---\n")

    md.append(f"# Atlas — {cliente.upper()} ({meta['env']})")
    md.append(f"\n> **Punto de entrada navegable** generado en F5 ({TODAY}). Cruza snapshot operativo F4, BUG-001 cross-cliente, learnings, ADRs y ficha existente.\n")

    md.append("## Identificación rápida\n")
    md.append("| Campo | Valor |")
    md.append("|---|---|")
    md.append(f"| **Cliente** | `{cliente}` |")
    md.append(f"| **Database** | `{meta['db']}` en EC2 `52.41.114.122:1437` |")
    md.append(f"| **Environment** | {meta['env']} |")
    md.append(f"| **Rubro** | {meta['rubro']} |")
    md.append(f"| **ERP destino** | {meta['erp']} |")
    md.append(f"| **Rama productiva** | `{meta['rama_prod']}` |")
    md.append(f"| **Modelo configuración** | {meta['modelo_config']} |")
    md.append(f"| **Fingerprint** | `{meta['fingerprint']}` |")
    md.append(f"| **BUG-001 severidad** | {meta['bug001_severidad']} |\n")

    md.append(f"> {meta['notas_atlas']}\n")

    # Snapshot F4 condensado
    if snap and snap.get("ok"):
        bug = snap["bug001"]
        md.append("## Snapshot operativo F4 (mediciones del 2026-05-05)\n")
        md.append("### BUG-001 — métricas\n")
        dp = bug.get("danados_picking", 0) or 0
        dv = bug.get("danados_verif", 0) or 0
        al = bug.get("ajustes_locales", 0) or 0
        ae = bug.get("ajustes_enviados_erp", 0) or 0
        ap = bug.get("ajustes_pendientes_erp", 0) or 0
        aj = bug.get("ajcantn")

        md.append("| Métrica | Valor |")
        md.append("|---|---:|")
        md.append(f"| Marcas `dañado_picking = 1` (histórico) | **{dp:,}** |")
        md.append(f"| Marcas `dañado_verificacion = 1` (histórico) | {dv:,} |")
        md.append(f"| Outbox `tipo_documento = 'AJCANTN'` | " + (f"**{aj:,}**" if aj is not None else "_no aplica_") + " |")
        md.append(f"| Ajustes locales (`trans_ajuste_enc`) | {al:,} |")
        md.append(f"| `Enviado_A_ERP = 1` | {ae:,} ({(ae/al*100 if al else 0):.0f}%) |")
        md.append(f"| `Enviado_A_ERP = 0/NULL` (pendientes) | **{ap:,}** ({(ap/al*100 if al else 0):.0f}%) |\n")

        if snap.get("bodegas"):
            md.append(f"### Bodegas configuradas ({len(snap['bodegas'])})\n")
            md.append("| IdBodega | código | nombre | activo |")
            md.append("|---|---|---|---|")
            for b in snap["bodegas"][:20]:
                act = "✓" if b.get("activo") else "·"
                md.append(f"| {b.get('IdBodega')} | {b.get('codigo','—')} | {b.get('nombre','—')} | {act} |")
            if len(snap["bodegas"]) > 20:
                md.append(f"\n_(... y {len(snap['bodegas']) - 20} bodegas más)_")
            md.append("")

        if snap.get("outbox_estado"):
            ob_total = sum((r.get("cant", 0) or 0) for r in snap["outbox_estado"])
            ob_pend = sum((r.get("cant", 0) or 0) for r in snap["outbox_estado"] if r.get("enviado") == 0)
            md.append(f"### Outbox `i_nav_transacciones_out`\n")
            md.append(f"- **Total**: {ob_total:,} filas")
            md.append(f"- **Pendientes (enviado=0)**: {ob_pend:,} ({(ob_pend/ob_total*100 if ob_total else 0):.1f}%)\n")

        if snap.get("log_totals"):
            lt = snap["log_totals"]
            disponibles = [k for k, v in lt.items() if v is not None and v > 0]
            md.append(f"### Logs disponibles\n")
            md.append(f"- Tablas pobladas: `{', '.join(disponibles)}`")
            total = sum(v for v in lt.values() if v is not None)
            md.append(f"- Total filas log_error_wms_*: {total:,}\n")

        md.append(f"📂 **Snapshot completo**: [`data-deep-dive/{cliente}/snapshot-{TODAY}.md`](../data-deep-dive/{cliente}/snapshot-{TODAY}.md)\n")
    else:
        md.append("## Snapshot operativo F4\n")
        md.append("_(snapshot no disponible o falló)_\n")

    # Diagnostic queries
    md.append("## Diagnostic queries (copy-paste contra EC2)\n")
    md.append("> Server: `52.41.114.122,1437` · login: `sa` · permisos: read-only.\n")
    if snap:
        md.append(diagnostic_queries(cliente, snap, meta))

    # Cross-refs
    md.append("## Cross-refs al brain\n")
    md.append("### Documentación detallada\n")
    if "falta" in meta["ficha_existente"]:
        md.append(f"- ⚠️ Ficha detallada del cliente: **{meta['ficha_existente']}** (pendiente)")
    else:
        md.append(f"- 📄 Ficha detallada: [`{meta['ficha_existente']}`](../{meta['ficha_existente']})")

    md.append(f"- 🐞 BUG-001 cross-cliente: [`wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md)")
    md.append(f"- 🔄 Cross-comparativa F4: [`data-deep-dive/CROSS-COMPARATIVA.md`](../data-deep-dive/CROSS-COMPARATIVA.md)")
    md.append(f"- 🧬 Heat-map params cross-cliente: `heat-map-params/cross-cliente/`")
    md.append(f"- 🔬 Diffs código 2023↔2028: `code-deep-flow/DIFF-BOF-2023-VS-2028.md`, `DIFF-HH-2023-VS-2028.md`")
    md.append(f"- 🚩 Flags callsites: `code-deep-flow/FLAGS-CALLSITES.md`")
    md.append("")

    if meta["adrs"]:
        md.append("### ADRs específicos del cliente")
        for adr in meta["adrs"]:
            md.append(f"- [`architecture/adr/{adr}.md`](../architecture/adr/{adr}.md)")
        md.append("")

    if meta["learnings_relevantes"]:
        md.append("### Learnings relevantes")
        for l in meta["learnings_relevantes"]:
            md.append(f"- [`learnings/{l}.md`](../learnings/{l}.md)")
        md.append("")

    if meta["casos"]:
        md.append("### Casos abiertos / debugged")
        for c in meta["casos"]:
            md.append(f"- `customer-open-cases/{c}/` ó `debuged-cases/{c}/`")
        md.append("")

    md.append("---")
    md.append(f"*Generado por F5 (`tools/wms-deep-dive/build_atlas.py`) — {TODAY}*")
    return "\n".join(md)


# ============= Render ATLAS.md =============

def render_atlas(snaps):
    md = []
    md.append("---")
    md.append(f"id: ATLAS")
    md.append("tipo: atlas-maestro")
    md.append("estado: vigente")
    md.append(f"fecha: {TODAY}")
    md.append("autores: [agente-brain-replit]")
    md.append("clientes: [becofarma, byb, cealsa, killios, killios_2026, mampa, mercopan, merhonsa]")
    md.append("ramas: [dev_2023_estable, dev_2028_merge]")
    md.append("tags: [atlas, indice, navegacion, F5, cliente-aware]")
    md.append("relacionado_con: [INDEX, BUG-001, F4, F5]")
    md.append("---\n")

    md.append("# ATLAS WMS — punto de entrada cliente-aware\n")
    md.append(f"> Última actualización: **{TODAY}** · Generado por F5.")
    md.append("> Complementa al `INDEX.md` (vista árbol del brain) con una vista **cliente × dimensión**.")
    md.append("> Para entender un cliente: arrancá por su **ficha atlas** (col 'Atlas'), después saltá a la ficha detallada (col 'Ficha').\n")

    md.append("## 1. Matriz maestra cliente × dimensión\n")
    md.append("| Cliente | Env | DB | ERP | Rama PRD | BUG-001 | Daños hist. | Pend. ERP | Atlas | Ficha detallada |")
    md.append("|---|---|---|---|---|---|---:|---:|---|---|")
    for cliente, meta in CLIENTES.items():
        snap = snaps.get(cliente)
        if snap and snap.get("ok"):
            bug = snap["bug001"]
            dp = bug.get("danados_picking", 0) or 0
            ap = bug.get("ajustes_pendientes_erp", 0) or 0
            al = bug.get("ajustes_locales", 0) or 0
            pct = f"{ap}/{al}" if al else "—"
        else:
            dp = "?"
            pct = "?"
        sev_short = meta["bug001_severidad"].split("(")[0].strip().split("—")[0].strip()
        atlas_link = f"[`{cliente}-atlas-{TODAY}.md`](../clients/{cliente}-atlas-{TODAY}.md)"
        ficha = meta["ficha_existente"]
        ficha_link = "_pendiente_" if "falta" in ficha else f"[`{Path(ficha).name}`](../{ficha})"
        erp_short = meta["erp"].split("(")[0].strip() or meta["erp"].strip("()") or "_pendiente_"
        md.append(f"| **{cliente}** | {meta['env']} | `{meta['db']}` | {erp_short} | {meta['rama_prod']} | {sev_short} | {dp if isinstance(dp,str) else f'{dp:,}'} | {pct} | {atlas_link} | {ficha_link} |")
    md.append("")

    md.append("## 2. Vista por dimensión\n")

    md.append("### 2.1 Por ERP destino\n")
    by_erp = {}
    for c, m in CLIENTES.items():
        erp = m["erp"].split("(")[0].strip() or m["erp"].strip("()") or "_pendiente_"
        by_erp.setdefault(erp, []).append(c)
    for erp, clis in sorted(by_erp.items()):
        md.append(f"- **{erp}**: {', '.join(clis)}")
    md.append("")

    md.append("### 2.2 Por modelo de configuración\n")
    by_mod = {}
    for c, m in CLIENTES.items():
        by_mod.setdefault(m["modelo_config"], []).append(c)
    for mod, clis in sorted(by_mod.items()):
        md.append(f"- **{mod}**: {', '.join(clis)}")
    md.append("")

    md.append("### 2.3 Por rama productiva\n")
    by_rama = {}
    for c, m in CLIENTES.items():
        by_rama.setdefault(m["rama_prod"], []).append(c)
    for rama, clis in sorted(by_rama.items()):
        md.append(f"- **`{rama}`**: {', '.join(clis)}")
    md.append("")

    md.append("### 2.4 Por estado del BUG-001\n")
    md.append("| Severidad | Clientes | Acción |")
    md.append("|---|---|---|")
    md.append("| **CRÍTICA** | killios_2026 (cliente reportante), mercopan (mayor volumen histórico), killios (snapshot viejo) | Fix urgente; hotfix condicional según PLAYBOOK §H.3 |")
    md.append("| **MEDIA** | byb (484 líneas, outlier 21% HH) | Investigar HH específico; cliente posiblemente inactivo |")
    md.append("| **NO AFECTADO** | mampa, becofarma, cealsa, merhonsa | n/a |")
    md.append("")

    md.append("## 3. Cross-refs maestros\n")
    md.append("### Capa código (F1+F2+F3)\n")
    md.append("- `code-deep-flow/DIFF-BOF-2023-VS-2028.md` — diff completo backend BOF VB.NET")
    md.append("- `code-deep-flow/DIFF-HH-2023-VS-2028.md` — diff handheld Android")
    md.append("- `code-deep-flow/FLAGS-CALLSITES.md` — 69 flags `i_nav_config_enc` × callsites código")
    md.append("- `code-deep-flow/00-mapa-de-cajas.md`, `02-portal-y-dms.md`, `03-implosion-y-merge-lp.md`, `04-mi3-y-reserva-clavaud.md`")
    md.append("- `code-deep-flow/traza-001-license-plate.md`, `traza-002-danado-picking.md`")
    md.append("")

    md.append("### Capa datos (F4)\n")
    md.append("- `data-deep-dive/CROSS-COMPARATIVA.md` — tabla cruzada 8 BDs")
    md.append("- `data-deep-dive/<cliente>/snapshot-2026-05-05.md` — snapshot por cliente (8 archivos)")
    md.append("- `data-deep-dive/<cliente>/snapshot-2026-05-05-raw.json` — datos crudos para reprocesar")
    md.append("")

    md.append("### Capa heat-map de capabilities\n")
    md.append("- `heat-map-params/cross-cliente/01-i_nav_config_enc.md` — 78 cols, schema drift severo")
    md.append("- `heat-map-params/cross-cliente/02-bodega.md` — 123 cols, capabilities por bodega")
    md.append("- `heat-map-params/cross-cliente/03-tipos-documento.md`")
    md.append("- `heat-map-params/cross-cliente/04-producto.md`")
    md.append("")

    md.append("### Bugs y casos\n")
    md.append("- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md`")
    md.append("- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`")
    md.append("- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CASOS-RELACIONADOS.md`")
    md.append("- `customer-open-cases/CP-013-killios-wms164/`")
    md.append("- `debuged-cases/CP-015-bug-danado-picking-transversal/`")
    md.append("")

    md.append("### Arquitectura y decisiones\n")
    md.append("- ADRs por cliente: `architecture/adr/ADR-007-killios-sap-b1-integration.md`, `ADR-008-byb-replenishment-module.md`, `ADR-009-cealsa-3pl-jornada-prefactura.md`")
    md.append("- ADRs transversales: `ADR-005-identity-migration.md`, `ADR-006-multi-environment-config.md`, `ADR-010..012`")
    md.append("- Reglas: `entities/rules/` (12 reglas)")
    md.append("- Módulos: `entities/modules/` (incluye `mod-repo-tomwms-bof.md`, `mod-repo-tomhh2025.md`)")
    md.append("")

    md.append("### Learnings cross-cliente más usados\n")
    md.append("- L-014 → BECOFARMA es BD diagnóstica, no productiva real")
    md.append("- L-015 → ClickOnce empaqueta TODAS las interfaces, dispatch dinámico")
    md.append("- L-017 → FK sentinela cero en outbox (no usa NULL)")
    md.append("- L-018 → lic_plate universal en outbox (99-100%)")
    md.append("- L-019 → `i_nav_config_enc` es la fuente maestra de capability flags")
    md.append("- L-022 → Patrón naming sincronizador (`SAPBOSync<Cliente>.exe`, `NavSync.exe`, `<Cliente>Sync.exe`)")
    md.append("- L-023 → BYB outbox parado dic-2023 → oct-2025")
    md.append("")

    md.append("## 4. Cómo usar este atlas\n")
    md.append("**Caso 1: \"hay un ticket de Killios sobre daños\"**")
    md.append("→ abrir `clients/killios_2026-atlas-2026-05-05.md` → ver métricas BUG-001 → ejecutar Q1 contra EC2 → cruzar con `BUG-001/CLIENTES-AFECTADOS.md` para ver concentración por operador.\n")

    md.append("**Caso 2: \"qué clientes están afectados por el flag X\"**")
    md.append("→ abrir `code-deep-flow/FLAGS-CALLSITES.md` → ver callsites del flag → mapear a clientes con `heat-map-params/cross-cliente/01-i_nav_config_enc.md`.\n")

    md.append("**Caso 3: \"un cliente nuevo se queja de que el outbox no envía\"**")
    md.append("→ ejecutar Q3 (outbox por tipo) → cruzar con `learnings/L-022` (naming sync) y `L-010` (NAV no procesa ingresos) → ver fingerprint del cliente en `INDEX.md`.\n")

    md.append("**Caso 4: \"diferencias entre BOF 2023 y 2028 en módulo Y\"**")
    md.append("→ abrir `code-deep-flow/DIFF-BOF-2023-VS-2028.md` → buscar el módulo → cruzar con cliente productivo en col 'Rama PRD' del cuadro 2.\n")

    md.append("---")
    md.append(f"*F5 Atlas consolidado · generado {TODAY} por agente-brain-replit*")
    return "\n".join(md)


def main():
    snaps = {c: load_snapshot(c) for c in CLIENTES}
    print(f"Snapshots cargados: {sum(1 for s in snaps.values() if s)}/{len(CLIENTES)}")

    # 1. Fichas atlas por cliente
    for cliente, meta in CLIENTES.items():
        snap = snaps[cliente]
        path = CLIENTS / f"{cliente}-atlas-{TODAY}.md"
        path.write_text(render_ficha_atlas(cliente, meta, snap), encoding="utf-8")
        print(f"  ✓ {path.relative_to(ROOT)}  ({path.stat().st_size:,} bytes)")

    # 2. ATLAS maestro
    INDEX.mkdir(parents=True, exist_ok=True)
    atlas_path = INDEX / "ATLAS.md"
    atlas_path.write_text(render_atlas(snaps), encoding="utf-8")
    print(f"\n  ✓ {atlas_path.relative_to(ROOT)}  ({atlas_path.stat().st_size:,} bytes)")
    print("\nF5 listo.")


if __name__ == "__main__":
    main()
