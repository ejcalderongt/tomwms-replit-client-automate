---
protocolVersion: 1
id: A-008
answersQuestion: Q-008
title: Manejo de devoluciones en el outbox (frente nuevo)
operator: agent-replit
operatorRole: developer
target:
  codename: K7
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 8
verdict: confirmed
confidence: high
status: answered
tags: [outbox, devoluciones, K7, BB, frente-nuevo]
---

## Resumen

Confirmado: **el outbox NO contiene devoluciones hoy** ni en K7
ni en BB. Los 24,193 registros K7 son normales; los 533,329 BB
tambien. Las columnas de devolucion existen en el esquema
(`IdPedidoEncDevol`, `no_documento_salida_ref_devol`) pero
**estan 100% en NULL/vacio**. Las devoluciones se manejan por
otro canal (probablemente directo en el ERP o tabla aparte) — el
schema esta preparado pero no instrumentado por los SPs actuales.

## Hallazgos

### q1: K7 — clases de movimiento por enviado

```
tipo_transaccion | clase | enviado | cnt
--- | --- | --- | ---
INGRESO | normal | 0 | 3
INGRESO | normal | 1 | 4391
SALIDA | normal | 1 | 19799
```

**Interpretacion**: Solo clase `normal`. Cero filas con
`IdPedidoEncDevol` o `no_documento_salida_ref_devol`.

### q3: K7 — tablas de devoluciones disponibles

```
TABLE_NAME
---
motivo_devolucion
motivo_devolucion_bodega
VW_MotivoDevolucion
```

**Interpretacion**: Existen `motivo_devolucion`,
`motivo_devolucion_bodega`, `VW_MotivoDevolucion`. Hay modelo
de devoluciones en el WMS pero **no se proyecta al outbox**.

### q1-BB: BB — clases de movimiento

```
tipo_transaccion | clase | enviado | cnt
--- | --- | --- | ---
INGRESO | normal | 0 | 110795
INGRESO | normal | 1 | 107
SALIDA | normal | 0 | 145117
SALIDA | normal | 1 | 277310
```

**Interpretacion**: Mismo resultado en BB: solo `normal` para
INGRESO y SALIDA. Las 533k filas no incluyen devoluciones.

## Conclusion

- El **frente nuevo** que diseñe el bridge puede asumir que el
  outbox actual no soporta devoluciones — debera o (a) agregar
  un nuevo `tipo_transaccion='DEVOLUCION'`, o (b) leer otra
  tabla del WMS para devoluciones.
- El esquema soporta a futuro (tiene los campos) pero los SPs
  que insertan al outbox actualmente no los rellenan.
- En K7 las devoluciones probablemente entran al SAP B1 por su
  modulo de devoluciones nativo (no por SAPSYNC).

## Anomalias detectadas

- Discrepancia entre el modelo de datos (preparado para
  devoluciones) y la implementacion (sin uso). Riesgo de bug si
  algun cliente "asume" que outbox cubre devoluciones.

## Sugerencia de follow-up

- Q-010 (politica para devoluciones BB / SALIDAS `IdTipoDocumento=1`).

## Notas del operador

Confirmar con el equipo WMS-base si `IdPedidoEncDevol` se planeo
y se descarto, o si esta pendiente de implementacion.
