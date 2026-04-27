---
id: db-brain-table-trans-picking-prioridad
type: db-table
title: dbo.trans_picking_prioridad
schema: dbo
name: trans_picking_prioridad
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_prioridad`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPrioridadPicking` | `int` |  |  |
| 2 | `Codigo` | `int` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_prioridad` | CLUSTERED · **PK** | IdPrioridadPicking |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Tareas_Picking_HH` (view)

