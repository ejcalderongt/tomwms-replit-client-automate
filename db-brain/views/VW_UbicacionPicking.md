---
id: db-brain-view-vw-ubicacionpicking
type: db-view
title: dbo.VW_UbicacionPicking
schema: dbo
name: VW_UbicacionPicking
kind: view
modify_date: 2019-11-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_UbicacionPicking`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-11-19 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCliente` | `int` |  |  |
| 2 | `NombreCliente` | `nvarchar(150)` | ✓ |  |
| 3 | `IdPedidoEnc` | `int` |  |  |
| 4 | `IdPickingEnc` | `int` |  |  |
| 5 | `FechaPedido` | `datetime` | ✓ |  |
| 6 | `FechaPicking` | `datetime` | ✓ |  |
| 7 | `CodigoProducto` | `nvarchar(50)` | ✓ |  |
| 8 | `NombreProducto` | `nvarchar(100)` | ✓ |  |
| 9 | `Vence` | `datetime` | ✓ |  |
| 10 | `Estado` | `nvarchar(50)` | ✓ |  |
| 11 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 12 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 13 | `Operador` | `nvarchar(201)` |  |  |
| 14 | `lote` | `nvarchar(35)` | ✓ |  |
| 15 | `Solicitado` | `float` | ✓ |  |
| 16 | `Picking` | `float` | ✓ |  |
| 17 | `Verificado` | `float` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `cliente`
- `operador`
- `operador_bodega`
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
- `trans_picking_op`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_UbicacionPicking]
AS
SELECT        c.IdCliente, c.nombre_comercial AS NombreCliente, penc.IdPedidoEnc, d.IdPickingEnc, penc.Fecha_Pedido AS FechaPedido, enc.fecha_picking AS FechaPicking, 
                         p.codigo AS CodigoProducto, p.nombre AS NombreProducto, u.fecha_vence AS Vence, pe.nombre AS Estado, b.descripcion AS Ubicacion, pp.nombre AS Presentación,
                          ISNULL(pd.nombres, '') + ' ' + ISNULL(pd.apellidos, '') AS Operador, u.lote, u.cantidad_solicitada AS Solicitado, u.cantidad_recibida AS Picking, 
                         u.cantidad_verificada AS Verificado
FROM            dbo.trans_picking_op INNER JOIN
                         dbo.trans_picking_ubic AS u INNER JOIN
                         dbo.trans_picking_det AS d ON u.IdPickingDet = d.IdPickingDet INNER JOIN
                         dbo.bodega_ubicacion AS b ON u.IdUbicacion = b.IdUbicacion INNER JOIN
                         dbo.trans_picking_enc AS enc ON d.IdPickingEnc = enc.IdPickingEnc AND b.IdBodega = enc.IdBodega INNER JOIN
                         dbo.propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                         dbo.trans_pe_det AS pdet ON d.IdPedidoDet = pdet.IdPedidoDet INNER JOIN
                         dbo.trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc INNER JOIN
                         dbo.cliente AS c ON penc.IdCliente = c.IdCliente INNER JOIN
                         dbo.producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON pdet.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                         dbo.producto_estado AS pe ON pdet.IdEstado = pe.IdEstado ON dbo.trans_picking_op.IdPickingEnc = enc.IdPickingEnc INNER JOIN
                         dbo.operador AS pd INNER JOIN
                         dbo.operador_bodega AS op ON pd.IdOperador = op.IdOperador ON dbo.trans_picking_op.IdOperadorBodega = op.IdOperadorBodega
WHERE        (u.dañado_picking = 0) AND (u.dañado_verificacion = 0)
```
