---
id: rule-11-cambios-incrementales
type: rule
title: Cambios pequeños e incrementales
status: vigente
severity: high
applies_to: [agente Replit, agentes locales, todo el equipo]
sources:
  - skill: wms-tomwms §4 #11
  - validated_at: 2026-04-27
---
# Regla 11 — Cambios pequeños e incrementales

## Statement

Cada bundle, PR, commit o decisión cambia **una cosa a la vez**. Cambios atómicos y reversibles, no paquetes grandes.

## Rationale

Bundles grandes:
- Bloquean QA (más para revisar = más demora).
- Aumentan riesgo de regresión cross-cliente (Killios + Becofarma + BYB + Cealsa).
- Hacen rollback "todo o nada" — no se puede revertir solo la parte rota.

Bundles chicos:
- QA paralelo posible.
- Bisect simple cuando aparece bug.
- Rollback selectivo.

## Cómo cumplir

1. Una feature grande se descompone en N bundles del tamaño mínimo coherente (ej: feature lotes-por-cliente = bundle 1 schema, bundle 2 form, bundle 3 import excel).
2. Cada bundle es independientemente deployable y testeable.
3. Si un cambio "necesita" tocar 30 archivos en 5 áreas distintas → revisar el alcance, casi seguro hay subdivisión posible.
4. Excepción real: refactors de naming (rename de columna usada en 50 lugares) — pero documentar como decisión.

## Excepciones

Refactors transversales documentados (raros, requieren decisión explícita en `entities/decisions/`).

## Cross-refs

- `rules/rule-04-no-reescribir-desde-cero` — corolario filosófico
- `rules/rule-12-no-romper-compatibilidad` — corolario operacional
- `modules/mod-repo-exchange` — flujo de bundles
