---
id: db-brain-view-vw-pickingubicacion
type: db-view
title: dbo.VW_PickingUbicacion
schema: dbo
name: VW_PickingUbicacion
kind: view
modify_date: 2020-01-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PickingUbicacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2020-01-25 |
| Columnas | 22 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Pedido` | `int` |  |  |
| 2 | `ID Picking` | `int` |  |  |
| 3 | `Código` | `nvarchar(50)` | ✓ |  |
| 4 | `Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `Unidad Medida` | `nvarchar(50)` | ✓ |  |
| 8 | `Ubicación` | `nvarchar(50)` | ✓ |  |
| 9 | `Solicitado` | `float` | ✓ |  |
| 10 | `Picking` | `float` | ✓ |  |
| 11 | `Verificado` | `float` | ✓ |  |
| 12 | `Fecha Pedido` | `datetime` | ✓ |  |
| 13 | `Fecha Picking` | `datetime` | ✓ |  |
| 14 | `Vence` | `datetime` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `IdPickingUbic` | `int` |  |  |
| 17 | `IdPropietarioBodega` | `int` | ✓ |  |
| 18 | `IdProductoBodega` | `int` |  |  |
| 19 | `IdEstado` | `int` |  |  |
| 20 | `IdPresentacion` | `int` | ✓ |  |
| 21 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 22 | `IdPedidoDet` | `int` |  |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_PickingUbicacion]
AS
SELECT        penc.IdPedidoEnc AS Pedido, d.IdPickingEnc AS [ID Picking], p.codigo AS Código, p.nombre AS Producto, pp.nombre AS Presentación, pe.nombre AS Estado, um.Nombre AS [Unidad Medida], b.descripcion AS Ubicación, 
                         pdet.Cantidad AS Solicitado, d.cantidad_recibida AS Picking, u.cantidad_verificada AS Verificado, penc.Fecha_Pedido AS [Fecha Pedido], enc.fecha_picking AS [Fecha Picking], u.fecha_vence AS Vence, u.activo, u.IdPickingUbic, 
                         enc.IdPropietarioBodega, pdet.IdProductoBodega, pdet.IdEstado, pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdPedidoDet
FROM            dbo.trans_picking_ubic AS u INNER JOIN
                         dbo.trans_picking_det AS d ON u.IdPickingDet = d.IdPickingDet INNER JOIN
                         dbo.bodega_ubicacion AS b ON u.IdUbicacion = b.IdUbicacion AND u.IdBodega = b.IdBodega INNER JOIN
                         dbo.trans_picking_enc AS enc ON d.IdPickingEnc = enc.IdPickingEnc INNER JOIN
                         dbo.propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                         dbo.trans_pe_det AS pdet ON d.IdPedidoDet = pdet.IdPedidoDet INNER JOIN
                         dbo.trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc INNER JOIN
                         dbo.producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON pdet.IdPresentacion = pp.IdPresentacion LEFT OUTER JOIN
                         dbo.producto_estado AS pe ON pdet.IdEstado = pe.IdEstado INNER JOIN
                         dbo.unidad_medida AS um ON pdet.IdUnidadMedidaBasica = um.IdUnidadMedida
```
