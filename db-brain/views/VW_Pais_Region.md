---
id: db-brain-view-vw-pais-region
type: db-view
title: dbo.VW_Pais_Region
schema: dbo
name: VW_Pais_Region
kind: view
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pais_Region`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-05-27 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPais` | `int` |  |  |
| 2 | `NombrePais` | `nvarchar(50)` | ✓ |  |
| 3 | `IdRegion` | `int` |  |  |
| 4 | `NombreRegion` | `varchar(25)` | ✓ |  |

## Consume

- `pais_region`
- `paises`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Pais_Region
AS
SELECT        dbo.paises.IdPais, dbo.paises.NOMBRE AS NombrePais, dbo.pais_region.IdRegion, dbo.pais_region.Nombre AS NombreRegion
FROM            dbo.pais_region INNER JOIN
                         dbo.paises ON dbo.pais_region.IdPais = dbo.paises.IdPais
```
