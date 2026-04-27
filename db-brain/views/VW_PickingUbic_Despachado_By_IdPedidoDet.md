---
id: db-brain-view-vw-pickingubic-despachado-by-idpedidodet
type: db-view
title: dbo.VW_PickingUbic_Despachado_By_IdPedidoDet
schema: dbo
name: VW_PickingUbic_Despachado_By_IdPedidoDet
kind: view
modify_date: 2022-05-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PickingUbic_Despachado_By_IdPedidoDet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-10 |
| Columnas | 52 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDespachoEnc` | `int` |  |  |
| 2 | `IdPedidoEnc` | `int` |  |  |
| 3 | `IdPedidoDet` | `int` | ✓ |  |
| 4 | `IdPickingEnc` | `int` |  |  |
| 5 | `IdPickingUbic` | `int` |  |  |
| 6 | `codigo` | `nvarchar(50)` | ✓ |  |
| 7 | `nombre` | `nvarchar(100)` | ✓ |  |
| 8 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 9 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 10 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 11 | `Propietario` | `nvarchar(100)` |  |  |
| 12 | `cantidad_solicitada` | `float` | ✓ |  |
| 13 | `IdStock` | `int` | ✓ |  |
| 14 | `IdProductoBodega` | `int` | ✓ |  |
| 15 | `IdUbicacion` | `int` | ✓ |  |
| 16 | `IdProductoEstado` | `int` | ✓ |  |
| 17 | `IdPresentacion` | `int` | ✓ |  |
| 18 | `IdUnidadMedida` | `int` | ✓ |  |
| 19 | `lote` | `nvarchar(35)` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 22 | `serial` | `nvarchar(35)` | ✓ |  |
| 23 | `peso_solicitado` | `float` | ✓ |  |
| 24 | `cantidad_recibida` | `float` | ✓ |  |
| 25 | `cantidad_verificada` | `float` | ✓ |  |
| 26 | `cantidad_despachada` | `float` | ✓ |  |
| 27 | `peso_recibido` | `float` | ✓ |  |
| 28 | `peso_verificado` | `float` | ✓ |  |
| 29 | `peso_despachado` | `float` | ✓ |  |
| 30 | `acepto` | `bit` | ✓ |  |
| 31 | `encontrado` | `bit` | ✓ |  |
| 32 | `dañado_verificacion` | `bit` | ✓ |  |
| 33 | `IdPickingDet` | `int` |  |  |
| 34 | `IdPropietarioBodega` | `int` | ✓ |  |
| 35 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 36 | `IdRecepcion` | `bigint` | ✓ |  |
| 37 | `fecha_minima` | `datetime` | ✓ |  |
| 38 | `fecha_real_vence` | `datetime` | ✓ |  |
| 39 | `no_packing` | `nvarchar(50)` | ✓ |  |
| 40 | `fecha_picking` | `datetime` | ✓ |  |
| 41 | `fecha_verificado` | `datetime` | ✓ |  |
| 42 | `fecha_packing` | `datetime` | ✓ |  |
| 43 | `fecha_despachado` | `datetime` | ✓ |  |
| 44 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 45 | `fec_agr` | `datetime` | ✓ |  |
| 46 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 47 | `fec_mod` | `datetime` | ✓ |  |
| 48 | `activo` | `bit` | ✓ |  |
| 49 | `dañado_picking` | `bit` | ✓ |  |
| 50 | `Nombre_Ubicacion` | `nvarchar(200)` | ✓ |  |
| 51 | `no_encontrado` | `bit` |  |  |
| 52 | `IdUbicacionTemporal` | `int` | ✓ |  |

## Consume

- `bodega_area`
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
- `trans_despacho_det`
- `trans_pe_det`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_PickingUbic_Despachado_By_IdPedidoDet]
AS
SELECT        dbo.trans_despacho_det.IdDespachoEnc, dbo.trans_pe_det.IdPedidoEnc, dbo.trans_picking_ubic.IdPedidoDet, dbo.trans_picking_ubic.IdPickingEnc, dbo.trans_picking_ubic.IdPickingUbic, dbo.producto.codigo, 
                         dbo.producto.nombre, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_estado.nombre AS NomEstado, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.propietarios.nombre_comercial AS Propietario, 
                         dbo.trans_picking_ubic.cantidad_solicitada, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdProductoBodega, dbo.trans_picking_ubic.IdUbicacion, dbo.trans_picking_ubic.IdProductoEstado, 
                         dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence, dbo.trans_picking_ubic.lic_plate, dbo.trans_picking_ubic.serial, 
                         dbo.trans_picking_ubic.peso_solicitado, dbo.trans_picking_ubic.cantidad_recibida, dbo.trans_picking_ubic.cantidad_verificada, dbo.trans_picking_ubic.cantidad_despachada, dbo.trans_picking_ubic.peso_recibido, 
                         dbo.trans_picking_ubic.peso_verificado, dbo.trans_picking_ubic.peso_despachado, dbo.trans_picking_ubic.acepto, dbo.trans_picking_ubic.encontrado, dbo.trans_picking_ubic.dañado_verificacion, 
                         dbo.trans_picking_ubic.IdPickingDet, dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdUbicacionAnterior, dbo.trans_picking_ubic.IdRecepcion, dbo.trans_picking_ubic.fecha_minima, 
                         dbo.trans_picking_ubic.fecha_real_vence, dbo.trans_picking_ubic.no_packing, dbo.trans_picking_ubic.fecha_picking, dbo.trans_picking_ubic.fecha_verificado, dbo.trans_picking_ubic.fecha_packing, 
                         dbo.trans_picking_ubic.fecha_despachado, dbo.trans_picking_ubic.user_agr, dbo.trans_picking_ubic.fec_agr, dbo.trans_picking_ubic.user_mod, dbo.trans_picking_ubic.fec_mod, dbo.trans_picking_ubic.activo, 
                         dbo.trans_picking_ubic.dañado_picking, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Nombre_Ubicacion, dbo.trans_picking_ubic.no_encontrado, 
                         dbo.trans_picking_ubic.IdUbicacionTemporal
FROM            dbo.trans_pe_det INNER JOIN
                         dbo.producto INNER JOIN
                         dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto ON dbo.trans_pe_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto_estado INNER JOIN
                         dbo.trans_picking_ubic ON dbo.producto_estado.IdEstado = dbo.trans_picking_ubic.IdProductoEstado INNER JOIN
                         dbo.propietario_bodega ON dbo.trans_picking_ubic.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                         dbo.trans_despacho_det ON dbo.trans_picking_ubic.IdPickingUbic = dbo.trans_despacho_det.IdPickingUbic AND dbo.trans_picking_ubic.IdPedidoDet = dbo.trans_despacho_det.IdPedidoDet AND 
                         dbo.trans_picking_ubic.IdProductoBodega = dbo.trans_despacho_det.IdProductoBodega AND dbo.trans_picking_ubic.IdUnidadMedida = dbo.trans_despacho_det.IdUnidadMedidaBasica AND 
                         dbo.trans_picking_ubic.IdPresentacion = dbo.trans_despacho_det.IdPresentacion ON dbo.trans_pe_det.IdPedidoDet = dbo.trans_picking_ubic.IdPedidoDet AND 
                         dbo.trans_pe_det.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega AND dbo.trans_pe_det.IdUnidadMedidaBasica = dbo.trans_picking_ubic.IdUnidadMedida AND 
                         dbo.trans_pe_det.IdEstado = dbo.trans_picking_ubic.IdProductoEstado INNER JOIN
                         dbo.unidad_medida ON dbo.trans_pe_det.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                         dbo.bodega_ubicacion ON dbo.trans_picking_ubic.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.trans_picking_ubic.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_pe_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion
```
