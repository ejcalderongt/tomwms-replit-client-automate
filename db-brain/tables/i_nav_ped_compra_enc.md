---
id: db-brain-table-i-nav-ped-compra-enc
type: db-table
title: dbo.i_nav_ped_compra_enc
schema: dbo
name: i_nav_ped_compra_enc
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_compra_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 21 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `No` | `nvarchar(50)` |  |  |
| 2 | `Buy_From_Vendor_No` | `nvarchar(150)` | ✓ |  |
| 3 | `Buy_From_Vendor_Name` | `nvarchar(250)` | ✓ |  |
| 4 | `Posting_Description` | `nvarchar(150)` | ✓ |  |
| 5 | `Posting_Date` | `datetime` | ✓ |  |
| 6 | `Order_Date` | `datetime` | ✓ |  |
| 7 | `Document_Date` | `datetime` | ✓ |  |
| 8 | `Vendor_Invoice_No` | `nvarchar(150)` | ✓ |  |
| 9 | `Status` | `nvarchar(150)` | ✓ |  |
| 10 | `Payment_Terms_Code` | `nvarchar(150)` | ✓ |  |
| 11 | `Ship_To_Name` | `nvarchar(250)` | ✓ |  |
| 12 | `Location_Code` | `nvarchar(150)` | ✓ |  |
| 13 | `Ship_To_Contact` | `nvarchar(150)` | ✓ |  |
| 14 | `Expected_Receipt_Date` | `date` | ✓ |  |
| 15 | `Is_Internal_Transfer` | `bit` | ✓ |  |
| 16 | `Product_Owner_Code` | `nvarchar(25)` | ✓ |  |
| 17 | `Internal_Transfer_Document_No` | `nvarchar(50)` | ✓ |  |
| 18 | `Document_Type` | `int` |  |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `IsImport` | `bit` | ✓ |  |
| 21 | `Company_Code` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ped_compra_enc` | CLUSTERED · **PK** | No, Document_Type |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

