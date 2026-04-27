---
id: db-brain-table-stock-picking20250624
type: db-table
title: dbo.stock_picking20250624
schema: dbo
name: stock_picking20250624
kind: table
rows: 14
modify_date: 2025-06-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_picking20250624`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14 |
| Schema modify_date | 2025-06-24 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idstock` | `int` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `lote` | `nvarchar(50)` |  |  |
| 5 | `fecha_vence` | `datetime` | ✓ |  |
| 6 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 7 | `cantidad` | `float` |  |  |
| 8 | `IdPedido` | `bigint` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

