---
id: db-brain-view-vw-pe-con-diferencias
type: db-view
title: dbo.VW_PE_CON_DIFERENCIAS
schema: dbo
name: VW_PE_CON_DIFERENCIAS
kind: view
modify_date: 2021-09-20
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PE_CON_DIFERENCIAS`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-09-20 |
| Columnas | 21 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `ORDENPEDIDO` | `bigint` | ✓ |  |
| 2 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 4 | `Cantidad` | `float` | ✓ |  |
| 5 | `cant_despachada` | `float` | ✓ |  |
| 6 | `PRESENTACION` | `nvarchar(50)` | ✓ |  |
| 7 | `DIFERENCIA` | `float` | ✓ |  |
| 8 | `IdPropietarioBodega` | `int` | ✓ |  |
| 9 | `BODEGA` | `nvarchar(50)` | ✓ |  |
| 10 | `PROPIETARIO` | `nvarchar(100)` |  |  |
| 11 | `IdTipoPedido` | `int` | ✓ |  |
| 12 | `NOMBRE_PEDIDO` | `nvarchar(250)` | ✓ |  |
| 13 | `IdProductoBodega` | `int` |  |  |
| 14 | `IdPresentacion` | `int` | ✓ |  |
| 15 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 16 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 17 | `estado` | `nvarchar(20)` | ✓ |  |
| 18 | `activo` | `bit` | ✓ |  |
| 19 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 20 | `IdPropietario` | `int` |  |  |
| 21 | `IdBodega` | `int` | ✓ |  |

## Consume

- `bodega`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_pe_tipo`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view VW_Pe_Con_Diferencias as

SELECT        enc.no_documento AS ORDENPEDIDO, det.codigo_producto, det.nombre_producto, det.Cantidad, det.cant_despachada, CASE WHEN ISNULL(DET.IdPresentacion, 0) = 0 THEN um.Nombre ELSE pr.nombre END AS PRESENTACION, 
                         det.cant_despachada - det.Cantidad AS DIFERENCIA, enc.IdPropietarioBodega, dbo.bodega.nombre_comercial AS BODEGA, PROP.nombre_comercial AS PROPIETARIO, enc.IdTipoPedido, TIPO.Descripcion AS NOMBRE_PEDIDO, 
                         det.IdProductoBodega, det.IdPresentacion, det.IdUnidadMedidaBasica, um.Nombre as UMBas,enc.estado, enc.activo, enc.Fecha_Pedido, PROP.IdPropietario, enc.IdBodega
FROM            dbo.trans_pe_enc AS enc INNER JOIN
                         dbo.trans_pe_det AS det ON enc.IdPedidoEnc = det.IdPedidoEnc INNER JOIN
                         dbo.trans_pe_tipo AS TIPO ON enc.IdTipoPedido = TIPO.IdTipoPedido INNER JOIN
                         dbo.propietario_bodega AS PROP_BD ON enc.IdPropietarioBodega = PROP_BD.IdPropietarioBodega INNER JOIN
                         dbo.bodega ON PROP_BD.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.propietarios AS PROP ON PROP_BD.IdPropietario = PROP.IdPropietario INNER JOIN
                         dbo.unidad_medida AS um ON um.IdUnidadMedida = det.IdUnidadMedidaBasica LEFT OUTER JOIN
                         dbo.producto_presentacion AS pr ON det.IdPresentacion = pr.IdPresentacion
WHERE        (enc.activo = 1) AND (enc.estado <> 'Anulado')
```
