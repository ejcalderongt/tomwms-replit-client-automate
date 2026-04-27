---
id: db-brain-view-vw-stock-resumen
type: db-view
title: dbo.VW_Stock_Resumen
schema: dbo
name: VW_Stock_Resumen
kind: view
modify_date: 2022-09-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Resumen`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-09-29 |
| Columnas | 81 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` |  |  |
| 5 | `IdProductoBodega` | `int` |  |  |
| 6 | `IdStock` | `int` |  |  |
| 7 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` |  |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `IdRecepcionEnc` | `int` | ✓ |  |
| 12 | `Propietario` | `nvarchar(100)` |  |  |
| 13 | `codigo` | `nvarchar(50)` | ✓ |  |
| 14 | `Barra` | `nvarchar(35)` | ✓ |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |
| 16 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 17 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `lote` | `nvarchar(50)` |  |  |
| 19 | `Ingreso` | `datetime` | ✓ |  |
| 20 | `Vence` | `datetime` | ✓ |  |
| 21 | `Cantidad_UMBas` | `float` |  |  |
| 22 | `Cantidad_Presentacion` | `float` |  |  |
| 23 | `factor` | `float` | ✓ |  |
| 24 | `CantidadReservadaUmBas` | `float` | ✓ |  |
| 25 | `Cantidad_Reservada_Pres` | `float` | ✓ |  |
| 26 | `Disponible_UMBas` | `float` |  |  |
| 27 | `Disponible_Presentacion` | `float` | ✓ |  |
| 28 | `peso` | `float` | ✓ |  |
| 29 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 30 | `UbicacionCompleta` | `nvarchar(200)` | ✓ |  |
| 31 | `dañado` | `bit` | ✓ |  |
| 32 | `EstadoUtilizable` | `bit` | ✓ |  |
| 33 | `IdUbicacion` | `int` |  |  |
| 34 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 35 | `serial` | `nvarchar(50)` | ✓ |  |
| 36 | `añada` | `int` | ✓ |  |
| 37 | `IdIndiceRotacion` | `int` | ✓ |  |
| 38 | `alto` | `float` | ✓ |  |
| 39 | `largo` | `float` | ✓ |  |
| 40 | `ancho` | `float` | ✓ |  |
| 41 | `IdTramo` | `int` | ✓ |  |
| 42 | `Ancho_ubicacion` | `float` | ✓ |  |
| 43 | `Largo_ubicacion` | `float` | ✓ |  |
| 44 | `Alto_ubicacion` | `float` | ✓ |  |
| 45 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 46 | `Existencia_min_umbas` | `float` | ✓ |  |
| 47 | `Existencia_max_umbas` | `float` | ✓ |  |
| 48 | `costo` | `float` | ✓ |  |
| 49 | `Existencia_min_pres` | `float` | ✓ |  |
| 50 | `Existencia_max_pres` | `float` | ✓ |  |
| 51 | `IdUbicacionActual` | `int` | ✓ |  |
| 52 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 53 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 54 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 55 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 56 | `MotivoDevolucion` | `nvarchar(50)` |  |  |
| 57 | `Codigo_Poliza` | `nvarchar(150)` |  |  |
| 58 | `Numero_poliza` | `nvarchar(50)` |  |  |
| 59 | `Familia` | `nvarchar(50)` | ✓ |  |
| 60 | `NoTO` | `nvarchar(50)` | ✓ |  |
| 61 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 62 | `Area` | `nvarchar(200)` | ✓ |  |
| 63 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 64 | `IdProductoParametroA` | `int` | ✓ |  |
| 65 | `IdProductoParametroB` | `int` | ✓ |  |
| 66 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 67 | `parametro_a` | `nvarchar(50)` | ✓ |  |
| 68 | `parametro_b` | `nvarchar(50)` | ✓ |  |
| 69 | `parametro_personalizado` | `nvarchar(50)` | ✓ |  |
| 70 | `parametro_valor` | `float` | ✓ |  |
| 71 | `IdTipoProducto` | `int` | ✓ |  |
| 72 | `tipo` | `nvarchar(50)` | ✓ |  |
| 73 | `IdMarca` | `int` | ✓ |  |
| 74 | `marca` | `nvarchar(50)` | ✓ |  |
| 75 | `cantidad` | `float` |  |  |
| 76 | `doc_ingreso` | `int` | ✓ |  |
| 77 | `posiciones` | `int` |  |  |
| 78 | `valor_unitario` | `float` | ✓ |  |
| 79 | `valor_total` | `float` | ✓ |  |
| 80 | `IdEmbarcador` | `int` |  |  |
| 81 | `Shipper` | `nvarchar(150)` |  |  |

## Consume

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
- `trans_oc_det`
- `trans_oc_embarcador`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Resumen] AS 
SELECT     dbo.stock.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.stock.IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida,
                  dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo, dbo.producto.codigo_barra AS Barra, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida,
                  dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.fecha_ingreso AS Ingreso, dbo.stock.fecha_vence AS Vence, dbo.stock.cantidad AS Cantidad_UMBas, ISNULL(dbo.stock.cantidad / dbo.producto_presentacion.factor, 0)
                  AS Cantidad_Presentacion, dbo.producto_presentacion.factor, SUM(ISNULL(dbo.stock_res.cantidad, 0)) AS CantidadReservadaUmBas, CASE WHEN ISNULL(dbo.producto_presentacion.factor, 0) > 0 THEN ROUND(SUM(ISNULL(dbo.stock_res.cantidad, 0))
                  / dbo.producto_presentacion.factor, 6) ELSE 0 END AS Cantidad_Reservada_Pres, dbo.stock.cantidad - ISNULL(SUM(dbo.stock_res.cantidad), 0) AS Disponible_UMBas, ISNULL(dbo.stock.cantidad / dbo.producto_presentacion.factor, 0)
                  - (CASE WHEN ISNULL(dbo.producto_presentacion.factor, 0) > 0 THEN ROUND(SUM(ISNULL(dbo.stock_res.cantidad, 0)) / dbo.producto_presentacion.factor, 6) ELSE 0 END) AS Disponible_Presentacion, dbo.stock.peso, dbo.producto_estado.nombre AS NomEstado,
                  dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS UbicacionCompleta, dbo.producto_estado.dañado, dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.stock.IdUbicacion, dbo.stock.lic_plate,
                  dbo.stock.serial, dbo.stock.añada, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS Ancho_ubicacion,
                  dbo.bodega_ubicacion.largo AS Largo_ubicacion, dbo.bodega_ubicacion.alto AS Alto_ubicacion, dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto.existencia_min AS Existencia_min_umbas, dbo.producto.existencia_max AS Existencia_max_umbas,
                  dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia AS Existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS Existencia_max_pres, dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual,
                  dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, ISNULL(dbo.motivo_devolucion.Nombre, 'N/A')
                  AS MotivoDevolucion, ISNULL(dbo.trans_oc_pol.codigo_poliza, 'N/D') AS Codigo_Poliza, ISNULL(dbo.trans_oc_pol.numero_orden, 'N/D') AS Numero_poliza, dbo.producto_familia.nombre AS Familia, dbo.trans_oc_pol.NoPoliza AS NoTO, dbo.trans_oc_enc.Referencia,
                  dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega) AS Area, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_parametro_a.IdProductoParametroA, dbo.producto_parametro_b.IdProductoParametroB, stock.atributo_variante_1,
                  dbo.producto_parametro_a.Nombre AS parametro_a, dbo.producto_parametro_b.Nombre AS parametro_b,
                  dbo.p_parametro.descripcion AS parametro_personalizado,
                    case
                        when p_parametro.tipo = 'Númerico' then dbo.stock_parametro.valor_numerico
                        when p_parametro.tipo = 'Texto' then dbo.stock_parametro.valor_texto
                        when p_parametro.tipo = 'Fecha' then  convert(varchar, dbo.stock_parametro.valor_fecha, 101)   
                        when p_parametro.tipo = 'Lógico' then dbo.stock_parametro.valor_logico end as parametro_valor,

				 dbo.producto_tipo.IdTipoProducto, dbo.producto_tipo.NombreTipoProducto as tipo,
				 dbo.producto_marca.IdMarca,dbo.producto_marca.nombre as marca,
				 dbo.stock.cantidad,
				 dbo.trans_oc_enc.IdOrdenCompraEnc as doc_ingreso,


				 ISNULL(dbo.stock_det.posiciones, 1) posiciones,

				 CASE WHEN dbo.trans_oc_det.costo > 0 THEN ROUND(dbo.trans_oc_det.costo, 2) ELSE 0 END valor_unitario, 
				  
				  CASE WHEN dbo.trans_oc_det.cantidad_recibida > 0 THEN 
										CASE WHEN SUM(ISNULL(dbo.stock_res.cantidad, 0)) > 0 
												THEN (dbo.stock.cantidad - ISNULL(SUM(dbo.stock_res.cantidad), 0)) * dbo.trans_oc_det.costo 
												ELSE dbo.stock.cantidad * dbo.trans_oc_det.costo END 
				  END valor_total,
				  ISNULL(dbo.trans_oc_det.IdEmbarcador,'') IdEmbarcador, ISNULL(dbo.trans_oc_embarcador.Nombre,'N/D') as Shipper

FROM        dbo.p_parametro RIGHT OUTER JOIN
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
                  dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON
                  dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega ON dbo.producto_parametro_b.IdProductoParametroB = dbo.producto.IDPRODUCTOPARAMETROB ON dbo.producto_clasificacion.IdClasificacion = dbo.producto.IdClasificacion LEFT OUTER JOIN
                  dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock ON dbo.trans_re_oc.IdRecepcionEnc = dbo.stock.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.motivo_devolucion ON dbo.trans_oc_enc.IdMotivoDevolucion = dbo.motivo_devolucion.IdMotivoDevolucion ON dbo.trans_oc_pol.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON dbo.bodega_ubicacion.IdBodega = dbo.stock.IdBodega AND
                  dbo.bodega_ubicacion.IdUbicacion = dbo.stock.IdUbicacion ON dbo.producto_presentacion.IdPresentacion = dbo.stock.IdPresentacion ON dbo.indice_rotacion.IdIndiceRotacion = dbo.producto.IdIndiceRotacion ON dbo.stock_parametro.IdStock = dbo.stock.IdStock ON
                  dbo.producto_parametros.IdProductoParametro = dbo.stock_parametro.IdProductoParametro AND dbo.producto_parametros.IdProducto = dbo.producto.IdProducto ON
                  dbo.producto_parametro_a.IdProductoParametroA = dbo.producto.IDPRODUCTOPARAMETROA LEFT OUTER JOIN
                  dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                  dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia ON dbo.p_parametro.IdParametro = dbo.producto_parametros.IdParametro
				  LEFT OUTER JOIN dbo.producto_marca on dbo.producto.IdMarca = dbo.producto_marca.IdMarca
				  and dbo.producto.IdPropietario = dbo.producto_marca.IdPropietario
				  LEFT OUTER JOIN dbo.producto_tipo on dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto
				  and dbo.producto.IdPropietario = dbo.producto_marca.IdPropietario

				  LEFT OUTER JOIN dbo.stock_det on dbo.stock.IdStock = dbo.stock_det.IdStock
				 left outer join dbo.trans_re_enc on dbo.stock.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc
				 left outer join dbo.trans_re_det on dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc 
				 and dbo.stock.IdRecepcionDet = dbo.trans_re_det.IdRecepciondet
				 left outer join  dbo.trans_oc_det on dbo.trans_re_det.IdOrdenCompraDet = dbo.trans_oc_det.IdOrdenCompraDet 
				 and dbo.trans_re_det.IdOrdenCompraEnc = dbo.trans_oc_det.IdOrdenCompraEnc
				 
				 LEFT OUTER JOIN dbo.trans_oc_embarcador on dbo.trans_oc_det.IdEmbarcador = dbo.trans_oc_embarcador.IdEmbarcador
				
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega,
                  dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada,
                  dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso,
                  dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo,
                  dbo.bodega_ubicacion.alto, dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia,
                  dbo.stock.cantidad, dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion,
                  dbo.bodega_ubicacion.orientacion_pos, dbo.motivo_devolucion.Nombre, dbo.trans_oc_pol.codigo_poliza, dbo.bodega_ubicacion.IdBodega, dbo.trans_oc_pol.numero_orden, dbo.producto_familia.nombre, dbo.trans_oc_pol.NoPoliza, dbo.trans_oc_enc.Referencia,
                  dbo.bodega_ubicacion.IdArea, dbo.producto_clasificacion.nombre, dbo.producto_parametro_a.IdProductoParametroA, dbo.producto_parametro_b.IdProductoParametroB, dbo.producto_parametro_a.Nombre, dbo.producto_parametro_b.Nombre, dbo.stock.IdBodega,
                  p_parametro.descripcion,p_parametro.tipo,stock_parametro.valor_numerico,stock_parametro.valor_texto,stock_parametro.valor_fecha,stock_parametro.valor_logico,
				  dbo.producto_tipo.IdTipoProducto,dbo.producto_marca.IdMarca,
				  dbo.producto_marca.nombre,dbo.producto_tipo.NombreTipoProducto,

				  dbo.trans_oc_enc.IdOrdenCompraEnc,dbo.stock_det.posiciones,
				  dbo.trans_oc_det.costo,
				  dbo.trans_oc_det.cantidad_recibida,
				  dbo.trans_oc_det.IdEmbarcador,dbo.trans_oc_embarcador.Nombre
```
