#!/usr/bin/env python3
"""
Parser de inventario para Atlas BOF/HH 2023 vs 2028.

Por cada archivo VB/CS/SQL/Java en cada checkout, extrae:
- hash, lineas, tipo
- funciones publicas (signature simplificada)
- tablas tocadas (regex sobre strings SQL embebidos)
- flags i_nav_config_enc consultados (substring literal)
- procedimientos almacenados invocados (EXEC sp_xxx)

Output: CSV por checkout en /tmp/wms-deep-dive/work/
"""
import os
import re
import csv
import hashlib
import sys
from pathlib import Path
from concurrent.futures import ProcessPoolExecutor, as_completed

# Lista canonica de flags i_nav_config_enc (extraida del brain)
FLAGS = [
    "Ejecutar_En_Despacho_Automaticamente", "IdProductoEstado_NC",
    "bodega_facturacion", "bodega_faltante", "bodega_prorrateo",
    "bodega_prorrateo1", "cantidad_en_presentacion_transacciones_out",
    "cealsa_vwacuerdocomercialenc", "codigo_bodega_erp_nc",
    "codigo_bodega_nc_erp", "conservar_zona_picking_clavaud",
    "considerar_disponibilidad_ubicacion_reabasto",
    "considerar_paletizado_en_reabasto", "control_lote", "control_peso",
    "control_vencimiento", "convertir_decimales_a_umbas",
    "crear_recepcion_de_compra_nav", "crear_recepcion_de_transferencia_nav",
    "despachar_existencia_parcial", "dias_vida_defecto_perecederos",
    "equiparar_cliente_con_propietario_en_doc_salida", "equiparar_productos",
    "excluir_recepcion_picking", "excluir_ubicaciones_reabasto",
    "explosio_automatica_nivel_max", "explosion_automatica",
    "explosion_automatica_desde_ubicacion_picking",
    "explosion_automatica_nivel_max", "genera_lp",
    "generar_pedido_ingreso_bodega_destino",
    "generar_recepcion_auto_bodega_destino", "implosion_automatica",
    "inferir_bonificacion_pedido_sap", "interface_sap", "lote_defecto_nc",
    "producto_estado", "push_ingreso_nav_desde_hh",
    "rechazar_bonificacion_incompleta", "rechazar_pedido_incompleto",
    "reservar_umbas_primero", "sap_control_draft_ajustes",
    "sap_control_draft_traslados", "valida_solo_codigo", "vence_defecto_nc",
    "rotacion", "IdTipoRotacion", "IdIndiceRotacion",
    # Flags adicionales mencionados en el brain
    "danado_picking", "AJCANTN", "genera_lote", "genera_lp_old",
    "serializado",
]

EXTENSIONS = {".vb", ".cs", ".sql", ".java", ".asmx", ".aspx", ".vbproj", ".csproj"}

# Regex compilados
RE_VB_FUNC = re.compile(
    r'^\s*(?:Public|Friend)\s+(?:Shared\s+)?(?:Overrides\s+)?(?:Overridable\s+)?'
    r'(Function|Sub|Property|Class)\s+(\w+)',
    re.MULTILINE | re.IGNORECASE)
RE_CS_FUNC = re.compile(
    r'^\s*(?:public|internal)\s+(?:async\s+|static\s+|virtual\s+|override\s+|sealed\s+|abstract\s+|partial\s+)*'
    r'(?:[\w\<\>\[\],\s\.]+?)\s+(\w+)\s*[\(<]',
    re.MULTILINE)
RE_CS_CLASS = re.compile(
    r'^\s*(?:public|internal)\s+(?:static\s+|sealed\s+|abstract\s+|partial\s+)*'
    r'(class|interface|struct|enum)\s+(\w+)',
    re.MULTILINE)
RE_JAVA_FUNC = re.compile(
    r'^\s*(?:public|protected)\s+(?:static\s+|final\s+|synchronized\s+|abstract\s+|native\s+)*'
    r'(?:[\w\<\>\[\],\s\.]+?)\s+(\w+)\s*\(',
    re.MULTILINE)
RE_JAVA_CLASS = re.compile(
    r'^\s*(?:public|protected)\s+(?:static\s+|final\s+|abstract\s+)*'
    r'(class|interface|enum)\s+(\w+)',
    re.MULTILINE)
RE_SQL_PROC = re.compile(
    r'CREATE\s+(?:OR\s+ALTER\s+)?(PROCEDURE|FUNCTION|TRIGGER|VIEW)\s+'
    r'(?:\[?dbo\]?\.)?(\[?\w+\]?)',
    re.IGNORECASE)

# Tablas tocadas (en strings SQL embebidos)
RE_TBL_FROM = re.compile(r'\bFROM\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)', re.IGNORECASE)
RE_TBL_JOIN = re.compile(r'\bJOIN\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)', re.IGNORECASE)
RE_TBL_INSERT = re.compile(r'\bINSERT\s+INTO\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)', re.IGNORECASE)
RE_TBL_UPDATE = re.compile(r'\bUPDATE\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)\s+SET', re.IGNORECASE)
RE_TBL_DELETE = re.compile(r'\bDELETE\s+FROM\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)', re.IGNORECASE)
RE_EXEC_SP = re.compile(r'\b(?:EXEC|EXECUTE)\s+(?:\[?dbo\]?\.)?(\[?\w+\]?)', re.IGNORECASE)

# Tablas a ignorar (palabras clave SQL, alias, etc)
TBL_IGNORE = {
    "SELECT", "WHERE", "AND", "OR", "ON", "AS", "WITH", "CASE",
    "BEGIN", "END", "IF", "ELSE", "TOP", "DISTINCT", "ORDER",
    "GROUP", "HAVING", "INNER", "OUTER", "LEFT", "RIGHT", "FULL",
    "CROSS", "APPLY", "TABLE", "ROW", "ROWS", "NULL", "NOT",
    "EXEC", "EXECUTE", "RETURN", "INTO", "VALUES", "OUTPUT",
    "UNION", "ALL", "EXCEPT", "INTERSECT", "CTE", "TEMP",
    "@TABLE", "INSERTED", "DELETED",
}


def normalize_tbl(t):
    if not t:
        return None
    t = t.strip("[]").strip()
    if not t or t.upper() in TBL_IGNORE:
        return None
    if len(t) < 2 or len(t) > 80:
        return None
    if t.startswith("@") or t.startswith("#"):
        return None
    return t


def parse_file(args):
    full_path, rel_path, ext, checkout = args
    try:
        with open(full_path, "rb") as f:
            content = f.read()
    except (OSError, IOError) as e:
        return None

    h = hashlib.sha256(content).hexdigest()[:16]
    size = len(content)

    # decode
    try:
        text = content.decode("utf-8")
    except UnicodeDecodeError:
        try:
            text = content.decode("latin-1")
        except Exception:
            return None

    lines = text.count("\n") + 1

    # Funciones publicas
    funcs = []
    if ext == ".vb":
        for m in RE_VB_FUNC.finditer(text):
            funcs.append(f"{m.group(1)}:{m.group(2)}")
    elif ext == ".cs":
        for m in RE_CS_FUNC.finditer(text):
            funcs.append(f"method:{m.group(1)}")
        for m in RE_CS_CLASS.finditer(text):
            funcs.append(f"{m.group(1)}:{m.group(2)}")
    elif ext == ".java":
        for m in RE_JAVA_FUNC.finditer(text):
            funcs.append(f"method:{m.group(1)}")
        for m in RE_JAVA_CLASS.finditer(text):
            funcs.append(f"{m.group(1)}:{m.group(2)}")
    elif ext == ".sql":
        for m in RE_SQL_PROC.finditer(text):
            funcs.append(f"{m.group(1).lower()}:{m.group(2).strip('[]')}")

    # Tablas tocadas
    tablas = set()
    for rx in (RE_TBL_FROM, RE_TBL_JOIN, RE_TBL_INSERT, RE_TBL_UPDATE, RE_TBL_DELETE):
        for m in rx.finditer(text):
            tn = normalize_tbl(m.group(1))
            if tn:
                tablas.add(tn)

    # SPs invocados
    sps = set()
    for m in RE_EXEC_SP.finditer(text):
        sn = normalize_tbl(m.group(1))
        if sn and (sn.lower().startswith("sp_") or "_sp" in sn.lower() or "proc_" in sn.lower()):
            sps.add(sn)
        elif sn:
            sps.add(sn)  # cualquier identificador post EXEC

    # Flags consultados
    flags_used = set()
    for flag in FLAGS:
        if flag in text:
            flags_used.add(flag)

    return {
        "checkout": checkout,
        "rel_path": rel_path,
        "ext": ext,
        "size_bytes": size,
        "lines": lines,
        "hash16": h,
        "funcs_count": len(funcs),
        "funcs": "|".join(funcs[:50]),  # cap a 50 por archivo
        "tablas_count": len(tablas),
        "tablas": "|".join(sorted(tablas)[:30]),
        "sps_count": len(sps),
        "sps": "|".join(sorted(sps)[:20]),
        "flags_count": len(flags_used),
        "flags": "|".join(sorted(flags_used)),
    }


def collect_files(checkout_root, checkout_name):
    items = []
    base = Path(checkout_root)
    for f in base.rglob("*"):
        if not f.is_file():
            continue
        if "/.git/" in str(f):
            continue
        ext = f.suffix.lower()
        if ext not in EXTENSIONS:
            continue
        rel = str(f.relative_to(base))
        items.append((str(f), rel, ext, checkout_name))
    return items


def main():
    checkouts = [
        ("/tmp/wms-deep-dive/repos/BOF-2023", "BOF-2023"),
        ("/tmp/wms-deep-dive/repos/BOF-2028", "BOF-2028"),
        ("/tmp/wms-deep-dive/repos/HH-2023", "HH-2023"),
        ("/tmp/wms-deep-dive/repos/HH-2028", "HH-2028"),
    ]
    out_dir = Path("/tmp/wms-deep-dive/work")
    out_dir.mkdir(parents=True, exist_ok=True)

    fields = [
        "checkout", "rel_path", "ext", "size_bytes", "lines", "hash16",
        "funcs_count", "funcs", "tablas_count", "tablas",
        "sps_count", "sps", "flags_count", "flags",
    ]

    total_files = 0
    grand_summary = []

    for root, name in checkouts:
        print(f"\n=== {name} ===")
        items = collect_files(root, name)
        print(f"  archivos a parsear: {len(items)}")

        out_csv = out_dir / f"inventario_{name}.csv"
        rows = []

        with ProcessPoolExecutor(max_workers=os.cpu_count() or 4) as ex:
            futures = [ex.submit(parse_file, it) for it in items]
            for fut in as_completed(futures):
                r = fut.result()
                if r:
                    rows.append(r)

        with open(out_csv, "w", newline="", encoding="utf-8") as f:
            w = csv.DictWriter(f, fieldnames=fields)
            w.writeheader()
            for r in rows:
                w.writerow(r)

        total_lines = sum(r["lines"] for r in rows)
        total_funcs = sum(r["funcs_count"] for r in rows)
        unique_tablas = set()
        unique_sps = set()
        for r in rows:
            for t in r["tablas"].split("|"):
                if t:
                    unique_tablas.add(t)
            for s in r["sps"].split("|"):
                if s:
                    unique_sps.add(s)
        flag_hits = sum(r["flags_count"] for r in rows)

        summary = {
            "checkout": name,
            "archivos": len(rows),
            "lineas_total": total_lines,
            "funcs_total": total_funcs,
            "tablas_unicas": len(unique_tablas),
            "sps_unicos": len(unique_sps),
            "flag_hits": flag_hits,
        }
        grand_summary.append(summary)
        print(f"  archivos OK: {len(rows)}")
        print(f"  lineas: {total_lines:,}")
        print(f"  funcs: {total_funcs:,}")
        print(f"  tablas unicas: {len(unique_tablas)}")
        print(f"  sps unicos: {len(unique_sps)}")
        print(f"  flag hits: {flag_hits}")
        print(f"  CSV: {out_csv}")
        total_files += len(rows)

    # Summary CSV
    sum_csv = out_dir / "_summary.csv"
    with open(sum_csv, "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=["checkout","archivos","lineas_total","funcs_total","tablas_unicas","sps_unicos","flag_hits"])
        w.writeheader()
        for s in grand_summary:
            w.writerow(s)

    print(f"\n=== TOTAL: {total_files} archivos parseados ===")
    print(f"Summary: {sum_csv}")


if __name__ == "__main__":
    main()
