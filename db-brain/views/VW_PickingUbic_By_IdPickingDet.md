---
id: db-brain-view-vw-pickingubic-by-idpickingdet
type: db-view
title: dbo.VW_PickingUbic_By_IdPickingDet
schema: dbo
name: VW_PickingUbic_By_IdPickingDet
kind: view
modify_date: 2022-06-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PickingUbic_By_IdPickingDet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-07 |
| Columnas | 56 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 2 | `IdPickingUbic` | `int` |  |  |
| 3 | `IdPickingDet` | `int` |  |  |
| 4 | `IdUbicacion` | `int` | ✓ |  |
| 5 | `IdStock` | `int` | ✓ |  |
| 6 | `IdPropietarioBodega` | `int` | ✓ |  |
| 7 | `IdProductoBodega` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 12 | `IdRecepcion` | `bigint` | ✓ |  |
| 13 | `lote` | `nvarchar(35)` | ✓ |  |
| 14 | `fecha_vence` | `datetime` | ✓ |  |
| 15 | `fecha_minima` | `datetime` | ✓ |  |
| 16 | `serial` | `nvarchar(35)` | ✓ |  |
| 17 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 18 | `acepto` | `bit` | ✓ |  |
| 19 | `peso_solicitado` | `float` | ✓ |  |
| 20 | `peso_recibido` | `float` | ✓ |  |
| 21 | `peso_verificado` | `float` | ✓ |  |
| 22 | `peso_despachado` | `float` | ✓ |  |
| 23 | `cantidad_solicitada` | `float` | ✓ |  |
| 24 | `cantidad_recibida` | `float` | ✓ |  |
| 25 | `cantidad_verificada` | `float` | ✓ |  |
| 26 | `encontrado` | `bit` | ✓ |  |
| 27 | `dañado_verificacion` | `bit` | ✓ |  |
| 28 | `fecha_real_vence` | `datetime` | ✓ |  |
| 29 | `no_packing` | `nvarchar(50)` | ✓ |  |
| 30 | `fecha_picking` | `datetime` | ✓ |  |
| 31 | `fecha_verificado` | `datetime` | ✓ |  |
| 32 | `fecha_packing` | `datetime` | ✓ |  |
| 33 | `fecha_despachado` | `datetime` | ✓ |  |
| 34 | `cantidad_despachada` | `float` | ✓ |  |
| 35 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 36 | `fec_agr` | `datetime` | ✓ |  |
| 37 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 38 | `fec_mod` | `datetime` | ✓ |  |
| 39 | `activo` | `bit` | ✓ |  |
| 40 | `IdPedidoDet` | `int` | ✓ |  |
| 41 | `dañado_picking` | `bit` | ✓ |  |
| 42 | `IdStockRes` | `int` |  |  |
| 43 | `lic_plate_reemplazo` | `nvarchar(50)` | ✓ |  |
| 44 | `IdUbicacion_reemplazo` | `int` | ✓ |  |
| 45 | `IdStock_reemplazo` | `int` | ✓ |  |
| 46 | `IdBodega` | `int` | ✓ |  |
| 47 | `IdPickingEnc` | `int` |  |  |
| 48 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 49 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 50 | `Nombre_Operador_Pickeo` | `nvarchar(100)` | ✓ |  |
| 51 | `Nombre_Operador_Verifico` | `nvarchar(100)` | ✓ |  |
| 52 | `IdPedidoEnc` | `int` | ✓ |  |
| 53 | `no_encontrado` | `bit` |  |  |
| 54 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 55 | `NombreUbicacionTemporal` | `nvarchar(200)` | ✓ |  |
| 56 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
/*****************#GT07062022 campo IdOperadorBodega_Asignado *************************************************************/

CREATE VIEW [dbo].[VW_PickingUbic_By_IdPickingDet]
AS
SELECT bu.descripcion AS Ubicacion, u.IdPickingUbic, u.IdPickingDet, u.IdUbicacion, u.IdStock, u.IdPropietarioBodega, u.IdProductoBodega, u.IdProductoEstado, u.IdPresentacion, u.IdUnidadMedida, u.IdUbicacionAnterior, u.IdRecepcion, u.lote, 
                  u.fecha_vence, u.fecha_minima, u.serial, u.lic_plate, u.acepto, u.peso_solicitado, u.peso_recibido, u.peso_verificado, u.peso_despachado, u.cantidad_solicitada, u.cantidad_recibida, u.cantidad_verificada, u.encontrado, 
                  u.dañado_verificacion, u.fecha_real_vence, u.no_packing, u.fecha_picking, u.fecha_verificado, u.fecha_packing, u.fecha_despachado, u.cantidad_despachada, u.user_agr, u.fec_agr, u.user_mod, u.fec_mod, u.activo, u.IdPedidoDet, 
                  u.dañado_picking, u.IdStockRes, u.lic_plate_reemplazo, u.IdUbicacion_reemplazo, u.IdStock_reemplazo, u.IdBodega, u.IdPickingEnc, u.IdOperadorBodega_Pickeo, u.IdOperadorBodega_Verifico, 
                  dbo.operador.nombres AS Nombre_Operador_Pickeo, operador_1.nombres AS Nombre_Operador_Verifico, u.IdPedidoEnc, u.no_encontrado, u.IdUbicacionTemporal, dbo.Nombre_Completo_Ubicacion(u.IdUbicacionTemporal, u.IdBodega) 
                  AS NombreUbicacionTemporal, u.IdOperadorBodega_Asignado
FROM     dbo.operador_bodega AS operador_bodega_1 INNER JOIN
                  dbo.operador AS operador_1 ON operador_bodega_1.IdOperador = operador_1.IdOperador RIGHT OUTER JOIN
                  dbo.operador INNER JOIN
                  dbo.operador_bodega ON dbo.operador.IdOperador = dbo.operador_bodega.IdOperador RIGHT OUTER JOIN
                  dbo.trans_picking_ubic AS u INNER JOIN
                  dbo.bodega_ubicacion AS bu ON u.IdUbicacion = bu.IdUbicacion AND u.IdBodega = bu.IdBodega INNER JOIN
                  dbo.trans_picking_det ON u.IdPickingDet = dbo.trans_picking_det.IdPickingDet AND dbo.trans_picking_det.IdPickingEnc = u.IdPickingEnc INNER JOIN
                  dbo.trans_picking_enc ON dbo.trans_picking_det.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc ON dbo.operador_bodega.IdOperadorBodega = u.IdOperadorBodega_Pickeo ON 
                  operador_bodega_1.IdOperadorBodega = u.IdOperadorBodega_Verifico
```
