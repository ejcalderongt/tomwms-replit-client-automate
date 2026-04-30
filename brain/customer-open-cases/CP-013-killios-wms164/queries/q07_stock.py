"""q07: cruzar stock con bodega_ubicacion para entender la ubicacion fisica.

Aparece la grieta del FK roto: stock.IdUbicacion=22 con IdBodega=1, pero
bodega_ubicacion(22) solo existe en IdBodega=6. La integridad referencial
queda solo en convencion (no enforced).
"""
from _db import q

for r in q("""
    SELECT s.IdStock, s.IdUbicacion, s.IdProductoEstado, s.Cantidad,
           bu.IdBodega AS bodega_de_la_ubicacion, bu.Codigo, bu.Descripcion
    FROM stock s
    LEFT JOIN bodega_ubicacion bu ON bu.IdUbicacion = s.IdUbicacion
    WHERE s.IdProductoBodega = 381 AND s.Cantidad > 0
    ORDER BY s.IdStock
"""): print(r)
