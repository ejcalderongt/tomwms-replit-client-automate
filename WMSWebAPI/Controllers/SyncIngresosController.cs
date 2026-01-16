using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Services.Ingresos;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/ingresos")]
    public class SyncIngresosController : ControllerBase
    {
        private readonly ISyncIngresosService _service;
        private readonly ILogger<SyncIngresosController> _logger;
        private readonly IConfiguration _configuration;
        public SyncIngresosController(ISyncIngresosService service, ILogger<SyncIngresosController> logger, IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("documento-ingreso")]
        public async Task<IActionResult> PostDocumentoIngreso([FromBody] List<OrdenCompra_3plDto>  dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("RecepcionCompletaDto recibido es nulo.");
                return BadRequest("El objeto RecepcionCompletaDto no puede ser nulo.");
            }

            string? connectionString = _configuration.GetConnectionString("CST");
            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }, TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            _service.ProcesarDocumentosIngreso_3pl(dto, connection, transaction);

                            transaction.Commit();
                            scope.Complete();

                            _logger.LogInformation("Documento de ingreso procesado correctamente.");
                            return Ok(new { Exito = true, Mensaje = "Documento de ingreso procesado correctamente." });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar RecepcionCompletaDto");
                            transaction.Rollback();

                            var showStackTrace = _configuration.GetValue<bool>("MostrarDetallesErrores");
                            return StatusCode(500, new
                            {
                                Exito = false,
                                Mensaje = ex.Message,
                                Detalles = showStackTrace ? ex.ToString() : null
                            });
                        }
                    }
                }
            }
        }

        [HttpPost("documentos-ingreso/listar")]
        public IActionResult Listar([FromBody] DocumentoIngresoFiltroDto filtro)
        {
            try
            {
                var documentos = _service.ObtenerDocumentosDeIngreso(true,
                                                                    filtro.FechaInicio,
                                                                    filtro.FechaFin,
                                                                    filtro.IdBodega,
                                                                    filtro.IdPropietario);

                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("{IdOrdenCompraEnc}/detalle-oc")]
        public IActionResult GetDetalleOC(int IdOrdenCompraEnc)
        {
            try
            {
                var detallesOC = _service.ObtenerDetalleOrdenCompra(IdOrdenCompraEnc);
                return Ok(detallesOC);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetDetalleOC");
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
        [HttpGet("{IdOrdenCompraEnc}/recepciones")]
        public IActionResult ObtenerRecepcionesPorOrden(int IdOrdenCompraEnc)
        {
            try
            {                
                var recepciones = _service.ObtenerDetalleRecepcion(IdOrdenCompraEnc);                
                return Ok(recepciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
        
        [HttpPost("mi3/insert")]
        public IActionResult Insert([FromBody] clsBeI_nav_ped_compra_enc beINavPedCompraEnc)
        {
            if (beINavPedCompraEnc == null)
                return BadRequest("El payload no puede ser nulo.");

            try
            {
                int IdOrdenCompraEnc = _service.Insert(beINavPedCompraEnc);

                return StatusCode(201, new
                {
                    Exito = true,
                    Mensaje = "Orden de compra creada correctamente.",
                    IdOrdenCompraEnc = IdOrdenCompraEnc
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en PedidoCompra/mi3/insert");
                var showStackTrace = _configuration.GetValue<bool>("MostrarDetallesErrores");
                return StatusCode(500, new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalles = showStackTrace ? ex.ToString() : null
                });
            }
        }       

        [HttpGet("mi3/pendientes-procesar")]
        public IActionResult GetIngresosPendientesDeProcesar([FromQuery] string? noPedido = null,[FromQuery] string fields = "full")
        {
            try
            {
                var data = _service.Get_Ingresos_Pendientes_De_Procesar();

                if (!string.IsNullOrWhiteSpace(noPedido))
                    data = data.Where(x => x.No_pedido != null &&
                                           x.No_pedido.Equals(noPedido, StringComparison.OrdinalIgnoreCase))
                               .ToList();

                if (fields.Equals("minimal", StringComparison.OrdinalIgnoreCase))
                {
                    
                    var umIds = data.Select(x => x.Idunidadmedida).Where(id => id > 0).Distinct().ToList();
                    var presIds = data.Select(x => x.Idpresentacion).Where(id => id > 0).Distinct().ToList();

                    var ums = clsLnUnidad_medida.GetByIds(_configuration, umIds);
                    var presList = clsLnProducto_presentacion.GetByIds(_configuration, presIds);

                    var umById = ums.ToDictionary(u => u.IdUnidadMedida, u => u.Codigo);
                    var presCodigoById = presList.ToDictionary(p => p.IdPresentacion, p => p.Codigo ?? "");

                    var result = data.Select(x => new RecepcionSimpleReturnDto
                    {
                        Idtransaccion = x.Idtransaccion,
                        No_pedido = x.No_pedido,
                        Codigo_producto = x.Codigo_producto,
                        Nombre_producto = x.Nombre_producto,
                        UM = umById.TryGetValue(x.Idunidadmedida, out var um) ? um : "",
                        Presentacion = presCodigoById.TryGetValue(x.Idpresentacion, out var codPres) ? codPres : "",
                        Cantidad = x.Cantidad_Pendiente,
                        Lote = x.Lote,
                        Vence = x.Fecha_vence,
                        Linea = int.TryParse(x.No_linea, out var ln) ? ln : 0,
                        Fecha = x.Fec_agr
                    }).ToList();

                    return Ok(result);
                }

                return Ok(data); // full
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }
        [HttpPatch("mi3/pendientes-procesar/marcar-enviadas")]
        public IActionResult MarcarIngresosComoEnviados([FromBody] MarcarTransaccionesEnviadasRequestDto request)
        {
            try
            {
                if (request?.IdTransacciones == null || request.IdTransacciones.Count == 0)
                    return BadRequest(new { ok = false, message = "Debe enviar IdTransacciones (uno o más)." });

                int marcadas = _service.Marcar_Ingresos_Como_Enviados(request.IdTransacciones);
                return Ok(marcadas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }
    }
}