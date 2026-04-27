---
id: db-brain-view-vw-inventario-prg-por-tipo
type: db-view
title: dbo.VW_Inventario_prg_por_tipo
schema: dbo
name: VW_Inventario_prg_por_tipo
kind: view
modify_date: 2018-09-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Inventario_prg_por_tipo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-09-03 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `NombreTipoProducto` | `nvarchar(50)` | ✓ |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `Teórico` | `float` | ✓ |  |
| 4 | `Contado` | `float` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_tipo`
- `trans_inv_ciclico`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Inventario_prg_por_tipo]
AS
SELECT     dbo.producto_tipo.NombreTipoProducto, dbo.trans_inv_ciclico.idinventarioenc, SUM(dbo.trans_inv_ciclico.cant_stock) AS Teórico, 
                      SUM(dbo.trans_inv_ciclico.cantidad) AS Contado
FROM         dbo.producto_tipo INNER JOIN
                      dbo.producto ON dbo.producto_tipo.IdTipoProducto = dbo.producto.IdTipoProducto INNER JOIN
                      dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                      dbo.trans_inv_ciclico ON dbo.producto_bodega.IdProductoBodega = dbo.trans_inv_ciclico.IdProductoBodega
GROUP BY dbo.producto_tipo.NombreTipoProducto, dbo.trans_inv_ciclico.idinventarioenc
```
