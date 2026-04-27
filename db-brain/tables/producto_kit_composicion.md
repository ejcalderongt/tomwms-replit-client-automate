---
id: db-brain-table-producto-kit-composicion
type: db-table
title: dbo.producto_kit_composicion
schema: dbo
name: producto_kit_composicion
kind: table
rows: 0
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_kit_composicion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-10-07 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoKitComposicion` | `int` |  |  |
| 2 | `IdProductoPadre` | `int` |  |  |
| 3 | `IdProductoHijo` | `int` |  |  |
| 4 | `IdUnidadMedidaBasicaPadre` | `int` | ✓ |  |
| 5 | `IdUnidadMedidaBasicaHijo` | `int` | ✓ |  |
| 6 | `Cantidad` | `float` | ✓ |  |
| 7 | `fecha_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `IdBodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_kit_composicion` | CLUSTERED · **PK** | IdProductoKitComposicion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

