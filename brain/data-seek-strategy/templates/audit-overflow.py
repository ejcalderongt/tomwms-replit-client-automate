# -*- coding: utf-8 -*-
"""
audit-overflow.py — Detecta valores absurdos en columnas numéricas.

Uso:
    python audit-overflow.py [tabla] [columna] [pk]

Default: corre el set de auditorías conocidas (cantidad_original/nueva en
trans_ajuste_det, cantidad en trans_movimientos, cantidad en stock).

Estrategia:
- Lista los TOP 20 más altos.
- Marca como SOSPECHOSO si valor > 1,000,000 (umbral duro) o > 100x el percentil 99.
- Lista también las fechas anómalas (< 2010 o > año actual + 5).
"""
import sys
from _db import q
from csv_helpers import csv_out

UMBRAL_DURO = 1_000_000.0
FACTOR_P99 = 100

DEFAULTS = [
    # (tabla, col_cantidad, pk, col_fecha_opcional)
    ('trans_ajuste_det', 'cantidad_original', 'idajustedet', None),
    ('trans_ajuste_det', 'cantidad_nueva', 'idajustedet', None),
    ('trans_movimientos', 'cantidad', 'IdMovimiento', 'fecha_agr'),
    ('trans_movimientos', 'cantidad_hist', 'IdMovimiento', 'fecha_agr'),
    ('stock', 'cantidad', 'IdStock', None),
]


def audit(tabla: str, col: str, pk: str, col_fecha: str = None, out_dir: str = 'outputs/audit'):
    print(f"\n=== {tabla}.{col} ===")
    # Stats básicas
    stats = q(f"""
        SELECT MIN({col}) min_v, MAX({col}) max_v, AVG({col}*1.0) avg_v,
               COUNT(*) n, SUM(CASE WHEN {col} > {UMBRAL_DURO} THEN 1 ELSE 0 END) n_sospechosos
        FROM {tabla} WITH (NOLOCK)
    """)[0]
    print(f"  rows={stats['n']:,} min={stats['min_v']} max={stats['max_v']:,} avg={stats['avg_v']:.2f}")
    print(f"  sospechosos (>{UMBRAL_DURO:,}): {stats['n_sospechosos']}")

    # Top 20 outliers
    extra = f", {col_fecha}" if col_fecha else ""
    rows = q(f"SELECT TOP 20 {pk}, {col}{extra} FROM {tabla} WITH (NOLOCK) WHERE {col} IS NOT NULL ORDER BY {col} DESC")
    csv_out(f"{out_dir}/overflow-{tabla}-{col}-top.csv", rows)

    # Fechas anómalas
    if col_fecha:
        anom_fechas = q(f"""
            SELECT TOP 50 {pk}, {col_fecha}, {col}
            FROM {tabla} WITH (NOLOCK)
            WHERE {col_fecha} < '2010-01-01' OR {col_fecha} > DATEADD(YEAR, 5, GETDATE())
            ORDER BY {col_fecha}
        """)
        csv_out(f"{out_dir}/overflow-{tabla}-fechas-anomalas.csv", anom_fechas)


def main():
    args = sys.argv[1:]
    if len(args) >= 3:
        audit(args[0], args[1], args[2], args[3] if len(args) > 3 else None)
    else:
        for t, c, pk, f in DEFAULTS:
            audit(t, c, pk, f)


if __name__ == '__main__':
    main()
