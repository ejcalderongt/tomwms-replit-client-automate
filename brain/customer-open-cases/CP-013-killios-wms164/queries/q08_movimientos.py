"""q08: cardex completo del 23-abr-2026 — los 5 movimientos.

Cronologia esperada:
  10:32:18  M1 UBIC 307->308  40 estado=1
  10:32:42  M2 UBIC 307->308  95 estado=1
  10:33:01  M3 CEST 307->22   40 estado=1->16   *** split 1 ***
  10:33:14  M4 CEST 307->22   30 estado=1->16   *** split 2, NO consolida ***
  10:33:33  M5 UBIC 307->308  65 estado=1
"""
from _db import q

for r in q("""
    SELECT m.IdMovimiento, m.fec_agr,
           t.Codigo AS tipo,
           m.IdUbicOrigen, m.IdUbicDestino,
           m.IdProductoEstado_Origen AS eo,
           m.IdProductoEstado_Destino AS ed,
           m.Cantidad, m.lic_plate,
           m.IdStock_Origen, m.IdStock_Destino
    FROM trans_movimientos m
    JOIN sis_tipo_tarea t ON t.IdTipoTarea = m.TipoTarea
    WHERE m.IdProductoBodega = 381
      AND m.fec_agr BETWEEN '2026-04-23 10:32:00' AND '2026-04-23 10:34:00'
    ORDER BY m.fec_agr, m.IdMovimiento
"""): print(r)
