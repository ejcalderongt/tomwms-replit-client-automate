"""q03: schema detallado de las 3 tablas centrales (stock, bodega_ubicacion, trans_movimientos).

Confirma:
  - stock.lic_plate es nvarchar (no FK a tabla licencia)
  - bodega_ubicacion no tiene PK unica por IdUbicacion (mismo IdUbicacion
    puede repetirse entre IdBodega distintos)
  - trans_movimientos tiene lic_plate, IdUbicOrigen, IdUbicDestino,
    IdProductoEstado, TipoTarea (FK a sis_tipo_tarea)
"""
from _db import q

for tbl in ("stock", "bodega_ubicacion", "trans_movimientos", "sis_tipo_tarea"):
    print(f"=== {tbl} ===")
    for r in q("""
        SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH AS len, IS_NULLABLE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = %s
        ORDER BY ORDINAL_POSITION
    """, (tbl,)):
        print(f"  {r['COLUMN_NAME']:30s} {r['DATA_TYPE']}({r['len']})  null={r['IS_NULLABLE']}")
