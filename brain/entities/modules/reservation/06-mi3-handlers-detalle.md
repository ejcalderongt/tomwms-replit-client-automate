# 06 · Handlers de reserva en detalle

> **Propósito**: documentar cada uno de los 5 handlers concretos del motor MI3 (Chain of Responsibility) con su contrato `CanProcess`, su lógica de `Process`, los flags del contexto que mutan y los checkpoints que emiten al logger.
>
> **Ubicación en repo**: `data/repos/TOMWMS_BOF/WMS.DALCore/Reserva_Stock/Strategies/`
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md` (visión general), `05-mi3-algoritmo-fefo-clavaud.md` (algoritmo selección), `02-mi3-motor-legacy-vb.md` (anchors legacy), `decisions/003-mi3-reescrito.md`.

---

## Índice

1. Contrato base `IReservationHandler` y `BaseReservationHandler`
2. CASO_1 · `CompletePackagesHandler` (pallets completos no-picking, Clavaud)
3. CASO_2 · `IncompletePackagesHandler` (pallets incompletos no-picking, Clavaud)
4. CASO_3 · `PickingZoneHandler` (zona picking)
5. CASO_4 · `NonPickingZoneHandler` (zonas no-picking general)
6. CASO_EXPLOSION/UMBAS · `UMBasExplosionHandler` (fallback explosión + UMBas)
7. Flags del `ReservationContext` que mutan los handlers
8. Vocabulario de checkpoints emitidos

---

## 1. Contrato base

### 1.1 `IReservationHandler`

```csharp
public interface IReservationHandler
{
    IReservationHandler SetNext(IReservationHandler next);
    void Process(ReservationContext context);
    bool CanProcess(ReservationContext context);
    string CasoId { get; }   // "CASO_1" .. "CASO_EXPLOSION"
}
```

Tres responsabilidades:

- **Encadenar**: `SetNext` retorna `this` para fluent API (`a.SetNext(b).SetNext(c)`).
- **Decidir**: `CanProcess` evalúa precondiciones (existencia de stock, flags, cantidad pendiente).
- **Procesar**: `Process` ejecuta el algoritmo y delega al siguiente si queda pendiente.

### 1.2 `BaseReservationHandler` (abstracta)

Implementa el patrón Template Method:

```csharp
public abstract class BaseReservationHandler : IReservationHandler
{
    private IReservationHandler _next;
    protected readonly IReservationLogger _logger;
    public abstract string CasoId { get; }

    public IReservationHandler SetNext(IReservationHandler next)
    {
        _next = next;
        return next;
    }

    public void Process(ReservationContext context)
    {
        if (CanProcess(context))
        {
            _logger.LogCheckpoint(context, $"#{CasoId}_START");
            DoProcess(context);
            _logger.LogCheckpoint(context, $"#{CasoId}_COMPLETED",
                $"Pendiente: {context.PendingQuantity}");
        }
        else
        {
            _logger.LogCheckpoint(context, $"#{CasoId}_SKIP",
                "CanProcess returned false");
        }

        if (_next != null && context.PendingQuantity > context.Tolerance)
        {
            _logger.LogCheckpoint(context, $"#{CasoId}_PASS_TO_NEXT",
                $"Delegando a {_next.CasoId}");
            _next.Process(context);
        }
    }

    public abstract bool CanProcess(ReservationContext context);
    protected abstract void DoProcess(ReservationContext context);

    // Helpers compartidos
    protected void CreateReservation(clsBeStock stock, double cantidad,
                                     ReservationContext context, string casoOrigen) { ... }
    protected bool IsExpired(clsBeStock stock, ReservationContext context) { ... }
    protected double CalculateAmountToReserve(clsBeStock stock, ReservationContext context) { ... }
}
```

Garantiza que cada handler:

- Loguea `#START`, `#COMPLETED` o `#SKIP` consistentemente.
- Pasa al siguiente solo si hay pendiente (no malgasta CPU).
- Reusa helpers de creación de reservas, detección de vencimiento y cálculo de cantidad.

---

## 2. CASO_1 · `CompletePackagesHandler` (~144 L)

### 2.1 Equivalente legacy

Anchor `INICIAR_EN_1` del legacy (L1273-L1903 en `clsLnStock_res_Partial.vb`).

### 2.2 Contrato

```csharp
public override string CasoId => "CASO_1";

public override bool CanProcess(ReservationContext context)
{
    if (!context.OwnerConfig.Conservar_Zona_Picking_Clavaud) return false;
    if (context.PendingQuantity <= context.Tolerance) return false;
    if (!context.StockListNonPickingZones.Any(s => s.Pallet_Completo)) return false;

    // Solo procesar si la cantidad pendiente justifica al menos un pallet
    var palletSize = context.PalletSize;
    if (context.PendingQuantity < palletSize)
    {
        _logger.LogCheckpoint(context, "#CASO_1_SKIP",
            $"Pendiente {context.PendingQuantity} < pallet {palletSize}");
        return false;
    }

    return true;
}
```

### 2.3 Lógica de `Process`

1. Filtrar `StockListNonPickingZones` por `s.Pallet_Completo == true`.
2. Filtrar por no vencidos: `!IsExpired(s, context)`.
3. Ordenar FEFO + tie-breaker `Lic_plate`.
4. Por cada stock candidato:
   - Si `s.Fecha_vence > context.MinExpirationDateGlobal`: marcar `EstadoProceso 100`, salir del bucle (priorizar fechas más antiguas).
   - Validar `Explosion_Automatica_Nivel_Max` vs `s.IdUbicacion.Nivel`.
   - Calcular `cantidadAReservar = min(s.Cantidad, context.PendingQuantity)`.
   - Crear reserva vía `CreateReservation(s, cantidadAReservar, context, "CASO_1")`.
   - Decrementar `s.Cantidad` (en memoria, persistido en `PostProcessingStep`).
   - Decrementar `context.PendingQuantity`.
   - Si `context.PendingQuantity <= context.Tolerance`: `break`.

### 2.4 Mutaciones al contexto

- `context.PendingQuantity` ← decrementa con cada reserva.
- `context.CreatedReservations` ← agrega cada `clsBeStock_res` creada.
- `context.UsedClavaud = true` (flag para `ReservationResultDto`).

### 2.5 Checkpoints

```
#CASO_1_START
#CASO_1_RESERVED <IdStock> qty=<n>
#CASO_1_FECHA_GT_MIN  (cuando salta por fecha)
#CASO_1_NIVEL_MAX_EXCLUDED  (cuando excluye por nivel)
#CASO_1_COMPLETED  Pendiente: <n>
#CASO_1_SKIP  (si CanProcess = false)
#CASO_1_PASS_TO_NEXT  Delegando a CASO_2
```

---

## 3. CASO_2 · `IncompletePackagesHandler` (~139 L)

### 3.1 Equivalente legacy

Anchor `INICIAR_EN_2` del legacy (L1904-L2711).

### 3.2 Contrato

```csharp
public override string CasoId => "CASO_2";

public override bool CanProcess(ReservationContext context)
{
    if (!context.OwnerConfig.Conservar_Zona_Picking_Clavaud) return false;
    if (context.PendingQuantity <= context.Tolerance) return false;
    if (!context.StockListNonPickingZones.Any(s => !s.Pallet_Completo)) return false;

    // Caso patológico de reabasto sin pallets completos
    if (context.IsTareaReabasto && context.OwnerConfig.considerar_paletizado_en_reabasto)
    {
        _logger.LogCheckpoint(context, "#CASO_2_SKIP",
            "Reabasto exige tarimas completas y no las hay");
        context.AddFailureReason(ReservationFailureCode.NO_STOCK,
            "Reabasto: sin tarimas completas y configuración exige paletizado");
        return false;
    }

    return true;
}
```

### 3.3 Lógica de `Process`

Casi idéntica a CASO_1 pero sobre pallets incompletos:

1. Filtrar `StockListNonPickingZones` por `!s.Pallet_Completo`.
2. Ordenar FEFO + `Lic_plate`.
3. Por cada stock:
   - Validar fecha vs `MinExpirationIncompletePalletsClavaud`.
   - Reservar `min(s.Cantidad, PendingQuantity)`.
   - Decrementar.
4. Si tras agotar pallets incompletos sigue pendiente: pasa al siguiente handler (CASO_3).

### 3.4 Diferencia clave con legacy

El legacy invocaba `XtraMessageBox.Show` (L1929) cuando reabasto + sin tarimas completas + flag activo. **El nuevo emite `FailureCode = NO_STOCK`** con el mismo mensaje, sin UI.

---

## 4. CASO_3 · `PickingZoneHandler` (~137 L)

### 4.1 Equivalente legacy

Region "Reservar stock de zona de picking" (L2752-L3201) dentro de INICIAR_EN_3.

### 4.2 Contrato

```csharp
public override string CasoId => "CASO_3";

public override bool CanProcess(ReservationContext context)
{
    if (context.PendingQuantity <= context.Tolerance) return false;
    if (!context.StockListPickingZone.Any()) return false;

    // Caso patológico: picking vence antes que ALM
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

### 4.3 Lógica de `Process`

1. Filtrar `StockListPickingZone` por `s.Cantidad > 0 && !IsExpired(s, context)`.
2. Ordenar FEFO + `Lic_plate`.
3. Por cada stock:
   - Reservar `min(s.Cantidad, PendingQuantity)`.
   - Si `s.IdPresentacion != PendingPresentacion`: aplicar conversión por factor (UMBas).
   - Crear reserva con flag `EsZonaPicking = true`.
   - Decrementar.

### 4.4 Particularidad: explosión por múltiplo

CASO_3 puede invocar internamente la lógica de "Explosión por múltiplo" cuando `IdPresentacion = 0` y el stock tiene presentación. Conceptualmente: convierte 3 cajas de 12 unidades a 36 UMBas. Lógica delegada a `QuantityConverter` (Core/Services).

---

## 5. CASO_4 · `NonPickingZoneHandler` (~137 L)

### 5.1 Equivalente legacy

Regions "Reserverar/Reservar stock de zona NO Picking" (L3920-L4645 + L4654-L5683 duplicada en legacy). Consolidado en un solo handler.

### 5.2 Contrato

```csharp
public override string CasoId => "CASO_4";

public override bool CanProcess(ReservationContext context)
{
    if (context.PendingQuantity <= context.Tolerance) return false;
    if (!context.StockListNonPickingZones.Any()) return false;

    return true;
}
```

Más permisivo que los otros: si hay stock en zona no-picking y hay pendiente, procesa.

### 5.3 Lógica de `Process`

1. Filtrar `StockListNonPickingZones` por `s.Cantidad > 0 && !IsExpired(s, context)`.
2. Ordenar FEFO + `Lic_plate`.
3. Por cada stock (incluyendo pallets completos e incompletos):
   - Reservar.
   - Decrementar.

### 5.4 Diferencia con CASO_1/CASO_2

CASO_4 **no distingue** pallet completo vs incompleto. Procesa toda la zona no-picking en orden FEFO. Es el "barrido final" cuando los handlers Clavaud ya pasaron o están deshabilitados.

---

## 6. CASO_EXPLOSION/UMBAS · `UMBasExplosionHandler` (~193 L)

### 6.1 Equivalente legacy

- Region "Explosión por múltiplo" (L3133-L3201 + L6204-L6272 duplicada).
- Recursión `Reserva_Stock_From_MI3(... No_bulto = 1965)` (L8059-L8132).

Consolidado en un único handler con dos modos: `ExplosionMode` y `UMBasMode`.

### 6.2 Contrato

```csharp
public override string CasoId => "CASO_EXPLOSION";

public override bool CanProcess(ReservationContext context)
{
    if (context.PendingQuantity <= context.Tolerance) return false;
    if (!context.IsExplosionModeEnabled && !context.IsUMBasModeEnabled) return false;
    if (!context.OwnerConfig.Explosion_Automatica) return false;

    return true;
}
```

Solo procesa cuando uno de los dos modos está activado por `ReservationLoopStep.TryEnableExplosionFallback` o `TryEnableUMBasFallback`.

### 6.3 Lógica de `Process`

#### 6.3.1 Modo Explosión

1. Para cada stock con `IdPresentacion != context.Request.IdPresentacion`:
   - Convertir cantidad a la presentación del stock (ej. 5 cajas → 60 UMBas si factor=12).
   - Reservar.
   - Marcar reserva con `EsExplosion = true`.

#### 6.3.2 Modo UMBas

1. Setear `context.Request.IdPresentacion = 0` (modo UMBas).
2. Recargar listas de stock con `IdPresentacion = 0`.
3. Reservar normalmente, pero marcando reservas con `EsUMBas = true` y guardando `IdPresentacionOriginal` para audit.

#### 6.3.3 Detalle de la unificación

El legacy usaba recursión (`Reserva_Stock_From_MI3` se llamaba a sí misma). El handler nuevo simplemente:

1. Es invocado por `ReservationLoopStep` en una iteración subsecuente del loop while.
2. La cadena se reconstruye por `BuildHandlerChain` con `IsUMBasModeEnabled = true`.
3. La cadena UMBas solo contiene este handler (saltea CASO_1..CASO_4).

### 6.4 Mutaciones al contexto

- `context.UsedExplosion = true` o `context.UsedUMBas = true`.
- `context.Request.IdPresentacion = 0` (en modo UMBas; **única excepción** a la inmutabilidad del request).
- `context.PendingQuantity` decrementa.
- `context.CreatedReservations` agrega con flags `EsExplosion` o `EsUMBas`.

### 6.5 Checkpoints

```
#CASO_EXPLOSION_START mode=Explosion|UMBas
#CASO_EXPLOSION_RESERVED <IdStock> qty=<n> presOriginal=<p1> presReservada=<p2>
#CASO_EXPLOSION_NO_STOCK_IN_PRES
#CASO_EXPLOSION_FALLBACK_TO_UMBAS
#CASO_EXPLOSION_COMPLETED  Pendiente: <n>
```

---

## 7. Flags del `ReservationContext` que mutan los handlers

| Flag                        | Quien lo setea                                    | Efecto                              |
|-----------------------------|---------------------------------------------------|-------------------------------------|
| `PendingQuantity`           | Todos los handlers (decrementan)                  | Define si el siguiente handler corre |
| `CreatedReservations`       | Todos (agregan)                                   | Lista de `clsBeStock_res` a persistir |
| `UsedClavaud`               | CASO_1, CASO_2                                    | `ReservationResultDto.UsedClavaud`  |
| `UsedExplosion`             | CASO_EXPLOSION (modo Explosion)                   | `ReservationResultDto.UsedExplosion`|
| `UsedUMBas`                 | CASO_EXPLOSION (modo UMBas)                       | `ReservationResultDto.UsedUMBas`    |
| `IsExplosionModeEnabled`    | `ReservationLoopStep.TryEnableExplosionFallback` | Permite a CASO_EXPLOSION procesar   |
| `IsUMBasModeEnabled`        | `ReservationLoopStep.TryEnableUMBasFallback`      | Permite a CASO_EXPLOSION en modo UMBas |
| `Request.IdPresentacion = 0`| CASO_EXPLOSION (solo en modo UMBas)               | Cambia búsqueda de stock            |
| `FailureReasons`            | Cualquiera (vía `AddFailureReason`)               | Lista para `ReservationResultDto`   |

---

## 8. Vocabulario de checkpoints emitidos

Convención: `#<TIPO>_<DETALLE>` con prefijo `#` para distinguir checkpoints de logs informacionales.

### 8.1 Por handler (CASO_X)

```
#CASO_X_START
#CASO_X_SKIP <razón>
#CASO_X_RESERVED <IdStock> qty=<n>
#CASO_X_FECHA_GT_MIN
#CASO_X_NIVEL_MAX_EXCLUDED
#CASO_X_PALLET_NOT_COMPLETE  (CASO_1 específico)
#CASO_X_COMPLETED  Pendiente: <n>
#CASO_X_PASS_TO_NEXT  Delegando a <SiguienteCASO>
```

### 8.2 Por step del pipeline

```
#STEP_VALIDATION_START | #STEP_VALIDATION_COMPLETED
#STEP_ENTITY_LOADING_START | #STEP_ENTITY_LOADING_COMPLETED
#STEP_STOCK_QUERY_START | #STEP_STOCK_QUERY_COMPLETED
#STEP_DATE_CALCULATION_START | #STEP_DATE_CALCULATION_COMPLETED
#STEP_RESERVATION_LOOP_START | #STEP_RESERVATION_LOOP_ITERATION_<n> | #STEP_RESERVATION_LOOP_COMPLETED
#STEP_POST_PROCESSING_START | #STEP_POST_PROCESSING_COMPLETED
```

### 8.3 De fallback

```
#FALLBACK_EXPLOSION_ENABLED
#FALLBACK_UMBAS_ENABLED
#FALLBACK_EXHAUSTED  (todos los fallbacks intentados sin éxito)
```

### 8.4 Útiles para grep en `log_error_wms`

Para investigar por qué un pedido específico falló:

```sql
SELECT * FROM log_error_wms
WHERE Mensaje LIKE '%IdTransaccion=12345%'
ORDER BY Fecha;
```

Y luego buscar la cadena de checkpoints `#CASO_*_START`, `#CASO_*_SKIP`, `#CASO_*_PASS_TO_NEXT` para reconstruir la decisión del motor.

---

> Próximo: `07-stock-res-ciclo-vida.md` documenta los estados de la reserva (`UNCOMMITED` → `COMMITED` → `CANCELLED` → `EXPIRED`), las transiciones permitidas y los triggers que las activan.
