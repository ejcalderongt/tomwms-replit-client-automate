# Estrategia de Trazas Finas Operativas (HH/WS/DAL/BD)

Fecha: 2026-05-26  
Estado: Vigente

## Objetivo

Estandarizar el diagnóstico y corrección de procesos operativos WMS para reducir tiempo de análisis, minimizar regresiones y acelerar fixes.

## Regla base

Antes de cualquier debug/corrección en procesos operativos, revisar primero la traza fina del proceso.  
Si no existe traza fina, crearla antes del fix.

## Cobertura actual de trazas finas

- Recepción: `RECEPCION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Picking: `PICKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Packing: `PACKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Verificación: `VERIFICACION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Reemplazo: `REEMPLAZO-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Existencias: `EXISTENCIAS-HH-BOF-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Inventario cíclico (ya existente): `INV-CIC-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Índice maestro: `TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml`

## Mejoras ya existentes detectadas (baseline)

1. Recepción: homologación de `GetSingleRec` a JSON en detalle HH y reutilización de contexto para evitar roundtrip redundante.
2. Picking/Reemplazo: correcciones previas para conflicto de constraint non-negative en `trans_picking_ubic`.
3. Verificación: ajustes previos en escaneo fuera de lista general y matching por SKU.
4. Packing: filtro por licencia de packing (`MM...`) en vez de licencia de stock.
5. Existencias: corrección SQL y endurecimiento de filtros para evitar errores de sintaxis/inyección accidental.

## Estrategia operativa recomendada

### Fase 1 — Diagnóstico dirigido por traza
- Ubicar nodo exacto de falla (HH/WS/DAL/BD).
- Clasificar síntoma: regla, data, concurrencia, performance o contrato.

### Fase 2 — Fix mínimo seguro
- Corregir solo el nodo causal.
- Evitar refactor transversal sin evidencia.
- Mantener compatibilidad de contratos (especialmente HH legacy).

### Fase 3 — Instrumentación y regresión
- Medir ms por callback/endpoint.
- Validar overlap de requests.
- Ejecutar checklist corto del proceso tocado.

## Priorización sugerida para siguiente iteración

1. Picking + Reemplazo (alta criticidad operativa y bugs recurrentes).
2. Verificación (impacto directo en cierre de pedidos).
3. Packing (coherencia de licencia y performance de filtros).
4. Existencias (estabilidad de consulta y filtros).
5. Recepción (continuar tuning de secuencias seriales).

## Criterio de salida de cada mejora

- 0 errores bloqueantes.
- 0 regresiones funcionales en flujo principal.
- Tiempos de respuesta medidos antes/después.
- Evidencia en bitácora con archivo, fecha y tag técnico.
