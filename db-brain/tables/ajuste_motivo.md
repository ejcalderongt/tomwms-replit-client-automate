---
id: db-brain-table-ajuste-motivo
type: db-table
title: dbo.ajuste_motivo
schema: dbo
name: ajuste_motivo
kind: table
rows: 8
modify_date: 2018-08-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.ajuste_motivo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8 |
| Schema modify_date | 2018-08-28 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idmotivoajuste` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `fec_agr` | `datetime` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_mod` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |
| 8 | `sistema` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_ajuste_momtivo` | CLUSTERED · **PK** | idmotivoajuste |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Ajustes` (view)

