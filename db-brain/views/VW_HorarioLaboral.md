---
id: db-brain-view-vw-horariolaboral
type: db-view
title: dbo.VW_HorarioLaboral
schema: dbo
name: VW_HorarioLaboral
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_HorarioLaboral`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `dia` | `int` | ✓ |  |
| 3 | `IdJornada` | `int` | ✓ |  |
| 4 | `Código` | `int` |  |  |
| 5 | `Día` | `varchar(9)` | ✓ |  |
| 6 | `Hora Inicio` | `varchar(8)` | ✓ |  |
| 7 | `Hora Fin` | `varchar(8)` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Consume

- `horario_laboral_det`
- `horario_laboral_enc`
- `jornada_laboral`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_HorarioLaboral]
AS
SELECT        h.IdBodega, i.dia, h.IdJornada, h.IdHorarioLaboralEnc AS Código, 
                         CASE WHEN i.dia = 1 THEN 'Lunes' WHEN i.dia = 2 THEN 'Martes' WHEN i.dia = 3 THEN 'Miércoles' WHEN i.dia = 4 THEN 'Jueves' WHEN i.dia = 5 THEN 'Viernes' WHEN
                          i.dia = 6 THEN 'Sábado' WHEN i.dia = 7 THEN 'Domingo' END AS Día, RIGHT(CONVERT(DATETIME, i.hora_inicio, 109), 8) AS [Hora Inicio], 
                         RIGHT(CONVERT(DATETIME, i.hora_fin, 109), 8) AS [Hora Fin], h.activo
FROM            dbo.horario_laboral_enc AS h INNER JOIN
                         dbo.jornada_laboral AS j ON h.IdBodega = j.IdBodega AND h.IdJornada = j.IdJornada INNER JOIN
                         dbo.horario_laboral_det AS i ON h.IdHorarioLaboralEnc = i.IdHorarioLaboralEnc
```
