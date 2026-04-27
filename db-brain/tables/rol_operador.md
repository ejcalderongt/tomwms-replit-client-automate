---
id: db-brain-table-rol-operador
type: db-table
title: dbo.rol_operador
schema: dbo
name: rol_operador
kind: table
rows: 14
modify_date: 2016-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.rol_operador`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14 |
| Schema modify_date | 2016-09-12 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRolOperador` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_rol_operador` | CLUSTERED · **PK** | IdRolOperador |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `menu_rol_op` (`FK_menu_rol_op_rol_operador`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

