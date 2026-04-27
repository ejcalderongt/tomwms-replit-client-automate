---
id: db-brain-view-vw-codigobarra-oc
type: db-view
title: dbo.VW_CodigoBarra_OC
schema: dbo
name: VW_CodigoBarra_OC
kind: view
modify_date: 2017-08-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_CodigoBarra_OC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-08-16 |
| Columnas | 2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Codigo_barra` | `nvarchar(35)` | ✓ |  |
| 2 | `IdOrdenCompraEnc` | `int` |  |  |

## Consume

- `producto`
- `producto_bodega`
- `trans_oc_det`
- `trans_oc_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_CodigoBarra_OC
AS
SELECT        dbo.producto.codigo_barra AS Codigo_barra, dbo.trans_oc_enc.IdOrdenCompraEnc
FROM            dbo.producto INNER JOIN
                         dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                         dbo.trans_oc_enc INNER JOIN
                         dbo.trans_oc_det ON dbo.trans_oc_enc.IdOrdenCompraEnc = dbo.trans_oc_det.IdOrdenCompraEnc ON 
                         dbo.producto_bodega.IdProductoBodega = dbo.trans_oc_det.IdProductoBodega
```
