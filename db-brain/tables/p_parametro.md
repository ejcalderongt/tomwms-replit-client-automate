---
id: db-brain-table-p-parametro
type: db-table
title: dbo.p_parametro
schema: dbo
name: p_parametro
kind: table
rows: 0
modify_date: 2017-07-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.p_parametro`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-07-13 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametro` | `int` |  |  |
| 3 | `tipo` | `nvarchar(50)` | ✓ |  |
| 4 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 5 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 6 | `valor_numerico` | `float` | ✓ |  |
| 7 | `valor_fecha` | `datetime` | ✓ |  |
| 8 | `valor_logico` | `bit` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_parametros_1` | CLUSTERED · **PK** | IdParametro |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_parametros` (`FK_producto_parametros_p_parametro`)

## Quién la referencia

**8** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Parametro` (view)
- `VW_ProductoBodegaParametro` (view)
- `VW_ProductoParametro` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)

