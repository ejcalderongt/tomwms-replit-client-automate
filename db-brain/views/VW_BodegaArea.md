---
id: db-brain-view-vw-bodegaarea
type: db-view
title: dbo.VW_BodegaArea
schema: dbo
name: VW_BodegaArea
kind: view
modify_date: 2016-04-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_BodegaArea`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-04-21 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdArea` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `Descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `sistema` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `Codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_BodegaArea
AS
SELECT        dbo.bodega_area.*
FROM            dbo.bodega RIGHT OUTER JOIN
                         dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega
```
