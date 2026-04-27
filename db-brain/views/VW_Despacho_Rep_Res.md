---
id: db-brain-view-vw-despacho-rep-res
type: db-view
title: dbo.VW_Despacho_Rep_Res
schema: dbo
name: VW_Despacho_Rep_Res
kind: view
modify_date: 2022-04-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Despacho_Rep_Res`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-04-27 |
| Columnas | 51 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPropietarioBodega` | `int` | ✓ |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `IdProductoEstado` | `int` | ✓ |  |
| 4 | `IdPresentacion` | `int` | ✓ |  |
| 5 | `IdUnidadMedida` | `int` | ✓ |  |
| 6 | `IdDespachoEnc` | `int` |  |  |
| 7 | `IdPedidoEnc` | `int` | ✓ |  |
| 8 | `Propietario` | `nvarchar(100)` |  |  |
| 9 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 10 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 11 | `UM` | `nvarchar(50)` | ✓ |  |
| 12 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 13 | `factor` | `float` | ✓ |  |
| 14 | `Estado` | `nvarchar(50)` | ✓ |  |
| 15 | `lote` | `nvarchar(35)` | ✓ |  |
| 16 | `Vence` | `datetime` | ✓ |  |
| 17 | `cantidad_pickeada` | `float` | ✓ |  |
| 18 | `cantidad_verificada` | `float` | ✓ |  |
| 19 | `Peso_Pickeado` | `float` | ✓ |  |
| 20 | `Peso_Verificado` | `float` | ✓ |  |
| 21 | `CantidadDespachada` | `float` | ✓ |  |
| 22 | `PesoDespachado` | `float` | ✓ |  |
| 23 | `Encontrado` | `bit` |  |  |
| 24 | `Acepto` | `bit` |  |  |
| 25 | `No_Documento_WMS` | `bigint` | ✓ |  |
| 26 | `No_Referencia` | `nvarchar(25)` | ✓ |  |
| 27 | `Codigo_Cliente` | `nvarchar(150)` | ✓ |  |
| 28 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 29 | `idubicacionvirtual` | `int` | ✓ |  |
| 30 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 31 | `es_bodega_traslado` | `bit` | ✓ |  |
| 32 | `no_pase` | `int` | ✓ |  |
| 33 | `observacion` | `nvarchar(500)` | ✓ |  |
| 34 | `numero` | `int` | ✓ |  |
| 35 | `marchamo` | `nvarchar(50)` | ✓ |  |
| 36 | `Codigo_Ruta` | `nvarchar(15)` | ✓ |  |
| 37 | `Nombre_Ruta` | `nvarchar(50)` | ✓ |  |
| 38 | `Placa_Vehiculo` | `nvarchar(20)` | ✓ |  |
| 39 | `placa_comercial` | `nvarchar(50)` | ✓ |  |
| 40 | `Marca_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 41 | `Modelo_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 42 | `Nombre_Piloto` | `nvarchar(150)` | ✓ |  |
| 43 | `Apellido_Piloto` | `nvarchar(150)` | ✓ |  |
| 44 | `No_Carnet_Piloto` | `nvarchar(50)` | ✓ |  |
| 45 | `No_Licencia_Piloto` | `nvarchar(50)` | ✓ |  |
| 46 | `Fecha_Despacho` | `datetime` | ✓ |  |
| 47 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 48 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 49 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 50 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 51 | `IdPickingEnc` | `int` |  |  |

## Consume

- `cliente`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `road_ruta`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Despacho_Rep_Res]
AS
SELECT dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida,
dbo.trans_despacho_det.IdDespachoEnc, dbo.trans_despacho_det.IdPedidoEnc, pr.nombre_comercial AS Propietario, p.codigo AS Codigo_Producto, p.nombre AS Nombre_Producto, um.Nombre AS UM,
pp.nombre AS Presentacion, pp.factor, pe.nombre AS Estado, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence AS Vence, SUM(ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0)) AS cantidad_pickeada,
SUM(ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0)) AS cantidad_verificada, SUM(ISNULL(dbo.trans_picking_ubic.peso_recibido, 0)) AS Peso_Pickeado, SUM(ISNULL(dbo.trans_picking_ubic.peso_verificado, 0))
AS Peso_Verificado, SUM(dbo.trans_despacho_det.CantidadDespachada) AS CantidadDespachada, SUM(dbo.trans_despacho_det.PesoDespachado) AS PesoDespachado, ISNULL(dbo.trans_picking_ubic.encontrado, 0)
AS Encontrado, ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, dbo.trans_pe_enc.no_documento AS No_Documento_WMS, dbo.trans_pe_enc.referencia AS No_Referencia, dbo.cliente.codigo AS Codigo_Cliente,
dbo.cliente.nombre_comercial AS Nombre_Cliente, dbo.cliente.idubicacionvirtual, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.trans_despacho_enc.no_pase, dbo.trans_despacho_enc.observacion,
dbo.trans_despacho_enc.numero, dbo.trans_despacho_enc.marchamo, dbo.road_ruta.CODIGO AS Codigo_Ruta, dbo.road_ruta.NOMBRE AS Nombre_Ruta, dbo.empresa_transporte_vehiculos.placa AS Placa_Vehiculo,
dbo.empresa_transporte_vehiculos.placa_comercial, dbo.empresa_transporte_vehiculos.marca AS Marca_Vehiculo, dbo.empresa_transporte_vehiculos.modelo AS Modelo_Vehiculo,
dbo.empresa_transporte_pilotos.nombres AS Nombre_Piloto, dbo.empresa_transporte_pilotos.apellidos AS Apellido_Piloto, dbo.empresa_transporte_pilotos.no_carnet AS No_Carnet_Piloto,
dbo.empresa_transporte_pilotos.no_licencia AS No_Licencia_Piloto, dbo.trans_despacho_enc.fecha AS Fecha_Despacho, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.trans_picking_ubic.lic_plate,
dbo.trans_picking_ubic.IdOperadorBodega_Pickeo, dbo.trans_picking_ubic.IdOperadorBodega_Verifico, dbo.trans_picking_ubic.IdPickingEnc
FROM dbo.trans_pe_enc INNER JOIN
dbo.producto AS p INNER JOIN
dbo.producto_bodega AS pb ON p.IdProducto = pb.IdProducto INNER JOIN
dbo.trans_picking_det INNER JOIN
dbo.trans_picking_ubic ON dbo.trans_picking_det.IdPickingDet = dbo.trans_picking_ubic.IdPickingDet AND dbo.trans_picking_det.IdPedidoDet = dbo.trans_picking_ubic.IdPedidoDet ON
pb.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega INNER JOIN
dbo.producto_estado AS pe ON dbo.trans_picking_ubic.IdProductoEstado = pe.IdEstado INNER JOIN
dbo.unidad_medida AS um ON dbo.trans_picking_ubic.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
dbo.trans_despacho_det ON dbo.trans_picking_ubic.IdPickingUbic = dbo.trans_despacho_det.IdPickingUbic AND dbo.trans_picking_ubic.IdProductoBodega = dbo.trans_despacho_det.IdProductoBodega AND
dbo.trans_picking_ubic.IdUnidadMedida = dbo.trans_despacho_det.IdUnidadMedidaBasica ON dbo.trans_pe_enc.IdPedidoEnc = dbo.trans_despacho_det.IdPedidoEnc INNER JOIN
dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente INNER JOIN
dbo.trans_despacho_enc ON dbo.trans_despacho_det.IdDespachoEnc = dbo.trans_despacho_enc.IdDespachoEnc INNER JOIN
dbo.propietario_bodega ON dbo.trans_pe_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND dbo.trans_despacho_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
dbo.propietarios AS pr ON dbo.propietario_bodega.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
dbo.producto_clasificacion ON p.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
dbo.empresa_transporte_vehiculos ON dbo.trans_despacho_enc.IdVehiculo = dbo.empresa_transporte_vehiculos.IdVehiculo LEFT OUTER JOIN
dbo.road_ruta ON dbo.trans_despacho_enc.IdRuta = dbo.road_ruta.IdRuta LEFT OUTER JOIN
dbo.empresa_transporte_pilotos ON dbo.trans_despacho_enc.IdPiloto = dbo.empresa_transporte_pilotos.IdPiloto LEFT OUTER JOIN
dbo.producto_presentacion AS pp ON dbo.trans_picking_ubic.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto
GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial, pp.factor, dbo.trans_picking_ubic.peso_recibido, dbo.trans_picking_ubic.peso_verificado, dbo.trans_picking_ubic.acepto,
dbo.trans_picking_ubic.encontrado, dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion,
dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_despacho_det.IdDespachoEnc, dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.referencia, dbo.cliente.codigo, dbo.cliente.nombre_comercial,
dbo.cliente.idubicacionvirtual, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence, dbo.trans_despacho_det.IdPedidoEnc,
dbo.trans_despacho_enc.no_pase, dbo.trans_despacho_enc.observacion, dbo.trans_despacho_enc.numero, dbo.trans_despacho_enc.marchamo, dbo.road_ruta.CODIGO, dbo.road_ruta.NOMBRE,
dbo.empresa_transporte_vehiculos.placa, dbo.empresa_transporte_vehiculos.marca, dbo.empresa_transporte_vehiculos.modelo, dbo.empresa_transporte_vehiculos.placa_comercial,
dbo.empresa_transporte_pilotos.nombres, dbo.empresa_transporte_pilotos.apellidos, dbo.empresa_transporte_pilotos.no_carnet, dbo.empresa_transporte_pilotos.no_licencia, dbo.trans_despacho_enc.fecha,
dbo.producto_clasificacion.nombre, dbo.trans_picking_ubic.lic_plate, dbo.trans_picking_ubic.IdOperadorBodega_Pickeo, dbo.trans_picking_ubic.IdOperadorBodega_Verifico, dbo.trans_picking_ubic.IdPickingEnc
```
