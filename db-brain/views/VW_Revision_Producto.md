---
id: db-brain-view-vw-revision-producto
type: db-view
title: dbo.VW_Revision_Producto
schema: dbo
name: VW_Revision_Producto
kind: view
modify_date: 2022-06-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Revision_Producto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-12 |
| Columnas | 38 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRellenado` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdProductoBodega` | `int` | ✓ |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 6 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 7 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 8 | `NombreUmBas` | `nvarchar(50)` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 11 | `Minimo` | `float` | ✓ |  |
| 12 | `Maximo` | `float` | ✓ |  |
| 13 | `IdProductoEstado` | `int` | ✓ |  |
| 14 | `Estado` | `nvarchar(50)` | ✓ |  |
| 15 | `StockUMBas` | `float` | ✓ |  |
| 16 | `ReservadoUmBas` | `float` | ✓ |  |
| 17 | `StockPres` | `int` |  |  |
| 18 | `DisponiblePres` | `int` |  |  |
| 19 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 20 | `IdPropietarioBodega` | `int` |  |  |
| 21 | `IdUbicacion` | `int` | ✓ |  |
| 22 | `IdTipoAccion` | `int` | ✓ |  |
| 23 | `Activo` | `bit` | ✓ |  |
| 24 | `IdPropietario` | `int` |  |  |
| 25 | `Nombre_Propietario` | `nvarchar(100)` |  |  |
| 26 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 27 | `fec_agr` | `datetime` | ✓ |  |
| 28 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 29 | `fec_mod` | `datetime` | ✓ |  |
| 30 | `IdUmBasAbastercerCon` | `int` | ✓ |  |
| 31 | `IdPresentacionAbastercerCon` | `int` | ✓ |  |
| 32 | `NombrePresentacionAbastecerCon` | `nvarchar(50)` | ✓ |  |
| 33 | `FactorAbastecerCon` | `float` | ✓ |  |
| 34 | `CantidadSFUbicDestino` | `float` | ✓ |  |
| 35 | `CantidadReservadaUbicDestino` | `float` | ✓ |  |
| 36 | `CantidadSFDispo` | `int` |  |  |
| 37 | `CantidadPresDispo` | `int` |  |  |
| 38 | `CantidadReservadaDispo` | `int` |  |  |

## Consume

- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `producto_rellenado`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `unidad_medida`
- `VW_Stock_Res`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Revision_Producto]
AS
SELECT 
DISTINCT
dbo.producto_rellenado.IdRellenado, dbo.producto_rellenado.IdBodega, dbo.producto_rellenado.IdProductoBodega, dbo.producto_bodega.IdProducto, dbo.producto.codigo AS Codigo_Producto,
dbo.producto.nombre AS Nombre_Producto, dbo.producto_rellenado.IdUnidadMedidaBasica, dbo.unidad_medida.Nombre AS NombreUmBas, dbo.producto_rellenado.IdPresentacion,
dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_rellenado.Minimo, dbo.producto_rellenado.Maximo, dbo.producto_rellenado.IdProductoEstado, dbo.producto_estado.nombre AS Estado,
ROUND(SUM(ISNULL(dbo.stock.cantidad, 0)), 6) AS StockUMBas,
ROUND(SUM(ISNULL(dbo.stock_res.cantidad, 0)), 6) AS ReservadoUmBas,
0 AS StockPres,
0 AS DisponiblePres, 
dbo.Nombre_Completo_Ubicacion(dbo.producto_rellenado.IdUbicacion, dbo.producto_rellenado.IdBodega)
AS Ubicacion, 
dbo.propietario_bodega.IdPropietarioBodega,
dbo.producto_rellenado.IdUbicacion, dbo.producto_rellenado.IdTipoAccion, dbo.producto_rellenado.Activo, dbo.producto.IdPropietario,
dbo.propietarios.nombre_comercial AS Nombre_Propietario, 
dbo.producto_rellenado.user_agr, dbo.producto_rellenado.fec_agr, dbo.producto_rellenado.user_mod, dbo.producto_rellenado.fec_mod,
dbo.producto_rellenado.IdUmBasAbastercerCon, 
dbo.producto_rellenado.IdPresentacionAbastercerCon, producto_presentacion_1.nombre AS NombrePresentacionAbastecerCon,
producto_presentacion_1.factor as FactorAbastecerCon,
SUM((ISNULL(vw_stock_ubicacion_picking.CantidadSF, 0))) AS CantidadSFUbicDestino,
SUM((ISNULL(vw_stock_ubicacion_picking.CantidadReservada, 0))) AS CantidadReservadaUbicDestino,
0 AS CantidadSFDispo,
0 AS CantidadPresDispo,
0 AS CantidadReservadaDispo
FROM dbo.unidad_medida INNER JOIN
dbo.producto_rellenado INNER JOIN
dbo.producto_bodega INNER JOIN
dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.producto_rellenado.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND
dbo.producto_rellenado.IdBodega = dbo.producto_bodega.IdBodega ON dbo.unidad_medida.IdUnidadMedida = dbo.producto_rellenado.IdUnidadMedidaBasica INNER JOIN
dbo.producto_estado ON dbo.producto_rellenado.IdProductoEstado = dbo.producto_estado.IdEstado 

INNER JOIN dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN 
dbo.propietario_bodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario AND
dbo.producto_rellenado.IdPropietario = dbo.propietarios.IdPropietario
AND producto_estado.IdPropietario = propietarios.IdPropietario AND 
                         producto_estado.IdPropietario = propietarios.IdPropietario AND
						 producto_bodega.IdBodega = propietario_bodega.IdBodega

LEFT OUTER JOIN
dbo.VW_Stock_Res AS vw_stock_ubicacion_picking ON dbo.producto_rellenado.IdProductoEstado = vw_stock_ubicacion_picking.IdProductoEstado AND
dbo.producto_rellenado.IdBodega = vw_stock_ubicacion_picking.IdBodega AND dbo.producto_rellenado.IdProductoBodega = vw_stock_ubicacion_picking.IdProductoBodega AND
dbo.producto_rellenado.IdPropietario = vw_stock_ubicacion_picking.IdPropietario AND dbo.producto_rellenado.IdUbicacion = vw_stock_ubicacion_picking.IdUbicacion LEFT OUTER JOIN
dbo.unidad_medida AS unidad_medida_1 ON dbo.producto_rellenado.IdUmBasAbastercerCon = unidad_medida_1.IdUnidadMedida LEFT OUTER JOIN
dbo.producto_presentacion AS producto_presentacion_1 ON dbo.producto_rellenado.IdPresentacionAbastercerCon = producto_presentacion_1.IdPresentacion LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.producto_rellenado.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
dbo.stock_res INNER JOIN
dbo.stock ON dbo.stock_res.IdStock = dbo.stock.IdStock AND dbo.stock_res.IdBodega = dbo.stock.IdBodega ON dbo.producto_rellenado.IdUnidadMedidaBasica = dbo.stock.IdUnidadMedida AND
dbo.producto_rellenado.IdProductoBodega = dbo.stock.IdProductoBodega AND dbo.producto_rellenado.IdBodega = dbo.stock.IdBodega AND
dbo.producto_rellenado.IdProductoEstado = dbo.stock.IdProductoEstado AND dbo.producto_rellenado.IdPresentacion = dbo.stock.IdPresentacion AND
dbo.producto_rellenado.IdUbicacion = dbo.stock.IdUbicacion 

AND stock.IdPropietarioBodega = propietario_bodega.IdPropietario 
and stock.IdBodega  = propietario_bodega.IdBodega
AND stock_res.IdPropietarioBodega = propietario_bodega.IdPropietario 
and stock_res.IdBodega  = propietario_bodega.IdBodega

GROUP BY dbo.producto_rellenado.IdRellenado, dbo.producto_rellenado.IdBodega, dbo.producto_rellenado.IdProductoBodega, dbo.producto_bodega.IdProducto, dbo.producto.codigo, dbo.producto.nombre,
dbo.producto_rellenado.IdUnidadMedidaBasica, dbo.unidad_medida.Nombre, dbo.producto_rellenado.IdPresentacion, producto_presentacion_1.nombre, dbo.producto_rellenado.Minimo,
dbo.producto_rellenado.Maximo, dbo.producto_rellenado.IdProductoEstado, dbo.producto_estado.nombre, producto_presentacion_1.factor , dbo.producto_rellenado.IdBodega, dbo.producto_rellenado.IdUbicacion,
dbo.propietario_bodega.IdPropietarioBodega, 
dbo.producto_rellenado.IdTipoAccion, dbo.producto_rellenado.Activo, dbo.producto.IdPropietario, 
dbo.propietarios.nombre_comercial,
dbo.producto_rellenado.user_agr, dbo.producto_rellenado.fec_agr, dbo.producto_rellenado.user_mod, dbo.producto_rellenado.fec_mod, dbo.producto_rellenado.IdUmBasAbastercerCon,
dbo.producto_rellenado.IdPresentacionAbastercerCon, dbo.producto_presentacion.nombre
HAVING (dbo.producto_rellenado.Activo = 1)
```
