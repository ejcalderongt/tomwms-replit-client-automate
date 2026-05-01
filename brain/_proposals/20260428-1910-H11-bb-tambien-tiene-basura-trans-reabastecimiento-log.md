---
id: 20260428-1910-H11-bb-tambien-tiene-basura-trans-reabastecimiento-log
tipo: proposal
estado: vigente
titulo: 20260428-1910-H11 - Becofarma tambien acumulo basura en trans_reabastecimiento_log (755 filas) sin usar el modulo. Extender H-02.
clientes: [byb]
tags: [proposal, cliente/byb]
---

# 20260428-1910-H11 - Becofarma tambien acumulo basura en trans_reabastecimiento_log (755 filas) sin usar el modulo. Extender H-02.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Ciclo 8a via ejecucion live SQL.

## Contexto

Verificacion durante Q-014 setup (28-abr-2026): trans_reabastecimiento_log en IMS4MB_BYB_PRD tiene 755 filas, en TOMWMS_KILLIOS_PRD tiene 1218 filas, en IMS4MB_CEALSA_QAS tiene 0 filas. Carol (P-24) habia identificado el problema solo en Killios. Ampliacion del alcance: el patron 'basura instalacion no limpiada por SP CLBD_PRC' afecta tambien a BB. Decisiones: (a) la accion atomica de H-02 (agregar trans_reabastecimiento_log al SP CLBD_PRC) es una mejora estructural del INSTALADOR, no un fix puntual de Killios; (b) hay que ejecutar TRUNCATE/DELETE FROM trans_reabastecimiento_log WHERE [criterios] tambien en BB; (c) auditar otras tablas de modulos no-usados que pudieron quedar pobladas (al menos: trans_inv_*, trans_devolucion_*, etc).

## Modulos tocados

- `SP CLBD_PRC`
- `trans_reabastecimiento_log`
- `proceso-instalacion`
- `becofarma`
- `killios`

## Decision provisional

`accepted_atomic_action_extended_to_bb_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1910-H11-bb-tambien-tiene-basura-trans-reabastecimiento-log.json`
- Tags: `validated-via-live-sql`, `bug-operativo`, `instalacion`, `sp-mantenimiento`, `becofarma`, `killios`, `concrete-action`, `extiende-h02`
- Preguntas origen: `P-24`
- Respondedoras: `CKFK-Carol`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
