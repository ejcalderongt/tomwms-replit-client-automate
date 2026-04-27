---
id: db-brain-view-vw-operador-horario
type: db-view
title: dbo.VW_Operador_Horario
schema: dbo
name: VW_Operador_Horario
kind: view
modify_date: 2024-09-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Operador_Horario`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-18 |
| Columnas | 22 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` | ✓ |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdOperador` | `int` |  |  |
| 4 | `IdJornada` | `int` |  |  |
| 5 | `fecha_inicio` | `datetime` | ✓ |  |
| 6 | `fecha_fin` | `datetime` | ✓ |  |
| 7 | `IdHorarioLaboralEnc` | `int` |  |  |
| 8 | `dia` | `int` | ✓ |  |
| 9 | `hora_inicio` | `datetime` | ✓ |  |
| 10 | `hora_fin` | `datetime` | ✓ |  |
| 11 | `minimo_min_hora_ingreso` | `int` | ✓ |  |
| 12 | `maximo_min_hora_ingreso` | `int` | ✓ |  |
| 13 | `minimo_min_hora_salida` | `int` | ✓ |  |
| 14 | `maximo_min_hora_salida` | `int` | ✓ |  |
| 15 | `tiempo_retraso_permitido` | `int` | ✓ |  |
| 16 | `horas_extras` | `bit` | ✓ |  |
| 17 | `OperadorActivo` | `bit` | ✓ |  |
| 18 | `OperadorBodegaActivo` | `bit` | ✓ |  |
| 19 | `JornadaLaboralActivo` | `bit` | ✓ |  |
| 20 | `HorarioActivo` | `bit` | ✓ |  |
| 21 | `HorarioLaboralDetActivo` | `bit` | ✓ |  |
| 22 | `TurnoActivo` | `bit` |  |  |

## Consume

- `horario_laboral_det`
- `horario_laboral_enc`
- `jornada_laboral`
- `operador`
- `operador_bodega`
- `operador_jornada_laboral`
- `turno`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Operador_Horario]
AS
select 
op.IdEmpresa, ojl.IdBodega, op.IdOperador, jl.IdJornada, jl.fecha_inicio, jl.fecha_fin, hle.IdHorarioLaboralEnc, 
                         hld.dia, hld.hora_inicio, hld.hora_fin, hld.minimo_min_hora_ingreso, hld.maximo_min_hora_ingreso, 
                         hld.minimo_min_hora_salida, hld.maximo_min_hora_salida, hld.tiempo_retraso_permitido, hld.horas_extras, 
						 op.activo AS OperadorActivo, 
                         opb.activo AS OperadorBodegaActivo, jl.activo AS JornadaLaboralActivo, hle.activo AS HorarioActivo, 
						 hld.activo AS HorarioLaboralDetActivo, 
                         tr.activo AS TurnoActivo
from operador_jornada_laboral ojl 
				INNER JOIN jornada_laboral jl on ojl.IdJornada=jl.IdJornada and ojl.IdBodega=jl.IdBodega
				INNER JOIN horario_laboral_enc hle on jl.IdJornada=hle.IdJornada and jl.IdBodega=hle.IdBodega
				INNER JOIN  horario_laboral_det hld on hle.IdHorarioLaboralEnc = hld.IdHorarioLaboralEnc
				INNER JOIN turno tr on tr.IdTurno = hle.IdTurno and tr.IdBodega= hle.IdBodega
				INNER JOIN  dbo.operador_bodega opb on ojl.IdOperador= opb.IdOperador and ojl.IdBodega=opb.IdBodega
				INNER JOIN  operador op on opb.IdOperador= op.IdOperador
```
