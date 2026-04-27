---
id: db-brain-view-vw-existencias-producto-categoria
type: db-view
title: dbo.vw_existencias_producto_categoria
schema: dbo
name: vw_existencias_producto_categoria
kind: view
modify_date: 2017-10-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.vw_existencias_producto_categoria`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-04 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Tipo` | `nvarchar(50)` | ✓ |  |
| 2 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Producto` | `nvarchar(50)` | ✓ |  |
| 6 | `existencia_min` | `float` | ✓ |  |
| 7 | `existencia_max` | `float` | ✓ |  |
| 8 | `Existencia` | `float` |  |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_familia`
- `producto_tipo`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.vw_existencias_producto_categoria
AS
SELECT     TOP (100) PERCENT dbo.producto_tipo.NombreTipoProducto AS Tipo, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto.IdProducto, 
                      dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, dbo.producto.existencia_min, dbo.producto.existencia_max, ISNULL(SUM(dbo.stock.cantidad), 0) 
                      AS Existencia
FROM         dbo.producto INNER JOIN
                      dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto LEFT OUTER JOIN
                      dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                      dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia LEFT OUTER JOIN
                      dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                      dbo.stock ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega AND 
                      dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega
GROUP BY dbo.producto_tipo.NombreTipoProducto, dbo.producto_clasificacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, 
                      dbo.producto.existencia_min, dbo.producto.existencia_max
ORDER BY Existencia DESC
```
