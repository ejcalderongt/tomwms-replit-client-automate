---
id: rule-01-no-push-automatico-wms
type: rule
title: No commits ni push automáticos al WMS
status: vigente
severity: critical
applies_to: [agente Replit, agente local consumidor (openclaw)]
sources:
  - skill: wms-tomwms §4 #1
  - validated_at: 2026-04-27
---
# Regla 01 — No commits ni push automáticos al WMS

## Statement

El agente Replit (productor) NO commitea ni pushea automáticamente a `TOMWMS_BOF`, `TOMHH2025` ni `DBA`. Genera bundles que el consumidor en Windows aplica con revisión humana de EJC.

## Rationale

EJC es el único firmante de cambios productivos del WMS. Push automático sin revisión rompe trazabilidad, dificulta rollback y puede mergear cambios sin contexto del equipo (GT, AG, MA, AT, MECR, CF).

## Cómo cumplir

1. El productor genera bundles `.zip` versionados en `tomwms-replit-client-automate/main`.
2. El consumidor descarga el bundle, aplica el patch en local, EJC revisa el diff.
3. El commit + push al repo WMS lo hace EJC con su propia identidad.
4. NUNCA usar `AZURE_DEVOPS_PAT` para POST/PUT contra Azure DevOps desde Replit.

## Excepciones

Ninguna. Si surge un caso (ej. CI futuro), debe documentarse como decisión nueva en `entities/decisions/`.

## Cross-refs

- `modules/mod-repo-tomwms-bof`, `mod-repo-tomhh2025`, `mod-repo-dba`
- `modules/mod-repo-exchange` — el flujo bundle correcto
- `decisions/dec-formato-commits` — autoría humana es regla
