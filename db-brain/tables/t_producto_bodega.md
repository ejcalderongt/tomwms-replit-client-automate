---
id: db-brain-table-t-producto-bodega
type: db-table
title: dbo.t_producto_bodega
schema: dbo
name: t_producto_bodega
kind: table
rows: 42357
modify_date: 2019-05-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.t_producto_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 42.357 |
| Schema modify_date | 2019-05-21 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBodega` | `int` |  |  |
| 2 | `IdProducto` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `sistema` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` |  |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

