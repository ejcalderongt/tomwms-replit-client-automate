---
id: db-brain-view-vw-productodimension
type: db-view
title: dbo.VW_ProductoDimension
schema: dbo
name: VW_ProductoDimension
kind: view
modify_date: 2017-06-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoDimension`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-06-10 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `IdProducto` | `int` | ✓ |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `cantidad` | `float` | ✓ |  |
| 6 | `alto` | `float` | ✓ |  |
| 7 | `largo` | `float` | ✓ |  |
| 8 | `ancho` | `float` | ✓ |  |
| 9 | `dimension` | `float` | ✓ |  |
| 10 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 11 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 12 | `factor` | `float` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_presentacion`
- `stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_ProductoDimension
AS
SELECT     dbo.stock.IdStock, dbo.stock.IdUbicacion, dbo.producto.IdProducto, dbo.producto.nombre AS Nombre, dbo.stock.cantidad, dbo.producto_presentacion.alto, 
                      dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, 
                      dbo.producto_presentacion.alto * dbo.producto_presentacion.largo * dbo.producto_presentacion.ancho * dbo.stock.cantidad AS dimension, 
                      dbo.unidad_medida.Nombre AS UMBas, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_presentacion.factor
FROM         dbo.producto INNER JOIN
                      dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                      dbo.producto_presentacion ON dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto INNER JOIN
                      dbo.unidad_medida ON dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida RIGHT OUTER JOIN
                      dbo.stock ON dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida AND dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida AND 
                      dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion AND dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion AND 
                      dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega AND dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega
```
