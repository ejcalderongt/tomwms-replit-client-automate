# -*- coding: utf-8 -*-
"""
audit-danados-sin-ajuste.py — Generaliza el bug de CP-013.

Cuenta líneas con dañado_picking=1, activo=1, cantidad_verificada=0,
cantidad_despachada=0 y reporta UM totales fantasma + top usuarios + top productos.

Uso:
    python audit-danados-sin-ajuste.py
"""
from _db import q
from csv_helpers import csv_out


def main(out_dir: str = 'outputs/audit'):
    print("=== Bug dañado_picking sin descuento (generalizado CP-013) ===")

    total = q("""
        SELECT COUNT(*) n_lineas, SUM(cantidad_solicitada) um,
               COUNT(DISTINCT IdProductoBodega) productos,
               COUNT(DISTINCT lote) lotes,
               MIN(fec_agr) desde, MAX(fec_agr) hasta
        FROM trans_picking_ubic WITH (NOLOCK)
        WHERE [dañado_picking] = 1 AND activo = 1
          AND cantidad_verificada = 0 AND cantidad_despachada = 0
    """)[0]
    print(f"  total: {total['n_lineas']:,} lineas, {total['um']:,} UM, "
          f"{total['productos']} productos, {total['lotes']} lotes")
    print(f"  rango: {total['desde']} -> {total['hasta']}")

    by_user = q("""
        SELECT pu.user_agr, u.nombres+' '+ISNULL(u.apellidos,'') usuario,
               COUNT(*) n_lineas, SUM(pu.cantidad_solicitada) um,
               COUNT(DISTINCT pu.IdProductoBodega) productos,
               COUNT(DISTINCT pu.lote) lotes,
               MIN(pu.fec_agr) desde, MAX(pu.fec_agr) hasta
        FROM trans_picking_ubic pu WITH (NOLOCK)
        LEFT JOIN usuario u ON u.IdUsuario = pu.user_agr
        WHERE pu.[dañado_picking] = 1 AND pu.activo = 1
          AND pu.cantidad_verificada = 0 AND pu.cantidad_despachada = 0
        GROUP BY pu.user_agr, u.nombres, u.apellidos
        ORDER BY um DESC
    """)
    csv_out(f"{out_dir}/danados-por-usuario.csv", by_user)

    by_prod = q("""
        SELECT TOP 30 pu.IdProductoBodega, p.codigo, p.nombre,
               SUM(pu.cantidad_solicitada) um,
               COUNT(DISTINCT pu.lote) lotes,
               COUNT(*) n_lineas
        FROM trans_picking_ubic pu WITH (NOLOCK)
        LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = pu.IdProductoBodega
        LEFT JOIN producto p ON p.IdProducto = pb.IdProducto
        WHERE pu.[dañado_picking] = 1 AND pu.activo = 1
          AND pu.cantidad_verificada = 0 AND pu.cantidad_despachada = 0
        GROUP BY pu.IdProductoBodega, p.codigo, p.nombre
        ORDER BY um DESC
    """)
    csv_out(f"{out_dir}/danados-top-productos.csv", by_prod)

    sin_ajcantn = q("""
        SELECT TOP 50 pu.IdPickingUbic, pu.IdProductoBodega, pu.lote,
               pu.lic_plate, pu.cantidad_solicitada, pu.fec_agr, pu.user_agr
        FROM trans_picking_ubic pu WITH (NOLOCK)
        LEFT JOIN trans_movimientos m WITH (NOLOCK)
               ON m.lic_plate COLLATE DATABASE_DEFAULT = pu.lic_plate COLLATE DATABASE_DEFAULT
              AND m.lote COLLATE DATABASE_DEFAULT = pu.lote COLLATE DATABASE_DEFAULT
              AND m.IdTipoTarea = 17
              AND ABS(DATEDIFF(DAY, m.fecha_agr, pu.fec_agr)) <= 1
        WHERE pu.[dañado_picking] = 1 AND pu.activo = 1
          AND pu.cantidad_verificada = 0 AND pu.cantidad_despachada = 0
          AND m.IdMovimiento IS NULL
        ORDER BY pu.fec_agr DESC
    """)
    csv_out(f"{out_dir}/danados-sin-ajcantn-asociado.csv", sin_ajcantn)


if __name__ == '__main__':
    main()
