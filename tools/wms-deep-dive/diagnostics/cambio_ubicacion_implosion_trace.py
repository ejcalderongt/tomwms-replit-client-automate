#!/usr/bin/env python3
"""
Diagnostico rapido de cambio de ubicacion + implosion por licencia.
"""

from __future__ import annotations

import argparse
import datetime as dt
import json
from typing import Any, Dict, List

import pyodbc


def fetch_all_dict(cur: pyodbc.Cursor, sql: str, params: tuple = ()) -> List[Dict[str, Any]]:
    cur.execute(sql, params)
    cols = [c[0] for c in cur.description] if cur.description else []
    rows = cur.fetchall()
    return [{cols[i]: r[i] for i in range(len(cols))} for r in rows]


def build_conn(args: argparse.Namespace) -> pyodbc.Connection:
    cstr = (
        "DRIVER={ODBC Driver 17 for SQL Server};"
        f"SERVER={args.server};DATABASE={args.database};UID={args.user};PWD={args.password};"
        "TrustServerCertificate=yes;"
    )
    return pyodbc.connect(cstr, timeout=30)


def run(args: argparse.Namespace) -> Dict[str, Any]:
    result: Dict[str, Any] = {
        "meta": {
            "timestamp": dt.datetime.now().isoformat(),
            "server": args.server,
            "database": args.database,
            "bodega": args.bodega,
            "lic_origen": args.lic_origen,
            "lic_destino": args.lic_destino,
            "hours": args.hours,
        }
    }

    with build_conn(args) as con:
        cur = con.cursor()
        result["bodega_parametros"] = fetch_all_dict(
            cur,
            """
            SELECT IdBodega, ubic_implosion_auto, cambio_ubicacion_auto, inferir_origen_en_cambio_ubic
            FROM bodega WHERE IdBodega = ?
            """,
            (args.bodega,),
        )
        result["stock_licencias"] = fetch_all_dict(
            cur,
            """
            SELECT IdStock, IdProductoBodega, IdUbicacion, IdUbicacion_anterior, lic_plate, cantidad, activo, fec_agr, fec_mod
            FROM stock
            WHERE IdBodega = ? AND (lic_plate = ? OR lic_plate = ?)
            ORDER BY IdStock DESC
            """,
            (args.bodega, args.lic_origen, args.lic_destino),
        )
        result["movimientos_recientes"] = fetch_all_dict(
            cur,
            """
            SELECT TOP 200 IdMovimiento, IdTransaccion, IdProductoBodega, IdUbicacionOrigen, IdUbicacionDestino,
                           cantidad, lic_plate, fecha, usuario_agr
            FROM trans_movimientos
            WHERE IdBodegaOrigen = ?
              AND fecha >= DATEADD(HOUR, -?, GETDATE())
              AND (lic_plate = ? OR lic_plate = ? OR IdUbicacionOrigen IN (112,113) OR IdUbicacionDestino IN (112,113))
            ORDER BY IdMovimiento DESC
            """,
            (args.bodega, args.hours, args.lic_origen, args.lic_destino),
        )

    return result


def parse_args() -> argparse.Namespace:
    p = argparse.ArgumentParser()
    p.add_argument("--server", required=True)
    p.add_argument("--database", required=True)
    p.add_argument("--user", required=True)
    p.add_argument("--password", required=True)
    p.add_argument("--bodega", type=int, required=True)
    p.add_argument("--lic-origen", required=True)
    p.add_argument("--lic-destino", required=True)
    p.add_argument("--hours", type=int, default=48)
    p.add_argument("--out-json", default="")
    return p.parse_args()


def main() -> None:
    args = parse_args()
    data = run(args)
    if args.out_json:
        with open(args.out_json, "w", encoding="utf-8") as f:
            json.dump(data, f, ensure_ascii=False, indent=2, default=str)
        print(args.out_json)
    else:
        print(json.dumps(data, ensure_ascii=False, indent=2, default=str))


if __name__ == "__main__":
    main()

