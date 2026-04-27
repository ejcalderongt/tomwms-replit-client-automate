---
id: db-brain-view-vw-pickingubic-by-idpickingenc
type: db-view
title: dbo.VW_PickingUbic_By_IdPickingEnc
schema: dbo
name: VW_PickingUbic_By_IdPickingEnc
kind: view
modify_date: 2022-12-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PickingUbic_By_IdPickingEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-12-23 |
| Columnas | 73 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idpickingenc` | `int` |  |  |
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
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
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
| 46 | `IdPedidoEnc` | `int` |  |  |
| 47 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 48 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 49 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 50 | `nom_presentacion` | `nvarchar(50)` |  |  |
| 51 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 52 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 53 | `IdEstado` | `int` |  |  |
| 54 | `Peso` | `float` | ✓ |  |
| 55 | `Precio` | `float` | ✓ |  |
| 56 | `Sr_IdStockRes` | `int` | ✓ |  |
| 57 | `Sr_IdStock` | `int` | ✓ |  |
| 58 | `IdBodega` | `int` | ✓ |  |
| 59 | `IDOPERADORBODEGA_PICKEO` | `int` | ✓ |  |
| 60 | `IDOPERADORBODEGA_VERIFICO` | `int` | ✓ |  |
| 61 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |
| 62 | `IdOperadorBodega` | `int` | ✓ |  |
| 63 | `Tarima` | `float` | ✓ |  |
| 64 | `no_encontrado` | `bit` |  |  |
| 65 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 66 | `IdClasificacion` | `int` | ✓ |  |
| 67 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 68 | `NombreUbicacionTemporal` | `nvarchar(200)` | ✓ |  |
| 69 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 70 | `nivel` | `int` | ✓ |  |
| 71 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |
| 72 | `IdTramo` | `int` |  |  |
| 73 | `Nombre_Tramo` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_presentacion`
- `stock_res`
- `trans_pe_det`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_PickingUbic_By_IdPickingEnc]
AS
SELECT pu.idpickingenc, pu.IdPickingUbic, pu.IdPickingDet, pu.IdUbicacion, pu.IdStock, pu.IdPropietarioBodega, pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, pu.IdUbicacionAnterior, pu.IdRecepcion, pu.lote, 
                  pu.fecha_vence, pu.fecha_minima, pu.serial, pu.lic_plate, pu.acepto, pu.peso_solicitado, pu.peso_recibido, pu.peso_verificado, pu.peso_despachado, pu.cantidad_solicitada, pu.cantidad_recibida, pu.cantidad_verificada, pu.encontrado, 
                  pu.dañado_verificacion, pu.fecha_real_vence, pu.no_packing, pu.fecha_picking, pu.fecha_verificado, pu.fecha_packing, pu.fecha_despachado, pu.cantidad_despachada, pu.user_agr, pu.fec_agr, pu.user_mod, pu.fec_mod, pu.activo, 
                  pu.IdPedidoDet, pu.dañado_picking, pu.IdStockRes, pu.lic_plate_reemplazo, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pdet.IdPedidoEnc, pdet.IdUnidadMedidaBasica, pdet.codigo_producto, pdet.nombre_producto, 
                  ISNULL(pp.nombre, '') AS nom_presentacion, pdet.nom_unid_med, pdet.nom_estado, pdet.IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes AS Sr_IdStockRes, sr.IdStock AS Sr_IdStock, pu.IdBodega, pu.IDOPERADORBODEGA_PICKEO, 
                  pu.IDOPERADORBODEGA_VERIFICO, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS NombreUbicacion, pkdet.IdOperadorBodega, 
                  CASE WHEN (CASE WHEN (pu.IdPresentacion IS NOT NULL AND pu.IdPresentacion <> 0 AND ISNULL(pp.CajasPorCama, 0) > 0 AND ISNULL(pp.CamasPorTarima, 0) > 0) THEN ROUND(pu.cantidad_solicitada / (ISNULL(pp.CajasPorCama, 0) 
                  * ISNULL(pp.CamasPorTarima, 0)), 2) ELSE 0 END) >= 1 THEN CASE WHEN (pdet.IdPresentacion IS NOT NULL AND pdet.IdPresentacion <> 0 AND ISNULL(pp.CajasPorCama, 0) > 0 AND ISNULL(pp.CamasPorTarima, 0) > 0) 
                  THEN ROUND(pu.cantidad_solicitada / (ISNULL(pp.CajasPorCama, 0) * ISNULL(pp.CamasPorTarima, 0)), 2) ELSE 0 END ELSE 0 END AS Tarima, pu.no_encontrado, dbo.producto_clasificacion.nombre AS Clasificacion, 
                  dbo.producto.IdClasificacion, pu.IdUbicacionTemporal, dbo.Nombre_Completo_Ubicacion(pu.IdUbicacionTemporal, pu.IdBodega) AS NombreUbicacionTemporal, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, pu.IdBodega) 
                  AS NombreArea, dbo.bodega_ubicacion.nivel, pu.IdOperadorBodega_Asignado, dbo.bodega_ubicacion.IdTramo, dbo.bodega_tramo.descripcion AS Nombre_Tramo
FROM     dbo.producto_bodega INNER JOIN
                  dbo.trans_picking_ubic AS pu INNER JOIN
                  dbo.bodega_ubicacion ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND pu.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                  dbo.trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet AND pkdet.IdPickingEnc = pu.idpickingenc INNER JOIN
                  dbo.trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
                  dbo.trans_picking_enc ON pkdet.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc ON dbo.producto_bodega.IdProductoBodega = pu.IdProductoBodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega LEFT OUTER JOIN
                  dbo.stock_res AS sr ON pu.IdUbicacion = sr.IdUbicacion AND pu.IdStockRes = sr.IdStockRes AND pu.IdPropietarioBodega = sr.IdPropietarioBodega AND pu.IdProductoBodega = sr.IdProductoBodega AND 
                  pu.IdPedidoDet = sr.IdPedidoDet LEFT OUTER JOIN
                  dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON pp.IdPresentacion = pu.IdPresentacion
```
