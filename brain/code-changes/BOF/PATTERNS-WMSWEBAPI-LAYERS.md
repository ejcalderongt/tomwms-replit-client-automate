---
tipo: other
---
# Patron: Capas en WMSWebAPI (.NET Core)

> Decision arquitectonica canonizada 2026-05-20.
> Origen: handoff inverso `codex-learning-2026-05-20-mi3-di-estatus-endpoint`,
> implementacion del endpoint `GET /api/sync/ingresos/mi3/di-estatus`.

Aplica a **WMSWebAPI** (.NET Core / .NET 8.0) en TOMWMS_BOF.
NO aplica a `WSHHRN` (.asmx, VB.NET legacy) — ese sigue su propio modelo
de capas Entity/DAL/WS.

## Las 4 capas

| Capa | Proyecto | Responsabilidad | NO debe |
|---|---|---|---|
| `DALCore` | `DALCore/` | Queries SQL puntuales y parametrizadas. Devuelve POCOs o DataReaders. | Tener reglas de negocio ni decisiones de filtrado. |
| `EntityCore` | `EntityCore/` | DTOs / POCOs / models serializables. Sin metodos con logica. | Tener logica de negocio. |
| `Services` | `WMSWebAPI/Services/` | Composicion de queries DAL, aplicacion de reglas de negocio, calculo derivado, validaciones de dominio. Devuelve DTOs listos. | Saber de HTTP, status codes ni del wrapper Forma A. |
| `Controller` | `WMSWebAPI/Controllers/` | Routing HTTP, validacion de entrada (modelbinding/atributos), envoltorio Forma A `{data, error}`, status codes. | Tener queries SQL ni reglas de negocio. |

## Flujo de una llamada

```
HTTP request
   ▼
Controller
   - parse query/body
   - validar input
   ▼
Service
   - call DAL (1..N veces)
   - aplicar reglas
   - componer DTO
   ▼
DAL
   - ejecutar query
   - mapear a Entity
   ▼
Entity (POCO)
   ◄ vuelve a Service
Service compone DTO final
   ◄ vuelve a Controller
Controller envuelve en { data, error }
   ▼
HTTP response (200 / 500)
```

## Ejemplo: estatus OC MI3

```csharp
// Controller
[HttpGet("api/sync/ingresos/mi3/di-estatus")]
public IActionResult GetEstatus([FromQuery] string referencia)
{
    if (string.IsNullOrWhiteSpace(referencia))
        return BadRequest(new { data = (object)null,
                                 error = new { code = "BAD_REQUEST",
                                               message = "referencia requerida" }});
    try {
        var data = _ocService.GetEstatusByReferencia(referencia);
        return Ok(new { data, error = (object)null });
    } catch (Exception ex) {
        return StatusCode(500, new { data = (object)null,
                                      error = new { code = "INTERNAL_ERROR",
                                                    message = ex.Message,
                                                    detalle = ex.ToString() }});
    }
}

// Service
public List<EstatusLineaOcDto> GetEstatusByReferencia(string referencia)
{
    var oc = _dal.GetOcByReferencia(referencia)
             ?? throw new NotFoundException($"OC ref {referencia} no existe");
    var lineas = _dal.GetLineasOc(oc.IdOrdenCompraEnc);
    var recepciones = _dal.GetRecepcionesByOc(oc.IdOrdenCompraEnc);
    var tareas = _dal.GetTareasHHByRecepciones(
        recepciones.Select(r => r.IdRecepcionEnc).ToList(),
        idTipoTarea: 1);

    return lineas.Select(l => new EstatusLineaOcDto {
        IdProducto = l.IdProducto,
        CodigoProducto = l.CodigoProducto,
        CantidadSolicitada = l.Cantidad,
        CantidadRecibida = recepciones
            .SelectMany(r => r.Detalles)
            .Where(d => d.IdProducto == l.IdProducto)
            .Sum(d => d.Cantidad),
        Pendiente = /* solicitada - recibida */,
        RecepcionCompleta = /* ... */,
        TareasHH = tareas
            .Where(t => /* asociadas a esta linea */)
            .Select(t => new TareaHHDto {
                IdTareaHH = t.IdTareaHH,
                IdEstado = t.IdEstado,
                EstadoNombre = _catalogos.EstadoTareaHH(t.IdEstado),
                Finalizada = (t.IdEstado == 4)
            }).ToList()
    }).ToList();
}

// DAL
public OrdenCompraEnc GetOcByReferencia(string referencia)
{
    const string sql = @"SELECT IdOrdenCompraEnc, Referencia, ...
                         FROM trans_oc_enc WHERE Referencia = @ref";
    /* ejecutar y mapear */
}
```

## Anti-patrones a evitar

- Controller que abre `SqlConnection` o llama a `Dapper` directo. → mover a DAL.
- Service que retorna `IActionResult` o sabe de HTTP. → service devuelve DTO/throw.
- DAL que aplica reglas (ej. "si estado=4 entonces ..."). → mover a Service.
- Entity con metodos calculados de negocio. → mover el calculo a Service.
- Wrappers Forma A armados en Service. → solo Controller arma `{data, error}`.

## Excepciones (cuando se relajan las capas)

- Endpoints de healthcheck / version: Controller puede devolver directo sin
  Service.
- Endpoints de echo / debug: pueden saltarse DAL.
- Migracion oportunista de legacy: si traes algo de WSHHRN a WMSWebAPI por
  refactor, esta permitido un paso intermedio "controller + service flaco".
  Documentar en el codigo con un TODO.

## Referencias

- Handoff inverso origen:
  `wms-brain/brain/handoffs/codex-learning-2026-05-20-mi3-di-estatus-endpoint/PROPOSAL.md`
- Endpoint de ejemplo: `wms-brain/brain/code-changes/BOF/PATTERNS-OC-MI3.md`
- Catalogo estados HH: `wms-brain/brain/reference/catalogo-tarea-hh-estados.md`

## Reserva de stock: arquitectura Core ya implementada (agregado 2026-05-20)

El motor `WMS.DALCore/Reserva_Stock/` ya implementa el patron pipeline + contexto + logger que se considera "deseable" en discusiones recientes. **No re-inventar.** Estructura:

```
WMS.DALCore/Reserva_Stock/
├── Compatibility/
│   └── StockReservationFacade.cs       ← 3 sobrecargas Reserva_Stock_From_MI3 + Reserva_Stock_Internal
├── Core/
│   ├── Domain/
│   │   └── ReservationContext.cs       ← ~60 propiedades + helpers AddFailure/AddLotFailure/AddZoneFailure
│   ├── Interfaces/
│   │   ├── IPipelineStep.cs
│   │   ├── IReservationHandler.cs
│   │   ├── IReservationLogger.cs
│   │   ├── IReservationPipeline.cs
│   │   ├── IServiceFactory.cs
│   │   └── ReservationResultDto.cs     ← enum ReservationFailureCode (14 valores) + ReservationFailureReason + ReservationResultDto.StatusMessage
│   └── Services/
│       ├── ValidationStep.cs
│       ├── EntityLoadingStep.cs
│       ├── StockQueryStep.cs
│       ├── DateCalculationStep.cs
│       ├── ReservationLoopStep.cs      ← loop principal MAX_ITERATIONS=10 + Clavaud dinamico + clamping + fallbacks Explosion/UMBas
│       ├── PostProcessingStep.cs
│       ├── DecimalQuantityHandler.cs
│       ├── QuantityConverter.cs
│       └── PipelineExecutor.cs
├── Strategies/
│   ├── BaseReservationHandler.cs
│   ├── PickingZoneHandler.cs
│   ├── NonPickingZoneHandler.cs
│   ├── CompletePackagesHandler.cs
│   ├── IncompletePackagesHandler.cs
│   └── UMBasExplosionHandler.cs
├── Infrastructure/
│   ├── ServiceFactory.cs
│   ├── Logging/ReservationLogger.cs
│   └── Legacy/clsLnStock_res_Facade.cs ← compat exacta con firma ByRef VB
```

### Capas (en orden de llamada)

1. **Controller** (cuando aplica desde HH/WebAPI) → invoca el facade.
2. **Compatibility/StockReservationFacade** → valida request, normaliza, construye `ReservationContext`, llama al pipeline.
3. **Core/Services/PipelineExecutor** → ejecuta los 7 steps en secuencia con early-exit si `context.HasError` o `context.IsQuantityFullyReserved()`. `PostProcessingStep` siempre corre.
4. **Core/Domain/ReservationContext** → estado compartido entre steps.
5. **Strategies/*Handler** → invocados desde `ReservationLoopStep` segun `StartingPoint` (3=Picking, 4=NonPicking, 0=cadena completa).
6. **Infrastructure/Logging/ReservationLogger** → buffer de mensajes (Checkpoint/Info/Warning/Error/Reservation).

### Carpetas paralelas a IGNORAR (huerfanas)

```
WMS.StockReservation2/   ← lab antiguo, NO usar
WMS.StockReservation3/   ← lab intermedio, NO usar
reservastockfrommi3/     ← lab inicial, NO usar
```

Antes de borrarlas, verificar referencias con `grep -r "WMS\.StockReservation[23]\|reservastockfrommi3" --include="*.csproj" --include="*.cs"`.

### Cross-ref

- Paridad legacy VB vs Core: `code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md`
- Diagnostico tipificado (taxonomia 10 motivos): `code-changes/BOF/PATTERNS-DIAGNOSTICO-NO-RESERVA-MI3.md`
- UMBAS reserva: `code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`
