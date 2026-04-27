---
id: db-brain-view-vw-get-all-pickingubic-by-idpickingenc-consolidado
type: db-view
title: dbo.VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado
schema: dbo
name: VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado
kind: view
modify_date: 2025-06-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-10 |
| Columnas | 69 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbic` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdPickingDet` | `int` |  |  |
| 4 | `IdUbicacion` | `int` | ✓ |  |
| 5 | `IdStock` | `int` |  |  |
| 6 | `IdPropietarioBodega` | `int` | ✓ |  |
| 7 | `IdProductoBodega` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacionAnterior` | `int` |  |  |
| 12 | `IdRecepcion` | `bigint` | ✓ |  |
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha_vence` | `datetime` | ✓ |  |
| 15 | `fecha_minima` | `datetime` | ✓ |  |
| 16 | `serial` | `varchar(1)` |  |  |
| 17 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 18 | `acepto` | `int` |  |  |
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
| 35 | `user_agr` | `varchar(1)` |  |  |
| 36 | `fec_agr` | `datetime` | ✓ |  |
| 37 | `user_mod` | `varchar(1)` |  |  |
| 38 | `fec_mod` | `datetime` | ✓ |  |
| 39 | `activo` | `bit` | ✓ |  |
| 40 | `IdPedidoDet` | `int` |  |  |
| 41 | `dañado_picking` | `bit` | ✓ |  |
| 42 | `IdStockRes` | `int` |  |  |
| 43 | `lic_plate_reemplazo` | `nvarchar(50)` | ✓ |  |
| 44 | `IdUbicacion_reemplazo` | `int` | ✓ |  |
| 45 | `IdStock_reemplazo` | `int` | ✓ |  |
| 46 | `IdBodega` | `int` | ✓ |  |
| 47 | `IdOperadorBodega_Pickeo` | `int` |  |  |
| 48 | `IdOperadorBodega_Verifico` | `int` |  |  |
| 49 | `IdPedidoEnc` | `int` |  |  |
| 50 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 51 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 52 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 53 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 54 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 55 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 56 | `IdEstado` | `int` |  |  |
| 57 | `Peso` | `float` | ✓ |  |
| 58 | `Precio` | `float` | ✓ |  |
| 59 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |
| 60 | `Tarima` | `float` | ✓ |  |
| 61 | `no_encontrado` | `bit` |  |  |
| 62 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 63 | `NombreClasificacion` | `nvarchar(50)` | ✓ |  |
| 64 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 65 | `RutaDespacho` | `nvarchar(66)` | ✓ |  |
| 66 | `NombreUbicacionTemporal` | `nvarchar(200)` | ✓ |  |
| 67 | `nivel` | `int` | ✓ |  |
| 68 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |
| 69 | `IdTramo` | `int` |  |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_presentacion`
- `road_ruta`
- `stock_res`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
--select * from VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado

CREATE VIEW [dbo].[VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado]
AS
SELECT 0 AS IdPickingUbic, pu.IdPickingEnc, 0 AS IdPickingDet, pu.IdUbicacion, 0 AS IdStock, pu.IdPropietarioBodega, pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, 0 AS IdUbicacionAnterior,
pu.IdRecepcion, pu.lote, pu.fecha_vence, pu.fecha_minima, '' AS serial, pu.lic_plate, 0 AS acepto, SUM(pu.peso_solicitado) AS peso_solicitado, SUM(pu.peso_recibido) AS peso_recibido, SUM(pu.peso_verificado)
AS peso_verificado, SUM(pu.peso_despachado) AS peso_despachado, SUM(pu.cantidad_solicitada) AS cantidad_solicitada, SUM(pu.cantidad_recibida) AS cantidad_recibida, SUM(pu.cantidad_verificada) AS cantidad_verificada,
pu.encontrado, pu.dañado_verificacion, pu.fecha_real_vence, pu.no_packing, MIN(pu.fecha_picking) fecha_picking, 
MIN(pu.fecha_verificado) fecha_verificado, pu.fecha_packing, pu.fecha_despachado, pu.cantidad_despachada, '' user_agr, 
CAST(CONVERT(NVARCHAR,MAX(pu.fec_agr), 112) AS DATETIME) AS fec_agr, '' user_mod, 
CAST(CONVERT(NVARCHAR, MAX(pu.fec_mod), 112) AS DATETIME) AS fec_mod, pu.activo, 0 AS IdPedidoDet, pu.dañado_picking, 0 AS IdStockRes, pu.lic_plate_reemplazo,
pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pu.IdBodega, 0 IdOperadorBodega_Pickeo, 0 AS IdOperadorBodega_Verifico,
0 AS IdPedidoEnc, pdet.IdUnidadMedidaBasica, pdet.codigo_producto, pdet.nombre_producto, pp.nombre AS nom_presentacion, pdet.nom_unid_med, pdet.nom_estado, pdet.IdEstado, 
MAX(pdet.Peso) Peso, MAx(pdet.Precio) Precio,
dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion, pu.IdBodega) AS NombreUbicacion, CASE WHEN (CASE WHEN pdet.IdPresentacion IS NOT NULL AND pdet.IdPresentacion <> 0 AND ISNULL(pp.CajasPorCama, 0) > 0 AND
ISNULL(pp.CamasPorTarima, 0) > 0 THEN ROUND(SUM(pu.cantidad_solicitada) / (ISNULL(pp.CajasPorCama, 0) * ISNULL(pp.CamasPorTarima, 0)), 2) ELSE 0 END) < 1 THEN 0 ELSE (CASE WHEN pdet.IdPresentacion IS NOT NULL
AND pdet.IdPresentacion <> 0 AND ISNULL(pp.CajasPorCama, 0) > 0 AND ISNULL(pp.CamasPorTarima, 0) > 0 THEN ROUND(SUM(pu.cantidad_solicitada) / (ISNULL(pp.CajasPorCama, 0) * ISNULL(pp.CamasPorTarima, 0)), 2)
ELSE 0 END) END AS Tarima, pu.no_encontrado, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, pu.IdBodega) AS NombreArea, pc.nombre AS NombreClasificacion, pu.IdUbicacionTemporal,
dbo.road_ruta.CODIGO + ' ' + dbo.road_ruta.NOMBRE AS RutaDespacho,
dbo.Nombre_Completo_Ubicacion(pu.IdUbicacionTemporal, pu.IdBodega) NombreUbicacionTemporal, bodega_ubicacion.nivel, pu.IdOperadorBodega_Asignado,bodega_ubicacion.IdTramo
FROM dbo.bodega_ubicacion INNER JOIN
dbo.trans_picking_ubic AS pu ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.bodega_ubicacion.IdBodega = pu.IdBodega INNER JOIN
dbo.trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet INNER JOIN
dbo.trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
dbo.stock_res AS sr ON pkdet.IdPedidoDet = sr.IdPedidoDet AND pu.IdUbicacion = sr.IdUbicacion AND pu.IdStockRes = sr.IdStockRes INNER JOIN
dbo.trans_picking_enc ON pkdet.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc LEFT OUTER JOIN
dbo.producto_presentacion AS pp ON pp.IdPresentacion = pdet.IdPresentacion INNER JOIN
dbo.producto_bodega AS pb ON pb.IdProductoBodega = pu.IdProductoBodega INNER JOIN
dbo.producto AS p ON p.IdProducto = pb.IdProducto INNER JOIN
dbo.trans_pe_enc ON pdet.IdPedidoEnc = dbo.trans_pe_enc.IdPedidoEnc LEFT OUTER JOIN
dbo.road_ruta ON dbo.trans_pe_enc.RoadIdRutaDespacho = dbo.road_ruta.IdRuta LEFT OUTER JOIN
dbo.producto_clasificacion AS pc ON pc.IdClasificacion = p.IdClasificacion
GROUP BY pu.IdPickingEnc, pu.IdUbicacion, pu.IdPropietarioBodega, pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, pu.IdRecepcion, pu.lote, pu.fecha_vence, pu.fecha_minima, pu.lic_plate,
pu.encontrado, pu.dañado_verificacion, pu.fecha_real_vence, pu.no_packing, pu.fecha_packing, pu.fecha_despachado, pu.cantidad_despachada, pu.activo, pu.dañado_picking, pu.lic_plate_reemplazo, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo,
pu.IdBodega, pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, pdet.codigo_producto, pdet.nombre_producto, pp.nombre, pdet.nom_unid_med, pdet.nom_estado,
pdet.IdEstado, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.bodega_ubicacion.IdUbicacion, pp.CajasPorCama, pp.CamasPorTarima,
pu.no_encontrado, dbo.bodega_ubicacion.IdArea, pc.nombre, pu.IdUbicacionTemporal, dbo.road_ruta.CODIGO, dbo.road_ruta.NOMBRE, pu.IdOperadorBodega_Asignado, bodega_ubicacion.IdTramo
```
