---
id: db-brain-table-rol
type: db-table
title: dbo.rol
schema: dbo
name: rol
kind: table
rows: 8
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.rol`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8 |
| Schema modify_date | 2018-01-19 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRol` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `registrar_clave_autorizacion` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_rol_1` | CLUSTERED · **PK** | IdRol |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_rol_empresa` → `empresa`

### Entrantes (otra tabla → esta)

- `menu_rol` (`FK_menu_rol_rol`)
- `rol_bodega` (`FK_rol_bodega_rol`)
- `rol_menu` (`FK_rol_menu_rol`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_TransUbicacionHhEnc` (view)

