---
id: db-brain-view-vw-trans-inv-stock
type: db-view
title: dbo.VW_Trans_Inv_Stock
schema: dbo
name: VW_Trans_Inv_Stock
kind: view
modify_date: 2018-04-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Trans_Inv_Stock`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-04-27 |
| Columnas | 52 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdStock` | `int` |  |  |
| 7 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` |  |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `IdRecepcionEnc` | `int` | ✓ |  |
| 12 | `Propietario` | `nvarchar(100)` |  |  |
| 13 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 14 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 15 | `codigo` | `nvarchar(50)` | ✓ |  |
| 16 | `Producto` | `nvarchar(100)` | ✓ |  |
| 17 | `lote` | `nvarchar(50)` | ✓ |  |
| 18 | `fecha_ingreso` | `datetime` | ✓ |  |
| 19 | `serial` | `nvarchar(50)` | ✓ |  |
| 20 | `añada` | `int` | ✓ |  |
| 21 | `CantidadUM` | `float` | ✓ |  |
| 22 | `factor` | `float` | ✓ |  |
| 23 | `CantidadPresentacion` | `float` | ✓ |  |
| 24 | `fecha_vence` | `datetime` | ✓ |  |
| 25 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 26 | `utilizable` | `bit` | ✓ |  |
| 27 | `dañado` | `bit` | ✓ |  |
| 28 | `IdUbicacion` | `int` |  |  |
| 29 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `IdIndiceRotacion` | `int` | ✓ |  |
| 32 | `alto` | `float` | ✓ |  |
| 33 | `largo` | `float` | ✓ |  |
| 34 | `ancho` | `float` | ✓ |  |
| 35 | `IdTramo` | `int` | ✓ |  |
| 36 | `ancho_ubicacion` | `float` | ✓ |  |
| 37 | `largo_ubicacion` | `float` | ✓ |  |
| 38 | `alto_ubicacion` | `float` | ✓ |  |
| 39 | `Descripcion` | `nvarchar(50)` | ✓ |  |
| 40 | `existencia_min` | `float` | ✓ |  |
| 41 | `existencia_max` | `float` | ✓ |  |
| 42 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 43 | `costo` | `float` | ✓ |  |
| 44 | `MinimoExistencia` | `float` | ✓ |  |
| 45 | `MaximoExistencia` | `float` | ✓ |  |
| 46 | `IdUbicacionActual` | `int` | ✓ |  |
| 47 | `nivel` | `int` | ✓ |  |
| 48 | `indice_x` | `int` | ✓ |  |
| 49 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 50 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 51 | `IdFamilia` | `int` | ✓ |  |
| 52 | `IdClasificacion` | `int` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_inv_stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Trans_Inv_Stock]
AS
SELECT dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, 
dbo.producto_bodega.IdProductoBodega, dbo.trans_inv_stock.IdStock, dbo.trans_inv_stock.IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, 
dbo.trans_inv_stock.IdProductoEstado, dbo.trans_inv_stock.IdPresentacion, dbo.trans_inv_stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario,
dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto.codigo, dbo.producto.nombre AS Producto, 
dbo.trans_inv_stock.lote, dbo.trans_inv_stock.fecha_ingreso, dbo.trans_inv_stock.serial, dbo.trans_inv_stock.añada, dbo.trans_inv_stock.cantidad AS CantidadUM, 
dbo.producto_presentacion.factor, dbo.trans_inv_stock.cantidad / dbo.producto_presentacion.factor AS CantidadPresentacion, dbo.trans_inv_stock.fecha_vence, 
dbo.producto_estado.nombre AS EstadoProducto, dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.trans_inv_stock.IdUbicacion, 
dbo.trans_inv_stock.lic_plate, dbo.trans_inv_stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, 
dbo.producto_presentacion.ancho, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS ancho_ubicacion, 
dbo.bodega_ubicacion.largo AS largo_ubicacion, dbo.bodega_ubicacion.alto AS alto_ubicacion, dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, 
dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, 
dbo.producto_presentacion.MaximoExistencia, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.bodega_ubicacion.nivel, 
dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, 
dbo.producto.IdFamilia, dbo.producto.IdClasificacion
FROM dbo.bodega_tramo INNER JOIN
dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo RIGHT OUTER JOIN
dbo.producto_estado RIGHT OUTER JOIN
dbo.producto_bodega INNER JOIN
dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto LEFT OUTER JOIN
dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion RIGHT OUTER JOIN
dbo.unidad_medida INNER JOIN
dbo.propietarios INNER JOIN
dbo.trans_inv_stock INNER JOIN
dbo.propietario_bodega ON dbo.trans_inv_stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON 
dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON dbo.unidad_medida.IdUnidadMedida = dbo.trans_inv_stock.IdUnidadMedida ON 
dbo.producto_bodega.IdProductoBodega = dbo.trans_inv_stock.IdProductoBodega ON dbo.producto_estado.IdEstado = dbo.trans_inv_stock.IdProductoEstado ON 
dbo.bodega_ubicacion.IdUbicacion = dbo.trans_inv_stock.IdUbicacion LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.trans_inv_stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.trans_inv_stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, 
dbo.trans_inv_stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega, 
dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, 
dbo.producto.nombre, dbo.trans_inv_stock.lote, dbo.trans_inv_stock.fecha_ingreso, dbo.trans_inv_stock.serial, dbo.trans_inv_stock.añada, 
dbo.producto_bodega.IdBodega, dbo.trans_inv_stock.fecha_vence, dbo.trans_inv_stock.IdProductoEstado, dbo.producto_estado.nombre, 
dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.trans_inv_stock.IdUbicacion, dbo.trans_inv_stock.IdPresentacion, 
dbo.trans_inv_stock.IdRecepcionEnc, dbo.trans_inv_stock.lic_plate, dbo.trans_inv_stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, 
dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, 
dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, 
dbo.producto_presentacion.MaximoExistencia, dbo.trans_inv_stock.cantidad, dbo.trans_inv_stock.cantidad / dbo.producto_presentacion.factor, 
dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.producto.IdFamilia, 
dbo.producto.IdClasificacion
```
