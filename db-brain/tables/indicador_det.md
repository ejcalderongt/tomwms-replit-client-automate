---
id: db-brain-table-indicador-det
type: db-table
title: dbo.indicador_det
schema: dbo
name: indicador_det
kind: table
rows: 4
modify_date: 2023-06-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.indicador_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2023-06-18 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdIndicadorEnc` | `int` |  |  |
| 2 | `IdIndicadorDet` | `int` |  |  |
| 3 | `descripcion` | `nvarchar(50)` |  |  |
| 4 | `estado` | `nvarchar(2)` |  |  |
| 5 | `fec_agr` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_indicador_det` | CLUSTERED · **PK** | IdIndicadorDet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

