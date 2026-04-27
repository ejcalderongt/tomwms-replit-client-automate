---
id: db-brain-table-i-nav-ped-compra-det
type: db-table
title: dbo.i_nav_ped_compra_det
schema: dbo
name: i_nav_ped_compra_det
kind: table
rows: 0
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_compra_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-08-21 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `NoEnc` | `nvarchar(50)` |  |  |
| 2 | `No` | `nvarchar(50)` |  |  |
| 3 | `Line_No` | `int` |  |  |
| 4 | `Type` | `nvarchar(50)` | ✓ |  |
| 5 | `Description` | `nvarchar(50)` | ✓ |  |
| 6 | `Description2` | `nvarchar(50)` | ✓ |  |
| 7 | `Location_Code` | `nvarchar(50)` | ✓ |  |
| 8 | `Quantity` | `float` | ✓ |  |
| 9 | `Unit_Of_Measure_Code` | `nvarchar(50)` | ✓ |  |
| 10 | `Direct_Unit_Cost` | `float` | ✓ |  |
| 11 | `Line_Amount` | `float` | ✓ |  |
| 12 | `Quantity_Received` | `float` | ✓ |  |
| 13 | `Planed_Receipt_Date` | `datetime` | ✓ |  |
| 14 | `Variant_Code` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__i_nav_pe__0D8B837B72B8B3D5` | CLUSTERED · **PK** | NoEnc, No, Line_No |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

