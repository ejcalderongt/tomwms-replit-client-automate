---
id: rule-02-no-mezclar-hh-backend
type: rule
title: No mezclar HH (Java) con backend (VB.NET) en un mismo cambio
status: vigente
severity: critical
applies_to: [productor, consumidor, todo el equipo]
sources:
  - skill: wms-tomwms §4 #2
  - validated_at: 2026-04-27
---
# Regla 02 — No mezclar HH (Java) con backend (VB.NET) en un mismo cambio

## Statement

Un commit, PR o bundle NO puede mezclar cambios de TOMHH2025 (Java) con cambios de TOMWMS_BOF (VB.NET). Cada release stack viaja por separado.

## Rationale

Los repos viven en pipelines distintos: TOMHH2025 produce un APK Android (deploy a handhelds), TOMWMS_BOF produce binarios .NET (deploy a servidores). Mezclar:
- Rompe la atomicidad de cada release.
- Hace casi imposible bisect cuando aparece un bug.
- Bloquea hotfixes de un stack porque el otro no está listo.

## Cómo cumplir

1. Si una feature requiere cambios en HH **y** backend (ej. nuevo WebMethod consumido por nueva pantalla HH):
   - Bundle 1: cambio backend (WebMethod nuevo, retrocompatible).
   - Re-indexar VB en Brain. Validar que HH actual sigue funcionando.
   - Bundle 2: cambio HH que consume el WebMethod nuevo.
2. Documentar la dependencia en el bundle 2 (qué versión de backend requiere).
3. NO juntar ambos en un solo commit.

## Excepciones

Ninguna. La excepción es la propia regla: si parece que necesitás mezclarlos, **estás haciendo algo mal** — refactorizá la feature en 2 deploys.

## Cross-refs

- `modules/mod-protocolo-hh-ws` (M3) — interface entre stacks
- `rules/rule-12-no-romper-compatibilidad` — backend nuevo debe ser retrocompatible mientras HH viejo siga en campo
