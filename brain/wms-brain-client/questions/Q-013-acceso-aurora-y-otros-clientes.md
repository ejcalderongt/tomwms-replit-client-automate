---
protocolVersion: 1
id: Q-013
title: Habilitar acceso read-only a DBs Aurora (ID, MH, MC, MP, IN) y resto
createdBy: agent-replit
createdAt: 2026-04-27T19:00:00Z
priority: high
status: pending
tags: [infra, acceso, mi3, aurora, ID, MH, MC, MP, IN, MS, BF, MM, LC, follow-up-A-006]
targets:
  - codename: ID
    environment: PRD
  - codename: MH
    environment: PRD
  - codename: MC
    environment: PRD
  - codename: MP
    environment: PRD
  - codename: IN
    environment: PRD
relatedDocs:
  - brain/wms-brain-client/answers/A-006-mi3-cadencia-trigger.md
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
suggestedQueries: []
expectedOutputs: []
followUp:
  ifFinding: Una vez con acceso, reabrir Q-006 y abrir Qs analogas para MS/BF/MM/LC.
  thenAsk: Q-014..N (cadencia/granularidad/tipos por cliente).
estimatedTimeMinutes: 0
allowFreeFormNotes: true
---

## Contexto

A-006 quedo en **inconclusive** porque el host autorizado para
el agente (`52.41.114.122,1437`) solo expone:
- TOMWMS_KILLIOS_PRD (K7)
- IMS4MB_BYB_PRD (BB)
- IMS4MB_CEALSA_QAS (C9)

El grupo Aurora (ID, MH, MC, MP, IN) usa una arquitectura
distinta (MI3 WCF) y sus DBs no estan accesibles. Tampoco MS,
BF, MM, LC. Esto representa **el 60-70% de los clientes** del
parque WMS.

Esta question es **operativa / de infraestructura**, no SQL: es
para coordinar con el equipo de operaciones la habilitacion de
un endpoint read-only por cliente (o un proxy unificado).

## Pregunta concreta

1. ¿Es viable abrir un endpoint read-only (usuario `read_brain`
   o similar) en cada DB Aurora y resto?
2. ¿Bajo que condiciones (IP allowlist, password rotation,
   logging)?
3. ¿O conviene mas que Erik ejecute las queries localmente y
   solo suba los resultados al brain?

## Que se espera del operador (Erik)

Esta no requiere `Invoke-WmsBrainQuestion`. Requiere:

1. Coordinar con infra / DBA team.
2. Decidir: (a) acceso directo del agente, o (b) Erik corre
   queries y sube resultados manualmente.
3. Documentar la decision en `brain/wms-specific-process-flow/`
   y cerrar esta question con verdict=`confirmed` y la
   politica como nota.

## Notas tecnicas

- Si la decision es (b), el flujo del brain no cambia: las
  questions/answer cards funcionan igual, solo el `Invoke-*` lo
  ejecuta Erik en su laptop con perfil local.
- Si es (a), agregar nuevos perfiles a la config del agente
  (`AURORA-PRD`, `MS-PRD`, etc.) — ver SPEC del PowerShell client.
