---
id: db-brain-view-vw-minimosmaximosporpresentacion
type: db-view
title: dbo.VW_MinimosMaximosPorPresentacion
schema: dbo
name: VW_MinimosMaximosPorPresentacion
kind: view
modify_date: 2016-07-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_MinimosMaximosPorPresentacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-07-22 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPresentacion` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `nombreProducto` | `nvarchar(50)` | ✓ |  |
| 6 | `nombreProductoPresentacion` | `nvarchar(50)` | ✓ |  |
| 7 | `MinimoExistencia` | `float` | ✓ |  |
| 8 | `MaximoExistencia` | `float` | ✓ |  |
| 9 | `cantidad` | `float` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_presentacion`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_MinimosMaximosPorPresentacion
AS
SELECT        dbo.producto_presentacion.IdPresentacion, dbo.producto_presentacion.IdProducto, dbo.producto_bodega.IdBodega, dbo.producto.codigo, dbo.producto.nombre AS nombreProducto, 
                         dbo.producto_presentacion.nombre AS nombreProductoPresentacion, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.stock.cantidad
FROM            dbo.producto_presentacion INNER JOIN
                         dbo.producto ON dbo.producto_presentacion.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                         dbo.stock ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega AND dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion
```
