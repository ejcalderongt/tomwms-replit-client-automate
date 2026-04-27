---
id: db-brain-table-propietario-reglas-enc
type: db-table
title: dbo.propietario_reglas_enc
schema: dbo
name: propietario_reglas_enc
kind: table
rows: 1
modify_date: 2016-08-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.propietario_reglas_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2016-08-23 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:3 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaPropietarioEnc` | `int` |  |  |
| 2 | `IdReglaRecepcion` | `int` | ✓ |  |
| 3 | `IdPropietario` | `int` | ✓ |  |
| 4 | `IdMensajeRegla` | `int` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_propietario_reglas` | CLUSTERED · **PK** | IdReglaPropietarioEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_propietario_reglas_enc_mensaje_regla` → `mensaje_regla`
- `FK_propietario_reglas_enc_propietarios` → `propietarios`
- `FK_propietario_reglas_enc_reglas_recepcion` → `reglas_recepcion`

### Entrantes (otra tabla → esta)

- `propietario_reglas_det` (`FK_propietario_reglas_det_propietario_reglas_enc`)
- `trans_re_det_infraccion` (`FK_trans_re_det_infraccion_propietario_reglas_enc`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_Propietario_Regla_Recepcion` (view)

