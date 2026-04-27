---
id: db-brain-view-vw-picking-det-by-idpickingenc
type: db-view
title: dbo.VW_Picking_Det_By_IdPickingEnc
schema: dbo
name: VW_Picking_Det_By_IdPickingEnc
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Picking_Det_By_IdPickingEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 27 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 2 | `Cliente` | `nvarchar(150)` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 5 | `IdPickingDet` | `int` |  |  |
| 6 | `IdPickingEnc` | `int` |  |  |
| 7 | `IdPedidoDet` | `int` |  |  |
| 8 | `IdOperadorBodega` | `int` | ✓ |  |
| 9 | `cantidad` | `float` | ✓ |  |
| 10 | `cliente_dias` | `int` | ✓ |  |
| 11 | `cantidad_recibida` | `float` | ✓ |  |
| 12 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 13 | `fec_agr` | `datetime` | ✓ |  |
| 14 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 15 | `fec_mod` | `datetime` | ✓ |  |
| 16 | `activo` | `bit` | ✓ |  |
| 17 | `IdPedidoEnc` | `int` | ✓ |  |
| 18 | `IdProducto` | `int` |  |  |
| 19 | `IdPresentacion` | `int` | ✓ |  |
| 20 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 21 | `IdEstado` | `int` |  |  |
| 22 | `Nombre_Estado` | `nvarchar(50)` | ✓ |  |
| 23 | `Nombre_Presentacion` | `nvarchar(50)` | ✓ |  |
| 24 | `Nombre_Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 25 | `codigo` | `nvarchar(50)` | ✓ |  |
| 26 | `nombre` | `nvarchar(100)` | ✓ |  |
| 27 | `Bono` | `varchar(4)` |  |  |

## Consume

- `bodega`
- `cliente`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Picking_Det_By_IdPickingEnc]
AS
SELECT        b.nombre AS Bodega, c.nombre_comercial AS Cliente, pr.nombre_comercial AS Propietario, penc.Fecha_Pedido, pcdet.IdPickingDet, pcdet.IdPickingEnc, pcdet.IdPedidoDet, pcdet.IdOperadorBodega, pcdet.cantidad, 
                         pcdet.cliente_dias, pcdet.cantidad_recibida, pcdet.user_agr, pcdet.fec_agr, pcdet.user_mod, pcdet.fec_mod, pcdet.activo, pcdet.IdPedidoEnc, p.IdProducto, pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdEstado, 
                         dbo.producto_estado.nombre AS Nombre_Estado, dbo.producto_presentacion.nombre AS Nombre_Presentacion, dbo.unidad_medida.Nombre AS Nombre_Unidad_Medida, p.codigo, p.nombre,
						 case when p.IdTipoManufactura = 1 THEN 'Bono' ELSE '' END Bono
FROM            dbo.trans_picking_det AS pcdet INNER JOIN
                         dbo.trans_pe_det AS pdet ON pcdet.IdPedidoDet = pdet.IdPedidoDet INNER JOIN
                         dbo.trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc INNER JOIN
                         dbo.propietario_bodega AS prb ON penc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                         dbo.bodega AS b ON penc.IdBodega = b.IdBodega INNER JOIN
                         dbo.cliente AS c ON penc.IdCliente = c.IdCliente INNER JOIN
                         dbo.producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                         dbo.producto_estado ON pdet.IdEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.unidad_medida ON pdet.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                         dbo.producto_presentacion ON pdet.IdPresentacion = dbo.producto_presentacion.IdPresentacion
```
