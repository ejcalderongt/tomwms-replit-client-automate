---
id: db-brain-view-vw-stock
type: db-view
title: dbo.VW_Stock
schema: dbo
name: VW_Stock
kind: view
modify_date: 2017-10-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-18 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBodega` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `IdUnidadMedida` | `int` | ✓ |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `NombrePropietario` | `nvarchar(100)` |  |  |
| 8 | `codigo` | `nvarchar(50)` | ✓ |  |
| 9 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 10 | `nombre` | `nvarchar(50)` | ✓ |  |
| 11 | `NombreUnidadMedida` | `nvarchar(50)` | ✓ |  |
| 12 | `NombrePresentacion` | `nvarchar(50)` | ✓ |  |
| 13 | `CodigoBarraPresentacion` | `nvarchar(50)` | ✓ |  |
| 14 | `NombreEstadoProducto` | `nvarchar(50)` | ✓ |  |
| 15 | `cantidad` | `float` | ✓ |  |
| 16 | `peso` | `float` | ✓ |  |
| 17 | `precio` | `float` | ✓ |  |

## Consume

- `bodega_ubicacion`
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
CREATE VIEW dbo.VW_Stock
AS
SELECT     dbo.producto_bodega.IdProductoBodega, dbo.producto.IdProducto, dbo.producto.IdPropietario, dbo.stock.IdUnidadMedida, dbo.stock.IdPresentacion, 
                      dbo.stock.IdProductoEstado, dbo.propietarios.nombre_comercial AS NombrePropietario, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre, 
                      dbo.unidad_medida.Nombre AS NombreUnidadMedida, dbo.producto_presentacion.nombre AS NombrePresentacion, 
                      dbo.producto_presentacion.codigo_barra AS CodigoBarraPresentacion, dbo.producto_estado.nombre AS NombreEstadoProducto, dbo.stock.cantidad, dbo.stock.peso, 
                      dbo.producto.precio
FROM         dbo.stock INNER JOIN
                      dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND 
                      dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                      dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND 
                      dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                      dbo.unidad_medida ON dbo.stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND 
                      dbo.stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                      dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto AND 
                      dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica LEFT OUTER JOIN
                      dbo.producto_presentacion ON dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto AND 
                      dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion INNER JOIN
                      dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                      dbo.bodega_ubicacion ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                      dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND dbo.unidad_medida.IdPropietario = dbo.propietarios.IdPropietario AND 
                      dbo.producto.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND 
                      dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario
WHERE     (dbo.producto.activo = 1) AND (dbo.bodega_ubicacion.bloqueada = 0)
GROUP BY dbo.producto.IdProducto, dbo.producto.IdPropietario, dbo.propietarios.nombre_comercial, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre, 
                      dbo.stock.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.stock.IdPresentacion, dbo.producto_presentacion.nombre, dbo.producto_presentacion.codigo_barra, 
                      dbo.producto_bodega.IdProductoBodega, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.stock.cantidad, dbo.stock.peso, dbo.producto.precio
```
