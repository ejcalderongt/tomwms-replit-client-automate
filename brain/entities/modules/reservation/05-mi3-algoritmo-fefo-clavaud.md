---
id: 05-mi3-algoritmo-fefo-clavaud
tipo: entity
estado: vigente
titulo: 05 · Algoritmo FEFO + Clavaud dinámico + degradación a CASO_3
modulo: [reservation]
tags: [entity, modulo/reservation]
---

# 05 · Algoritmo FEFO + Clavaud dinámico + degradación a CASO_3

> **Propósito**: documentar el algoritmo central de selección de stock que el motor MI3 aplica para escoger qué unidades reservar y en qué orden. Cubre FEFO, tie-breakers, evaluación dinámica de Clavaud (que es la novedad arquitectónica más importante del motor nuevo respecto al legacy) y la lógica de degradación a CASO_3 cuando reservar pallets enteros no tiene sentido.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md` (visión general handlers), `02-mi3-motor-legacy-vb.md` (algoritmo legacy disperso), `03-comparison.md` §4 (diferencias semánticas), `04-mi3-config-propietario.md` (flags Clavaud).

---

## Índice

1. FEFO: definición y aplicación
2. Tie-breakers cuando hay empate de fecha
3. Cálculo de las 4 fechas mínimas en `DateCalculationStep`
4. Clavaud estático vs Clavaud dinámico
5. Evaluación de "pallet completo" (cómo el motor decide qué cuenta como pallet entero)
6. Degradación a CASO_3 cuando pendiente < 1 pallet
7. Caso patológico: zona picking con fecha menor que zona ALM
8. Pseudocódigo del loop completo (motor nuevo)
9. Diferencias con el algoritmo del legacy

---

## 1. FEFO: definición y aplicación

**FEFO** = First-Expired, First-Out. El stock con menor `Fecha_vence` se reserva primero.

### 1.1 Aplicación universal

En el motor MI3, FEFO se aplica en **todos los handlers** (CASO_1..CASO_4 y UMBasExplosion). No hay configuración para desactivarlo. Razón: las normativas farmacéuticas (Killios) y alimenticias requieren rotación FEFO obligatoria.

### 1.2 Implementación en motor nuevo

```csharp
// BaseReservationHandler.cs (patrón usado por todos los handlers)
var orderedStock = stockList
    .Where(s => s.Cantidad > 0 && !IsExpired(s, context))
    .OrderBy(s => s.Fecha_vence)
    .ThenBy(s => s.Lic_plate)
    .ToList();
```

> Nota: `IsExpired` no es `s.Fecha_vence < DateTime.Now` directo. Considera `DiasVencimiento` del producto: un stock se considera "vencido para reserva" si `s.Fecha_vence < DateTime.Now.AddDays(producto.DiasVencimiento)`. Esto evita reservar stock que vencería antes de salir.

### 1.3 Implementación en legacy

Disperso en cada bloque `For Each`. Ejemplo de INICIAR_EN_3 L2737:

```vbnet
lBeStockZonaPicking = lBeStockZonaPicking
    .Where(Function(x) x.Cantidad > 0)
    .OrderBy(Function(x) x.Fecha_vence)
    .ToList()
```

El legacy NO incluye `ThenBy(Lic_plate)` consistentemente; queda al criterio del SQL `clsLnStock.lStock` ordenar por `IdStock` u otro campo. Esto causaba **no-determinismo** en empates de fecha. El motor nuevo lo fuerza.

---

## 2. Tie-breakers cuando hay empate de fecha

Cuando dos lotes tienen exactamente la misma `Fecha_vence`, el orden de selección es:

1. **`Fecha_vence` ASC** (FEFO base)
2. **`Lic_plate` ASC** (preserva preferencia por lote más antiguo en numeración Killios)
3. **`IdStock` ASC** (último recurso, garantiza determinismo)

### 2.1 Por qué `Lic_plate`

`Lic_plate` (license plate) es el identificador único de pallet/lote en Killios. Históricamente Killios numera secuencialmente, por lo que `Lic_plate ASC` aproxima "fecha de recepción ASC" cuando dos pallets tienen idéntica fecha de vencimiento.

### 2.2 Excepción: fecha = `1900-01-01`

`vFechaDefecto = New Date(1900, 1, 1)` indica "fecha no aplicable" (típico en stock recibido sin fecha de vencimiento configurada). Estos lotes:

- En el legacy: ordenan al inicio (porque 1900 < cualquier fecha real). Esto generaba el bug histórico de reservar primero stock "sin fecha" aunque hubiera lotes reales con fecha próxima.
- En el motor nuevo: `DateCalculationStep` los reasigna a `DateTime.MaxValue` para que ordenen al final. Salvo configuración explícita del propietario.

---

## 3. Cálculo de las 4 fechas mínimas en `DateCalculationStep`

El motor nuevo concentra el cálculo de fechas mínimas FEFO en un ciclo, exponiendo 4 valores en `ReservationContext`:

| Campo                                           | Significado                                                                 | Usado por                       |
|-------------------------------------------------|------------------------------------------------------------------------------|---------------------------------|
| `MinExpirationDateGlobal`                       | Mínima fecha de toda la lista `WorkingStockList` (filtrada y descontada)    | Comparación general            |
| `MinExpirationDatePickingZone`                  | Mínima fecha solo de `StockListPickingZone`                                  | `PickingZoneHandler`           |
| `MinExpirationDateNonPickingZones`              | Mínima fecha solo de `StockListNonPickingZones`                              | `NonPickingZoneHandler`        |
| `MinExpirationCompletePalletsClavaud`           | Mínima fecha solo de pallets completos (filtrados por `Pallet_Completo`)    | `CompletePackagesHandler`      |
| `MinExpirationIncompletePalletsClavaud`         | Mínima fecha solo de pallets incompletos                                    | `IncompletePackagesHandler`    |

> Son **5 valores** de fecha mínima, no 4. La denominación "4 fechas" del título alude a las 4 dimensiones operativas distintas (global, picking, no-picking, complete vs incomplete dentro de no-picking).

### 3.1 Implementación

```csharp
// DateCalculationStep.cs
public PipelineStepResult Execute(ReservationContext context)
{
    var maxDate = DateTime.MaxValue;

    context.MinExpirationDateGlobal = context.WorkingStockList.Any()
        ? context.WorkingStockList.Min(s => NormalizeDate(s.Fecha_vence))
        : maxDate;

    context.MinExpirationDatePickingZone = context.StockListPickingZone.Any()
        ? context.StockListPickingZone.Min(s => NormalizeDate(s.Fecha_vence))
        : maxDate;

    context.MinExpirationDateNonPickingZones = context.StockListNonPickingZones.Any()
        ? context.StockListNonPickingZones.Min(s => NormalizeDate(s.Fecha_vence))
        : maxDate;

    var completePallets = context.StockListNonPickingZones
        .Where(s => s.Pallet_Completo).ToList();
    context.MinExpirationCompletePalletsClavaud = completePallets.Any()
        ? completePallets.Min(s => NormalizeDate(s.Fecha_vence))
        : maxDate;

    var incompletePallets = context.StockListNonPickingZones
        .Where(s => !s.Pallet_Completo).ToList();
    context.MinExpirationIncompletePalletsClavaud = incompletePallets.Any()
        ? incompletePallets.Min(s => NormalizeDate(s.Fecha_vence))
        : maxDate;

    return PipelineStepResult.Success();
}

private DateTime NormalizeDate(DateTime fecha)
{
    return fecha.Year < 1901 ? DateTime.MaxValue : fecha;
}
```

### 3.2 Diferencia con legacy

El legacy llama a `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` **3 veces** durante el flujo (L223, L1907, L2715, L2760), cada una haciendo lecturas a BD. El nuevo lo hace **una sola vez** sobre las listas ya en memoria, ahorrando 2-3 round-trips a Killios.

---

## 4. Clavaud estático vs Clavaud dinámico

### 4.1 Clavaud estático (legacy)

```vbnet
INICIAR_EN_1:
    If Not ListaEstadosDeProceso.Contains(100) Then
        If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then
            ' Procesa pallets completos
            ...
        End If
    End If
```

La evaluación es **una vez al entrar** a INICIAR_EN_1. Si el propietario tiene `Conservar_Zona_Picking_Clavaud = true`, se ejecuta el handler de pallets completos **independientemente de si tiene sentido**.

**Bug histórico**: cuando el cliente pedía 5 cajas y había un pallet de 100 cajas, el legacy reservaba el pallet entero, dejando un pallet "casi-completo" inutilizable.

### 4.2 Clavaud dinámico (nuevo)

```csharp
// ReservationLoopStep.EvaluateClavaudDynamic
private bool ShouldUseClavaudHandlers(ReservationContext context)
{
    if (!context.OwnerConfig.Conservar_Zona_Picking_Clavaud)
        return false;

    // Si la cantidad pendiente es menor a un pallet completo, NO uses Clavaud
    var palletSize = context.PalletSize; // CajasPorCama * CamasPorTarima de la presentación
    if (context.PendingQuantity < palletSize)
    {
        _logger.LogCheckpoint(context, "#CASO_1_CASO_2_SKIP",
            $"Pendiente {context.PendingQuantity} < pallet {palletSize}, degradando a CASO_3");
        return false;
    }

    return true;
}
```

La evaluación se hace **en cada iteración del loop** de `ReservationLoopStep`. Si la cantidad pendiente cae por debajo de un pallet, el motor degrada a CASO_3 (`PickingZoneHandler`) y deja los pallets completos intactos en zona ALM.

### 4.3 Beneficio operativo

- **Conservación de pallets**: pallets completos en ALM permanecen como tales hasta que la demanda lo justifique.
- **Eficiencia de picking**: las cantidades pequeñas se sirven desde zona picking, que está optimizada para picks fraccionados.
- **Reducción de reabastos**: si el pallet en ALM no se rompe, no hay que generar reabasto inmediato.

---

## 5. Evaluación de "pallet completo"

### 5.1 Origen del flag

`clsBeStock.Pallet_Completo` es un **boolean persistido** en la tabla `stock` de Killios. Se setea/actualiza por triggers de Killios cuando se ingresa o se modifica un pallet:

```sql
-- Pseudo-trigger conceptual
UPDATE stock
SET Pallet_Completo = (Cantidad >= (
    SELECT pp.CajasPorCama * pp.CamasPorTarima
    FROM productos_presentacion pp
    WHERE pp.IdPresentacion = stock.IdPresentacion
))
WHERE IdStock = @IdStock;
```

> El motor MI3 **no calcula** este flag en runtime. Confía en el valor persistido. Esto es una decisión deliberada para no duplicar lógica entre WMS y Killios.

### 5.2 Cálculo legacy en `Procesar_Logica_Presentacion_Stock`

El legacy SÍ recalcula en runtime:

```vbnet
vCantidadProductoPorTarima = Math.Round(
    BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima, 2)
vCantidadTarimasCompletasAPickearClavaud = Math.Round(
    pStockResSolicitud.Cantidad / vCantidadProductoPorTarima, 2)
Split_Decimal(vCantidadTarimasCompletasAPickearClavaud, wholePart, fractionalPart)
vCantidadEnteraTarimasCompletasClavaud = Convert.ToInt32(wholePart)
vCantidadDecimalTarimasCompletasClavaud = fractionalPart
```

Esto define cuántos pallets completos puede llegar a pickear el cliente (`vCantidadEnteraTarimasCompletasClavaud`) y cuál sería el remanente fraccional (`vCantidadDecimalTarimasCompletasClavaud`). Pero **no afecta** la decisión de qué stock seleccionar; solo se usa luego en CASO_2 e Incomplete handler.

---

## 6. Degradación a CASO_3 cuando pendiente < 1 pallet

### 6.1 Escenario típico

Pedido: 8 cajas de SKU X. Stock disponible:

- Pallet completo en ALM (100 cajas, vence 2026-12-31)
- Picking: 12 cajas sueltas (vence 2026-08-15)

**Comportamiento legacy**: si `Conservar_Zona_Picking_Clavaud = true`, INICIAR_EN_1 evalúa el pallet completo. Como el pallet (100 cajas) > pendiente (8 cajas), reserva 8 cajas del pallet (rompiéndolo). Pallet queda con 92 cajas, ya no es completo.

**Comportamiento nuevo**: `EvaluateClavaudDynamic` detecta `PendingQuantity (8) < PalletSize (100)` → degrada a CASO_3. Reserva 8 cajas de la zona picking. El pallet en ALM permanece intacto.

### 6.2 Pseudocódigo de la decisión

```csharp
// Dentro del loop de ReservationLoopStep
while (context.PendingQuantity > context.Tolerance)
{
    var startingPoint = context.StartingPoint;
    var useClavaud = ShouldUseClavaudHandlers(context);

    if (!useClavaud && startingPoint <= 2)
    {
        // Degradar: saltar Complete e Incomplete, ir a CASO_3
        context.StartingPoint = 3;
        _logger.LogInfo(context, "Degradando a startingPoint=3 (Clavaud no aplica)");
    }

    var chain = ServiceFactory.BuildHandlerChain(
        context.StartingPoint, useClavaud, context.IsUMBasModeEnabled);

    var result = chain.Process(context);

    if (context.PendingQuantity <= context.Tolerance) break;

    // Intentar fallback de explosión
    if (TryEnableExplosionFallback(context)) continue;

    // Intentar fallback UMBas
    if (TryEnableUMBasFallback(context)) continue;

    break; // Sin más opciones
}
```

### 6.3 Excepciones a la degradación

La degradación NO ocurre en estos casos:

1. **Pedido es de devolución a proveedor** (`pEs_Devolucion = true`): se debe respetar el pallet original.
2. **Producto exige `Reservar_En_UmBas`** (estado del producto): se va directo a UMBasExplosion.
3. **`Conservar_Zona_Picking_Clavaud = false`**: no aplica Clavaud en absoluto.

---

## 7. Caso patológico: zona picking con fecha menor que zona ALM

Es el caso que el legacy maneja con la bandera `ExcepcionFechaVenceEsInferiorEnZonaPicking` (L2773 en INICIAR_EN_3):

```vbnet
If Not ExcepcionFechaVenceEsInferiorEnZonaPicking AndAlso lBeStockZonaPicking.Count > 0 Then
    ' Reservar de zona picking
End If
```

### 7.1 Escenario

- Picking: 5 cajas de SKU X, vence 2026-05-30
- ALM: 200 cajas de SKU X, vence 2027-01-15

FEFO dice: tomar primero el de picking (vence antes). Pero hay un dilema operativo:

- Si tomamos las 5 de picking, dejamos picking vacío. Próximo pedido pequeño tendrá que reabastecerse desde ALM.
- Si tomamos de ALM, rompemos un pallet de larga vida útil para preservar picking corto.

### 7.2 Resolución legacy

La bandera `ExcepcionFechaVenceEsInferiorEnZonaPicking` se setea en `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` (no expuesto en extract). Cuando es `true`, **bloquea** el bloque de zona picking (L2773) y obliga a tomar de ALM. Esto preserva picking aunque vencerá antes.

> Es un comportamiento contraintuitivo respecto a FEFO puro, pero responde a la regla operativa: "el stock con vencimiento corto en picking se va a vender pronto por la velocidad de rotación; preservarlo permite no reabastecer constantemente".

### 7.3 Resolución nueva

`DateCalculationStep` expone `MinExpirationDatePickingZone` y `MinExpirationDateNonPickingZones` por separado. `PickingZoneHandler.CanProcess` evalúa:

```csharp
public bool CanProcess(ReservationContext context)
{
    if (!context.StockListPickingZone.Any()) return false;

    // Si el stock de picking vence antes que el de ALM y es zona de alta rotación,
    // preservar picking (no procesar acá, dejar que NonPickingZoneHandler lo agarre)
    if (context.MinExpirationDatePickingZone < context.MinExpirationDateNonPickingZones
        && context.OwnerConfig.PreservarPickingPorRotacion)
    {
        _logger.LogCheckpoint(context, "#CASO_3_SKIP",
            "Preservando picking por rotación (fecha menor que ALM)");
        return false;
    }

    return true;
}
```

> El flag `PreservarPickingPorRotacion` es un placeholder en el nuevo (no necesariamente coincide 1:1 con el comportamiento legacy). Es uno de los riesgos abiertos en `03-comparison.md` §9.

---

## 8. Pseudocódigo del loop completo (motor nuevo)

```text
ReservationLoopStep.Execute(context):

  iteration = 0
  while context.PendingQuantity > context.Tolerance:
    iteration += 1
    if iteration > MaxIterations: break  # safety

    useClavaud = EvaluateClavaudDynamic(context)
    if not useClavaud and context.StartingPoint <= 2:
      context.StartingPoint = 3

    chain = ServiceFactory.BuildHandlerChain(
      context.StartingPoint,
      useClavaud,
      context.IsUMBasModeEnabled
    )

    chain.Process(context)
    # Cada handler:
    #   1. Si CanProcess(context): procesa
    #   2. Llama a SetNext().Process(context) si aún hay pendiente
    #   3. Sino: termina cadena

    if context.PendingQuantity <= context.Tolerance:
      break  # success

    if TryEnableExplosionFallback(context):
      continue  # nueva iteración con modo Explosion

    if TryEnableUMBasFallback(context):
      continue  # nueva iteración con modo UMBas

    break  # sin más opciones, deja Partial o Failed

  # Determinación final del status
  context.FinalStatus = DetermineFinalStatus(context)
```

`DetermineFinalStatus` lógica:

```csharp
if (context.PendingQuantity <= context.Tolerance)
    return ReservationStatus.Success;

if (context.OwnerConfig.Rechazar_pedido_incompleto == tRechazarPedidoIncompleto.Si)
    throw new ReservationException(ReservationFailureCode.NO_STOCK, ...);

if (context.CreatedReservations.Any())
    return ReservationStatus.Partial;

return ReservationStatus.Failed;
```

---

## 9. Diferencias con el algoritmo del legacy

| Aspecto                                | Legacy                                          | Nuevo                                            |
|----------------------------------------|--------------------------------------------------|--------------------------------------------------|
| Cálculo fecha mínima                   | 3+ llamadas a BD (`Get_Fecha_Vence_Minima_*`)    | 1 ciclo en `DateCalculationStep` (in-memory)    |
| Tie-breaker `Lic_plate`                | No determinista (depende SQL `lStock`)          | Forzado en cada handler (`OrderBy.ThenBy`)       |
| Fechas `1900-01-01`                    | Ordenan primero (bug histórico)                 | Reasignadas a `MaxValue` (`NormalizeDate`)       |
| Evaluación Clavaud                     | Estática (1 vez)                                | Dinámica (por iteración)                         |
| Degradación a CASO_3                   | No                                              | Sí (cuando `PendingQuantity < PalletSize`)       |
| Recálculo de "pallet completo"         | Runtime (en `Procesar_Logica_Presentacion_Stock`) | Confía en flag `Pallet_Completo` persistido      |
| Bandera "fecha picking < ALM"          | `ExcepcionFechaVenceEsInferiorEnZonaPicking` (legacy) | Lógica en `PickingZoneHandler.CanProcess`        |
| Loop principal                         | GoTo entre etiquetas                            | While loop con `BuildHandlerChain` por iteración |

---

> Próximo: `06-mi3-handlers-detalle.md` documenta cada uno de los 5 handlers (CompletePackages, IncompletePackages, PickingZone, NonPickingZone, UMBasExplosion) con su contrato `CanProcess`, su lógica de `Process` y los flags de contexto que mutan.
