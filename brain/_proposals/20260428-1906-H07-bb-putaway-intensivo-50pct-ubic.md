# 20260428-1906-H07 - Becofarma tiene distribucion de tareas HH muy distinta a Killios: 50% UBIC vs 71% PIK.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras Ciclo 8a via ejecucion live SQL.

## Contexto

Q-014 ejecutada contra 3 BDs (28-abr-2026): K7-PRD 71% PIK + 28% RECE (outbound-heavy). BB-PRD 50% UBIC + 31% RECE + 19% PIK (putaway-heavy). C9-QAS 74% PIK + 26% RECE. AFINIDAD DE PROCESOS validada: las 3 BDs comparten el catalogo `sis_tipo_tarea` con los mismos IdTipoTarea (PIK, RECE, UBIC, INVE, CEST, etc.) y la tabla `tarea_hh` registra ejecuciones del mismo modo. Implicaciones para nueva WebAPI: (a) los KPIs de productividad por cliente NO pueden ser uniformes - hay que parametrizar peso de tareas porque cada deployment tiene un perfil operativo distinto; (b) el modulo de ubicacion (UBIC) merece prioridad de optimizacion en BB y baja en K7; (c) tipos como TRASL/REUB/CEST suman <1% en las 3 BDs (afinidad de datos diferida).

## Modulos tocados

- `tarea_hh`
- `sis_tipo_tarea`
- `kpis-productividad`
- `modulo-ubicacion`

## Decision provisional

`accepted_perfiles_operativos_distintos_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1906-H07-bb-putaway-intensivo-50pct-ubic.json`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
