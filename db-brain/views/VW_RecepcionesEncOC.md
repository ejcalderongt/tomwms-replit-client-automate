---
id: db-brain-view-vw-recepcionesencoc
type: db-view
title: dbo.VW_RecepcionesEncOC
schema: dbo
name: VW_RecepcionesEncOC
kind: view
modify_date: 2017-06-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RecepcionesEncOC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-06-06 |
| Columnas | 3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` |  |  |
| 2 | `IdOrdenCompraEnc` | `int` |  |  |
| 3 | `Estado` | `nvarchar(20)` | ✓ |  |

## Consume

- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_RecepcionesEncOC
AS
SELECT     dbo.trans_re_oc.IdRecepcionEnc, dbo.trans_re_oc.IdOrdenCompraEnc, dbo.trans_re_enc.estado AS Estado
FROM         dbo.trans_re_oc INNER JOIN
                      dbo.trans_re_enc ON dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc
```
