# 20260428-1905-H06 - Q-011 valida que el proceso 'estado=Despachado sin trans_despacho_det' existe como camino real en TOMWMS_KILLIOS_PRD.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras Ciclo 8a via ejecucion live SQL.

## Contexto

Ejecucion live contra TOMWMS_KILLIOS_PRD (28-abr-2026, snapshot del EC2 al que tenemos acceso): de 3989 pedidos en estado='Despachado', 1 caso (2025-06) tiene 0 filas en trans_despacho_det. Esto VALIDA EMPIRICAMENTE que el proceso de bypass (P-19) existe como camino tecnicamente posible en el WMS - el estado se puede mover a 'Despachado' sin que se materialice el detalle. **Aclaracion metodologica importante**: la frecuencia observada en este snapshot NO es comparable con la frecuencia reportada por las personas del cliente, porque ellas trabajan sobre backups mas recientes y nosotros sobre el snapshot del EC2. Validamos AFINIDAD DE PROCESOS (que el camino existe, que las tablas se relacionan asi); la AFINIDAD DE DATOS (cuantos casos exactos hay, distribucion mensual real) requiere otro segmento de trabajo con backups sincronizados. Por lo tanto, la decision sobre ADR-012 NO se ajusta con base a frecuencia observada en este snapshot - se sostiene en el HALLAZGO DE PROCESO de que el bypass es tecnicamente posible.

## Modulos tocados

- `ADR-012`
- `trans_despacho_det`
- `trans_pe_enc`
- `estado-despachado`
- `proceso-bypass`

## Hallazgo de PROCESO (validado)

El camino tecnico "estado='Despachado' sin filas en trans_despacho_det" existe en el modelo y se ejerce
al menos ocasionalmente. NO requiere validacion server-side bloqueante para ocurrir.

## Hallazgo de DATOS (diferido)

La frecuencia observada en este snapshot (1 caso) NO es comparable con la frecuencia reportada por las
personas del cliente (otro snapshot). La comparacion queda diferida a un segmento de "afinidad de
datos" que requeriria sincronizacion de backups.

## Decision provisional

`accepted_proceso_bypass_validado_frecuencia_diferida_a_afinidad_datos`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012.json`
- Tags: `validated-via-live-sql`, `afinidad-procesos`, `killios`, `adr-revision`, `q-011`, `proceso-validado`
- Preguntas origen: `P-19`, `Q-011`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
