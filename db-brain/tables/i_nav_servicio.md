---
id: db-brain-table-i-nav-servicio
type: db-table
title: dbo.i_nav_servicio
schema: dbo
name: i_nav_servicio
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_servicio`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdServicio` | `int` |  |  |
| 2 | `codigo_servicio` | `nvarchar(50)` |  |  |
| 3 | `descripcion` | `nvarchar(500)` |  |  |
| 4 | `nemonico` | `nvarchar(10)` |  |  |
| 5 | `codigo_rubro` | `int` | ✓ |  |
| 6 | `activo` | `bit` |  |  |
| 7 | `procesado_wms` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_servicio` | CLUSTERED · **PK** | IdServicio |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

