---
id: rule-06-migracion-xml-json-oportunista
type: rule
title: Migración XML → JSON: oportunista, NO masiva
status: vigente
severity: medium
applies_to: [productor, consumidor, todo el equipo]
sources:
  - skill: wms-tomwms §4 #6
  - validated_at: 2026-04-27
---
# Regla 06 — Migración XML → JSON: oportunista, NO masiva

## Statement

El protocolo HH↔WSHHRN está migrando de XML legacy a JSON-sobre-SOAP-envelope (Forma A). La migración es **caso por caso, oportunista**, NO un sweep masivo.

## Rationale

El parser XML legacy tiene 8 años de bugs resueltos y casos productivos validados. Reemplazar 100% de los métodos en un release sería:
- Riesgo enorme de regresión transversal a 4-6 clientes.
- Imposible de validar en QA en tiempo razonable.
- Bloquea otros cambios de negocio durante la transición.

JSON-sobre-SOAP es preferible para nuevos métodos (más simple, más fácil de loggear/debuggear). Pero los métodos existentes que funcionan se quedan en XML.

## Cómo cumplir

1. Métodos NUEVOS de WSHHRN: usar JSON-sobre-SOAP-envelope (Forma A) por default.
2. Métodos EXISTENTES en XML: dejarlos como están salvo que haya un bug específico que justifique migrar uno particular.
3. NUNCA "limpiar" o "modernizar" un batch de métodos sin caso de negocio claro.
4. Cuando se migra uno: la HH consumer en Java debe actualizarse en bundle separado (regla 2).

## Excepciones

Refactor masivo solo si EJC lo decide explícitamente y queda como decisión documentada en `entities/decisions/`.

## Cross-refs

- `modules/mod-protocolo-hh-ws` (M3) — Forma A vs XML legacy
- `rules/rule-02-no-mezclar-hh-backend` — coordinación entre stacks
- `rules/rule-04-no-reescribir-desde-cero` — corolario
