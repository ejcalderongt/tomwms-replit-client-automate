---
id: db-brain-view-vw-operador
type: db-view
title: dbo.VW_Operador
schema: dbo
name: VW_Operador
kind: view
modify_date: 2022-06-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-13 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 3 | `nombres` | `nvarchar(100)` | ✓ |  |
| 4 | `apellidos` | `nvarchar(100)` | ✓ |  |
| 5 | `Dirección` | `nvarchar(50)` | ✓ |  |
| 6 | `Teléfono` | `nvarchar(50)` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |
| 8 | `recibe` | `bit` | ✓ |  |
| 9 | `ubica` | `bit` | ✓ |  |
| 10 | `transporta` | `bit` | ✓ |  |
| 11 | `pickea` | `bit` | ✓ |  |
| 12 | `verifica` | `bit` | ✓ |  |
| 13 | `montacarga` | `bit` |  |  |

## Consume

- `empresa`
- `operador`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Operador]
AS
SELECT        o.IdOperador AS Código, e.nombre AS Empresa, o.nombres, o.apellidos, o.direccion AS Dirección, o.telefono AS Teléfono, o.activo, o.recibe, o.ubica, o.transporta, o.pickea, o.verifica, o.montacarga
FROM            dbo.operador AS o INNER JOIN
                         dbo.empresa AS e ON o.IdEmpresa = e.IdEmpresa
```
