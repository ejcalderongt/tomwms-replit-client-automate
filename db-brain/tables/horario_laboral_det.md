---
id: db-brain-table-horario-laboral-det
type: db-table
title: dbo.horario_laboral_det
schema: dbo
name: horario_laboral_det
kind: table
rows: 51
modify_date: 2016-09-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.horario_laboral_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 51 |
| Schema modify_date | 2016-09-16 |
| Columnas | 17 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdHorarioLaboralDet` | `int` |  |  |
| 2 | `IdHorarioLaboralEnc` | `int` | ✓ |  |
| 3 | `dia` | `int` | ✓ |  |
| 4 | `hora_inicio` | `datetime` | ✓ |  |
| 5 | `hora_fin` | `datetime` | ✓ |  |
| 6 | `minimo_min_hora_ingreso` | `int` | ✓ |  |
| 7 | `maximo_min_hora_ingreso` | `int` | ✓ |  |
| 8 | `minimo_min_hora_salida` | `int` | ✓ |  |
| 9 | `maximo_min_hora_salida` | `int` | ✓ |  |
| 10 | `tiempo_retraso_permitido` | `int` | ✓ |  |
| 11 | `horas_extras` | `bit` | ✓ |  |
| 12 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 13 | `fec_agr` | `datetime` | ✓ |  |
| 14 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 15 | `fec_mod` | `datetime` | ✓ |  |
| 16 | `fecha_baja` | `datetime` | ✓ |  |
| 17 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_horario_laboral_det` | CLUSTERED · **PK** | IdHorarioLaboralDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_horario_laboral_det_horario_laboral_enc` → `horario_laboral_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_HorarioLaboral` (view)
- `VW_HorarioLaboralDet` (view)
- `VW_Operador_Horario` (view)

