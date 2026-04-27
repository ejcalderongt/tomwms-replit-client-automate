---
id: db-brain-view-vw-existenciaspornodocumento
type: db-view
title: dbo.VW_ExistenciasPorNoDocumento
schema: dbo
name: VW_ExistenciasPorNoDocumento
kind: view
modify_date: 2022-05-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ExistenciasPorNoDocumento`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-24 |
| Columnas | 32 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 4 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 5 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 6 | `IdOrdenCompraEnc` | `int` |  |  |
| 7 | `No_DocumentoOC` | `nvarchar(30)` | ✓ |  |
| 8 | `No_DocumentoRec` | `nvarchar(100)` | ✓ |  |
| 9 | `ReferenciaOC` | `nvarchar(100)` | ✓ |  |
| 10 | `Fecha` | `datetime` | ✓ |  |
| 11 | `estado` | `nvarchar(20)` | ✓ |  |
| 12 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 13 | `Descripcion` | `nvarchar(50)` |  |  |
| 14 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `Usuario_Agrego` | `nvarchar(100)` | ✓ |  |
| 17 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 18 | `CodigoProd` | `nvarchar(50)` | ✓ |  |
| 19 | `BarraProd` | `nvarchar(35)` | ✓ |  |
| 20 | `NombreProd` | `nvarchar(100)` | ✓ |  |
| 21 | `Recibido` | `float` | ✓ |  |
| 22 | `Existencia_Actual_UMBas` | `float` |  |  |
| 23 | `Existencia_Actual_Pres` | `float` | ✓ |  |
| 24 | `UM` | `nvarchar(50)` | ✓ |  |
| 25 | `EstadoProd` | `nvarchar(50)` | ✓ |  |
| 26 | `PresProd` | `nvarchar(50)` | ✓ |  |
| 27 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 28 | `factor` | `float` | ✓ |  |
| 29 | `Lote` | `nvarchar(50)` |  |  |
| 30 | `Vence` | `datetime` | ✓ |  |
| 31 | `IdStock` | `int` |  |  |
| 32 | `Ubicacion_Origen` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
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
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`
- `unidad_medida`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ExistenciasPorNoDocumento] AS
SELECT dbo.trans_re_enc.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, oc_pol.codigo_poliza AS Poliza, dbo.proveedor.nombre AS Proveedor, dbo.bodega.nombre AS Bodega, dbo.trans_re_oc.IdOrdenCompraEnc, 
                  dbo.trans_oc_enc.No_Documento AS No_DocumentoOC, dbo.trans_re_oc.no_docto AS No_DocumentoRec, dbo.trans_oc_enc.Referencia AS ReferenciaOC, dbo.trans_re_enc.fecha_recepcion AS Fecha, dbo.trans_re_enc.estado, 
                  dbo.trans_re_tr.TipoTrans, dbo.trans_re_tr.Descripcion, dbo.bodega_muelles.nombre AS Muelle, dbo.trans_re_enc.activo, dbo.usuario.nombres AS Usuario_Agrego, dbo.trans_re_enc.fec_agr AS Fecha_Agrego, 
                  dbo.producto.codigo AS CodigoProd, dbo.producto.codigo_barra AS BarraProd, dbo.producto.nombre AS NombreProd, dbo.trans_re_det.cantidad_recibida AS Recibido, dbo.stock.cantidad AS Existencia_Actual_UMBas, 
                  CASE WHEN dbo.stock.IdPresentacion <> 0 THEN dbo.stock.cantidad / IIF(dbo.producto_presentacion.factor = 0, 1, dbo.producto_presentacion.factor) ELSE dbo.stock.cantidad END AS Existencia_Actual_Pres, 
                  dbo.unidad_medida.Nombre AS UM, dbo.producto_estado.nombre AS EstadoProd, dbo.producto_presentacion.nombre AS PresProd, dbo.trans_re_det.lic_plate Licencia, dbo.producto_presentacion.factor, dbo.stock.lote AS Lote, 
                  dbo.stock.fecha_vence AS Vence, dbo.stock.IdStock, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion,dbo.Bodega.IdBodega) AS Ubicacion_Origen
FROM     dbo.trans_re_det INNER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.bodega_muelles INNER JOIN
                  dbo.bodega ON dbo.bodega_muelles.IdBodega = dbo.bodega.IdBodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega ON dbo.trans_re_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.unidad_medida ON dbo.trans_re_det.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                  dbo.producto_estado ON dbo.trans_re_det.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                  dbo.trans_re_tr INNER JOIN
                  dbo.usuario INNER JOIN
                  dbo.propietarios INNER JOIN
                  dbo.propietario_bodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario INNER JOIN
                  dbo.trans_re_enc ON dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_re_enc.IdPropietarioBodega ON dbo.usuario.IdUsuario = dbo.trans_re_enc.user_agr ON 
                  dbo.trans_re_tr.IdTipoTransaccion = dbo.trans_re_enc.IdTipoTransaccion ON dbo.trans_re_det.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND dbo.bodega_muelles.IdMuelle = dbo.trans_re_enc.IdMuelle INNER JOIN
                  dbo.trans_re_oc INNER JOIN
                  dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc INNER JOIN
                  dbo.proveedor_bodega ON dbo.trans_oc_enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                  dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc INNER JOIN
                  dbo.stock ON dbo.trans_re_det.IdProductoBodega = dbo.stock.IdProductoBodega AND dbo.trans_re_det.IdRecepcionEnc = dbo.stock.IdRecepcionEnc AND dbo.trans_re_det.IdRecepcionDet = dbo.stock.IdRecepcionDet LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.producto_presentacion.IdPresentacion = dbo.trans_re_det.IdPresentacion AND dbo.producto_presentacion.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.bodega_ubicacion ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega LEFT OUTER JOIN
                  dbo.trans_oc_pol oc_pol ON dbo.trans_oc_enc.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc
```
