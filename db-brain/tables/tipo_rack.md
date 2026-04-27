---
id: db-brain-table-tipo-rack
type: db-table
title: dbo.tipo_rack
schema: dbo
name: tipo_rack
kind: table
rows: 4
modify_date: 2018-03-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_rack`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2018-03-16 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoRack` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `cantidad_posiciones_ancho` | `int` | ✓ |  |
| 4 | `cantidad_posiciones_profundo` | `int` | ✓ |  |
| 5 | `Descripcion` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_rack` | CLUSTERED · **PK** | IdTipoRack |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

