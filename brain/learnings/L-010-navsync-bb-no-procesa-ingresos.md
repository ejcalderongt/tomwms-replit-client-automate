---
protocolVersion: 1
id: L-010
title: NavSync de BB dejo de procesar INGRESOS desde sep-2023 (110k pendientes)
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: BB
  environment: PRD
relatedQuestions: [Q-003, Q-009]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md
status: open
priority: high
tags: [outbox, navsync, BB, bandera-roja, PEND-12]
---

## Que aprendimos

NavSync de BB **hoy procesa de forma fiable solo SALIDAS
`IdTipoDocumento=3`** (>99%). Los INGRESOs sí fueron procesados
historicamente — 107 filas marcadas `enviado=1` distribuidas
entre **mayo-2022 y septiembre-2023** — pero el flujo se detuvo
por completo desde el 2023-09-18 (≈2.5 años sin enviar ningun
INGRESO).

Las 110,795 INGRESOs pendientes resultan de la combinacion de:
- Tipos que NavSync **nunca** ha procesado: `IdTipoDocumento`
  ∈ {1, 12} → 8,901 filas.
- Tipos que se procesaron a baja tasa entre 2022-23 y luego se
  detuvieron: `IdTipoDocumento` ∈ {6, 8} → 101,894 filas
  pendientes vs 107 enviadas (0.10%).

**La causa de la detencion en sep-2023 NO es verificable desde el
WMS** (no hay logs de NavSync ni jobs de SQL Agent ni SP locales
relacionados). Necesita inspeccion del lado cliente / del binario
NavSync (Q-009).

## Evidencia

- Answer card: A-003 (`brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md`)
- Queries: `Q-003/q1`, `q2`, `q3`, `Q-003-EXTRA/q8`
- Output relevante (sanitizado):

```
INGRESO IdTipoDocumento=6  enviado=0 cnt=94993   enviado=1 cnt=16   ultimo enviado: 2023-08
INGRESO IdTipoDocumento=8  enviado=0 cnt=6901    enviado=1 cnt=91   ultimo enviado: 2023-09-18
INGRESO IdTipoDocumento=12 enviado=0 cnt=8879    enviado=1 cnt=0    nunca enviado
INGRESO IdTipoDocumento=1  enviado=0 cnt=22      enviado=1 cnt=0    nunca enviado

SALIDA  IdTipoDocumento=3  enviado=0 cnt=28      enviado=1 cnt=277309 (99.99% live)
```

## Implicancias

### Para el codigo

- El bridge **no puede asumir que outbox cubre INGRESOS BB**.
  Hoy de hecho no los cubre.
- Cualquier metrica de "% procesado" en BB debe filtrar por
  `tipo_transaccion='SALIDA' AND IdTipoDocumento=3` para tener
  sentido (sino dara cifras enganosas al promediar tipos
  nunca-procesados con tipos sí-procesados).
- Si se necesita reportar INGRESOs al ERP, hace falta entender
  primero por que NavSync se detuvo (puede que el ERP ya los
  tome por otro canal y NavSync esta deprecated para INGRESOs).

### Para la operacion

- 110k filas envenenan el monitoreo: agregar columna `procesar`
  (bit) o purgar las pendientes con `tipo_transaccion='INGRESO'`
  y `fec_agr` < 2024.
- 145k SALIDAs `IdTipoDocumento=1` tambien estan pendientes
  (similar patron, posibles devoluciones — abrir Q-010).

### Para el equipo

- Actualizar `interfaces-erp-por-cliente.md`: documentar que
  **BB.NavSync** procesa hoy **solo SALIDA tipo_doc=3**, y que
  INGRESOs estuvieron parcialmente cubiertos en 2022-23 pero
  el flujo esta inactivo.
- Definir politica de purga / archivado de outbox `enviado=0`
  con antiguedad > 12 meses.

## Acciones propuestas

- [ ] Q-009: identificar el host del NavSync de BB y verificar
      por que dejo de procesar INGRESOs en 2023-09.
- [ ] Validar con BB: ¿INGRESOs al ERP llegan por otro canal hoy
      (modulo de compras nativo)? Si sí, deprecar formalmente
      el flujo de INGRESOs en outbox.
- [ ] Definir politica de purga del outbox (criterios y job).
- [ ] Excluir tipos nunca-procesados (`IdTipoDocumento` ∈ {1, 12}
      para INGRESO; {1, 4} para SALIDA) de los dashboards de
      "outbox pendiente" o etiquetarlos como "no reclamado".

## Como se cierra esta learning

Cerrar cuando (a) este confirmada la causa de la detencion en
2023-09 (Q-009), (b) la politica de purga / etiquetado este
implementada y documentada, y (c) los dashboards de monitoreo
ya no alerten sobre los pendientes "no reclamados" o
"deprecados".
