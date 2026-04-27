---
id: db-brain-table-trans-inv-ne
type: db-table
title: dbo.trans_inv_ne
schema: dbo
name: trans_inv_ne
kind: table
rows: 1
modify_date: 2019-04-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_ne`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2019-04-08 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarione` | `int` |  |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `idproducto` | `int` | ✓ |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(50)` | ✓ |  |
| 6 | `cantidad` | `float` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `usr_agr` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_ne` | CLUSTERED · **PK** | idinventarione |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

