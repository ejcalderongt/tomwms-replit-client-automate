---
protocolVersion: 1
id: A-002
answersQuestion: Q-002
title: Decimales en cantidades enviadas a SAP B1 (K7)
operator: agent-replit
operatorRole: developer
target:
  codename: K7
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 9
verdict: confirmed
confidence: high
status: answered
tags: [sap, decimales, K7, SAPSYNCKILLIOS]
---

## Resumen

Confirmado: **K7 nunca envia cantidades fraccionales al ERP**. Las
24,190 filas de outbox son 100% enteras (4,391 INGRESOS + 19,799
SALIDAS). Por lo tanto SAPSYNCKILLIOS no requiere logica de
redondeo: la fraccion la maneja el WMS via `codigo_variante`
("Caja12", "Caja24", etc.) y la conversion a unidad base ya
quedo aplicada antes del insert al outbox.

## Hallazgos

### q1: fraccional vs entera por tipo

```
tipo_cantidad | tipo_transaccion | cnt | min_val | max_val
--- | --- | --- | --- | ---
entera | INGRESO | 4391 | 1 | 4256
entera | SALIDA | 19799 | 1 | 7344
```

**Interpretacion**: Cero filas con cantidad fraccional. `min_val=1`
y `max_val=7344` para SALIDAS (todas enteras). Este es el dato
mas contundente: **no existe el problema de decimales** en el
outbox K7 hoy.

### q2: precision decimal (filas con > 0.001 fraccion)

```
(0 rows)
```

**Interpretacion**: 0 filas. Coherente con q1.

### q3: unidades de medida usadas

```
unidad_medida | codigo_variante | idunidadmedida | cnt_uso
--- | --- | --- | ---
UN | Caja12 | 1 | 6839
UN | Caja24 | 1 | 3798
UN |  | 1 | 3557
UN | Caja6 | 1 | 3035
Caja12 | Caja12 | 1 | 1005
Caja24 | Caja24 | 1 | 826
UN | Caja8 | 1 | 605
UN | Caja20 | 1 | 516
Caja6 | Caja6 | 1 | 512
Caja12 |  | 1 | 459
... (29 more rows)
```

**Interpretacion**: La unidad dominante es `UN` (unidades), y
el campo `codigo_variante` lleva el "agrupador" comercial
("Caja12", "Caja24", ...). Esto explica por que SAP recibe enteros:
el WMS multiplica al insertar al outbox (12 unidades por caja),
no envia "1.0 caja".

## Conclusion

- SAPSYNCKILLIOS no necesita redondeo ni truncado.
- Si en el futuro un cliente cambia a presentaciones fraccionables
  (kg, litro, metro), el outbox actual ya soporta `float` pero
  habria que decidir politica de redondeo del lado SAP B1.
- El campo `codigo_variante` es el "documento" semantico de la
  presentacion enviada — no se debe perder en logs ni en bridge.

## Anomalias detectadas

Ninguna detectada en esta corrida.

## Sugerencia de follow-up

- Ninguna por ahora. Ver L-009 para consolidar la regla.

## Notas del operador

Si Killios incorpora producto a granel (sucede en algunos rubros
alimenticios), reabrir esta question.
