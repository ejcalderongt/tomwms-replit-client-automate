---
id: db-brain-view-vw-horariolaboralenc
type: db-view
title: dbo.VW_HorarioLaboralEnc
schema: dbo
name: VW_HorarioLaboralEnc
kind: view
modify_date: 2016-07-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_HorarioLaboralEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-07-12 |
| Columnas | 7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdJornada` | `int` | ✓ |  |
| 4 | `IdTurno` | `int` | ✓ |  |
| 5 | `Jornada` | `nvarchar(50)` | ✓ |  |
| 6 | `Turno` | `nvarchar(50)` | ✓ |  |
| 7 | `Activo` | `bit` | ✓ |  |

## Consume

- `horario_laboral_enc`
- `jornada_laboral`
- `turno`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_HorarioLaboralEnc
AS
SELECT        dbo.horario_laboral_enc.IdHorarioLaboralEnc AS Código, dbo.horario_laboral_enc.IdBodega, dbo.horario_laboral_enc.IdJornada, dbo.horario_laboral_enc.IdTurno, 
                         dbo.jornada_laboral.nombre_jornada AS Jornada, dbo.turno.nombre AS Turno, dbo.horario_laboral_enc.activo AS Activo
FROM            dbo.horario_laboral_enc LEFT OUTER JOIN
                         dbo.jornada_laboral ON dbo.horario_laboral_enc.IdJornada = dbo.jornada_laboral.IdJornada LEFT OUTER JOIN
                         dbo.turno ON dbo.horario_laboral_enc.IdTurno = dbo.turno.IdTurno
```
