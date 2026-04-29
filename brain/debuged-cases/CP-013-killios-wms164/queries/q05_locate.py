"""q05: refinar por IdRecepcion=2179 (el de la recepcion original del 09-feb-2026
que sigue dando movimientos en abril).
"""
from _db import q

for r in q("""
    SELECT m.IdMovimiento, m.IdUsuario, m.TipoTarea,
           m.IdUbicOrigen, m.IdUbicDestino,
           m.IdProductoEstado_Origen, m.IdProductoEstado_Destino,
           m.Cantidad, m.Lote, m.lic_plate, m.fec_agr,
           m.IdStock_Origen, m.IdStock_Destino
    FROM trans_movimientos m
    WHERE m.IdRecepcion = 2179
      AND m.IdProductoBodega = 381
      AND m.fec_agr >= '2026-04-23' AND m.fec_agr < '2026-04-24'
    ORDER BY m.fec_agr, m.IdMovimiento
"""): print(r)
