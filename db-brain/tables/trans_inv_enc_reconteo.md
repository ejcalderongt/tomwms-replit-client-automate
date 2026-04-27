---
id: db-brain-table-trans-inv-enc-reconteo
type: db-table
title: dbo.trans_inv_enc_reconteo
schema: dbo
name: trans_inv_enc_reconteo
kind: table
rows: 0
modify_date: 2018-10-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_enc_reconteo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-10-03 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinvencreconteo` | `int` |  |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `reconteo` | `int` | ✓ |  |
| 4 | `estado` | `nvarchar(20)` | ✓ |  |
| 5 | `hora_ini` | `datetime` | ✓ |  |
| 6 | `hora_fin` | `datetime` | ✓ |  |
| 7 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_enc_reconteo` | CLUSTERED · **PK** | idinvencreconteo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inv_enc_reconteo_trans_inv_enc` → `trans_inv_enc`

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

