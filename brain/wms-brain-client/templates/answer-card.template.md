---
protocolVersion: 1
id: A-XXX
answersQuestion: Q-XXX
title: <copia del title de la question>
operator: <slug, ej. ejc>
operatorRole: developer
target:
  codename: <K7|BB|C9|ID|MH|MC|MP|IN|MS|BF|MM|LC>
  environment: <PRD|QAS|DEV>
  schemaVersion: <opcional, hash de baseline si aplica>
executedAt: <ISO 8601, ej. 2026-04-27T15:30:00-03:00>
durationSeconds: <numero>
verdict: <confirmed|partial|inconclusive|rejected>
confidence: <high|medium|low>
status: answered
tags: [<heredados de la question>]
---

## Resumen

<2-4 lineas con la conclusion. Si verdict=confirmed, decir que se
confirmo y la magnitud. Si partial, decir que se confirmo y que falta.>

## Hallazgos

### q1: <id-de-query-1>

```
<output table o snippet relevante. Sanitizado: sin nombres reales.
Codenames OK. Sin servidor real, sin password, sin path local
identificable.>
```

**Interpretacion**: <1-3 lineas>

### q2: <id-de-query-2>

```
<output>
```

**Interpretacion**: <...>

<...repetir por cada query ejecutada...>

## Conclusion

<3-6 lineas. Que se confirma, que queda abierto, que sugerencia
para el siguiente paso.>

## Anomalias detectadas

- <Si hubo, listar. Si no, "Ninguna detectada en esta corrida".>

## Sugerencia de follow-up

- <id de pregunta sugerida (Q-XXX) o "Ninguna por ahora">

## Notas del operador

<Opcional. Contexto que el agente brain no tenia. Maximo 5 lineas.>
