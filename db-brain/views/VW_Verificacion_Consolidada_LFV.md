---
id: db-brain-view-vw-verificacion-consolidada-lfv
type: db-view
title: dbo.VW_Verificacion_Consolidada_LFV
schema: dbo
name: VW_Verificacion_Consolidada_LFV
kind: view
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Verificacion_Consolidada_LFV`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-03-18 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` |  |  |
| 2 | `IdPedidoDet` | `int` |  |  |
| 3 | `lote` | `nvarchar(50)` | ✓ |  |
| 4 | `fecha_vence` | `datetime` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `lic_plate` | `varchar(1)` |  |  |
| 7 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 8 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 9 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 10 | `cantidad_solicitada` | `float` | ✓ |  |
| 11 | `cantidad_recibida` | `float` | ✓ |  |
| 12 | `cantidad_verificada` | `float` | ✓ |  |
| 13 | `IdPresentacion` | `int` | ✓ |  |
| 14 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 15 | `codigo` | `nvarchar(50)` | ✓ |  |
| 16 | `ndias` | `int` | ✓ |  |
| 17 | `diferencia` | `float` | ✓ |  |
| 18 | `IdPresentacionPicking` | `int` | ✓ |  |
| 19 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 20 | `IdProductoEstado` | `int` | ✓ |  |
| 21 | `NombreArea` | `varchar(1)` |  |  |
| 22 | `NombreClasificacion` | `varchar(1)` |  |  |
| 23 | `Bono` | `nvarchar(50)` |  |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_presentacion`
- `trans_manufactura_enc`
- `trans_manufactura_picking`
- `trans_manufactura_tipo`
- `trans_pe_det`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
--#CKFK20250226 Agregué el campo bono
CREATE VIEW [dbo].[VW_Verificacion_Consolidada_LFV]
AS
SELECT ped.IdPedidoEnc,0 as IdPedidoDet, pubic.lote, pubic.fecha_vence,pubic.IdProductoBodega, 
'' as lic_plate, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado,
SUM(pubic.cantidad_solicitada) AS cantidad_solicitada,
SUM(pubic.cantidad_recibida) AS cantidad_recibida,
SUM(pubic.cantidad_verificada) AS cantidad_verificada,
ped.IdPresentacion, ped.IdUnidadMedidaBasica,P.codigo, 
ped.ndias,SUM(pubic.cantidad_recibida) - SUM(pubic.cantidad_verificada) AS diferencia,
pubic.IdPresentacion AS IdPresentacionPicking,dbo.producto_presentacion.nombre AS nom_presentacion,
pubic.IdProductoEstado,'' as NombreArea,'' AS NombreClasificacion,
case when P.IdTipoManufactura = 1 THEN 'Bono' ELSE ISNULL(mt.nombre,'') END Bono
FROM dbo.trans_pe_det AS ped 
INNER JOIN dbo.trans_picking_det AS pkdet ON ped.IdPedidoDet = pkdet.IdPedidoDet 
INNER JOIN dbo.trans_picking_ubic AS pubic ON pkdet.IdPickingDet = pubic.IdPickingDet  
LEFT OUTER JOIN dbo.producto AS P 
INNER JOIN dbo.producto_bodega AS pb ON P.IdProducto = pb.IdProducto ON pubic.IdProductoBodega = pb.IdProductoBodega 
LEFT OUTER JOIN dbo.producto_presentacion ON pubic.IdPresentacion = dbo.producto_presentacion.IdPresentacion  LEFT OUTER JOIN
dbo.trans_manufactura_enc me ON me.IdPedidoEnc = ped.IdPedidoEnc LEFT OUTER JOIN
dbo.trans_manufactura_picking mp ON ped.IdPedidoDet = mp.IdPedidoDet ANd me.IdManufacturaEnc= mp.IdManufacturaEnc LEFT OUTER JOIN
dbo.trans_manufactura_tipo mt ON mt.idtipomanufactura = me.IdTipoManufactura
WHERE (pubic.dañado_verificacion = 0) AND (pubic.dañado_picking = 0) AND (pubic.no_encontrado = 0) 
AND (pkdet.IdPickingEnc NOT IN(SELECT IdPickingEnc FROM dbo.trans_picking_enc AS trans_picking_enc_1 WHERE (estado = 'Anulado')))
GROUP BY ped.IdPedidoEnc, ped.IdProductoBodega, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado, ped.IdPresentacion, ped.IdUnidadMedidaBasica, P.codigo, 
ped.ndias,pubic.dañado_verificacion, pubic.IdPresentacion, dbo.producto_presentacion.nombre, pubic.IdProductoBodega, pubic.IdProductoEstado, pubic.IdBodega, 
pubic.lote, pubic.fecha_vence,P.IdTipoManufactura,mt.nombre
```
