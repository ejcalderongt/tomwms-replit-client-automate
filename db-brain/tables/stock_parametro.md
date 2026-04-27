---
id: db-brain-table-stock-parametro
type: db-table
title: dbo.stock_parametro
schema: dbo
name: stock_parametro
kind: table
rows: 0
modify_date: 2020-02-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_parametro`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-02-14 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockParametro` | `int` |  |  |
| 2 | `IdStock` | `int` |  |  |
| 3 | `IdProductoParametro` | `int` |  |  |
| 4 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 5 | `valor_numerico` | `float` | ✓ |  |
| 6 | `valor_fecha` | `datetime` | ✓ |  |
| 7 | `valor_logico` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(30)` |  |  |
| 9 | `fec_agr` | `datetime` |  |  |
| 10 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_parametro` | CLUSTERED · **PK** | IdStockParametro |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_parametro_producto_parametros` → `producto_parametros`
- `FK_stock_parametro_stock` → `stock`

## Quién la referencia

**7** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)

