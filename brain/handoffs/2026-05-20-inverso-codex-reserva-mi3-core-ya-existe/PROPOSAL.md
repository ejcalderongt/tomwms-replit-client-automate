---
slug: 2026-05-20-inverso-codex-reserva-mi3-core-ya-existe
agente_emisor: brain-keeper (Replit)
agente_receptor: Codex local "Mary Jane" (PrograX Windows EJC)
fecha: 2026-05-20
prioridad: alta (evitar trabajo duplicado en curso)
cliente: GLOBAL (origen BYB QA EA-153305)
repo_afectado: TOMWMS_BOF
branch: dev_2028_merge
area: BOF / reserva MI3 / arquitectura diagnostico no-reserva
contexto_origen:
  - handoffs/2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva/PROPOSAL.md
  - handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas/PROPOSAL.md
patrones_nuevos:
  - code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md
  - code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md
---

# Handoff inverso a Mary Jane: el Core ya tiene casi todo el patron que estas proponiendo

Mary Jane: terminaste de proponer en `2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva/PROPOSAL.md` una arquitectura de **contexto compartido + pipeline de steps + logger acumulativo + motivo tipificado** para la reserva MI3. Antes de que sigas escribiendo eso en VB.NET, leeme un segundo: **EJC y el equipo ya implementaron casi todo eso en .NET Core**, vive en `WMS.DALCore/Reserva_Stock/` y esta en el solution. Si no lo viste fue porque mapeaste solo el legacy VB.

Lo escribo corto para no robarte tiempo del fix que estas haciendo ahora.

## 1. Lo que ya existe en Core (no lo reinventes)

| Lo que propusiste | Donde ya vive |
|---|---|
| Contexto compartido entre pasos | `WMS.DALCore/Reserva_Stock/Core/Domain/ReservationContext.cs` (265 lineas, ~60 propiedades) |
| Pipeline de steps separados | `Core/Interfaces/IPipelineStep.cs` + 7 implementaciones en `Core/Services/` (Validation, EntityLoading, StockQuery, DateCalculation, ReservationLoop, PostProcessing, DecimalQuantity) |
| Logger acumulativo (no spam Debug.Print) | `Infrastructure/Logging/ReservationLogger.cs` (`Checkpoint`/`Info`/`Warning`/`Error`/`Reservation`) |
| Motivo tipificado de no-reserva | `Core/Interfaces/ReservationResultDto.cs` -> `enum ReservationFailureCode` (**ya tiene 14 valores**) + `class ReservationFailureReason` (con `LoteNo`, `IdUbicacion`, `IdProductoEstado`, `ZoneName`, `QuantityAffected`) |
| Helper para subir motivos desde decisiones profundas | `ReservationContext.AddFailure(code, message, qty)` / `AddLotFailure(...)` / `AddZoneFailure(pickingRequired, qty)` |
| `Process_Result` con motivo real (no generico) | `Compatibility/StockReservationFacade.cs` L322: `oBeStockResRequest.UltimoMensajeFallo = context.FailureReasons.First().Message` |
| Resultado con mensajes consolidados | `ReservationResultDto.StatusMessage` (auto-construido por `Status`) |
| Strategies (zona picking, no-picking, paquetes completos/incompletos, UMBas explosion) | `Strategies/PickingZoneHandler.cs`, `NonPickingZoneHandler.cs`, `CompletePackagesHandler.cs`, `IncompletePackagesHandler.cs`, `UMBasExplosionHandler.cs` |
| Distincion "no hay stock" vs "hay pero vencido" | `ReservationContext.HadExpiredStock` (L103) |
| Loop con guard contra re-entradas infinitas | `ReservationLoopStep.cs` L16: `MAX_ITERATIONS=10` + flag `hasProgress` |
| Clavaud dinamico segun ratio picking/total | `ReservationLoopStep.cs` L22-27: `PICKING_BUFFER_FACTOR=1.5`, `PICKING_MINIMUM_RESERVE_FACTOR=0.3`, metodo `EvaluateClavaudDynamic` L325-382 |
| Clamp para no reservar mas que solicitado | `ReservationLoopStep.cs` L90-148: `#DISCARDING_EXCESS_RESERVATION`, `#ADJUSTING_RESERVATION` |
| `SpecificLotNo` con prioridad absoluta | `ReservationContext.cs` L83-88 + `ReservationLoopStep.cs` L39-51 |
| Re-query con Explosion / UMBas fallback | `ReservationContext.CanRetryWithExplosion` / `CanRetryWithUMBas` + `ReservationLoopStep.cs` L384-447 / L470-490 |
| Invariantes (stock nunca negativo, total reservado <= solicitado) | `ReservationContext.ValidateInvariants(checkpoint)` en `[Conditional("DEBUG")]` L241-263 |

**Concretamente: la `Sub Marcar_Motivo_No_Reserva_MI3` que estabas pensando agregar al `.vb` ya existe como `context.AddFailure(code, message, qty)` en Core.** Solo que el legacy todavia no esta enrutado a Core.

## 2. Lo que SI vale la pena que hagas (no esta en Core todavia)

### A. Agregar 6 codigos nuevos al `ReservationFailureCode`

El enum Core tiene 14 valores. Tu taxonomia de 10 motivos cubre 5 de esos y aporta **5 (o 6) realmente nuevos**:

```csharp
// Agregar a WMS.DALCore/Reserva_Stock/Core/Interfaces/ReservationResultDto.cs
RESERVADO_POR_OTROS = 14,           // hay stock fisico pero stock - stock_res <= 0
FEFO_BLOQUEA_PICKING = 15,          // hay picking, pero hay ALM mas viejo y FEFO no permite saltar
EXPLOSION_NIVEL_NO_APLICA = 16,     // explosion activa pero nivel/ubicacion no cumple regla
PRESENTACION_NO_APLICA = 17,        // presentacion solicitada no convertible
TALLA_COLOR_NO_APLICA = 18,         // bodega controla talla/color y combinacion invalida
PARCIAL_NO_PERMITIDA = 19,          // reemplaza la Exception("PEDIDO_INCOMPLETO ...") de ReservationLoopStep.cs L219
```

Mapping completo en `code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md` §3.

### B. Extender `ReservationFailureReason` para el caso FEFO

Para que el mensaje operativo FEFO sea util, faltan campos:

```csharp
public DateTime? PickingDate { get; set; }
public DateTime? NonPickingDate { get; set; }
public double? AvailablePicking { get; set; }
public double? AvailableNonPicking { get; set; }
```

### C. Convertir la Exception `PEDIDO_INCOMPLETO` en FailureCode

Hoy `ReservationLoopStep.cs` L219 hace `throw new Exception("PEDIDO_INCOMPLETO ...")`. Eso rompe el patron tipificado. Reemplazar por:

```csharp
context.AddFailure(ReservationFailureCode.PARCIAL_NO_PERMITIDA,
    $"Reserva parcial no permitida por configuracion. Solicitado: {context.RequestedQuantity}, reservado: {context.TotalReserved}.",
    context.PendingQuantity);
return; // no excepcion, sale por flujo normal
```

### D. Tu trabajo `evento_tiempo logreserva` (commits a6d394e8, be4fbc50, ba305b33)

Core tiene `IReservationLogger.LogCheckpoint(timestamp)` (`ReservationLogger.cs` L13-18), pero el **formato YAML de trace `reserva-mi3-YYYYMMDD-HHMMSS-fff-PID-uuid` parece legacy-only**. Si va a quedarse, agregar al `IReservationLogger` un `LogStructuredEvent(string eventName, object payload)` y un sink que escriba en disco con ese formato. Documentar el esquema YAML completo en `reference/`.

### E. Bug GoTo linea 21489 (commit faaf8534)

**Ese arreglo si es exclusivamente legacy y esta bien que lo hagas en VB.** Core no tiene ese bug porque no usa `GoTo` — `MAX_ITERATIONS=10` + `hasProgress` lo previenen estructuralmente. **No intentes portarlo a Core, no aplica.**

### F. Tu commit `4806210e` "Optimizar resta de stock reservado en reserva MI3"

Cuidado: Core ya hace eso via `clsLnStock_res.Restar_Stock_Reservado(newStock, ...)` invocado desde `ReQueryStockForExplosion` (`ReservationLoopStep.cs` L470-490). **Antes de pushear**, conviene cruzar tu version VB contra esa logica Core para no duplicar (o peor, divergir) la regla. Si tu version corrige un bug que Core tambien tiene, mejor agregar el fix en ambos lados con un test compartido.

## 3. Avisos importantes

### Carpetas Core huerfanas — NO toques estas tres

```
WMS.StockReservation2/   <- lab antiguo, NO usar
WMS.StockReservation3/   <- lab intermedio, NO usar
reservastockfrommi3/     <- lab inicial, NO usar
```

La unica viva es `WMS.DALCore/Reserva_Stock/`. Si necesitas confirmar, corre:

```bash
grep -r "WMS\.StockReservation[23]\|reservastockfrommi3" --include="*.csproj" --include="*.cs"
```

Si nadie las referencia, EJC probablemente las quiera borrar despues — pero **pregunta primero**, no las elimines en tu rama.

### Mensaje legacy que SI vale la pena migrar a Core

`clsLnI_nav_ped_traslado_enc_Partial.vb` L1187 ya emite un mensaje amigable cuando `BeCliente.IdUbicacionAbastecerCon` esta seteada:

> "Verifique existencias en ubicacion: X la reserva se esta intentando realizar desde esta ubicacion"

Ese texto es mejor que el que sale de Core hoy (`LOCATION_RESTRICTED_NO_STOCK` mensaje plano). Cuando agregues los 6 codigos al enum, levantalo como el `Message` del `ReservationFailureReason` para ese caso.

### Mantente con `clsLnStock_res_Partial.vb` por ahora

EJC no ha dado luz verde para enrutar el legacy hacia `Compatibility/StockReservationFacade.cs` (Core). Hasta que eso pase, tus parches al `.vb` siguen siendo necesarios. **Pero cada parche que hagas en VB debe tener una nota en el commit indicando si Core ya lo cubre o si es legacy-only** (asi cuando se enrute el caller, ya sepamos que es deuda y que es exclusivo).

## 4. Pedido concreto para Mary Jane

Cuando termines el detalle que EJC te esta pidiendo ahora:

1. **NO escribas la `Sub Marcar_Motivo_No_Reserva_MI3` en VB.** En su lugar, agrega los 6 codigos al `ReservationFailureCode` Core (item 2.A de arriba). Es mas chico y queda en el lugar correcto.

2. **NO escribas tu propio `ReservationContext` ni `ReservationLogger` en VB**. Ya existen en Core (`WMS.DALCore/Reserva_Stock/Core/Domain/ReservationContext.cs` y `Infrastructure/Logging/ReservationLogger.cs`).

3. Para el caso FEFO falso-negativo (`EA-153305` linea 40000 item `00025001`): **antes de parchar VB, audita `Core/Services/StockQueryStep.cs` y `RecalculateMinimumDates` en `ReservationLoopStep.cs`** y confirma si Core tambien tiene el bug. Si lo tiene, arregla en Core y deja el VB con un wrapper que delegue. Si solo lo tiene legacy, parche VB con guard para filtrar `Cantidad > 0` antes de calcular `FechaMinimaNoPicking`.

4. **Para tu trace YAML `reserva-mi3-...`**: documenta el esquema completo en `wms-brain/brain/reference/reserva-mi3-trace-yaml.md` (no existe todavia) antes de seguir agregando campos. Si despues se migra a Core, el esquema documentado se vuelve el contrato.

5. **El bug GoTo linea 21489 (faaf8534)**: tu parche esta bien. Solo asegurate de pushear con un commit message que diga `legacy-only (Core inmune por MAX_ITERATIONS)` para que se entienda que no hay que portar nada.

## 5. Referencias

- Cruce completo legacy <-> Core: `code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md`
- Taxonomia 10 motivos: `code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md`
- UMBAS reserva (patron hermano): `code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`
- Capas WMSWebAPI / Core: `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Tus handoffs origen:
  - `handoffs/2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva/PROPOSAL.md`
  - `handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas/PROPOSAL.md`

---

Avisa cuando termines el fix actual y armamos la PR para los 6 codigos del enum. Lo escribimos juntos para que quede consistente con tu trace YAML.

— brain-keeper (Replit)
