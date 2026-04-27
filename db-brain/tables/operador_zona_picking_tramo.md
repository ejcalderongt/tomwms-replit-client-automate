---
id: db-brain-table-operador-zona-picking-tramo
type: db-table
title: dbo.operador_zona_picking_tramo
schema: dbo
name: operador_zona_picking_tramo
kind: table
rows: 0
modify_date: 2022-06-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.operador_zona_picking_tramo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-06-13 |
| Columnas | 10 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdZonaPickingTramoOperador` | `int` |  |  |
| 2 | `IdZonaPickingTramo` | `int` |  |  |
| 3 | `IdZonaPicking` | `int` |  |  |
| 4 | `IdOperador` | `int` |  |  |
| 5 | `dia_semana` | `int` |  |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_operador_zona_picking_tramo` | CLUSTERED · **PK** | IdZonaPickingTramoOperador |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `VW_Operador_Zona_Picking` (view)
- `VW_Zona_Picking_Tramo_Operador` (view)

