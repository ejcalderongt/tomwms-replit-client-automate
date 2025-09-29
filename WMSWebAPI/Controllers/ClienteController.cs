using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Clientes;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Cliente;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IClienteMi3SyncService _syncService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IMapper mapper, IClienteMi3SyncService syncService, ILogger<ClienteController> logger)
        {
            _mapper = mapper;
            _syncService = syncService;
            _logger = logger;
        }

        [HttpPost("list/mi3/insert")]
        public async Task<IActionResult> Sincronizar([FromBody] List<ClienteMi3Dto> ClienteMi3Dto, [FromServices] IConfiguration configuration) 
        {
            if (ClienteMi3Dto == null || ClienteMi3Dto.Count == 0)
            {
                _logger.LogWarning("proveedorDto recibido es nulo o viene vacio.");
                return BadRequest("El objeto proveedorDto no puede ser nulo o vacio.");
            }

            var resultados = new List<object>();
            string? connectionString = configuration.GetConnectionString("CST");
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

                            foreach (var dto in ClienteMi3Dto)
                            {
                               _syncService.ProcesarClienteDesdeDto(dto, connection, transaction);
                                resultados.Add(new { dto.codigo, Procesado = true, Mensaje = "Procesado correctamente" });
                            }

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new { Exito = true, Resultados = resultados });

                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar proveedorDto_mi3");
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
