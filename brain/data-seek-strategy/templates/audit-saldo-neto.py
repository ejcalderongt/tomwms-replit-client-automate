# -*- coding: utf-8 -*-
"""
audit-saldo-neto.py — Lista productos con stock < reservas vivas.

Joina stock vivas (activo=1) contra stock_res (reservas zombi-filtradas)
y reporta cualquier producto con neto < 0.

Uso:
    python audit-saldo-neto.py
"""
from _db import q
from csv_helpers import csv_out


def main(out_dir: str = 'outputs/audit'):
    print("=== Saldo neto stock - reservas (productos con neto < 0) ===")
    rows = q("""
        WITH stk AS (
            SELECT IdProductoBodega, SUM(cantidad) tot
            FROM stock WITH (NOLOCK) WHERE activo=1 GROUP BY IdProductoBodega
        ),
        res AS (
            SELECT s.IdProductoBodega, SUM(sr.cantidad) tot
            FROM stock_res sr WITH (NOLOCK)
            JOIN stock s WITH (NOLOCK) ON s.IdStock = sr.IdStock
            WHERE s.activo = 1
            GROUP BY s.IdProductoBodega
        )
        SELECT TOP 200 stk.IdProductoBodega, p.codigo, p.nombre,
               stk.tot stock_total, ISNULL(res.tot,0) reservas_total,
               stk.tot - ISNULL(res.tot,0) neto
        FROM stk
        LEFT JOIN res ON res.IdProductoBodega = stk.IdProductoBodega
        LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = stk.IdProductoBodega
        LEFT JOIN producto p ON p.IdProducto = pb.IdProducto
        WHERE stk.tot - ISNULL(res.tot,0) < 0
        ORDER BY 6
    """)
    print(f"  productos con neto<0: {len(rows)}")
    csv_out(f"{out_dir}/saldo-neto-negativo.csv", rows)

    # Histograma de saldos para contexto
    hist = q("""
        WITH stk AS (
            SELECT IdProductoBodega, SUM(cantidad) tot
            FROM stock WITH (NOLOCK) WHERE activo=1 GROUP BY IdProductoBodega
        ),
        res AS (
            SELECT s.IdProductoBodega, SUM(sr.cantidad) tot
            FROM stock_res sr WITH (NOLOCK)
            JOIN stock s WITH (NOLOCK) ON s.IdStock = sr.IdStock
            WHERE s.activo = 1 GROUP BY s.IdProductoBodega
        )
        SELECT
          SUM(CASE WHEN stk.tot - ISNULL(res.tot,0) < 0 THEN 1 ELSE 0 END) negativos,
          SUM(CASE WHEN stk.tot - ISNULL(res.tot,0) = 0 THEN 1 ELSE 0 END) cero,
          SUM(CASE WHEN stk.tot - ISNULL(res.tot,0) > 0 THEN 1 ELSE 0 END) positivos
        FROM stk LEFT JOIN res ON res.IdProductoBodega = stk.IdProductoBodega
    """)[0]
    print(f"  histograma: neg={hist['negativos']} cero={hist['cero']} pos={hist['positivos']}")


if __name__ == '__main__':
    main()
