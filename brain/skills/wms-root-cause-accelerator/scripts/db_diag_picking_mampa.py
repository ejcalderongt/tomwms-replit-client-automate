#!/usr/bin/env python3
"""
#EJC20260527
Diagnostico rapido de picking en MAMPA para BOF/HH.

Uso:
  python db_diag_picking_mampa.py --server 10.238.26.70 --database TOMWMS_MAMPA_DEV --user sa --password "***" --picking 108
"""

import argparse
import sys

try:
    import pyodbc
except Exception as exc:  # pragma: no cover
    print(f"pyodbc no disponible: {exc}")
    sys.exit(2)


def query_rows(cur, sql, params=()):
    cur.execute(sql, params)
    cols = [d[0] for d in cur.description]
    rows = cur.fetchall()
    return cols, rows


def print_table(title, cols, rows):
    print(f"\n=== {title} ===")
    print(" | ".join(cols))
    for row in rows:
        print(" | ".join("" if v is None else str(v) for v in row))
    if not rows:
        print("(sin filas)")


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--server", required=True)
    ap.add_argument("--database", required=True)
    ap.add_argument("--user", required=True)
    ap.add_argument("--password", required=True)
    ap.add_argument("--picking", type=int, required=True)
    args = ap.parse_args()

    conn_str = (
        "DRIVER={ODBC Driver 17 for SQL Server};"
        f"SERVER={args.server};DATABASE={args.database};UID={args.user};PWD={args.password};"
        "Encrypt=no;"
    )

    with pyodbc.connect(conn_str, timeout=15) as conn:
        cur = conn.cursor()

        cols, rows = query_rows(
            cur,
            """
            SELECT TOP 1 IdPickingEnc, estado, verifica_auto, procesado_bof, requiere_preparacion, fotografia_verificacion, fec_mod
            FROM trans_picking_enc
            WHERE IdPickingEnc = ?
            """,
            (args.picking,),
        )
        print_table("Picking Enc", cols, rows)

        cols, rows = query_rows(
            cur,
            """
            SELECT IdPickingUbic, IdStockRes, cantidad_solicitada, ISNULL(cantidad_recibida,0) cant_recibida,
                   ISNULL(cantidad_verificada,0) cant_verificada, encontrado, dañado_verificacion,
                   no_encontrado, fecha_verificado, fecha_despachado
            FROM trans_picking_ubic
            WHERE IdPickingEnc = ?
            ORDER BY IdPickingUbic
            """,
            (args.picking,),
        )
        print_table("Picking Ubic", cols, rows)

        cols, rows = query_rows(
            cur,
            """
            SELECT
                SUM(CASE WHEN ISNULL(cantidad_verificada,0) > 0 THEN 1 ELSE 0 END) lineas_verificadas,
                SUM(CASE WHEN ISNULL(cantidad_verificada,0) = 0 THEN 1 ELSE 0 END) lineas_no_verificadas,
                SUM(CASE WHEN ISNULL(no_encontrado,0) = 1 THEN 1 ELSE 0 END) lineas_no_encontradas
            FROM trans_picking_ubic
            WHERE IdPickingEnc = ?
            """,
            (args.picking,),
        )
        print_table("Resumen", cols, rows)


if __name__ == "__main__":
    main()
