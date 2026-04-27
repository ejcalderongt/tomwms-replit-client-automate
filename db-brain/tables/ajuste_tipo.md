---
id: db-brain-table-ajuste-tipo
type: db-table
title: dbo.ajuste_tipo
schema: dbo
name: ajuste_tipo
kind: table
rows: 6
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.ajuste_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2018-01-19 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idtipoajuste` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `modifica_lote` | `bit` | ✓ |  |
| 4 | `momdifica_vencimiento` | `bit` | ✓ |  |
| 5 | `modifica_cantidad` | `bit` | ✓ |  |
| 6 | `modifica_peso` | `bit` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_ajuste` | CLUSTERED · **PK** | idtipoajuste |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Ajustes` (view)

