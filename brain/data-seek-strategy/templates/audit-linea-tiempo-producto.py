"""
Linea de tiempo COMPLETA de un producto (codigo o IdProductoBodega) en una BD WMS.

Reune en orden cronologico:
- Recepciones (trans_movimientos IdTipoTarea = entrada)
- Despachos (trans_movimientos IdTipoTarea = salida via picking)
- Ajustes manuales (trans_ajuste_enc + trans_ajuste_det)
- Ajustes automaticos AJCANTN (trans_movimientos IdTipoTarea = 17)
- Picking marcado (trans_picking_ubic con flags danado/no_encontrado)
- Reemplazos (HH genera reemplazo cuando un picking se marca danado)

Uso:
    python3 audit-linea-tiempo-producto.py --db TOMWMS_KILLIOS_PRD_2026 --codigo WMS164
    python3 audit-linea-tiempo-producto.py --db IMS4MB_MERCOPAN_PRD --codigo 440292 --desde 2024-01-01
    python3 audit-linea-tiempo-producto.py --db TOMWMS_KILLIOS_PRD_2026 --idpb 1234

Sirve para responder:
- ¿Cuando estaba bien y cuando dejo de estarlo?
- ¿Que evento rompio el saldo?
- ¿Hay reemplazos huerfanos?
- ¿Cuantos AJCANTN se aplicaron y cuantos faltaron?

Caso padre: brain/debuged-cases/CP-013-killios-wms164/REPORTE-CONCLUSION-V3.md
"""
import os
import sys
import argparse
from datetime import datetime, date

import pymssql

HOST = '52.41.114.122'
PORT = 1437
USER = os.environ['WMS_DB_USER']
PW = os.environ['WMS_KILLIOS_DB_PASSWORD']


def conectar(db: str):
    return pymssql.connect(
        server=HOST, port=PORT, user=USER, password=PW,
        database=db, as_dict=True, login_timeout=8, timeout=120,
    )


def resolver_idpb(cur, codigo: str | None, idpb: int | None) -> list[dict]:
    """Devuelve lista de {IdProductoBodega, IdBodega, codigo, nombre} candidatos."""
    if idpb:
        cur.execute("""
            SELECT pb.IdProductoBodega, pb.IdBodega, p.codigo, p.nombre
            FROM producto_bodega pb WITH (NOLOCK)
            LEFT JOIN producto p WITH (NOLOCK) ON p.IdProducto = pb.IdProducto
            WHERE pb.IdProductoBodega = %d
        """, idpb)
        return cur.fetchall()
    if codigo:
        cur.execute("""
            SELECT pb.IdProductoBodega, pb.IdBodega, p.codigo, p.nombre
            FROM producto p WITH (NOLOCK)
            INNER JOIN producto_bodega pb WITH (NOLOCK) ON pb.IdProducto = p.IdProducto
            WHERE p.codigo = %s
              AND pb.activo = 1
        """, codigo)
        return cur.fetchall()
    return []


def linea_tiempo(cur, idpb: int, desde: date | None, hasta: date | None) -> list[dict]:
    eventos: list[dict] = []
    rango = ''
    params: list = [idpb]
    if desde:
        rango += ' AND fecha_agr >= %s'; params.append(desde)
    if hasta:
        rango += ' AND fecha_agr <= %s'; params.append(hasta)

    # Movimientos (entradas, salidas, AJCANTN)
    sql_m = f"""
        SELECT
            'MOVIMIENTO' AS tipo,
            fecha_agr AS fecha,
            IdTipoTarea AS subtipo,
            cantidad,
            lote,
            lic_plate,
            user_agr,
            ubicacion_origen,
            ubicacion_destino,
            IdMovimiento AS ref_id
        FROM trans_movimientos WITH (NOLOCK)
        WHERE IdProductoBodega = %d {rango}
        ORDER BY fecha_agr
    """
    try:
        cur.execute(sql_m, tuple(params))
        eventos.extend(cur.fetchall())
    except Exception as e:
        print(f'  WARN movimientos: {e}', file=sys.stderr)

    # Pickings con flags
    rango2 = ''
    params2: list = [idpb]
    if desde:
        rango2 += ' AND fec_agr >= %s'; params2.append(desde)
    if hasta:
        rango2 += ' AND fec_agr <= %s'; params2.append(hasta)
    sql_pu = f"""
        SELECT
            'PICKING_UBIC' AS tipo,
            fec_agr AS fecha,
            CASE
                WHEN [dañado_picking]=1 THEN 'DANADO_PICK'
                WHEN [dañado_verificacion]=1 THEN 'DANADO_VERIF'
                WHEN no_encontrado=1 THEN 'NO_ENCONTRADO'
                WHEN cantidad_despachada > 0 THEN 'DESPACHADO'
                WHEN cantidad_verificada > 0 THEN 'VERIFICADO'
                ELSE 'PENDIENTE'
            END AS subtipo,
            cantidad_solicitada AS cantidad,
            lote,
            lic_plate,
            user_agr,
            CONVERT(varchar, IdOperadorBodega_Pickeo) AS origen_pickeo,
            CONVERT(varchar, activo) AS activo,
            IdPickingUbic AS ref_id
        FROM trans_picking_ubic WITH (NOLOCK)
        WHERE IdProductoBodega = %d {rango2}
        ORDER BY fec_agr
    """
    try:
        cur.execute(sql_pu, tuple(params2))
        eventos.extend(cur.fetchall())
    except Exception as e:
        print(f'  WARN picking: {e}', file=sys.stderr)

    eventos.sort(key=lambda e: e['fecha'] or datetime.min)
    return eventos


def imprimir_tl(prod: dict, eventos: list[dict]):
    print(f'\n{"="*120}')
    print(f'LINEA DE TIEMPO — IdProductoBodega={prod["IdProductoBodega"]}  Bodega={prod["IdBodega"]}')
    print(f'  codigo={prod["codigo"]}  nombre={(prod["nombre"] or "")[:60]}')
    print(f'  total eventos={len(eventos)}')
    print(f'{"="*120}\n')
    print(f'{"fecha":<20} {"tipo":<14} {"subtipo":<14} {"cant":>10} {"lote":<14} {"lic_plate":<14} {"user":<8} ref')
    print('-' * 120)
    for e in eventos:
        fecha = str(e['fecha'])[:19] if e['fecha'] else '-'
        sub = str(e.get('subtipo') or '')[:14]
        cant = float(e.get('cantidad') or 0)
        lote = str(e.get('lote') or '')[:14]
        lp = str(e.get('lic_plate') or '')[:14]
        user = str(e.get('user_agr') or '')[:8]
        ref = e.get('ref_id', '')
        print(f'{fecha:<20} {e["tipo"]:<14} {sub:<14} {cant:>10,.2f} {lote:<14} {lp:<14} {user:<8} {ref}')


def parse_fecha(s: str | None) -> date | None:
    if not s:
        return None
    return datetime.strptime(s, '%Y-%m-%d').date()


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--db', required=True, help='nombre de la BD (ej TOMWMS_KILLIOS_PRD_2026)')
    g = ap.add_mutually_exclusive_group(required=True)
    g.add_argument('--codigo', help='codigo del producto en tabla producto')
    g.add_argument('--idpb', type=int, help='IdProductoBodega especifico')
    ap.add_argument('--desde', help='fecha desde YYYY-MM-DD')
    ap.add_argument('--hasta', help='fecha hasta YYYY-MM-DD')
    args = ap.parse_args()

    desde = parse_fecha(args.desde)
    hasta = parse_fecha(args.hasta)

    c = conectar(args.db)
    cur = c.cursor()

    candidatos = resolver_idpb(cur, args.codigo, args.idpb)
    if not candidatos:
        print(f'No se encontro producto codigo={args.codigo} idpb={args.idpb} en {args.db}', file=sys.stderr)
        sys.exit(1)
    print(f'Candidatos para "{args.codigo or args.idpb}": {len(candidatos)}')
    for cand in candidatos:
        print(f'  -> IdProductoBodega={cand["IdProductoBodega"]} Bodega={cand["IdBodega"]} {cand["codigo"]} {cand["nombre"]}')

    for cand in candidatos:
        eventos = linea_tiempo(cur, cand['IdProductoBodega'], desde, hasta)
        imprimir_tl(cand, eventos)

    c.close()


if __name__ == '__main__':
    main()
