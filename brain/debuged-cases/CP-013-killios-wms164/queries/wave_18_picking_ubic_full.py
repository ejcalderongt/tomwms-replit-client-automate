import sys, csv, os
sys.path.insert(0, os.path.dirname(__file__))
from _db import q

OUT = "/tmp/wms-brain-fresh/brain/debuged-cases/CP-013-killios-wms164/outputs/wave-18"
os.makedirs(OUT, exist_ok=True)

IDPB = 381  # IdProductoBodega WMS164
IDPROD = 77  # IdProducto WMS164
LOTE_ALERTA = "BG2512"

def csv_out(name, rows):
    p = f"{OUT}/{name}"
    if not rows:
        with open(p, "w") as f: f.write("(sin filas)\n")
        print(f"  {name}: 0 filas")
        return
    with open(p, "w", newline="") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
        w.writeheader(); w.writerows(rows)
    print(f"  {name}: {len(rows)} filas")

print(f"WMS164 IdProducto={IDPROD} IdProductoBodega={IDPB} | Lote alerta {LOTE_ALERTA}\n")

# === BLOQUE 1: TODO trans_picking_ubic WMS164 (cualquier lote, toda la historia) ===
print("[1] trans_picking_ubic WMS164 todo")
csv_out("W18-01-picking-ubic-WMS164-todo.csv", q(f"""
SELECT pu.IdPickingUbic, pu.IdPickingEnc, pu.IdPickingDet, pu.IdUbicacion,
       u.descripcion AS ubic_desc, pu.IdStock, pu.lic_plate, pu.lote, pu.fecha_vence,
       pu.cantidad_solicitada, pu.cantidad_verificada, pu.cantidad_despachada,
       pu.peso_solicitado, pu.peso_verificado, pu.peso_despachado,
       pu.encontrado, pu.no_encontrado, pu.dañado_picking, pu.dañado_verificacion,
       pu.acepto, pu.activo, pu.fecha_picking, pu.fecha_verificado, pu.fecha_despachado,
       pu.IdOperadorBodega_Asignado, pu.IdOperadorBodega_Pickeo, pu.IdOperadorBodega_Verifico,
       pu.user_agr, pu.fec_agr, pu.user_mod, pu.fec_mod,
       pu.IdProductoEstado, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pu.lic_plate_reemplazo
FROM trans_picking_ubic pu
LEFT JOIN bodega_ubicacion u ON u.IdUbicacion = pu.IdUbicacion
WHERE pu.IdProductoBodega = {IDPB}
ORDER BY pu.fec_agr DESC, pu.IdPickingUbic DESC
"""))

# === BLOQUE 2: trans_picking_ubic BG2512 (cualquier producto) ===
print("[2] trans_picking_ubic LOTE BG2512 todos los productos")
csv_out("W18-02-picking-ubic-BG2512-cualquier-prod.csv", q(f"""
SELECT pu.IdPickingUbic, pu.IdPickingEnc, pu.IdPickingDet, pu.IdProductoBodega,
       p.codigo AS prod_codigo, p.nombre AS prod_nombre,
       u.descripcion AS ubic_desc, pu.IdStock, pu.lic_plate,
       pu.cantidad_solicitada, pu.cantidad_verificada, pu.cantidad_despachada,
       pu.encontrado, pu.no_encontrado, pu.dañado_picking, pu.dañado_verificacion,
       pu.activo, pu.fec_agr, pu.fec_mod, pu.user_agr, pu.user_mod,
       pu.IdOperadorBodega_Pickeo, pu.IdOperadorBodega_Verifico
FROM trans_picking_ubic pu
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = pu.IdProductoBodega LEFT JOIN producto p ON p.IdProducto = pb.IdProducto
LEFT JOIN bodega_ubicacion u ON u.IdUbicacion = pu.IdUbicacion
WHERE pu.lote = '{LOTE_ALERTA}'
ORDER BY pu.fec_agr DESC
"""))

# === BLOQUE 3: trans_movimientos WMS164 todo (toda la historia del producto) ===
print("[3] trans_movimientos WMS164 todo (toda la historia)")
csv_out("W18-03-movimientos-WMS164-todo.csv", q(f"""
SELECT m.IdMovimiento, m.fecha, m.fecha_agr, m.usuario_agr, m.IdOperadorBodega,
       m.IdTipoTarea, tt.Nombre AS tipo_tarea_desc,
       m.IdUbicacionOrigen, uo.descripcion AS ubic_origen,
       m.IdUbicacionDestino, ud.descripcion AS ubic_destino,
       m.IdEstadoOrigen, m.IdEstadoDestino,
       m.cantidad, m.cantidad_hist, m.peso, m.peso_hist,
       m.lote, m.fecha_vence, m.lic_plate,
       m.IdRecepcion, m.IdRecepcionDet,
       m.IdPedidoEnc, m.IdPedidoDet, m.IdDespachoEnc, m.IdDespachoDet,
       m.serie, m.barra_pallet, m.hora_ini, m.hora_fin
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea tt ON tt.IdTipoTarea = m.IdTipoTarea
LEFT JOIN bodega_ubicacion uo ON uo.IdUbicacion = m.IdUbicacionOrigen
LEFT JOIN bodega_ubicacion ud ON ud.IdUbicacion = m.IdUbicacionDestino
WHERE m.IdProductoBodega = {IDPB}
ORDER BY m.fecha_agr DESC, m.IdMovimiento DESC
"""))

# === BLOQUE 4: trans_movimientos cantidad_hist != cantidad (ajustes in-place) ===
print("[4] movimientos con cantidad_hist != cantidad (cambios cuantitativos)")
csv_out("W18-04-movimientos-cant-cambia.csv", q(f"""
SELECT m.IdMovimiento, m.fecha, m.fecha_agr, m.usuario_agr, m.IdOperadorBodega,
       m.IdTipoTarea, tt.Nombre AS tipo_tarea,
       m.cantidad_hist AS cant_anterior, m.cantidad AS cant_nueva,
       (m.cantidad - m.cantidad_hist) AS delta,
       m.peso_hist, m.peso, m.lote, m.lic_plate,
       uo.descripcion AS ubic_orig, ud.descripcion AS ubic_dest
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea tt ON tt.IdTipoTarea = m.IdTipoTarea
LEFT JOIN bodega_ubicacion uo ON uo.IdUbicacion = m.IdUbicacionOrigen
LEFT JOIN bodega_ubicacion ud ON ud.IdUbicacion = m.IdUbicacionDestino
WHERE m.IdProductoBodega = {IDPB} AND m.cantidad <> ISNULL(m.cantidad_hist, m.cantidad)
ORDER BY m.fecha_agr DESC
"""))

# === BLOQUE 5: agrupado trans_movimientos por IdTipoTarea ===
print("[5] resumen movimientos por tipo_tarea WMS164")
csv_out("W18-05-movimientos-por-tipo.csv", q(f"""
SELECT m.IdTipoTarea, tt.Nombre AS tipo_tarea, COUNT(*) AS n_mov,
       SUM(ISNULL(m.cantidad,0)) AS suma_cant,
       MIN(m.fecha_agr) AS min_fecha, MAX(m.fecha_agr) AS max_fecha
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea tt ON tt.IdTipoTarea = m.IdTipoTarea
WHERE m.IdProductoBodega = {IDPB}
GROUP BY m.IdTipoTarea, tt.Nombre
ORDER BY n_mov DESC
"""))

# === BLOQUE 6: lic_plate WMS164 que aparezca con MULTIPLES lotes ===
print("[6] lic_plate con WMS164 + multi-lote (posible swap)")
csv_out("W18-06-lp-multilote.csv", q(f"""
SELECT lic_plate, COUNT(DISTINCT lote) AS n_lotes,
       STRING_AGG(CAST(lote AS VARCHAR(50)), ', ') AS lotes
FROM trans_picking_ubic
WHERE IdProductoBodega = {IDPB} AND lic_plate IS NOT NULL AND lic_plate <> ''
GROUP BY lic_plate
HAVING COUNT(DISTINCT lote) > 1
ORDER BY n_lotes DESC
"""))

# === BLOQUE 7: stock_hist con WMS164 en lote BG2512 (cambios al IdStock) ===
print("[7] stock_hist WMS164 lote BG2512")
csv_out("W18-07-stockhist-WMS164-BG2512.csv", q(f"""
SELECT sh.IdStockHist, sh.IdStock, sh.IdNuevoStock, sh.cantidad,
       sh.lote, sh.lic_plate, sh.IdUbicacion, u.descripcion AS ubic,
       sh.IdUbicacion_anterior, ua.descripcion AS ubic_anterior,
       sh.IdRecepcionEnc, sh.IdRecepcionDet,
       sh.IdPedidoEnc, sh.IdPickingEnc, sh.IdDespachoEnc,
       sh.fecha_ingreso, sh.fec_agr, sh.user_agr, sh.fec_mod, sh.user_mod, sh.activo
FROM stock_hist sh
LEFT JOIN bodega_ubicacion u ON u.IdUbicacion = sh.IdUbicacion
LEFT JOIN bodega_ubicacion ua ON ua.IdUbicacion = sh.IdUbicacion_anterior
WHERE sh.IdProductoBodega = {IDPB} AND sh.lote = '{LOTE_ALERTA}'
ORDER BY sh.fec_agr DESC
"""))

# === BLOQUE 8: stock_hist WMS164 todo (trazabilidad full) ===
print("[8] stock_hist WMS164 todo")
csv_out("W18-08-stockhist-WMS164-todo.csv", q(f"""
SELECT sh.IdStockHist, sh.IdStock, sh.IdNuevoStock, sh.cantidad,
       sh.lote, sh.lic_plate, u.descripcion AS ubic, ua.descripcion AS ubic_anterior,
       sh.IdRecepcionEnc, sh.IdPickingEnc, sh.IdDespachoEnc,
       sh.fec_agr, sh.user_agr, sh.activo
FROM stock_hist sh
LEFT JOIN bodega_ubicacion u ON u.IdUbicacion = sh.IdUbicacion
LEFT JOIN bodega_ubicacion ua ON ua.IdUbicacion = sh.IdUbicacion_anterior
WHERE sh.IdProductoBodega = {IDPB}
ORDER BY sh.fec_agr DESC
"""))

# === BLOQUE 9: pickings WMS164 con anomalia (dañado o no_encontrado) ===
print("[9] pickings WMS164 con dañado / no_encontrado")
csv_out("W18-09-pickings-anomalias.csv", q(f"""
SELECT IdPickingUbic, IdPickingEnc, lote, lic_plate,
       cantidad_solicitada, cantidad_verificada, cantidad_despachada,
       encontrado, no_encontrado, dañado_picking, dañado_verificacion,
       activo, fec_agr, fec_mod, user_agr, user_mod
FROM trans_picking_ubic
WHERE IdProductoBodega = {IDPB}
  AND (dañado_picking = 1 OR dañado_verificacion = 1 OR no_encontrado = 1
       OR cantidad_verificada <> cantidad_solicitada
       OR cantidad_despachada <> cantidad_verificada)
ORDER BY fec_agr DESC
"""))

# === BLOQUE 10: pickings WMS164 anulados (activo=0) ===
print("[10] pickings WMS164 anulados activo=0")
csv_out("W18-10-pickings-anulados.csv", q(f"""
SELECT IdPickingUbic, IdPickingEnc, lote, lic_plate,
       cantidad_solicitada, cantidad_verificada, cantidad_despachada,
       fec_agr, user_agr, fec_mod, user_mod
FROM trans_picking_ubic
WHERE IdProductoBodega = {IDPB} AND activo = 0
ORDER BY fec_mod DESC
"""))

print(f"\nOK - outputs en {OUT}")
