"""q12: medir alcance sistemico del anti-patron INSERT-stock-sin-merge.

Cuenta combos (IdProductoBodega + IdUbicacion + IdProductoEstado + Lote +
lic_plate) que tienen mas de una fila en stock activo. Cada combo es un
caso WMS164-equivalente: una llave natural partida en N filas con IdStock
distintos cuando deberia haber sido 1.

Resultado en Killios:
  total stock activo:    4.914 filas
  combos duplicados:       469
  filas redundantes:       919  (18.7%)
  UN involucradas:     183.375
"""
from _db import q

print("=== total stock activo ===")
for r in q("""
    SELECT COUNT(*) cnt, SUM(Cantidad) un_total
    FROM stock WHERE Cantidad > 0
"""): print(r)

print("=== combos duplicados (top 20 por cantidad de filas) ===")
for r in q("""
    SELECT TOP 20
           pb.IdProducto,
           p.Codigo,
           s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado,
           s.Lote, s.lic_plate,
           COUNT(*) cnt, SUM(s.Cantidad) total,
           MIN(s.IdStock) min_id, MAX(s.IdStock) max_id
    FROM stock s
    JOIN t_producto_bodega pb ON pb.IdProductoBodega = s.IdProductoBodega
    JOIN producto p ON p.IdProducto = pb.IdProducto
    WHERE s.Cantidad > 0
    GROUP BY pb.IdProducto, p.Codigo,
             s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado,
             s.Lote, s.lic_plate
    HAVING COUNT(*) > 1
    ORDER BY COUNT(*) DESC, SUM(s.Cantidad) DESC
"""): print(r)

print("=== resumen agregado ===")
for r in q("""
    SELECT
        COUNT(*) AS combos_duplicados,
        SUM(filas) AS filas_totales_en_combos,
        SUM(filas - 1) AS filas_redundantes,
        SUM(total_un) AS un_involucradas
    FROM (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
               COUNT(*) AS filas, SUM(Cantidad) AS total_un
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) sub
"""): print(r)
