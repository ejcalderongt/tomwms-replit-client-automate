---
id: db-brain-table-sis-prioridad-tarea-hh
type: db-table
title: dbo.sis_prioridad_tarea_hh
schema: dbo
name: sis_prioridad_tarea_hh
kind: table
rows: 4
modify_date: 2016-06-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sis_prioridad_tarea_hh`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2016-06-08 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPrioridad` | `int` |  |  |
| 2 | `descripcion` | `nvarchar(50)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_sis_prioridad_tarea_hh` | CLUSTERED · **PK** | IdPrioridad |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `tarea_hh` (`FK_tarea_hh_sis_prioridad_tarea_hh`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_tareas_hh` (view)

