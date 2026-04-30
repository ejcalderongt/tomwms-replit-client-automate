# -*- coding: utf-8 -*-
"""
Conexión read-only a SQL Server WMS via pymssql.

Uso simple (en el directorio del caso, con env vars seteadas):
    from _db import q
    rows = q("SELECT TOP 10 * FROM stock WITH (NOLOCK)")

Uso con yml (desde client-index/replicate.py):
    import _db
    _db.configure(host='52.41.114.122,1437', user_env='WMS_DB_USER',
                  password_env='WMS_KILLIOS_DB_PASSWORD',
                  database='TOMWMS_KILLIOS_PRD_2026')
    from _db import q

Defaults conocidos (se aplican si no se sobrescribe):
    host: 52.41.114.122,1437
    user_env: WMS_DB_USER
    password_env: WMS_KILLIOS_DB_PASSWORD
    database: TOMWMS_KILLIOS_PRD_2026

Bloquea explícitamente cualquier statement que NO sea SELECT/WITH/EXEC sp_help/sp_columns/sp_helptext.
Si necesitás escribir, NO uses este helper.
"""
import os
import re
import sys

try:
    import pymssql
except ImportError:
    print("ERROR: pymssql no instalado. pip install pymssql", file=sys.stderr)
    sys.exit(1)

# Defaults razonables (los del cliente Killios, conocidos al momento de cerrar CP-013)
_CFG = {
    'host': os.environ.get('WMS_DB_HOST', '52.41.114.122,1437'),
    'user_env': 'WMS_DB_USER',
    'password_env': 'WMS_KILLIOS_DB_PASSWORD',
    'database': os.environ.get('WMS_DB_NAME', 'TOMWMS_KILLIOS_PRD_2026'),
}

ALLOWED = re.compile(
    r'^\s*(SELECT|WITH|EXEC\s+sp_help|EXEC\s+sp_columns|EXEC\s+sp_helptext)\b',
    re.I,
)


def configure(host: str = None, user_env: str = None,
              password_env: str = None, database: str = None) -> None:
    """Sobrescribe los defaults antes de la primera llamada a q()."""
    if host: _CFG['host'] = host
    if user_env: _CFG['user_env'] = user_env
    if password_env: _CFG['password_env'] = password_env
    if database: _CFG['database'] = database


def _split_host(h: str):
    if ',' in h:
        a, b = h.split(',', 1)
        return a.strip(), int(b.strip())
    return h, 1433


def _safe(sql: str) -> None:
    if not ALLOWED.match(sql):
        raise PermissionError(
            "Statement bloqueado por _db.py: solo SELECT/WITH/sp_help/sp_columns/sp_helptext."
        )


def conn():
    user = os.environ.get(_CFG['user_env'])
    pwd = os.environ.get(_CFG['password_env'])
    if not user or not pwd:
        raise RuntimeError(
            f"Faltan env vars: {_CFG['user_env']} y/o {_CFG['password_env']}"
        )
    host, port = _split_host(_CFG['host'])
    return pymssql.connect(
        server=host, port=port, user=user, password=pwd,
        database=_CFG['database'], as_dict=True,
        tds_version='7.4', charset='UTF-8',
        timeout=120, login_timeout=30,
    )


def q(sql: str, params=None):
    """Ejecuta SQL read-only y devuelve lista de dicts."""
    _safe(sql)
    c = conn()
    try:
        cur = c.cursor()
        if params:
            cur.execute(sql, params)
        else:
            cur.execute(sql)
        try:
            return cur.fetchall()
        except pymssql.OperationalError:
            return []
    finally:
        c.close()


def smoke_test() -> None:
    """Prueba rápida de conectividad. Llamar desde script."""
    print(f"Host: {_CFG['host']}")
    print(f"BD: {_CFG['database']}")
    print(f"User env: {_CFG['user_env']}")
    v = q("SELECT @@VERSION AS v")[0]['v']
    print(f"Server: {v.splitlines()[0]}")
    n_tab = q("SELECT COUNT(*) n FROM sys.tables")[0]['n']
    print(f"Tablas en BD: {n_tab}")
    print("OK")


if __name__ == '__main__':
    smoke_test()
