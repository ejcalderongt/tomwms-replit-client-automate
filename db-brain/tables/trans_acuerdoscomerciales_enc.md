---
id: db-brain-table-trans-acuerdoscomerciales-enc
type: db-table
title: dbo.trans_acuerdoscomerciales_enc
schema: dbo
name: trans_acuerdoscomerciales_enc
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_acuerdoscomerciales_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 13 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAcuerdoEnc` | `int` |  |  |
| 2 | `IdCliente` | `int` |  |  |
| 3 | `codigo_acuerdo` | `int` | ✓ |  |
| 4 | `descripcion` | `varchar(500)` | ✓ |  |
| 5 | `tipo_cobro` | `char(1)` | ✓ |  |
| 6 | `cod_moneda` | `int` | ✓ |  |
| 7 | `moneda` | `varchar(20)` | ✓ |  |
| 8 | `estado` | `bit` | ✓ |  |
| 9 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `fec_erp` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_acuerdoscomerciales_enc` | CLUSTERED · **PK** | IdAcuerdoEnc |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_acuerdoscomerciales_det` (`FK_trans_acuerdoscomerciales_det_trans_acuerdoscomerciales_enc`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

