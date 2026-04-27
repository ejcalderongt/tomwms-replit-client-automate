---
id: db-brain-view-vw-despacho-rep-det-i
type: db-view
title: dbo.VW_Despacho_Rep_Det_I
schema: dbo
name: VW_Despacho_Rep_Det_I
kind: view
modify_date: 2025-06-09
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Despacho_Rep_Det_I`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-09 |
| Columnas | 70 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdPickingUbic` | `int` | ✓ |  |
| 3 | `IdStock` | `int` | ✓ |  |
| 4 | `IdPedidoDet` | `int` | ✓ |  |
| 5 | `IdPropietarioBodega` | `int` | ✓ |  |
| 6 | `IdProductoBodega` | `int` | ✓ |  |
| 7 | `IdProductoEstado` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `IdUnidadMedida` | `int` | ✓ |  |
| 10 | `IdRecepcion` | `bigint` | ✓ |  |
| 11 | `IdDespachoEnc` | `int` |  |  |
| 12 | `IdDespachoDet` | `int` |  |  |
| 13 | `IdPedidoEnc` | `int` | ✓ |  |
| 14 | `IdProducto` | `int` |  |  |
| 15 | `Propietario` | `nvarchar(100)` |  |  |
| 16 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 17 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 18 | `UM` | `nvarchar(50)` | ✓ |  |
| 19 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 20 | `Fecha` | `datetime` |  |  |
| 21 | `NombreEstado` | `nvarchar(50)` | ✓ |  |
| 22 | `factor` | `float` | ✓ |  |
| 23 | `Estado` | `nvarchar(50)` | ✓ |  |
| 24 | `lote` | `nvarchar(50)` | ✓ |  |
| 25 | `Vence` | `datetime` | ✓ |  |
| 26 | `cantidad_pickeada` | `float` |  |  |
| 27 | `cantidad_verificada` | `float` |  |  |
| 28 | `Peso_Pickeado` | `float` |  |  |
| 29 | `Peso_Verificado` | `float` |  |  |
| 30 | `CantidadDespachada` | `float` | ✓ |  |
| 31 | `PesoDespachado` | `float` | ✓ |  |
| 32 | `Encontrado` | `bit` |  |  |
| 33 | `Acepto` | `bit` |  |  |
| 34 | `No_Documento_WMS` | `bigint` | ✓ |  |
| 35 | `No_Referencia` | `nvarchar(25)` | ✓ |  |
| 36 | `Codigo_Cliente` | `nvarchar(150)` | ✓ |  |
| 37 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 38 | `idubicacionvirtual` | `int` | ✓ |  |
| 39 | `no_pase` | `int` | ✓ |  |
| 40 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 41 | `es_bodega_traslado` | `bit` | ✓ |  |
| 42 | `numero` | `int` | ✓ |  |
| 43 | `marchamo` | `nvarchar(50)` | ✓ |  |
| 44 | `observacion` | `nvarchar(500)` | ✓ |  |
| 45 | `Ubicacion_Origen` | `int` | ✓ |  |
| 46 | `Codigo_Ruta` | `nvarchar(15)` | ✓ |  |
| 47 | `Nombre_Ruta` | `nvarchar(50)` | ✓ |  |
| 48 | `Placa_Vehiculo` | `nvarchar(20)` | ✓ |  |
| 49 | `placa_comercial` | `nvarchar(50)` | ✓ |  |
| 50 | `Marca_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 51 | `Modelo_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 52 | `Nombre_Piloto` | `nvarchar(150)` | ✓ |  |
| 53 | `Apellido_Piloto` | `nvarchar(150)` | ✓ |  |
| 54 | `No_Carnet_Piloto` | `nvarchar(50)` | ✓ |  |
| 55 | `No_Licencia_Piloto` | `nvarchar(50)` | ✓ |  |
| 56 | `Licencia` | `nvarchar(25)` | ✓ |  |
| 57 | `parametro_a` | `nvarchar(50)` | ✓ |  |
| 58 | `parametro_b` | `nvarchar(50)` | ✓ |  |
| 59 | `marca` | `nvarchar(50)` | ✓ |  |
| 60 | `clasificacion` | `nvarchar(50)` | ✓ |  |
| 61 | `familia` | `nvarchar(50)` | ✓ |  |
| 62 | `numero_orden_pedido` | `nvarchar(50)` | ✓ |  |
| 63 | `codigo_poliza_pedido` | `nvarchar(50)` | ✓ |  |
| 64 | `codigo_poliza_ingreso` | `nvarchar(150)` | ✓ |  |
| 65 | `numero_orden_ingreso` | `nvarchar(50)` | ✓ |  |
| 66 | `codigo_regimen_salida` | `nvarchar(20)` | ✓ |  |
| 67 | `placa_contenedor_salida` | `nvarchar(20)` | ✓ |  |
| 68 | `poliza_activa` | `bit` | ✓ |  |
| 69 | `poliza_salida_activa` | `bit` | ✓ |  |
| 70 | `dua_salida` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `cliente`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
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
- `regimen_fiscal`
- `road_ruta`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_oc_pol`
- `trans_pe_enc`
- `trans_pe_pol`
- `trans_picking_ubic`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Despacho_Rep_Det_I]
AS
SELECT dbo.trans_despacho_enc.IdBodega, dbo.trans_picking_ubic.IdPickingUbic, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdPedidoDet, dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, 
                  dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_picking_ubic.IdRecepcion, dbo.trans_despacho_enc.IdDespachoEnc, 
                  dbo.trans_despacho_det.IdDespachoDet, dbo.trans_despacho_det.IdPedidoEnc, dbo.producto.IdProducto, dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_despacho_det.Codigo AS Codigo_Producto, 
                  dbo.producto.nombre AS Nombre_Producto, dbo.unidad_medida.Nombre AS UM, dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_despacho_det.Fecha, dbo.trans_despacho_det.NombreEstado, 
                  dbo.producto_presentacion.factor, dbo.producto_estado.nombre AS Estado, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence AS Vence, ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0) AS cantidad_pickeada, 
                  ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0) AS cantidad_verificada, ISNULL(dbo.trans_picking_ubic.peso_recibido, 0) AS Peso_Pickeado, ISNULL(dbo.trans_picking_ubic.peso_verificado, 0) AS Peso_Verificado, 
                  dbo.trans_despacho_det.CantidadDespachada, dbo.trans_despacho_det.PesoDespachado, ISNULL(dbo.trans_picking_ubic.encontrado, 0) AS Encontrado, ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, 
                  dbo.trans_pe_enc.no_documento AS No_Documento_WMS, dbo.trans_pe_enc.referencia AS No_Referencia, dbo.cliente.codigo AS Codigo_Cliente, dbo.cliente.nombre_comercial AS Nombre_Cliente, dbo.cliente.idubicacionvirtual, 
                  dbo.trans_despacho_enc.no_pase, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.trans_despacho_enc.numero, dbo.trans_despacho_enc.marchamo, dbo.trans_despacho_enc.observacion, 
                  dbo.trans_picking_ubic.IdUbicacion AS Ubicacion_Origen, dbo.road_ruta.CODIGO AS Codigo_Ruta, dbo.road_ruta.NOMBRE AS Nombre_Ruta, dbo.empresa_transporte_vehiculos.placa AS Placa_Vehiculo, 
                  dbo.empresa_transporte_vehiculos.placa_comercial, dbo.empresa_transporte_vehiculos.marca AS Marca_Vehiculo, dbo.empresa_transporte_vehiculos.modelo AS Modelo_Vehiculo, 
                  dbo.empresa_transporte_pilotos.nombres AS Nombre_Piloto, dbo.empresa_transporte_pilotos.apellidos AS Apellido_Piloto, dbo.empresa_transporte_pilotos.no_carnet AS No_Carnet_Piloto, 
                  dbo.empresa_transporte_pilotos.no_licencia AS No_Licencia_Piloto, dbo.trans_picking_ubic.lic_plate AS Licencia, dbo.producto_parametro_a.Nombre AS parametro_a, dbo.producto_parametro_b.Nombre AS parametro_b, 
                  dbo.producto_marca.nombre AS marca, dbo.producto_clasificacion.nombre AS clasificacion, dbo.producto_familia.nombre AS familia, pe_pol.numero_orden AS numero_orden_pedido, pe_pol.codigo_poliza AS codigo_poliza_pedido, 
                  oc_pol.codigo_poliza AS codigo_poliza_ingreso, oc_pol.numero_orden AS numero_orden_ingreso, dbo.regimen_fiscal.codigo_regimen AS codigo_regimen_salida, vehiculo.placa AS placa_contenedor_salida, 
                  oc_pol.activo AS poliza_activa, pe_pol.activo AS poliza_salida_activa, pe_pol.dua AS dua_salida
FROM     dbo.trans_pe_enc INNER JOIN
                  dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente INNER JOIN
                  dbo.trans_despacho_enc INNER JOIN
                  dbo.trans_despacho_det ON dbo.trans_despacho_enc.IdDespachoEnc = dbo.trans_despacho_det.IdDespachoEnc INNER JOIN
                  dbo.producto_bodega ON dbo.trans_despacho_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND dbo.trans_despacho_enc.IdBodega = dbo.producto_bodega.IdBodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.propietario_bodega ON dbo.trans_despacho_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                  dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario ON dbo.trans_pe_enc.IdPedidoEnc = dbo.trans_despacho_det.IdPedidoEnc LEFT OUTER JOIN
                  dbo.producto_parametro_a ON dbo.producto.IDPRODUCTOPARAMETROA = dbo.producto_parametro_a.IdProductoParametroA LEFT OUTER JOIN
                  dbo.producto_parametro_b ON dbo.producto.IDPRODUCTOPARAMETROB = dbo.producto_parametro_b.IdProductoParametroB LEFT OUTER JOIN
                  dbo.producto_marca ON dbo.producto.IdMarca = dbo.producto_marca.IdMarca AND dbo.propietarios.IdPropietario = dbo.producto_marca.IdPropietario LEFT OUTER JOIN
                  dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion AND dbo.propietarios.IdPropietario = dbo.producto_clasificacion.IdPropietario LEFT OUTER JOIN
                  dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia AND dbo.propietarios.IdPropietario = dbo.producto_familia.IdPropietario LEFT OUTER JOIN
                  dbo.empresa_transporte_vehiculos ON dbo.trans_despacho_enc.IdVehiculo = dbo.empresa_transporte_vehiculos.IdVehiculo LEFT OUTER JOIN
                  dbo.empresa_transporte_pilotos ON dbo.trans_despacho_enc.IdPiloto = dbo.empresa_transporte_pilotos.IdPiloto LEFT OUTER JOIN
                  dbo.road_ruta ON dbo.trans_despacho_enc.IdRuta = dbo.road_ruta.IdRuta LEFT OUTER JOIN
                  dbo.producto_estado INNER JOIN
                  dbo.trans_picking_ubic ON dbo.producto_estado.IdEstado = dbo.trans_picking_ubic.IdProductoEstado INNER JOIN
                  dbo.bodega_ubicacion ON dbo.trans_picking_ubic.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.trans_picking_ubic.IdBodega = dbo.bodega_ubicacion.IdBodega ON 
                  dbo.trans_despacho_det.IdUnidadMedidaBasica = dbo.trans_picking_ubic.IdUnidadMedida AND dbo.trans_despacho_det.IdPickingUbic = dbo.trans_picking_ubic.IdPickingUbic AND 
                  dbo.producto_bodega.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega AND dbo.trans_despacho_enc.IdPropietarioBodega = dbo.trans_picking_ubic.IdPropietarioBodega AND 
                  dbo.trans_despacho_det.IdPresentacion = dbo.trans_picking_ubic.IdPresentacion LEFT OUTER JOIN
                  dbo.unidad_medida ON dbo.trans_despacho_det.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.trans_despacho_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
                  dbo.trans_pe_pol AS pe_pol ON dbo.trans_pe_enc.IdPedidoEnc = pe_pol.IdOrdenPedidoEnc LEFT OUTER JOIN
                  dbo.trans_re_oc AS re_oc ON dbo.trans_picking_ubic.IdRecepcion = re_oc.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.trans_oc_pol AS oc_pol ON re_oc.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.regimen_fiscal ON pe_pol.IdRegimen = dbo.regimen_fiscal.IdRegimen LEFT OUTER JOIN
                  dbo.empresa_transporte_vehiculos AS vehiculo ON dbo.trans_despacho_enc.IdVehiculo = vehiculo.IdVehiculo
```
