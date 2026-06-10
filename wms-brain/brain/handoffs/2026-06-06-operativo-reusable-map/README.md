# Mapa Reutilizable Operativo WMS (2026-06-06)

## Objetivo

Reducir trabajo repetitivo en sesiones operativas de HH/BOF/WS/BD, dejando una ruta única de:

1. Preflight de contexto.
2. Diagnóstico con trazas reutilizables.
3. Validación SQL estándar por proceso.
4. Cierre técnico (compilación, sync git, documentación).

## Cuándo cargar este mapa

- Incidente operativo en recepción, picking, packing, verificación, reemplazo o cambio de ubicación.
- Síntoma de concurrencia, cache desfasado o doble operación entre HH.
- Ajuste que requiere cruzar HH + BOF + DAL/WS + BD.
- Sesión con objetivo de "fix rápido seguro" y evidencia reproducible.

## Secuencia mínima obligatoria

### Paso 0 - Contexto base

1. `brain/agents/_index.yml`
2. `brain/agents/coordinator.yml`
3. `brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml`

### Paso 1 - Si hay concurrencia/cache

4. `brain/handoffs/2026-06-05-hh-concurrencia-cache-sync/README.md`

### Paso 2 - Si hay Jira

5. `brain/handoffs/2026-05-22-codex-performance-bof-hh/BITACORA_JIRA.md`
6. `tools/jira-task-assistant/README.md`

## Plantillas de validación que NO se deben rehacer

Referencia principal:

- `brain/handoffs/2026-06-05-hh-concurrencia-cache-sync/README.md`

Bloques estándar:

- Plantilla A: estado de línea en recepción (`trans_oc_det`, `trans_oc_det_lote`).
- Plantilla B: estado de línea en picking/verificación (`trans_picking_ubic`).
- Plantilla C: completitud por licencia/caja master.

Regla:

- Reusar plantilla + cambiar parámetros.
- Evitar crear query nueva ad hoc si el objetivo es el mismo.

## Mapa de tareas repetitivas (checklist corto)

### A. Diagnóstico operativo

1. Identificar proceso y pantalla exacta.
2. Buscar tag de traza de ese proceso en logcat.
3. Ejecutar plantilla SQL estándar.
4. Clasificar causa:
   - data inconsistente
   - validación/flujo HH
   - regla WS/DAL
   - concurrencia/cache

### B. Fix seguro

1. Aplicar cambio mínimo en punto causal.
2. Agregar tag inline `#EJCYYYYMMDD_*`.
3. Agregar traza de entrada/salida si el flujo es crítico.
4. Validar que no rompe contrato HH/WS (campos de entity compartida).

### C. Cierre técnico

1. Compilar proyecto afectado:
   - BOF: `WMS.vbproj` (o solución objetivo).
   - HH: módulo `app` en `TOMHH2025`.
2. Validar `0 errores`.
3. Documentar en handoff o bitácora del proceso.
4. Si Erik lo confirma: commit/push con mensaje trazable.

## Convenciones operativas consolidadas

- No agregar campos transitorios UI a entidades compartidas HH/WS.
- Si un dato es solo para grid/UI, calcularlo como campo anónimo/transitorio.
- En concurrencia: preferir fail-safe con recarga de contexto, no auto-merge local.
- Mensajes operativos en HH: preferir ExDialog en lugar de `msgbox` legacy.

## Evidencia de progreso reciente (sesiones 2026-06-03 a 2026-06-06)

- Blindajes de concurrencia y recarga segura en recepción/picking/verificación HH.
- Unificación de búsqueda SKU/licencia en recepción (MHS) para reducir roundtrip.
- Fortalecimiento de trazas logcat para escenarios de error no determinista.
- Corrección de compilación BOF por uso de campos transitorios sin tocar entity compartida.

## Resultado esperado al usar este mapa

- Menos iteraciones para llegar al root cause.
- Menos SQL repetido.
- Menos fixes reactivos sin evidencia.
- Mejor reutilización de trazas y documentación entre sesiones.
