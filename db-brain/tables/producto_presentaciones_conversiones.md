---
id: db-brain-table-producto-presentaciones-conversiones
type: db-table
title: dbo.producto_presentaciones_conversiones
schema: dbo
name: producto_presentaciones_conversiones
kind: table
rows: 0
modify_date: 2018-01-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_presentaciones_conversiones`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-22 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConversion` | `int` |  |  |
| 3 | `IdPresentacionOrigen` | `int` | ✓ |  |
| 4 | `IdPresentacionDestino` | `int` | ✓ |  |
| 5 | `Factor` | `float` | ✓ |  |
| 6 | `activo` | `bit` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 11 | `inverso` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_presentaciones_conversiones` | CLUSTERED · **PK** | IdConversion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_presentaciones_conversiones_producto_presentacion` → `producto_presentacion`
- `FK_producto_presentaciones_conversiones_producto_presentacion1` → `producto_presentacion`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

