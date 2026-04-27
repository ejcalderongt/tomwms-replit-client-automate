---
id: db-brain-table-i-nav-bodega
type: db-table
title: dbo.i_nav_bodega
schema: dbo
name: i_nav_bodega
kind: table
rows: 8
modify_date: 2017-10-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8 |
| Schema modify_date | 2017-10-27 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `bodega_code` | `nvarchar(50)` |  |  |
| 2 | `bodega_name` | `nvarchar(150)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_bodega` | CLUSTERED · **PK** | bodega_code |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

