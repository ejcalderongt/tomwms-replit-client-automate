---
id: db-brain-table-menu-sistema
type: db-table
title: dbo.menu_sistema
schema: dbo
name: menu_sistema
kind: table
rows: 289
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.menu_sistema`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 289 |
| Schema modify_date | 2023-08-21 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMenu` | `nvarchar(50)` |  |  |
| 2 | `titulo` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre_lgco` | `nvarchar(50)` | ✓ |  |
| 4 | `nivel` | `int` | ✓ |  |
| 5 | `padre` | `nvarchar(50)` | ✓ |  |
| 6 | `solicitar_clave_autorizacion` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_menu_sistema` | CLUSTERED · **PK** | IdMenu |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `rol_menu` (`FK_rol_menu_menu_sistema`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

