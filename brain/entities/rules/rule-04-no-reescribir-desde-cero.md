---
id: rule-04-no-reescribir-desde-cero
type: rule
title: No reescribir desde cero — debuggear primero
status: vigente
severity: high
applies_to: [agente Replit, agentes locales]
sources:
  - skill: wms-tomwms §4 #4
  - validated_at: 2026-04-27
---
# Regla 04 — No reescribir desde cero — debuggear primero

## Statement

Frente a un bug o un código que parece confuso, **el default es debuggear y entender**, no reescribir. Reescritura solo después de descartar todas las hipótesis con evidencia.

## Rationale

El WMS tiene 8+ años de iteraciones con clientes productivos (Killios, Becofarma, BYB, Cealsa, Mampa, La Cumbre). Cada decisión "rara" del código suele tener una razón productiva detrás (un bug específico de un cliente, un edge case de SAP, un workaround de una versión vieja de SQL Server).

Reescribir sin entender:
- Pierde reglas tácitas que solo viven en el código.
- Reintroduce bugs viejos que ya estaban resueltos.
- Multiplica el riesgo de regresión a través de los 4-6 clientes simultáneos.

## Cómo cumplir

1. Reproducir el bug. Si no se reproduce, no se entiende. Sin entendimiento no hay fix.
2. Usar el Brain para mapear quién más toca el código sospechoso (`/impact`, `/dependencies`).
3. Buscar comentarios con prefijos de autor (#EJCRP, #GT_, #AG, etc.) en el área — cuentan la historia.
4. Recién con hipótesis sólida + evidencia, proponer cambio mínimo.
5. Si la reescritura es realmente necesaria (deuda técnica acumulada), documentar la decisión en `entities/decisions/` antes de empezar.

## Excepciones

Casos de reescritura justificada (poco frecuentes):
- Migración explícita planeada (ej. .NET 8 → reemplazo de `Entity`/`DAL` legacy por `EntityCore`/`DALCore`).
- Código demostrablemente muerto (sin callers en Brain, sin uso en producción).

## Cross-refs

- `modules/mod-brain-api` (M3) — herramienta para mapear impacto antes de cambiar
- `rules/rule-11-cambios-incrementales` — corolario operativo
- AGENTS.md §"Workflow obligatorio según el caso"
