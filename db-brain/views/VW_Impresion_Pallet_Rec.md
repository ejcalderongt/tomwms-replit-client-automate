---
id: db-brain-view-vw-impresion-pallet-rec
type: db-view
title: dbo.VW_Impresion_Pallet_Rec
schema: dbo
name: VW_Impresion_Pallet_Rec
kind: view
modify_date: 2018-03-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Impresion_Pallet_Rec`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-03-12 |
| Columnas | 33 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockRec` | `int` | ✓ |  |
| 2 | `lic_plate` | `varchar(max)` | ✓ |  |
| 3 | `Rec_No` | `int` | ✓ |  |
| 4 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 5 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 6 | `Propietario_Nombre` | `nvarchar(100)` | ✓ |  |
| 7 | `IdProveedorBodega` | `int` | ✓ |  |
| 8 | `Proveedor_Nombre` | `nvarchar(50)` | ✓ |  |
| 9 | `Proveedor_Tel` | `nvarchar(50)` | ✓ |  |
| 10 | `Proveedor_Codigo` | `nvarchar(50)` | ✓ |  |
| 11 | `Proveedor_Dir` | `nvarchar(250)` | ✓ |  |
| 12 | `Producto_Codigo` | `nvarchar(50)` | ✓ |  |
| 13 | `impreso` | `bit` | ✓ |  |
| 14 | `Producto_Nombre_Largo` | `nvarchar(100)` | ✓ |  |
| 15 | `Producto_UM` | `nvarchar(50)` | ✓ |  |
| 16 | `Producto_Presentacion` | `nvarchar(50)` | ✓ |  |
| 17 | `Producto_Cantidad` | `float` | ✓ |  |
| 18 | `Producto_Vence` | `datetime` | ✓ |  |
| 19 | `Producto_Lote` | `nvarchar(50)` | ✓ |  |
| 20 | `Producto_Estado` | `nvarchar(50)` | ✓ |  |
| 21 | `LP` | `varchar(max)` | ✓ |  |
| 22 | `PC` | `nvarchar(25)` | ✓ |  |
| 23 | `Ref_PC` | `nvarchar(50)` | ✓ |  |
| 24 | `Fecha_PC` | `datetime` | ✓ |  |
| 25 | `Observacion` | `text` | ✓ |  |
| 26 | `IdOperadorBodega` | `int` | ✓ |  |
| 27 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 28 | `Imprimio` | `nvarchar(201)` | ✓ |  |
| 29 | `IdEmpresa` | `int` | ✓ |  |
| 30 | `IdPropietario` | `int` | ✓ |  |
| 31 | `fecha_manufactura` | `datetime` | ✓ |  |
| 32 | `Producto_Peso` | `float` | ✓ |  |
| 33 | `IdPresentacion` | `int` | ✓ |  |

## Consume

- `bodega`
- `empresa`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `stock_rec`
- `trans_oc_enc`
- `trans_re_det`
- `trans_re_oc`
- `unidad_medida`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Impresion_Pallet_Rec
AS
SELECT     dbo.stock_rec.IdStockRec, dbo.stock_rec.lic_plate, dbo.stock_rec.IdRecepcionEnc AS Rec_No, dbo.empresa.nombre AS Empresa, dbo.bodega.nombre AS Bodega, 
                      dbo.propietarios.nombre_comercial AS Propietario_Nombre, dbo.trans_oc_enc.IdProveedorBodega, dbo.proveedor.nombre AS Proveedor_Nombre, 
                      dbo.proveedor.telefono AS Proveedor_Tel, dbo.proveedor.codigo AS Proveedor_Codigo, dbo.proveedor.direccion AS Proveedor_Dir, 
                      dbo.producto.codigo AS Producto_Codigo, dbo.stock_rec.impreso, dbo.producto.nombre AS Producto_Nombre_Largo, dbo.unidad_medida.Nombre AS Producto_UM, 
                      dbo.producto_presentacion.nombre AS Producto_Presentacion, dbo.trans_re_det.cantidad_recibida AS Producto_Cantidad, 
                      dbo.trans_re_det.fecha_vence AS Producto_Vence, dbo.trans_re_det.lote AS Producto_Lote, dbo.trans_re_det.nombre_producto_estado AS Producto_Estado, 
                      dbo.stock_rec.lic_plate AS LP, dbo.trans_oc_enc.No_Documento AS PC, dbo.trans_oc_enc.Referencia AS Ref_PC, dbo.trans_oc_enc.Fecha_Recepcion AS Fecha_PC, 
                      dbo.trans_oc_enc.Observacion, dbo.trans_re_det.IdOperadorBodega, dbo.trans_re_det.user_agr, dbo.usuario.nombres + ' ' + dbo.usuario.apellidos AS Imprimio, 
                      dbo.empresa.IdEmpresa, dbo.propietario_bodega.IdPropietario, dbo.stock_rec.fecha_manufactura, dbo.stock_rec.peso AS Producto_Peso, 
                      dbo.stock_rec.IdPresentacion
FROM         dbo.proveedor_bodega INNER JOIN
                      dbo.trans_oc_enc ON dbo.proveedor_bodega.IdAsignacion = dbo.trans_oc_enc.IdProveedorBodega INNER JOIN
                      dbo.stock_rec INNER JOIN
                      dbo.propietario_bodega ON dbo.stock_rec.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                      dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                      dbo.bodega ON dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega INNER JOIN
                      dbo.empresa ON dbo.propietarios.IdEmpresa = dbo.empresa.IdEmpresa AND dbo.bodega.IdEmpresa = dbo.empresa.IdEmpresa ON 
                      dbo.proveedor_bodega.IdBodega = dbo.bodega.IdBodega INNER JOIN
                      dbo.trans_re_det ON dbo.stock_rec.IdRecepcionDet = dbo.trans_re_det.IdRecepcionDet AND 
                      dbo.stock_rec.IdRecepcionEnc = dbo.trans_re_det.IdRecepcionEnc INNER JOIN
                      dbo.producto_bodega ON dbo.stock_rec.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND 
                      dbo.bodega.IdBodega = dbo.producto_bodega.IdBodega AND dbo.trans_re_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                      dbo.producto ON dbo.propietarios.IdPropietario = dbo.producto.IdPropietario AND dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                      dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor AND dbo.propietarios.IdPropietario = dbo.proveedor.IdPropietario AND 
                      dbo.empresa.IdEmpresa = dbo.proveedor.IdEmpresa INNER JOIN
                      dbo.unidad_medida ON dbo.propietarios.IdPropietario = dbo.unidad_medida.IdPropietario AND 
                      dbo.trans_re_det.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                      dbo.usuario ON dbo.trans_re_det.user_agr = dbo.usuario.IdUsuario LEFT OUTER JOIN
                      dbo.producto_presentacion ON dbo.stock_rec.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND 
                      dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto RIGHT OUTER JOIN
                      dbo.trans_re_oc ON dbo.trans_oc_enc.IdOrdenCompraEnc = dbo.trans_re_oc.IdOrdenCompraEnc AND 
                      dbo.stock_rec.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc
WHERE     (NOT (dbo.stock_rec.lic_plate IS NULL)) AND (dbo.stock_rec.impreso = 0)
```
