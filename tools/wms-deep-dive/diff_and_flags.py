#!/usr/bin/env python3
"""
F2: Diff 2023 vs 2028 (BOF y HH) usando los inventarios CSV.
F3: Mapa flag → callsites a partir de los inventarios.
"""
import csv
from collections import defaultdict, Counter
from pathlib import Path

WORK = Path("/tmp/wms-deep-dive/work")


def load_inventory(name):
    """Devuelve dict {rel_path: row}."""
    rows = {}
    with open(WORK / f"inventario_{name}.csv", encoding="utf-8") as f:
        for r in csv.DictReader(f):
            rows[r["rel_path"]] = r
    return rows


def diff_checkouts(inv_a, inv_b, label_a, label_b):
    """Devuelve dict con added/removed/modified/identical."""
    paths_a = set(inv_a.keys())
    paths_b = set(inv_b.keys())

    added = sorted(paths_b - paths_a)
    removed = sorted(paths_a - paths_b)

    modified = []
    identical = []
    for p in sorted(paths_a & paths_b):
        a = inv_a[p]
        b = inv_b[p]
        if a["hash16"] == b["hash16"]:
            identical.append(p)
        else:
            funcs_a = set(a["funcs"].split("|")) if a["funcs"] else set()
            funcs_b = set(b["funcs"].split("|")) if b["funcs"] else set()
            tablas_a = set(a["tablas"].split("|")) if a["tablas"] else set()
            tablas_b = set(b["tablas"].split("|")) if b["tablas"] else set()
            modified.append({
                "path": p,
                "ext": a["ext"],
                "lines_a": int(a["lines"]),
                "lines_b": int(b["lines"]),
                "delta_lines": int(b["lines"]) - int(a["lines"]),
                "funcs_added": sorted(funcs_b - funcs_a),
                "funcs_removed": sorted(funcs_a - funcs_b),
                "tablas_added": sorted(tablas_b - tablas_a),
                "tablas_removed": sorted(tablas_a - tablas_b),
            })

    return {"added": added, "removed": removed, "modified": modified, "identical": identical}


def write_diff_md(repo, diff, label_a, label_b, out_path):
    """Escribe un MD con el diff."""
    added = diff["added"]
    removed = diff["removed"]
    modified = diff["modified"]
    identical = diff["identical"]

    # Top archivos modificados por delta funciones
    mod_sorted = sorted(modified,
                        key=lambda m: -(len(m["funcs_added"]) + len(m["funcs_removed"])))

    with open(out_path, "w", encoding="utf-8") as f:
        f.write(f"---\n")
        f.write(f"id: DIFF-{repo}-2023-VS-2028\n")
        f.write(f"tipo: code-deep-flow\n")
        f.write(f"estado: vigente\n")
        f.write(f"ramas: [{label_a}, {label_b}]\n")
        f.write(f"tags: [code-deep-flow/diff, repo/{repo.lower()}]\n")
        f.write(f"---\n\n")
        f.write(f"# Diff {repo}: `{label_a}` vs `{label_b}`\n\n")
        f.write(f"> Generado por F2 del Atlas BOF/HH 2023↔2028 cliente-aware (2026-04-30).\n")
        f.write(f"> Fuente: parsing exhaustivo de los 4 checkouts (ver `tools/wms-deep-dive/`).\n\n")

        f.write("## Resumen ejecutivo\n\n")
        f.write(f"| Categoría | Conteo |\n|---|---:|\n")
        f.write(f"| Archivos solo en `{label_a}` (eliminados) | **{len(removed)}** |\n")
        f.write(f"| Archivos solo en `{label_b}` (nuevos) | **{len(added)}** |\n")
        f.write(f"| Archivos modificados (mismo path, distinto hash) | **{len(modified)}** |\n")
        f.write(f"| Archivos idénticos (sin cambios entre ramas) | **{len(identical)}** |\n\n")

        # Archivos nuevos
        if added:
            # agrupar por top dir
            by_dir = defaultdict(list)
            for p in added:
                top = p.split("/", 1)[0] if "/" in p else "(root)"
                by_dir[top].append(p)
            f.write(f"## Archivos NUEVOS en `{label_b}` ({len(added)})\n\n")
            f.write("Agrupados por carpeta de primer nivel:\n\n")
            f.write("| Carpeta | # archivos | Ejemplos |\n|---|---:|---|\n")
            for d in sorted(by_dir, key=lambda k: -len(by_dir[k])):
                ex = by_dir[d][:3]
                f.write(f"| `{d}` | {len(by_dir[d])} | {', '.join('`'+e.split('/')[-1]+'`' for e in ex)} |\n")
            f.write("\n")

        # Archivos eliminados
        if removed:
            by_dir = defaultdict(list)
            for p in removed:
                top = p.split("/", 1)[0] if "/" in p else "(root)"
                by_dir[top].append(p)
            f.write(f"## Archivos ELIMINADOS en `{label_b}` ({len(removed)})\n\n")
            f.write("| Carpeta | # archivos | Ejemplos |\n|---|---:|---|\n")
            for d in sorted(by_dir, key=lambda k: -len(by_dir[k])):
                ex = by_dir[d][:3]
                f.write(f"| `{d}` | {len(by_dir[d])} | {', '.join('`'+e.split('/')[-1]+'`' for e in ex)} |\n")
            f.write("\n")

        # Top 30 archivos modificados con más cambios funcionales
        if mod_sorted:
            f.write("## TOP 30 archivos con más cambios estructurales\n\n")
            f.write("Ordenados por |funcs agregadas| + |funcs eliminadas|.\n\n")
            f.write("| Archivo | ext | Δ líneas | + funcs | - funcs | + tablas | - tablas |\n")
            f.write("|---|---|---:|---:|---:|---:|---:|\n")
            for m in mod_sorted[:30]:
                f.write(f"| `{m['path']}` | {m['ext']} | {m['delta_lines']:+d} "
                        f"| {len(m['funcs_added'])} | {len(m['funcs_removed'])} "
                        f"| {len(m['tablas_added'])} | {len(m['tablas_removed'])} |\n")
            f.write("\n")

        # Detalle de los TOP 10 con funciones específicas
        f.write("## Detalle TOP 10 (con funciones puntuales)\n\n")
        for m in mod_sorted[:10]:
            f.write(f"### `{m['path']}`\n")
            f.write(f"- Extensión: `{m['ext']}`\n")
            f.write(f"- Líneas: {m['lines_a']:,} → {m['lines_b']:,} ({m['delta_lines']:+,})\n")
            if m['funcs_added']:
                f.write(f"- **Funciones agregadas en 2028** ({len(m['funcs_added'])}):\n")
                for fn in m['funcs_added'][:15]:
                    f.write(f"  - `{fn}`\n")
                if len(m['funcs_added']) > 15:
                    f.write(f"  - _(+{len(m['funcs_added'])-15} más)_\n")
            if m['funcs_removed']:
                f.write(f"- **Funciones removidas en 2028** ({len(m['funcs_removed'])}):\n")
                for fn in m['funcs_removed'][:15]:
                    f.write(f"  - `{fn}`\n")
                if len(m['funcs_removed']) > 15:
                    f.write(f"  - _(+{len(m['funcs_removed'])-15} más)_\n")
            if m['tablas_added']:
                f.write(f"- Tablas nuevas tocadas: {', '.join('`'+t+'`' for t in m['tablas_added'][:10])}\n")
            if m['tablas_removed']:
                f.write(f"- Tablas dejaron de usarse: {', '.join('`'+t+'`' for t in m['tablas_removed'][:10])}\n")
            f.write("\n")

    return out_path


def build_flags_callsites():
    """F3: para cada flag, listar archivos:checkout que lo consultan."""
    inv = {
        "BOF-2023": load_inventory("BOF-2023"),
        "BOF-2028": load_inventory("BOF-2028"),
        "HH-2023": load_inventory("HH-2023"),
        "HH-2028": load_inventory("HH-2028"),
    }

    # flag -> {checkout -> [paths]}
    flag_map = defaultdict(lambda: defaultdict(list))
    for ck, rows in inv.items():
        for path, row in rows.items():
            if not row["flags"]:
                continue
            flags = row["flags"].split("|")
            for fl in flags:
                if fl:
                    flag_map[fl][ck].append(path)

    out = WORK / "flags_callsites.md"
    with open(out, "w", encoding="utf-8") as f:
        f.write("---\n")
        f.write("id: FLAGS-CALLSITES\n")
        f.write("tipo: reference\n")
        f.write("estado: vigente\n")
        f.write("tags: [reference/flags, code-deep-flow/callsites]\n")
        f.write("---\n\n")
        f.write("# Flags `i_nav_config_enc` — callsites en código\n\n")
        f.write("> Generado por F3 del Atlas BOF/HH 2023↔2028 (2026-04-30).\n")
        f.write("> Para cada flag, lista los archivos que lo mencionan en cada rama.\n")
        f.write("> Permite responder: ¿qué se rompe si activamos/desactivamos este flag?\n\n")

        # Resumen
        f.write("## Resumen — flags más referenciados\n\n")
        f.write("| Flag | BOF-2023 | BOF-2028 | Δ | HH-2023 | HH-2028 |\n")
        f.write("|---|---:|---:|---:|---:|---:|\n")
        flag_totals = []
        for fl in flag_map:
            totals = {ck: len(flag_map[fl][ck]) for ck in inv}
            flag_totals.append((fl, totals))
        flag_totals.sort(key=lambda x: -x[1]["BOF-2028"])
        for fl, t in flag_totals:
            delta = t["BOF-2028"] - t["BOF-2023"]
            delta_s = f"{delta:+d}" if delta else "0"
            f.write(f"| `{fl}` | {t['BOF-2023']} | {t['BOF-2028']} | {delta_s} "
                    f"| {t['HH-2023']} | {t['HH-2028']} |\n")
        f.write("\n")

        # Detalle por flag
        f.write("## Detalle por flag\n\n")
        for fl, t in flag_totals:
            f.write(f"### `{fl}`\n\n")
            for ck in ["BOF-2023", "BOF-2028", "HH-2023", "HH-2028"]:
                paths = flag_map[fl][ck]
                if paths:
                    f.write(f"**{ck}** ({len(paths)} archivos):\n")
                    for p in paths[:10]:
                        f.write(f"- `{p}`\n")
                    if len(paths) > 10:
                        f.write(f"- _(+{len(paths)-10} más)_\n")
                    f.write("\n")
            f.write("---\n\n")

    return out


def main():
    print("=== F2: Diffs 2023 vs 2028 ===")
    bof_2023 = load_inventory("BOF-2023")
    bof_2028 = load_inventory("BOF-2028")
    hh_2023 = load_inventory("HH-2023")
    hh_2028 = load_inventory("HH-2028")

    print(f"\nBOF: {len(bof_2023)} → {len(bof_2028)}")
    diff_bof = diff_checkouts(bof_2023, bof_2028, "dev_2023_estable", "dev_2028_merge")
    print(f"  +{len(diff_bof['added'])} -{len(diff_bof['removed'])} ~{len(diff_bof['modified'])} ={len(diff_bof['identical'])}")
    out = write_diff_md("BOF", diff_bof, "dev_2023_estable", "dev_2028_merge",
                        WORK / "DIFF-BOF-2023-VS-2028.md")
    print(f"  → {out}")

    print(f"\nHH: {len(hh_2023)} → {len(hh_2028)}")
    diff_hh = diff_checkouts(hh_2023, hh_2028, "dev_2023_estable", "dev_2028_merge")
    print(f"  +{len(diff_hh['added'])} -{len(diff_hh['removed'])} ~{len(diff_hh['modified'])} ={len(diff_hh['identical'])}")
    out = write_diff_md("HH", diff_hh, "dev_2023_estable", "dev_2028_merge",
                        WORK / "DIFF-HH-2023-VS-2028.md")
    print(f"  → {out}")

    print("\n=== F3: Flags callsites ===")
    out = build_flags_callsites()
    print(f"  → {out}")
    print("\nDONE")


if __name__ == "__main__":
    main()
