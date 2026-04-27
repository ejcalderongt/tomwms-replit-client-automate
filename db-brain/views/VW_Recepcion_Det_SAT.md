---
id: db-brain-view-vw-recepcion-det-sat
type: db-view
title: dbo.VW_Recepcion_Det_SAT
schema: dbo
name: VW_Recepcion_Det_SAT
kind: view
modify_date: 2023-11-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Recepcion_Det_SAT`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-11-06 |
| Columnas | 39 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 5 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 6 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 7 | `Codigo_Proveedor` | `nvarchar(50)` | ✓ |  |
| 8 | `Nombre_Proveedor` | `nvarchar(100)` | ✓ |  |
| 9 | `No_DocumentoOC` | `nvarchar(30)` | ✓ |  |
| 10 | `ReferenciaOC` | `nvarchar(100)` | ✓ |  |
| 11 | `Fecha` | `datetime` | ✓ |  |
| 12 | `estado` | `nvarchar(20)` | ✓ |  |
| 13 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 14 | `Descripcion` | `nvarchar(50)` |  |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `Usuario_Agrego` | `nvarchar(100)` | ✓ |  |
| 17 | `Operador_HH` | `nvarchar(100)` | ✓ |  |
| 18 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 19 | `CodigoProd` | `nvarchar(50)` | ✓ |  |
| 20 | `BarraProd` | `nvarchar(35)` | ✓ |  |
| 21 | `NombreProd` | `nvarchar(100)` | ✓ |  |
| 22 | `Recibido` | `float` | ✓ |  |
| 23 | `UM` | `nvarchar(50)` | ✓ |  |
| 24 | `EstadoProd` | `nvarchar(50)` | ✓ |  |
| 25 | `PresProd` | `nvarchar(50)` | ✓ |  |
| 26 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 27 | `lote` | `nvarchar(50)` | ✓ |  |
| 28 | `fecha_vence` | `datetime` | ✓ |  |
| 29 | `IdPedidoEncDevolucion` | `int` | ✓ |  |
| 30 | `no_documento_devolucion` | `nvarchar(50)` | ✓ |  |
| 31 | `Nombre_Cliente_Devolucion` | `nvarchar(150)` | ✓ |  |
| 32 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 33 | `IdUbicacionRecepcion` | `int` | ✓ |  |
| 34 | `Area` | `nvarchar(200)` | ✓ |  |
| 35 | `familia` | `nvarchar(50)` | ✓ |  |
| 36 | `parametro_a` | `nvarchar(50)` | ✓ |  |
| 37 | `parametro_b` | `nvarchar(50)` | ✓ |  |
| 38 | `Marca` | `nvarchar(50)` | ✓ |  |
| 39 | `Observacion` | `text` | ✓ |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `cliente`
- `Nombre_Area`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_marca`
- `producto_parametro_a`
- `producto_parametro_b`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_pe_enc`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`
- `unidad_medida`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Recepcion_Det_SAT]
AS
SELECT pb.IdBodega, re_det.IdRecepcionEnc, pr.nombre_comercial AS Propietario, oc_pol.codigo_poliza AS Poliza, bd.codigo AS Codigo_Bodega, oc_enc.IdOrdenCompraEnc, prov.codigo AS Codigo_Proveedor, 
                  prov.nombre AS Nombre_Proveedor, oc_enc.No_Documento AS No_DocumentoOC, oc_enc.Referencia AS ReferenciaOC, re_enc.hora_ini_pc AS Fecha, re_enc.estado, re_tr.TipoTrans, re_tr.Descripcion, re_enc.activo, 
                  usr.nombres AS Usuario_Agrego, op.nombres AS Operador_HH, re_det.fec_agr AS Fecha_Agrego, prod.codigo AS CodigoProd, prod.codigo_barra AS BarraProd, prod.nombre AS NombreProd, re_det.cantidad_recibida AS Recibido, 
                  um.Nombre AS UM, pr_estado.nombre AS EstadoProd, prod_pr.nombre AS PresProd, re_det.lic_plate AS Licencia, re_det.lote, re_det.fecha_vence, oc_enc.IdPedidoEncDevolucion, oc_enc.no_documento_devolucion, 
                  dbo.cliente.nombre_comercial AS Nombre_Cliente_Devolucion, dbo.producto_clasificacion.nombre AS Clasificacion, re_enc.IdUbicacionRecepcion, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, re_enc.IdBodega) AS Area, 
                  dbo.producto_familia.nombre AS familia, dbo.producto_parametro_a.Nombre AS parametro_a, dbo.producto_parametro_b.Nombre AS parametro_b, dbo.producto_marca.nombre AS Marca
				  ,oc_enc.Observacion
FROM     dbo.bodega_ubicacion INNER JOIN
                  dbo.producto_estado AS pr_estado INNER JOIN
                  dbo.trans_oc_enc AS oc_enc INNER JOIN
                  dbo.trans_re_oc AS re_oc ON oc_enc.IdOrdenCompraEnc = re_oc.IdOrdenCompraEnc INNER JOIN
                  dbo.trans_re_enc AS re_enc ON re_oc.IdRecepcionEnc = re_enc.IdRecepcionEnc RIGHT OUTER JOIN
                  dbo.trans_re_det AS re_det ON re_enc.IdRecepcionEnc = re_det.IdRecepcionEnc INNER JOIN
                  dbo.trans_re_tr AS re_tr ON re_enc.IdTipoTransaccion = re_tr.IdTipoTransaccion LEFT OUTER JOIN
                  dbo.trans_oc_pol AS oc_pol ON oc_enc.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc INNER JOIN
                  dbo.bodega AS bd ON oc_enc.IDBODEGA = bd.IdBodega INNER JOIN
                  dbo.producto_bodega AS pb ON pb.IdBodega = bd.IdBodega AND re_det.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                  dbo.propietario_bodega AS pb2 ON oc_enc.IdPropietarioBodega = pb2.IdPropietarioBodega INNER JOIN
                  dbo.propietarios AS pr ON pb2.IdPropietario = pr.IdPropietario INNER JOIN
                  dbo.unidad_medida AS um ON re_det.IdUnidadMedida = um.IdUnidadMedida ON pr_estado.IdEstado = re_det.IdProductoEstado INNER JOIN
                  dbo.producto AS prod ON pb.IdProducto = prod.IdProducto INNER JOIN
                  dbo.proveedor_bodega AS prov_b ON oc_enc.IdProveedorBodega = prov_b.IdAsignacion INNER JOIN
                  dbo.proveedor AS prov ON prov_b.IdProveedor = prov.IdProveedor ON dbo.bodega_ubicacion.IdUbicacion = re_enc.IdUbicacionRecepcion AND dbo.bodega_ubicacion.IdBodega = re_enc.IdBodega LEFT OUTER JOIN
                  dbo.producto_parametro_b ON prod.IDPRODUCTOPARAMETROB = dbo.producto_parametro_b.IdProductoParametroB LEFT OUTER JOIN
                  dbo.producto_parametro_a ON prod.IDPRODUCTOPARAMETROA = dbo.producto_parametro_a.IdProductoParametroA LEFT OUTER JOIN
                  dbo.producto_familia ON pr.IdPropietario = dbo.producto_familia.IdPropietario AND prod.IdFamilia = dbo.producto_familia.IdFamilia LEFT OUTER JOIN
                  dbo.producto_marca ON pr.IdPropietario = dbo.producto_marca.IdPropietario AND prod.IdMarca = dbo.producto_marca.IdMarca LEFT OUTER JOIN
                  dbo.producto_presentacion AS prod_pr ON prod_pr.IdPresentacion = re_det.IdPresentacion LEFT OUTER JOIN
                  dbo.producto_clasificacion ON prod.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                  dbo.trans_pe_enc RIGHT OUTER JOIN
                  dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente ON oc_enc.IdPedidoEncDevolucion = dbo.trans_pe_enc.IdPedidoEnc LEFT OUTER JOIN
                  dbo.operador AS op INNER JOIN
                  dbo.operador_bodega AS op_b ON op.IdOperador = op_b.IdOperador ON re_det.IdOperadorBodega = op_b.IdOperadorBodega LEFT OUTER JOIN
                  dbo.usuario AS usr ON re_enc.user_agr = usr.IdUsuario
```
