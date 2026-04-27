---
id: db-brain-view-vw-rec-conoc-fin
type: db-view
title: dbo.VW_REC_CONOC_FIN
schema: dbo
name: VW_REC_CONOC_FIN
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_REC_CONOC_FIN`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `IdProductoBodega` | `int` |  |  |
| 3 | `codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Producto` | `nvarchar(50)` | ✓ |  |
| 5 | `cantidad` | `float` | ✓ |  |
| 6 | `CantidadRecibida` | `float` | ✓ |  |
| 7 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 8 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 9 | `IdRecepcionEnc` | `int` | ✓ |  |
| 10 | `fecha_recepcion` | `datetime` | ✓ |  |
| 11 | `hora_ini_pc` | `datetime` | ✓ |  |
| 12 | `hora_fin_pc` | `datetime` | ✓ |  |
| 13 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` | ✓ |  |
| 15 | `fecha_vence` | `datetime` | ✓ |  |
| 16 | `peso` | `float` | ✓ |  |
| 17 | `Proveedor` | `nvarchar(50)` | ✓ |  |
| 18 | `IdProveedorBodega` | `int` |  |  |
| 19 | `Id_Proveedor` | `int` |  |  |
| 20 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 21 | `No_Documento` | `nvarchar(50)` | ✓ |  |
| 22 | `No_Linea` | `int` | ✓ |  |
| 23 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |

## Consume

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
- `trans_re_tr`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_REC_CONOC_FIN
AS
SELECT     TOP (100) PERCENT dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_oc_det.IdProductoBodega, dbo.producto.codigo, dbo.producto.nombre AS Producto, 
                      dbo.trans_oc_det.cantidad, SUM(dbo.trans_re_det.cantidad_recibida) AS CantidadRecibida, dbo.unidad_medida.Nombre AS Unidad_Medida, 
                      dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_re_oc.IdRecepcionEnc, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, 
                      dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_enc.estado AS EstadoRec, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.trans_re_det.peso, 
                      dbo.proveedor.nombre AS Proveedor, dbo.trans_oc_enc.IdProveedorBodega, dbo.proveedor.IdProveedor AS Id_Proveedor, dbo.trans_re_tr.TipoTrans, 
                      dbo.trans_re_oc.no_docto AS No_Documento, dbo.trans_re_det.No_Linea, dbo.producto_estado.nombre AS EstadoProducto
FROM         dbo.producto_presentacion RIGHT OUTER JOIN
                      dbo.trans_re_tr INNER JOIN
                      dbo.trans_re_det INNER JOIN
                      dbo.trans_re_oc RIGHT OUTER JOIN
                      dbo.trans_re_enc ON dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc ON 
                      dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc ON 
                      dbo.trans_re_tr.IdTipoTransaccion = dbo.trans_re_enc.IdTipoTransaccion INNER JOIN
                      dbo.producto_estado ON dbo.trans_re_det.IdProductoEstado = dbo.producto_estado.IdEstado RIGHT OUTER JOIN
                      dbo.producto INNER JOIN
                      dbo.unidad_medida ON dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                      dbo.proveedor_bodega INNER JOIN
                      dbo.trans_oc_enc INNER JOIN
                      dbo.trans_oc_det ON dbo.trans_oc_enc.IdOrdenCompraEnc = dbo.trans_oc_det.IdOrdenCompraEnc INNER JOIN
                      dbo.propietario_bodega ON dbo.trans_oc_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                      dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                      dbo.producto_bodega ON dbo.trans_oc_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                      dbo.proveedor ON dbo.propietarios.IdPropietario = dbo.proveedor.IdPropietario ON dbo.proveedor_bodega.IdAsignacion = dbo.trans_oc_enc.IdProveedorBodega AND 
                      dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario AND 
                      dbo.producto.IdProducto = dbo.producto_bodega.IdProducto AND dbo.unidad_medida.IdUnidadMedida = dbo.trans_oc_det.IdUnidadMedidaBasica AND 
                      dbo.unidad_medida.IdPropietario = dbo.propietarios.IdPropietario ON dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND 
                      dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON 
                      dbo.producto_presentacion.IdPresentacion = dbo.trans_oc_det.IdPresentacion AND dbo.producto_presentacion.IdProducto = dbo.producto.IdProducto LEFT OUTER JOIN
                      dbo.trans_oc_det AS OcDet ON dbo.trans_re_det.IdProductoBodega = OcDet.IdProductoBodega AND dbo.trans_re_det.IdPresentacion = OcDet.IdPresentacion AND 
                      dbo.trans_re_det.IdUnidadMedida = OcDet.IdUnidadMedidaBasica
GROUP BY dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_oc_det.IdProductoBodega, dbo.producto.codigo, dbo.producto.nombre, dbo.trans_oc_det.cantidad, 
                      dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.trans_re_oc.IdRecepcionEnc, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, 
                      dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_enc.estado, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.trans_re_det.peso, 
                      dbo.trans_re_det.nombre_producto_estado, dbo.proveedor.nombre, dbo.trans_oc_enc.IdProveedorBodega, dbo.proveedor.IdProveedor, dbo.trans_re_tr.TipoTrans, 
                      dbo.trans_re_oc.no_docto, dbo.trans_re_det.No_Linea, dbo.producto_estado.nombre
ORDER BY dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_oc.IdRecepcionEnc
```
