---
id: db-brain-table-rol-menu
type: db-table
title: dbo.rol_menu
schema: dbo
name: rol_menu
kind: table
rows: 0
modify_date: 2016-05-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.rol_menu`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-05-06 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMenuRol` | `int` |  |  |
| 2 | `IdRol` | `int` |  |  |
| 3 | `IdMenu` | `nvarchar(50)` |  |  |
| 4 | `visible` | `int` |  |  |
| 5 | `usuario_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_menu_rol_1` | CLUSTERED · **PK** | IdMenuRol |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_rol_menu_menu_sistema` → `menu_sistema`
- `FK_rol_menu_rol` → `rol`

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

