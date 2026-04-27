---
protocolVersion: 1
id: L-010
title: NavSync de BB no procesa INGRESOS — los 110k pendientes son por diseño
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: BB
  environment: PRD
relatedQuestions: [Q-003]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md
status: open
priority: high
tags: [outbox, navsync, BB, bandera-roja, PEND-12]
---

## Que aprendimos

NavSync para BB **solo procesa SALIDAS** (tipo_documento=3, 99.99%
de procesamiento) y nunca proceso INGRESOS de forma sostenida.
Las 107 unicas INGRESOS marcadas `enviado=1` ocurrieron entre
mayo y julio de 2022, despues de eso 0. Las 110,795 INGRESOS
pendientes hoy (IdTipoDocumento 6, 8, 12) **no son falla a
corregir** — son resultado de que el ERP de BB recibe sus
INGRESOS por otro canal (probablemente modulo de compras nativo
del ERP) y no necesita el outbox WMS para esto.

## Evidencia

- Answer card: A-003 (`brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md`)
- Queries: `Q-003/q1`, `q2`, `q3`, `Q-003-EXTRA/q8`
- Output relevante (sanitizado):

```
INGRESO IdTipoDocumento=6  enviado=0 cnt=94993   enviado=1 cnt=16
INGRESO IdTipoDocumento=8  enviado=0 cnt=6901    enviado=1 cnt=91
INGRESO IdTipoDocumento=12 enviado=0 cnt=8879    enviado=1 cnt=0

SALIDA  IdTipoDocumento=3  enviado=0 cnt=28      enviado=1 cnt=277309
```

## Implicancias

### Para el codigo

- El bridge **no debe asumir que outbox cubre INGRESOS BB**.
- Cualquier metrica de "% procesado" en BB debe filtrar por
  `tipo_transaccion='SALIDA' AND IdTipoDocumento=3` para tener
  sentido (sino siempre dara ~50%).
- Si se necesita reportar INGRESOS al ERP, requiere flujo nuevo
  (no es bug del bridge actual).

### Para la operacion

- Los 110k pendientes consumen disco y enmascaran el monitoreo —
  considerar agregar columna `procesar` (bit) o purgar las que
  tienen mas de 2 años.
- 145k SALIDAS de IdTipoDocumento=1 tambien estan pendientes
  (similar patron, posibles devoluciones — abrir Q-010).

### Para el equipo

- Actualizar `interfaces-erp-por-cliente.md` declarando que
  **BB.NavSync = solo SALIDAS tipo_doc=3**.
- Decidir politica de purga / archivado de outbox `enviado=0`
  con antiguedad > 12 meses.

## Acciones propuestas

- [ ] Validar con equipo de BB: ¿INGRESOS al ERP llegan por
      modulo de compras o quedaria pendiente para una pasada
      futura?
- [ ] Definir politica de purga del outbox (criterios y job).
- [ ] Excluir IdTipoDocumento != 3 de los dashboards de
      "outbox pendiente".

## Como se cierra esta learning

Cerrar cuando (a) la politica de purga este implementada y
documentada, y (b) los dashboards de monitoreo se ajusten para
no alertar sobre los pendientes "por diseño".
