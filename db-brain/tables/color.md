---
id: db-brain-table-color
type: db-table
title: dbo.color
schema: dbo
name: color
kind: table
rows: 0
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.color`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-05-13 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdColor` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` |  |  |
| 3 | `CodigoHex` | `nvarchar(7)` | ✓ |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `fec_agr` | `datetime` |  |  |
| 6 | `user_agr` | `nvarchar(25)` |  |  |
| 7 | `fec_mod` | `datetime` |  |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__color__E83D55CB9C480390` | CLUSTERED · **PK** | IdColor |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_talla_color` (`FK__producto___IdCol__599E6480`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

