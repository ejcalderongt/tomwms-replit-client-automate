---
id: db-brain-table-indicador-enc
type: db-table
title: dbo.indicador_enc
schema: dbo
name: indicador_enc
kind: table
rows: 4
modify_date: 2023-06-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.indicador_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2023-06-18 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdIndicadorEnc` | `int` |  |  |
| 2 | `descripcion` | `nvarchar(50)` |  |  |
| 3 | `estado` | `nvarchar(2)` |  |  |
| 4 | `fec_agr` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_indicador_enc` | CLUSTERED · **PK** | IdIndicadorEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

