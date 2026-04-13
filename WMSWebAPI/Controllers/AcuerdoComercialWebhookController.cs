using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Acuerdos;
using WMSWebAPI.Services.AcuerdosComerciales;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/webhooks/[controller]")]
    public class AcuerdoComercialWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAcuerdoComercialService _acuerdoService;
        private readonly ILogger<AcuerdoComercialWebhookController> _logger;

        public AcuerdoComercialWebhookController(
            IConfiguration configuration,
            IAcuerdoComercialService acuerdoService,
            ILogger<AcuerdoComercialWebhookController> logger)
        {
            _configuration = configuration;
            _acuerdoService = acuerdoService;
            _logger = logger;
        }

        [HttpPost("recibir")]
        public async Task<IActionResult> RecibirAcuerdo([FromBody] AcuerdoComercialEncDto request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "El payload del webhook no puede estar vacío" });
            }

            _logger.LogInformation("WebHook recibido - Procesando acuerdo comercial: {@Acuerdo}", request);

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();

                // Usar SqlTransaction explícitamente
                using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                try
                {
                    _acuerdoService.ProcesarAcuerdoComercialDesdeDto(request, connection, transaction);
                    await transaction.CommitAsync();

                    _logger.LogInformation("WebHook procesado exitosamente - Acuerdo ID: {IdAcuerdoEnc}", request.IdAcuerdoEnc);

                    return Ok(new
                    {
                        success = true,
                        message = "Acuerdo comercial procesado exitosamente",
                        idAcuerdoEnc = request.IdAcuerdoEnc,
                        timestamp = DateTime.UtcNow
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al procesar webhook - Acuerdo ID: {IdAcuerdoEnc}", request.IdAcuerdoEnc);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook de acuerdo comercial");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error interno al procesar el acuerdo comercial",
                    details = ex.Message
                });
            }
        }

        [HttpPost("recibir-lote")]
        public async Task<IActionResult> RecibirAcuerdosLote([FromBody] List<AcuerdoComercialEncDto> requests)
        {
            if (requests == null || !requests.Any())
            {
                return BadRequest(new { error = "El payload del webhook no puede estar vacío" });
            }

            _logger.LogInformation("WebHook recibido - Procesando lote de {Cantidad} acuerdos comerciales", requests.Count);

            var resultados = new List<object>();
            var exitosos = 0;
            var fallidos = 0;

            using var connection = new SqlConnection(_configuration.GetConnectionString("CST"));
            await connection.OpenAsync();

            // Usar SqlTransaction explícitamente
            using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                foreach (var request in requests)
                {
                    try
                    {
                        _acuerdoService.ProcesarAcuerdoComercialDesdeDto(request, connection, transaction);
                        exitosos++;
                        resultados.Add(new
                        {
                            idAcuerdoEnc = request.IdAcuerdoEnc,
                            success = true,
                            message = "Procesado exitosamente"
                        });
                    }
                    catch (Exception ex)
                    {
                        fallidos++;
                        resultados.Add(new
                        {
                            idAcuerdoEnc = request.IdAcuerdoEnc,
                            success = false,
                            error = ex.Message
                        });
                        _logger.LogError(ex, "Error procesando acuerdo ID: {IdAcuerdoEnc}", request.IdAcuerdoEnc);
                    }
                }

                await transaction.CommitAsync();

                _logger.LogInformation("WebHook lote procesado - Exitosos: {Exitosos}, Fallidos: {Fallidos}", exitosos, fallidos);

                return Ok(new
                {
                    success = true,
                    total = requests.Count,
                    exitosos = exitosos,
                    fallidos = fallidos,
                    resultados = resultados,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error crítico procesando lote de acuerdos comerciales");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error crítico al procesar el lote de acuerdos comerciales",
                    details = ex.Message
                });
            }
        }

        [HttpPut("actualizar/{idAcuerdoEnc}")]
        public async Task<IActionResult> ActualizarAcuerdo(int idAcuerdoEnc, [FromBody] AcuerdoComercialEncDto request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "El payload del webhook no puede estar vacío" });
            }

            if (idAcuerdoEnc != request.IdAcuerdoEnc)
            {
                return BadRequest(new { error = "El ID de la URL no coincide con el ID del payload" });
            }

            _logger.LogInformation("WebHook actualización recibida - Acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();

                // Usar SqlTransaction explícitamente
                using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                try
                {
                    var existe = _acuerdoService.ExisteAcuerdo(idAcuerdoEnc, connection, transaction);

                    if (!existe)
                    {
                        return NotFound(new
                        {
                            success = false,
                            error = $"No se encontró el acuerdo comercial con ID {idAcuerdoEnc}"
                        });
                    }

                    _acuerdoService.ProcesarAcuerdoComercialDesdeDto(request, connection, transaction);
                    await transaction.CommitAsync();

                    _logger.LogInformation("WebHook actualización exitosa - Acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);

                    return Ok(new
                    {
                        success = true,
                        message = "Acuerdo comercial actualizado exitosamente",
                        idAcuerdoEnc = idAcuerdoEnc,
                        timestamp = DateTime.UtcNow
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al actualizar acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook de actualización");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error interno al actualizar el acuerdo comercial",
                    details = ex.Message
                });
            }
        }

        [HttpDelete("eliminar/{idAcuerdoEnc}")]
        public async Task<IActionResult> EliminarAcuerdo(int idAcuerdoEnc)
        {
            _logger.LogInformation("WebHook eliminación recibida - Acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();

                // Usar SqlTransaction explícitamente
                using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                try
                {
                    var existe = _acuerdoService.ExisteAcuerdo(idAcuerdoEnc, connection, transaction);

                    if (!existe)
                    {
                        return NotFound(new
                        {
                            success = false,
                            error = $"No se encontró el acuerdo comercial con ID {idAcuerdoEnc}"
                        });
                    }

                    _acuerdoService.EliminarAcuerdoComercial(idAcuerdoEnc, connection, transaction);
                    await transaction.CommitAsync();

                    _logger.LogInformation("WebHook eliminación exitosa - Acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);

                    return Ok(new
                    {
                        success = true,
                        message = "Acuerdo comercial eliminado exitosamente",
                        idAcuerdoEnc = idAcuerdoEnc,
                        timestamp = DateTime.UtcNow
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al eliminar acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook de eliminación");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error interno al eliminar el acuerdo comercial",
                    details = ex.Message
                });
            }
        }

        [HttpGet("consultar/{idAcuerdoEnc}")]
        public IActionResult ConsultarAcuerdo(int idAcuerdoEnc)
        {
            _logger.LogInformation("WebHook consulta recibida - Acuerdo ID: {IdAcuerdoEnc}", idAcuerdoEnc);

            try
            {
                var acuerdo = _acuerdoService.Get_By_Id(idAcuerdoEnc);

                if (acuerdo == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = $"No se encontró el acuerdo comercial con ID {idAcuerdoEnc}"
                    });
                }

                var detalles = _acuerdoService.Get_Detalles_By_IdAcuerdoEnc(idAcuerdoEnc);

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        encabezado = acuerdo,
                        detalles = detalles
                    },
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook de consulta");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error interno al consultar el acuerdo comercial",
                    details = ex.Message
                });
            }
        }

        [HttpGet("consultar-por-cliente/{idCliente}")]
        public IActionResult ConsultarAcuerdosPorCliente(int idCliente)
        {
            _logger.LogInformation("WebHook consulta por cliente - Cliente ID: {IdCliente}", idCliente);

            try
            {
                var acuerdos = _acuerdoService.Get_All_By_IdCliente(idCliente);

                return Ok(new
                {
                    success = true,
                    data = acuerdos,
                    count = acuerdos.Count,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook de consulta por cliente");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error interno al consultar acuerdos por cliente",
                    details = ex.Message
                });
            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                status = "healthy",
                service = "AcuerdoComercialWebhook",
                timestamp = DateTime.UtcNow,
                endpoints = new[]
                {
                    "POST /recibir",
                    "POST /recibir-lote",
                    "PUT /actualizar/{id}",
                    "DELETE /eliminar/{id}",
                    "GET /consultar/{id}",
                    "GET /consultar-por-cliente/{idCliente}",
                    "GET /health"
                }
            });
        }
    }
}