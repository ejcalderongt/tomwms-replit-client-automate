---
id: db-brain-view-vw-pickingubic-by-idpedidodet
type: db-view
title: dbo.VW_PickingUbic_By_IdPedidoDet
schema: dbo
name: VW_PickingUbic_By_IdPedidoDet
kind: view
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PickingUbic_By_IdPedidoDet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-05 |
| Columnas | 65 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingEnc` | `int` |  |  |
| 2 | `IdStock` | `int` | ✓ |  |
| 3 | `IdPickingUbic` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdUbicacion` | `int` | ✓ |  |
| 6 | `IdPickingDet` | `int` |  |  |
| 7 | `IdProductoBodega` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 12 | `IdRecepcion` | `bigint` | ✓ |  |
| 13 | `peso_solicitado` | `float` | ✓ |  |
| 14 | `peso_recibido` | `float` | ✓ |  |
| 15 | `peso_verificado` | `float` | ✓ |  |
| 16 | `cantidad_solicitada` | `float` | ✓ |  |
| 17 | `cantidad_recibida` | `float` | ✓ |  |
| 18 | `cantidad_verificada` | `float` | ✓ |  |
| 19 | `cantidad_despachada` | `float` | ✓ |  |
| 20 | `fecha_real_vence` | `datetime` | ✓ |  |
| 21 | `no_packing` | `nvarchar(50)` | ✓ |  |
| 22 | `fecha_verificado` | `datetime` | ✓ |  |
| 23 | `fecha_despachado` | `datetime` | ✓ |  |
| 24 | `IdPedidoDet` | `int` | ✓ |  |
| 25 | `IdPedidoEnc` | `int` |  |  |
| 26 | `PDetIdPresentacion` | `int` | ✓ |  |
| 27 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 28 | `PDetIdProductoBodega` | `int` |  |  |
| 29 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 30 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 31 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 32 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 33 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 34 | `IdEstado` | `int` |  |  |
| 35 | `Peso` | `float` | ✓ |  |
| 36 | `Precio` | `float` | ✓ |  |
| 37 | `IdStockRes` | `int` | ✓ |  |
| 38 | `StockResIdStock` | `int` | ✓ |  |
| 39 | `peso_despachado` | `float` | ✓ |  |
| 40 | `lote` | `nvarchar(50)` | ✓ |  |
| 41 | `fecha_vence` | `datetime` | ✓ |  |
| 42 | `fecha_picking` | `datetime` | ✓ |  |
| 43 | `fecha_packing` | `datetime` | ✓ |  |
| 44 | `fecha_minima` | `datetime` | ✓ |  |
| 45 | `serial` | `nvarchar(35)` | ✓ |  |
| 46 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 47 | `acepto` | `bit` | ✓ |  |
| 48 | `encontrado` | `bit` | ✓ |  |
| 49 | `dañado_verificacion` | `bit` | ✓ |  |
| 50 | `activo` | `bit` | ✓ |  |
| 51 | `fec_mod` | `datetime` | ✓ |  |
| 52 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 53 | `fec_agr` | `datetime` | ✓ |  |
| 54 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 55 | `dañado_picking` | `bit` | ✓ |  |
| 56 | `lic_plate_reemplazo` | `nvarchar(50)` | ✓ |  |
| 57 | `IdUbicacion_reemplazo` | `int` | ✓ |  |
| 58 | `IdStock_reemplazo` | `int` | ✓ |  |
| 59 | `IdBodega` | `int` | ✓ |  |
| 60 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 61 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 62 | `Nombre_Ubicacion` | `nvarchar(200)` | ✓ |  |
| 63 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 64 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |
| 65 | `no_encontrado` | `bit` |  |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `stock_res`
- `trans_pe_det`
- `trans_picking_det`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_PickingUbic_By_IdPedidoDet]
AS
SELECT pu.IdPickingEnc, pu.IdStock, pu.IdPickingUbic, pu.IdPropietarioBodega, pu.IdUbicacion, pu.IdPickingDet, pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, pu.IdUbicacionAnterior, pu.IdRecepcion, 
                  pu.peso_solicitado, pu.peso_recibido, pu.peso_verificado, pu.cantidad_solicitada, pu.cantidad_recibida, pu.cantidad_verificada, pu.cantidad_despachada, pu.fecha_real_vence, pu.no_packing, pu.fecha_verificado, pu.fecha_despachado, 
                  pu.IdPedidoDet, pdet.IdPedidoEnc, pdet.IdPresentacion AS PDetIdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega AS PDetIdProductoBodega, pdet.codigo_producto, pdet.nombre_producto, pdet.nom_presentacion, 
                  pdet.nom_unid_med, pdet.nom_estado, pdet.IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes, sr.IdStock AS StockResIdStock, pu.peso_despachado, pu.lote, pu.fecha_vence, pu.fecha_picking, pu.fecha_packing, pu.fecha_minima, pu.serial, 
                  pu.lic_plate, pu.acepto, pu.encontrado, pu.dañado_verificacion, pu.activo, pu.fec_mod, pu.user_mod, pu.fec_agr, pu.user_agr, pu.dañado_picking, pu.lic_plate_reemplazo, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pu.IdBodega, 
                  pu.IdOperadorBodega_Pickeo, pu.IdOperadorBodega_Verifico, dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion, pu.IdBodega) AS Nombre_Ubicacion, pu.IdUbicacionTemporal, pu.IdOperadorBodega_Asignado, pu.no_encontrado
FROM     dbo.trans_picking_ubic AS pu INNER JOIN
                  dbo.trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet INNER JOIN
                  dbo.trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
                  dbo.bodega_ubicacion ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND pu.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                  dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                  dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                  dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega LEFT OUTER JOIN
                  dbo.stock_res AS sr ON pu.IdPropietarioBodega = sr.IdPropietarioBodega AND pu.IdStock = sr.IdStock AND pu.IdPedidoDet = sr.IdPedidoDet AND pu.IdProductoBodega = sr.IdProductoBodega AND 
                  pu.IdProductoEstado = sr.IdProductoEstado AND pu.IdUnidadMedida = sr.IdUnidadMedida AND pu.IdStockRes = sr.IdStockRes
```
