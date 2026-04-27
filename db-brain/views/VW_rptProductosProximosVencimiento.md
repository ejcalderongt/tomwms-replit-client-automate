---
id: db-brain-view-vw-rptproductosproximosvencimiento
type: db-view
title: dbo.VW_rptProductosProximosVencimiento
schema: dbo
name: VW_rptProductosProximosVencimiento
kind: view
modify_date: 2017-05-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_rptProductosProximosVencimiento`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-05-19 |
| Columnas | 28 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPropietario` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProducto` | `int` | ✓ |  |
| 4 | `IdStock` | `int` |  |  |
| 5 | `IdUnidadMedida` | `int` |  |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `Propietario` | `nvarchar(100)` |  |  |
| 9 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 10 | `control_vencimiento` | `bit` | ✓ |  |
| 11 | `codigo` | `nvarchar(50)` | ✓ |  |
| 12 | `nombre` | `nvarchar(50)` | ✓ |  |
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha_ingreso` | `datetime` | ✓ |  |
| 15 | `cantidad` | `float` | ✓ |  |
| 16 | `existencia_min` | `float` | ✓ |  |
| 17 | `existencia_max` | `float` | ✓ |  |
| 18 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 19 | `fecha_vence` | `datetime` | ✓ |  |
| 20 | `FechaProyectada` | `datetime` | ✓ |  |
| 21 | `Tolerancia` | `int` | ✓ |  |
| 22 | `ToleranciaProyectada` | `int` | ✓ |  |
| 23 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 24 | `IdEstado` | `int` |  |  |
| 25 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 26 | `serial` | `nvarchar(50)` | ✓ |  |
| 27 | `IdUbicacion` | `int` |  |  |
| 28 | `CantidadPresentacion` | `float` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_rptProductosProximosVencimiento
AS
SELECT     dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.stock.IdStock, dbo.unidad_medida.IdUnidadMedida, 
                      dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.propietarios.nombre_comercial AS Propietario, dbo.unidad_medida.Nombre AS UnidadMedida, 
                      dbo.producto.control_vencimiento, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.cantidad, 
                      dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.stock.fecha_vence, DATEADD(day, ISNULL(dbo.producto.tolerancia, 0), 
                      GETDATE()) AS FechaProyectada, dbo.producto.tolerancia AS Tolerancia, DATEDIFF(day, DATEADD(day, ISNULL(dbo.producto.tolerancia, 0), GETDATE()), 
                      dbo.stock.fecha_vence) AS ToleranciaProyectada, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_estado.IdEstado, 
                      dbo.producto_estado.nombre AS NomEstado, dbo.stock.serial, dbo.stock.IdUbicacion, 
                      dbo.stock.cantidad / dbo.producto_presentacion.factor AS CantidadPresentacion
FROM         dbo.producto_presentacion INNER JOIN
                      dbo.unidad_medida INNER JOIN
                      dbo.propietarios INNER JOIN
                      dbo.propietario_bodega INNER JOIN
                      dbo.producto_bodega INNER JOIN
                      dbo.stock ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega ON 
                      dbo.propietario_bodega.IdPropietarioBodega = dbo.stock.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                      dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion INNER JOIN
                      dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                      dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
WHERE     (dbo.producto.control_vencimiento = 1)
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.propietario_bodega.IdPropietarioBodega, 
                      dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                      dbo.stock.fecha_ingreso, dbo.stock.cantidad, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.producto.existencia_min, 
                      dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.tolerancia, dbo.producto.control_vencimiento, dbo.producto_presentacion.nombre, 
                      dbo.producto_estado.IdEstado, dbo.producto_estado.nombre, dbo.stock.serial, dbo.stock.IdUbicacion, dbo.stock.cantidad / dbo.producto_presentacion.factor
HAVING      (DATEADD(day, ISNULL(dbo.producto.tolerancia, 0), GETDATE()) >= dbo.stock.fecha_vence)
```
