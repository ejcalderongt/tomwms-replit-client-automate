using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Services.Ingresos;
using WMSWebAPI.Services.Proveedor;

namespace WMSWebAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISyncProveedorService _syncProveedorService;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ISyncProveedorService service, ILogger<ProveedorController> logger, IConfiguration configuration)
        {
            _syncProveedorService = service;
            _logger = logger;
            _configuration = configuration;
        }


        [HttpPost("sincronizar")]
        public async Task<IActionResult> Sincronizar([FromBody] List<ProveedorDto> proveedorDto, [FromServices] IConfiguration configuration)
        {
            if (proveedorDto == null)
            {
                _logger.LogWarning("proveedorDto recibido es nulo.");
                return BadRequest("El objeto proveedorDto no puede ser nulo.");
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
                            
                            _syncProveedorService.ProcesarProveedorListDto(proveedorDto, connection, transaction);

                            transaction.Commit();
                            scope.Complete();

                            _logger.LogInformation("Lista proveedor procesado correctamente.");
                            return Ok(new { Exito = true, Mensaje = "Lista proveedor procesado correctamente." });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar proveedorDto");
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

    }
}
