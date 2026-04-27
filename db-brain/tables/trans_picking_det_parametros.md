---
id: db-brain-table-trans-picking-det-parametros
type: db-table
title: dbo.trans_picking_det_parametros
schema: dbo
name: trans_picking_det_parametros
kind: table
rows: 0
modify_date: 2020-01-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_det_parametros`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-01-25 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametroPicking` | `int` |  |  |
| 2 | `IdPickingDet` | `int` |  |  |
| 3 | `IdProductoParametro` | `int` |  |  |
| 4 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 5 | `valor_numerico` | `float` | ✓ |  |
| 6 | `valor_fecha` | `datetime` | ✓ |  |
| 7 | `valor_logico` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_det_parametros` | CLUSTERED · **PK** | IdParametroPicking |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_det_parametros_producto_parametros` → `producto_parametros`
- `FK_trans_picking_det_parametros_trans_picking_det` → `trans_picking_det`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

