"""q01: localizar el producto del caso WMS164.

Hipotesis: el ticket menciona un producto cuyo IdProducto interno es 77
y su IdProductoBodega es 381 sobre IdBodega=1 (BOD1) en propietario=1
(KILIO-GARESA, codigo='01').
"""
from _db import q

print("=== producto raiz ===")
for r in q("""
    SELECT TOP 5 p.IdProducto, p.Codigo, p.Descripcion
    FROM producto p
    WHERE p.IdProducto = 77
"""): print(r)

print("=== producto-bodega del caso ===")
for r in q("""
    SELECT pb.IdProductoBodega, pb.IdProducto, pb.IdBodega, pb.IdPropietario,
           b.Nombre AS bodega
    FROM t_producto_bodega pb
    JOIN bodega b ON b.IdBodega = pb.IdBodega
    WHERE pb.IdProductoBodega = 381
"""): print(r)
