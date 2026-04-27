---
id: db-brain-table-proveedor-tiempos
type: db-table
title: dbo.proveedor_tiempos
schema: dbo
name: proveedor_tiempos
kind: table
rows: 0
modify_date: 2023-10-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.proveedor_tiempos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-23 |
| Columnas | 11 |
| Índices | 1 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTiempoproveedor` | `int` |  |  |
| 2 | `Idproveedor` | `int` |  |  |
| 3 | `IdFamilia` | `int` | ✓ |  |
| 4 | `IdClasificacion` | `int` | ✓ |  |
| 5 | `Dias_Local` | `int` | ✓ |  |
| 6 | `Dias_Exterior` | `int` | ✓ |  |
| 7 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_proveedor_tiempos_1` | CLUSTERED · **PK** | IdTiempoproveedor |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_proveedor_tiempos_producto_clasificacion` → `producto_clasificacion`
- `FK_proveedor_tiempos_producto_familia` → `producto_familia`
- `FK_proveedor_tiempos_proveedor` → `proveedor`

## Quién la referencia

**1** objetos:

- `VW_TiempoProveedor` (view)

