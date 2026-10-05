---
output_type: aprendizaje
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-07-14T00:00:00-06:00
---

# L-060 - PROC: HH Packing implosion con trazas finas completas

## Regla operativa

En `frm_Packing.java`, el flujo de implosion ya debe leerse y diagnosticarse con trazas finas, no con logs sueltos.

El punto de entrada real es `implo_onScanSku()`, y desde alli el rastro util es:

1. `SCAN input=...`
2. `SCAN encolado-codigo=...`
3. `WS producto-start ...`
4. `WS producto-result ...`
5. `SCAN resolver-pendiente ...`
6. `PROCESS start ...`
7. `PROCESS match ...` o `SCAN maestra-sin-coincidir ...` o `SCAN no-en-maestra ...`
8. `ADJUST ...`
9. `BARCODE validate-start / validate-ok ...`
10. `RESET ...`

## Hallazgo

Antes de esta iteracion el archivo `frm_Packing.java` ya tenia trazas parciales, pero el proceso de implosion quedaba fragmentado:

- habia logs aislados con `TRACE_IMPLO`
- faltaba una traza unificada de entrada del scan
- faltaba visibilidad del resultado del WS antes de resolver el match
- faltaba una huella clara del ajuste de cantidades seleccionadas
- faltaba una marca de limpieza final del estado pendiente

Se agrego un helper local `traceImplo(...)` para mantener el mismo canal `TRACE_IMPLO` en todo el flujo.

## Resultado practico

Erik puede abrir la HH en su PC y seguir el ciclo completo de implosion sin tener que reconstruir el contexto a mano:

- que se escaneo
- que retorno el WS
- si hubo fallback exacto
- si el item matcheo o no
- si la seleccion quedo bloqueada por disponibilidad
- si se limpio el estado pendiente al final

## Evidencia tecnica

- HH Android: `app/src/main/java/com/dts/tom/Transacciones/Packing/frm_Packing.java`
- Grafo de Packing: `brain/handoffs/2026-05-22-codex-performance-bof-hh/PACKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Validacion ejecutada: `:app:compileDebugJavaWithJavac`

## Nota de seguimiento

Si despues Erik quiere profundizar, el siguiente candidato natural es revisar si `frm_preparacion_packing.java` merece el mismo nivel de traza para que todo el modulo Packing quede homogeneo.
