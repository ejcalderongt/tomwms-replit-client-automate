---
id: 005-bridge-schema-v2-bump
tipo: decision
estado: vigente
titulo: "005 — Bump del brain bridge a SCHEMA_VERSION=\"2\""
tags: [decision]
---

# 005 — Bump del brain bridge a SCHEMA_VERSION="2"

**Fecha**: 2026-04-27
**Autor**: agente Replit (sesion task-8 "Aprobar y aplicar el bump del bridge a schema_version 2")
**Estado**: aplicado.

## Contexto

El brain bridge nacio con `SCHEMA_VERSION="1"` cubriendo 6 tipos de
evento, todos originados en el WMS (apply de bundles, skill updates,
directivas, merges, cambios externos).

El cliente PowerShell `WmsBrainClient` (en construccion en la rama
`wms-brain-client` del mismo repo) introduce un caso nuevo:
**investigacion SQL al brain de la BD** mediante question cards
(Q-001..Q-NNN). El flujo es: pregunta → ejecucion de queries dirigidas
en K7-PRD/BB-PRD/C9-QAS → answer card → opcionalmente learning card
consolidada.

Este flujo no encaja en ningun tipo v1: no es `apply_succeeded` (no
se aplico nada al WMS), no es `skill_update` (todavia no se sabe
que actualizar), no es `directive` (no es una orden, es una
pregunta).

Hasta hoy se workarounea con `type=directive + tags=["question","Q-NNN"]`.
Es funcional pero pierde semantica y rompe el `analyze` heuristico
(que busca modulos del WMS, no preguntas SQL).

## Decision

Bumpear `SCHEMA_VERSION` a `"2"` en `scripts/brain_bridge.mjs`
(rama `main` del repo de exchange) y agregar:

- 3 tipos nuevos: `question_request`, `question_answer`,
  `learning_proposed`.
- 1 estado terminal nuevo: `answered` (specifico para
  `question_request` cuya `question_answer` ya fue producida).
- 3 analyzers especificos con dispatch por type.

## Impacto

- **Compatibilidad**: total con eventos schema 1. No se requiere
  migracion de eventos viejos.
- **Validacion**: el bridge rechaza `schema_version=1` con un type
  v2-only en `notify`. Eso fuerza al cliente PS a setear
  `schema_version: "2"` cuando emite los nuevos tipos.
- **analyze**: switch explicito por type. Tipos desconocidos lanzan
  error claro (`type 'X' sin analyzer registrado`).

## Side-effect del analyze de `question_answer`

Cuando un `question_answer` referencia un `question_request` via
`ref.answers_event_id`, el analyzer flipea automaticamente el
`question_request` a `status=answered` y agrega una entrada de
`history` con `action: "flip-status->answered"`. Esto cierra el
ciclo notify -> list -> analyze -> proposed -> answered sin necesidad
de subcomandos manuales adicionales.

## Trazabilidad

- Propuesta original: `wms-brain-client/EXTENSION-V2-PROPOSAL.md`.
- Sign-off: `brain/replit.md` §Decisiones registradas.
- Test E2E: realizado en la sesion del agente Replit (cycle Q-001 OK).
- Workaround `directive+tags` eliminado de `PROTOCOL.md` §5 y de
  `CMDLETS.md` (`New-WmsBrainQuestionEvent` pasa a emitir
  `-Type question_request` nativo).
