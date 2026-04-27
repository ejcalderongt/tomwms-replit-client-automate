---
id: db-brain-table-producto-subtarea
type: db-table
title: dbo.producto_subtarea
schema: dbo
name: producto_subtarea
kind: table
rows: 0
modify_date: 2023-10-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_subtarea`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-23 |
| Columnas | 11 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoSubTarea` | `int` |  |  |
| 2 | `IdSubTareaTipo` | `int` | ✓ |  |
| 3 | `IdProductoBodega` | `int` | ✓ |  |
| 4 | `porcentaje_escaneo` | `float` | ✓ |  |
| 5 | `escanear_producto` | `bit` | ✓ |  |
| 6 | `obligatorio_recepcion` | `bit` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_subtarea_1` | CLUSTERED · **PK** | IdProductoSubTarea |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_subtarea_producto_bodega` → `producto_bodega`
- `FK_producto_subtarea_producto_subtarea_tipo` → `producto_subtarea_tipo`

## Quién la referencia

**1** objetos:

- `VW_Producto_Subtareas` (view)

