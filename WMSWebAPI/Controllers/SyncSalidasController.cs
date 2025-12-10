using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Services.Salidas;
using WMS.EntityCore.Dtos.Pedido;

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
        public IActionResult GetDetallePedido(int IdPedidoEnc, [FromServices] ILogger<SyncSalidasController> _logger)
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
                var despachos = _salidaService.ObtenerDespachos(IdOrdenSalidaEnc, null, null);
                return Ok(despachos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Inserta un documento de traslado/pedido desde MI3.
        /// ACTUALIZADO: Ahora usa NavPedTrasladoRequestDto para mapear el JSON correctamente
        /// ANTES de llamar a Datos_Validos(), replicando el patrón de integración SAP HANA.
        /// </summary>
        [HttpPost("mi3/insertar")]
        public IActionResult PostMi3Documento([FromBody] NavPedTrasladoRequestDto request,
                                              [FromServices] IConfiguration configuration,
                                              [FromServices] ILogger<SyncSalidasController> _logger)
        {
            // Validar que el request y el documento interno no sean nulos
            if (request == null || request.beINavPedCompraEnc == null)
            {
                _logger.LogWarning("Request o documento clsBeI_nav_ped_traslado_enc es nulo.");
                return BadRequest("Debe proporcionar un documento válido con la estructura { beINavPedCompraEnc: {...} }");
            } 

            // MAPEO EXPLÍCITO: Extraer el documento del wrapper DTO
            // Esto asegura que documento.Lineas_Detalle ya esté poblado desde el JSON
            // ANTES de llamar a Datos_Validos() (replica patrón SAP HANA)
            var documento = request.beINavPedCompraEnc;

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            try
            {
                string resultado = string.Empty;
                var salidaService = _salidaService as SyncSalidasService;

                if (salidaService == null)
                {
                    throw new InvalidOperationException("El servicio no implementa la interfaz requerida");
                }

                // AHORA documento.Lineas_Detalle ya está poblado desde el JSON deserializado                
                int lineasProcesadas = salidaService.Insert_salida_mi3(ref documento, ref resultado);

                // Validar resultado de texto (errores explicitos)
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("exito"))
                {
                    throw new Exception(resultado);
                }

                // CRITICO: Si no se procesaron lineas, es un error (reserva fallo)
                if (lineasProcesadas == 0)
                {
                    string mensajeError = !string.IsNullOrEmpty(resultado)
                        ? resultado
                        : "No se procesaron lineas. Verifique stock disponible y configuracion de bodega.";
                    throw new Exception(mensajeError);
                }

                _logger.LogInformation("Documento MI3 procesado correctamente. Lineas procesadas: {LineasProcesadas}", lineasProcesadas);

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
