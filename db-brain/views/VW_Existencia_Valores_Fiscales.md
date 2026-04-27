---
id: db-brain-view-vw-existencia-valores-fiscales
type: db-view
title: dbo.VW_Existencia_Valores_Fiscales
schema: dbo
name: VW_Existencia_Valores_Fiscales
kind: view
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Existencia_Valores_Fiscales`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-05-28 |
| Columnas | 38 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 4 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `IdOrdenCompraEnc` | `int` |  |  |
| 6 | `No_DocumentoOC` | `nvarchar(30)` | ✓ |  |
| 7 | `No_DocumentoRec` | `nvarchar(50)` | ✓ |  |
| 8 | `ReferenciaOC` | `nvarchar(100)` | ✓ |  |
| 9 | `Fecha` | `datetime` | ✓ |  |
| 10 | `estado` | `nvarchar(20)` | ✓ |  |
| 11 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 12 | `Descripcion` | `nvarchar(50)` |  |  |
| 13 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 16 | `CodigoProd` | `nvarchar(50)` | ✓ |  |
| 17 | `BarraProd` | `nvarchar(35)` | ✓ |  |
| 18 | `NombreProd` | `nvarchar(100)` | ✓ |  |
| 19 | `Recibido` | `float` | ✓ |  |
| 20 | `Existencia_Actual_UMBas` | `float` |  |  |
| 21 | `Existencia_Actual_Pres` | `float` | ✓ |  |
| 22 | `UM` | `nvarchar(50)` | ✓ |  |
| 23 | `EstadoProd` | `nvarchar(50)` | ✓ |  |
| 24 | `PresProd` | `nvarchar(50)` | ✓ |  |
| 25 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 26 | `factor` | `float` | ✓ |  |
| 27 | `lote` | `nvarchar(50)` |  |  |
| 28 | `Vence` | `datetime` | ✓ |  |
| 29 | `IdStock` | `int` |  |  |
| 30 | `Ubicacion_Origen` | `nvarchar(200)` | ✓ |  |
| 31 | `NoPoliza` | `nvarchar(50)` | ✓ |  |
| 32 | `valor_aduana` | `float` | ✓ |  |
| 33 | `valor_fob` | `float` | ✓ |  |
| 34 | `valor_iva` | `float` | ✓ |  |
| 35 | `valor_dai` | `float` | ✓ |  |
| 36 | `valor_seguro` | `float` | ✓ |  |
| 37 | `valor_flete` | `float` | ✓ |  |
| 38 | `peso_neto` | `float` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_muelles`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `stock`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Existencia_Valores_Fiscales]
AS
SELECT        trans_re_enc.IdRecepcionEnc, propietarios.nombre_comercial AS Propietario, proveedor.nombre AS Proveedor, bodega.nombre AS Bodega, trans_re_oc.IdOrdenCompraEnc, 
                         trans_oc_enc.No_Documento AS No_DocumentoOC, trans_re_oc.no_docto AS No_DocumentoRec, trans_oc_enc.Referencia AS ReferenciaOC, trans_re_enc.fecha_recepcion AS Fecha, trans_re_enc.estado, 
                         trans_re_tr.TipoTrans, trans_re_tr.Descripcion, bodega_muelles.nombre AS Muelle, trans_re_enc.activo, trans_re_enc.fec_agr AS Fecha_Agrego, producto.codigo AS CodigoProd, 
                         producto.codigo_barra AS BarraProd, producto.nombre AS NombreProd, trans_re_det.cantidad_recibida AS Recibido, stock.cantidad AS Existencia_Actual_UMBas, 
                         CASE WHEN stock.IdPresentacion <> 0 THEN stock.cantidad / producto_presentacion.factor ELSE stock.cantidad END AS Existencia_Actual_Pres, unidad_medida.Nombre AS UM, 
                         producto_estado.nombre AS EstadoProd, producto_presentacion.nombre AS PresProd, trans_re_det.lic_plate, producto_presentacion.factor, stock.lote, stock.fecha_vence AS Vence, stock.IdStock, 
                         DBO.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Ubicacion_Origen, trans_oc_pol.NoPoliza, trans_oc_det.valor_aduana, trans_oc_det.valor_fob, 
                         trans_oc_det.valor_iva, trans_oc_det.valor_dai, trans_oc_det.valor_seguro, trans_oc_det.valor_flete, trans_oc_det.peso_neto
FROM            bodega_tramo INNER JOIN
                         trans_re_det INNER JOIN
                         producto_bodega INNER JOIN
                         bodega_muelles INNER JOIN
                         bodega ON bodega_muelles.IdBodega = bodega.IdBodega ON producto_bodega.IdBodega = bodega.IdBodega ON trans_re_det.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                         producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                         unidad_medida ON trans_re_det.IdUnidadMedida = unidad_medida.IdUnidadMedida AND producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida INNER JOIN
                         producto_estado ON trans_re_det.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                         trans_re_tr INNER JOIN
                         propietarios INNER JOIN
                         propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario INNER JOIN
                         trans_re_enc ON propietario_bodega.IdPropietarioBodega = trans_re_enc.IdPropietarioBodega ON trans_re_tr.IdTipoTransaccion = trans_re_enc.IdTipoTransaccion ON 
                         trans_re_det.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc AND bodega_muelles.IdMuelle = trans_re_enc.IdMuelle INNER JOIN
                         trans_re_oc INNER JOIN
                         trans_oc_enc ON trans_re_oc.IdOrdenCompraEnc = trans_oc_enc.IdOrdenCompraEnc INNER JOIN
                         proveedor_bodega ON trans_oc_enc.IdProveedorBodega = proveedor_bodega.IdAsignacion INNER JOIN
                         proveedor ON proveedor_bodega.IdProveedor = proveedor.IdProveedor ON trans_re_enc.IdRecepcionEnc = trans_re_oc.IdRecepcionEnc INNER JOIN
                         stock ON trans_re_det.IdProductoBodega = stock.IdProductoBodega AND trans_re_det.IdRecepcionEnc = stock.IdRecepcionEnc AND 
                         trans_re_det.IdRecepcionDet = stock.IdRecepcionDet INNER JOIN
                         bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion AND stock.IdBodega = bodega_ubicacion.IdBodega ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo AND 
                         bodega_tramo.IdBodega = bodega_ubicacion.IdBodega AND bodega_tramo.IdArea = bodega_ubicacion.IdArea INNER JOIN
                         bodega_sector INNER JOIN
                         bodega_area ON bodega_sector.IdArea = bodega_area.IdArea AND bodega_sector.IdBodega = bodega_area.IdBodega ON bodega_tramo.IdSector = bodega_sector.IdSector AND 
                         bodega_tramo.IdBodega = bodega_sector.IdBodega AND bodega_tramo.IdArea = bodega_sector.IdArea INNER JOIN
                         trans_oc_det ON producto_bodega.IdProductoBodega = trans_oc_det.IdProductoBodega AND unidad_medida.IdUnidadMedida = trans_oc_det.IdUnidadMedidaBasica AND 
                         trans_oc_enc.IdOrdenCompraEnc = trans_oc_det.IdOrdenCompraEnc LEFT OUTER JOIN
                         trans_oc_pol ON trans_re_oc.IdOrdenCompraEnc = trans_oc_pol.IdOrdenCompraEnc LEFT OUTER JOIN
                         producto_presentacion ON trans_oc_det.IdPresentacion = producto_presentacion.IdPresentacion AND trans_re_det.IdPresentacion = producto_presentacion.IdPresentacion AND 
                         producto.IdProducto = producto_presentacion.IdProducto
```
