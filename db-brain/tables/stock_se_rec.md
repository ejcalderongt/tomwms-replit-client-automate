---
id: db-brain-table-stock-se-rec
type: db-table
title: dbo.stock_se_rec
schema: dbo
name: stock_se_rec
kind: table
rows: 0
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_se_rec`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-12-17 |
| Columnas | 13 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockSeRec` | `int` |  |  |
| 2 | `IdStockRec` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` | ✓ |  |
| 4 | `NoSerie` | `nvarchar(50)` | ✓ |  |
| 5 | `NoSerieInicial` | `nvarchar(50)` | ✓ |  |
| 6 | `NoSerieFinal` | `nvarchar(50)` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` |  |  |
| 8 | `fec_agr` | `datetime` |  |  |
| 9 | `user_mod` | `nvarchar(50)` |  |  |
| 10 | `fec_mod` | `datetime` |  |  |
| 11 | `activo` | `bit` |  |  |
| 12 | `regularizado` | `bit` | ✓ |  |
| 13 | `fecha_regularizacion` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_se_rec` | CLUSTERED · **PK** | IdStockSeRec |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_se_rec_producto_bodega` → `producto_bodega`
- `FK_stock_se_rec_stock_rec` → `stock_rec`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

