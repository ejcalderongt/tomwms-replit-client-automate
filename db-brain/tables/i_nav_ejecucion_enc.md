---
id: db-brain-table-i-nav-ejecucion-enc
type: db-table
title: dbo.i_nav_ejecucion_enc
schema: dbo
name: i_nav_ejecucion_enc
kind: table
rows: 7762
modify_date: 2023-02-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ejecucion_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 7.762 |
| Schema modify_date | 2023-02-14 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idejecucionenc` | `int` |  |  |
| 2 | `idnavconfigenc` | `int` | ✓ |  |
| 3 | `fecha` | `datetime` | ✓ |  |
| 4 | `exitosa` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ejecucion_enc` | CLUSTERED · **PK** | idejecucionenc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_PRC` (stored_procedure)

