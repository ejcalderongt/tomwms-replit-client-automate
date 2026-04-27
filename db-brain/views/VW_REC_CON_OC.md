---
id: db-brain-view-vw-rec-con-oc
type: db-view
title: dbo.VW_REC_CON_OC
schema: dbo
name: VW_REC_CON_OC
kind: view
modify_date: 2022-09-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_REC_CON_OC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-09-07 |
| Columnas | 36 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` |  |  |
| 2 | `IdRecepcionDet` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `Propietario` | `nvarchar(100)` |  |  |
| 5 | `fecha_recepcion` | `datetime` | ✓ |  |
| 6 | `hora_ini_pc` | `datetime` | ✓ |  |
| 7 | `hora_fin_pc` | `datetime` | ✓ |  |
| 8 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 9 | `No_Linea` | `int` | ✓ |  |
| 10 | `codigo` | `nvarchar(50)` | ✓ |  |
| 11 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 12 | `Producto` | `nvarchar(100)` | ✓ |  |
| 13 | `cantidad` | `float` | ✓ |  |
| 14 | `CantidadRecibida` | `float` | ✓ |  |
| 15 | `fecha_ingreso` | `datetime` | ✓ |  |
| 16 | `lote` | `nvarchar(50)` | ✓ |  |
| 17 | `fecha_vence` | `datetime` | ✓ |  |
| 18 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 19 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 20 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 21 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 22 | `IdOrdenCompraEnc` | `int` |  |  |
| 23 | `IdRecepcionOc` | `int` |  |  |
| 24 | `no_docto` | `nvarchar(100)` | ✓ |  |
| 25 | `Id_Proveedor` | `int` | ✓ |  |
| 26 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 27 | `IdProductoBodega` | `int` |  |  |
| 28 | `IdProveedorBodega` | `int` |  |  |
| 29 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 30 | `NombrePiloto` | `nvarchar(301)` | ✓ |  |
| 31 | `placa` | `nvarchar(20)` | ✓ |  |
| 32 | `marca` | `nvarchar(50)` | ✓ |  |
| 33 | `firma_piloto` | `image` | ✓ |  |
| 34 | `Operador` | `nvarchar(201)` | ✓ |  |
| 35 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 36 | `IdTipoIngresoOC` | `int` | ✓ |  |

## Consume

- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_op`
- `trans_re_tr`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_REC_CON_OC]
AS
SELECT dbo.trans_re_enc.IdRecepcionEnc, dbo.trans_re_det.IdRecepcionDet, dbo.trans_re_enc.IdPropietarioBodega, 
dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, dbo.trans_re_enc.hora_fin_pc, 
dbo.trans_re_tr.TipoTrans, dbo.trans_re_det.No_Linea, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre AS Producto,
(SELECT TOP(1) ISNULL(cantidad, 0) AS Cantidad
FROM dbo.trans_oc_det
WHERE (IdProductoBodega = dbo.trans_re_det.IdProductoBodega) AND (IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc AND IdOrdenCompraDet = trans_oc_det.IdOrdenCompraDet) 
AND (No_Linea = dbo.trans_re_det.No_Linea)) AS cantidad, 
dbo.trans_re_det.cantidad_recibida AS CantidadRecibida, dbo.trans_re_det.fecha_ingreso, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, 
dbo.producto_estado.nombre AS EstadoProducto, dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_re_enc.estado AS EstadoRec, 
dbo.unidad_medida.Nombre AS Unidad_Medida, dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_oc.IdRecepcionOc, dbo.trans_re_oc.no_docto, 
dbo.proveedor_bodega.IdProveedor AS Id_Proveedor, dbo.proveedor.nombre AS Proveedor, dbo.producto_bodega.IdProductoBodega, 
dbo.trans_oc_enc.IdProveedorBodega, dbo.trans_oc_enc.Referencia, 
dbo.empresa_transporte_pilotos.nombres + N' ' + dbo.empresa_transporte_pilotos.apellidos AS NombrePiloto, dbo.empresa_transporte_vehiculos.placa, 
dbo.empresa_transporte_vehiculos.marca, dbo.trans_re_enc.firma_piloto,
(SELECT TOP (1) dbo.operador.nombres + N' ' + dbo.operador.apellidos AS Operador
FROM dbo.operador INNER JOIN
dbo.operador_bodega ON dbo.operador.IdOperador = dbo.operador_bodega.IdOperador INNER JOIN
dbo.trans_re_op ON dbo.operador_bodega.IdOperadorBodega = dbo.trans_re_op.IdOperadorBodega
WHERE (dbo.trans_re_op.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc)) AS Operador, dbo.trans_re_enc.No_Marchamo, trans_oc_enc.IdTipoIngresoOC
FROM dbo.empresa_transporte_pilotos RIGHT OUTER JOIN
dbo.trans_re_oc INNER JOIN
dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc INNER JOIN
dbo.propietario_bodega ON dbo.trans_oc_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
dbo.proveedor_bodega ON dbo.trans_oc_enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor INNER JOIN
dbo.trans_re_enc INNER JOIN
dbo.trans_re_det ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_det.IdRecepcionEnc INNER JOIN
dbo.producto_bodega ON dbo.trans_re_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
dbo.unidad_medida ON dbo.trans_re_det.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND 
dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida AND dbo.propietarios.IdPropietario = dbo.unidad_medida.IdPropietario INNER JOIN
dbo.trans_re_tr ON dbo.trans_re_enc.IdTipoTransaccion = dbo.trans_re_tr.IdTipoTransaccion ON dbo.propietarios.IdPropietario = dbo.proveedor.IdPropietario AND 
dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario AND dbo.trans_re_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND
dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc AND dbo.trans_re_enc.IdPropietarioBodega = dbo.trans_oc_enc.IdPropietarioBodega ON 
dbo.empresa_transporte_pilotos.IdPiloto = dbo.trans_re_enc.idpiloto LEFT OUTER JOIN
dbo.empresa_transporte_vehiculos ON dbo.trans_re_enc.idvehiculo = dbo.empresa_transporte_vehiculos.IdVehiculo LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.trans_re_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND 
dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto LEFT OUTER JOIN
dbo.producto_estado ON dbo.trans_re_det.IdProductoEstado = dbo.producto_estado.IdEstado AND 
dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario AND dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario
```
