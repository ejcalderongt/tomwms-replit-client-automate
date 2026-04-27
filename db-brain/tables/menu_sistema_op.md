---
id: db-brain-table-menu-sistema-op
type: db-table
title: dbo.menu_sistema_op
schema: dbo
name: menu_sistema_op
kind: table
rows: 19
modify_date: 2016-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.menu_sistema_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 19 |
| Schema modify_date | 2016-09-12 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMenuSistemaOP` | `nvarchar(50)` |  |  |
| 2 | `IdTipoTarea` | `int` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `Nivel` | `int` | ✓ |  |
| 5 | `Padre` | `nvarchar(50)` | ✓ |  |
| 6 | `Posicion` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_menu_operador` | CLUSTERED · **PK** | IdMenuSistemaOP |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_menu_sistema_op_sis_tipo_tarea` → `sis_tipo_tarea`

### Entrantes (otra tabla → esta)

- `menu_rol_op` (`FK_menu_rol_op_menu_sistema_op`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_Tareas_Operador` (view)

