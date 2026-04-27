---
id: db-brain-view-vw-despacho-detalle
type: db-view
title: dbo.VW_Despacho_Detalle
schema: dbo
name: VW_Despacho_Detalle
kind: view
modify_date: 2017-12-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Despacho_Detalle`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-12-18 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDespachoEnc` | `int` |  |  |
| 2 | `IdDespachoDet` | `int` |  |  |
| 3 | `codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Producto` | `nvarchar(50)` | ✓ |  |
| 5 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 8 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `IdPickingUbic` | `int` |  |  |
| 11 | `user_agr` | `nvarchar(50)` |  |  |
| 12 | `fec_agr` | `datetime` |  |  |
| 13 | `user_mod` | `nvarchar(50)` |  |  |
| 14 | `fec_mod` | `nvarchar(50)` |  |  |
| 15 | `Fecha` | `datetime` |  |  |
| 16 | `IdPedidoEnc` | `int` |  |  |
| 17 | `IdPedidoDet` | `int` |  |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_despacho_det`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Despacho_Detalle
AS
SELECT     ddet.IdDespachoEnc, ddet.IdDespachoDet, p.codigo, p.nombre AS Producto, pp.nombre AS Presentacion, pe.nombre AS Estado, um.Nombre AS UnidadMedida, 
                      b.descripcion AS Ubicacion, u.activo, u.IdPickingUbic, ddet.user_agr, ddet.fec_agr, ddet.user_mod, ddet.fec_mod, ddet.Fecha, pdet.IdPedidoEnc, 
                      pdet.IdPedidoDet
FROM         dbo.trans_despacho_det AS ddet INNER JOIN
                      dbo.trans_picking_ubic AS u ON ddet.IdPickingUbic = u.IdPickingUbic INNER JOIN
                      dbo.trans_picking_det AS d ON u.IdPickingDet = d.IdPickingDet INNER JOIN
                      dbo.bodega_ubicacion AS b ON u.IdUbicacion = b.IdUbicacion INNER JOIN
                      dbo.trans_picking_enc AS enc ON d.IdPickingEnc = enc.IdPickingEnc INNER JOIN
                      dbo.propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                      dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                      dbo.trans_pe_det AS pdet ON d.IdPedidoDet = pdet.IdPedidoDet INNER JOIN
                      dbo.trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc INNER JOIN
                      dbo.producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                      dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                      dbo.producto_presentacion AS pp ON pdet.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                      dbo.producto_estado AS pe ON pdet.IdEstado = pe.IdEstado INNER JOIN
                      dbo.unidad_medida AS um ON pdet.IdUnidadMedidaBasica = um.IdUnidadMedida
```
