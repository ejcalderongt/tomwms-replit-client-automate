---
id: db-brain-table-stock-res-se
type: db-table
title: dbo.stock_res_se
schema: dbo
name: stock_res_se
kind: table
rows: 0
modify_date: 2016-07-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_res_se`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-07-11 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockResSe` | `int` |  |  |
| 2 | `IdTransaccion` | `int` | ✓ |  |
| 3 | `Indicador` | `nvarchar(50)` | ✓ |  |
| 4 | `IdStockSe` | `int` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` |  |  |
| 6 | `fec_agr` | `datetime` |  |  |
| 7 | `user_mod` | `nvarchar(50)` |  |  |
| 8 | `fec_mod` | `datetime` |  |  |
| 9 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_res_se` | CLUSTERED · **PK** | IdStockResSe |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

