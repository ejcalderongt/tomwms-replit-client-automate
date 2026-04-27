---
id: db-brain-table-ubicaciones-por-regla
type: db-table
title: dbo.ubicaciones_por_regla
schema: dbo
name: ubicaciones_por_regla
kind: table
rows: 1102
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.ubicaciones_por_regla`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.102 |
| Schema modify_date | 2018-01-19 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicacionEnc` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `ancho` | `float` | ✓ |  |
| 5 | `largo` | `float` | ✓ |  |
| 6 | `alto` | `float` | ✓ |  |
| 7 | `IdTramo` | `int` | ✓ |  |
| 8 | `indice_x` | `int` | ✓ |  |
| 9 | `nivel` | `int` | ✓ |  |
| 10 | `IdIndiceRotacion` | `int` | ✓ |  |
| 11 | `IdTipoRotacion` | `int` | ✓ |  |
| 12 | `dañado` | `bit` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `bloqueada` | `bit` | ✓ |  |
| 15 | `acepta_pallet` | `bit` | ✓ |  |
| 16 | `IdBodega` | `int` |  |  |
| 17 | `IdPropietarioBodega` | `int` | ✓ |  |
| 18 | `regla_ubic_det_prop_Activo` | `bit` | ✓ |  |
| 19 | `IdPropietario` | `int` | ✓ |  |
| 20 | `IdIndiceRotacionRegla` | `int` | ✓ |  |
| 21 | `IdTipoRotacionRegla` | `int` | ✓ |  |
| 22 | `IdTipoProducto` | `int` | ✓ |  |
| 23 | `regla_ubic_det_tp_Activo` | `bit` | ✓ |  |
| 24 | `IdEstado` | `int` | ✓ |  |
| 25 | `regla_ubic_det_pe_Activo` | `bit` | ✓ |  |
| 26 | `IdPresentacion` | `int` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

