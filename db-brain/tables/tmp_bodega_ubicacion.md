---
id: db-brain-table-tmp-bodega-ubicacion
type: db-table
title: dbo.tmp_bodega_ubicacion
schema: dbo
name: tmp_bodega_ubicacion
kind: table
rows: 0
modify_date: 2019-09-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tmp_bodega_ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2019-09-02 |
| Columnas | 35 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdTramo` | `int` |  |  |
| 3 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `ancho` | `float` | ✓ |  |
| 5 | `largo` | `float` | ✓ |  |
| 6 | `alto` | `float` | ✓ |  |
| 7 | `nivel` | `int` | ✓ |  |
| 8 | `indice_x` | `int` | ✓ |  |
| 9 | `IdIndiceRotacion` | `int` | ✓ |  |
| 10 | `IdTipoRotacion` | `int` | ✓ |  |
| 11 | `sistema` | `bit` | ✓ |  |
| 12 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 13 | `codigo_barra2` | `nvarchar(50)` | ✓ |  |
| 14 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `dañado` | `bit` | ✓ |  |
| 19 | `activo` | `bit` | ✓ |  |
| 20 | `bloqueada` | `bit` | ✓ |  |
| 21 | `acepta_pallet` | `bit` | ✓ |  |
| 22 | `ubicacion_picking` | `bit` | ✓ |  |
| 23 | `ubicacion_recepcion` | `bit` | ✓ |  |
| 24 | `ubicacion_despacho` | `bit` | ✓ |  |
| 25 | `ubicacion_merma` | `bit` | ✓ |  |
| 26 | `margen_izquierdo` | `float` | ✓ |  |
| 27 | `margen_derecho` | `float` | ✓ |  |
| 28 | `margen_superior` | `float` | ✓ |  |
| 29 | `margen_inferior` | `float` | ✓ |  |
| 30 | `orientacion_pos` | `nvarchar(50)` | ✓ |  |
| 31 | `ubicacion_virtual` | `bit` | ✓ |  |
| 32 | `ubicacion_ne` | `bit` | ✓ |  |
| 33 | `IdBodega` | `int` |  |  |
| 34 | `IdArea` | `int` |  |  |
| 35 | `IdSector` | `int` |  |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

