---
id: db-brain-table-trans-inv-stock-prod
type: db-table
title: dbo.trans_inv_stock_prod
schema: dbo
name: trans_inv_stock_prod
kind: table
rows: 1153
modify_date: 2025-05-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_stock_prod`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.153 |
| Schema modify_date | 2025-05-22 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventario` | `int` |  |  |
| 2 | `idinvstockprod` | `int` |  |  |
| 3 | `idProducto` | `int` |  |  |
| 4 | `idPresentacion` | `int` |  |  |
| 5 | `cant` | `float` | ✓ |  |
| 6 | `peso` | `float` | ✓ |  |
| 7 | `idUnidadMedida` | `int` | ✓ |  |
| 8 | `lote` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_vence` | `datetime` | ✓ |  |
| 10 | `codigo` | `nvarchar(50)` | ✓ |  |
| 11 | `idbodega` | `int` | ✓ |  |
| 12 | `idubicacion` | `int` | ✓ |  |
| 13 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 14 | `codigo_area` | `nvarchar(10)` | ✓ |  |
| 15 | `TipoTeoricoImportacion` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_stock_prod_1` | CLUSTERED · **PK** | idinventario, idinvstockprod, TipoTeoricoImportacion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**4** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)

