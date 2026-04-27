---
id: db-brain-table-propuesta
type: db-table
title: dbo.Propuesta
schema: dbo
name: Propuesta
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Propuesta`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-29 |
| Columnas | 5 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `PropuestaId` | `int` |  | ✓ |
| 2 | `ProspectoId` | `int` | ✓ |  |
| 3 | `Giro_Negocio` | `nvarchar(100)` | ✓ |  |
| 4 | `Posible_Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Proyecto` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Propuest__4B06786237E890B9` | CLUSTERED · **PK** | PropuestaId |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__Propuesta__Prosp__606070A8` → `Prospecto`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

