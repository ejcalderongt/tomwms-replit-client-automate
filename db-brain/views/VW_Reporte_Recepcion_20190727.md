---
id: db-brain-view-vw-reporte-recepcion-20190727
type: db-view
title: dbo.VW_Reporte_Recepcion_20190727
schema: dbo
name: VW_Reporte_Recepcion_20190727
kind: view
modify_date: 2019-07-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Reporte_Recepcion_20190727`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-07-29 |
| Columnas | 22 |

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
| 13 | `CantidadRecibida` | `float` | ✓ |  |
| 14 | `fecha_ingreso` | `datetime` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `fecha_vence` | `datetime` | ✓ |  |
| 17 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 18 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 19 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 20 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 21 | `peso` | `float` | ✓ |  |
| 22 | `Atributo_Variante` | `nvarchar(25)` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Reporte_Recepcion_20190727]
AS
SELECT        TOP (100) PERCENT dbo.trans_re_enc.IdRecepcionEnc, dbo.trans_re_det.IdRecepcionDet, dbo.trans_re_enc.IdPropietarioBodega, dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_re_enc.fecha_recepcion, 
                         dbo.trans_re_enc.hora_ini_pc, dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_tr.TipoTrans, dbo.trans_re_det.No_Linea, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre AS Producto, 
                         dbo.trans_re_det.cantidad_recibida AS CantidadRecibida, dbo.trans_re_det.fecha_ingreso, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.producto_estado.nombre AS EstadoProducto, 
                         dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_re_enc.estado AS EstadoRec, dbo.unidad_medida.Nombre AS Unidad_Medida, dbo.trans_re_det.peso, 
                         dbo.trans_re_det.atributo_variante_1 AS Atributo_Variante
FROM            dbo.trans_re_oc INNER JOIN
                         dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc INNER JOIN
                         dbo.propietario_bodega ON dbo.trans_oc_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.proveedor_bodega ON dbo.trans_oc_enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                         dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor RIGHT OUTER JOIN
                         dbo.trans_re_enc INNER JOIN
                         dbo.trans_re_det ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_det.IdRecepcionEnc INNER JOIN
                         dbo.producto_bodega ON dbo.trans_re_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                         dbo.unidad_medida ON dbo.trans_re_det.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida AND 
                         dbo.propietarios.IdPropietario = dbo.unidad_medida.IdPropietario INNER JOIN
                         dbo.trans_re_tr ON dbo.trans_re_enc.IdTipoTransaccion = dbo.trans_re_tr.IdTipoTransaccion ON dbo.proveedor.IdPropietario = dbo.propietarios.IdPropietario AND 
                         dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega AND 
                         dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND dbo.trans_oc_enc.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_re_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto LEFT OUTER JOIN
                         dbo.producto_estado ON dbo.trans_re_det.IdProductoEstado = dbo.producto_estado.IdEstado AND dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario AND 
                         dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario
GROUP BY dbo.trans_re_enc.IdRecepcionEnc, dbo.trans_re_det.IdRecepcionDet, dbo.trans_re_enc.IdPropietarioBodega, dbo.propietarios.nombre_comercial, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, 
                         dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_tr.TipoTrans, dbo.trans_re_det.No_Linea, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre, dbo.trans_re_det.cantidad_recibida, 
                         dbo.trans_re_det.fecha_ingreso, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.producto_estado.nombre, dbo.producto_presentacion.nombre, dbo.trans_re_enc.estado, dbo.unidad_medida.Nombre, 
                         dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_oc.IdRecepcionOc, dbo.trans_re_oc.no_docto, dbo.proveedor_bodega.IdProveedor, dbo.proveedor.nombre, dbo.trans_re_det.peso, 
                         dbo.trans_re_det.atributo_variante_1
ORDER BY dbo.trans_re_enc.IdRecepcionEnc
```
