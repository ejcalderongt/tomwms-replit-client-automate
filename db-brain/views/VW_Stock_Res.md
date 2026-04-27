---
id: db-brain-view-vw-stock-res
type: db-view
title: dbo.VW_Stock_Res
schema: dbo
name: VW_Stock_Res
kind: view
modify_date: 2025-06-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-13 |
| Columnas | 112 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `IdProducto` | `int` |  |  |
| 7 | `IdProductoBodega` | `int` |  |  |
| 8 | `IdStock` | `int` |  |  |
| 9 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` |  |  |
| 11 | `IdProductoEstado` | `int` |  |  |
| 12 | `IdPresentacion` | `int` | ✓ |  |
| 13 | `IdRecepcionEnc` | `int` | ✓ |  |
| 14 | `codigo` | `nvarchar(50)` | ✓ |  |
| 15 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 16 | `nombre` | `nvarchar(100)` | ✓ |  |
| 17 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 18 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 19 | `lote` | `nvarchar(50)` |  |  |
| 20 | `fecha_ingreso` | `datetime` | ✓ |  |
| 21 | `fecha_vence` | `datetime` | ✓ |  |
| 22 | `Cantidad_UMBas` | `float` |  |  |
| 23 | `CantidadSF` | `float` |  |  |
| 24 | `Cantidad_Presentacion` | `float` |  |  |
| 25 | `factor` | `float` | ✓ |  |
| 26 | `CantidadReservadaUmBas` | `float` | ✓ |  |
| 27 | `Cantidad_Reservada_Pres` | `float` | ✓ |  |
| 28 | `Disponible_UMBas` | `float` |  |  |
| 29 | `Disponible_Presentacion` | `float` | ✓ |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 32 | `dañado` | `bit` | ✓ |  |
| 33 | `EstadoUtilizable` | `bit` | ✓ |  |
| 34 | `IdUbicacion` | `int` |  |  |
| 35 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 36 | `serial` | `nvarchar(50)` | ✓ |  |
| 37 | `añada` | `int` | ✓ |  |
| 38 | `IdIndiceRotacion` | `int` | ✓ |  |
| 39 | `alto` | `float` | ✓ |  |
| 40 | `largo` | `float` | ✓ |  |
| 41 | `ancho` | `float` | ✓ |  |
| 42 | `IdTramo` | `int` | ✓ |  |
| 43 | `Ancho_ubicacion` | `float` | ✓ |  |
| 44 | `Largo_ubicacion` | `float` | ✓ |  |
| 45 | `Alto_ubicacion` | `float` | ✓ |  |
| 46 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 47 | `Existencia_min_umbas` | `float` | ✓ |  |
| 48 | `Existencia_max_umbas` | `float` | ✓ |  |
| 49 | `costo` | `float` | ✓ |  |
| 50 | `Existencia_min_pres` | `float` | ✓ |  |
| 51 | `Existencia_max_pres` | `float` | ✓ |  |
| 52 | `IdUbicacionActual` | `int` | ✓ |  |
| 53 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 54 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 55 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 56 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 57 | `activo` | `bit` | ✓ |  |
| 58 | `bloqueada` | `bit` | ✓ |  |
| 59 | `ubicacion_merma` | `bit` | ✓ |  |
| 60 | `MotivoDevolucion` | `nvarchar(50)` |  |  |
| 61 | `Codigo_Poliza` | `nvarchar(150)` |  |  |
| 62 | `Numero_poliza` | `nvarchar(50)` |  |  |
| 63 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 64 | `Familia` | `nvarchar(50)` | ✓ |  |
| 65 | `NoTO` | `nvarchar(50)` | ✓ |  |
| 66 | `Area` | `nvarchar(200)` | ✓ |  |
| 67 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 68 | `IdProductoParametroA` | `int` | ✓ |  |
| 69 | `IdProductoParametroB` | `int` | ✓ |  |
| 70 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 71 | `parametro_a` | `nvarchar(50)` | ✓ |  |
| 72 | `parametro_b` | `nvarchar(50)` | ✓ |  |
| 73 | `parametro_personalizado` | `nvarchar(50)` | ✓ |  |
| 74 | `parametro_valor` | `nvarchar(50)` | ✓ |  |
| 75 | `IdTipoProducto` | `int` | ✓ |  |
| 76 | `tipo` | `nvarchar(50)` | ✓ |  |
| 77 | `IdMarca` | `int` | ✓ |  |
| 78 | `marca` | `nvarchar(50)` | ✓ |  |
| 79 | `cantidad` | `float` |  |  |
| 80 | `doc_ingreso` | `int` | ✓ |  |
| 81 | `posiciones` | `int` |  |  |
| 82 | `valor_unitario` | `float` | ✓ |  |
| 83 | `valor_total` | `float` | ✓ |  |
| 84 | `IdEmbarcador` | `int` |  |  |
| 85 | `Shipper` | `nvarchar(150)` |  |  |
| 86 | `Regimen` | `nvarchar(50)` | ✓ |  |
| 87 | `ingreso_ticket` | `varchar(19)` |  |  |
| 88 | `IdEmpresa` | `int` |  |  |
| 89 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 90 | `ubicacion_picking` | `bit` | ✓ |  |
| 91 | `CamasPorTarima` | `float` | ✓ |  |
| 92 | `CajasPorCama` | `float` | ✓ |  |
| 93 | `IdPedido` | `bigint` | ✓ |  |
| 94 | `pallet_no_estandar` | `bit` | ✓ |  |
| 95 | `Embarcador` | `nvarchar(150)` |  |  |
| 96 | `Documento_Ingreso` | `int` |  |  |
| 97 | `CantidadReservada` | `float` | ✓ |  |
| 98 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |
| 99 | `es_rack` | `bit` | ✓ |  |
| 100 | `ReferenciaOCEnc` | `nvarchar(100)` |  |  |
| 101 | `precio_producto` | `float` | ✓ |  |
| 102 | `costo_producto` | `float` | ✓ |  |
| 103 | `costo_ingreso` | `float` | ✓ |  |
| 104 | `IdRecepcionDet` | `int` | ✓ |  |
| 105 | `Ingreso` | `nvarchar(50)` |  |  |
| 106 | `es_devolucion` | `bit` |  |  |
| 107 | `No_Linea` | `int` |  |  |
| 108 | `IdArea` | `int` |  |  |
| 109 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 110 | `no_docto` | `nvarchar(100)` | ✓ |  |
| 111 | `no_contenedor` | `nvarchar(50)` | ✓ |  |
| 112 | `IdTipoRotacion` | `int` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `indice_rotacion`
- `motivo_devolucion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `p_parametro`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_marca`
- `producto_parametro_a`
- `producto_parametro_b`
- `producto_parametros`
- `producto_presentacion`
- `producto_tipo`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_det`
- `stock_parametro`
- `stock_res`
- `tms_ticket`
- `trans_oc_det`
- `trans_oc_embarcador`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_oc_ti`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `unidad_medida`

## Quién la referencia

**2** objetos:

- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Revision_Producto` (view)

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Res]
AS
SELECT dbo.stock.IdBodega, dbo.bodega.nombre AS Bodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.IdProducto, 
                  dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.producto.codigo, 
                  dbo.producto.codigo_barra, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.fecha_vence, 
                  dbo.stock.cantidad AS Cantidad_UMBas, ISNULL(dbo.stock.cantidad, 0) AS CantidadSF, ISNULL(dbo.stock.cantidad / dbo.producto_presentacion.factor, 0) AS Cantidad_Presentacion, dbo.producto_presentacion.factor, 
                  SUM(ISNULL(dbo.stock_res.cantidad, 0)) AS CantidadReservadaUmBas, 
				  CASE WHEN ISNULL(dbo.producto_presentacion.factor, 0) > 0 THEN ROUND(SUM(ISNULL(dbo.stock_res.cantidad, 0)) / dbo.producto_presentacion.factor, 6) 
                  ELSE 0 END AS Cantidad_Reservada_Pres, dbo.stock.cantidad - ISNULL(SUM(dbo.stock_res.cantidad), 0) AS Disponible_UMBas, ISNULL(dbo.stock.cantidad / dbo.producto_presentacion.factor, 0) 
                  - (CASE WHEN ISNULL(dbo.producto_presentacion.factor, 0) > 0 THEN 
				  ROUND(SUM(ISNULL(dbo.stock_res.cantidad, 0)) / dbo.producto_presentacion.factor, 6) ELSE 0 END) AS Disponible_Presentacion, dbo.stock.peso, 
                  dbo.producto_estado.nombre AS NomEstado, dbo.producto_estado.dañado, dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, 
                  dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS Ancho_ubicacion, 
                  dbo.bodega_ubicacion.largo AS Largo_ubicacion, dbo.bodega_ubicacion.alto AS Alto_ubicacion, dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto.existencia_min AS Existencia_min_umbas, 
                  dbo.producto.existencia_max AS Existencia_max_umbas, dbo.trans_oc_det.costo, dbo.producto_presentacion.MinimoExistencia AS Existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS Existencia_max_pres, 
                  dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, 
                  dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, dbo.bodega_ubicacion.activo, dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.ubicacion_merma, ISNULL(dbo.motivo_devolucion.Nombre, 'N/A') AS MotivoDevolucion, 
                  ISNULL(dbo.trans_oc_pol.codigo_poliza, 'N/D') AS Codigo_Poliza, ISNULL(dbo.trans_oc_pol.numero_orden, 'N/D') AS Numero_poliza, dbo.trans_oc_pol.numero_orden, dbo.producto_familia.nombre AS Familia, 
                  dbo.trans_oc_pol.NoPoliza AS NoTO, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega) AS Area, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_parametro_a.IdProductoParametroA, 
                  dbo.producto_parametro_b.IdProductoParametroB, dbo.stock.atributo_variante_1, dbo.producto_parametro_a.Nombre AS parametro_a, dbo.producto_parametro_b.Nombre AS parametro_b, 
                  dbo.p_parametro.descripcion AS parametro_personalizado, CASE WHEN p_parametro.tipo = 'Númerico' THEN CAST(dbo.stock_parametro.valor_numerico AS nvarchar) 
                  WHEN p_parametro.tipo = 'Texto' THEN dbo.stock_parametro.valor_texto WHEN p_parametro.tipo = 'Fecha' THEN CONVERT(varchar, dbo.stock_parametro.valor_fecha, 101) 
                  WHEN p_parametro.tipo = 'Lógico' THEN CAST(dbo.stock_parametro.valor_logico AS nvarchar) END AS parametro_valor, dbo.producto_tipo.IdTipoProducto, dbo.producto_tipo.NombreTipoProducto AS tipo, dbo.producto_marca.IdMarca, 
                  dbo.producto_marca.nombre AS marca, dbo.stock.cantidad, dbo.trans_oc_enc.IdOrdenCompraEnc AS doc_ingreso, ISNULL(dbo.stock_det.posiciones, 1) AS posiciones, 
                  CASE WHEN dbo.trans_oc_det.costo > 0 THEN ROUND(dbo.trans_oc_det.costo, 2) ELSE 0 END AS valor_unitario, CASE WHEN dbo.trans_oc_det.cantidad_recibida > 0 THEN CASE WHEN SUM(ISNULL(dbo.stock_res.cantidad, 0)) 
                  > 0 THEN (dbo.stock.cantidad - ISNULL(SUM(dbo.stock_res.cantidad), 0)) * dbo.trans_oc_det.costo ELSE dbo.stock.cantidad * dbo.trans_oc_det.costo END END AS valor_total, ISNULL(dbo.trans_oc_det.IdEmbarcador, 0) AS IdEmbarcador, 
                  ISNULL(dbo.trans_oc_embarcador.Nombre, 'N/D') AS Shipper, dbo.bodega.nombre AS Regimen, ISNULL(CONVERT(VARCHAR(10), dbo.tms_ticket.Fecha_Ingreso, 103) + ' ' + CONVERT(VARCHAR(8), dbo.tms_ticket.Fecha_Ingreso, 14), '') 
                  AS ingreso_ticket, dbo.bodega.IdEmpresa, dbo.producto.IdTipoEtiqueta, dbo.bodega_ubicacion.ubicacion_picking, dbo.producto_presentacion.CamasPorTarima, dbo.producto_presentacion.CajasPorCama, MAX(dbo.stock_res.IdPedido) 
                  AS IdPedido, dbo.stock.pallet_no_estandar, ISNULL(dbo.trans_oc_embarcador.Nombre, 'N/D') AS Embarcador, ISNULL(dbo.trans_oc_enc.IdOrdenCompraEnc, 0) AS Documento_Ingreso, SUM(ISNULL(dbo.stock_res.cantidad, 0)) 
                  AS CantidadReservada, dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.bodega_tramo.es_rack, ISNULL(dbo.trans_oc_enc.Referencia, '') AS ReferenciaOCEnc, 
                  dbo.producto.precio AS precio_producto, dbo.producto.costo AS costo_producto, dbo.trans_re_det.costo AS costo_ingreso, dbo.stock.IdRecepcionDet, ISNULL(dbo.trans_oc_ti.Nombre, 'ND') AS Ingreso, 
                  ISNULL(dbo.trans_oc_ti.es_devolucion, 0) AS es_devolucion, ISNULL(dbo.trans_re_det.No_Linea, 0) AS No_Linea, ISNULL(dbo.bodega_ubicacion.IdArea, 0) AS IdArea, dbo.trans_oc_enc.Referencia, dbo.trans_re_oc.no_docto, 
                  dbo.trans_re_enc.no_contenedor, dbo.producto.IdTipoRotacion
FROM     dbo.p_parametro RIGHT OUTER JOIN
                  dbo.producto_parametro_a RIGHT OUTER JOIN
                  dbo.producto_parametros RIGHT OUTER JOIN
                  dbo.stock_parametro RIGHT OUTER JOIN
                  dbo.indice_rotacion RIGHT OUTER JOIN
                  dbo.producto_presentacion RIGHT OUTER JOIN
                  dbo.bodega_tramo INNER JOIN
                  dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                  dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega RIGHT OUTER JOIN
                  dbo.trans_oc_pol RIGHT OUTER JOIN
                  dbo.trans_re_oc LEFT OUTER JOIN
                  dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc RIGHT OUTER JOIN
                  dbo.producto_clasificacion RIGHT OUTER JOIN
                  dbo.producto_parametro_b RIGHT OUTER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.unidad_medida INNER JOIN
                  dbo.propietarios INNER JOIN
                  dbo.stock INNER JOIN
                  dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                  dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega ON 
                  dbo.producto_parametro_b.IdProductoParametroB = dbo.producto.IDPRODUCTOPARAMETROB ON dbo.producto_clasificacion.IdClasificacion = dbo.producto.IdClasificacion LEFT OUTER JOIN
                  dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock ON dbo.trans_re_oc.IdRecepcionEnc = dbo.stock.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.motivo_devolucion ON dbo.trans_oc_enc.IdMotivoDevolucion = dbo.motivo_devolucion.IdMotivoDevolucion ON dbo.trans_oc_pol.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON 
                  dbo.bodega_ubicacion.IdBodega = dbo.stock.IdBodega AND dbo.bodega_ubicacion.IdUbicacion = dbo.stock.IdUbicacion ON dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion ON 
                  dbo.indice_rotacion.IdIndiceRotacion = dbo.producto.IdIndiceRotacion ON dbo.stock_parametro.IdStock = dbo.stock.IdStock ON dbo.producto_parametros.IdProductoParametro = dbo.stock_parametro.IdProductoParametro AND 
                  dbo.producto_parametros.IdProducto = dbo.producto.IdProducto ON dbo.producto_parametro_a.IdProductoParametroA = dbo.producto.IDPRODUCTOPARAMETROA LEFT OUTER JOIN
                  dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                  dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia ON dbo.p_parametro.IdParametro = dbo.producto_parametros.IdParametro LEFT OUTER JOIN
                  dbo.producto_marca ON dbo.producto.IdMarca = dbo.producto_marca.IdMarca AND dbo.producto.IdPropietario = dbo.producto_marca.IdPropietario LEFT OUTER JOIN
                  dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto AND dbo.producto.IdPropietario = dbo.producto_marca.IdPropietario LEFT OUTER JOIN
                  dbo.stock_det ON dbo.stock.IdStock = dbo.stock_det.IdStock LEFT OUTER JOIN
                  dbo.trans_re_enc ON dbo.stock.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.trans_re_det ON dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND dbo.stock.IdRecepcionDet = dbo.trans_re_det.IdRecepcionDet LEFT OUTER JOIN
                  dbo.trans_oc_det ON dbo.trans_re_det.IdOrdenCompraDet = dbo.trans_oc_det.IdOrdenCompraDet AND dbo.trans_re_det.IdOrdenCompraEnc = dbo.trans_oc_det.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.trans_oc_embarcador ON dbo.trans_oc_det.IdEmbarcador = dbo.trans_oc_embarcador.IdEmbarcador INNER JOIN
                  dbo.bodega ON dbo.stock.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                  dbo.tms_ticket ON dbo.trans_oc_enc.no_ticket_tms = dbo.tms_ticket.IdTicket LEFT OUTER JOIN
                  dbo.trans_oc_ti ON dbo.trans_oc_enc.IdTipoIngresoOC = dbo.trans_oc_ti.IdTipoIngresoOC
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega, 
                  dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, 
                  dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.stock.IdUbicacion, 
                  dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, 
                  dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, 
                  dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.stock.cantidad, 
                  dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, 
                  dbo.bodega_ubicacion.orientacion_pos, dbo.motivo_devolucion.Nombre, dbo.trans_oc_pol.codigo_poliza, dbo.bodega_ubicacion.IdBodega, dbo.trans_oc_pol.numero_orden, dbo.producto_familia.nombre, dbo.trans_oc_pol.NoPoliza, 
                  dbo.trans_oc_enc.Referencia, dbo.bodega_ubicacion.IdArea, dbo.producto_clasificacion.nombre, dbo.producto_parametro_a.IdProductoParametroA, dbo.producto_parametro_b.IdProductoParametroB, 
                  dbo.producto_parametro_a.Nombre, dbo.producto_parametro_b.Nombre, dbo.stock.IdBodega, dbo.p_parametro.descripcion, dbo.p_parametro.tipo, dbo.stock_parametro.valor_numerico, dbo.stock_parametro.valor_texto, 
                  dbo.stock_parametro.valor_fecha, dbo.stock_parametro.valor_logico, dbo.producto_tipo.IdTipoProducto, dbo.producto_marca.IdMarca, dbo.producto_marca.nombre, dbo.producto_tipo.NombreTipoProducto, 
                  dbo.bodega_ubicacion.activo, dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.ubicacion_merma, dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.stock_det.posiciones, dbo.trans_oc_det.costo, dbo.trans_oc_det.cantidad_recibida, 
                  dbo.trans_oc_det.IdEmbarcador, dbo.trans_oc_embarcador.Nombre, dbo.bodega.nombre, dbo.tms_ticket.Fecha_Ingreso, dbo.bodega.IdEmpresa, dbo.producto.IdTipoEtiqueta, dbo.bodega_ubicacion.ubicacion_picking, 
                  dbo.producto_presentacion.CamasPorTarima, dbo.producto_presentacion.CajasPorCama, dbo.stock.pallet_no_estandar, dbo.bodega_tramo.es_rack, dbo.producto.precio, dbo.producto.costo, dbo.trans_re_det.costo, 
                  dbo.stock.IdRecepcionDet, dbo.trans_oc_ti.Nombre, dbo.trans_oc_ti.es_devolucion, dbo.trans_re_det.No_Linea, dbo.bodega_ubicacion.IdArea, dbo.trans_oc_enc.Referencia, dbo.trans_re_oc.no_docto, dbo.trans_re_enc.no_contenedor, 
                  dbo.producto.IdTipoRotacion
```
