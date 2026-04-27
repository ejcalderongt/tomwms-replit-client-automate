---
id: db-brain-table-reglas-recepcion
type: db-table
title: dbo.reglas_recepcion
schema: dbo
name: reglas_recepcion
kind: table
rows: 7
modify_date: 2016-07-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.reglas_recepcion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 7 |
| Schema modify_date | 2016-07-28 |
| Columnas | 11 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaRecepcion` | `int` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `descripcion` | `nvarchar(100)` | ✓ |  |
| 5 | `Rechazar` | `bit` | ✓ |  |
| 6 | `StockNoDisponible` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_reglas_recepcion` | CLUSTERED · **PK** | IdReglaRecepcion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `propietario_reglas_enc` (`FK_propietario_reglas_enc_reglas_recepcion`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_Propietario_Regla_Recepcion` (view)

