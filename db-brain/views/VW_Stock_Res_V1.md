---
id: db-brain-view-vw-stock-res-v1
type: db-view
title: dbo.VW_Stock_Res_V1
schema: dbo
name: VW_Stock_Res_V1
kind: view
modify_date: 2019-12-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res_V1`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-12-18 |
| Columnas | 43 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 3 | `IdProductoEstado` | `int` | ✓ |  |
| 4 | `IdPresentacion` | `int` | ✓ |  |
| 5 | `IdRecepcionEnc` | `int` | ✓ |  |
| 6 | `lote` | `nvarchar(50)` |  |  |
| 7 | `fecha_ingreso` | `datetime` | ✓ |  |
| 8 | `fecha_vence` | `datetime` | ✓ |  |
| 9 | `CantidadSF` | `float` | ✓ |  |
| 10 | `peso` | `float` | ✓ |  |
| 11 | `Cantidad` | `float` | ✓ |  |
| 12 | `IdUbicacion` | `int` |  |  |
| 13 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 14 | `serial` | `nvarchar(50)` | ✓ |  |
| 15 | `añada` | `int` | ✓ |  |
| 16 | `CantRes` | `float` | ✓ |  |
| 17 | `IdBodega` | `int` | ✓ |  |
| 18 | `IdPropietarioBodega` | `int` |  |  |
| 19 | `IdProducto` | `int` |  |  |
| 20 | `IdProductoBodega` | `int` |  |  |
| 21 | `IdUnidadMedida` | `int` |  |  |
| 22 | `Propietario` | `nvarchar(100)` |  |  |
| 23 | `codigo` | `nvarchar(50)` | ✓ |  |
| 24 | `nombre` | `nvarchar(100)` | ✓ |  |
| 25 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 26 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 27 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 28 | `dañado` | `bit` | ✓ |  |
| 29 | `factor` | `float` | ✓ |  |
| 30 | `EstadoUtilizable` | `bit` | ✓ |  |
| 31 | `IdIndiceRotacion` | `int` | ✓ |  |
| 32 | `alto` | `float` | ✓ |  |
| 33 | `largo` | `float` | ✓ |  |
| 34 | `ancho` | `float` | ✓ |  |
| 35 | `CantidadReservada` | `float` | ✓ |  |
| 36 | `existencia_max_umbas` | `float` | ✓ |  |
| 37 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 38 | `costo` | `float` | ✓ |  |
| 39 | `existencia_min_pres` | `float` | ✓ |  |
| 40 | `existencia_max_pres` | `float` | ✓ |  |
| 41 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 42 | `existencia_min_umbas` | `float` | ✓ |  |
| 43 | `IdPropietario` | `int` |  |  |

## Consume

- `bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Stock_Res_V1
AS
SELECT        TOP (100) PERCENT dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, 
                         SUM(dbo.stock.cantidad) AS CantidadSF, dbo.stock.peso, dbo.stock.cantidad / dbo.producto_presentacion.factor AS Cantidad, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, 
                         SUM(dbo.stock_res.cantidad) AS CantRes, dbo.producto_bodega.IdBodega, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, 
                         dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, 
                         dbo.producto_estado.nombre AS NomEstado, dbo.producto_estado.dañado, dbo.producto_presentacion.factor, dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
                         dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, SUM(dbo.stock_res.cantidad) AS CantidadReservada, dbo.producto.existencia_max AS existencia_max_umbas, dbo.producto.codigo_barra, 
                         dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia AS existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS existencia_max_pres, dbo.stock.atributo_variante_1, 
                         dbo.producto.existencia_min AS existencia_min_umbas, dbo.propietarios.IdPropietario
FROM            dbo.stock INNER JOIN
                         dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND 
                         dbo.bodega.IdBodega = dbo.propietario_bodega.IdBodega INNER JOIN
                         dbo.unidad_medida ON dbo.stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND dbo.stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                         dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado AND dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario AND dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND 
                         dbo.unidad_medida.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND 
                         dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario LEFT OUTER JOIN
                         dbo.stock_res ON dbo.stock.IdUbicacion = dbo.stock_res.IdUbicacion AND dbo.stock.IdUnidadMedida = dbo.stock_res.IdUnidadMedida AND dbo.stock.IdPresentacion = dbo.stock_res.IdPresentacion AND 
                         dbo.stock.IdStock = dbo.stock_res.IdStock AND dbo.stock.IdPropietarioBodega = dbo.stock_res.IdPropietarioBodega AND dbo.stock.IdProductoBodega = dbo.stock_res.IdProductoBodega AND 
                         dbo.stock.IdProductoEstado = dbo.stock_res.IdProductoEstado LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND 
                         dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto
GROUP BY dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, dbo.stock.peso, 
                         dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, dbo.producto_bodega.IdBodega, dbo.propietario_bodega.IdPropietarioBodega, 
                         dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.propietarios.nombre_comercial, dbo.producto.codigo, dbo.producto.nombre, dbo.unidad_medida.Nombre, 
                         dbo.producto_presentacion.nombre, dbo.producto_estado.nombre, dbo.producto_estado.dañado, dbo.producto_estado.utilizable, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, 
                         dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, 
                         dbo.producto_presentacion.MaximoExistencia, dbo.stock.atributo_variante_1, dbo.producto.existencia_min, dbo.propietarios.IdPropietario
ORDER BY dbo.stock.IdStock
```
