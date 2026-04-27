---
id: db-brain-table-dh-ocupacion-bodega
type: db-table
title: dbo.dh_ocupacion_bodega
schema: dbo
name: dh_ocupacion_bodega
kind: table
rows: 8144
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.dh_ocupacion_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8.144 |
| Schema modify_date | 2023-08-21 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOcupacionBodega` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `cant_ubicaciones_vacias` | `float` | ✓ |  |
| 4 | `cant_ubicaciones_ocupadas` | `float` | ✓ |  |
| 5 | `fecha` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_ocupacion_bodega_hist` | CLUSTERED · **PK** | IdOcupacionBodega |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

