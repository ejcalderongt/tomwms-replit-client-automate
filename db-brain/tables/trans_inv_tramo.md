---
id: db-brain-table-trans-inv-tramo
type: db-table
title: dbo.trans_inv_tramo
schema: dbo
name: trans_inv_tramo
kind: table
rows: 54
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_tramo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 54 |
| Schema modify_date | 2023-08-21 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventario` | `int` |  |  |
| 2 | `idtramo` | `int` |  |  |
| 3 | `det_idoperador` | `int` | ✓ |  |
| 4 | `det_estado` | `nvarchar(20)` | ✓ |  |
| 5 | `det_inicio` | `datetime` | ✓ |  |
| 6 | `det_fin` | `datetime` | ✓ |  |
| 7 | `res_idoperador` | `int` | ✓ |  |
| 8 | `res_estado` | `nvarchar(20)` | ✓ |  |
| 9 | `res_inicio` | `datetime` | ✓ |  |
| 10 | `res_fin` | `datetime` | ✓ |  |
| 11 | `aplicado` | `bit` | ✓ |  |
| 12 | `IdBodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_tramo` | CLUSTERED · **PK** | idinventario, idtramo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_inv_ini_tramo_inv_enc` → `trans_inv_enc`

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

