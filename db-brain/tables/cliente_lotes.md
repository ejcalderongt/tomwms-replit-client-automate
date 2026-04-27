---
id: db-brain-table-cliente-lotes
type: db-table
title: dbo.cliente_lotes
schema: dbo
name: cliente_lotes
kind: table
rows: 0
modify_date: 2025-07-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.cliente_lotes`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-07-13 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdClienteLote` | `int` |  |  |
| 2 | `IdCliente` | `int` |  |  |
| 3 | `Lote` | `nvarchar(50)` | ✓ |  |
| 4 | `IdProductoEstado` | `int` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `bloquear` | `bit` | ✓ |  |
| 11 | `IdProducto` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_lote` | CLUSTERED · **PK** | IdClienteLote |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

