---
id: db-brain-table-zona-picking-tramo
type: db-table
title: dbo.zona_picking_tramo
schema: dbo
name: zona_picking_tramo
kind: table
rows: 0
modify_date: 2022-05-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.zona_picking_tramo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-05-26 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdZonaPickingTramo` | `int` |  |  |
| 2 | `IdZonaPicking` | `int` |  |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdArea` | `int` | ✓ |  |
| 5 | `IdSector` | `int` | ✓ |  |
| 6 | `IdTramo` | `int` | ✓ |  |
| 7 | `min_x` | `int` | ✓ |  |
| 8 | `max_x` | `int` | ✓ |  |
| 9 | `min_y` | `int` | ✓ |  |
| 10 | `max_y` | `int` | ✓ |  |
| 11 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_zona_picking_tramo` | CLUSTERED · **PK** | IdZonaPickingTramo |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `VW_Operador_Zona_Picking` (view)
- `VW_Zona_Picking_Tramo` (view)
- `VW_Zona_Picking_Tramo_Operador` (view)

