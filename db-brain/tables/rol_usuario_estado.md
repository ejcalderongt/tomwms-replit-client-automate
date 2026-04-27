---
id: db-brain-table-rol-usuario-estado
type: db-table
title: dbo.rol_usuario_estado
schema: dbo
name: rol_usuario_estado
kind: table
rows: 14
modify_date: 2024-10-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.rol_usuario_estado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14 |
| Schema modify_date | 2024-10-28 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRolUsuarioEstado` | `int` |  |  |
| 2 | `IdRolUsuario` | `int` | ✓ |  |
| 3 | `IdPropietario` | `int` | ✓ |  |
| 4 | `IdEstadoOrigen` | `int` | ✓ |  |
| 5 | `IdEstadoDestino` | `int` | ✓ |  |
| 6 | `Permitir` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_rol_usuario_estado` | CLUSTERED · **PK** | IdRolUsuarioEstado |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Rol_Usuario_Estado` (view)

