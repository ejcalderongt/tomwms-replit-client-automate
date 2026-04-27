---
id: db-brain-view-vw-recepcion-det
type: db-view
title: dbo.VW_Recepcion_Det
schema: dbo
name: VW_Recepcion_Det
kind: view
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Recepcion_Det`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-03-18 |
| Columnas | 47 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `Propietario` | `nvarchar(100)` |  |  |
| 5 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 6 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 7 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 8 | `Codigo_Proveedor` | `nvarchar(50)` | ✓ |  |
| 9 | `Nombre_Proveedor` | `nvarchar(100)` | ✓ |  |
| 10 | `No_DocumentoOC` | `nvarchar(30)` | ✓ |  |
| 11 | `ReferenciaOC` | `nvarchar(100)` | ✓ |  |
| 12 | `Fecha` | `datetime` | ✓ |  |
| 13 | `estado` | `nvarchar(20)` | ✓ |  |
| 14 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 15 | `Descripcion` | `nvarchar(50)` |  |  |
| 16 | `activo` | `bit` | ✓ |  |
| 17 | `Usuario_Agrego` | `nvarchar(100)` | ✓ |  |
| 18 | `Operador_HH` | `nvarchar(100)` | ✓ |  |
| 19 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 20 | `CodigoProd` | `nvarchar(50)` | ✓ |  |
| 21 | `BarraProd` | `nvarchar(35)` | ✓ |  |
| 22 | `NombreProd` | `nvarchar(100)` | ✓ |  |
| 23 | `Recibido` | `float` | ✓ |  |
| 24 | `UM` | `nvarchar(50)` | ✓ |  |
| 25 | `EstadoProd` | `nvarchar(50)` | ✓ |  |
| 26 | `PresProd` | `nvarchar(50)` | ✓ |  |
| 27 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 28 | `lote` | `nvarchar(50)` | ✓ |  |
| 29 | `fecha_vence` | `datetime` | ✓ |  |
| 30 | `IdPedidoEncDevolucion` | `int` | ✓ |  |
| 31 | `no_documento_devolucion` | `nvarchar(50)` | ✓ |  |
| 32 | `Nombre_Cliente_Devolucion` | `nvarchar(150)` | ✓ |  |
| 33 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 34 | `IdUbicacionRecepcion` | `int` | ✓ |  |
| 35 | `Area` | `nvarchar(200)` | ✓ |  |
| 36 | `Familia` | `nvarchar(50)` | ✓ |  |
| 37 | `Parametro_A` | `nvarchar(50)` | ✓ |  |
| 38 | `Parametro_B` | `nvarchar(50)` | ✓ |  |
| 39 | `Marca` | `nvarchar(50)` | ✓ |  |
| 40 | `Observacion` | `text` | ✓ |  |
| 41 | `Codigo_Regimen_Ingreso` | `nvarchar(20)` | ✓ |  |
| 42 | `Numero_Orden_Ingreso` | `nvarchar(50)` | ✓ |  |
| 43 | `Carta_Cupo` | `nvarchar(50)` | ✓ |  |
| 44 | `No_Contenedor` | `nvarchar(50)` | ✓ |  |
| 45 | `Placa_Contenedor_Ingreso` | `nvarchar(20)` | ✓ |  |
| 46 | `Poliza_Activa` | `bit` | ✓ |  |
| 47 | `Dua_Ingreso` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `cliente`
- `empresa_transporte_vehiculos`
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
- `regimen_fiscal`
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
CREATE VIEW [dbo].[VW_Recepcion_Det]
AS
SELECT pb.IdBodega, re_det.IdRecepcionEnc, pr.IdPropietario, pr.nombre_comercial AS Propietario, oc_pol.codigo_poliza AS Poliza, bd.codigo AS Codigo_Bodega, oc_enc.IdOrdenCompraEnc, prov.codigo AS Codigo_Proveedor, 
                  prov.nombre AS Nombre_Proveedor, oc_enc.No_Documento AS No_DocumentoOC, oc_enc.Referencia AS ReferenciaOC, re_enc.hora_ini_pc AS Fecha, re_enc.estado, re_tr.TipoTrans, re_tr.Descripcion, re_enc.activo, 
                  usr.nombres AS Usuario_Agrego, op.nombres AS Operador_HH, re_det.fec_agr AS Fecha_Agrego, prod.codigo AS CodigoProd, prod.codigo_barra AS BarraProd, prod.nombre AS NombreProd, re_det.cantidad_recibida AS Recibido, 
                  um.Nombre AS UM, pr_estado.nombre AS EstadoProd, prod_pr.nombre AS PresProd, re_det.lic_plate AS Licencia, re_det.lote, re_det.fecha_vence, oc_enc.IdPedidoEncDevolucion, oc_enc.no_documento_devolucion, 
                  dbo.cliente.nombre_comercial AS Nombre_Cliente_Devolucion, dbo.producto_clasificacion.nombre AS Clasificacion, re_enc.IdUbicacionRecepcion, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, re_enc.IdBodega) AS Area, 
                  dbo.producto_familia.nombre AS Familia, dbo.producto_parametro_a.Nombre AS Parametro_A, 
				  dbo.producto_parametro_b.Nombre AS Parametro_B, dbo.producto_marca.nombre AS Marca
				  ,oc_enc.Observacion,
				  regimen_fiscal.codigo_regimen Codigo_Regimen_Ingreso, oc_pol.numero_orden Numero_Orden_Ingreso,
				  re_enc.Carta_Cupo, trim(re_enc.no_contenedor) No_Contenedor,
				  vehiculo.placa as Placa_Contenedor_Ingreso,oc_pol.activo as Poliza_Activa,oc_pol.dua as Dua_Ingreso
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
                  dbo.usuario AS usr ON re_enc.user_agr = usr.IdUsuario LEFT OUTER JOIN
				  dbo.regimen_fiscal on oc_pol.IdRegimen=regimen_fiscal.IdRegimen LEFT OUTER JOIN
				  dbo.empresa_transporte_vehiculos vehiculo on re_enc.idvehiculo = vehiculo.IdVehiculo
```
