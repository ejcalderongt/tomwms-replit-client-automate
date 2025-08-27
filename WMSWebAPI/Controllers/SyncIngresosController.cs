using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Services;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/ingresos")]
    public class SyncIngresosController : ControllerBase
    {
        private readonly ISyncIngresosService _service;

        public SyncIngresosController(ISyncIngresosService service)
        {
            _service = service;
        }

        [HttpPost("documento-ingreso")]
        public async Task<IActionResult> PostDocumentoIngreso([FromBody] List<OrdenCompraDto> dto,
                                                              [FromServices] IConfiguration configuration,
                                                              [FromServices] ILogger<SyncIngresosController> logger)
        {
            if (dto == null)
            {
                logger.LogWarning("RecepcionCompletaDto recibido es nulo.");
                return BadRequest("El objeto RecepcionCompletaDto no puede ser nulo.");
            }

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrEmpty(connectionString))
            {
                logger.LogError("Cadena de conexión 'CST' no configurada.");
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

                            logger.LogInformation("Documento de ingreso procesado correctamente.");
                            return Ok(new { Exito = true, Mensaje = "Documento de ingreso procesado correctamente." });
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Error al procesar RecepcionCompletaDto");
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
        }

    }
}