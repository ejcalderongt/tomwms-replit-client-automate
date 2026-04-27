---
id: db-brain-table-trans-inv-ciclico-ubic
type: db-table
title: dbo.trans_inv_ciclico_ubic
schema: dbo
name: trans_inv_ciclico_ubic
kind: table
rows: 0
modify_date: 2020-02-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_ciclico_ubic`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-02-04 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` |  |  |
| 2 | `idubicacion` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_ciclico_ubic` | CLUSTERED · **PK** | idinventarioenc, idubicacion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `VW_Ubicaciones_Inventario_Ciclico` (view)

