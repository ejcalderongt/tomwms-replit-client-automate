---
id: db-brain-table-sis-obs-log
type: db-table
title: dbo.sis_obs_log
schema: dbo
name: sis_obs_log
kind: table
rows: 3
modify_date: 2016-07-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sis_obs_log`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2016-07-06 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdObservacion` | `int` |  |  |
| 2 | `Descripcion` | `nvarchar(max)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_sis_obs_log` | CLUSTERED · **PK** | IdObservacion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `transacciones_log` (`FK_transacciones_log_sis_obs_log`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

