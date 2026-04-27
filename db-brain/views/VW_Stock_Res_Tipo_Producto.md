---
id: db-brain-view-vw-stock-res-tipo-producto
type: db-view
title: dbo.VW_Stock_Res_Tipo_Producto
schema: dbo
name: VW_Stock_Res_Tipo_Producto
kind: view
modify_date: 2022-10-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res_Tipo_Producto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-10-25 |
| Columnas | 63 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPropietario` | `int` | ✓ |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdStock` | `int` | ✓ |  |
| 7 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `IdRecepcionEnc` | `int` | ✓ |  |
| 12 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 13 | `codigo` | `nvarchar(50)` | ✓ |  |
| 14 | `nombre` | `nvarchar(100)` | ✓ |  |
| 15 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 16 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 17 | `lote` | `nvarchar(50)` | ✓ |  |
| 18 | `fecha_ingreso` | `datetime` | ✓ |  |
| 19 | `fecha_vence` | `datetime` | ✓ |  |
| 20 | `CantidadSF` | `float` | ✓ |  |
| 21 | `peso` | `float` | ✓ |  |
| 22 | `Cantidad` | `float` | ✓ |  |
| 23 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 24 | `dañado` | `bit` | ✓ |  |
| 25 | `factor` | `float` | ✓ |  |
| 26 | `EstadoUtilizable` | `bit` | ✓ |  |
| 27 | `IdUbicacion` | `int` | ✓ |  |
| 28 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 29 | `serial` | `nvarchar(50)` | ✓ |  |
| 30 | `añada` | `int` | ✓ |  |
| 31 | `IdIndiceRotacion` | `int` | ✓ |  |
| 32 | `alto` | `float` | ✓ |  |
| 33 | `largo` | `float` | ✓ |  |
| 34 | `ancho` | `float` | ✓ |  |
| 35 | `CantidadReservada` | `float` | ✓ |  |
| 36 | `IdTramo` | `int` | ✓ |  |
| 37 | `ancho_ubicacion` | `float` | ✓ |  |
| 38 | `largo_ubicacion` | `float` | ✓ |  |
| 39 | `alto_ubicacion` | `float` | ✓ |  |
| 40 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 41 | `existencia_min_umbas` | `float` | ✓ |  |
| 42 | `existencia_max_umbas` | `float` | ✓ |  |
| 43 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 44 | `costo` | `float` | ✓ |  |
| 45 | `existencia_min_pres` | `float` | ✓ |  |
| 46 | `existencia_max_pres` | `float` | ✓ |  |
| 47 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 48 | `IdUbicacionActual` | `int` | ✓ |  |
| 49 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 50 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 51 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 52 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 53 | `NombreTipoProducto` | `nvarchar(50)` | ✓ |  |
| 54 | `IdMarca` | `int` | ✓ |  |
| 55 | `IdFamilia` | `int` | ✓ |  |
| 56 | `Familia` | `nvarchar(50)` | ✓ |  |
| 57 | `Marca` | `nvarchar(50)` | ✓ |  |
| 58 | `IdClasificacion` | `int` | ✓ |  |
| 59 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 60 | `IdProductoParametroA` | `int` | ✓ |  |
| 61 | `IdProductoParametroB` | `int` | ✓ |  |
| 62 | `parametro_a` | `nvarchar(50)` | ✓ |  |
| 63 | `parametro_b` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_marca`
- `producto_parametro_a`
- `producto_parametro_b`
- `producto_presentacion`
- `producto_tipo`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Res_Tipo_Producto]
AS
SELECT dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, 
                  dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo, dbo.producto.nombre, 
                  dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, dbo.stock.cantidad AS CantidadSF, dbo.stock.peso, 
                  dbo.stock.cantidad / dbo.producto_presentacion.factor AS Cantidad, dbo.producto_estado.nombre AS NomEstado, dbo.producto_estado.dañado, dbo.producto_presentacion.factor, dbo.producto_estado.utilizable AS EstadoUtilizable, 
                  dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, 
                  SUM(dbo.stock_res.cantidad) AS CantidadReservada, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS ancho_ubicacion, dbo.bodega_ubicacion.largo AS largo_ubicacion, dbo.bodega_ubicacion.alto AS alto_ubicacion, 
                  dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto.existencia_min AS existencia_min_umbas, dbo.producto.existencia_max AS existencia_max_umbas, dbo.producto.codigo_barra, dbo.producto.costo, 
                  dbo.producto_presentacion.MinimoExistencia AS existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS existencia_max_pres, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, 
                  dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, 
                  dbo.producto_tipo.NombreTipoProducto, dbo.producto_marca.IdMarca, dbo.producto_familia.IdFamilia, dbo.producto_familia.nombre AS Familia, dbo.producto_marca.nombre AS Marca, dbo.producto_clasificacion.IdClasificacion, 
                  dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_parametro_a.IdProductoParametroA, dbo.producto_parametro_b.IdProductoParametroB, dbo.producto_parametro_a.Nombre AS parametro_a, 
                  dbo.producto_parametro_b.Nombre AS parametro_b
FROM     dbo.indice_rotacion RIGHT OUTER JOIN
                  dbo.producto_parametro_a RIGHT OUTER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                  dbo.producto_parametro_b ON dbo.producto.IDPRODUCTOPARAMETROB = dbo.producto_parametro_b.IdProductoParametroB ON 
                  dbo.producto_parametro_a.IdProductoParametroA = dbo.producto.IDPRODUCTOPARAMETROA LEFT OUTER JOIN
                  dbo.producto_marca ON dbo.producto.IdMarca = dbo.producto_marca.IdMarca ON dbo.indice_rotacion.IdIndiceRotacion = dbo.producto.IdIndiceRotacion FULL OUTER JOIN
                  dbo.producto_familia RIGHT OUTER JOIN
                  dbo.unidad_medida INNER JOIN
                  dbo.propietarios INNER JOIN
                  dbo.stock INNER JOIN
                  dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                  dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida INNER JOIN
                  dbo.producto_clasificacion ON dbo.propietarios.IdPropietario = dbo.producto_clasificacion.IdPropietario ON dbo.producto_familia.IdPropietario = dbo.propietarios.IdPropietario LEFT OUTER JOIN
                  dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock AND dbo.stock.IdPropietarioBodega = dbo.stock_res.IdPropietarioBodega AND dbo.stock.IdProductoBodega = dbo.stock_res.IdProductoBodega AND 
                  dbo.stock.IdUbicacion = dbo.stock_res.IdUbicacion LEFT OUTER JOIN
                  dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                  dbo.bodega_tramo INNER JOIN
                  dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia AND 
                  dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion AND dbo.producto_marca.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto_tipo.IdPropietario = dbo.propietarios.IdPropietario AND 
                  dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega, 
                  dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                  dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable, 
                  dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
                  dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, 
                  dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, 
                  dbo.stock.cantidad, dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, 
                  dbo.producto_tipo.NombreTipoProducto, dbo.producto_marca.IdMarca, dbo.producto_familia.IdFamilia, dbo.producto_familia.nombre, dbo.producto_marca.nombre, dbo.producto_clasificacion.IdClasificacion, 
                  dbo.producto_clasificacion.nombre, dbo.producto_parametro_a.IdProductoParametroA, dbo.producto_parametro_b.IdProductoParametroB, dbo.producto_parametro_a.Nombre, dbo.producto_parametro_b.Nombre
```
