---
id: db-brain-table-turno
type: db-table
title: dbo.turno
schema: dbo
name: turno
kind: table
rows: 6
modify_date: 2016-09-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.turno`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2016-09-16 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTurno` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `nombre` | `nvarchar(50)` |  |  |
| 4 | `activo` | `bit` |  |  |
| 5 | `user_agr` | `nvarchar(25)` |  |  |
| 6 | `fec_agr` | `datetime` |  |  |
| 7 | `user_mod` | `nvarchar(25)` |  |  |
| 8 | `fec_mod` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_turno` | CLUSTERED · **PK** | IdTurno |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_turno_bodega` → `bodega`

### Entrantes (otra tabla → esta)

- `horario_laboral_enc` (`FK_horario_laboral_enc_turno`)

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `VW_HorarioLaboralEnc` (view)
- `VW_Operador_Horario` (view)

