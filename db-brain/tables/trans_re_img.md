---
id: db-brain-table-trans-re-img
type: db-table
title: dbo.trans_re_img
schema: dbo
name: trans_re_img
kind: table
rows: 0
modify_date: 2018-10-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_img`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-10-11 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImagen` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `Imagen` | `image` |  |  |
| 4 | `user_agr` | `nvarchar(50)` |  |  |
| 5 | `fec_agr` | `datetime` |  |  |
| 6 | `observacion` | `nvarchar(150)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_re_img` | CLUSTERED · **PK** | IdImagen, IdRecepcionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_recepcion_img_trans_recepcion_enc` → `trans_re_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

