"""
Helper de conexion READ-ONLY a TOMWMS_KILLIOS_PRD_2026.

Requisitos:
  - pip install pymssql
  - env: WMS_DB_USER, WMS_KILLIOS_DB_PASSWORD

Uso:
  from _db import q
  rows = q("SELECT TOP 1 IdProducto, Codigo FROM producto")
"""
import os, pymssql

SERVER   = "52.41.114.122"
PORT     = 1437
DATABASE = "TOMWMS_KILLIOS_PRD_2026"
USER     = os.environ["WMS_DB_USER"]
PASSWD   = os.environ["WMS_KILLIOS_DB_PASSWORD"]

def conn():
    return pymssql.connect(server=SERVER, port=PORT, user=USER,
                           password=PASSWD, database=DATABASE, as_dict=True)

def q(sql, params=None):
    with conn() as c, c.cursor() as cur:
        if params is None:
            cur.execute(sql)
        else:
            cur.execute(sql, params)
        try:
            return cur.fetchall()
        except Exception:
            return None
