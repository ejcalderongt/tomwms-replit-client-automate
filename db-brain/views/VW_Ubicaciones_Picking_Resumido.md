---
id: db-brain-view-vw-ubicaciones-picking-resumido
type: db-view
title: dbo.VW_Ubicaciones_Picking_Resumido
schema: dbo
name: VW_Ubicaciones_Picking_Resumido
kind: view
modify_date: 2022-06-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ubicaciones_Picking_Resumido`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-26 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedido` | `bigint` | ✓ |  |
| 2 | `IdPicking` | `bigint` | ✓ |  |
| 3 | `codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `nombre` | `nvarchar(100)` | ✓ |  |
| 5 | `Cant_UMBas` | `float` |  |  |
| 6 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 7 | `Cant_Pres` | `float` |  |  |
| 8 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 9 | `peso` | `float` | ✓ |  |
| 10 | `lote` | `nvarchar(50)` | ✓ |  |
| 11 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 12 | `Fecha_Vence` | `date` | ✓ |  |
| 13 | `Ubicacion` | `nvarchar(200)` | ✓ |  |

## Consume

- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `stock`
- `stock_res`
- `trans_picking_det`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ubicaciones_Picking_Resumido]
AS
SELECT     res.IdPedido, res.IdPicking, p.codigo, p.nombre, ISNULL(s.cantidad, 0) AS Cant_UMBas, um.Nombre AS UMBas, ISNULL(ISNULL(s.cantidad, 0) / pp.factor, 0) AS Cant_Pres, pp.nombre AS Presentacion, res.peso, res.lote, 
                         res.lic_plate AS Licencia, CONVERT(DATE, res.fecha_vence) AS Fecha_Vence, dbo.Nombre_Completo_Ubicacion(res.IdUbicacion, res.IdBodega) AS Ubicacion
FROM            dbo.stock_res AS res INNER JOIN
                         dbo.producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN
                         dbo.unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.trans_picking_det INNER JOIN
                         dbo.trans_picking_ubic ON dbo.trans_picking_det.IdPickingDet = dbo.trans_picking_ubic.IdPickingDet ON res.IdBodega = dbo.trans_picking_ubic.IdBodega AND res.IdPedidoDet = dbo.trans_picking_det.IdPedidoDet AND 
                         res.IdStock = dbo.trans_picking_ubic.IdStock AND res.IdStockRes = dbo.trans_picking_ubic.IdStockRes LEFT OUTER JOIN
                         dbo.stock AS s ON res.IdStock = s.IdStock LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
WHERE        (ISNULL(dbo.trans_picking_ubic.dañado_verificacion, 0) = 0) AND (ISNULL(dbo.trans_picking_ubic.dañado_picking, 0) = 0) AND (ISNULL(dbo.trans_picking_ubic.no_encontrado, 0) = 0)
GROUP BY res.IdPedido,res.IdPicking, p.codigo, p.nombre, um.Nombre, s.cantidad, pp.factor, pp.nombre, res.lote, res.lic_plate, res.serial, res.peso, res.fecha_vence, res.IdUbicacion, res.IdBodega
```
