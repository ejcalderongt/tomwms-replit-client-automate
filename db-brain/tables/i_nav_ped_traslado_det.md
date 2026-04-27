---
id: db-brain-table-i-nav-ped-traslado-det
type: db-table
title: dbo.i_nav_ped_traslado_det
schema: dbo
name: i_nav_ped_traslado_det
kind: table
rows: 14953
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_traslado_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14.953 |
| Schema modify_date | 2025-03-18 |
| Columnas | 22 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `NoEnc` | `nvarchar(50)` |  |  |
| 2 | `No` | `nvarchar(50)` |  |  |
| 3 | `Description` | `nvarchar(100)` | ✓ |  |
| 4 | `Item_No` | `nvarchar(50)` | ✓ |  |
| 5 | `Qty_to_Receive` | `float` | ✓ |  |
| 6 | `Qty_to_Ship` | `float` | ✓ |  |
| 7 | `Quantity` | `float` | ✓ |  |
| 8 | `transfer_to_CodeField` | `nvarchar(50)` | ✓ |  |
| 9 | `Shipment_Date` | `date` | ✓ |  |
| 10 | `Unit_of_Measure_Code` | `nvarchar(50)` | ✓ |  |
| 11 | `Line_No` | `nvarchar(25)` |  |  |
| 12 | `Variant_Code` | `nvarchar(25)` | ✓ |  |
| 13 | `Status` | `int` | ✓ |  |
| 14 | `Process_Result` | `nvarchar(1000)` | ✓ |  |
| 15 | `price` | `float` | ✓ |  |
| 16 | `source_id` | `nvarchar(50)` | ✓ |  |
| 17 | `Is_Partially_Processed` | `bit` | ✓ |  |
| 18 | `IdPedidoDet` | `int` | ✓ |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `Quantity_Reserved_WMS` | `float` | ✓ |  |
| 21 | `transfer_From_CodeField` | `nvarchar(25)` | ✓ |  |
| 22 | `scan_type` | `nvarchar(25)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ped_traslado_det` | CLUSTERED · **PK** | NoEnc, No, Line_No |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

