---
id: db-brain-table-jornada-laboral
type: db-table
title: dbo.jornada_laboral
schema: dbo
name: jornada_laboral
kind: table
rows: 6
modify_date: 2023-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.jornada_laboral`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2023-05-18 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:1 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdJornada` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `nombre_jornada` | `nvarchar(50)` | ✓ |  |
| 4 | `fecha_inicio` | `datetime` | ✓ |  |
| 5 | `fecha_fin` | `datetime` | ✓ |  |
| 6 | `horas_trabajadas` | `int` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `fecha_baja` | `datetime` | ✓ |  |
| 12 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_jornada_laboral` | CLUSTERED · **PK** | IdJornada |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_jornada_laboral_bodega` → `bodega`

### Entrantes (otra tabla → esta)

- `horario_laboral_enc` (`FK_horario_laboral_enc_jornada_laboral`)
- `operador_jornada_laboral` (`FK_operador_jornada_laboral_jornada`)
- `operador` (`FK_operador_jornada_laboral`)

## Quién la referencia

**5** objetos:

- `asignar_jornada_laboral` (stored_procedure)
- `CLBD` (stored_procedure)
- `VW_HorarioLaboral` (view)
- `VW_HorarioLaboralEnc` (view)
- `VW_Operador_Horario` (view)

