---
id: db-brain-view-vw-producto-subtareas
type: db-view
title: dbo.VW_Producto_Subtareas
schema: dbo
name: VW_Producto_Subtareas
kind: view
modify_date: 2023-10-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Producto_Subtareas`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-10-23 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoSubTarea` | `int` |  |  |
| 2 | `IdSubTareaTipo` | `int` | ✓ |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `IdBodega` | `int` | ✓ |  |
| 6 | `CodigoProducto` | `nvarchar(50)` | ✓ |  |
| 7 | `NombreProducto` | `nvarchar(100)` | ✓ |  |
| 8 | `SubTarea` | `nvarchar(50)` | ✓ |  |
| 9 | `porcentaje_escaneo` | `float` | ✓ |  |
| 10 | `escanear_producto` | `bit` | ✓ |  |
| 11 | `obligatorio_recepcion` | `bit` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_subtarea`
- `producto_subtarea_tipo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Producto_Subtareas
AS
SELECT dbo.producto_subtarea.IdProductoSubTarea, dbo.producto_subtarea.IdSubTareaTipo, dbo.producto_bodega.IdProductoBodega, dbo.producto_bodega.IdProducto, dbo.producto_bodega.IdBodega, 
                  dbo.producto.codigo AS CodigoProducto, dbo.producto.nombre AS NombreProducto, dbo.producto_subtarea_tipo.nombre AS SubTarea, dbo.producto_subtarea.porcentaje_escaneo, dbo.producto_subtarea.escanear_producto, 
                  dbo.producto_subtarea.obligatorio_recepcion
FROM     dbo.producto_bodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.producto_subtarea INNER JOIN
                  dbo.producto_subtarea_tipo ON dbo.producto_subtarea.IdSubTareaTipo = dbo.producto_subtarea_tipo.IdSubTareaTipo ON dbo.producto_bodega.IdProductoBodega = dbo.producto_subtarea.IdProductoBodega
```
