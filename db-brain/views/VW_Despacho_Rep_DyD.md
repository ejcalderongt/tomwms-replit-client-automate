---
id: db-brain-view-vw-despacho-rep-dyd
type: db-view
title: dbo.VW_Despacho_Rep_DyD
schema: dbo
name: VW_Despacho_Rep_DyD
kind: view
modify_date: 2023-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Despacho_Rep_DyD`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-01-11 |
| Columnas | 59 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbic` | `int` |  |  |
| 2 | `IdStock` | `int` | ✓ |  |
| 3 | `IdPedidoDet` | `int` | ✓ |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `IdRecepcion` | `bigint` | ✓ |  |
| 10 | `IdDespachoEnc` | `int` |  |  |
| 11 | `IdDespachoDet` | `int` |  |  |
| 12 | `IdPedidoEnc` | `int` | ✓ |  |
| 13 | `Propietario` | `nvarchar(100)` |  |  |
| 14 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 15 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 16 | `UM` | `nvarchar(50)` | ✓ |  |
| 17 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `factor` | `float` | ✓ |  |
| 19 | `Estado` | `nvarchar(50)` | ✓ |  |
| 20 | `lote` | `nvarchar(50)` | ✓ |  |
| 21 | `Vence` | `datetime` | ✓ |  |
| 22 | `Ubicacion_Origen` | `nvarchar(50)` | ✓ |  |
| 23 | `cantidad_pickeada` | `float` |  |  |
| 24 | `cantidad_verificada` | `float` |  |  |
| 25 | `Peso_Pickeado` | `float` |  |  |
| 26 | `Peso_Verificado` | `float` |  |  |
| 27 | `CantidadDespachada` | `float` | ✓ |  |
| 28 | `PesoDespachado` | `float` | ✓ |  |
| 29 | `Encontrado` | `bit` |  |  |
| 30 | `Acepto` | `bit` |  |  |
| 31 | `No_Documento_WMS` | `bigint` | ✓ |  |
| 32 | `No_Referencia` | `nvarchar(25)` | ✓ |  |
| 33 | `Codigo_Cliente` | `nvarchar(150)` | ✓ |  |
| 34 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 35 | `idubicacionvirtual` | `int` | ✓ |  |
| 36 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 37 | `es_bodega_traslado` | `bit` | ✓ |  |
| 38 | `no_pase` | `int` | ✓ |  |
| 39 | `observacion` | `nvarchar(500)` | ✓ |  |
| 40 | `numero` | `int` | ✓ |  |
| 41 | `marchamo` | `nvarchar(50)` | ✓ |  |
| 42 | `Codigo_Ruta` | `nvarchar(15)` | ✓ |  |
| 43 | `Nombre_Ruta` | `nvarchar(50)` | ✓ |  |
| 44 | `Placa_Vehiculo` | `nvarchar(20)` | ✓ |  |
| 45 | `placa_comercial` | `nvarchar(50)` | ✓ |  |
| 46 | `Marca_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 47 | `Modelo_Vehiculo` | `nvarchar(50)` | ✓ |  |
| 48 | `Nombre_Piloto` | `nvarchar(150)` | ✓ |  |
| 49 | `Apellido_Piloto` | `nvarchar(150)` | ✓ |  |
| 50 | `No_Carnet_Piloto` | `nvarchar(50)` | ✓ |  |
| 51 | `No_Licencia_Piloto` | `nvarchar(50)` | ✓ |  |
| 52 | `fecha` | `datetime` | ✓ |  |
| 53 | `Marca` | `nvarchar(50)` | ✓ |  |
| 54 | `Linea` | `nvarchar(50)` | ✓ |  |
| 55 | `Modelo` | `nvarchar(50)` | ✓ |  |
| 56 | `Repuesto` | `nvarchar(50)` | ✓ |  |
| 57 | `TipoRepuesto` | `nvarchar(50)` | ✓ |  |
| 58 | `Nuevo_Usado` | `nvarchar(50)` | ✓ |  |
| 59 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |

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
- `producto_tipo`
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
CREATE VIEW [dbo].[VW_Despacho_Rep_DyD]
AS
SELECT dbo.trans_picking_ubic.IdPickingUbic, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdPedidoDet, dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, 
                  dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_picking_ubic.IdRecepcion, dbo.trans_despacho_det.IdDespachoEnc, 
                  dbo.trans_despacho_det.IdDespachoDet, dbo.trans_despacho_det.IdPedidoEnc, pr.nombre_comercial AS Propietario, p.codigo AS Codigo_Producto, p.nombre AS Nombre_Producto, um.Nombre AS UM, pp.nombre AS Presentacion, 
                  pp.factor, pe.nombre AS Estado, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence AS Vence, bu.descripcion AS Ubicacion_Origen, ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0) AS cantidad_pickeada, 
                  ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0) AS cantidad_verificada, ISNULL(dbo.trans_picking_ubic.peso_recibido, 0) AS Peso_Pickeado, ISNULL(dbo.trans_picking_ubic.peso_verificado, 0) AS Peso_Verificado, 
                  dbo.trans_despacho_det.CantidadDespachada, dbo.trans_despacho_det.PesoDespachado, ISNULL(dbo.trans_picking_ubic.encontrado, 0) AS Encontrado, ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, 
                  dbo.trans_pe_enc.no_documento AS No_Documento_WMS, dbo.trans_pe_enc.referencia AS No_Referencia, dbo.cliente.codigo AS Codigo_Cliente, dbo.cliente.nombre_comercial AS Nombre_Cliente, dbo.cliente.idubicacionvirtual, 
                  dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.trans_despacho_enc.no_pase, dbo.trans_despacho_enc.observacion, dbo.trans_despacho_enc.numero, dbo.trans_despacho_enc.marchamo, 
                  dbo.road_ruta.CODIGO AS Codigo_Ruta, dbo.road_ruta.NOMBRE AS Nombre_Ruta, dbo.empresa_transporte_vehiculos.placa AS Placa_Vehiculo, dbo.empresa_transporte_vehiculos.placa_comercial, 
                  dbo.empresa_transporte_vehiculos.marca AS Marca_Vehiculo, dbo.empresa_transporte_vehiculos.modelo AS Modelo_Vehiculo, dbo.empresa_transporte_pilotos.nombres AS Nombre_Piloto, 
                  dbo.empresa_transporte_pilotos.apellidos AS Apellido_Piloto, dbo.empresa_transporte_pilotos.no_carnet AS No_Carnet_Piloto, dbo.empresa_transporte_pilotos.no_licencia AS No_Licencia_Piloto, dbo.trans_despacho_enc.fecha, 
                  pm.nombre AS Marca, pf.nombre AS Linea, pc.nombre AS Modelo, pt.NombreTipoProducto AS Repuesto, pmb.Nombre AS TipoRepuesto, pa.Nombre AS Nuevo_Usado, dbo.trans_picking_ubic.IdOperadorBodega_Pickeo
FROM     dbo.trans_despacho_det INNER JOIN
                  dbo.bodega_ubicacion AS bu INNER JOIN
                  dbo.producto AS p INNER JOIN
                  dbo.producto_bodega AS pb ON p.IdProducto = pb.IdProducto INNER JOIN
                  dbo.propietario_bodega AS prb ON p.IdPropietario = prb.IdPropietario INNER JOIN
                  dbo.producto_estado AS pe ON prb.IdPropietario = pe.IdPropietario INNER JOIN
                  dbo.unidad_medida AS um ON prb.IdPropietario = um.IdPropietario INNER JOIN
                  dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                  dbo.trans_picking_det INNER JOIN
                  dbo.trans_picking_ubic ON dbo.trans_picking_det.IdPickingDet = dbo.trans_picking_ubic.IdPickingDet AND dbo.trans_picking_det.IdPedidoDet = dbo.trans_picking_ubic.IdPedidoDet ON 
                  prb.IdPropietarioBodega = dbo.trans_picking_ubic.IdPropietarioBodega AND pb.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega AND pe.IdEstado = dbo.trans_picking_ubic.IdProductoEstado AND 
                  um.IdUnidadMedida = dbo.trans_picking_ubic.IdUnidadMedida ON bu.IdUbicacion = dbo.trans_picking_ubic.IdUbicacion ON dbo.trans_despacho_det.IdPickingUbic = dbo.trans_picking_ubic.IdPickingUbic AND 
                  dbo.trans_despacho_det.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega AND dbo.trans_despacho_det.IdUnidadMedidaBasica = dbo.trans_picking_ubic.IdUnidadMedida INNER JOIN
                  dbo.trans_pe_enc ON prb.IdPropietarioBodega = dbo.trans_pe_enc.IdPropietarioBodega AND dbo.trans_despacho_det.IdPedidoEnc = dbo.trans_pe_enc.IdPedidoEnc INNER JOIN
                  dbo.cliente ON pr.IdPropietario = dbo.cliente.IdPropietario AND dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente INNER JOIN
                  dbo.trans_despacho_enc ON dbo.trans_despacho_det.IdDespachoEnc = dbo.trans_despacho_enc.IdDespachoEnc AND prb.IdPropietarioBodega = dbo.trans_despacho_enc.IdPropietarioBodega LEFT OUTER JOIN
                  dbo.empresa_transporte_vehiculos ON dbo.trans_despacho_enc.IdVehiculo = dbo.empresa_transporte_vehiculos.IdVehiculo LEFT OUTER JOIN
                  dbo.road_ruta ON dbo.trans_despacho_enc.IdRuta = dbo.road_ruta.IdRuta LEFT OUTER JOIN
                  dbo.empresa_transporte_pilotos ON dbo.trans_despacho_enc.IdPiloto = dbo.empresa_transporte_pilotos.IdPiloto LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON dbo.trans_picking_ubic.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                  dbo.producto_marca AS pm ON pm.IdMarca = p.IdMarca AND pm.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_familia AS pf ON pf.IdFamilia = p.IdFamilia AND pf.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_clasificacion AS pc ON pc.IdClasificacion = p.IdClasificacion AND pc.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_tipo AS pt ON pt.IdTipoProducto = p.IdTipoProducto AND pt.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_parametro_a AS pa ON pa.IdProductoParametroA = p.IDPRODUCTOPARAMETROA LEFT OUTER JOIN
                  dbo.producto_parametro_b AS pmb ON pmb.IdProductoParametroB = p.IDPRODUCTOPARAMETROB
GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial, bu.descripcion, pp.factor, dbo.trans_picking_ubic.peso_recibido, dbo.trans_picking_ubic.peso_verificado, dbo.trans_picking_ubic.acepto, 
                  dbo.trans_picking_ubic.cantidad_recibida, dbo.trans_picking_ubic.cantidad_verificada, dbo.trans_picking_ubic.encontrado, dbo.trans_picking_ubic.IdPickingUbic, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdPedidoDet, 
                  dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, 
                  dbo.trans_picking_ubic.IdRecepcion, dbo.trans_despacho_det.IdDespachoEnc, dbo.trans_despacho_det.IdDespachoDet, dbo.trans_despacho_det.CantidadDespachada, dbo.trans_despacho_det.PesoDespachado, 
                  dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.referencia, dbo.cliente.codigo, dbo.cliente.nombre_comercial, dbo.cliente.idubicacionvirtual, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, 
                  dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence, dbo.trans_despacho_det.IdPedidoEnc, dbo.trans_despacho_enc.no_pase, dbo.trans_despacho_enc.observacion, dbo.trans_despacho_enc.numero, 
                  dbo.trans_despacho_enc.marchamo, dbo.road_ruta.CODIGO, dbo.road_ruta.NOMBRE, dbo.empresa_transporte_vehiculos.placa, dbo.empresa_transporte_vehiculos.marca, dbo.empresa_transporte_vehiculos.modelo, 
                  dbo.empresa_transporte_vehiculos.placa_comercial, dbo.empresa_transporte_pilotos.nombres, dbo.empresa_transporte_pilotos.apellidos, dbo.empresa_transporte_pilotos.no_carnet, dbo.empresa_transporte_pilotos.no_licencia, 
                  dbo.trans_despacho_enc.fecha, pm.nombre, pf.nombre, pc.nombre, pt.NombreTipoProducto, pmb.Nombre, pa.Nombre, dbo.trans_picking_ubic.IdOperadorBodega_Pickeo
```
