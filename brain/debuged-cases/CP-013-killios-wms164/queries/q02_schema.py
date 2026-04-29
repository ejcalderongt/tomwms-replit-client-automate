"""q02: inventario de tablas del esquema Killios para guiar el trace.

Confirma que el esquema es lowercase y que no hay tabla 'posicion' ni
'licencia' (la posicion es bodega_ubicacion y la licencia es lic_plate
como string libre dentro de la fila de stock).
"""
from _db import q

for r in q("""
    SELECT TABLE_NAME
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_TYPE='BASE TABLE'
      AND (TABLE_NAME LIKE '%stock%'
        OR TABLE_NAME LIKE '%bodega%'
        OR TABLE_NAME LIKE '%producto%'
        OR TABLE_NAME LIKE '%movim%'
        OR TABLE_NAME LIKE '%lic%')
    ORDER BY TABLE_NAME
"""): print(r["TABLE_NAME"])
