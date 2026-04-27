---
id: db-brain-table-i-nav-config-producto-estado
type: db-table
title: dbo.i_nav_config_producto_estado
schema: dbo
name: i_nav_config_producto_estado
kind: table
rows: 0
modify_date: 2023-10-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_config_producto_estado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-16 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnavconfigproductoestado` | `int` |  |  |
| 2 | `IdProductoEstado` | `int` | ✓ |  |
| 3 | `codigo_bodega_erp` | `nvarchar(50)` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_config_producto_estado_1` | CLUSTERED · **PK** | idnavconfigproductoestado |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

