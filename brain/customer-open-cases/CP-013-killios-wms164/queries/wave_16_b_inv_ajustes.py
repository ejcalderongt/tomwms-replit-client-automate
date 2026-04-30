"""WAVE 16b — evidencias W16-06..09 (inventarios, ajustes, operadores, cronicidad CESTs)"""
import sys, csv, os
sys.path.insert(0, ".")
from _db import q

OUT = "../outputs/wave-16-conclusion"
os.makedirs(OUT, exist_ok=True)
prod_id = q("SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=381")[0]['IdProducto']

def write_csv(name, rows, header=None):
    if rows:
        cols = list(rows[0].keys())
        with open(f"{OUT}/{name}", "w", newline="", encoding="utf-8") as f:
            w = csv.DictWriter(f, fieldnames=cols); w.writeheader()
            for r in rows: w.writerow({k:("" if v is None else v) for k,v in r.items()})
    else:
        with open(f"{OUT}/{name}", "w", encoding="utf-8") as f:
            f.write((header or "vacio") + "\n# ZERO ROWS — evidencia de ausencia\n")
    print(f"  {name} -> {len(rows)} filas")

write_csv("W16-06-inventarios-WMS164.csv", q(f"""
SELECT id.IdInventarioEnc, ie.fecha, ie.tipo_conteo, ie.tipo_inv, ie.estado,
       id.IdInventarioDet, id.IdStock, id.IdEstado, id.lote, id.lic_plate, id.IdUbicacion,
       id.cantidad AS cant_sistema, id.conteo, id.recuento, id.inicial
FROM trans_inventario_det id
INNER JOIN trans_inventario_enc ie ON ie.IdInventarioEnc=id.IdInventarioEnc
WHERE id.IdProducto={prod_id} ORDER BY ie.fecha
"""), header="IdInventarioEnc,fecha,lote,IdStock,cant_sistema,conteo")

write_csv("W16-07-ajustes-WMS164.csv", q("""
SELECT ad.idajustedet, ae.idajusteenc, ae.fecha, ae.idusuario, ae.referencia, ae.ajuste_por_inventario,
       ad.lote_original, ad.lote_nuevo, ad.cantidad_original, ad.cantidad_nueva,
       ad.IdStock, ad.IdProductoEstado, ad.IdUbicacion, ad.idtipoajuste, ad.idmotivoajuste, ad.observacion
FROM trans_ajuste_det ad INNER JOIN trans_ajuste_enc ae ON ae.idajusteenc=ad.idajusteenc
WHERE ad.IdProductoBodega=381 ORDER BY ae.fecha
"""))

write_csv("W16-08-operadores-clave.csv", q("""
SELECT IdOperador, codigo, nombres, apellidos, IdRolOperador, activo, recibe, ubica, transporta, pickea, verifica
FROM operador WHERE IdOperador IN (13,18,20,30,31,124)
"""))

write_csv("W16-09-cronicidad-CESTs.csv", q("""
SELECT IdMovimiento, fecha, lote, IdEstadoOrigen, IdEstadoDestino,
       IdUbicacionOrigen, IdUbicacionDestino, lic_plate, cantidad, IdRecepcion, usuario_agr
FROM trans_movimientos
WHERE IdProductoBodega=381 AND IdTipoTarea=3 AND IdEstadoOrigen=1 AND IdEstadoDestino=16
ORDER BY fecha
"""))
