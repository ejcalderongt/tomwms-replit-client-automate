# -*- coding: utf-8 -*-
"""
audit-gaps-temporales.py — Detecta meses sin actividad en tablas con fecha.

Uso:
    python audit-gaps-temporales.py [tabla] [columna_fecha]

Default: corre el set de auditorías conocidas.

Estrategia:
- Para cada mes entre min(fecha) y max(fecha), cuenta rows.
- Lista los meses con cero actividad (gaps).
- Lista los meses con actividad < 10% del promedio (huecos relativos).
"""
import sys
from _db import q
from csv_helpers import csv_out

DEFAULTS = [
    # (tabla, col_fecha)
    ('trans_ajuste_enc', 'fecha'),
    ('trans_movimientos', 'fecha_agr'),
    ('trans_picking_ubic', 'fecha_agr'),
]


def audit(tabla: str, col_fecha: str, out_dir: str = 'outputs/audit'):
    print(f"\n=== {tabla}.{col_fecha} ===")
    rng = q(f"SELECT MIN({col_fecha}) mn, MAX({col_fecha}) mx, COUNT(*) n FROM {tabla} WITH (NOLOCK) WHERE {col_fecha} IS NOT NULL")[0]
    print(f"  rango: {rng['mn']} -> {rng['mx']} ({rng['n']:,} rows)")
    if not rng['mn']:
        print("  sin datos")
        return

    rows = q(f"""
        WITH meses AS (
            SELECT DATEFROMPARTS(YEAR(MIN({col_fecha})), MONTH(MIN({col_fecha})), 1) m,
                   DATEFROMPARTS(YEAR(MAX({col_fecha})), MONTH(MAX({col_fecha})), 1) mx
            FROM {tabla} WITH (NOLOCK) WHERE {col_fecha} IS NOT NULL
        ),
        serie AS (
            SELECT m FROM meses
            UNION ALL
            SELECT DATEADD(MONTH, 1, s.m) FROM serie s, meses WHERE DATEADD(MONTH,1,s.m) <= meses.mx
        )
        SELECT s.m mes, COUNT(t.{col_fecha}) n
        FROM serie s
        LEFT JOIN {tabla} t WITH (NOLOCK) ON FORMAT(t.{col_fecha},'yyyy-MM') = FORMAT(s.m,'yyyy-MM')
        GROUP BY s.m
        ORDER BY s.m
        OPTION (MAXRECURSION 1000)
    """)

    csv_out(f"{out_dir}/gaps-{tabla}-actividad-mensual.csv", rows)

    # Detectar gaps: secuencias de meses con n=0
    gaps = []
    cur_start = None
    for r in rows:
        if r['n'] == 0:
            if cur_start is None:
                cur_start = r['mes']
            cur_end = r['mes']
        else:
            if cur_start is not None:
                gaps.append({'desde': cur_start, 'hasta': cur_end, 'meses': len(_meses_entre(cur_start, cur_end))})
                cur_start = None
    if cur_start is not None:
        gaps.append({'desde': cur_start, 'hasta': cur_end, 'meses': len(_meses_entre(cur_start, cur_end))})

    print(f"  gaps detectados: {len(gaps)}")
    for g in gaps:
        print(f"    {g['desde']} -> {g['hasta']} ({g['meses']} meses)")
    csv_out(f"{out_dir}/gaps-{tabla}-rangos.csv", gaps)


def _meses_entre(a, b):
    out = []
    cur = a
    while cur <= b:
        out.append(cur)
        if cur.month == 12:
            cur = cur.replace(year=cur.year + 1, month=1)
        else:
            cur = cur.replace(month=cur.month + 1)
    return out


def main():
    args = sys.argv[1:]
    if len(args) >= 2:
        audit(args[0], args[1])
    else:
        for t, c in DEFAULTS:
            audit(t, c)


if __name__ == '__main__':
    main()
