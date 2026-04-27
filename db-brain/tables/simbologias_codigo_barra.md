---
id: db-brain-table-simbologias-codigo-barra
type: db-table
title: dbo.simbologias_codigo_barra
schema: dbo
name: simbologias_codigo_barra
kind: table
rows: 9
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.simbologias_codigo_barra`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 9 |
| Schema modify_date | 2018-01-11 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdSimbologia` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_simbologias_codigo_barra` | CLUSTERED · **PK** | IdSimbologia |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_simbologias_codigo_barra`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

