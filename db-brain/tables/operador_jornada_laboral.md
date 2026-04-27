---
id: db-brain-table-operador-jornada-laboral
type: db-table
title: dbo.operador_jornada_laboral
schema: dbo
name: operador_jornada_laboral
kind: table
rows: 58
modify_date: 2023-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.operador_jornada_laboral`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 58 |
| Schema modify_date | 2023-05-18 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperadorJornadaLaboral` | `int` |  |  |
| 2 | `IdOperador` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdJornada` | `int` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_operador_jornada_laboral` | CLUSTERED · **PK** | IdOperadorJornadaLaboral |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_operador_jornada_laboral_bodega` → `bodega`
- `FK_operador_jornada_laboral_jornada` → `jornada_laboral`
- `FK_operador_jornada_laboral_operador` → `operador`

## Quién la referencia

**2** objetos:

- `asignar_jornada_laboral` (stored_procedure)
- `VW_Operador_Horario` (view)

