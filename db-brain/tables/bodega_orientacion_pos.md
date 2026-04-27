---
id: db-brain-table-bodega-orientacion-pos
type: db-table
title: dbo.bodega_orientacion_pos
schema: dbo
name: bodega_orientacion_pos
kind: table
rows: 4
modify_date: 2018-02-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_orientacion_pos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2018-02-06 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrientacionPos` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_bodega_orientacion_pos` | CLUSTERED · **PK** | IdOrientacionPos |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

