---
id: rule-10-hello-sync-antes-de-operar
type: rule
title: Hello sync con el consumidor antes de operar
status: vigente
severity: medium
applies_to: [agente Replit, agente local consumidor (openclaw)]
sources:
  - skill: wms-tomwms §4 #10
  - validated_at: 2026-04-27
---
# Regla 10 — Hello sync con el consumidor antes de operar

## Statement

Antes de un workflow productor↔consumidor (publicar bundle, leer estado del repo de intercambio), correr `scripts/hello_sync.mjs` para confirmar que ambos lados están vivos y en versiones compatibles.

## Rationale

El productor (Replit) y consumidor (openclaw en Windows) viven en máquinas/horarios distintos. Si:
- El consumidor está caído.
- Hay desfase de versión del contrato de bundles.
- El `GITHUB_TOKEN` expiró.

… aplicar un bundle puede fallar en silencio (descarga OK, apply roto). Hello sync detecta esto antes.

## Cómo cumplir

1. Productor: `pnpm exec node scripts/hello_sync.mjs --as productor`.
2. Verificar respuesta del consumidor (`scripts/hello_sync.mjs --as consumidor` corriendo en su lado).
3. Si timeout o desfase de versión: NO publicar bundle. Coordinar con EJC.
4. Documentar en el bundle qué versión de hello_sync se usó.

## Excepciones

Operaciones puramente locales del productor (generar brain entities, leer Killios) NO requieren hello sync. Solo cuando hay handoff productor↔consumidor.

## Cross-refs

- `modules/mod-repo-exchange` — branch main contiene `scripts/hello_sync.mjs`
- `rules/rule-01-no-push-automatico-wms` — el bundle no llega solo al WMS
