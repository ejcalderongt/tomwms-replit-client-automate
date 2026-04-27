---
id: db-brain-view-vw-recepcionconoc
type: db-view
title: dbo.VW_RecepcionConOC
schema: dbo
name: VW_RecepcionConOC
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RecepcionConOC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `fecha_recepcion` | `datetime` | ✓ |  |
| 2 | `hora_ini_pc` | `datetime` | ✓ |  |
| 3 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 4 | `IdOrdenCompraEnc` | `int` |  |  |
| 5 | `hora_fin_pc` | `datetime` | ✓ |  |
| 6 | `No_Documento` | `nvarchar(50)` | ✓ |  |
| 7 | `IdRecepcionEnc` | `int` |  |  |
| 8 | `No_Linea` | `int` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `Producto` | `nvarchar(50)` | ✓ |  |
| 11 | `cantidad` | `float` | ✓ |  |
| 12 | `CantidadRecibida` | `float` | ✓ |  |
| 13 | `fecha_ingreso` | `datetime` | ✓ |  |
| 14 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 17 | `Proveedor` | `nvarchar(50)` | ✓ |  |
| 18 | `IdProveedorBodega` | `int` |  |  |
| 19 | `Id_Proveedor` | `int` |  |  |
| 20 | `IdEstadoOC` | `int` | ✓ |  |
| 21 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 22 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 23 | `fecha_vence` | `datetime` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
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
CREATE VIEW dbo.VW_RecepcionConOC
AS
SELECT     enc.fecha_recepcion, enc.hora_ini_pc, tr.TipoTrans, oc.IdOrdenCompraEnc, enc.hora_fin_pc, reoc.no_docto AS No_Documento, enc.IdRecepcionEnc, det.No_Linea, 
                      pr.codigo, pr.nombre AS Producto, det.cantidad, detr.cantidad_recibida AS CantidadRecibida, detr.fecha_ingreso, pe.nombre AS EstadoProducto, detr.lote, 
                      pp.nombre AS Presentacion, p.nombre AS Proveedor, oc.IdProveedorBodega, p.IdProveedor AS Id_Proveedor, oc.IdEstadoOC, enc.estado AS EstadoRec, 
                      dbo.unidad_medida.Nombre AS Unidad_Medida, detr.fecha_vence
FROM         dbo.proveedor_bodega INNER JOIN
                      dbo.trans_re_enc AS enc INNER JOIN
                      dbo.trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion INNER JOIN
                      dbo.trans_re_oc AS reoc ON enc.IdRecepcionEnc = reoc.IdRecepcionEnc INNER JOIN
                      dbo.trans_oc_enc AS oc ON reoc.IdOrdenCompraEnc = oc.IdOrdenCompraEnc INNER JOIN
                      dbo.trans_re_det AS detr ON enc.IdRecepcionEnc = detr.IdRecepcionEnc ON dbo.proveedor_bodega.IdAsignacion = oc.IdProveedorBodega INNER JOIN
                      dbo.proveedor AS p ON dbo.proveedor_bodega.IdProveedor = p.IdProveedor INNER JOIN
                      dbo.unidad_medida ON detr.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                      dbo.producto_bodega AS prb INNER JOIN
                      dbo.trans_oc_det AS det ON prb.IdProductoBodega = det.IdProductoBodega INNER JOIN
                      dbo.producto AS pr ON prb.IdProducto = pr.IdProducto ON dbo.unidad_medida.IdUnidadMedida = det.IdUnidadMedidaBasica AND 
                      dbo.unidad_medida.IdUnidadMedida = pr.IdUnidadMedidaBasica AND oc.IdOrdenCompraEnc = det.IdOrdenCompraEnc AND 
                      detr.IdProductoBodega = det.IdProductoBodega LEFT OUTER JOIN
                      dbo.producto_estado AS pe ON detr.IdProductoEstado = pe.IdEstado LEFT OUTER JOIN
                      dbo.producto_presentacion AS pp ON detr.IdPresentacion = pp.IdPresentacion
```
