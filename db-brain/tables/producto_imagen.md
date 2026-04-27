---
id: db-brain-table-producto-imagen
type: db-table
title: dbo.producto_imagen
schema: dbo
name: producto_imagen
kind: table
rows: 9
modify_date: 2022-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_imagen`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 9 |
| Schema modify_date | 2022-03-18 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoImagen` | `int` |  |  |
| 2 | `IdProducto` | `int` | ✓ |  |
| 3 | `Etiqueta` | `nvarchar(50)` | ✓ |  |
| 4 | `Imagen` | `image` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` |  |  |
| 6 | `fec_agr` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_imagen` | CLUSTERED · **PK** | IdProductoImagen |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

