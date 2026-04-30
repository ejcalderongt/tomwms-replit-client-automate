"""q06: foto actual de stock para IdProductoBodega=381.

Resultado esperado del bug: 3 filas activas, dos de ellas (134176 y 134177)
con IDENTICA llave natural (Ubicacion=22, Estado=16, Lote=L, lic_plate='').
"""
from _db import q

for r in q("""
    SELECT s.IdStock, s.IdProductoBodega, s.IdUbicacion,
           s.IdProductoEstado, s.Lote, s.lic_plate, s.Cantidad,
           s.fec_agr, s.fec_mod
    FROM stock s
    WHERE s.IdProductoBodega = 381
      AND s.Cantidad > 0
    ORDER BY s.IdStock
"""): print(r)
