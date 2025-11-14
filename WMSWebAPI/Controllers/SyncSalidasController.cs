using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Pedido;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Services.Salidas;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/salidas")]
    public class SyncSalidasController : ControllerBase
    {
        private readonly ISyncSalidasService _salidaService;

        public SyncSalidasController(ISyncSalidasService service)
        {
            _salidaService = service;
        }

        [HttpPost("documento-salida")]
        public async Task<IActionResult> PostDocumentoSalida([FromBody] List<SalidaTransDto> dto,
                                                             [FromServices] IConfiguration configuration,
                                                             [FromServices] ILogger<SyncSalidasController> _logger)
        {
            if (dto == null || dto.Count == 0)
            {
                _logger.LogWarning("Lista de SalidaTransDto es nula o vacía.");
                return BadRequest("Debe proporcionar al menos un documento de salida.");
            }

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }, TransactionScopeAsyncFlowOption.Enabled);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var salida in dto)
                {
                    _salidaService.ProcesarSalidaDesdeDto(salida, connection, transaction);
                }

                transaction.Commit();
                scope.Complete();

                _logger.LogInformation("Documento(s) de salida procesado(s) correctamente.");
                return Ok(new { Exito = true, Mensaje = "Documento(s) de salida procesado(s) correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar documento(s) de salida.");
                transaction.Rollback();

                var showStackTrace = configuration.GetValue<bool>("MostrarDetallesErrores");
                return StatusCode(500, new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalles = showStackTrace ? ex.ToString() : null
                });
            }
        }
        [HttpPost("documentos-salida/listar")]
        public IActionResult Listar([FromBody] DocumentoSalidaFiltroDto filtro)
        {
            try
            {
                var documentos = _salidaService.ObtenerDocumentosDeSalida(true,
                                                                        filtro.FechaInicio,
                                                                        filtro.FechaFin,
                                                                        filtro.IdBodega,
                                                                        filtro.IdPropietario
                                                                        );

                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("{IdPedidoEnc}/detalle-pe")]
        public IActionResult GetDetallePedido(int IdPedidoEnc,[FromServices] ILogger<SyncSalidasController> _logger)
        {
            try
            {
                var detallesOS = _salidaService.ObtenerDetallePedido(IdPedidoEnc);
                return Ok(detallesOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetDetalleOS");
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("{IdOrdenSalidaEnc}/despachos")]
        public IActionResult ObtenerDespachosPedido(int IdOrdenSalidaEnc)
        {
            try
            {
                var despachos = _salidaService.ObtenerDespachos(IdOrdenSalidaEnc,null,null);
                return Ok(despachos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
        [HttpPost("mi3/insertar")]
        public async Task<IActionResult> PostMi3Documento([FromBody] clsBeI_nav_ped_traslado_enc documento,
                                                 [FromServices] IConfiguration configuration,
                                                 [FromServices] ILogger<SyncSalidasController> _logger)
        {
            if (documento == null)
            {
                _logger.LogWarning("Documento clsBeI_nav_ped_traslado_enc es nulo.");
                return BadRequest("Debe proporcionar un documento válido.");
            }

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }, TransactionScopeAsyncFlowOption.Enabled);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                string resultado = string.Empty;
                var salidaService = _salidaService as SyncSalidasService; // Asumiendo que ISyncSalidasService implementa IPedidoCliente

                if (salidaService == null)
                {
                    throw new InvalidOperationException("El servicio no implementa la interfaz IPedidoCliente");
                }

                int lineasProcesadas = salidaService.Insert_salida_mi3(ref documento, ref resultado);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("éxito"))
                {
                    throw new Exception(resultado);
                }

                transaction.Commit();
                scope.Complete();

                _logger.LogInformation("Documento MI3 procesado correctamente. Líneas procesadas: {LineasProcesadas}", lineasProcesadas);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Documento MI3 procesado correctamente.",
                    LineasProcesadas = lineasProcesadas,
                    Resultado = resultado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar documento MI3.");
                transaction.Rollback();

                var showStackTrace = configuration.GetValue<bool>("MostrarDetallesErrores");
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