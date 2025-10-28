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
        public async Task<IActionResult> PostDocumentoIngreso([FromBody] List<OrdenCompraDto>  dto)
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
                            _service.ProcesarDocumentosIngreso(dto, connection, transaction);

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
                int result = _service.Insert(_configuration, beINavPedCompraEnc);

                // Convención: 1 = OK, 0 = error (en nuestro flujo, los errores lanzan excepción)
                return Ok(result); // devolverá 1 si todo bien
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
    }
}