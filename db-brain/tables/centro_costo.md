---
id: db-brain-table-centro-costo
type: db-table
title: dbo.centro_costo
schema: dbo
name: centro_costo
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.centro_costo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCentroCosto` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Nombre` | `nvarchar(150)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `Referencia` | `nvarchar(150)` | ✓ |  |
| 11 | `control_inventario` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_centro_costo` | CLUSTERED · **PK** | IdCentroCosto |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Ajustes` (view)

