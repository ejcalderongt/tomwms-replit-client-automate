#!/usr/bin/env python3
import argparse
import sys
try:
    import pyodbc
except Exception as exc:
    print(f"pyodbc not available: {exc}")
    sys.exit(2)

PROCESS_TABLE = {
    "picking": "trans_picking_enc",
    "recepcion": "trans_re_enc",
    "packing": "trans_packing_enc",
    "verificacion": "trans_picking_enc",
    "inventario": "trans_inv_enc",
    "existencias": "trans_picking_enc",
    "cambio_ubicacion": "trans_tras_enc",
}

def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--server", required=True)
    ap.add_argument("--database", required=True)
    ap.add_argument("--user", required=True)
    ap.add_argument("--password", required=True)
    ap.add_argument("--process", required=True)
    args = ap.parse_args()
    table = PROCESS_TABLE.get(args.process, "trans_picking_enc")
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 17 for SQL Server};"
        f"SERVER={args.server};DATABASE={args.database};UID={args.user};PWD={args.password};Encrypt=no;"
    )
    cur = conn.cursor()
    sql = f"SELECT TOP 50 estado, COUNT(1) total FROM {table} GROUP BY estado ORDER BY total DESC"
    cur.execute(sql)
    print("estado,total")
    for row in cur.fetchall():
        print(f"{row[0]},{row[1]}")
    conn.close()

if __name__ == "__main__":
    main()

