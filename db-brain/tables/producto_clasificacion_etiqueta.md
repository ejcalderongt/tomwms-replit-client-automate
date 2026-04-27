---
id: db-brain-table-producto-clasificacion-etiqueta
type: db-table
title: dbo.producto_clasificacion_etiqueta
schema: dbo
name: producto_clasificacion_etiqueta
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_clasificacion_etiqueta`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Idclasificacion_etiqueta` | `int` |  |  |
| 2 | `Descripcion` | `nvarchar(50)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_clasificacion_etiqueta` | CLUSTERED · **PK** | Idclasificacion_etiqueta |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `tipo_etiqueta` (`FK_tipo_etiqueta_producto_clasificacion_etiqueta`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

