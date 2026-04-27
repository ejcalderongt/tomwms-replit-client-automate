# 01 · Reserva de Stock MI3 — Motor Nuevo (.NET 8)

> **Fuente primaria**: `TOMWMS_BOF/WMS.DALCore/Reserva_Stock/` (27 archivos, 3833 líneas).  
> **Endpoint productivo**: `POST /api/sync/salidas/mi3/insertar` (`SyncSalidasController.cs`, L133).  
> **Caller raíz BOF**: `WMS.DALCore/Pedido/clsLnTrans_pe_det.cs` L1294 → `StockReservationFacade.Reserva_Stock_From_MI3(...)`.  
> **Última verificación contra repo**: rama `dev_2028_merge` Azure DevOps `ejcalderon0892/TOMWMS_BOF`, commit indexado en `data/repos/TOMWMS_BOF/`.  
> **Cross-refs**: `02-mi3-motor-legacy-vb.md`, `03-comparison.md`, `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md`.

---

## Índice

1. Punto de entrada (REST → Service → Facade)
2. Arquitectura del motor nuevo (Pipeline + Chain of Responsibility)
3. Contrato del Facade (`StockReservationFacade`)
4. `ReservationContext` — el estado compartido
5. Los 6 pasos del pipeline (Validation → PostProcessing)
6. La cadena dinámica de handlers (5 estrategias + DecimalQuantityHandler)
7. La decisión Clavaud dinámica (`EvaluateClavaudDynamic`)
8. Fallbacks: Explosión y UMBas
9. Modelo de resultado (`ReservationResultDto`, `HandlerResult`, status, failure codes)
10. Persistencia y efectos colaterales (`PostProcessingStep`, tablas Killios tocadas)
11. Logging y trazabilidad (checkpoints, log_error_wms)
12. Invariantes y reglas de oro

---

## 1. Punto de entrada — REST → Service → Facade

### 1.1 Endpoint REST productivo

Archivo: `WMSWebAPI/Controllers/SyncSalidasController.cs`, L133.

```csharp
[HttpPost("mi3/insertar")]
public async Task<IActionResult> InsertarSalidaMI3([FromBody] DocumentoSalidaDto documento)
{
    if (documento == null)
        return BadRequest("Documento requerido.");

    var resultado = await _salidaService.Insert_salida_mi3(documento);
    return Ok(resultado);
}
```

- Ruta completa: `POST /api/sync/salidas/mi3/insertar` (prefijo `[Route("api/sync/salidas")]` declarado a nivel clase).
- DTO: `DocumentoSalidaDto` — encapsula el pedido MI3 entrante desde sistemas externos (Killios u otro origen).
- No hay autenticación visible en este controller (delegada al pipeline ASP.NET / middleware).

### 1.2 Cadena de llamadas BOF (resumen)

```
SyncSalidasController.InsertarSalidaMI3
   └─> _salidaService.Insert_salida_mi3(documento)             // WMS.Service.Salidas
         └─> clsLnTrans_pe_det.Insert_Trans_Pe_Det(...)        // WMS.DALCore/Pedido/clsLnTrans_pe_det.cs L1294
               └─> StockReservationFacade.Reserva_Stock_From_MI3(request)
                     └─> ServiceFactory.CreateReservationPipeline().Execute(context)
```

> **Detalle clave**: `clsLnTrans_pe_det.cs:1294` es la frontera donde el código viejo VB (DAL legacy) entrega control al motor nuevo C#. Antes de esa línea aún se hacen validaciones VB.NET; después, todo el flujo de reservas vive en C# 8.

---

## 2. Arquitectura del motor nuevo

Tres patrones combinados:

| Patrón                       | Implementación                                  | Propósito |
|------------------------------|--------------------------------------------------|-----------|
| **Pipeline secuencial**      | `PipelineExecutor` + 6 `IPipelineStep`           | Romper el método legacy de 8 mil líneas en pasos atómicos |
| **Chain of Responsibility**  | `IReservationHandler` + 5 handlers concretos     | Encadenar las estrategias de reserva (CASO_1 .. CASO_4 + EXPLOSION/UMBAS) |
| **Factory dinámico**         | `ServiceFactory.BuildHandlerChain(startingPoint, ctx, logger)` | Reconstruir la cadena en cada iteración del loop según el `startingPoint` y el modo activo |

Estructura física en disco:

```
WMS.DALCore/Reserva_Stock/
├── Core/
│   ├── Domain/         (ReservationContext, ReservationConfig, ReservationRequest, ReservationResult)
│   ├── Interfaces/     (IPipelineStep, IReservationHandler, IReservationLogger,
│   │                    IReservationPipeline, IServiceFactory, ReservationResultDto)
│   └── Services/       (QuantityConverter, ProductValidator, etc.)
├── Pipeline/
│   ├── PipelineExecutor.cs            (109 L)
│   ├── ValidationStep.cs              (83 L)
│   ├── EntityLoadingStep.cs           (141 L)
│   ├── StockQueryStep.cs              (354 L)
│   ├── DateCalculationStep.cs         (85 L)
│   ├── ReservationLoopStep.cs         (~741 L)
│   └── PostProcessingStep.cs          (121 L)
├── Strategies/
│   ├── BaseReservationHandler.cs      (67 L)
│   ├── CompletePackagesHandler.cs     (144 L)  CASO_1
│   ├── IncompletePackagesHandler.cs   (139 L)  CASO_2
│   ├── PickingZoneHandler.cs          (137 L)  CASO_3
│   ├── NonPickingZoneHandler.cs       (137 L)  CASO_4
│   ├── UMBasExplosionHandler.cs       (193 L)  CASO_EXPLOSION / CASO_UMBAS
│   └── DecimalQuantityHandler.cs      (~71 L)  Helper para granularidad sub-unidad
├── Infrastructure/
│   ├── ServiceFactory.cs              (104 L)  ← DI manual
│   └── ReservationLogger.cs
└── StockReservationFacade.cs          (451 L)  ← API pública
```

> **Por qué no DI container**: `ServiceFactory.cs` declara explícitamente `// NO usa DI container (compatibilidad con VB.NET legacy)`. El motor nuevo se invoca desde código VB.NET que no tiene host ASP.NET, por eso el factory es manual.

---

## 3. Contrato del Facade — `StockReservationFacade`

Archivo: `WMS.DALCore/Reserva_Stock/StockReservationFacade.cs` (451 L). Es la **única** superficie pública que el resto del BOF debe consumir.

### 3.1 Tres sobrecargas públicas

| Método                                | Origen del request                                     | Cuándo se usa |
|---------------------------------------|--------------------------------------------------------|---------------|
| `Reserva_Stock_From_MI3(...)`         | DTO interno construido por `clsLnTrans_pe_det`        | Todo flujo MI3 (endpoint REST + procesos batch) |
| `Reserva_Stock_FromHandheld(...)`     | DTO desde HH Android                                  | Picking en HH (no es el flujo MI3) |
| `Reserva_Stock_Manual(...)`           | DTO armado por operador desde BOF Forms               | Reserva manual desde la UI VB.NET legacy |

Las tres convergen en `Reserva_Stock_Internal(ReservationRequest req, ReservationConfig cfg)`, que crea el `ReservationContext` y dispara `pipeline.Execute(context)`.

### 3.2 Contrato de `ReservationRequest` (campos mínimos)

| Campo               | Tipo     | Origen MI3                                |
|---------------------|----------|--------------------------------------------|
| `IdTransaccion`     | int      | `trans_pe_det.id_transaccion`             |
| `IdPedidoDet`       | int      | `trans_pe_det.id_pedido_det`              |
| `IdProducto`        | int      | resuelto desde `cod_producto` MI3         |
| `IdPropietario`     | int      | resuelto desde `cod_propietario` MI3      |
| `IdBodega`          | int      | resuelto desde header MI3                 |
| `IdPresentacion`    | int      | presentación solicitada (caja, unidad…)   |
| `Cantidad`          | double   | cantidad MI3 en presentación              |
| `Lote`              | string?  | si MI3 vino con lote específico (opcional) |
| `Indicador`         | string   | tipo movimiento (`SALIDA`, `TRASLADO`, …) |

### 3.3 Contrato de `ReservationConfig` (resumen)

Carga desde tabla `propietarios` (Killios, **plural**, 23 cols):

- `Conservar_Zona_Picking_Clavaud` (bool)
- `Permitir_Explosion_Presentacion` (bool)
- `Permitir_UMBas_Fallback` (bool)
- `Tolerancia_Decimal` (double, default `0.000001`)
- `Politica_FEFO` / `Politica_FIFO` (string)
- ... ver `decisions/003-mi3-reescrito.md` para la matriz completa

---

## 4. `ReservationContext` — el estado compartido

Archivo: `Core/Domain/ReservationContext.cs` (265 L).

Es un **único objeto mutable** que viaja por todo el pipeline. Cada step lo lee y lo modifica. Los handlers también.

### 4.1 Campos (organizados por bloque funcional)

```csharp
public class ReservationContext
{
    // === Request original ===
    public ReservationRequest Request { get; set; }
    public ReservationConfig Configuration { get; set; }
    public string MachineName { get; set; }

    // === Cantidades (en presentación solicitada) ===
    public double OriginalRequestedQuantity { get; set; }
    public double PendingQuantity { get; set; }   // ← se descuenta en cada handler
    public double ReservedQuantity => OriginalRequestedQuantity - PendingQuantity;

    // === Entidades cargadas (EntityLoadingStep) ===
    public clsBeProducto Producto { get; set; }
    public clsBePropietario Propietario { get; set; }
    public clsBeBodega Bodega { get; set; }
    public clsBePresentacion DefaultPresentation { get; set; }
    public List<clsBePresentacion> AllPresentations { get; set; }

    // === Stock (StockQueryStep) ===
    public List<clsBeStock> WorkingStockList { get; set; }            // copia mutable
    public List<clsBeStock> StockListPickingZone { get; set; }
    public List<clsBeStock> StockListNonPickingZones { get; set; }

    // === Fechas mínimas (DateCalculationStep — FEFO) ===
    public DateTime? MinExpirationDatePickingZone { get; set; }
    public DateTime? MinExpirationDateNonPickingZones { get; set; }
    public DateTime? MinExpirationCompletePalletsClavaud { get; set; }
    public DateTime? MinExpirationIncompletePalletsClavaud { get; set; }

    // === Modos de fallback (ReservationLoopStep) ===
    public bool IsExplosionModeEnabled { get; set; }
    public bool IsUMBasModeEnabled { get; set; }
    public bool ExplosionModeEnabled { get; set; }   // alias de compatibilidad

    // === Resultado (acumulado por handlers + PostProcessing) ===
    public List<clsBeStock_res> CreatedReservations { get; set; }
    public List<ReservationFailureReason> FailureReasons { get; set; }
    public bool UsedPickingZone { get; set; }
    public bool UsedNonPickingZone { get; set; }
    public bool HasSpecificLot { get; set; }

    // === Helpers ===
    public bool IsQuantityFullyReserved() => PendingQuantity <= 0.000001;
}
```

### 4.2 Por qué es mutable

Decisión de diseño consciente: los handlers necesitan **observar el descuento progresivo** de `PendingQuantity` y de `WorkingStockList[*].Cantidad` para encadenarse. Un context inmutable obligaría a pasar dos resultados por handler (cantidad reservada + nuevo context), aumentando la superficie de error. La regla compensatoria es: **solo el handler activo puede mutar**, y siempre dentro de su `ProcessInternal`.

---

## 5. Los 6 pasos del pipeline

### 5.1 `PipelineExecutor` (109 L)

```csharp
public class PipelineExecutor : IReservationPipeline
{
    private readonly IPipelineStep[] _steps;
    private readonly IReservationLogger _logger;

    public ReservationResultDto Execute(ReservationContext context)
    {
        try
        {
            foreach (var step in _steps)
            {
                _logger.LogCheckpoint($"#STEP_START_{step.GetType().Name}");
                var stepResult = step.Execute(context);
                _logger.LogCheckpoint($"#STEP_END_{step.GetType().Name}");

                if (!stepResult.Success)
                {
                    _logger.LogError($"Pipeline aborted at {step.GetType().Name}: {stepResult.Message}");
                    return ReservationResultDto.FromContext(context, context.FailureReasons);
                }
            }
            return ReservationResultDto.FromContext(context);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Pipeline execution failed");
            // ...registro en log_error_wms y devolución de Failed...
        }
    }
}
```

Ejecución estrictamente secuencial. Si cualquier step retorna `Success=false`, se aborta y se devuelve un `ReservationResultDto.Failed` con las `FailureReasons` acumuladas.

### 5.2 Step 1: `ValidationStep` (83 L)

Valida lo siguiente:

- `Request != null` y `Cantidad > 0` → si no, `INVALID_QUANTITY`.
- `IdProducto > 0`, `IdBodega > 0`, `IdPropietario > 0`.
- `Configuration != null` (ya cargado en Facade).
- `MachineName` resuelto (fallback a `Environment.MachineName`).

No toca BD. No carga entidades. Solo precondiciones.

### 5.3 Step 2: `EntityLoadingStep` (141 L)

Carga las cuatro entidades clave desde Killios:

| Entidad             | Tabla Killios            | Método DAL                                |
|---------------------|--------------------------|-------------------------------------------|
| `Producto`          | `productos`              | `clsLnProducto.Get(IdProducto)`           |
| `Propietario`       | `propietarios` (PLURAL)  | `clsLnPropietario.Get(IdPropietario)`     |
| `Bodega`            | `bodegas`                | `clsLnBodega.Get(IdBodega)`               |
| `Presentaciones`    | `producto_presentacion`  | `clsLnPresentacion.GetByProducto(idProd)` |

Punto crítico: el método `ConvertQuantityToUnits(...)` se aplica acá si el request viene en presentación distinta a la **default** del producto. La conversión usa el `Factor` de `clsBePresentacion`.

### 5.4 Step 3: `StockQueryStep` (354 L)

El step más grande del pipeline. Hace tres cosas:

1. **Consulta el stock disponible**:  
   `clsLnStock.GetStockDisponiblePorProductoPropietarioBodega(idProd, idProp, idBod, idEstado)`.  
   Tabla origen: `stock` (33 cols). Filtra `cantidad > 0`, sin reservas pendientes en `stock_res`.

2. **Aplica filtros de exclusión**:
   - **Stock vencido** (`Fecha_vence < DateTime.Now`) → si TODO el stock está vencido, `FailureReason = ALL_STOCK_EXPIRED` y aborta.
   - **Stock con `Fecha_manufactura` inválida** (NULL en producto que la requiere) → `MANUFACTURING_DATE_INVALID`.
   - **Estado de producto** (calidad) → si el pedido exige un `IdProductoEstado` específico y no hay stock con ese estado, `PRODUCT_STATE_REQUIRED_NO_STOCK`.

3. **Particiona** la lista en `StockListPickingZone` y `StockListNonPickingZones` según `clsBeStock.UbicacionPicking`.

Si tras filtros queda vacío, llama `DetectStockFailures(context)` que clasifica el motivo (lote no encontrado, estado faltante, ubicación restringida, condición de almacenamiento, etc.) y arma la `FailureReason` específica.

### 5.5 Step 4: `DateCalculationStep` (85 L)

Calcula las **fechas mínimas FEFO** para que cada handler sepa qué stock priorizar:

```csharp
context.MinExpirationDatePickingZone =
    context.StockListPickingZone?
        .Where(s => s.Cantidad > 0)
        .Min(s => s.Fecha_vence);

context.MinExpirationDateNonPickingZones =
    context.StockListNonPickingZones?
        .Where(s => s.Cantidad > 0)
        .Min(s => s.Fecha_vence);

context.MinExpirationCompletePalletsClavaud =
    context.StockListNonPickingZones?
        .Where(s => s.Pallet_Completo && s.Cantidad > 0)
        .Min(s => s.Fecha_vence);

context.MinExpirationIncompletePalletsClavaud =
    context.StockListNonPickingZones?
        .Where(s => !s.Pallet_Completo && s.Cantidad > 0)
        .Min(s => s.Fecha_vence);
```

Este step es el cerebro del **FEFO** (First Expired, First Out). Cada handler luego filtra `s.Fecha_vence == context.MinExpiration*` para reservar **solo** del lote más viejo.

### 5.6 Step 5: `ReservationLoopStep` (~741 L)

Es el corazón. Bucle externo `while (PendingQuantity > 0)` con varios mecanismos:

1. **`DetermineStartingPoint(context)`** — decide en qué CASO arrancar (1, 2, 3, 4) según:
   - Si `Conservar_Zona_Picking_Clavaud == true` → arranca en CASO_1 (pallets completos no-picking).
   - Si solo hay stock en picking → arranca en CASO_3.
   - Si hay request explícito de no-picking → arranca en CASO_4.

2. **`EvaluateClavaudDynamic(context)`** — re-evalúa Clavaud en cada iteración (ver §7).

3. **Construcción dinámica de cadena**:
   ```csharp
   var chain = _factory.BuildHandlerChain(startingPoint, context, _logger);
   var result = chain.Handle(context);
   ```

4. **Fallbacks tras intento fallido**:
   - `TryEnableExplosionFallback(context)` — habilita modo explosión si quedó pendiente y hay stock en otra presentación.
   - `TryEnableUMBasFallback(context)` — habilita modo UMBas si quedó pendiente y hay stock en UMBas.

5. **Salida del loop**:
   - Si `IsQuantityFullyReserved()` → break con éxito.
   - Si tras explosión + UMBas sigue pendiente → break con `Partial` o `Failed`.

### 5.7 Step 6: `PostProcessingStep` (121 L)

Una vez que el loop terminó:

1. **Inserta** las reservas acumuladas en `context.CreatedReservations` a la tabla `stock_res` (Killios, 35 cols).
2. **Actualiza** `trans_pe_det` con la cantidad reservada y el indicador de estado.
3. Si el indicador es `TRASLADO`, **actualiza** también `i_nav_ped_traslado_det` (22 cols).
4. **Inserta log de reserva** en `trans_pe_det_log_reserva` (17 cols) con un snapshot de cada reserva creada.
5. Si hubo `FailureReasons`, **registra** el detalle en `log_error_wms` (15 cols).

Toda la persistencia ocurre dentro de una **única transacción SQL** (TransactionScope). Si algo falla acá, se hace rollback completo y no queda nada en `stock_res`.

---

## 6. La cadena dinámica de handlers

### 6.1 Interfaz `IReservationHandler`

```csharp
public interface IReservationHandler
{
    IReservationHandler SetNext(IReservationHandler next);
    HandlerResult Handle(ReservationContext context);
}

public class HandlerResult
{
    public bool Success { get; set; }
    public double ReservedQuantity { get; set; }
    public List<clsBeStock_res> Reservations { get; set; } = new();
    public string CaseCode { get; set; } = "";
    public string Message { get; set; } = "";
}
```

### 6.2 `BaseReservationHandler` (67 L)

Implementa el patrón Chain of Responsibility con paso al siguiente:

```csharp
public HandlerResult Handle(ReservationContext context)
{
    var result = ProcessInternal(context);

    if (context.IsQuantityFullyReserved())
    {
        _logger.LogCheckpoint($"#{GetType().Name}_COMPLETED");
        return result;
    }

    if (_nextHandler != null && context.PendingQuantity > 0.000001)
    {
        _logger.LogCheckpoint($"#{GetType().Name}_PASS_TO_NEXT");
        var nextResult = _nextHandler.Handle(context);
        result.ReservedQuantity += nextResult.ReservedQuantity;
        result.Reservations.AddRange(nextResult.Reservations);
        result.CaseCode += "+" + nextResult.CaseCode;
    }
    return result;
}
```

Cada handler concreto implementa `CanProcess` y `ProcessInternal`.

### 6.3 Los 5 handlers concretos

| Handler                       | CaseCode          | Filtro principal                                                                  |
|-------------------------------|-------------------|------------------------------------------------------------------------------------|
| `CompletePackagesHandler`     | `CASO_1`          | `Pallet_Completo && !UbicacionPicking && UbicacionNivel > 0` + `Fecha_vence == MinExpirationCompletePalletsClavaud` |
| `IncompletePackagesHandler`   | `CASO_2`          | `!Pallet_Completo && !UbicacionPicking && UbicacionNivel > 0` + `Fecha_vence == MinExpirationIncompletePalletsClavaud` |
| `PickingZoneHandler`          | `CASO_3`          | `s.UbicacionPicking == true` + `Fecha_vence == MinExpirationDatePickingZone` |
| `NonPickingZoneHandler`       | `CASO_4`          | `s.UbicacionPicking == false` + `Fecha_vence == MinExpirationDateNonPickingZones` |
| `UMBasExplosionHandler`       | `CASO_EXPLOSION` o `CASO_UMBAS` | activo solo si `IsExplosionModeEnabled` o `IsUMBasModeEnabled` |

Todos ordenan `OrderBy(s => s.Fecha_vence).ThenBy(s => s.Lic_plate)` (FEFO + license plate).

### 6.4 `ServiceFactory.BuildHandlerChain` — la decisión de orden

```csharp
public IReservationHandler BuildHandlerChain(int startingPoint, ReservationContext ctx, IReservationLogger logger)
{
    var handlers = new List<IReservationHandler>();

    if (ctx.IsExplosionModeEnabled || ctx.IsUMBasModeEnabled)
    {
        handlers.Add(new UMBasExplosionHandler(logger));
    }
    else
    {
        switch (startingPoint)
        {
            case 1: // pallets completos
                if (ctx.Configuration.Conservar_Zona_Picking_Clavaud)
                {
                    handlers.Add(new CompletePackagesHandler(logger));
                    handlers.Add(new IncompletePackagesHandler(logger));
                }
                handlers.Add(new PickingZoneHandler(logger));
                handlers.Add(new NonPickingZoneHandler(logger));
                break;
            case 2: // pallets incompletos
                if (ctx.Configuration.Conservar_Zona_Picking_Clavaud)
                    handlers.Add(new IncompletePackagesHandler(logger));
                handlers.Add(new PickingZoneHandler(logger));
                handlers.Add(new NonPickingZoneHandler(logger));
                break;
            case 3: // picking zone
                handlers.Add(new PickingZoneHandler(logger));
                handlers.Add(new NonPickingZoneHandler(logger));
                break;
            case 4: // non-picking zones
                handlers.Add(new NonPickingZoneHandler(logger));
                break;
            default: // 0 o desconocido: cadena completa
                if (ctx.Configuration.Conservar_Zona_Picking_Clavaud)
                {
                    handlers.Add(new CompletePackagesHandler(logger));
                    handlers.Add(new IncompletePackagesHandler(logger));
                }
                handlers.Add(new PickingZoneHandler(logger));
                handlers.Add(new NonPickingZoneHandler(logger));
                break;
        }
    }

    if (handlers.Count == 0)
        throw new InvalidOperationException($"No handlers configurados para startingPoint={startingPoint}");

    for (int i = 0; i < handlers.Count - 1; i++)
        handlers[i].SetNext(handlers[i + 1]);

    return handlers[0];
}
```

> **Re-construcción por iteración**: `ReservationLoopStep` llama a `BuildHandlerChain` **en cada iteración del loop**. No hay handlers cacheados. Esto permite cambiar `startingPoint` y modos (Explosión/UMBas) sin estado residual.

### 6.5 `DecimalQuantityHandler` (~71 L)

Helper que se usa **dentro** de los handlers principales cuando hay cantidades fraccionarias (ej. 1.5 cajas). Aplica redondeo al `Factor` y reparte el remanente con el `QuantityConverter`.

---

## 7. La decisión Clavaud dinámica — `EvaluateClavaudDynamic`

`Conservar_Zona_Picking_Clavaud` no es solo un flag de configuración: el motor lo **re-evalúa por iteración** del loop dentro de `ReservationLoopStep`.

Lógica resumida:

1. **Si el flag está apagado**: nunca se invoca `CompletePackagesHandler` ni `IncompletePackagesHandler`. Se va directo a CASO_3/CASO_4.
2. **Si está prendido**:
   - Antes de cada iteración, se cuenta la cantidad disponible en zonas no-picking (pallets completos + incompletos).
   - Si la cantidad pendiente del request **es menor** que un pallet completo, se considera que vaciar ese pallet desde no-picking "rompe" la conservación de la zona picking, por lo que el motor **degrada** al CASO_3 (servir desde picking) en esa iteración, aún teniendo Clavaud habilitado.
   - Si la cantidad pendiente **es mayor o igual** que un pallet completo, se prefiere CASO_1 (vaciar desde no-picking) y así no consumir picking.

> Esto es una diferencia **conceptual** importante con el legacy, donde Clavaud era un flag estático evaluado una sola vez al entrar al método. Ver §6 de `02-mi3-motor-legacy-vb.md` y la matriz de comparación en `03-comparison.md`.

---

## 8. Fallbacks: Explosión y UMBas

### 8.1 Explosión (`IsExplosionModeEnabled`)

Cuando se agotaron CASO_1..CASO_4 y aún hay pendiente, `ReservationLoopStep.TryEnableExplosionFallback`:

1. Verifica `Configuration.Permitir_Explosion_Presentacion == true`.
2. Verifica que exista stock en otra presentación del mismo producto.
3. Activa `context.IsExplosionModeEnabled = true`.
4. Vuelve a `BuildHandlerChain` que esta vez devuelve **solo** `UMBasExplosionHandler`.

Lógica de explosión (extracto de `UMBasExplosionHandler.ProcessExplosion`):

```csharp
double pendingInUMBas = _converter.ConvertToUMBas(
    context.PendingQuantity, presentationFactor);

foreach (var stock in orderedStock)  // FEFO
{
    if (pendingInUMBas <= 0.000001) break;
    double quantityToReserve = Math.Min(stock.Cantidad, pendingInUMBas);
    var reservation = CreateReservation(context, stock, quantityToReserve, isUMBas: true);
    stock.Cantidad -= quantityToReserve;
    pendingInUMBas -= quantityToReserve;
    result.ReservedQuantity += quantityToReserve;
    result.Reservations.Add(reservation);
}
```

> Las reservas creadas en explosión tienen `IdPresentacion = 0` (UMBas no tiene presentación). Esto es la convención de marcado para que `PostProcessingStep` y los reportes BOF las traten distinto.

### 8.2 UMBas fallback

Análogo, pero arranca **desde** UMBas (no convierte). Activado si el producto tiene stock genérico en UMBas y el pedido lo permite.

### 8.3 Orden de fallback

```
[CASO_1 → CASO_2 → CASO_3 → CASO_4]
       ↓ (si pendiente > 0)
[CASO_EXPLOSION]
       ↓ (si pendiente > 0)
[CASO_UMBAS]
       ↓ (si pendiente > 0)
RESULTADO = Partial o Failed
```

---

## 9. Modelo de resultado

### 9.1 `ReservationStatus`

```csharp
public enum ReservationStatus { Success = 0, Partial = 1, Failed = 2 }
```

### 9.2 `ReservationFailureCode` (14 valores)

```csharp
NONE                              = 0
NO_STOCK                          = 1
LOT_NOT_FOUND                     = 2
LOCATION_RESTRICTED_NO_STOCK      = 3
PRODUCT_STATE_REQUIRED_NO_STOCK   = 4
PICKING_ZONE_REQUIRED_NO_STOCK    = 5
NON_PICKING_ZONE_REQUIRED_NO_STOCK = 6
RECEPTION_LOCATION_NOT_ALLOWED    = 7
ALL_STOCK_EXPIRED                 = 8
ZONE_PRIORITY_CONFLICT            = 9
PRODUCT_NOT_FOUND                 = 10
INVALID_QUANTITY                  = 11
STORAGE_CONDITION_MISMATCH        = 12
MANUFACTURING_DATE_INVALID        = 13
```

Cada uno se construye con `ReservationFailureReason.Create(code, message, qty?)`. El motor emite **una o más** razones (no es excluyente).

### 9.3 `ReservationResultDto`

Producido por `ReservationResultDto.FromContext(context)`. Contiene:

- `Status` (Success | Partial | Failed)
- `RequestedQuantity`, `ReservedQuantity`, `PendingQuantity`, `ReservationCount`
- `Reservations: List<clsBeStock_res>` (las reservas creadas)
- `FailureReasons: List<ReservationFailureReason>`
- Flags: `UsedPickingZone`, `UsedNonPickingZone`, `UsedSpecificLot`, `UsedExplosion`
- Helper: `StatusMessage`, `GetPrimaryFailureMessage()`, `GetAllFailureMessages()`
- `ToLegacyResult()` → adapta a `ReservationResult` (forma vieja para callers VB.NET no migrados)

> El método `FromContext` impone la lógica de status: si `reserved >= requested - 0.000001` → Success; si `reserved > 0.000001` → Partial; sino Failed. Esto es **único** en el motor nuevo (el legacy no tiene tipado de status).

---

## 10. Persistencia y efectos colaterales

`PostProcessingStep` toca exclusivamente estas tablas Killios (todas validadas con conexión `52.41.114.122,1437` en passada 3-2):

| Tabla                          | Operación   | Cols | Notas |
|--------------------------------|-------------|------|-------|
| `stock_res`                    | INSERT      | 35   | Una fila por cada `clsBeStock_res` en `context.CreatedReservations` |
| `trans_pe_det`                 | UPDATE      | 44   | Actualiza `cantidad_reservada` y `estado_reserva` |
| `i_nav_ped_traslado_det`       | UPDATE      | 22   | Solo si `Indicador == "TRASLADO"` |
| `trans_pe_det_log_reserva`     | INSERT      | 17   | Snapshot histórico de cada reserva |
| `log_error_wms`                | INSERT      | 15   | Solo si hay `FailureReasons` o excepciones |
| `propietario_bodega`           | SELECT      | 8    | Solo lectura, para validar que el propietario opera la bodega |
| `stock`                        | SELECT      | 33   | Lectura inicial en `StockQueryStep` |
| `propietarios` (PLURAL)        | SELECT      | 23   | Carga de `Configuration` |

> **Esquema de `stock_res`** (35 cols) y mapeos campo-por-campo: ver `sql-catalog/reservation-tables.md`. La fila se crea con `Estado = "UNCOMMITED"`, y un proceso posterior la promueve a `COMMITED` cuando el HH cierra el picking.

---

## 11. Logging y trazabilidad

### 11.1 `IReservationLogger`

Cinco operaciones:

```csharp
void LogInfo(string message);
void LogCheckpoint(string code);     // ej. "#CASO_1_START", "#STEP_END_StockQueryStep"
void LogReservation(clsBeStock_res reserva, string caseCode, string message);
void LogError(string message);
void LogException(Exception ex, string context);
```

### 11.2 Vocabulario de checkpoints (críticos para debugging)

| Prefijo                         | Significado |
|---------------------------------|-------------|
| `#STEP_START_*` / `#STEP_END_*` | Entrada/salida de cada paso del pipeline |
| `#CASO_1_START` .. `#CASO_4_END` | Procesamiento de los 4 handlers principales |
| `#CASO_EXPLOSION_START`         | Activación del fallback de explosión |
| `#CASO_UMBAS_START`             | Activación del fallback UMBas |
| `#*_PASS_TO_NEXT`               | Handler agotado, pasa al siguiente en cadena |
| `#*_COMPLETED`                  | Handler completó la reserva |
| `#*_SKIP`                       | Handler decidió no procesar (sin stock o config off) |

Estos checkpoints van a `log_error_wms` con `tipo = "CHECKPOINT"` y permiten reconstruir completamente la decisión del motor en cualquier transacción.

---

## 12. Invariantes y reglas de oro

1. **Una sola superficie pública**: `StockReservationFacade`. Todo el resto del namespace es interno.
2. **Context inmutable en estructura, mutable en valores**: nadie reasigna `context = new ReservationContext()` después de creado; solo se mutan campos.
3. **Tolerancia decimal universal**: `0.000001`. Siempre se compara `<= 0.000001` para considerar "cero". Hardcoded por consistencia con el legacy VB.NET.
4. **FEFO obligatorio**: todos los handlers principales filtran `s.Fecha_vence == context.MinExpiration*`. Sin excepciones documentadas.
5. **Sort estable**: `OrderBy(Fecha_vence).ThenBy(Lic_plate)`. Garantiza orden determinístico ante empates de fecha.
6. **Una sola transacción SQL para `PostProcessingStep`**: si una de las 5 operaciones falla, rollback completo. No queda persistido nada parcial.
7. **`Estado = "UNCOMMITED"`** en toda reserva nueva. La promoción a `COMMITED` es responsabilidad de un proceso posterior (HH picking close).
8. **`MachineName` siempre presente**: si el caller no lo pasa, fallback a `Environment.MachineName`. Va a `stock_res.host` para auditoría.
9. **Nunca se reserva stock vencido**: `StockQueryStep` lo filtra antes del loop. No hay manera de bypassear.
10. **`IdPresentacion = 0`** en reservas de UMBas/explosión, marca semántica.

---

## Glosario rápido

- **Clavaud**: política de "conservar zona picking", priorizando vaciar pallets completos no-picking antes de tocar picking.
- **FEFO**: First Expired, First Out. Siempre el más viejo primero.
- **UMBas**: Unidad de Medida Base. La unidad atómica del producto (ej. unidad suelta).
- **Explosión**: descomposición de una presentación grande (ej. caja) en su equivalente UMBas.
- **`UbicacionPicking`**: ubicación designada para picking manual de baja altura.
- **`Pallet_Completo`**: pallet armado con la cantidad estándar del SKU; bandera operativa.
- **`Lic_plate`**: license plate, código único físico de un pallet/contenedor.
- **`Indicador`**: tipo de movimiento WMS (`SALIDA`, `TRASLADO`, `RECEPCION`, etc.).

---

> Próximo: `02-mi3-motor-legacy-vb.md` documenta el método monolítico VB.NET de 8 mil líneas que este motor reemplaza, con los anchors L18192-L26337 del archivo `clsLnStock_res_Partial.vb`.
