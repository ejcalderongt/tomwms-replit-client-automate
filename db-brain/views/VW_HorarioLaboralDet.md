---
id: db-brain-view-vw-horariolaboraldet
type: db-view
title: dbo.VW_HorarioLaboralDet
schema: dbo
name: VW_HorarioLaboralDet
kind: view
modify_date: 2016-07-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_HorarioLaboralDet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-07-29 |
| Columnas | 20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdHorarioLaboralDet` | `int` |  |  |
| 2 | `IdHorarioLaboralEnc` | `int` | ✓ |  |
| 3 | `dia` | `int` | ✓ |  |
| 4 | `nombreDia` | `varchar(9)` |  |  |
| 5 | `Nhora_inicio` | `varchar(24)` | ✓ |  |
| 6 | `Nhora_fin` | `varchar(24)` | ✓ |  |
| 7 | `hora_inicio` | `datetime` | ✓ |  |
| 8 | `hora_fin` | `datetime` | ✓ |  |
| 9 | `minimo_min_hora_ingreso` | `int` | ✓ |  |
| 10 | `maximo_min_hora_ingreso` | `int` | ✓ |  |
| 11 | `minimo_min_hora_salida` | `int` | ✓ |  |
| 12 | `maximo_min_hora_salida` | `int` | ✓ |  |
| 13 | `tiempo_retraso_permitido` | `int` | ✓ |  |
| 14 | `horas_extras` | `bit` | ✓ |  |
| 15 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `fecha_baja` | `datetime` | ✓ |  |
| 20 | `activo` | `bit` | ✓ |  |

## Consume

- `horario_laboral_det`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_HorarioLaboralDet
AS
SELECT        IdHorarioLaboralDet, IdHorarioLaboralEnc, dia, 
                         CASE dia WHEN 1 THEN 'Lunes' WHEN 2 THEN 'Martes' WHEN 3 THEN 'Miercoles' WHEN 4 THEN 'Jueves' WHEN 5 THEN 'Viernes' WHEN 6 THEN 'Sabado' WHEN 7 THEN 'Domingo' ELSE '' END AS nombreDia,
                          CONVERT(VARCHAR(24), hora_inicio, 114) AS Nhora_inicio, CONVERT(VARCHAR(24), hora_fin, 114) AS Nhora_fin, hora_inicio, hora_fin, minimo_min_hora_ingreso, maximo_min_hora_ingreso, 
                         minimo_min_hora_salida, maximo_min_hora_salida, tiempo_retraso_permitido, horas_extras, user_agr, fec_agr, user_mod, fec_mod, fecha_baja, activo
FROM            dbo.horario_laboral_det
```
