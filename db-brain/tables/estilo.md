---
id: db-brain-table-estilo
type: db-table
title: dbo.estilo
schema: dbo
name: estilo
kind: table
rows: 0
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.estilo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-05-13 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEstilo` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` |  |  |
| 3 | `Descripcion` | `nvarchar(100)` | ✓ |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `fec_agr` | `datetime` |  |  |
| 6 | `user_agr` | `nvarchar(25)` |  |  |
| 7 | `fec_mod` | `datetime` |  |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__estilo__F11D5AFF404E0DE0` | CLUSTERED · **PK** | IdEstilo |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

