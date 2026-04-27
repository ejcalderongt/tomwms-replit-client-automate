---
id: db-brain-table-trans-inventario-enc
type: db-table
title: dbo.trans_inventario_enc
schema: dbo
name: trans_inventario_enc
kind: table
rows: 0
modify_date: 2016-05-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inventario_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-05-16 |
| Columnas | 14 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdInventarioEnc` | `bigint` |  |  |
| 2 | `IdPropietarioBodega` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `fecha` | `datetime` | ✓ |  |
| 5 | `hora_ini` | `datetime` | ✓ |  |
| 6 | `hora_fin` | `datetime` | ✓ |  |
| 7 | `estado` | `nvarchar(20)` | ✓ |  |
| 8 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `tipo_conteo` | `nvarchar(10)` | ✓ |  |
| 13 | `actualiza_val` | `bit` | ✓ |  |
| 14 | `tipo_inv` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inventario_enc` | CLUSTERED · **PK** | IdInventarioEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inventario_enc_propietario_bodega` → `propietario_bodega`

### Entrantes (otra tabla → esta)

- `trans_inventario_det` (`FK_trans_inventario_det_trans_inventario_enc`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

