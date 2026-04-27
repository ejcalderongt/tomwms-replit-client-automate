---
id: db-brain-table-trans-movimiento-pallet
type: db-table
title: dbo.trans_movimiento_pallet
schema: dbo
name: trans_movimiento_pallet
kind: table
rows: 245
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_movimiento_pallet`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 245 |
| Schema modify_date | 2023-08-21 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idmovimientopallet` | `int` |  |  |
| 2 | `idbodega` | `int` | ✓ |  |
| 3 | `lp_origen` | `nvarchar(50)` | ✓ |  |
| 4 | `lp_destino` | `nvarchar(50)` | ✓ |  |
| 5 | `orientacion` | `nvarchar(50)` | ✓ |  |
| 6 | `fecha` | `date` | ✓ |  |
| 7 | `idusuario` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_movimiento_pallet` | CLUSTERED · **PK** | idmovimientopallet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

