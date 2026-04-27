---
id: rule-09-modulo-definition-sensible
type: rule
title: El cuerpo (`definition`) de SPs/vistas/funciones es información sensible
status: vigente
severity: high
applies_to: [agente Replit, Brain API, extractor, agentes locales]
sources:
  - skill: wms-tomwms §4 #9
  - validated_at: 2026-04-27
---
# Regla 09 — El cuerpo (`definition`) de SPs/vistas/funciones es información sensible

## Statement

El campo `module.definition` (texto T-SQL del cuerpo de un SP, vista o función) NO se expone fuera del brain ni se incluye en respuestas a clientes finales o terceros.

## Rationale

El definition contiene lógica de negocio interna del WMS de Erik (PrograX24): cálculos de costo, reglas de pricing por cliente, validaciones competitivas. Exfiltrarlo a:
- Logs de observabilidad de terceros.
- Respuestas a usuarios externos.
- Otros productos / clientes que no son de Erik.

… constituye filtración de propiedad intelectual del producto WMS.

## Cómo cumplir

1. **Brain API** (`/api/brain/...`): si un endpoint devuelve `definition`, restringir por `X-Brain-Token`.
2. **db-brain markdown**: vive en GitHub privado del repo de intercambio (`ejcalderongt/tomwms-replit-client-automate`). El branch `wms-db-brain` hereda esa visibilidad.
3. **Logs**: NO logear definitions completos, ni siquiera para debug. Si un script lee 39 SPs y quiere mostrar progreso, log `"Procesando SP X (Y bytes)"`, no el body.
4. **Respuestas a usuario** del agente: si el usuario pregunta "qué hace el SP X", resumir, no pegar el cuerpo entero.

## Excepciones

Si EJC pide explícitamente el cuerpo de un SP (ej. para debug en una conversación interna), está OK pegarlo en su contexto. Asumir que cualquier otro requester NO debe verlo.

## Cross-refs

- `db-brain://README` — el catálogo guarda definitions con aviso de sensibilidad
- `modules/mod-brain-api` (M3) — autenticación por token
- AGENTS.md §"No commits automáticos" / §"Reglas duras"
