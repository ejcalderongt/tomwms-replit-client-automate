"""
Auditoria parametrizable del bug danado_picking sin descuento de stock.
Aplica a cualquier conjunto de BDs TOMWMS / IMS4MB en el server EC2 de Killios.

Uso:
    python3 audit-bug-danado-multi-bd.py
    python3 audit-bug-danado-multi-bd.py --dbs TOMWMS_KILLIOS_PRD_2026 IMS4MB_MERCOPAN_PRD
    python3 audit-bug-danado-multi-bd.py --csv --out outputs/audit_$(date +%F).csv

Output: stdout tabular + JSON estructurado en outputs/_audit-bug-multi-bd.json (ultima ejecucion).

Caso padre: brain/debuged-cases/CP-014-bug-danado-picking-transversal/
"""
import os
import sys
import json
import argparse
from datetime import datetime

import pymssql

HOST = '52.41.114.122'
PORT = 1437
USER = os.environ['WMS_DB_USER']
PW = os.environ['WMS_KILLIOS_DB_PASSWORD']

DBS_DEFAULT = [
    'TOMWMS_KILLIOS_PRD',
    'TOMWMS_KILLIOS_PRD_2026',
    'TOMWMS_MAMPA_QA',
    'IMS4MB_BECOFARMA_PRD',
    'IMS4MB_BYB_PRD',
    'IMS4MB_CEALSA_QAS',
    'IMS4MB_MERCOPAN_PRD',
    'IMS4MB_MERHONSA_PRD',
]


def conectar(db: str):
    return pymssql.connect(
        server=HOST, port=PORT, user=USER, password=PW,
        database=db, as_dict=True, login_timeout=8, timeout=120,
    )


def auditar_bd(db: str) -> dict:
    """Devuelve dict con todas las metricas relevantes para una BD."""
    out: dict = {'db': db}
    try:
        c = conectar(db)
        cur = c.cursor()

        # 0. Existencia y rango
        cur.execute("SELECT COUNT(*) AS n FROM sys.tables WHERE name='trans_picking_ubic'")
        if not cur.fetchone()['n']:
            out['error'] = 'sin trans_picking_ubic'
            c.close()
            return out

        # 1. Total bug + breakdown BOF/HH
        cur.execute("""
            SELECT
                COUNT(*) AS lineas_bug,
                ISNULL(SUM(cantidad_solicitada),0) AS um_fantasma,
                COUNT(DISTINCT IdProductoBodega) AS productos,
                COUNT(DISTINCT lote) AS lotes,
                COUNT(DISTINCT user_agr) AS usuarios,
                MIN(fec_agr) AS desde,
                MAX(fec_agr) AS hasta,
                ISNULL(SUM(CASE WHEN IdOperadorBodega_Pickeo = 0 THEN 1 ELSE 0 END),0) AS lineas_bof,
                ISNULL(SUM(CASE WHEN IdOperadorBodega_Pickeo > 0 THEN 1 ELSE 0 END),0) AS lineas_hh,
                ISNULL(SUM(CASE WHEN IdOperadorBodega_Pickeo = 0 THEN cantidad_solicitada ELSE 0 END),0) AS um_bof,
                ISNULL(SUM(CASE WHEN IdOperadorBodega_Pickeo > 0 THEN cantidad_solicitada ELSE 0 END),0) AS um_hh
            FROM trans_picking_ubic WITH (NOLOCK)
            WHERE [dañado_picking] = 1
              AND activo = 1
              AND cantidad_verificada = 0
              AND cantidad_despachada = 0
        """)
        out.update(cur.fetchone())

        if out['lineas_bug'] == 0:
            c.close()
            return out

        # 2. Total dañados generales
        cur.execute("SELECT COUNT(*) AS n FROM trans_picking_ubic WITH (NOLOCK) WHERE [dañado_picking]=1")
        out['total_danados_general'] = cur.fetchone()['n']

        # 3. AJCANTN cruce
        cur.execute("""
            SELECT COUNT(DISTINCT pu.IdPickingUbic) AS con_ajcantn
            FROM trans_picking_ubic pu WITH (NOLOCK)
            INNER JOIN trans_movimientos m WITH (NOLOCK)
                ON m.lic_plate COLLATE DATABASE_DEFAULT = pu.lic_plate COLLATE DATABASE_DEFAULT
               AND m.lote COLLATE DATABASE_DEFAULT = pu.lote COLLATE DATABASE_DEFAULT
               AND m.IdTipoTarea = 17
               AND ABS(DATEDIFF(DAY, m.fecha_agr, pu.fec_agr)) <= 1
            WHERE pu.[dañado_picking] = 1
              AND pu.activo = 1
              AND pu.cantidad_verificada = 0
              AND pu.cantidad_despachada = 0
        """)
        out['con_ajcantn'] = cur.fetchone()['con_ajcantn']
        out['sin_ajcantn'] = out['lineas_bug'] - out['con_ajcantn']

        c.close()
    except Exception as e:
        out['error'] = str(e)[:300]
    return out


def imprimir_tabular(resultados: list[dict]):
    print(f'\n{"="*130}')
    print(f'AUDIT bug danado_picking sin AJCANTN ({len(resultados)} BDs) — {datetime.now():%Y-%m-%d %H:%M}')
    print(f'{"="*130}\n')
    print(f'{"BD":<28} {"lin_bug":>8} {"um":>12} {"prods":>5} {"users":>5} {"%BOF":>6} {"%sinAJ":>7} {"rango"}')
    print('-' * 130)
    for r in resultados:
        if 'error' in r:
            print(f'{r["db"]:<28} ERROR: {r["error"][:80]}')
            continue
        if r['lineas_bug'] == 0:
            print(f'{r["db"]:<28} sin bug (no usa la feature o fix aplicado)')
            continue
        pct_bof = 100 * r['lineas_bof'] / r['lineas_bug']
        pct_sin_aj = 100 * r['sin_ajcantn'] / r['lineas_bug']
        rango = f'{str(r["desde"])[:10]}->{str(r["hasta"])[:10]}'
        print(f'{r["db"]:<28} {r["lineas_bug"]:>8,} {r["um_fantasma"]:>12,.0f} {r["productos"]:>5} {r["usuarios"]:>5} {pct_bof:>5.1f}% {pct_sin_aj:>6.1f}% {rango}')


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--dbs', nargs='+', default=DBS_DEFAULT)
    ap.add_argument('--out', default=os.path.join(os.path.dirname(__file__), 'outputs/_audit-bug-multi-bd.json'))
    ap.add_argument('--csv', action='store_true', help='emite CSV adicional al .json')
    args = ap.parse_args()

    resultados = [auditar_bd(db) for db in args.dbs]
    imprimir_tabular(resultados)

    os.makedirs(os.path.dirname(args.out), exist_ok=True)
    with open(args.out, 'w') as f:
        json.dump(resultados, f, indent=2, default=str)
    print(f'\nGuardado: {args.out}')

    if args.csv:
        import csv
        path_csv = args.out.replace('.json', '.csv')
        if resultados:
            cols = list(resultados[0].keys())
            with open(path_csv, 'w') as f:
                w = csv.DictWriter(f, fieldnames=cols)
                w.writeheader()
                w.writerows([{k: r.get(k, '') for k in cols} for r in resultados])
            print(f'CSV:      {path_csv}')


if __name__ == '__main__':
    main()
