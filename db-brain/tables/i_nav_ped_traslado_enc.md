---
id: db-brain-table-i-nav-ped-traslado-enc
type: db-table
title: dbo.i_nav_ped_traslado_enc
schema: dbo
name: i_nav_ped_traslado_enc
kind: table
rows: 4237
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_traslado_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.237 |
| Schema modify_date | 2025-06-11 |
| Columnas | 24 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `No` | `nvarchar(50)` |  |  |
| 2 | `Posting_Date` | `date` | ✓ |  |
| 3 | `Receipt_Date` | `date` | ✓ |  |
| 4 | `Shipment_Date` | `date` | ✓ |  |
| 5 | `Status` | `int` | ✓ |  |
| 6 | `Transfer_from_Code` | `nvarchar(50)` | ✓ |  |
| 7 | `Transfer_from_Contact` | `nvarchar(50)` | ✓ |  |
| 8 | `Transfer_from_Name` | `nvarchar(50)` | ✓ |  |
| 9 | `Transfer_to_Code` | `nvarchar(50)` | ✓ |  |
| 10 | `Transfer_to_Contact` | `nvarchar(50)` | ✓ |  |
| 11 | `Transfer_to_Name` | `nvarchar(150)` | ✓ |  |
| 12 | `transfer_to_CodeField` | `nvarchar(50)` | ✓ |  |
| 15 | `Product_Owner_Code` | `nvarchar(25)` | ✓ |  |
| 16 | `Receipt_Document_Reference` | `nvarchar(50)` | ✓ |  |
| 17 | `Document_Type` | `int` |  |  |
| 18 | `External_Document_No` | `nvarchar(50)` | ✓ |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `RoadCodigoRuta` | `nvarchar(50)` | ✓ |  |
| 21 | `RoadCodigoVendedor` | `nvarchar(50)` | ✓ |  |
| 22 | `Manufacturing_Process` | `int` | ✓ |  |
| 23 | `Address` | `nvarchar(255)` | ✓ |  |
| 24 | `Comments` | `nvarchar(255)` | ✓ |  |
| 25 | `Company_Code` | `nvarchar(50)` | ✓ |  |
| 26 | `IsExport` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ped_traslado_enc` | CLUSTERED · **PK** | No, Document_Type |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

