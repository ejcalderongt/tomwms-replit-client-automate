---
id: db-brain-view-vw-rptminimosmaximos
type: db-view
title: dbo.VW_rptMinimosMaximos
schema: dbo
name: VW_rptMinimosMaximos
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_rptMinimosMaximos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 38 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` |  |  |
| 5 | `IdProductoBodega` | `int` |  |  |
| 6 | `IdStock` | `int` |  |  |
| 7 | `IdUbicacionActual` | `int` | ✓ |  |
| 8 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 9 | `IdUnidadMedida` | `int` |  |  |
| 10 | `IdProductoEstado` | `int` | ✓ |  |
| 11 | `IdPresentacion` | `int` | ✓ |  |
| 12 | `IdRecepcionEnc` | `int` | ✓ |  |
| 13 | `Propietario` | `nvarchar(100)` |  |  |
| 14 | `UbicacionAnterior` | `nvarchar(50)` | ✓ |  |
| 15 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 16 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 17 | `codigo` | `nvarchar(50)` | ✓ |  |
| 18 | `nombre` | `nvarchar(50)` | ✓ |  |
| 19 | `lote` | `nvarchar(50)` | ✓ |  |
| 20 | `fecha_ingreso` | `datetime` | ✓ |  |
| 21 | `serial` | `nvarchar(50)` | ✓ |  |
| 22 | `añada` | `int` | ✓ |  |
| 23 | `cantidad` | `float` | ✓ |  |
| 24 | `fecha_vence` | `datetime` | ✓ |  |
| 25 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 26 | `EstadoUtilizable` | `bit` | ✓ |  |
| 27 | `dañado` | `bit` | ✓ |  |
| 28 | `IdUbicacion` | `int` |  |  |
| 29 | `lic_plate` | `varchar(max)` | ✓ |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `IdIndiceRotacion` | `int` | ✓ |  |
| 32 | `MinimoExistencia` | `float` |  |  |
| 33 | `MaximoExistencia` | `float` |  |  |
| 34 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 35 | `IndiceRotacion` | `nvarchar(50)` |  |  |
| 36 | `Costo` | `float` | ✓ |  |
| 37 | `totalLinea` | `float` | ✓ |  |
| 38 | `CantidadPresentacion` | `float` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `indice_rotacion`
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
CREATE VIEW dbo.VW_rptMinimosMaximos
AS
SELECT     dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, 
                      dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, 
                      dbo.stock.IdUbicacion_anterior AS IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, 
                      dbo.stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, bodega_ubicacion_1.descripcion AS UbicacionAnterior, 
                      dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                      dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.stock.cantidad, dbo.stock.fecha_vence, dbo.producto_estado.nombre AS NomEstado, 
                      dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.peso, 
                      dbo.producto.IdIndiceRotacion, ISNULL(dbo.producto_presentacion.MinimoExistencia, 0) AS MinimoExistencia, ISNULL(dbo.producto_presentacion.MaximoExistencia, 
                      0) AS MaximoExistencia, dbo.producto.codigo_barra, dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto_presentacion.Costo, 
                      dbo.stock.cantidad * dbo.producto_presentacion.Costo AS totalLinea, dbo.stock.cantidad / dbo.producto_presentacion.factor AS CantidadPresentacion
FROM         dbo.unidad_medida INNER JOIN
                      dbo.propietarios INNER JOIN
                      dbo.propietario_bodega INNER JOIN
                      dbo.stock INNER JOIN
                      dbo.producto_bodega INNER JOIN
                      dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega ON 
                      dbo.propietario_bodega.IdPropietarioBodega = dbo.stock.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                      dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida INNER JOIN
                      dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion LEFT OUTER JOIN
                      dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                      dbo.bodega_ubicacion AS bodega_ubicacion_1 ON dbo.stock.IdUbicacion_anterior = bodega_ubicacion_1.IdUbicacion LEFT OUTER JOIN
                      dbo.bodega_ubicacion ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                      dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, 
                      bodega_ubicacion_1.descripcion, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, 
                      dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                      dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.stock.cantidad, dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, 
                      dbo.producto_estado.nombre, dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, 
                      dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.MinimoExistencia, 
                      dbo.producto_presentacion.MaximoExistencia, dbo.producto.codigo_barra, dbo.indice_rotacion.Descripcion, dbo.producto_presentacion.Costo, 
                      dbo.stock.cantidad / dbo.producto_presentacion.factor
```
