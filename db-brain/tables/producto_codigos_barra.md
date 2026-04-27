---
id: db-brain-table-producto-codigos-barra
type: db-table
title: dbo.producto_codigos_barra
schema: dbo
name: producto_codigos_barra
kind: table
rows: 310
modify_date: 2022-06-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_codigos_barra`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 310 |
| Schema modify_date | 2022-06-22 |
| Columnas | 9 |
| Índices | 2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoCodigoBarra` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdProveedor` | `int` |  |  |
| 4 | `codigo_barra` | `nvarchar(35)` |  |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_codigos_barra` | CLUSTERED · **PK** | IdProductoCodigoBarra |
| `IX_producto_codigos_barra_20220622` | NONCLUSTERED | codigo_barra, IdProducto |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `VW_ProductoSI` (view)

