---
id: db-brain-table-producto-presentacion-bk
type: db-table
title: dbo.producto_presentacion_bk
schema: dbo
name: producto_presentacion_bk
kind: table
rows: 8630
modify_date: 2022-06-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_presentacion_bk`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8.630 |
| Schema modify_date | 2022-06-07 |
| Columnas | 28 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPresentacion` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `imprime_barra` | `bit` | ✓ |  |
| 6 | `peso` | `float` | ✓ |  |
| 7 | `alto` | `float` | ✓ |  |
| 8 | `largo` | `float` | ✓ |  |
| 9 | `ancho` | `float` | ✓ |  |
| 10 | `factor` | `float` |  |  |
| 11 | `MinimoExistencia` | `float` | ✓ |  |
| 12 | `MaximoExistencia` | `float` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `activo` | `bit` | ✓ |  |
| 18 | `EsPallet` | `bit` | ✓ |  |
| 19 | `Precio` | `float` | ✓ |  |
| 20 | `MinimoPeso` | `float` | ✓ |  |
| 21 | `MaximoPeso` | `float` | ✓ |  |
| 22 | `Costo` | `float` | ✓ |  |
| 23 | `CamasPorTarima` | `float` | ✓ |  |
| 24 | `CajasPorCama` | `float` | ✓ |  |
| 25 | `genera_lp_auto` | `bit` | ✓ |  |
| 26 | `permitir_paletizar` | `bit` | ✓ |  |
| 27 | `sistema` | `bit` | ✓ |  |
| 28 | `IdPresentacionPallet` | `int` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

