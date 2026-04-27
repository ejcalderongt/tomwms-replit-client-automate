---
id: db-brain-view-vw-ingreso-fiscal
type: db-view
title: dbo.VW_Ingreso_Fiscal
schema: dbo
name: VW_Ingreso_Fiscal
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ingreso_Fiscal`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 41 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `IdOrdenCompraEnc` | `int` |  |  |
| 4 | `IdOrdenCompraDet` | `int` | ✓ |  |
| 5 | `IdOrdenCompraPol` | `int` | ✓ |  |
| 6 | `No_Linea` | `int` | ✓ |  |
| 7 | `IdProveedorBodega` | `int` |  |  |
| 8 | `proveedor` | `nvarchar(100)` | ✓ |  |
| 9 | `IdTipoIngresoOC` | `int` | ✓ |  |
| 10 | `tipo_ingreso` | `nvarchar(50)` | ✓ |  |
| 11 | `IdPedidoEncDevolucion` | `int` | ✓ |  |
| 12 | `No_Documento_Devolucion` | `nvarchar(50)` | ✓ |  |
| 13 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 14 | `IdPresentacion` | `int` | ✓ |  |
| 15 | `IdProductoBodega` | `int` | ✓ |  |
| 16 | `codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 18 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 19 | `estado` | `nvarchar(50)` | ✓ |  |
| 20 | `cantidad` | `float` | ✓ |  |
| 21 | `recibido` | `float` | ✓ |  |
| 22 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 23 | `nombre_unidad_medida_basica` | `nvarchar(50)` | ✓ |  |
| 24 | `peso` | `float` | ✓ |  |
| 25 | `clasificacion` | `nvarchar(50)` | ✓ |  |
| 26 | `Fecha_Ingreso` | `datetime` | ✓ |  |
| 27 | `fecha_llegada_poliza` | `datetime` | ✓ |  |
| 28 | `observacion_ingreso` | `text` | ✓ |  |
| 29 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 30 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 31 | `codigo_poliza` | `nvarchar(150)` | ✓ |  |
| 32 | `IdRegimen` | `int` | ✓ |  |
| 33 | `codigo_regimen` | `nvarchar(20)` | ✓ |  |
| 34 | `placa` | `nvarchar(20)` | ✓ |  |
| 35 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 36 | `cbm` | `float` | ✓ |  |
| 37 | `observacion` | `nvarchar(100)` | ✓ |  |
| 38 | `carta_cupo` | `nvarchar(50)` | ✓ |  |
| 39 | `activo` | `bit` | ✓ |  |
| 40 | `dua` | `nvarchar(50)` | ✓ |  |
| 41 | `contenedor` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `empresa_transporte_vehiculos`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `proveedor`
- `proveedor_bodega`
- `regimen_fiscal`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_estado`
- `trans_oc_pol`
- `trans_oc_ti`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ingreso_Fiscal]
AS
SELECT bd.IdBodega, bd.nombre, oc_enc.IdOrdenCompraEnc, oc_det.IdOrdenCompraDet, oc_pol.IdOrdenCompraPol, oc_det.No_Linea, oc_enc.IdProveedorBodega, pv.nombre AS proveedor, oc_enc.IdTipoIngresoOC, 
                  oc_ti.Nombre AS tipo_ingreso, oc_enc.IdPedidoEncDevolucion, oc_enc.No_Documento_Devolucion, oc_enc.IdMotivoDevolucion, oc_det.IdPresentacion, oc_det.IdProductoBodega, pr.codigo, pr.codigo_barra, oc_det.nombre_producto, 
                  oc_estado.Nombre AS estado, oc_det.cantidad, oc_det.cantidad_recibida AS recibido, oc_det.IdUnidadMedidaBasica, oc_det.nombre_unidad_medida_basica, oc_det.peso, pc.nombre AS clasificacion, 
                  oc_enc.Fecha_Creacion AS Fecha_Ingreso, oc_pol.fecha_llegada AS fecha_llegada_poliza, oc_enc.Observacion AS observacion_ingreso, oc_enc.Referencia, oc_pol.numero_orden, oc_pol.codigo_poliza, oc_pol.IdRegimen, 
                  r_fiscal.codigo_regimen, vehiculo.placa, re_enc.No_Marchamo, oc_pol.cbm, re_enc.observacion, re_enc.carta_cupo, oc_pol.activo, oc_pol.dua, re_enc.no_contenedor AS contenedor
FROM     dbo.trans_oc_enc AS oc_enc LEFT OUTER JOIN
                  dbo.trans_oc_det AS oc_det ON oc_enc.IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.producto_bodega AS pb ON oc_det.IdProductoBodega = pb.IdProductoBodega LEFT OUTER JOIN
                  dbo.producto AS pr ON pb.IdProducto = pr.IdProducto INNER JOIN
                  dbo.bodega AS bd ON oc_enc.IDBODEGA = bd.IdBodega LEFT OUTER JOIN
                  dbo.producto_clasificacion AS pc ON pr.IdClasificacion = pc.IdClasificacion LEFT OUTER JOIN
                  dbo.trans_oc_pol AS oc_pol ON oc_enc.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.trans_re_oc AS re_oc ON oc_enc.IdOrdenCompraEnc = re_oc.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.trans_re_enc AS re_enc ON re_oc.IdRecepcionEnc = re_enc.IdRecepcionEnc INNER JOIN
                  dbo.proveedor_bodega AS p_bodega ON oc_enc.IdProveedorBodega = p_bodega.IdAsignacion INNER JOIN
                  dbo.proveedor AS pv ON p_bodega.IdProveedor = pv.IdProveedor LEFT OUTER JOIN
                  dbo.empresa_transporte_vehiculos AS vehiculo ON re_enc.idvehiculo = vehiculo.IdVehiculo INNER JOIN
                  dbo.trans_oc_ti AS oc_ti ON oc_enc.IdTipoIngresoOC = oc_ti.IdTipoIngresoOC LEFT OUTER JOIN
                  dbo.regimen_fiscal AS r_fiscal ON oc_pol.IdRegimen = r_fiscal.IdRegimen INNER JOIN
                  dbo.trans_oc_estado AS oc_estado ON oc_enc.IdEstadoOC = oc_estado.IdEstadoOC
```
