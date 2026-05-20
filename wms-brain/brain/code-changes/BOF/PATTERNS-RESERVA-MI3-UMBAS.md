# Patron: sobre-reserva en `Reserva_Stock_From_MI3` por confusion UMBAS vs presentacion

> Origen: handoff `2026-05-19-codex-learning-byb-mi3-reserva-umbas`
> (commit `a256cb7`, Codex local 2026-05-19, caso BYB QA EA-153305).
>
> Promovido por Replit como Brain Keeper. Hermano conceptual del Patron 2 de
> `PATTERNS-PICKING-VERI.md` (mismo meta-patron UMBAS BOF).
> Tag commit local TOMWMS_BOF: `#EJCBYB20250519CKF`.

## Resumen

`clsLnStock_res_Partial.Reserva_Stock_From_MI3` (BOF VB.NET legacy) puede
**sobre-reservar** stock cuando el pedido entra en UMBAS (`IdPresentacion = 0`)
y el algoritmo cae en el fallback recursivo a UMBAS: arma la solicitud
recursiva con `vCantidadDecimalUMBas`, variable que tambien se usa para
fraccion/explosion de presentacion y por lo tanto no representa el pendiente
real en este flujo.

Resultado: la recursion absorbe el remanente disponible completo del stock
UMBAS objetivo, en vez de tomar solo `vCantidadPendiente`.

## Caso confirmado (BYB QA `IMS4MB_BYB_QAS2`)

| Item | Valor |
|---|---|
| Documento NAV | `EA-153305` |
| Pedido WMS | `IdPedidoEnc = 37303` |
| Producto | `00194250` |
| Linea NAV/WMS | `120000` |
| Cantidad pedida | `15 UNI` |
| Reservas generadas | `12` desde `IdStock = 757275` (`IdPresentacion = 134`, presentacion `CJ`, factor `4`) + `997` desde `IdStock = 778373` (`IdPresentacion = 0`, UMBAS) |
| Quantity_Reserved_WMS mostrado | `1009` |
| Pendiente real esperado tras consumir 12 | `15 - (12 * 4)/4 = 3` UMBAS |
| Sobre-reserva | `997 - 3 = 994` unidades de exceso |

> Importante: la primera reserva de `12` desde stock CJ es **valida** como
> consumo parcial (12 UMBAS si `vCantidadDecimalUMBas` se interpreta como
> UMBAS, no como cajas). El bug aparece en la **segunda reserva recursiva
> UMBAS** que toma 997 en vez de 3.

## Modelo del flujo (resumido)

```
trans_pe_det (pedido NAV)                 IdPresentacion = 0 → pedido en UMBAS
   │
   ▼
Reserva_Stock_From_MI3 (clsLnStock_res_Partial.vb ~25553)
   ├─ primera pasada: Inserta_Stock_Reservado(lBeStockAReservar, ...)
   │       └─ stock CJ disponible → reserva 12 (parcial valido)
   │
   ├─ calcula pendiente
   │       └─ pre-fix: vCantidadDecimalUMBas (mezclada)
   │       └─ post-fix: vCantidadPendiente (canonico cuando IdPresentacion = 0)
   │
   └─ recursion: BeStockResUMBas.Cantidad = vCantidadDecimalUMBas (mal)
                                          = vCantidadPendiente (bien)
           └─ Inserta_Stock_Reservado(BeStockResUMBas, ...)
                  └─ stock UMBAS disponible 997 → reserva 997 (pre-fix bug)
                                                = 3 (post-fix correcto)
```

## Llave de deteccion (auditoria SELECT-only)

Una linea de pedido con sobre-reserva MI3 cumple:

```
trans_pe_det.IdPresentacion = 0
trans_pe_det.Cantidad       = X UMBAS
SUM(stock_res.Cantidad) por IdPedidoEnc + IdPedidoDet
   = Y_total UMBAS-equivalentes
Y_total > X
```

Donde el UMBAS-equivalente de cada `stock_res` se calcula como
`Cantidad * (Factor si IdPresentacion > 0 sino 1)`.

> Pendiente: confirmar con Erik el calculo exacto del UMBAS-equivalente
> cuando la reserva esta en presentacion (caso 12 CJ del ejemplo: ¿son
> 12 UMBAS porque ya vienen pre-convertidas en `stock_res.cantidad`, o son
> 12 cajas = 48 UMBAS?). Esto define el query de auditoria global.

## Cambio aplicado en codigo (parche local sin commit a Azure)

Archivo: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
Funcion: `Reserva_Stock_From_MI3`, alrededor de linea `25553`.

```vbnet
' Tag: #EJCBYB20250519CKF
If pStockResSolicitud.IdPresentacion = 0 Then
    vCantidadDecimalUMBas = vCantidadPendiente
ElseIf ' (rama existente sin cambio)
    ...
End If

' Recursion UMBAS:
BeStockResUMBas.Cantidad = Math.Round(vCantidadDecimalUMBas, 6)
```

Compilacion: `DAL.dll` OK con `MSBuild ... /v:minimal`. Encoding UTF-8 BOM
preservado.

## Regla minima documentada

> "Cuando una solicitud de reserva MI3 ya viene en UMBAS
> (`IdPresentacion = 0`), la recursion para reservar el remanente debe usar
> el pendiente canonico (`vCantidadPendiente`), nunca variables intermedias
> que tambien se usan en rutas de fraccion/explosion de presentacion."

## Por que es quirurgico (alcance del parche)

- NO cambia el flujo general de `Reserva_Stock_From_MI3`.
- NO toca el motor nuevo `WMS.DALCore/Reserva_Stock`.
- NO altera la logica cuando `IdPresentacion <> 0` (alli
  `vCantidadDecimalUMBas` conserva sentido para fraccion/explosion).
- Solo corrige el caso `IdPresentacion = 0` + recursion UMBAS.

## Pendientes para Erik

- [ ] Validar que la regla aplica solo a `IdPresentacion = 0` y no rompe los
      escenarios Mercopan/Killios documentados en comentarios cercanos del
      mismo metodo.
- [ ] Decidir si replicar la misma proteccion en motor nuevo
      `WMS.DALCore.Reserva_Stock`, especialmente en `ReservationLoopStep`
      y `UMBasExplosionHandler`.
- [ ] Decidir si abrir handoff de regularizacion historica (script de
      auditoria que detecte `stock_res` agregado > cantidad solicitada por
      linea en UMBAS).
- [ ] Confirmar si el caso `EA-153314` (NAV "There is nothing to handle")
      es una segunda causa apilada (post-reserva correcta) o variante del
      mismo bug.

## Cruce con Patron VERI (mismo meta-patron)

Este bug y el Patron 2 de `PATTERNS-PICKING-VERI.md` son **manifestaciones
distintas del mismo anti-pattern de fondo**:

| Dimension | Patron 2 VERI (KILLIOS picking 1465) | Patron reserva MI3 (BYB EA-153305) |
|---|---|---|
| Archivo | `clsLnTrans_picking_ubic_Partial.vb` | `clsLnStock_res_Partial.vb` |
| Funcion | `Procesar_Verificacion_Desde_BOF` | `Reserva_Stock_From_MI3` |
| Variable problematica | `Cantidad_Recibida` (pantalla en presentacion) | `vCantidadDecimalUMBas` (mezcla decimal de presentacion y UMBAS) |
| Destino violado | `trans_movimientos.cantidad` (Familia A) | `stock_res.cantidad` via `BeStockResUMBas.Cantidad` (Familia A) |
| Sintoma | dato guardado **bajo** (72 en vez de 1728) | dato guardado **alto** (997 en vez de 3) |
| Causa raiz | no se aplica `* Factor` al persistir | no se re-ancla al pendiente canonico en recursion |
| Trigger | re-ejecutar verificar pickeados desde BOF | pedido UMBAS con fallback recursivo |
| Fix conceptual | normalizar a UMBAS antes del insert | usar `vCantidadPendiente` cuando `IdPresentacion = 0` |

**Meta-regla candidata (no promovida a `replit.md` §4 todavia):**

> "Toda rutina BOF que escribe en Familia A (`stock`, `trans_movimientos`,
> `stock_res`) debe validar invariante UMBAS justo antes del insert/update,
> sin fiarse de variables intermedias cuyo nombre mezcla unidades. Cuando
> `IdPresentacion = 0`, usar variables marcadas como `_pendiente` o
> `_canonico`; cuando `IdPresentacion > 0`, multiplicar por `Factor`."

Cuando ambos fixes BOF se mergeen a `dev_2028_merge`, promover esta meta-regla
a `replit.md` §4 (candidata regla 10) — ver `PATTERNS-UMBAS.md` §"Cuando
promover".

## Gotchas tecnicos (de LEARNINGS)

- `Quantity_Reserved_WMS` total puede ocultar la mezcla presentacion+UMBAS.
  Hay que revisar `stock_res.IdPresentacion` por linea.
- Log util: `trans_pe_det_log_reserva` traza camino exacto. En el caso 153305
  se observo `CASO_#8_EJC202310090957` → `LLR_CASO_#29` →
  `CASO_#7_EJC202310090957`.
- "There is nothing to handle" en NAV (caso `EA-153314`) puede tener causa
  distinta a la sobre-reserva WMS (lote/cantidad para "Create Pick"). NO
  mezclar diagnosticos.

## Referencias

- PROPOSAL origen: `wms-brain/brain/handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas/PROPOSAL.md`
- LEARNINGS: `wms-brain/brain/handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas/LEARNINGS.md`
- Patron hermano (VERI): `wms-brain/brain/code-changes/BOF/PATTERNS-PICKING-VERI.md` §"Patron 2"
- Regla UMBAS canonica: `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md` §"Violaciones BOF confirmadas"
- ADRs reserva BYB previos (otra dimension):
  `brain/architecture/adr/ADR-010-reserva-webapi-vs-wms-legacy.md`,
  `brain/architecture/adr/ADR-011-tras-wms-reservastock-decision.md`
- Cliente BYB indice: `brain/clients/byb.md`
- Learnings BYB previos: `brain/learnings/L-023-byb-corte-operativo-2024.md`,
  `brain/learnings/L-024-byb-verificacion-half-implemented.md`

## Hermano: diagnostico de no-reserva MI3 (agregado 2026-05-20)

Este patron cubre el parche UMBAS aplicado en `clsLnStock_res_Partial.vb` linea 25553 (commit `d5ec5e9` `#EJCBYB20250519CKF`). Para el problema apilado — cuando la reserva NO logra completar y el sistema devuelve `"No se pudo completar la reserva"` generico — ver:

- **`code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md`**: taxonomia de 10 motivos operativos para tipificar el resultado.
- **`code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md`**: matriz de paridad con `WMS.DALCore/Reserva_Stock/` (Core ya tiene `enum ReservationFailureCode` con 14 valores + `ReservationContext.FailureReasons` + `context.AddFailure(...)`).

## Commits adicionales detectados en `dev_2028_merge` post-`d5ec5e9` (2026-05-19)

El parche UMBAS no quedo solo. Le siguieron 7+ commits en el mismo archivo `clsLnStock_res_Partial.vb` y proyecto interface BYB:

| SHA | Mensaje | Cobertura |
|---|---|---|
| `4806210e` | Optimizar resta de stock reservado en reserva MI3 | Core ya lo cubre via `clsLnStock_res.Restar_Stock_Reservado`. Riesgo de duplicacion. |
| `a6d394e8` | evento_tiempo logreserva | Core: `IReservationLogger.LogCheckpoint`. Formato YAML legacy especifico. |
| `ba305b33` | mejora de traza, correccion 1 byb reserva | Core lo cubre. |
| `be4fbc50` | LOG! para proceso de reserva | Core lo cubre. |
| `ff9b3f4e` | parametro de debug en proyecto interface byb | **NO en Core**. Flag debug runtime. |
| `2c3ded78` | set debug on, cuidado!!!! | Operativo (config). |
| `faaf8534` | Arregle el loop en clsLnStock_res_Partial.vb (line 21489) | **NO en Core** (Core no usa `GoTo`). Bug legacy: estado 102 disparaba `GoTo ANALIZAR_FECHAS_DE_VENCIMIENTO` indefinidamente en zona picking cuando `Fecha_vence > FechaMinimaVenceStock`. Parche: solo `GoTo` la primera vez que se marca 102. |

Ver detalle por commit y mapping legacy<->Core en `PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md` §7.
