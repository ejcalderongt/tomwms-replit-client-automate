---
id: db-brain-view-vw-verificacion
type: db-view
title: dbo.VW_Verificacion
schema: dbo
name: VW_Verificacion
kind: view
modify_date: 2025-05-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Verificacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-22 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` |  |  |
| 2 | `IdPedidoDet` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` | ✓ |  |
| 4 | `lote` | `nvarchar(50)` | ✓ |  |
| 5 | `fecha_vence` | `datetime` | ✓ |  |
| 6 | `lic_plate` | `nvarchar(25)` | ✓ |  |
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
| 21 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 22 | `NombreClasificacion` | `nvarchar(50)` | ✓ |  |
| 23 | `Bono` | `nvarchar(50)` |  |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Area`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_presentacion`
- `trans_manufactura_enc`
- `trans_manufactura_tipo`
- `trans_pe_det`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Verificacion]
AS
SELECT ped.IdPedidoEnc, ped.IdPedidoDet, pubic.IdProductoBodega, pubic.lote,
pubic.fecha_vence, pubic.lic_plate, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado,
SUM(pubic.cantidad_solicitada) AS cantidad_solicitada,
SUM(pubic.cantidad_recibida) AS cantidad_recibida,
SUM(pubic.cantidad_verificada) AS cantidad_verificada,
ped.IdPresentacion, ped.IdUnidadMedidaBasica,
P.codigo, ped.ndias,
SUM(pubic.cantidad_recibida) - SUM(pubic.cantidad_verificada) AS diferencia,
pubic.IdPresentacion AS IdPresentacionPicking,
dbo.producto_presentacion.nombre AS nom_presentacion,
pubic.IdProductoEstado,
dbo.Nombre_Area(bodega_ubicacion.IdArea, pubic.IdBodega) as NombreArea,
pc.nombre AS NombreClasificacion,
case when P.IdTipoManufactura = 1 THEN 'Bono' ELSE ISNULL(mt.nombre,'') END Bono
FROM dbo.bodega_ubicacion INNER JOIN
dbo.trans_pe_det AS ped INNER JOIN
dbo.trans_picking_det AS pkdet ON ped.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
dbo.trans_picking_ubic AS pubic ON pkdet.IdPickingDet = pubic.IdPickingDet ON dbo.bodega_ubicacion.IdUbicacion = pubic.IdUbicacion AND dbo.bodega_ubicacion.IdBodega = pubic.IdBodega LEFT OUTER JOIN
dbo.producto AS P INNER JOIN
dbo.producto_bodega AS pb ON P.IdProducto = pb.IdProducto ON pubic.IdProductoBodega = pb.IdProductoBodega LEFT OUTER JOIN
dbo.producto_presentacion ON pubic.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
dbo.producto_clasificacion AS pc ON pc.IdClasificacion = P.IdClasificacion LEFT OUTER JOIN
dbo.trans_manufactura_enc me ON me.IdPedidoEnc = ped.IdPedidoEnc LEFT OUTER JOIN
dbo.trans_manufactura_tipo mt ON mt.idtipomanufactura = me.IdTipoManufactura
WHERE (pubic.dañado_verificacion =
 0) AND (pubic.dañado_picking = 0) AND (pubic.no_encontrado = 0) AND (pkdet.IdPickingEnc NOT IN
(SELECT IdPickingEnc
FROM dbo.trans_picking_enc AS trans_picking_enc_1
WHERE (estado = 'Anulado')))
GROUP BY ped.IdPedidoEnc, ped.IdPedidoDet, ped.IdProductoBodega, pubic.lote, pubic.fecha_vence, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado, ped.IdPresentacion, ped.IdUnidadMedidaBasica, P.codigo, ped.ndias,
pubic.dañado_verificacion, pubic.lic_plate, pubic.IdPresentacion, dbo.producto_presentacion.nombre, pubic.IdProductoBodega, pubic.IdProductoEstado, pubic.IdBodega, pc.nombre, dbo.bodega_ubicacion.IdArea,P.IdTipoManufactura, mt.nombre
```
