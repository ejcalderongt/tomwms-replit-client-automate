---
id: rule-03-no-tocar-reference-vb
type: rule
title: No tocar `Reference.vb` (proxies SOAP autogenerados)
status: vigente
severity: high
applies_to: [productor, consumidor, agentes locales]
sources:
  - skill: wms-tomwms §4 #3
  - validated_at: 2026-04-27
---
# Regla 03 — No tocar `Reference.vb` (proxies SOAP autogenerados)

## Statement

Los archivos `Reference.vb` en TOMWMS_BOF son **proxies SOAP autogenerados** por VS al agregar/actualizar una WebReference. NUNCA editar manualmente.

## Rationale

Cualquier edición se pierde la próxima vez que alguien (o el build) regenera el proxy con `Update Service Reference`. Cambios manuales perdidos = bugs misteriosos en producción que reaparecen al rebuildear.

La implementación real vive en los `*.asmx.vb` (server-side) o se consume desde código que llama al proxy. Si necesitás cambiar el contrato SOAP, modificá el WebMethod en `WSHHRN/*.asmx.vb`, regenerá el Reference, y solo entonces actualizá el código cliente.

## Cómo cumplir

1. Identificar si el archivo es `*.designer.vb`, `Reference.vb` o similar autogenerado (suele tener header "auto-generated, do not modify").
2. NO editar.
3. Si Brain devuelve un símbolo en Reference.vb en resultados de búsqueda → **ignorarlo**, ese código es proxy, no implementación.
4. Si necesitás cambiar el contrato SOAP, ir al `*.asmx.vb` correspondiente.

## Excepciones

Ninguna. Si Reference.vb está mal regenerado o en un estado roto, hacer `Update Service Reference` desde VS, NO editar a mano.

## Cross-refs

- `modules/mod-protocolo-hh-ws` (M3) — WSHHRN
- `modules/mod-arquitectura-solution` — proyectos del .sln
- AGENTS.md "Limitaciones conocidas del Brain" — Reference.vb aparece en results, ignorarlo
