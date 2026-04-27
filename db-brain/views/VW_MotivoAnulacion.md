---
id: db-brain-view-vw-motivoanulacion
type: db-view
title: dbo.VW_MotivoAnulacion
schema: dbo
name: VW_MotivoAnulacion
kind: view
modify_date: 2017-06-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_MotivoAnulacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-06-01 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 2 | `IdMotivoAnulacion` | `int` |  |  |
| 3 | `IdEmpresa` | `int` |  |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Consume

- `empresa`
- `motivo_anulacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_MotivoAnulacion
AS
SELECT     e.nombre AS Empresa, d.*
FROM         dbo.motivo_anulacion AS d LEFT OUTER JOIN
                      dbo.empresa AS e ON d.IdEmpresa = e.IdEmpresa
```
