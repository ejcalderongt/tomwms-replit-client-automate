---
id: db-brain-view-vw-stock-sp
type: db-view
title: dbo.VW_Stock_SP
schema: dbo
name: VW_Stock_SP
kind: view
modify_date: 2019-04-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_SP`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-04-19 |
| Columnas | 35 |

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
| 18 | `nombre` | `nvarchar(100)` | ✓ |  |
| 19 | `lote` | `nvarchar(50)` |  |  |
| 20 | `fecha_ingreso` | `datetime` | ✓ |  |
| 21 | `serial` | `nvarchar(50)` | ✓ |  |
| 22 | `añada` | `int` | ✓ |  |
| 23 | `cantidad` | `float` | ✓ |  |
| 24 | `fecha_vence` | `datetime` | ✓ |  |
| 25 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 26 | `EstadoUtilizable` | `bit` | ✓ |  |
| 27 | `dañado` | `bit` | ✓ |  |
| 28 | `IdUbicacion` | `int` |  |  |
| 29 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `IdIndiceRotacion` | `int` | ✓ |  |
| 32 | `alto` | `float` | ✓ |  |
| 33 | `largo` | `float` | ✓ |  |
| 34 | `ancho` | `float` | ✓ |  |
| 35 | `factor` | `float` | ✓ |  |

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
CREATE VIEW dbo.VW_Stock_SP
AS
SELECT        dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, 
                         dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.stock.IdUbicacion_anterior, 
                         dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, 
                         dbo.propietarios.nombre_comercial AS Propietario, bodega_ubicacion_1.descripcion AS UbicacionAnterior, dbo.unidad_medida.Nombre AS UnidadMedida, 
                         dbo.producto_presentacion.nombre AS Presentacion, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.serial, 
                         dbo.stock.añada, dbo.stock.cantidad, dbo.stock.fecha_vence, dbo.producto_estado.nombre AS NomEstado, dbo.producto_estado.utilizable AS EstadoUtilizable, 
                         dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
                         dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor
FROM            dbo.unidad_medida INNER JOIN
                         dbo.propietarios INNER JOIN
                         dbo.propietario_bodega INNER JOIN
                         dbo.stock INNER JOIN
                         dbo.producto_bodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega ON 
                         dbo.propietario_bodega.IdPropietarioBodega = dbo.stock.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                         dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida LEFT OUTER JOIN
                         dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS bodega_ubicacion_1 ON dbo.stock.IdUbicacion_anterior = bodega_ubicacion_1.IdUbicacion LEFT OUTER JOIN
                         dbo.bodega_ubicacion ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, 
                         bodega_ubicacion_1.descripcion, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, 
                         dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                         dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.stock.cantidad, dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, 
                         dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.stock.IdUbicacion, 
                         dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
                         dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor
```
