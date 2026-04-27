---
id: db-brain-table-tipo-actualizacion-costo
type: db-table
title: dbo.tipo_actualizacion_costo
schema: dbo
name: tipo_actualizacion_costo
kind: table
rows: 3
modify_date: 2016-04-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_actualizacion_costo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2016-04-05 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoActualizacionCosto` | `int` |  |  |
| 2 | `NombreActualizacionCosto` | `nvarchar(50)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_actualizacion_costo` | CLUSTERED · **PK** | IdTipoActualizacionCosto |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `propietarios` (`FK_propietarios_tipo_actualizacion_costo`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

