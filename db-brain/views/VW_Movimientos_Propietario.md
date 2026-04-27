---
id: db-brain-view-vw-movimientos-propietario
type: db-view
title: dbo.VW_Movimientos_Propietario
schema: dbo
name: VW_Movimientos_Propietario
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_Propietario`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 44 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransaccion` | `int` |  |  |
| 2 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 3 | `IdPedidoEnc` | `int` | ✓ |  |
| 4 | `IdDespachoEnc` | `int` | ✓ |  |
| 5 | `IdDespachoDet` | `int` | ✓ |  |
| 6 | `observacion` | `nvarchar(500)` | ✓ |  |
| 7 | `no_ticket_tms` | `nvarchar(50)` |  |  |
| 8 | `poliza` | `nvarchar(150)` | ✓ |  |
| 9 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 10 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 11 | `IdRecepcion` | `int` | ✓ |  |
| 12 | `valor_aduana` | `float` | ✓ |  |
| 13 | `valor_dai` | `float` | ✓ |  |
| 14 | `valor_iva` | `float` | ✓ |  |
| 15 | `Propietario` | `nvarchar(100)` |  |  |
| 16 | `Producto` | `nvarchar(100)` | ✓ |  |
| 17 | `presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 19 | `cantidad` | `float` | ✓ |  |
| 20 | `fecha` | `datetime` | ✓ |  |
| 21 | `IdProducto` | `int` |  |  |
| 22 | `codigo` | `nvarchar(50)` | ✓ |  |
| 23 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 24 | `IdTipoTarea` | `int` |  |  |
| 25 | `Contabilizar` | `bit` | ✓ |  |
| 26 | `fecha_vence` | `datetime` | ✓ |  |
| 27 | `IdPresentacion` | `int` | ✓ |  |
| 28 | `IdUnidadMedida` | `int` | ✓ |  |
| 29 | `IdPropietarioBodega` | `int` | ✓ |  |
| 30 | `IdBodega` | `int` | ✓ |  |
| 31 | `licencia` | `nvarchar(50)` | ✓ |  |
| 32 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 33 | `Familia` | `nvarchar(50)` | ✓ |  |
| 34 | `IdMovimiento` | `int` |  |  |
| 35 | `Codigo_Bodega_Origen` | `nvarchar(50)` | ✓ |  |
| 36 | `Nombre_Bodega_Origen` | `nvarchar(50)` | ✓ |  |
| 37 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 38 | `factor` | `float` | ✓ |  |
| 39 | `fecha_ingreso_rec` | `date` | ✓ |  |
| 40 | `fecha_ingreso_ticket` | `date` | ✓ |  |
| 41 | `numero_orden_salida` | `nvarchar(50)` | ✓ |  |
| 42 | `codigo_poliza_salida` | `nvarchar(50)` | ✓ |  |
| 43 | `regimen_ingreso` | `nvarchar(20)` |  |  |
| 44 | `regimen_salida` | `nvarchar(20)` | ✓ |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `Nombre_Area`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `regimen_fiscal`
- `sis_tipo_tarea`
- `tms_ticket`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_movimientos`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_pe_det`
- `trans_pe_pol`
- `trans_re_det`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Movimientos_Propietario] as
SELECT m.IdTransaccion, stt.Nombre AS TipoTarea, pe_det.IdPedidoEnc, despacho_det.IdDespachoEnc, despacho_det.IdDespachoDet, despacho_enc.observacion, 
			      ISNULL(enc.ticket, 0) AS no_ticket_tms, enc.codigo_poliza AS poliza, 
                  enc.numero_orden, oc_enc.Referencia, m.IdRecepcion, CASE WHEN oc_det.valor_aduana > 0 THEN (oc_det.valor_aduana / oc_det.cantidad) * m.cantidad ELSE 0 END AS valor_aduana, 
                  CASE WHEN oc_det.valor_dai > 0 THEN (oc_det.valor_dai / oc_det.cantidad) * m.cantidad ELSE 0 END AS valor_dai, CASE WHEN oc_det.valor_iva > 0 THEN (oc_det.valor_iva / oc_det.cantidad) * m.cantidad ELSE 0 END AS valor_iva, 
                  pr.nombre_comercial AS Propietario, p.nombre AS Producto, pp.nombre AS presentacion, u.Nombre AS UMBas, m.cantidad, m.fecha, p.IdProducto, p.codigo, p.codigo_barra, stt.IdTipoTarea, stt.Contabilizar, m.fecha_vence, 
                  m.IdPresentacion, m.IdUnidadMedida, prb.IdPropietarioBodega, prb.IdBodega, m.barra_pallet AS licencia, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_familia.nombre AS Familia, m.IdMovimiento, 
                  dbo.bodega.codigo AS Codigo_Bodega_Origen, dbo.bodega.nombre AS Nombre_Bodega_Origen, dbo.Nombre_Area(u1.IdArea, m.IdBodegaOrigen) AS NombreArea, pp.factor, CAST(re_det.fecha_ingreso AS date) AS fecha_ingreso_rec, 
                  CAST(tms.Fecha_Ingreso AS date) AS fecha_ingreso_ticket, pe_pol.numero_orden AS numero_orden_salida, 
				  pe_pol.codigo_poliza AS codigo_poliza_salida, dbo.regimen_fiscal.codigo_regimen AS regimen_ingreso,
				  regimen_fiscal_salida.codigo_regimen as regimen_salida
FROM     dbo.trans_movimientos AS m LEFT OUTER JOIN
                  dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                  dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                  dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                  dbo.producto_clasificacion ON p.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                  dbo.producto_familia ON p.IdFamilia = dbo.producto_familia.IdFamilia LEFT OUTER JOIN
                  dbo.bodega ON m.IdBodegaOrigen = dbo.bodega.IdBodega AND m.IdEmpresa = dbo.bodega.IdEmpresa LEFT OUTER JOIN
                  dbo.bodega AS bodega_1 ON m.IdEmpresa = bodega_1.IdEmpresa AND m.IdBodegaDestino = bodega_1.IdBodega LEFT OUTER JOIN
                  dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND u2.IdBodega = m.IdBodegaDestino LEFT OUTER JOIN
                  dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND u1.IdBodega = m.IdBodegaOrigen LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion LEFT OUTER JOIN
                  dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado LEFT OUTER JOIN
                  dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado LEFT OUTER JOIN
                  dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida INNER JOIN
                  dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea INNER JOIN
                  dbo.trans_re_oc AS re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.trans_pe_det AS pe_det ON m.IdPedidoEnc = pe_det.IdPedidoEnc AND m.IdPedidoDet = pe_det.IdPedidoDet LEFT OUTER JOIN
                  dbo.trans_despacho_det AS despacho_det ON m.IdDespachoEnc = despacho_det.IdDespachoEnc AND m.IdDespachoDet = despacho_det.IdDespachoDet LEFT OUTER JOIN
                  dbo.trans_oc_pol AS enc ON re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc INNER JOIN
                  dbo.trans_re_det AS re_det ON m.IdRecepcion = re_det.IdRecepcionEnc AND re_det.lic_plate = m.barra_pallet COLLATE Modern_Spanish_CI_AS LEFT OUTER JOIN
                  dbo.trans_pe_pol AS pe_pol ON m.IdPedidoEnc = pe_pol.IdOrdenPedidoEnc INNER JOIN
                  dbo.trans_oc_det AS oc_det ON re_det.IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc AND re_det.IdOrdenCompraDet = oc_det.IdOrdenCompraDet INNER JOIN
                  dbo.trans_oc_enc AS oc_enc ON oc_det.IdOrdenCompraEnc = oc_enc.IdOrdenCompraEnc INNER JOIN
                  dbo.regimen_fiscal ON enc.IdRegimen = dbo.regimen_fiscal.IdRegimen LEFT OUTER JOIN
                  dbo.tms_ticket AS tms ON oc_enc.no_ticket_tms = tms.IdTicket LEFT OUTER JOIN
                  dbo.trans_despacho_enc AS despacho_enc ON despacho_det.IdDespachoEnc = despacho_enc.IdDespachoEnc LEFT OUTER JOIN
				  DBO.regimen_fiscal AS regimen_fiscal_salida on pe_pol.IdRegimen=regimen_fiscal_salida.IdRegimen
WHERE  (m.IdTipoTarea = 5) OR (m.IdTipoTarea = 1)
```
