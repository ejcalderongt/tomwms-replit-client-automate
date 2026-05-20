---
slug: patterns-reserva-paridad-legacy-vs-core
agente: brain-keeper
fecha: 2026-05-20
cliente: GLOBAL
ambiente: dev_2028_merge HEAD faaf853
repo_afectado: TOMWMS_BOF
branch: dev_2028_merge
area: BOF / reserva MI3 / arquitectura paralela legacy VB vs .NET Core
relacionado:
  - code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md
  - code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md
  - code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md
  - handoffs/2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva
---

# Reserva de stock: paridad legacy VB vs .NET Core

Cruce de superficie funcional entre `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` (legacy) y `WMS.DALCore/Reserva_Stock/` (Core). Objetivo: que un agente que reciba un BRIEF de reserva sepa **donde mirar primero**, **que ya existe en Core**, y **que solo vive en legacy**.

## 0. Carpetas Core existentes (importante: hay 4 copias)

```
WMS.StockReservation2/     ← lab antiguo, ~2,263 lineas (huerfano)
WMS.StockReservation3/     ← lab intermedio, ~1,422 lineas (huerfano)
reservastockfrommi3/       ← lab inicial (huerfano)
WMS.DALCore/Reserva_Stock/ ← VERSION VIVA, ~3,833 lineas + fachada legacy + DTO
```

**Regla**: tocar solo `WMS.DALCore/Reserva_Stock/`. Las otras 3 son iteraciones abandonadas con la misma estructura pero sin fachada legacy (`Infrastructure/Legacy/clsLnStock_res_Facade.cs`) ni `ReservationResultDto.cs`. Si una refactor toca las 4, es ruido — confirmar con EJC si pueden borrarse.

## 1. Funcion canonica: `Reserva_Stock_From_MI3`

| Aspecto | Legacy VB | Core (`WMS.DALCore/Reserva_Stock`) |
|---|---|---|
| Archivo | `clsLnStock_res_Partial.vb` | `Compatibility/StockReservationFacade.cs` + `Infrastructure/Legacy/clsLnStock_res_Facade.cs` |
| Lineas funcion | 18513 -> 26850 = **8,337 lineas en una sola Function** | 3 sobrecargas publicas (~110, ~80, ~30 lineas) que delegan a `Reserva_Stock_Internal` (~100) y a `pipeline.Execute(context)` |
| Firma original | `ByRef pStockResSolicitud, ByVal DiasVencimiento, ..., ByRef pListStockResOUT, ByRef lConnection, ByRef ltransaction, Optional No_Linea, pTarea_Reabasto, pBeTrasladoDet` | 3 sobrecargas: (a) misma firma con `ref` (compat), (b) variante para `clsLnTrans_pe_det`, (c) API moderna sin `ref` |
| Control flow | `GoTo` extensivo (etiquetas `ANALIZAR_FECHAS_DE_VENCIMIENTO`, `INICIAR_EN_3`, `INICIAR_EN_4`, etc.) | Pipeline declarativo: 7 steps + loop con `MAX_ITERATIONS=10` + `hasProgress` |
| Estado | Variables locales sueltas (`vStockOrigen`, `BeStockDestino`, `ListaEstadosDeProceso`, etc.) | Objeto `ReservationContext` con ~60 propiedades tipadas |
| Reentrancia | Caller debe orquestar | Loop interno con fallbacks `Explosion` y `UMBas` |
| Manejo de error | `Try/Catch` plano, propaga `ex.Message` a `Process_Result` | `context.HasError` + `context.ErrorMessage` + `context.FailureReasons` tipificadas |

## 2. Componentes Core que NO existen en legacy

| Componente Core | Archivo | Equivalente legacy | Estado |
|---|---|---|---|
| `ReservationContext` | `Core/Domain/ReservationContext.cs` (265L) | Variables locales en `Reserva_Stock_From_MI3` | Solo Core |
| `IPipelineStep` + 7 steps (`Validation`, `EntityLoading`, `StockQuery`, `DateCalculation`, `ReservationLoop`, `PostProcessing`, `DecimalQuantity`) | `Core/Services/*.cs` | Bloques `If/GoTo` dentro de la funcion monolitica | Solo Core |
| `IReservationLogger` (Checkpoint/Info/Warning/Error/Reservation) | `Infrastructure/Logging/ReservationLogger.cs` (54L) | `Console.WriteLine` y `Debug.Print` esparcidos + `log_error_wms` desde callers | Core mas estructurado; legacy NO inserta en `log_error_wms` desde el VB de reserva |
| `enum ReservationFailureCode` (14 valores) + `ReservationFailureReason` (con `LoteNo`, `IdUbicacion`, `ZoneName`, `QuantityAffected`) | `Core/Interfaces/ReservationResultDto.cs` | Texto libre `"No se pudo completar la reserva"` (lineas 1178/1316/1752 de `clsLnI_nav_ped_traslado_enc_Partial.vb`) | Solo Core; faltan 5 motivos (ver `PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md`) |
| `ReservationResultDto.StatusMessage` (auto-construido) | `Core/Interfaces/ReservationResultDto.cs` | N/A | Solo Core |
| 5 Strategies (`PickingZoneHandler`, `NonPickingZoneHandler`, `CompletePackagesHandler`, `IncompletePackagesHandler`, `UMBasExplosionHandler`) | `Strategies/*.cs` | `Select Case` y goto a etiquetas `INICIAR_EN_N` | Solo Core; legacy es mas dificil de testear |
| Clavaud dinamico (`PICKING_BUFFER_FACTOR`, `PICKING_MINIMUM_RESERVE_FACTOR`, `EvaluateClavaudDynamic`) | `Core/Services/ReservationLoopStep.cs` L22-27, L325-382 | NO existe — legacy usa solo `Bodega.Reservar_primero_almacenaje` + `Configuration.Conservar_Zona_Picking_Clavaud` (decision binaria) | Solo Core |
| Over-reservation clamping (`#DISCARDING_EXCESS_RESERVATION`, `#ADJUSTING_RESERVATION`) | `ReservationLoopStep.cs` L90-148 | Codex lo esta optimizando manualmente en commit `4806210e` "Optimizar resta de stock reservado". Riesgo de reservar mas que solicitado existe. | Solo Core lo previene |
| `MAX_ITERATIONS=10` + `hasProgress` detection | `ReservationLoopStep.cs` L16, L57-208 | NO existe — legacy depende de que el flujo `GoTo` converja por si solo. Bug detectado: estado 102 re-disparaba `GoTo ANALIZAR_FECHAS_DE_VENCIMIENTO` indefinidamente (parche Codex commit `faaf853`). | Core inmune; legacy era vulnerable a loop infinito |
| `SpecificLotNo` con prioridad absoluta (lote especifico para transferencias) | `Core/Domain/ReservationContext.cs` L83-88 + `ReservationLoopStep.cs` L39-51 | Vive como filtro dentro del query, sin ruta privilegiada | Core mas explicito |
| `HadExpiredStock` flag (distingue "no hay" vs "hay pero vencido") | `Core/Domain/ReservationContext.cs` L103 | Aplanado en mensaje generico | Solo Core |
| `ValidateInvariants(checkpoint)` en `[Conditional("DEBUG")]` (stock nunca negativo, pending nunca negativo, total reservado <= solicitado) | `Core/Domain/ReservationContext.cs` L241-263 | NO existe | Solo Core |
| `CanRetryWithExplosion` + `CanRetryWithUMBas` con re-query | `Core/Domain/ReservationContext.cs` L202-234 + `ReservationLoopStep.cs` L384-447 | Existe equivalente pero entrelazado en `GoTo` | Core mas claro |
| `Bodega.Reservar_primero_almacenaje` | Soportado en `ReservationLoopStep.DetermineStartingPoint` L245 | Soportado en legacy (verificado en codigo) | Paridad |
| `Configuration.Explosion_Automatica` | Soportado en `TryEnableExplosionFallback` L393 | Soportado en legacy | Paridad |
| `Configuration.Rechazar_pedido_incompleto = Si` | Lanza `Exception("PEDIDO_INCOMPLETO ...")` en `ReservationLoopStep.cs` L219 | Soportado en legacy | Paridad (Core: exception; legacy: comportamiento equivalente) |

## 3. Componentes que SOLO existen en LEGACY (y Codex toco recientemente)

| Componente | Archivo / commit | Por que solo legacy |
|---|---|---|
| **Bug GoTo re-entrante estado 102** | `clsLnStock_res_Partial.vb` linea 21489 (zona picking, `ANALIZAR_FECHAS_DE_VENCIMIENTO`). Commit Codex `faaf8534`: agrega guard para que el `GoTo` solo dispare la primera vez. | Core NO tiene este bug porque no usa `GoTo`; el loop esta acotado por `MAX_ITERATIONS` y `hasProgress`. **No portable a Core.** |
| **Optimizacion resta stock reservado** | Commit `4806210e` `clsLnStock_res_Partial.vb` | Core ya lo hace via `clsLnStock_res.Restar_Stock_Reservado(newStock, ...)` invocado desde `ReQueryStockForExplosion` (`ReservationLoopStep.cs` L470-490). **Codex podria estar duplicando logica que Core ya cubre.** Cross-check con Core antes de pushear. |
| **`evento_tiempo logreserva`** | Commit `a6d394e8` | Core tiene `IReservationLogger.LogCheckpoint(timestamp)` (`ReservationLogger.cs` L13-18). **El concepto ya existe en Core**, pero el formato de trace YAML `reserva-mi3-YYYYMMDD-HHMMSS-fff-PID-uuid` parece legacy-only. Documentar formato. |
| **Parametro `debug` en proyecto interface BYB** | Commit `ff9b3f4e` | Core no tiene flag global de debug por proyecto. Tiene `[Conditional("DEBUG")]` (build-time). **Si BYB necesita toggle runtime, falta en Core.** |
| **`Marcar_Motivo_No_Reserva_MI3` (Codex local, no pusheada)** | Mencionada en handoff diagnostico-no-reserva | Core lo cubre via `context.AddFailure(code, message, qty)` (`ReservationContext.cs` L156). **Codex re-inventando lo que Core ya tiene.** |
| **Mensaje `UBICACION_CLIENTE_OBLIGATORIA` parcial** | `clsLnI_nav_ped_traslado_enc_Partial.vb` linea 1187 | Core lo tiene como `LOCATION_RESTRICTED_NO_STOCK` (3) + `RECEPTION_LOCATION_NOT_ALLOWED` (7). **Mensaje legacy mas amigable que el codigo Core.** Migrar el texto al `Message` del `ReservationFailureReason`. |

## 4. Lo que Codex propone en handoff que YA existe en Core (no migrar a mano)

| Propuesta Codex | Ya existe en | Recomendacion |
|---|---|---|
| Contexto unico compartido | `ReservationContext.cs` | Reusar |
| Pipeline de steps | `IPipelineStep` + 7 steps | Reusar |
| Logger acumulativo | `IReservationLogger` + `ReservationLogger.cs` | Reusar |
| Resultado con mensajes | `ReservationResultDto.StatusMessage` | Reusar |
| Subir motivo desde decisiones profundas | `context.AddFailure(...)` | Reusar |
| `Process_Result` con motivo real | `oBeStockResRequest.UltimoMensajeFallo = context.FailureReasons.First().Message` (`StockReservationFacade.cs` L322) | Reusar |

## 5. Lo que Codex propone que CORE TODAVIA NO TIENE

| Propuesta Codex | Estado Core | Accion |
|---|---|---|
| 5 codigos nuevos (`RESERVADO_POR_OTROS`, `FEFO_BLOQUEA_PICKING`, `EXPLOSION_NIVEL_NO_APLICA`, `PRESENTACION_NO_APLICA`, `TALLA_COLOR_NO_APLICA`) | Falta en `ReservationFailureCode` (solo 14 valores actuales) | Agregar al enum |
| `PARCIAL_NO_PERMITIDA` como codigo tipificado | Core lo lanza como `Exception("PEDIDO_INCOMPLETO ...")` (`ReservationLoopStep.cs` L219) | Convertir exception a `FailureCode` |
| Trace YAML `reserva-mi3-...` con `motivo_no_reserva` estructurado | Core solo tiene logger plano | Definir formato y `IReservationLogger.LogStructuredEvent(...)` |
| Caso FEFO con datos suficientes (fecha picking + fecha ALM + cantidades disponibles post-reserva) | Core tiene los datos en `ReservationContext` pero no los expone en el `FailureReason` | Extender `ReservationFailureReason` con `PickingDate`, `NonPickingDate`, `AvailablePicking`, `AvailableNonPicking` |
| Caso falso negativo FEFO cuando listas ALM no filtran `Cantidad > 0` | Verificar `StockQueryStep.cs` y `RecalculateMinimumDates` en `ReservationLoopStep` | Auditar — caso `EA-153305` linea 40000 pendiente |

## 6. Reglas para futuros BRIEFs / tareas

1. **Antes de tocar `clsLnStock_res_Partial.vb`**, leer `WMS.DALCore/Reserva_Stock/` y verificar si el cambio ya existe en Core. Probable: 70% del comportamiento ya esta en Core.

2. **Si Core lo cubre**, dos opciones:
   - **Migrar el flow legacy a Core**: redirigir `clsLnStock_res_Facade.cs` (legacy compat) al nuevo `StockReservationFacade.Reserva_Stock_From_MI3` (`Reserva_Stock_Internal`) si todavia no esta enrutado.
   - **Mantener legacy pero anotar deuda tecnica**: handoff que indique "este parche en VB ya existe en Core, queda en deuda mover el caller".

3. **Si Core NO lo cubre** (caso bug `GoTo` linea 21489, formato YAML trace, flag debug runtime BYB): documentar como "VB-only" en este archivo con justificacion.

4. **`Bodega.Reservar_primero_almacenaje`, `Configuration.Conservar_Zona_Picking_Clavaud`, `Configuration.Explosion_Automatica`, `Configuration.Rechazar_pedido_incompleto`**: 4 flags claves. Cualquier cambio en uno debe verificarse en LEGACY y en CORE.

5. **Antes de borrar `WMS.StockReservation2`, `WMS.StockReservation3`, `reservastockfrommi3`**: confirmar con EJC. Parecen huerfanos pero verificar con `grep -r "WMS.StockReservation2\|WMS.StockReservation3\|reservastockfrommi3" --include="*.csproj" --include="*.cs"` que ningun proyecto los referencia.

## 7. Commits Codex/EJC en `dev_2028_merge` post-`d5ec5e9` (parche UMBAS)

| SHA | Mensaje | Archivo principal | Cubierto por handoff? | Cobertura Core |
|---|---|---|---|---|
| `d5ec5e9` | `#EJCBYB20250519CKF` (parche reserva UMBAS) | `clsLnStock_res_Partial.vb` L25553 | `2026-05-19-codex-learning-byb-mi3-reserva-umbas` | Core ya UMBAS-aware (`IsUMBasModeEnabled` + `CanRetryWithUMBas`) |
| `51bf68db` | sync with rules for reservation ejc fix, with love | varios | parcial | n/a |
| `1172f3b4` | merge en byb | merge | n/a | n/a |
| `be4fbc50` | LOG! para proceso de reserva | `clsLnStock_res_Partial.vb` | `diagnostico-no-reserva` (parcial) | Core lo cubre via `IReservationLogger.LogCheckpoint` |
| `ff9b3f4e` | parametro de debug en proyecto interface byb | `*.config` BYB | **NO cubierto** | Core no tiene flag debug runtime |
| `ba305b33` | mejora de traza, correccion 1 byb reserva | `clsLnStock_res_Partial.vb` | `diagnostico-no-reserva` (parcial) | Core lo cubre |
| `2c3ded78` | set debug on, cuidado!!!! | config | **NO cubierto** | n/a (operativo, no codigo de logica) |
| `a6d394e8` | evento_tiempo logreserva | `clsLnStock_res_Partial.vb` | parcial | Core: concepto si, formato YAML no |
| `a04fdcfd` | Merge | merge | n/a | n/a |
| `8594273950` | sync | varios | n/a | n/a |
| `4806210e` | Optimizar resta de stock reservado en reserva MI3 | `clsLnStock_res_Partial.vb` | **NO cubierto explicito** | Core ya lo hace via `Restar_Stock_Reservado` |
| `faaf8534` | Arregle el loop en clsLnStock_res_Partial.vb (line 21489) [estado 102 GoTo re-entrante] | `clsLnStock_res_Partial.vb` L21489 | **NO cubierto** (handoff lo deja como "cambio local") | **Bug exclusivo legacy**, Core inmune |

## 8. Referencias

- Handoffs origen: `wms-brain/brain/handoffs/2026-05-19-codex-learning-byb-mi3-{reserva-umbas,diagnostico-no-reserva}/`
- Patrones relacionados: `PATTERNS-RESERVA-MI3-UMBAS.md`, `PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md`, `PATTERNS-WMSWEBAPI-LAYERS.md`
- Codigo legacy: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` (35,259 lineas totales)
- Codigo Core (vivo): `WMS.DALCore/Reserva_Stock/` (~3,833 lineas en 27 archivos)
- Codigo Core (huerfanos sospechados): `WMS.StockReservation2/`, `WMS.StockReservation3/`, `reservastockfrommi3/`
