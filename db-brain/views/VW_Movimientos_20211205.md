---
id: db-brain-view-vw-movimientos-20211205
type: db-view
title: dbo.VW_Movimientos_20211205
schema: dbo
name: VW_Movimientos_20211205
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_20211205`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodegaOrigen` | `int` |  |  |
| 2 | `fecha` | `datetime` | ✓ |  |
| 3 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 4 | `Tipo_Movimiento` | `nvarchar(50)` | ✓ |  |
| 5 | `Producto` | `nvarchar(153)` | ✓ |  |
| 6 | `Cantidad` | `float` | ✓ |  |
| 7 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 8 | `UM` | `nvarchar(50)` | ✓ |  |
| 9 | `Presentacion` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `sis_tipo_tarea`
- `trans_movimientos`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Movimientos_20211205]
AS
SELECT        dbo.trans_movimientos.IdBodegaOrigen, dbo.trans_movimientos.fecha, dbo.bodega.codigo AS Codigo_Bodega, dbo.sis_tipo_tarea.Nombre AS Tipo_Movimiento, dbo.producto.codigo + ' - ' + dbo.producto.nombre AS Producto, 
                         SUM(dbo.trans_movimientos.cantidad) AS Cantidad, CASE WHEN producto_presentacion.nombre IS NULL THEN unidad_medida.Nombre ELSE producto_presentacion.nombre END AS Unidad_Medida, 
                         dbo.unidad_medida.Nombre AS UM, dbo.producto_presentacion.nombre AS Presentacion
FROM            dbo.trans_movimientos INNER JOIN
                         dbo.producto_bodega ON dbo.trans_movimientos.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                         dbo.unidad_medida ON dbo.unidad_medida.IdUnidadMedida = dbo.trans_movimientos.IdUnidadMedida INNER JOIN
                         dbo.sis_tipo_tarea ON dbo.trans_movimientos.IdTipoTarea = dbo.sis_tipo_tarea.IdTipoTarea INNER JOIN
                         dbo.bodega ON dbo.trans_movimientos.IdBodegaOrigen = dbo.bodega.IdBodega LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_movimientos.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.trans_movimientos.IdProductoBodega, dbo.producto.codigo, dbo.producto.nombre, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.trans_movimientos.IdBodegaOrigen, dbo.trans_movimientos.fecha, 
                         dbo.sis_tipo_tarea.Nombre, dbo.bodega.codigo
HAVING        (dbo.sis_tipo_tarea.Nombre IN (N'RECE', N'TRAS', N'DESP'))
```
