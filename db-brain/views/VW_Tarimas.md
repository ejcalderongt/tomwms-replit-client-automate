---
id: db-brain-view-vw-tarimas
type: db-view
title: dbo.VW_Tarimas
schema: dbo
name: VW_Tarimas
kind: view
modify_date: 2016-07-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tarimas`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-07-18 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTarima` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdTipoTarima` | `int` | ✓ |  |
| 4 | `nombreTipoTarima` | `nvarchar(50)` | ✓ |  |
| 5 | `codigo` | `nvarchar(50)` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` |  |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `disponible` | `bit` | ✓ |  |

## Consume

- `tarimas`
- `tipo_tarima`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Tarimas
AS
SELECT        a.IdTarima, a.IdEmpresa, a.IdTipoTarima, b.Nombre AS nombreTipoTarima, a.codigo, a.user_agr, a.fec_agr, a.user_mod, a.fec_mod, a.activo, a.disponible
FROM            dbo.tarimas AS a INNER JOIN
                         dbo.tipo_tarima AS b ON b.IdTipoTarima = a.IdTipoTarima
WHERE        (a.disponible = 1)
```
