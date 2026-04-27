---
id: db-brain-table-trans-ubic-tarima
type: db-table
title: dbo.trans_ubic_tarima
schema: dbo
name: trans_ubic_tarima
kind: table
rows: 0
modify_date: 2016-07-20
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_tarima`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-07-20 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTarimaTareaUbic` | `int` |  |  |
| 2 | `IdTareaUbicacionEnc` | `int` | ✓ |  |
| 3 | `IdTarima` | `int` | ✓ |  |
| 4 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Utilizada` | `bit` | ✓ |  |
| 6 | `FechaUtilizacion` | `datetime` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_tarima` | CLUSTERED · **PK** | IdTarimaTareaUbic |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ubic_tarima_tarimas` → `tarimas`
- `FK_trans_ubic_tarima_trans_ubic_hh_enc` → `trans_ubic_hh_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `VW_TarimasUsadasEnTransaccion` (view)

