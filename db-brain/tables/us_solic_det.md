---
id: db-brain-table-us-solic-det
type: db-table
title: dbo.us_solic_det
schema: dbo
name: us_solic_det
kind: table
rows: 0
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.us_solic_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-10-07 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdSugerenciaSolicDet` | `int` |  |  |
| 2 | `IdSugerenciaSolicEnc` | `int` | ✓ |  |
| 3 | `IdTramo` | `int` | ✓ |  |
| 4 | `Columna` | `int` | ✓ |  |
| 5 | `Nivel` | `int` | ✓ |  |
| 6 | `IdUbicacion` | `int` | ✓ |  |
| 7 | `Orientacion` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_us_solic_det` | CLUSTERED · **PK** | IdSugerenciaSolicDet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

