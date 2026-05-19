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
