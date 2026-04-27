---
id: db-brain-table-trans-series-doc
type: db-table
title: dbo.trans_series_doc
schema: dbo
name: trans_series_doc
kind: table
rows: 0
modify_date: 2022-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_series_doc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-07-02 |
| Columnas | 13 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransSerieDoc` | `int` |  |  |
| 2 | `Serie` | `nvarchar(50)` | ✓ |  |
| 3 | `Tipo_Doc` | `nvarchar(50)` | ✓ |  |
| 4 | `IdTipoTrans` | `int` | ✓ |  |
| 5 | `Inicial` | `int` | ✓ |  |
| 6 | `Final` | `int` | ✓ |  |
| 7 | `Actual` | `int` | ✓ |  |
| 8 | `Activo` | `bit` | ✓ |  |
| 9 | `IdBodega` | `int` | ✓ |  |
| 10 | `UserAgr` | `nvarchar(50)` | ✓ |  |
| 11 | `FecAgr` | `datetime` | ✓ |  |
| 12 | `UserMod` | `nvarchar(50)` | ✓ |  |
| 13 | `FecMod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_series_doc` | CLUSTERED · **PK** | IdTransSerieDoc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

