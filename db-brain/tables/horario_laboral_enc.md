---
id: db-brain-table-horario-laboral-enc
type: db-table
title: dbo.horario_laboral_enc
schema: dbo
name: horario_laboral_enc
kind: table
rows: 6
modify_date: 2016-09-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.horario_laboral_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2016-09-16 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:3 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdHorarioLaboralEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdJornada` | `int` | ✓ |  |
| 4 | `IdTurno` | `int` | ✓ |  |
| 5 | `nombre` | `nvarchar(128)` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_horario_laboral_enc` | CLUSTERED · **PK** | IdHorarioLaboralEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_horario_laboral_enc_bodega` → `bodega`
- `FK_horario_laboral_enc_jornada_laboral` → `jornada_laboral`
- `FK_horario_laboral_enc_turno` → `turno`

### Entrantes (otra tabla → esta)

- `horario_laboral_det` (`FK_horario_laboral_det_horario_laboral_enc`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_HorarioLaboral` (view)
- `VW_HorarioLaboralEnc` (view)
- `VW_Operador_Horario` (view)

