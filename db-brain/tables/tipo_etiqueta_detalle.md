---
id: db-brain-table-tipo-etiqueta-detalle
type: db-table
title: dbo.tipo_etiqueta_detalle
schema: dbo
name: tipo_etiqueta_detalle
kind: table
rows: 0
modify_date: 2025-03-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_etiqueta_detalle`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-03-27 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoEtiquetaDetalle` | `int` |  |  |
| 2 | `IdTipoEtiqueta` | `int` |  |  |
| 3 | `orden` | `int` | ✓ |  |
| 4 | `nombre` | `nvarchar(100)` | ✓ |  |
| 5 | `campo` | `nvarchar(100)` | ✓ |  |
| 6 | `coor_x` | `float` | ✓ |  |
| 7 | `coor_y` | `float` | ✓ |  |
| 8 | `width` | `float` | ✓ |  |
| 9 | `height` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_etiqueta_detalle` | CLUSTERED · **PK** | IdTipoEtiquetaDetalle |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_tipo_etiqueta_detalle_tipo_etiqueta` → `tipo_etiqueta`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

