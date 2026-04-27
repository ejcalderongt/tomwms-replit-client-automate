---
id: db-brain-table-menu-rol-op
type: db-table
title: dbo.menu_rol_op
schema: dbo
name: menu_rol_op
kind: table
rows: 187
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.menu_rol_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 187 |
| Schema modify_date | 2023-08-21 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMenuSistemaOP` | `nvarchar(50)` |  |  |
| 2 | `IdRolOperador` | `int` |  |  |
| 3 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 4 | `fec_agr` | `datetime` | ✓ |  |
| 5 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_mod` | `datetime` | ✓ |  |
| 7 | `visible` | `bit` |  |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_menu_rol_op` | CLUSTERED · **PK** | IdMenuSistemaOP, IdRolOperador |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_menu_rol_op_menu_sistema_op` → `menu_sistema_op`
- `FK_menu_rol_op_rol_operador` → `rol_operador`

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_Tareas_Operador` (view)

