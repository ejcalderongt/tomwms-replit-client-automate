import os, pymssql

def conn():
    return pymssql.connect(
        server="52.41.114.122", port=1437,
        user=os.environ['WMS_DB_USER'],
        password=os.environ['WMS_KILLIOS_DB_PASSWORD'],
        database='TOMWMS_KILLIOS_PRD_2026',
        as_dict=True, tds_version='7.4', charset='UTF-8',
        timeout=60, login_timeout=30
    )

def q(sql):
    c = conn()
    try:
        cur = c.cursor()
        cur.execute(sql)
        return cur.fetchall()
    finally:
        c.close()
