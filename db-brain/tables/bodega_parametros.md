---
id: db-brain-table-bodega-parametros
type: db-table
title: dbo.bodega_parametros
schema: dbo
name: bodega_parametros
kind: table
rows: 6
modify_date: 2017-05-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_parametros`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2017-05-04 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametroBodega` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `Descripcion` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_bodega_parametros` | CLUSTERED · **PK** | IdParametroBodega |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

