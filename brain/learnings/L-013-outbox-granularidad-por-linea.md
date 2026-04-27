---
protocolVersion: 1
id: L-013
title: Outbox WMS es por linea de detalle (1 outbox row = 1 RecepcionDet/DespachoDet)
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: K7
  environment: PRD
relatedQuestions: [Q-007, Q-008]
relatedDocs:
  - brain/wms-brain-client/answers/A-007-granularidad-recepcion-despacho.md
  - brain/wms-brain-client/answers/A-008-devoluciones-frente-nuevo.md
status: open
priority: medium
tags: [outbox, recepcion, despacho, granularidad, K7, PEND-09]
---

## Que aprendimos

La granularidad del outbox K7 es **siempre por linea de detalle**:
el 100% de los INGRESOS (4,394) tienen `IdRecepcionDet>0` y el
100% de las SALIDAs (19,799) tienen `IdDespachoDet>0`. **No hay
filas sin detalle ni con ambos**. Cada linea WMS produce
exactamente una fila outbox; un encabezado puede generar 30-105
filas (visto top: idencabezado=3 con 105 lineas).

Los campos `cantidad_enviada`/`cantidad_pendiente` existen pero
no se usan (0 filas con `cantidad_enviada <> cantidad`) — el
modelo asume envio atomico por linea.

Las columnas de devolucion (`IdPedidoEncDevol`,
`no_documento_salida_ref_devol`) tambien existen pero estan 100%
en NULL/vacio en K7 y BB → **outbox no maneja devoluciones**
hoy.

## Evidencia

- Answer cards: A-007, A-008
- Queries: `Q-007/q1-distribucion-iddet`, `Q-007/q3-cantidad-parcial`,
  `Q-008-K7/q1-conteo-devoluciones`, `Q-008-BB/q1-conteo-devoluciones-bb`

```
INGRESO con_recepcion_det=4394 sin_ningun_det=0 total=4394
SALIDA  con_despacho_det=19799 sin_ningun_det=0 total=19799
cantidad_enviada <> cantidad: 0 filas
clase=devolucion: 0 filas (K7 y BB)
```

## Implicancias

### Para el codigo

- El consumer / bridge **debe esperar N filas por encabezado** y
  reagrupar si su consumer aguas abajo necesita "documento".
- **No usar** `cantidad_enviada`/`cantidad_pendiente` para
  estimar avance — siempre son iguales o cero.
- Para soportar devoluciones en el futuro: requiere extender SPs
  que insertan al outbox (los campos ya estan), no requiere
  cambio de schema.

### Para la operacion

- Un solo despacho grande puede generar 100+ rows en outbox →
  el monitoreo debe usar `COUNT(DISTINCT iddespachoenc)`, no
  `COUNT(*)`, para "cuantos despachos pendientes".

### Para el equipo

- Documentar la granularidad en
  `interfaces-erp-por-cliente.md` como contrato de bridge.
- Marcar `cantidad_enviada`/`cantidad_pendiente` como "deprecated
  / unused" o eliminarlas en proxima limpieza de schema.

## Acciones propuestas

- [ ] Agregar nota en SPEC del bridge sobre granularidad por linea
- [ ] Decidir politica para devoluciones (¿extender outbox o
      tabla aparte?) — input para Q-010
- [ ] Auditar columnas no usadas para limpieza futura

## Como se cierra esta learning

Cerrar cuando la granularidad este declarada en el contrato del
bridge y la decision sobre devoluciones este tomada y documentada.
