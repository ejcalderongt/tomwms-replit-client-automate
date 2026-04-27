---
id: db-brain-table-producto-parametros
type: db-table
title: dbo.producto_parametros
schema: dbo
name: producto_parametros
kind: table
rows: 0
modify_date: 2018-01-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_parametros`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-22 |
| Columnas | 13 |
| Índices | 1 |
| FKs | out:2 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoParametro` | `int` |  |  |
| 2 | `IdParametro` | `int` |  |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 5 | `valor_numerico` | `float` | ✓ |  |
| 6 | `valor_fecha` | `datetime` | ✓ |  |
| 7 | `valor_logico` | `bit` | ✓ |  |
| 8 | `capturar_siempre` | `bit` |  |  |
| 9 | `user_agr` | `nvarchar(50)` |  |  |
| 10 | `fec_agr` | `datetime` |  |  |
| 11 | `user_mod` | `nvarchar(50)` |  |  |
| 12 | `fec_mod` | `datetime` |  |  |
| 13 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_parametros_2` | CLUSTERED · **PK** | IdProductoParametro |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_parametros_p_parametro` → `p_parametro`
- `FK_producto_parametros_producto` → `producto`

### Entrantes (otra tabla → esta)

- `stock_parametro` (`FK_stock_parametro_producto_parametros`)
- `trans_picking_det_parametros` (`FK_trans_picking_det_parametros_producto_parametros`)
- `trans_re_det_parametros` (`FK_trans_re_det_parametros_producto_parametros`)

## Quién la referencia

**7** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_ProductoBodegaParametro` (view)
- `VW_ProductoParametro` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)

