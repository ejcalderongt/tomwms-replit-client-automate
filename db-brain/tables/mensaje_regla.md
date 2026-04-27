---
id: db-brain-table-mensaje-regla
type: db-table
title: dbo.mensaje_regla
schema: dbo
name: mensaje_regla
kind: table
rows: 10
modify_date: 2016-07-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.mensaje_regla`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 10 |
| Schema modify_date | 2016-07-22 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMensajeRegla` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `fec_agr` | `datetime` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_mod` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_mensaje_regla` | CLUSTERED · **PK** | IdMensajeRegla |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `propietario_reglas_enc` (`FK_propietario_reglas_enc_mensaje_regla`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_Propietario_Regla_Recepcion` (view)

