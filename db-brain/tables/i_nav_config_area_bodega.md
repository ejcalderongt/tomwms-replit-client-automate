---
id: db-brain-table-i-nav-config-area-bodega
type: db-table
title: dbo.i_nav_config_area_bodega
schema: dbo
name: i_nav_config_area_bodega
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_config_area_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Idnavconfigareabodega` | `int` |  |  |
| 2 | `IdArea` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `codigo_bodega_erp` | `nvarchar(50)` |  |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_config_area_bodega` | CLUSTERED · **PK** | Idnavconfigareabodega |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

