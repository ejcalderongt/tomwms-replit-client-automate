"""q09: buscar el RECE original (recepcion del 09-feb-2026) para confirmar
    el linaje completo del lote.

Hallazgo lateral: stock.fec_agr=2026-04-23 > stock.fec_mod=2026-02-09 — la
fecha de agregado es POSTERIOR a la de modificacion. Incoherencia menor
que apunta a que fec_agr/fec_mod en stock no obedecen el contrato natural
"agregado siempre <= modificado".
"""
from _db import q

print("=== RECE original ===")
for r in q("""
    SELECT m.IdMovimiento, m.fec_agr, t.Codigo AS tipo, m.Cantidad,
           m.Lote, m.lic_plate, m.IdRecepcion
    FROM trans_movimientos m
    JOIN sis_tipo_tarea t ON t.IdTipoTarea = m.TipoTarea
    WHERE m.IdProductoBodega = 381
      AND t.Codigo = 'RECE'
      AND m.IdRecepcion = 2179
"""): print(r)
