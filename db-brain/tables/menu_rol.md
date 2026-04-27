---
id: db-brain-table-menu-rol
type: db-table
title: dbo.menu_rol
schema: dbo
name: menu_rol
kind: table
rows: 863
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.menu_rol`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 863 |
| Schema modify_date | 2024-07-02 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMenuRol` | `int` |  |  |
| 2 | `IdMenu` | `nvarchar(50)` |  |  |
| 3 | `IdRol` | `int` |  |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` |  |  |
| 9 | `visible` | `bit` |  |  |
| 10 | `leer` | `bit` |  |  |
| 11 | `modificar` | `bit` |  |  |
| 12 | `eliminar` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_menu_rol` | CLUSTERED · **PK** | IdMenuRol |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_menu_rol_rol` → `rol`

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

