---
id: db-brain-view-vw-reporte-recepcion-20190726
type: db-view
title: dbo.VW_Reporte_Recepcion_20190726
schema: dbo
name: VW_Reporte_Recepcion_20190726
kind: view
modify_date: 2019-07-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Reporte_Recepcion_20190726`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-07-29 |
| Columnas | 24 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `IdRecepcionEnc` | `int` |  |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Producto` | `nvarchar(100)` | ✓ |  |
| 6 | `cantidad` | `float` | ✓ |  |
| 7 | `CantidadRecibida` | `float` | ✓ |  |
| 8 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 9 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 10 | `fecha_recepcion` | `datetime` | ✓ |  |
| 11 | `hora_ini_pc` | `datetime` | ✓ |  |
| 12 | `hora_fin_pc` | `datetime` | ✓ |  |
| 13 | `EstadoRec` | `nvarchar(20)` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` | ✓ |  |
| 15 | `fecha_vence` | `datetime` | ✓ |  |
| 16 | `peso` | `float` | ✓ |  |
| 17 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 18 | `IdProveedorBodega` | `int` | ✓ |  |
| 19 | `Id_Proveedor` | `int` | ✓ |  |
| 20 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 21 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 22 | `No_Linea` | `int` | ✓ |  |
| 23 | `EstadoProducto` | `nvarchar(50)` | ✓ |  |
| 24 | `Atributo_Variante` | `nvarchar(25)` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
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
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Reporte_Recepcion_20190726]
AS
SELECT        dbo.trans_re_oc.IdOrdenCompraEnc, dbo.producto_bodega.IdProductoBodega, dbo.trans_re_enc.IdRecepcionEnc, dbo.producto.codigo, dbo.producto.nombre AS Producto, dbo.trans_oc_det.cantidad, 
                         dbo.trans_re_det.cantidad_recibida AS CantidadRecibida, dbo.unidad_medida.Nombre AS Unidad_Medida, dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_re_enc.fecha_recepcion, dbo.trans_re_enc.hora_ini_pc, 
                         dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_enc.estado AS EstadoRec, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.trans_re_det.peso, dbo.proveedor.nombre AS Proveedor, dbo.trans_oc_enc.IdProveedorBodega, 
                         dbo.proveedor.IdProveedor AS Id_Proveedor, dbo.trans_re_tr.TipoTrans, dbo.trans_oc_enc.No_Documento, dbo.trans_re_det.No_Linea, dbo.producto_estado.nombre AS EstadoProducto, 
                         dbo.trans_re_det.atributo_variante_1 AS Atributo_Variante
FROM            dbo.producto_estado INNER JOIN
                         dbo.unidad_medida INNER JOIN
                         dbo.trans_re_det ON dbo.unidad_medida.IdUnidadMedida = dbo.trans_re_det.IdUnidadMedida INNER JOIN
                         dbo.producto ON dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica ON dbo.producto_estado.IdEstado = dbo.trans_re_det.IdProductoEstado INNER JOIN
                         dbo.producto_bodega INNER JOIN
                         dbo.bodega_muelles INNER JOIN
                         dbo.bodega ON dbo.bodega_muelles.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.proveedor_bodega ON dbo.bodega.IdBodega = dbo.proveedor_bodega.IdBodega INNER JOIN
                         dbo.trans_re_oc INNER JOIN
                         dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON dbo.proveedor_bodega.IdAsignacion = dbo.trans_oc_enc.IdProveedorBodega INNER JOIN
                         dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega ON 
                         dbo.trans_re_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND dbo.producto.IdProducto = dbo.producto_bodega.IdProducto LEFT OUTER JOIN
                         dbo.trans_oc_det ON dbo.trans_re_oc.IdRecepcionOc = dbo.trans_oc_det.IdOrdenCompraEnc LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_re_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto RIGHT OUTER JOIN
                         dbo.usuario INNER JOIN
                         dbo.propietarios INNER JOIN
                         dbo.propietario_bodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario INNER JOIN
                         dbo.trans_re_tr INNER JOIN
                         dbo.trans_re_enc ON dbo.trans_re_tr.IdTipoTransaccion = dbo.trans_re_enc.IdTipoTransaccion ON dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega ON 
                         dbo.usuario.IdUsuario = dbo.trans_re_enc.user_agr ON dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto_estado.IdPropietario = dbo.propietarios.IdPropietario AND 
                         dbo.unidad_medida.IdPropietario = dbo.propietarios.IdPropietario AND dbo.producto.IdPropietario = dbo.propietarios.IdPropietario AND dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND 
                         dbo.proveedor.IdPropietario = dbo.propietarios.IdPropietario AND dbo.bodega_muelles.IdMuelle = dbo.trans_re_enc.IdMuelle AND dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc
GROUP BY dbo.trans_re_enc.IdRecepcionEnc, dbo.propietarios.nombre_comercial, dbo.proveedor.nombre, dbo.trans_re_oc.IdOrdenCompraEnc, dbo.trans_oc_enc.No_Documento, dbo.trans_re_enc.fecha_recepcion, 
                         dbo.trans_re_enc.estado, dbo.trans_re_tr.TipoTrans, dbo.trans_re_tr.Descripcion, dbo.bodega_muelles.nombre, dbo.usuario.nombres, dbo.trans_re_enc.fec_agr, dbo.producto.codigo, dbo.producto.codigo_barra, 
                         dbo.producto.nombre, dbo.trans_re_det.cantidad_recibida, dbo.unidad_medida.Nombre, dbo.producto_estado.nombre, dbo.producto_presentacion.nombre, dbo.trans_re_det.No_Linea, dbo.trans_oc_det.cantidad, 
                         dbo.trans_re_enc.hora_ini_pc, dbo.trans_re_enc.hora_fin_pc, dbo.trans_re_det.lote, dbo.trans_re_det.fecha_vence, dbo.trans_re_det.peso, dbo.trans_oc_enc.IdProveedorBodega, dbo.proveedor.IdProveedor, 
                         dbo.trans_re_enc.activo, dbo.producto_bodega.IdProductoBodega, dbo.trans_re_det.atributo_variante_1
```
