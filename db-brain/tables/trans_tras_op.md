---
id: db-brain-table-trans-tras-op
type: db-table
title: dbo.trans_tras_op
schema: dbo
name: trans_tras_op
kind: table
rows: 0
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_tras_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-19 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperadorTras` | `int` |  |  |
| 2 | `IdTrasladoEnc` | `int` |  |  |
| 3 | `IdOperadorBodega` | `int` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_tras_op` | CLUSTERED · **PK** | IdOperadorTras |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_tras_op_operador_bodega` → `operador_bodega`
- `FK_trans_tras_op_trans_tras_enc` → `trans_tras_enc`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

