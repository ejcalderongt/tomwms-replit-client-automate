---
id: db-brain-table-prospecto
type: db-table
title: dbo.Prospecto
schema: dbo
name: Prospecto
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Prospecto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-29 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `ProspectoId` | `int` |  | ✓ |
| 2 | `Nombre` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Prospect__C10D7A7F6DD716C9` | CLUSTERED · **PK** | ProspectoId |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `Contacto` (`FK__Contacto__Prospe__68F5B6A9`)
- `Infraestructura` (`FK__Infraestr__Prosp__633CDD53`)
- `Organizacion` (`FK__Organizac__Prosp__661949FE`)
- `Propuesta` (`FK__Propuesta__Prosp__606070A8`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

