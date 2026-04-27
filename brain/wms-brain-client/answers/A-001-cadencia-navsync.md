---
protocolVersion: 1
id: A-001
answersQuestion: Q-001
title: Cadencia real del job que procesa SALIDAS en BB outbox
operator: agent-replit
operatorRole: developer
target:
  codename: BB
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 12
verdict: partial
confidence: medium
status: answered
tags: [outbox, navsync, BB, cadencia, PEND-07]
---

## Resumen

Confirmamos que **el procesamiento de SALIDAS es esencialmente
instantaneo (99.5% en 0 segundos entre fec_agr y fec_mod)**, lo que
descarta un scheduler periodico de minutos/horas y apunta a trigger
post-insert o llamada inmediata desde un consumer externo. Sin
embargo no logramos identificar el caller real: SQL Agent solo tiene
`syspolicy_purge_history` y la busqueda de SPs con
`UPDATE ... enviado=1` sobre el outbox devolvio 0 resultados, por
lo que **el writer de `enviado=1` no es un SP local** (probablemente
un proceso externo Win Service / NavSync.exe).

## Hallazgos

### q1: histograma horario (ultimos 30 dias)

```
(0 rows)
```

**Interpretacion**: 0 filas en los ultimos 30 dias. NavSync para
SALIDAS BB **no ha procesado nada en el mes vigente** — confirma
hipotesis de que el job esta caido o pausado actualmente, aunque
historicamente sí procesa instantaneo.

### q2: latencia tipica (vida util del outbox)

```
rango_latencia | cnt
--- | ---
1-5 seg | 1401
0 seg (instantaneo) | 275866
> 1 hora | 43
```

**Interpretacion**: De 277,310 SALIDAS marcadas enviadas en toda la
historia: 275,866 (99.48%) en **0 segundos**, 1,401 (0.51%) en 1-5
seg, y solo 43 (0.015%) > 1 hora. Patron de trigger / llamada
sincronica, no batch.

### q3: batches detectados ultimos 7 dias

```
(0 rows)
```

**Interpretacion**: 0 filas. Sin batches recientes — coherente con
q1 (no hay actividad en 30 dias).

### q4: SPs locales que actualizan enviado

```
(0 rows)
```

**Interpretacion**: 0 SPs encontrados. El UPDATE `enviado=1` no
proviene de ningun SP local en `BB-PRD`. Esto es importante: el
writer es **externo** (probablemente NavSync.exe o un servicio Windows).

### q5 (extra): SQL Agent jobs en BB-PRD

```
job_name | step_name | enabled | step_command_preview
--- | --- | --- | ---
syspolicy_purge_history | Verify that automation is enabled. | 1 | IF (msdb.dbo.fn_syspolicy_is_automation_enabled() != 1)
...
syspolicy_purge_history | Purge history. | 1 | EXEC msdb.dbo.sp_syspolicy_purge_history
syspolicy_purge_history | Erase Phantom System Health Records. | 1 | $SQLServerConnection = New-Object System.Data.SqlClient.S...
```

**Interpretacion**: Solo `syspolicy_purge_history` (sistema). No
hay scheduler de NavSync corriendo en SQL Agent — se ejecuta fuera
del motor.

## Conclusion

- Cadencia historica: **post-insert / sincronica** (~99.5% en 0 seg).
- Cadencia actual: **detenida** (0 filas en ultimos 30 dias).
- Writer: **externo al motor SQL** (no es trigger ni SP local, no es
  SQL Agent job).
- Para cerrar la pregunta al 100% se necesita identificar el
  proceso `.exe` / Windows Service que abre la conexion al WMS y
  marca `enviado=1`. Sugerencia para Erik: verificar `tasklist`
  o servicios Windows en el host de aplicacion BB.

## Anomalias detectadas

- **NavSync SALIDAS BB esta detenido al menos los ultimos 30 dias**
  (potencial bandera roja: si las SALIDAS pendientes 145k siguen
  creciendo, el ERP no esta recibiendo movimientos).
- 43 filas con latencia > 1 hora — investigar si fueron reintentos
  o reproceso manual.

## Sugerencia de follow-up

- Q-009 (estado actual del proceso NavSync.exe en host BB).

## Notas del operador

Las queries se ejecutaron con un login SQL privilegiado desde un
host autorizado (vease nota de `brain/replit.md` §5: el agente
deberia migrar a un usuario read-only `read_brain` o equivalente
cuando este disponible). No se accedio al binario NavSync en el
host de aplicacion; queda fuera del alcance del agente.
