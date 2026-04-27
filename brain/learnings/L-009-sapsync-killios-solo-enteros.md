---
protocolVersion: 1
id: L-009
title: SAPSYNCKILLIOS solo procesa cantidades enteras (UN + codigo_variante)
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: K7
  environment: PRD
relatedQuestions: [Q-002]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-brain-client/answers/A-002-decimales-sap-killios.md
status: open
priority: medium
tags: [sap, decimales, K7, SAPSYNCKILLIOS]
---

## Que aprendimos

Las 24,190 filas historicas del outbox K7 son **100% cantidades
enteras**. La unidad dominante es `UN` (unidades sueltas) y la
"presentacion" comercial se transmite por separado en
`codigo_variante` (`Caja12`, `Caja24`, `Caja6`...). Esto significa
que el SAPSYNCKILLIOS **no requiere logica de redondeo ni
truncado**: el WMS ya hizo la conversion a unidad base antes del
insert al outbox.

## Evidencia

- Answer card: A-002 (ver `brain/wms-brain-client/answers/A-002-decimales-sap-killios.md`)
- Query especifica: `Q-002/q1-cantidades-fraccional-vs-entera`
- Output relevante (sanitizado):

```
tipo_cantidad | tipo_transaccion | cnt   | min_val | max_val
entera        | INGRESO          | 4391  | 1       | 4256
entera        | SALIDA           | 19799 | 1       | 7344
(0 filas con cantidad fraccional)
```

## Implicancias

### Para el codigo

- El bridge / consumer no necesita helpers `roundQty()` ni
  validar precision decimal en el flujo K7 actual.
- `codigo_variante` es **dato semantico** (no decorativo): debe
  preservarse en logs y en cualquier transformacion intermedia.
- Si en el futuro un cliente Killios incorpora producto a granel
  (kg, litro, metro), reabrir Q-002 y definir politica.

### Para la operacion

- No hay riesgo operativo de "decimales perdidos en el redondeo"
  para Killios hoy.
- Monitoreo puede asumir cantidades enteras al validar bridge
  (alertar si llega una fraccion seria una anomalia).

### Para el equipo

- Documentar en `interfaces-erp-por-cliente.md` que el contrato
  con SAP B1 de Killios es siempre entero + codigo_variante.

## Acciones propuestas

- [ ] Confirmar con equipo K7: ¿hay producto a granel planificado?
- [ ] Agregar nota en `SPEC.md` del bridge sobre tipo de dato
      esperado (`int` o `float` con `cantidad == FLOOR(cantidad)`).
- [ ] Validar lo mismo para BB y otros clientes (esta learning
      aplica solo a K7).

## Como se cierra esta learning

Cuando esten cerradas las 3 acciones y la regla este consolidada
en `wms-specific-process-flow/interfaces-erp-por-cliente.md` o
similar, mover a `learnings/closed/` con la nota de cierre.
