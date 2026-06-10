# HH Reemplazo + Cambio Ubicación (La Cumbre/Killios) — Fixes 2026-06-03

## Contexto

Este handoff consolida hallazgos y fixes aplicados en pruebas operativas de HH
relacionadas con:

1. Reemplazo en verificación/picking.
2. Carga de vencimiento/lote en verificación por búsqueda.
3. Error `XMLObject Error 2: Unexpected token` en cambio de ubicación ciega.

> Continuidad: para incidentes de concurrencia/estado desfasado posteriores,
> usar como complemento:
> `brain/handoffs/2026-06-05-hh-concurrencia-cache-sync/README.md`.

## Hallazgos clave

- El error `XMLObject Unexpected token` no fue un bug aislado del parser.
  El problema real fue mezcla de respuestas WS por concurrencia
  (`callback` y `xmlresult` compartidos) cuando había llamadas solapadas.
- En reemplazo, el flujo puede quedar sin stock reservado visible si el estado
  del reservado HH no coincide con el filtro usado en DAL/WS.
- En verificación por búsqueda (sin click en lista), la caducidad/vencimiento
  puede mostrar valor por defecto `1900` cuando no se refresca con la misma
  fuente de datos que usa selección por lista.

## Fixes aplicados

### 1) Blindaje de concurrencia WS en HH

Archivo:

- `TOMHH2025/app/src/main/java/com/dts/base/WebService.java`

Cambio:

- Ejecución serial por instancia (`singleThreadExecutor`).
- Snapshot por request de `callback`, `xmlresult`, `errorflag`, `error`.
- Restauración de snapshot antes de `wsCallBack`.

Objetivo:

- Evitar parseo cruzado JSON/XML entre callbacks concurrentes.

### 2) Reemplazo verificación/picking con trazas de diagnóstico

Patrón operativo:

- Validar línea pendiente real para reemplazo.
- Si no hay línea pendiente, devolver mensaje explícito (no silencioso).
- Trazar parámetros de entrada/salida del WS para correlación SQL.

Tags de referencia:

- `#EJC20260603_FIX_REEMPL...`
- `EJC_REEMP_VERIF_WS_IN`
- `EJC_REEMP_VERIF_WS_OUT`

### 3) Verificación por búsqueda y vencimiento

Regla:

- Unificar resolución de vencimiento/lote entre los dos entry points:
  búsqueda por código y selección por lista.
- Si el valor de BD es nulo o default técnico, mostrar mensaje operativo
  entendible y no dejar lectura ambigua.

## Checklist de regresión mínimo

1. Verificación por búsqueda muestra vencimiento correcto (no `1900`) cuando existe en BD.
2. Reemplazo parcial (ejemplo: reemplazar 2) crea pendiente correcta para picking.
3. Reemplazo sin pendiente real muestra mensaje y no deja progress colgado.
4. Cambio ubicación ciega no vuelve a lanzar `XMLObject Error 2` bajo escaneo rápido.
5. Logcat muestra correlación WS IN/OUT en casos de error.

## Riesgo residual

- Si se reintroduce ejecución paralela compartiendo `callback/xmlresult` en WS,
  el síntoma puede reaparecer.
- Si se altera catálogo/estado del reservado HH sin homologar filtros WS/DAL,
  el reemplazo puede volver a quedarse sin candidatos.
