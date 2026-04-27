---
id: db-brain-table-trans-ajuste-det-doc
type: db-table
title: dbo.trans_ajuste_det_doc
schema: dbo
name: trans_ajuste_det_doc
kind: table
rows: 0
modify_date: 2018-08-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ajuste_det_doc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-08-28 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idajustedoc` | `int` |  |  |
| 2 | `idajusteenc` | `int` |  |  |
| 3 | `documento` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ajuste_doc` | CLUSTERED · **PK** | idajustedoc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ajuste_enc_trans_ajuste_det_doc` → `trans_ajuste_enc`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

