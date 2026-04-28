# 20260428-1906-H07 - Becofarma tiene distribucion de tareas HH muy distinta a Killios: 50% UBIC vs 71% PIK.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Pasada 8a via ejecucion live SQL.

## Contexto

Q-014 ejecutada contra 3 BDs (28-abr-2026): K7-PRD 71% PIK + 28% RECE (outbound-heavy). BB-PRD 50% UBIC + 31% RECE + 19% PIK (putaway-heavy). C9-QAS 74% PIK + 26% RECE. Lectura: Becofarma corre un proceso operativo significativamente distinto - el ubicar es la tarea principal (logico para farmaceutica con miles de SKUs y rotacion alta). Implicaciones para nueva WebAPI: (a) los KPIs de productividad por cliente NO pueden ser uniformes, hay que parametrizar peso de tareas; (b) el modulo de ubicacion (UBIC) merece prioridad de optimizacion en BB y baja en K7; (c) Carol tenia razon: TRASL/REUB/CEST son irrelevantes en las 3 BDs (suma <1%).

## Modulos tocados

- `tarea_hh`
- `sis_tipo_tarea`
- `kpis-productividad`
- `modulo-ubicacion`

## Decision provisional

`accepted_perfiles_distintos_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1906-H07-bb-putaway-intensivo-50pct-ubic.json`
- Tags: `validated-via-live-sql`, `cliente-distintos-perfiles`, `becofarma`, `killios`, `cealsa`, `q-014`, `metricas`
- Preguntas origen: `P-25`, `Q-014`
- Respondedoras: `KKKL-Carol`, `SQL-agente`
- Pasada: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-pasada-8a.md`
