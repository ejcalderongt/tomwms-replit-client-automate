---
id: db-brain-table-trans-re-fact
type: db-table
title: dbo.trans_re_fact
schema: dbo
name: trans_re_fact
kind: table
rows: 0
modify_date: 2018-10-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_fact`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-10-11 |
| Columnas | 10 |
| Índices | 2 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdFacturaRecepcion` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` | ✓ |  |
| 3 | `Orden` | `int` | ✓ |  |
| 4 | `NoFactura` | `nvarchar(50)` | ✓ |  |
| 5 | `Observacion` | `nvarchar(250)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `Completa` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_re_fact` | CLUSTERED · **PK** | IdFacturaRecepcion |
| `numerofactura` | NONCLUSTERED · UNIQUE | NoFactura |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_fact_trans_re_enc` → `trans_re_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

