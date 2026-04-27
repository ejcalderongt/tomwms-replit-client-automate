---
id: db-brain-view-vw-stock-resumen-20220407
type: db-view
title: dbo.VW_Stock_Resumen_20220407
schema: dbo
name: VW_Stock_Resumen_20220407
kind: view
modify_date: 2022-05-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Resumen_20220407`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-10 |
| Columnas | 70 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdStock` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProductoBodega` | `int` |  |  |
| 5 | `IdProductoEstado` | `int` | ✓ |  |
| 6 | `IdPresentacion` | `int` | ✓ |  |
| 7 | `IdUnidadMedida` | `int` | ✓ |  |
| 8 | `IdUbicacion` | `int` |  |  |
| 9 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 10 | `IdRecepcionEnc` | `int` | ✓ |  |
| 11 | `IdRecepcionDet` | `int` | ✓ |  |
| 12 | `IdPedidoEnc` | `int` | ✓ |  |
| 13 | `IdPickingEnc` | `int` | ✓ |  |
| 14 | `IdDespachoEnc` | `int` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` |  |  |
| 16 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 17 | `serial` | `nvarchar(50)` | ✓ |  |
| 18 | `cantidad` | `float` |  |  |
| 19 | `fecha_ingreso` | `datetime` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `uds_lic_plate` | `float` | ✓ |  |
| 22 | `no_bulto` | `int` | ✓ |  |
| 23 | `fecha_manufactura` | `datetime` | ✓ |  |
| 24 | `añada` | `int` | ✓ |  |
| 25 | `user_agr` | `nvarchar(50)` |  |  |
| 26 | `fec_agr` | `datetime` |  |  |
| 27 | `user_mod` | `nvarchar(50)` |  |  |
| 28 | `fec_mod` | `datetime` |  |  |
| 29 | `activo` | `bit` |  |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `temperatura` | `float` | ✓ |  |
| 32 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 33 | `pallet_no_estandar` | `bit` | ✓ |  |
| 34 | `IdProducto` | `int` |  |  |
| 35 | `IdIndiceRotacion` | `int` | ✓ |  |
| 36 | `codigo` | `nvarchar(50)` | ✓ |  |
| 37 | `nombre` | `nvarchar(100)` | ✓ |  |
| 38 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 39 | `existencia_min` | `float` | ✓ |  |
| 40 | `existencia_max` | `float` | ✓ |  |
| 41 | `Familia` | `nvarchar(50)` | ✓ |  |
| 42 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 43 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 44 | `IndiceRotacion` | `nvarchar(50)` |  |  |
| 45 | `IdTramo` | `int` |  |  |
| 46 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 47 | `Ancho_ubicacion` | `float` | ✓ |  |
| 48 | `Largo_ubicacion` | `float` | ✓ |  |
| 49 | `Alto_ubicacion` | `float` | ✓ |  |
| 50 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 51 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 52 | `IdUbicacionActual` | `int` |  |  |
| 53 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 54 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 55 | `Posiciones` | `int` |  |  |
| 56 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 57 | `alto` | `float` | ✓ |  |
| 58 | `largo` | `float` | ✓ |  |
| 59 | `ancho` | `float` | ✓ |  |
| 60 | `factor` | `float` | ✓ |  |
| 61 | `Existencia_min_pres` | `float` | ✓ |  |
| 62 | `Existencia_max_pres` | `float` | ✓ |  |
| 63 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 64 | `valor_fecha` | `datetime` | ✓ |  |
| 65 | `valor_numerico` | `float` | ✓ |  |
| 66 | `valor_logico` | `bit` | ✓ |  |
| 67 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 68 | `Embarcador` | `nvarchar(150)` | ✓ |  |
| 69 | `Area` | `nvarchar(200)` | ✓ |  |
| 70 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `p_parametro`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_parametros`
- `producto_presentacion`
- `stock`
- `stock_det`
- `stock_parametro`
- `stock_res`
- `trans_oc_det`
- `trans_oc_embarcador`
- `trans_oc_enc`
- `trans_re_det`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Resumen_20220407]
AS
SELECT        dbo.stock.IdBodega, dbo.stock.IdStock, dbo.stock.IdPropietarioBodega, dbo.stock.IdProductoBodega, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdUnidadMedida, dbo.stock.IdUbicacion, 
                         dbo.stock.IdUbicacion_anterior, dbo.stock.IdRecepcionEnc, dbo.stock.IdRecepcionDet, dbo.stock.IdPedidoEnc, dbo.stock.IdPickingEnc, dbo.stock.IdDespachoEnc, dbo.stock.lote, dbo.stock.lic_plate, dbo.stock.serial, 
                         dbo.stock.cantidad, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, dbo.stock.uds_lic_plate, dbo.stock.no_bulto, dbo.stock.fecha_manufactura, dbo.stock.añada, dbo.stock.user_agr, dbo.stock.fec_agr, dbo.stock.user_mod, 
                         dbo.stock.fec_mod, dbo.stock.activo, dbo.stock.peso, dbo.stock.temperatura, dbo.stock.atributo_variante_1, dbo.stock.pallet_no_estandar, dbo.producto.IdProducto, dbo.producto.IdIndiceRotacion, dbo.producto.codigo, 
                         dbo.producto.nombre, dbo.producto.codigo_barra, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto_familia.nombre AS Familia, dbo.producto_clasificacion.nombre AS Clasificacion, 
                         dbo.unidad_medida.Nombre AS UnidadMedida, dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, 
                         dbo.bodega_ubicacion.ancho AS Ancho_ubicacion, dbo.bodega_ubicacion.largo AS Largo_ubicacion, dbo.bodega_ubicacion.alto AS Alto_ubicacion, dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, 
                         dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, dbo.producto_estado.nombre AS NomEstado, 
                         ISNULL(dbo.stock_det.posiciones, 1) AS Posiciones, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, 
                         dbo.producto_presentacion.factor, dbo.producto_presentacion.MinimoExistencia AS Existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS Existencia_max_pres, dbo.p_parametro.descripcion, 
                         dbo.stock_parametro.valor_fecha, dbo.stock_parametro.valor_numerico, dbo.stock_parametro.valor_logico, dbo.stock_parametro.valor_texto, dbo.trans_oc_embarcador.Nombre AS Embarcador,
						 dbo.Nombre_Area(bodega_ubicacion.IdArea,bodega_ubicacion.IdBodega) as Area, dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) as NombreUbicacion
FROM            dbo.trans_oc_det INNER JOIN
                         dbo.trans_re_det ON dbo.trans_oc_det.IdOrdenCompraEnc = dbo.trans_re_det.IdOrdenCompraEnc AND dbo.trans_oc_det.IdOrdenCompraDet = dbo.trans_re_det.IdOrdenCompraDet AND 
                         dbo.trans_oc_det.IdProductoBodega = dbo.trans_re_det.IdProductoBodega INNER JOIN
                         dbo.trans_oc_enc ON dbo.trans_oc_det.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc LEFT OUTER JOIN
                         dbo.trans_oc_embarcador ON dbo.trans_oc_det.IdEmbarcador = dbo.trans_oc_embarcador.IdEmbarcador RIGHT OUTER JOIN
                         dbo.bodega_ubicacion INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.stock INNER JOIN
                         dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion INNER JOIN
                         dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia INNER JOIN
                         dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion INNER JOIN
                         dbo.unidad_medida ON dbo.stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida ON dbo.bodega_ubicacion.IdUbicacion = dbo.stock.IdUbicacion AND dbo.bodega_ubicacion.IdBodega = dbo.stock.IdBodega INNER JOIN
                         dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado AND dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion ON 
                         dbo.trans_re_det.IdRecepcionEnc = dbo.stock.IdRecepcionEnc AND dbo.trans_re_det.IdRecepcionDet = dbo.stock.IdRecepcionDet AND dbo.trans_re_det.IdProductoBodega = dbo.stock.IdProductoBodega LEFT OUTER JOIN
                         dbo.stock_res ON dbo.stock.IdBodega = dbo.stock_res.IdBodega AND dbo.stock.IdStock = dbo.stock_res.IdStock LEFT OUTER JOIN
                         dbo.p_parametro INNER JOIN
                         dbo.producto_parametros ON dbo.p_parametro.IdParametro = dbo.producto_parametros.IdParametro LEFT OUTER JOIN
                         dbo.stock_parametro ON dbo.producto_parametros.IdProductoParametro = dbo.stock_parametro.IdProductoParametro ON dbo.stock.IdStock = dbo.stock_parametro.IdStock LEFT OUTER JOIN
                         dbo.stock_det ON dbo.stock.IdStock = dbo.stock_det.IdStock
GROUP BY dbo.stock.IdBodega, dbo.stock.IdStock, dbo.stock.IdPropietarioBodega, dbo.stock.IdProductoBodega, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdUnidadMedida, dbo.stock.IdUbicacion, 
                         dbo.stock.IdUbicacion_anterior, dbo.stock.IdRecepcionEnc, dbo.stock.IdRecepcionDet, dbo.stock.IdPedidoEnc, dbo.stock.IdPickingEnc, dbo.stock.IdDespachoEnc, dbo.stock.lote, dbo.stock.lic_plate, dbo.stock.serial, 
                         dbo.stock.cantidad, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, dbo.stock.uds_lic_plate, dbo.stock.no_bulto, dbo.stock.fecha_manufactura, dbo.stock.añada, dbo.stock.user_agr, dbo.stock.fec_agr, dbo.stock.user_mod, 
                         dbo.stock.fec_mod, dbo.stock.peso, dbo.stock.temperatura, dbo.stock.atributo_variante_1, dbo.producto.IdProducto, dbo.producto.IdIndiceRotacion, dbo.producto.codigo, dbo.producto.nombre, dbo.producto.codigo_barra, 
                         dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto_familia.nombre, dbo.producto_clasificacion.nombre, dbo.unidad_medida.Nombre, dbo.indice_rotacion.Descripcion, dbo.bodega_ubicacion.IdTramo, 
                         dbo.bodega_ubicacion.descripcion, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.IdUbicacion, 
                         dbo.bodega_tramo.descripcion, dbo.producto_estado.nombre, ISNULL(dbo.stock_det.posiciones, 1), dbo.producto_presentacion.nombre, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, 
                         dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.p_parametro.descripcion, 
                         dbo.stock_parametro.valor_fecha, dbo.stock_parametro.valor_numerico, dbo.stock_parametro.valor_texto, dbo.stock.activo, dbo.stock.pallet_no_estandar, dbo.stock_parametro.valor_logico, dbo.trans_oc_embarcador.Nombre,
						 bodega_ubicacion.IdArea, bodega_ubicacion.IdBodega
```
