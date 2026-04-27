---
id: db-brain-table-trans-re-det-parametros
type: db-table
title: dbo.trans_re_det_parametros
schema: dbo
name: trans_re_det_parametros
kind: table
rows: 0
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_det_parametros`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-19 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametroDet` | `int` |  |  |
| 2 | `IdRecepcionDet` | `int` |  |  |
| 3 | `IdRecepcionEnc` | `int` |  |  |
| 4 | `IdProductoParametro` | `int` |  |  |
| 5 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 6 | `valor_numerico` | `float` | ✓ |  |
| 7 | `valor_fecha` | `datetime` | ✓ |  |
| 8 | `valor_logico` | `bit` | ✓ |  |
| 9 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_recepcion_det_parametros` | CLUSTERED · **PK** | IdParametroDet, IdRecepcionDet, IdRecepcionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_det_parametros_producto_parametros` → `producto_parametros`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

