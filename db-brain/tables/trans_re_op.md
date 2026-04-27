---
id: db-brain-table-trans-re-op
type: db-table
title: dbo.trans_re_op
schema: dbo
name: trans_re_op
kind: table
rows: 1644
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.644 |
| Schema modify_date | 2023-08-21 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperadorRec` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdOperadorBodega` | `int` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_recepcion_operadores` | CLUSTERED · **PK** | IdOperadorRec, IdRecepcionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_op_operador_bodega` → `operador_bodega`
- `FK_trans_recepcion_operadores_trans_recepcion_enc` → `trans_re_enc`

## Quién la referencia

**7** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_REC_CON_OC` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_Tareas_Operador` (view)

