---
id: db-brain-table-trans-ubic-hh-se
type: db-table
title: dbo.trans_ubic_hh_se
schema: dbo
name: trans_ubic_hh_se
kind: table
rows: 0
modify_date: 2016-07-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_hh_se`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-07-11 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionDetSe` | `int` |  |  |
| 2 | `IdTareaUbicacionEnc` | `int` |  |  |
| 3 | `IdStockSe` | `int` | ✓ |  |
| 4 | `IdUbicacionDestino` | `int` | ✓ |  |
| 5 | `IdEstadoDestino` | `int` | ✓ |  |
| 6 | `IdOperador` | `int` | ✓ |  |
| 7 | `HoraInicio` | `datetime` | ✓ |  |
| 8 | `HoraFin` | `datetime` | ✓ |  |
| 9 | `Realizado` | `bit` | ✓ |  |
| 10 | `cantidad` | `float` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_hh_se` | CLUSTERED · **PK** | IdTareaUbicacionDetSe |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

