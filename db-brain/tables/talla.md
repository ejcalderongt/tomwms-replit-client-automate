---
id: db-brain-table-talla
type: db-table
title: dbo.talla
schema: dbo
name: talla
kind: table
rows: 0
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.talla`

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
| 1 | `IdTalla` | `int` |  |  |
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
| `PK__talla__E95BE94384391F78` | CLUSTERED · **PK** | IdTalla |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_talla_color` (`FK__producto___IdTal__58AA4047`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

