---
id: db-brain-table-regla-ubic-sel-item
type: db-table
title: dbo.regla_ubic_sel_item
schema: dbo
name: regla_ubic_sel_item
kind: table
rows: 9
modify_date: 2017-08-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_sel_item`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 9 |
| Schema modify_date | 2017-08-07 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idRegla` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 3 | `Activo` | `int` | ✓ |  |
| 4 | `Orden` | `int` | ✓ |  |
| 5 | `Tipo` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_orden_item` | CLUSTERED · **PK** | idRegla |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

