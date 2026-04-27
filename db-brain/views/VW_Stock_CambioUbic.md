---
id: db-brain-view-vw-stock-cambioubic
type: db-view
title: dbo.VW_Stock_CambioUbic
schema: dbo
name: VW_Stock_CambioUbic
kind: view
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_CambioUbic`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-02-01 |
| Columnas | 73 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `IdProducto` | `int` |  |  |
| 6 | `IdProductoBodega` | `int` |  |  |
| 7 | `IdStock` | `int` |  |  |
| 8 | `pallet_no_estandar` | `bit` | ✓ |  |
| 9 | `Posiciones` | `int` |  |  |
| 10 | `IdUbicacion_anterior` | `int` |  |  |
| 11 | `IdUnidadMedida` | `int` |  |  |
| 12 | `IdProductoEstado` | `int` | ✓ |  |
| 13 | `IdPresentacion` | `int` | ✓ |  |
| 14 | `IdRecepcionEnc` | `int` | ✓ |  |
| 15 | `Propietario` | `nvarchar(100)` |  |  |
| 16 | `codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `nombre` | `nvarchar(100)` | ✓ |  |
| 18 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 19 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 20 | `lote` | `nvarchar(50)` |  |  |
| 21 | `fecha_ingreso` | `datetime` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `CantidadSF` | `float` | ✓ |  |
| 24 | `peso` | `float` | ✓ |  |
| 25 | `Cantidad` | `float` |  |  |
| 26 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 27 | `dañado` | `bit` | ✓ |  |
| 28 | `Factor` | `float` |  |  |
| 29 | `EstadoUtilizable` | `bit` | ✓ |  |
| 30 | `IdUbicacion` | `int` |  |  |
| 31 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 32 | `serial` | `nvarchar(50)` | ✓ |  |
| 33 | `añada` | `int` | ✓ |  |
| 34 | `IdIndiceRotacion` | `int` | ✓ |  |
| 35 | `alto` | `float` | ✓ |  |
| 36 | `largo` | `float` | ✓ |  |
| 37 | `ancho` | `float` | ✓ |  |
| 38 | `CantidadReservada` | `float` | ✓ |  |
| 39 | `IdTramo` | `int` |  |  |
| 40 | `ancho_ubicacion` | `float` | ✓ |  |
| 41 | `largo_ubicacion` | `float` | ✓ |  |
| 42 | `alto_ubicacion` | `float` | ✓ |  |
| 43 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 44 | `existencia_min_umbas` | `float` | ✓ |  |
| 45 | `existencia_max_umbas` | `float` | ✓ |  |
| 46 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 47 | `costo` | `float` | ✓ |  |
| 48 | `existencia_min_pres` | `float` | ✓ |  |
| 49 | `existencia_max_pres` | `float` | ✓ |  |
| 50 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 51 | `IdUbicacionActual` | `int` |  |  |
| 52 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 53 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 54 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 55 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 56 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |
| 57 | `IdEmpresa` | `int` |  |  |
| 58 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 59 | `numero_orden` | `nvarchar(50)` |  |  |
| 60 | `codigo_poliza` | `nvarchar(150)` |  |  |
| 61 | `Documento_Ingreso` | `int` |  |  |
| 62 | `ubicacion_picking` | `bit` | ✓ |  |
| 63 | `CamasPorTarima` | `float` | ✓ |  |
| 64 | `CajasPorCama` | `float` | ✓ |  |
| 65 | `es_rack` | `bit` | ✓ |  |
| 66 | `Area` | `nvarchar(200)` | ✓ |  |
| 67 | `NoTO` | `nvarchar(50)` | ✓ |  |
| 68 | `clasificacion` | `nvarchar(50)` |  |  |
| 69 | `IdEmbarcador` | `int` | ✓ |  |
| 70 | `Embarcador` | `nvarchar(150)` | ✓ |  |
| 71 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |
| 72 | `IdPedido` | `bigint` | ✓ |  |
| 73 | `IdRecepcionDet` | `int` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_det`
- `stock_res`
- `trans_oc_det`
- `trans_oc_embarcador`
- `trans_oc_pol`
- `trans_re_enc`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_CambioUbic]
AS
SELECT dbo.producto_bodega.IdBodega, dbo.bodega.codigo AS Bodega, dbo.propietarios.IdPropietario, 
dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, 
0 AS IdStock, 
                  dbo.stock.pallet_no_estandar, ISNULL(dbo.stock_det.posiciones, 1) AS Posiciones,
				  0 AS IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, 
				  dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, 
                  dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo,
				  dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, 
				  dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, 
                  dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, 
				  MAX(ISNULL(dbo.stock.cantidad, 0)) AS CantidadSF, dbo.stock.peso, 
				  ISNULL(MAX(dbo.stock.cantidad) / dbo.producto_presentacion.factor, 0) AS Cantidad, 
				  pr_est.nombre AS NomEstado, 
                  pr_est.dañado, ISNULL(dbo.producto_presentacion.factor, 0) AS Factor, 
				  pr_est.utilizable AS EstadoUtilizable, dbo.stock.IdUbicacion, 
				  dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, 
				  dbo.producto.IdIndiceRotacion, 
                  dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, 
				  dbo.producto_presentacion.ancho, 
				  SUM(dbo.stock_res.cantidad)  AS CantidadReservada, dbo.bodega_ubicacion.IdTramo, 
                  dbo.bodega_ubicacion.ancho AS ancho_ubicacion, dbo.bodega_ubicacion.largo AS largo_ubicacion,
				  dbo.bodega_ubicacion.alto AS alto_ubicacion, 
				  dbo.indice_rotacion.Descripcion AS IndiceRotacion, 
                  dbo.producto.existencia_min AS existencia_min_umbas,
				  dbo.producto.existencia_max AS existencia_max_umbas, 
				  dbo.producto.codigo_barra, dbo.producto.costo, 
				  dbo.producto_presentacion.MinimoExistencia AS existencia_min_pres, 
                  dbo.producto_presentacion.MaximoExistencia AS existencia_max_pres, 
				  dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual,
				  dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, 
                  dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, 
				  dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, 
				  dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, 
				  dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, 
                  dbo.stock.IdBodega) AS Nombre_Completo, 
				  dbo.bodega.IdEmpresa, dbo.producto.IdTipoEtiqueta, 
				  ISNULL(oc_pol.numero_orden, 'ND') AS numero_orden, 
				  ISNULL(oc_pol.codigo_poliza, 'ND') AS codigo_poliza, 
                  ISNULL(re_oc.IdOrdenCompraEnc, 0) AS Documento_Ingreso, 
				  dbo.bodega_ubicacion.ubicacion_picking,
				  dbo.producto_presentacion.CamasPorTarima,
				  dbo.producto_presentacion.CajasPorCama, dbo.bodega_tramo.es_rack, 
                  dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, 
				  dbo.bodega_ubicacion.IdBodega) AS Area, 
				  oc_pol.NoPoliza AS NoTO, ISNULL(pr_clas.nombre, 'ND') AS clasificacion,
				  oc_det.IdEmbarcador, oc_ship.Nombre AS Embarcador, 
                  dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, 
				  dbo.bodega_ubicacion.IdBodega) AS NombreUbicacion, 
				  MAX(ISNULL(dbo.stock_res.IdPedido, 0)) AS IdPedido, dbo.stock.IdRecepcionDet
FROM     dbo.stock INNER JOIN
                  dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                  dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                  dbo.unidad_medida ON dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida INNER JOIN
                  dbo.producto_estado AS pr_est ON dbo.stock.IdProductoEstado = pr_est.IdEstado INNER JOIN
                  dbo.bodega_tramo INNER JOIN
                  dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON 
                  dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                  dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega LEFT OUTER JOIN
                  dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock AND dbo.stock.IdPropietarioBodega = dbo.stock_res.IdPropietarioBodega AND dbo.stock.IdProductoBodega = dbo.stock_res.IdProductoBodega AND 
                  dbo.stock.IdUbicacion = dbo.stock_res.IdUbicacion AND dbo.stock.IdBodega = dbo.stock_res.idbodega AND dbo.stock.IdProductoEstado = dbo.stock_res.IdProductoEstado AND dbo.stock.IdProductoEstado = pr_est.IdEstado AND 
                  dbo.stock_res.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
                  dbo.stock_det ON dbo.stock.IdStock = dbo.stock_det.IdStock LEFT OUTER JOIN
                  dbo.trans_re_enc AS re_enc ON re_enc.IdRecepcionEnc = dbo.stock.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.trans_re_oc AS re_oc ON re_oc.IdRecepcionEnc = re_enc.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.trans_oc_pol AS oc_pol ON oc_pol.IdOrdenCompraEnc = re_oc.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.producto_clasificacion AS pr_clas ON dbo.producto.IdClasificacion = pr_clas.IdClasificacion LEFT OUTER JOIN
                  dbo.trans_oc_det AS oc_det ON oc_det.IdOrdenCompraEnc = re_oc.IdOrdenCompraEnc AND oc_det.IdUnidadMedidaBasica = dbo.producto.IdUnidadMedidaBasica AND oc_det.IdProductoBodega = dbo.stock.IdProductoBodega AND 
                  oc_det.IdOrdenCompraDet = dbo.stock.IdRecepcionDet LEFT OUTER JOIN
                  dbo.trans_oc_embarcador AS oc_ship ON oc_det.IdEmbarcador = oc_ship.IdEmbarcador
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.bodega_ubicacion.IdUbicacion, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, 
                  dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, 
                  dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, pr_est.nombre, pr_est.utilizable, pr_est.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, 
                  dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, 
                  dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, 
                  dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, 
                  dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.bodega_tramo.descripcion, dbo.bodega_ubicacion.orientacion_pos, dbo.bodega_tramo.es_rack, dbo.bodega_tramo.descripcion, dbo.bodega_tramo.IdBodega, 
                  dbo.bodega_tramo.IdTramo, dbo.stock.IdBodega, dbo.bodega.codigo, dbo.bodega.IdEmpresa, dbo.stock_det.posiciones, dbo.stock.pallet_no_estandar, dbo.producto.IdTipoEtiqueta, oc_pol.numero_orden, oc_pol.codigo_poliza, 
                  re_oc.IdOrdenCompraEnc, dbo.bodega_ubicacion.ubicacion_picking, dbo.producto_presentacion.CamasPorTarima, dbo.producto_presentacion.CajasPorCama, dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega, 
                  oc_pol.NoPoliza, pr_clas.nombre, oc_det.IdEmbarcador, oc_ship.Nombre, dbo.stock.IdStock, dbo.stock.IdRecepcionDet
```
