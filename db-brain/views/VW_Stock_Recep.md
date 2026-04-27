---
id: db-brain-view-vw-stock-recep
type: db-view
title: dbo.VW_Stock_Recep
schema: dbo
name: VW_Stock_Recep
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Recep`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 31 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdProductoEstado` | `int` | ✓ |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdUnidadMedida` | `int` | ✓ |  |
| 7 | `IdUbicacion` | `int` |  |  |
| 8 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 9 | `IdRecepcionEnc` | `int` | ✓ |  |
| 10 | `IdRecepcionDet` | `int` | ✓ |  |
| 11 | `IdPedidoEnc` | `int` | ✓ |  |
| 12 | `IdPickingEnc` | `int` | ✓ |  |
| 13 | `IdDespachoEnc` | `int` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` | ✓ |  |
| 15 | `sumcant` | `float` | ✓ |  |
| 16 | `fecha_vence` | `datetime` | ✓ |  |
| 17 | `añada` | `int` | ✓ |  |
| 18 | `activo` | `bit` |  |  |
| 19 | `sumpeso` | `float` | ✓ |  |
| 20 | `temperatura` | `float` | ✓ |  |
| 21 | `IdBodega` | `int` | ✓ |  |
| 22 | `fecha_ingreso` | `datetime` | ✓ |  |
| 23 | `serial` | `nvarchar(50)` | ✓ |  |
| 24 | `lic_plate` | `varchar(max)` | ✓ |  |
| 25 | `cantidad` | `float` | ✓ |  |
| 26 | `nombre_propietario` | `nvarchar(100)` |  |  |
| 27 | `nombre_presentacion` | `nvarchar(50)` | ✓ |  |
| 28 | `nombre_estado` | `nvarchar(50)` | ✓ |  |
| 29 | `nombre` | `nvarchar(50)` | ✓ |  |
| 30 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 31 | `codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Stock_Recep
AS
SELECT     dbo.stock.IdStock, dbo.stock.IdPropietarioBodega, dbo.stock.IdProductoBodega, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdUnidadMedida, 
                      dbo.stock.IdUbicacion, dbo.stock.IdUbicacion_anterior AS IdUbicacion_anterior, dbo.stock.IdRecepcionEnc, dbo.stock.IdRecepcionDet, dbo.stock.IdPedidoEnc, 
                      dbo.stock.IdPickingEnc, dbo.stock.IdDespachoEnc, dbo.stock.lote, SUM(dbo.stock.cantidad) AS sumcant, dbo.stock.fecha_vence, dbo.stock.añada, dbo.stock.activo, 
                      SUM(dbo.stock.peso) AS sumpeso, dbo.stock.temperatura, dbo.propietario_bodega.IdBodega, dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.lic_plate, 
                      dbo.stock.cantidad, dbo.propietarios.nombre_comercial AS nombre_propietario, dbo.producto_presentacion.nombre AS nombre_presentacion, 
                      dbo.producto_estado.nombre AS nombre_estado, dbo.producto.nombre, dbo.producto.codigo_barra, dbo.producto.codigo
FROM         dbo.stock INNER JOIN
                      dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND 
                      dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                      dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND 
                      dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                      dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                      dbo.bodega_ubicacion ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.stock.IdUbicacion_anterior = dbo.bodega_ubicacion.IdUbicacion AND 
                      dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.stock.IdUbicacion_anterior = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                      dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                      dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND 
                      dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto INNER JOIN
                      dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado AND dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado AND 
                      dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario AND dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario
WHERE     (dbo.bodega_ubicacion.ubicacion_recepcion = 1)
GROUP BY dbo.stock.IdStock, dbo.stock.IdPropietarioBodega, dbo.stock.IdProductoBodega, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdUnidadMedida, 
                      dbo.stock.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.stock.IdRecepcionEnc, dbo.stock.IdRecepcionDet, dbo.stock.IdPedidoEnc, dbo.stock.IdPickingEnc, 
                      dbo.stock.IdDespachoEnc, dbo.stock.lote, dbo.stock.fecha_vence, dbo.stock.añada, dbo.stock.temperatura, dbo.propietario_bodega.IdBodega, dbo.stock.activo, 
                      dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.lic_plate, dbo.stock.cantidad, dbo.propietarios.nombre_comercial, dbo.producto_presentacion.nombre, 
                      dbo.producto_estado.nombre, dbo.producto.nombre, dbo.producto.codigo_barra, dbo.producto.codigo
HAVING      (dbo.stock.activo = 1)
```
