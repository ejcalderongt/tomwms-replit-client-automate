---
id: db-brain-table-trans-ajuste-enc
type: db-table
title: dbo.trans_ajuste_enc
schema: dbo
name: trans_ajuste_enc
kind: table
rows: 825
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ajuste_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 825 |
| Schema modify_date | 2025-02-11 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idajusteenc` | `int` |  |  |
| 4 | `fecha` | `date` | ✓ |  |
| 5 | `idusuario` | `int` | ✓ |  |
| 6 | `referencia` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `idbodega` | `int` | ✓ |  |
| 12 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 14 | `IdProductoFamilia` | `int` | ✓ |  |
| 15 | `IdPropietarioBodega` | `int` | ✓ |  |
| 16 | `ajuste_por_inventario` | `int` | ✓ |  |
| 17 | `IdCentroCosto` | `int` | ✓ |  |
| 18 | `auditado` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ajuste_enc` | CLUSTERED · **PK** | idajusteenc |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_ajuste_det_doc` (`FK_trans_ajuste_enc_trans_ajuste_det_doc`)
- `trans_ajuste_det` (`FK_trans_ajuste_enc_trans_ajuste_det`)

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `VW_Ajustes` (view)
- `VW_Movimientos_N1` (view)

