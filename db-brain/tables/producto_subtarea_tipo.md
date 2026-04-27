---
id: db-brain-table-producto-subtarea-tipo
type: db-table
title: dbo.producto_subtarea_tipo
schema: dbo
name: producto_subtarea_tipo
kind: table
rows: 3
modify_date: 2023-10-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_subtarea_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2023-10-23 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdSubTareaTipo` | `int` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_subtarea` | CLUSTERED · **PK** | IdSubTareaTipo |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_subtarea` (`FK_producto_subtarea_producto_subtarea_tipo`)

## Quién la referencia

**1** objetos:

- `VW_Producto_Subtareas` (view)

