---
protocolVersion: 1
id: A-007
answersQuestion: Q-007
title: Granularidad de IdRecepcionDet vs encabezado (PEND-09)
operator: agent-replit
operatorRole: developer
target:
  codename: K7
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 6
verdict: confirmed
confidence: high
status: answered
tags: [outbox, recepcion, despacho, granularidad, K7, PEND-09]
---

## Resumen

Confirmado: **la granularidad del outbox K7 es por LINEA DE
DETALLE**. El 100% de los INGRESOS (4,394) tienen
`IdRecepcionDet>0`, y el 100% de las SALIDAS (19,799) tienen
`IdDespachoDet>0`. **Cero filas sin detalle**. Cada linea WMS
genera exactamente una linea outbox; el agrupamiento por
encabezado (1 despacho con 105 lineas) se reconstruye por joins.

## Hallazgos

### q1: distribucion por IdDet

```
tipo_transaccion | con_recepcion_det | con_despacho_det | sin_ningun_det | total
--- | --- | --- | --- | ---
INGRESO | 4394 | 0 | 0 | 4394
SALIDA | 0 | 19799 | 0 | 19799
```

**Interpretacion**: Particion limpia: INGRESO siempre con
`IdRecepcionDet`, SALIDA siempre con `IdDespachoDet`. Nunca
ambos, nunca ninguno. **Modelo deterministico**.

### q2: top encabezados de recepcion (mas lineas)

```
idrecepcionenc | lineas_outbox
--- | ---
434 | 36
162 | 36
71 | 35
231 | 33
337 | 33
483 | 32
400 | 32
193 | 31
88 | 31
203 | 31
... (10 more rows)
```

**Interpretacion**: El top tiene ~30+ lineas por encabezado
(idrecepcionenc), confirmando que un solo documento puede generar
docenas de filas en outbox.

### q3: cantidad parcial (envio fraccionado)

```
(0 rows)
```

**Interpretacion**: 0 filas con `cantidad_enviada <> cantidad`.
**No existe envio parcial**: el campo `cantidad_enviada`/
`cantidad_pendiente` esta declarado pero K7 no lo usa hoy.

### q4: top encabezados de despacho

```
iddespachoenc | lineas_outbox
--- | ---
3 | 105
1258 | 74
3101 | 54
2370 | 54
925 | 53
1803 | 52
2630 | 51
810 | 50
3613 | 47
2974 | 45
... (10 more rows)
```

**Interpretacion**: Top despacho con 105 lineas (idencabezado=3),
varios con 50+. Coherente con operacion mayorista de Killios.

## Conclusion

- Granularidad = linea (det). Cada `UPDATE enviado=1` afecta
  exactamente una linea outbox.
- El consumer del puente debe estar preparado para recibir N
  movimientos por encabezado y reagrupar si necesita reportar
  por documento.
- El modelo de `cantidad_enviada`/`cantidad_pendiente` sugiere
  que historicamente se penso en envio parcial, pero **no esta en
  uso** — el bridge actual asume envio atomico de la linea.

## Anomalias detectadas

- Ninguna detectada. Modelo coherente.

## Sugerencia de follow-up

- Ninguna por ahora.

## Notas del operador

Si en el futuro se introduce despacho parcial, sera importante
revisar la semantica de `cantidad_enviada` (si suma o si sigue
siendo flag).
