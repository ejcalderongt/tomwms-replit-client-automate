---
id: db-brain-table-i-nav-producto
type: db-table
title: dbo.i_nav_producto
schema: dbo
name: i_nav_producto
kind: table
rows: 0
modify_date: 2025-08-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_producto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-08-26 |
| Columnas | 21 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `No` | `nvarchar(50)` |  |  |
| 2 | `Description` | `nvarchar(150)` | ✓ |  |
| 3 | `Description_2` | `nvarchar(150)` | ✓ |  |
| 4 | `Inventory` | `float` | ✓ |  |
| 5 | `Base_Unit_Of_Measure` | `nvarchar(150)` | ✓ |  |
| 6 | `Unit_Cost` | `float` | ✓ |  |
| 7 | `Inventory_Posting_Group` | `nvarchar(150)` | ✓ |  |
| 8 | `Gen_Prod_Posting_Group` | `nvarchar(150)` | ✓ |  |
| 9 | `Search_Description` | `nvarchar(150)` | ✓ |  |
| 10 | `Item_Category_Code` | `nvarchar(150)` | ✓ |  |
| 11 | `Product_Group_Code` | `nvarchar(150)` | ✓ |  |
| 12 | `Sales_Unit` | `nvarchar(150)` | ✓ |  |
| 13 | `Item_Tracking_Code` | `nvarchar(150)` | ✓ |  |
| 14 | `Item_Category_Name` | `nvarchar(100)` | ✓ |  |
| 15 | `Gen_Prod_Posting_Name` | `nvarchar(100)` | ✓ |  |
| 16 | `Producto_Group_Name` | `nvarchar(100)` | ✓ |  |
| 17 | `Manufacturing_Process` | `int` |  |  |
| 18 | `BatchControl` | `bit` |  |  |
| 19 | `product_class_code` | `nvarchar(50)` | ✓ |  |
| 20 | `product_class_name` | `nvarchar(150)` | ✓ |  |
| 21 | `ExpirationControl` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_producto` | CLUSTERED · **PK** | No |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

