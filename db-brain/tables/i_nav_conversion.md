---
id: db-brain-table-i-nav-conversion
type: db-table
title: dbo.i_nav_conversion
schema: dbo
name: i_nav_conversion
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_conversion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConversion` | `int` |  |  |
| 2 | `Item_No` | `nvarchar(50)` | ✓ |  |
| 3 | `Code` | `nvarchar(50)` | ✓ |  |
| 4 | `Qty_per_Unit_of_Measure` | `float` | ✓ |  |
| 5 | `Height` | `float` | ✓ |  |
| 6 | `Width` | `float` | ✓ |  |
| 7 | `Length` | `float` | ✓ |  |
| 8 | `Cubage` | `float` | ✓ |  |
| 9 | `Weight` | `float` | ✓ |  |
| 10 | `Package_Type` | `nvarchar(50)` | ✓ |  |
| 11 | `ItemUnitOfMeasure` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_conversion` | CLUSTERED · **PK** | IdConversion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

