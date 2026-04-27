---
id: db-brain-table-propietario-reglas-det
type: db-table
title: dbo.propietario_reglas_det
schema: dbo
name: propietario_reglas_det
kind: table
rows: 1
modify_date: 2016-07-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.propietario_reglas_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2016-07-22 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaPropietarioDet` | `int` |  |  |
| 2 | `IdReglaPropietarioEnc` | `int` | ✓ |  |
| 3 | `IdDestinatarioPropietario` | `int` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_propietario_reglas_det` | CLUSTERED · **PK** | IdReglaPropietarioDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_propietario_reglas_det_propietario_destinatario` → `propietario_destinatario`
- `FK_propietario_reglas_det_propietario_reglas_enc` → `propietario_reglas_enc`

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

