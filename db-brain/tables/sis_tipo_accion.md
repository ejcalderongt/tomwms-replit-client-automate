---
id: db-brain-table-sis-tipo-accion
type: db-table
title: dbo.sis_tipo_accion
schema: dbo
name: sis_tipo_accion
kind: table
rows: 2
modify_date: 2016-07-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sis_tipo_accion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2016-07-07 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoAccion` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_sis_tipo_accion` | CLUSTERED · **PK** | IdTipoAccion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_rellenado` (`FK_producto_rellenado_sis_tipo_accion`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

