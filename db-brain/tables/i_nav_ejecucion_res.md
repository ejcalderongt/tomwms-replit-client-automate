---
id: db-brain-table-i-nav-ejecucion-res
type: db-table
title: dbo.i_nav_ejecucion_res
schema: dbo
name: i_nav_ejecucion_res
kind: table
rows: 7760
modify_date: 2023-02-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ejecucion_res`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 7.760 |
| Schema modify_date | 2023-02-14 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idejecucionres` | `int` |  |  |
| 2 | `idejecucionenc` | `int` | ✓ |  |
| 3 | `idnavconfigdet` | `int` | ✓ |  |
| 4 | `registros_ws` | `int` | ✓ |  |
| 5 | `registros_ti` | `int` | ✓ |  |
| 6 | `registros_wms` | `int` | ✓ |  |
| 7 | `exitosa` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ejecucion_res` | CLUSTERED · **PK** | idejecucionres |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_PRC` (stored_procedure)

