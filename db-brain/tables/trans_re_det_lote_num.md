---
id: db-brain-table-trans-re-det-lote-num
type: db-table
title: dbo.trans_re_det_lote_num
schema: dbo
name: trans_re_det_lote_num
kind: table
rows: 180181
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_det_lote_num`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 180.181 |
| Schema modify_date | 2022-12-17 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdLoteNum` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` | ✓ |  |
| 3 | `IdProductoBodega` | `int` | ✓ |  |
| 4 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Lote` | `nvarchar(50)` | ✓ |  |
| 6 | `Lote_Numerico` | `int` | ✓ |  |
| 7 | `FechaIngreso` | `date` | ✓ |  |
| 8 | `Cantidad` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_lote_num` | CLUSTERED · **PK** | IdLoteNum |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

