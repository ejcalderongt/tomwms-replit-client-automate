# ADR-010: Dos motores de reserva — webapi vs WMS legacy

## Contexto

Existen DOS implementaciones del motor de reserva conviviendo:

1. **reserva-WMS** (legacy): VB.NET, en producción HOY. Es lo que registra los `Caso_Reserva` en `trans_pe_det_log_reserva` con sufijo `_EJC202310090957`.
2. **reserva-webapi**: .NET Core, DalCore + EntityCore, en rama `dev_2028` sin publicar. Reescrito casi por completo para usar webapis más rápidas.

Erik intentó unificarlas pero la complejidad acumulada del VB hacía la unificación titánica. Decisión: mantener reserva-WMS estable hasta que reserva-webapi esté lista.

## Decisión

- El bridge de tests (este brain) tiene como **target la reserva-webapi (dev_2028)**, NO la reserva-WMS legacy.
- Los `expected` de los escenarios RES-001..024 corresponden al comportamiento ideal de **reserva-webapi**.
- Cuando el comportamiento esperado **diverge** del observado en `trans_pe_det_log_reserva` (legacy actual), eso **no es un bug del expected**: es una corrección que la nueva implementación debe traer.
- El brain debe distinguir explicitamente "esperado (webapi)" vs "observado (WMS legacy)" en cada escenario.

## Implicancias

### Terminología

| Antes (incorrecto) | Ahora (correcto) |
|---|---|
| "comportamiento producción" | "comportamiento observado en reserva-WMS legacy" |
| "comportamiento legacy del documento" | "comportamiento esperado en reserva-webapi" |
| "discrepancia con producción" | "corrección que reserva-webapi traerá vs reserva-WMS" |

### Casos observados en reserva-WMS legacy (Killios PRD)

Solo estos `Caso_Reserva` aparecen en `trans_pe_det_log_reserva`:

| Caso | Conteo | Notas |
|---|---|---|
| #24 | 18,587 | camino feliz dominante (FEFO normal) |
| #8 | 1,675 | segundo más usado |
| #21 | 1,065 | tercero |
| #12 | 276 | |
| #20 | 267 | |
| #23 | 125 | |
| #22 | 44 | |
| #9 | 1 | rarisimo |

Más combinaciones `CASO_#X_LLR_CASO_#Y` (probablemente "Llamado Luego de Reserva") con casos secundarios #28, #29, #31.

**Casos #1..#7, #10, #11, #13..#19, #25..#27 NO se ejecutan en producción**. Son escenarios teóricos del legacy o nunca se dieron las condiciones.

### Diferencias conocidas que reserva-webapi debe corregir

1. **RES-021** (mismo vencimiento → preferir UDS sueltas): el legacy nunca lo hace porque `reservar_umbas_primero=false`. La reserva-webapi debe respetarlo cuando el flag está en true.
2. **Manejo de decimales SAP** (`convertir_decimales_a_umbas=1`): formalizar el algoritmo de conversion en webapi.
3. **Logging estructurado**: reemplazar el formato `CASO_#X_EJCxxx` con un esquema (caso_id, version_motor, contexto) parseable.

### Marca de versión del motor

`_EJC202310090957` = Erik J Calderón, build 2023-10-09 09:57. Si aparece otro sufijo, indica un build distinto del legacy. Para reserva-webapi se sugiere usar SemVer + commit hash en lugar de timestamp del autor.

## Pendientes

- Diff exhaustivo entre los `Caso_Reserva` registrados en log y los escenarios RES-001..024 del bridge.
- Capturar samples reales de cada `Caso_Reserva` con su contexto (pedido, stock, ubicación) para validar reserva-webapi cuando se publique.
- Decidir si reserva-webapi escribirá en el mismo `trans_pe_det_log_reserva` o en una tabla nueva con schema mejor.

## Estado

Aceptada. Toda la documentación del brain se reformula con esta terminología desde el ciclo 7 (2026-04-27T15:28:58.419Z).
