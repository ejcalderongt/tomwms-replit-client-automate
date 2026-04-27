---
id: db-brain-view-vw-get-all-pickingubic-by-idpickingenc-detallado
type: db-view
title: dbo.VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado
schema: dbo
name: VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado
kind: view
modify_date: 2023-02-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-02-21 |
| Columnas | 67 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbic` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdPickingDet` | `int` |  |  |
| 4 | `IdUbicacion` | `int` | ✓ |  |
| 5 | `IdPropietarioBodega` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdUnidadMedida` | `int` | ✓ |  |
| 8 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 9 | `IdRecepcion` | `bigint` | ✓ |  |
| 10 | `lote` | `nvarchar(50)` | ✓ |  |
| 11 | `fecha_vence` | `datetime` | ✓ |  |
| 12 | `fecha_minima` | `datetime` | ✓ |  |
| 13 | `serial` | `nvarchar(35)` | ✓ |  |
| 14 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 15 | `acepto` | `bit` | ✓ |  |
| 16 | `peso_solicitado` | `float` | ✓ |  |
| 17 | `peso_recibido` | `float` | ✓ |  |
| 18 | `peso_verificado` | `float` | ✓ |  |
| 19 | `peso_despachado` | `float` | ✓ |  |
| 20 | `cantidad_solicitada` | `float` | ✓ |  |
| 21 | `cantidad_recibida` | `float` | ✓ |  |
| 22 | `cantidad_verificada` | `float` | ✓ |  |
| 23 | `encontrado` | `bit` | ✓ |  |
| 24 | `dañado_verificacion` | `bit` | ✓ |  |
| 25 | `fecha_real_vence` | `datetime` | ✓ |  |
| 26 | `no_packing` | `nvarchar(50)` | ✓ |  |
| 27 | `fecha_picking` | `datetime` | ✓ |  |
| 28 | `fecha_verificado` | `datetime` | ✓ |  |
| 29 | `fecha_packing` | `datetime` | ✓ |  |
| 30 | `fecha_despachado` | `datetime` | ✓ |  |
| 31 | `cantidad_despachada` | `float` | ✓ |  |
| 32 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 33 | `fec_agr` | `datetime` | ✓ |  |
| 34 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 35 | `fec_mod` | `datetime` | ✓ |  |
| 36 | `activo` | `bit` | ✓ |  |
| 37 | `dañado_picking` | `bit` | ✓ |  |
| 38 | `lic_plate_reemplazo` | `nvarchar(50)` | ✓ |  |
| 39 | `IdUbicacion_reemplazo` | `int` | ✓ |  |
| 40 | `IdStock_reemplazo` | `int` | ✓ |  |
| 41 | `IdBodega` | `int` | ✓ |  |
| 42 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 43 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 44 | `IdPedidoEnc` | `int` |  |  |
| 45 | `IdPedidoDet` | `int` |  |  |
| 46 | `IdPresentacion` | `int` | ✓ |  |
| 47 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 48 | `IdProductoBodega` | `int` |  |  |
| 49 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 50 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 51 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 52 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 53 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 54 | `IdEstado` | `int` | ✓ |  |
| 55 | `Peso` | `float` | ✓ |  |
| 56 | `Precio` | `float` | ✓ |  |
| 57 | `IdStockRes` | `int` |  |  |
| 58 | `IdStock` | `int` |  |  |
| 59 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |
| 60 | `Tarima` | `float` | ✓ |  |
| 61 | `no_encontrado` | `bit` |  |  |
| 62 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 63 | `NombreClasificacion` | `nvarchar(50)` | ✓ |  |
| 64 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 65 | `nivel` | `int` | ✓ |  |
| 66 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |
| 67 | `IdTramo` | `int` |  |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_presentacion`
- `stock_res`
- `trans_pe_det`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado] AS
SELECT pu.IdPickingUbic, pu.IdPickingEnc, pu.IdPickingDet, pu.IdUbicacion, 
pu.IdPropietarioBodega, pu.IdProductoEstado, pu.IdUnidadMedida, pu.IdUbicacionAnterior, 
pu.IdRecepcion, pu.lote, pu.fecha_vence, pu.fecha_minima, pu.serial, 
pu.lic_plate, pu.acepto, pu.peso_solicitado, pu.peso_recibido, 
pu.peso_verificado, pu.peso_despachado, pu.cantidad_solicitada, 
pu.cantidad_recibida, pu.cantidad_verificada, pu.encontrado, 
pu.dañado_verificacion, pu.fecha_real_vence, pu.no_packing, pu.fecha_picking, 
pu.fecha_verificado, pu.fecha_packing, pu.fecha_despachado, pu.cantidad_despachada, 
pu.user_agr, pu.fec_agr, pu.user_mod, pu.fec_mod, pu.activo, 
pu.dañado_picking, pu.lic_plate_reemplazo, 
pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pu.IdBodega, 
pu.IdOperadorBodega_Pickeo, pu.IdOperadorBodega_Verifico,
pdet.IdPedidoEnc,pdet.IdPedidoDet,pu.IdPresentacion, pu.IdUnidadMedida IdUnidadMedidaBasica, 
pdet.IdProductoBodega, 
pdet.codigo_producto, pdet.nombre_producto,pp.nombre as nom_presentacion, um.Nombre as nom_unid_med,pe.nombre nom_estado,
pu.IdProductoEstado IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes, sr.IdStock,
dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion,pu.IdBodega) as NombreUbicacion,
CASE WHEN (
CASE WHEN pu.IdPresentacion IS NOT NULL AND pu.IdPresentacion <> 0
AND ISNULL(pp.CajasPorCama,0)>0 AND ISNULL(pp.CamasPorTarima,0)>0 THEN
ROUND(pu.cantidad_solicitada / (ISNULL(pp.CajasPorCama,0)*ISNULL(pp.CamasPorTarima,0)),2) ELSE 0 END) < 1 
THEN 0 ELSE (
CASE WHEN pu.IdPresentacion IS NOT NULL AND pu.IdPresentacion <> 0
AND ISNULL(pp.CajasPorCama,0)>0 AND ISNULL(pp.CamasPorTarima,0)>0 THEN
ROUND(pu.cantidad_solicitada / (ISNULL(pp.CajasPorCama,0)*ISNULL(pp.CamasPorTarima,0)),2) ELSE 0 END) END Tarima,
pu.no_encontrado,
dbo.Nombre_Area(bodega_ubicacion.IdArea, pu.IdBodega) as NombreArea,
pc.nombre as NombreClasificacion, pu.IdUbicacionTemporal, bodega_ubicacion.nivel,  pu.IdOperadorBodega_Asignado,
bodega_ubicacion.IdTramo
FROM           
bodega INNER JOIN
bodega_ubicacion INNER JOIN
trans_picking_ubic AS pu ON pu.IdUbicacion = bodega_ubicacion.IdUbicacion 
INNER JOIN trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet 
INNER JOIN trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
stock_res AS sr ON pkdet.IdPedidoDet = sr.IdPedidoDet AND pu.IdUbicacion = sr.IdUbicacion AND pu.IdStockRes = sr.IdStockRes ON 
bodega.IdBodega = bodega_ubicacion.IdBodega 
INNER JOIN trans_picking_enc ON bodega.IdBodega = trans_picking_enc.IdBodega
AND pkdet.IdPickingEnc = trans_picking_enc.IdPickingEnc 
INNER JOIN bodega_tramo ON bodega.IdBodega = bodega_tramo.IdBodega 
AND bodega_ubicacion.IdTramo = bodega_tramo.IdTramo                                
LEFT OUTER JOIN producto_presentacion pp ON pp.IdPresentacion = pu.IdPresentacion   
INNER JOIN producto_bodega pb on pb.IdProductoBodega = pu.IdProductoBodega
INNER JOIN producto p on p.IdProducto = pb.IdProducto
INNER JOIN dbo.unidad_medida um ON um.IdUnidadMedida = pu.IdUnidadMedida 
LEFT JOIN producto_clasificacion  pc on pc.IdClasificacion = p.IdClasificacion inner join 
dbo.producto_estado pe on pu.IdProductoEstado = pe.IdEstado 
WHERE pu.dañado_picking = 0 and pu.no_encontrado = 0
```
