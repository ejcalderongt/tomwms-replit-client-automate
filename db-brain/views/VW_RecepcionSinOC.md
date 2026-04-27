---
id: db-brain-view-vw-recepcionsinoc
type: db-view
title: dbo.VW_RecepcionSinOC
schema: dbo
name: VW_RecepcionSinOC
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RecepcionSinOC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` | ✓ |  |
| 2 | `IdRecepcionDet` | `int` | ✓ |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 5 | `fecha_recepcion` | `datetime` | ✓ |  |
| 6 | `hora_ini_pc` | `datetime` | ✓ |  |
| 7 | `hora_fin_pc` | `datetime` | ✓ |  |
| 8 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 11 | `Producto` | `nvarchar(50)` | ✓ |  |
| 12 | `fecha_ingreso` | `datetime` | ✓ |  |
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha_vence` | `datetime` | ✓ |  |
| 15 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 16 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 17 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 18 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 19 | `No_Documento` | `nvarchar(50)` | ✓ |  |
| 20 | `Id_Proveedor` | `int` | ✓ |  |
| 21 | `Proveedor` | `nvarchar(50)` | ✓ |  |
| 22 | `No_Linea` | `int` | ✓ |  |
| 23 | `CantidadRecibida` | `float` | ✓ |  |
| 24 | `IdRecepcionOc` | `int` | ✓ |  |
| 25 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 26 | `peso` | `float` | ✓ |  |

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
CREATE VIEW dbo.VW_RecepcionSinOC
AS
SELECT     TOP (100) PERCENT dbo.trans_re_enc.IdRecepcionEnc, dbo.trans_re_det.IdRecepcionDet, dbo.trans_re_enc.IdPropietarioBodega, 
                      dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, dbo.trans_re_enc.hora_fin_pc, 
                      dbo.trans_re_tr.TipoTrans, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.producto.nombre AS Producto, dbo.trans_re_det.fecha_ingreso, dbo.trans_re_det.lote, 
                      dbo.trans_re_det.fecha_vence, dbo.producto_estado.nombre AS EstadoProducto, dbo.producto_presentacion.nombre AS Presentacion, 
                      dbo.trans_re_enc.estado AS EstadoRec, dbo.unidad_medida.Nombre AS Unidad_Medida, dbo.trans_re_oc.no_docto AS No_Documento, 
                      dbo.proveedor_bodega.IdProveedor AS Id_Proveedor, dbo.proveedor.nombre AS Proveedor, dbo.trans_re_det.No_Linea, 
                      dbo.trans_re_det.cantidad_recibida AS CantidadRecibida, dbo.trans_re_oc.IdRecepcionOc, dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_det.peso
FROM         dbo.trans_re_oc INNER JOIN
                      dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc INNER JOIN
                      dbo.propietario_bodega ON dbo.trans_oc_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                      dbo.proveedor_bodega ON dbo.trans_oc_enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                      dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor RIGHT OUTER JOIN
                      dbo.unidad_medida INNER JOIN
                      dbo.trans_re_enc INNER JOIN
                      dbo.trans_re_det ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_det.IdRecepcionEnc ON 
                      dbo.unidad_medida.IdUnidadMedida = dbo.trans_re_det.IdUnidadMedida INNER JOIN
                      dbo.producto INNER JOIN
                      dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                      dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario ON dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica AND 
                      dbo.unidad_medida.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto_bodega.IdProductoBodega = dbo.trans_re_det.IdProductoBodega INNER JOIN
                      dbo.trans_re_tr ON dbo.trans_re_tr.IdTipoTransaccion = dbo.trans_re_enc.IdTipoTransaccion ON dbo.proveedor.IdPropietario = dbo.propietarios.IdPropietario AND 
                      dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega AND 
                      dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND 
                      dbo.trans_oc_enc.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega RIGHT OUTER JOIN
                      dbo.producto_presentacion ON dbo.trans_re_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND 
                      dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto RIGHT OUTER JOIN
                      dbo.producto_estado ON dbo.trans_re_det.IdProductoEstado = dbo.producto_estado.IdEstado AND 
                      dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario AND dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario
GROUP BY dbo.trans_re_enc.IdRecepcionEnc, dbo.trans_re_det.IdRecepcionDet, dbo.trans_re_enc.IdPropietarioBodega, dbo.propietarios.nombre_comercial, 
                      dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_tr.TipoTrans, dbo.producto.codigo, 
                      dbo.producto.codigo_barra, dbo.producto.nombre, dbo.trans_re_det.fecha_ingreso, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, 
                      dbo.producto_estado.nombre, dbo.producto_presentacion.nombre, dbo.trans_re_enc.estado, dbo.unidad_medida.Nombre, dbo.trans_re_oc.no_docto, 
                      dbo.proveedor_bodega.IdProveedor, dbo.proveedor.nombre, dbo.trans_re_det.No_Linea, dbo.trans_re_det.cantidad_recibida, dbo.trans_re_oc.IdRecepcionOc, 
                      dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_det.peso
ORDER BY dbo.trans_re_enc.IdRecepcionEnc
```
