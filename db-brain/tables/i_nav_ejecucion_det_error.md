---
id: db-brain-table-i-nav-ejecucion-det-error
type: db-table
title: dbo.i_nav_ejecucion_det_error
schema: dbo
name: i_nav_ejecucion_det_error
kind: table
rows: 4021
modify_date: 2022-03-15
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ejecucion_det_error`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.021 |
| Schema modify_date | 2022-03-15 |
| Columnas | 12 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idejecuciondet` | `int` |  |  |
| 2 | `idejecucionenc` | `int` | ✓ |  |
| 3 | `idnavconfigdet` | `int` | ✓ |  |
| 4 | `error` | `nvarchar(1000)` | ✓ |  |
| 5 | `fecha` | `datetime` | ✓ |  |
| 6 | `referencia` | `nvarchar(1000)` | ✓ |  |
| 7 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `no_linea` | `int` | ✓ |  |
| 10 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 11 | `umbas` | `nvarchar(50)` | ✓ |  |
| 12 | `codigo_presentacion` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ejecucion_det_error` | CLUSTERED · **PK** | idejecuciondet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_PRC` (stored_procedure)

