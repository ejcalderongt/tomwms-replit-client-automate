---
id: db-brain-table-zona-picking
type: db-table
title: dbo.zona_picking
schema: dbo
name: zona_picking
kind: table
rows: 0
modify_date: 2022-05-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.zona_picking`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-05-26 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdZonaPicking` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_zona_picking` | CLUSTERED · **PK** | IdZonaPicking |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `VW_Operador_Zona_Picking` (view)
- `VW_Zona_Picking_Tramo_Operador` (view)

