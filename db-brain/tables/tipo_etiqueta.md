---
id: db-brain-table-tipo-etiqueta
type: db-table
title: dbo.tipo_etiqueta
schema: dbo
name: tipo_etiqueta
kind: table
rows: 6
modify_date: 2025-03-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_etiqueta`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2025-03-27 |
| Columnas | 17 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoEtiqueta` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Alto` | `float` | ✓ |  |
| 4 | `Ancho` | `float` | ✓ |  |
| 5 | `MargenIzq` | `float` | ✓ |  |
| 6 | `MagenDer` | `float` | ✓ |  |
| 7 | `MargenSup` | `float` | ✓ |  |
| 8 | `MargenInf` | `float` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `date` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `date` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `dpi` | `int` |  |  |
| 15 | `codigo_zpl` | `nvarchar(4000)` | ✓ |  |
| 16 | `Idclasificacion_etiqueta` | `int` | ✓ |  |
| 17 | `es_inkjet` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_etiqueta` | CLUSTERED · **PK** | IdTipoEtiqueta |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_tipo_etiqueta_producto_clasificacion_etiqueta` → `producto_clasificacion_etiqueta`

### Entrantes (otra tabla → esta)

- `tipo_etiqueta_detalle` (`FK_tipo_etiqueta_detalle_tipo_etiqueta`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

