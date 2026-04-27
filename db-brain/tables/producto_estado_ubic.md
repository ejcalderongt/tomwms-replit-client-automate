---
id: db-brain-table-producto-estado-ubic
type: db-table
title: dbo.producto_estado_ubic
schema: dbo
name: producto_estado_ubic
kind: table
rows: 2
modify_date: 2019-11-20
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_estado_ubic`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2019-11-20 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoEstadUbic` | `int` |  |  |
| 2 | `IdEstado` | `int` |  |  |
| 3 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 4 | `fec_agr` | `datetime` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_mod` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `IdBodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_estado_ubic_1` | CLUSTERED · **PK** | IdProductoEstadUbic |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_estado_ubic_producto_estado` → `producto_estado`

## Quién la referencia

**4** objetos:

- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_Producto_Estado_Ubic_Bodega_HH` (view)
- `VW_ProductoEstadoUbic` (view)
- `VW_ProductoEstadoUbicacion` (view)

