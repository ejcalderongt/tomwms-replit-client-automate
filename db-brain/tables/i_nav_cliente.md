---
id: db-brain-table-i-nav-cliente
type: db-table
title: dbo.i_nav_cliente
schema: dbo
name: i_nav_cliente
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_cliente`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 16 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCliente` | `int` |  |  |
| 2 | `codigo_cliente` | `nvarchar(50)` |  |  |
| 3 | `nombre_cliente` | `nvarchar(120)` |  |  |
| 4 | `nit` | `nvarchar(20)` | ✓ |  |
| 5 | `razon_social` | `nvarchar(120)` | ✓ |  |
| 6 | `procesado_wms` | `bit` |  |  |
| 7 | `No` | `nvarchar(50)` | ✓ |  |
| 8 | `Name` | `nvarchar(120)` | ✓ |  |
| 9 | `Adress` | `nvarchar(120)` | ✓ |  |
| 10 | `City` | `nvarchar(120)` | ✓ |  |
| 11 | `Country` | `nvarchar(120)` | ✓ |  |
| 12 | `Phone_No` | `nvarchar(120)` | ✓ |  |
| 13 | `ContactName` | `nvarchar(120)` | ✓ |  |
| 14 | `Search_Name` | `nvarchar(120)` | ✓ |  |
| 15 | `VAT_Registratrion_No` | `nvarchar(120)` | ✓ |  |
| 16 | `Location_Code` | `nvarchar(120)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_cliente` | CLUSTERED · **PK** | IdCliente |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

