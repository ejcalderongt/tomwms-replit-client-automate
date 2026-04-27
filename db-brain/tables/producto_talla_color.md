---
id: db-brain-table-producto-talla-color
type: db-table
title: dbo.producto_talla_color
schema: dbo
name: producto_talla_color
kind: table
rows: 0
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_talla_color`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-05-13 |
| Columnas | 10 |
| Índices | 2 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoTallaColor` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdTalla` | `int` |  |  |
| 4 | `IdColor` | `int` |  |  |
| 5 | `IdCampaña` | `int` | ✓ |  |
| 6 | `CodigoSKU` | `nvarchar(50)` |  |  |
| 7 | `fec_agr` | `datetime` |  |  |
| 8 | `user_agr` | `nvarchar(25)` |  |  |
| 9 | `fec_mod` | `datetime` |  |  |
| 10 | `user_mod` | `nvarchar(25)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__producto__BFF791DD222C1927` | CLUSTERED · **PK** | IdProductoTallaColor |
| `UQ__producto__F02F03F941FFEFBE` | NONCLUSTERED · UNIQUE | CodigoSKU |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__producto___IdCam__56C1F7D5` → `campaña`
- `FK__producto___IdCol__599E6480` → `color`
- `FK__producto___IdPro__57B61C0E` → `producto`
- `FK__producto___IdTal__58AA4047` → `talla`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

