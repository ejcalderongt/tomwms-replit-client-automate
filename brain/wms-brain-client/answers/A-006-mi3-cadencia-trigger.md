---
protocolVersion: 1
id: A-006
answersQuestion: Q-006
title: Cadencia y trigger de MI3 WCF para grupo Aurora (PEND-06)
operator: agent-replit
operatorRole: developer
target:
  codename: ID
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 1
verdict: inconclusive
confidence: low
status: answered
tags: [mi3, wcf, aurora, ID, MH, MC, MP, IN, PEND-06]
---

## Resumen

**No fue posible responder esta question en esta corrida.** Los
DBs del grupo Aurora (ID-PRD, MH-PRD, MC-PRD, MP-PRD, IN-PRD) no
estan accesibles desde el endpoint autorizado para el agente, que
solo expone los DBs de los codenames K7-PRD, BB-PRD y C9-QAS.
Como proxy se busco en K7 si hay tablas `mi3*` / `aurora*` /
`wcf*` y devolvio 0 resultados — confirmando que MI3 es un canal
de los DBs Aurora, no replicado en K7.

## Hallazgos

### q1: busqueda proxy de tablas MI3 en K7-PRD

```
(0 rows)
```

**Interpretacion**: 0 tablas. K7 no participa en el flujo MI3
(consistente con la doc: K7 usa SAPSYNCKILLIOS, no MI3).

## Conclusion

- Veredicto **inconclusive** por falta de acceso al DB target.
- Para responder Q-006 se necesita acceso directo a una de las
  bases del grupo Aurora (ID-PRD, MH-PRD, etc.) o que el operador
  ejecute las queries originales en su entorno.
- Sugerencia: levantar un host autorizado a Aurora con la misma
  herramienta y reabrir la pregunta.

## Anomalias detectadas

- Acceso restrictivo: el agente no tiene visibilidad del 60% de
  los clientes (Aurora, MS, BF, MM, LC). Esto limita la cobertura
  de futuras questions cross-cliente.

## Sugerencia de follow-up

- Q-013 (acceso a DBs Aurora — coordinar con infra para abrir un
  endpoint read-only).

## Notas del operador

Esta question quedara abierta (status=answered/inconclusive) hasta
contar con acceso a los DBs Aurora.
