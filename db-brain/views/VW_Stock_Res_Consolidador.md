---
id: db-brain-view-vw-stock-res-consolidador
type: db-view
title: dbo.VW_Stock_Res_Consolidador
schema: dbo
name: VW_Stock_Res_Consolidador
kind: view
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res_Consolidador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-05-28 |
| Columnas | 56 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `IdProducto` | `int` | ✓ |  |
| 6 | `IdProductoBodega` | `int` | ✓ |  |
| 7 | `IdStock` | `int` |  |  |
| 8 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 9 | `IdUnidadMedida` | `int` |  |  |
| 10 | `IdProductoEstado` | `int` | ✓ |  |
| 11 | `IdPresentacion` | `int` | ✓ |  |
| 12 | `IdRecepcionEnc` | `int` | ✓ |  |
| 13 | `Propietario` | `nvarchar(100)` |  |  |
| 14 | `codigo` | `nvarchar(50)` | ✓ |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |
| 16 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 17 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `lote` | `nvarchar(50)` |  |  |
| 19 | `fecha_ingreso` | `datetime` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `CantidadSF` | `float` |  |  |
| 22 | `peso` | `float` | ✓ |  |
| 23 | `Cantidad` | `float` | ✓ |  |
| 24 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 25 | `dañado` | `bit` | ✓ |  |
| 26 | `factor` | `float` | ✓ |  |
| 27 | `EstadoUtilizable` | `bit` | ✓ |  |
| 28 | `IdUbicacion` | `int` |  |  |
| 29 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 30 | `serial` | `nvarchar(50)` | ✓ |  |
| 31 | `añada` | `int` | ✓ |  |
| 32 | `IdIndiceRotacion` | `int` | ✓ |  |
| 33 | `alto` | `float` | ✓ |  |
| 34 | `largo` | `float` | ✓ |  |
| 35 | `ancho` | `float` | ✓ |  |
| 36 | `CantidadReservada` | `float` | ✓ |  |
| 37 | `IdTramo` | `int` | ✓ |  |
| 38 | `ancho_ubicacion` | `float` | ✓ |  |
| 39 | `largo_ubicacion` | `float` | ✓ |  |
| 40 | `alto_ubicacion` | `float` | ✓ |  |
| 41 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 42 | `existencia_min_umbas` | `float` | ✓ |  |
| 43 | `existencia_max_umbas` | `float` | ✓ |  |
| 44 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 45 | `costo` | `float` | ✓ |  |
| 46 | `existencia_min_pres` | `float` | ✓ |  |
| 47 | `existencia_max_pres` | `float` | ✓ |  |
| 48 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 49 | `IdUbicacionActual` | `int` | ✓ |  |
| 50 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 51 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 52 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 53 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 54 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |
| 55 | `IdEmpresa` | `int` | ✓ |  |
| 56 | `consolidador` | `int` |  |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `trans_oc_det`
- `trans_oc_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE View  VW_Stock_Res_Consolidador as
SELECT        dbo.producto_bodega.IdBodega, dbo.bodega.codigo AS Bodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, 
                         dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, 
                         dbo.producto.codigo, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, 
                         dbo.stock.cantidad AS CantidadSF, dbo.stock.peso, dbo.stock.cantidad / dbo.producto_presentacion.factor AS Cantidad, dbo.producto_estado.nombre AS NomEstado, dbo.producto_estado.dañado, 
                         dbo.producto_presentacion.factor, dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, dbo.producto.IdIndiceRotacion, 
                         dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, SUM(dbo.stock_res.cantidad) AS CantidadReservada, dbo.bodega_ubicacion.IdTramo, 
                         dbo.bodega_ubicacion.ancho AS ancho_ubicacion, dbo.bodega_ubicacion.largo AS largo_ubicacion, dbo.bodega_ubicacion.alto AS alto_ubicacion, dbo.indice_rotacion.Descripcion AS IndiceRotacion, 
                         dbo.producto.existencia_min AS existencia_min_umbas, dbo.producto.existencia_max AS existencia_max_umbas, dbo.producto.codigo_barra, dbo.producto.costo, 
                         dbo.producto_presentacion.MinimoExistencia AS existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS existencia_max_pres, dbo.stock.atributo_variante_1, 
                         dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, 
                         dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, dbo.stock.IdBodega) AS Nombre_Completo, dbo.bodega.IdEmpresa,
						 oc_enc.IdPropietarioBodega as consolidador
FROM            dbo.producto_estado INNER JOIN
                         dbo.unidad_medida INNER JOIN
                         dbo.propietarios INNER JOIN
                         dbo.stock INNER JOIN
                         dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                         dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON dbo.producto_estado.IdEstado = dbo.stock.IdProductoEstado LEFT OUTER JOIN
                         dbo.bodega_tramo INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND 
                         dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                         dbo.producto_bodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                         dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega LEFT OUTER JOIN
                         dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock AND dbo.stock.IdPropietarioBodega = dbo.stock_res.IdPropietarioBodega AND dbo.stock.IdProductoBodega = dbo.stock_res.IdProductoBodega AND 
                         dbo.stock.IdUbicacion = dbo.stock_res.IdUbicacion LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
						 INNER JOIN dbo.trans_oc_det oc_det ON dbo.stock.IdPropietarioBodega = oc_det.IdPropietarioBodega
						 INNER JOIN dbo.trans_oc_enc oc_enc ON oc_det.IdOrdenCompraEnc = oc_enc.IdOrdenCompraEnc
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega, 
                         dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, 
                         dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable, 
                         dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, 
                         dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, 
                         dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, 
                         dbo.producto_presentacion.MaximoExistencia, dbo.stock.cantidad, dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, 
                         dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.bodega_tramo.descripcion, dbo.bodega_ubicacion.orientacion_pos, dbo.bodega_tramo.es_rack, dbo.bodega_tramo.descripcion, 
                         dbo.bodega_tramo.IdBodega, dbo.bodega_tramo.IdTramo, dbo.stock.IdBodega, dbo.bodega.codigo, dbo.bodega.IdEmpresa,
						  oc_enc.IdPropietarioBodega
```
