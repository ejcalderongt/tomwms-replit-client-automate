"""q04: localizar el caso por ventana ancha (todos los movimientos del 23-abr-2026
del IdProductoBodega=381) — el ticket WMS164 reporta un desfase visible ese dia.
"""
from _db import q

for r in q("""
    SELECT m.IdMovimiento, m.IdRecepcion, m.IdUsuario,
           m.TipoTarea, t.Codigo AS tipo_codigo,
           m.IdUbicOrigen, m.IdUbicDestino,
           m.IdProductoEstado_Origen, m.IdProductoEstado_Destino,
           m.Cantidad, m.Lote, m.lic_plate,
           m.fec_agr
    FROM trans_movimientos m
    JOIN sis_tipo_tarea t ON t.IdTipoTarea = m.TipoTarea
    WHERE m.IdProductoBodega = 381
      AND m.fec_agr >= '2026-04-23' AND m.fec_agr < '2026-04-24'
    ORDER BY m.fec_agr, m.IdMovimiento
"""): print(r)
