---
id: db-brain-table-i-nav-ped-compra-det-lote
type: db-table
title: dbo.i_nav_ped_compra_det_lote
schema: dbo
name: i_nav_ped_compra_det_lote
kind: table
rows: 0
modify_date: 2022-07-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_compra_det_lote`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-07-18 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `NoEnc` | `nvarchar(50)` |  |  |
| 2 | `source_ID` | `nvarchar(50)` | ✓ |  |
| 3 | `Source_Prod_Order_Line` | `int` |  |  |
| 4 | `Item_No` | `nvarchar(50)` |  |  |
| 5 | `Lot_No` | `nvarchar(100)` | ✓ |  |
| 6 | `Expiration_Date` | `datetime` | ✓ |  |
| 7 | `Entry_No` | `nvarchar(50)` | ✓ |  |
| 8 | `Source_Type` | `int` | ✓ |  |
| 9 | `Quantity_Base` | `float` | ✓ |  |
| 10 | `Variant_Code` | `nvarchar(25)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__i_nav_pe__6EC3B191C3EE4BF8` | CLUSTERED · **PK** | NoEnc, Item_No, Source_Prod_Order_Line |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

