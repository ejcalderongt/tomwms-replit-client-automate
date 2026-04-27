---
id: db-brain-table-i-nav-ped-traslado-det-lote
type: db-table
title: dbo.i_nav_ped_traslado_det_lote
schema: dbo
name: i_nav_ped_traslado_det_lote
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ped_traslado_det_lote`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `NoEnc` | `nvarchar(50)` |  |  |
| 2 | `Line_No` | `int` |  |  |
| 3 | `No` | `nvarchar(50)` |  |  |
| 4 | `Batch_No` | `nvarchar(100)` |  |  |
| 5 | `Serial_No` | `nvarchar(100)` |  |  |
| 6 | `Expiration_Date` | `datetime` | ✓ |  |
| 7 | `Quantity_Base` | `float` | ✓ |  |
| 8 | `Variant_Code` | `nvarchar(25)` | ✓ |  |
| 9 | `WhsFrom` | `nvarchar(25)` | ✓ |  |
| 10 | `WhsTo` | `nvarchar(25)` | ✓ |  |
| 11 | `Fec_Agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ped_traslado_det_lote` | CLUSTERED · **PK** | NoEnc, Line_No, No, Batch_No |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

