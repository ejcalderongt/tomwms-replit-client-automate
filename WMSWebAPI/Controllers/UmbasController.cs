using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Productos;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Umbas;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UmbasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUmbasMi3SyncService _umbasMi3SyncService;
        private readonly ILogger<UmbasController> _logger;

        public UmbasController(IMapper mapper, ILogger<UmbasController> logger, IUmbasMi3SyncService service)
        {
            _mapper = mapper;
            _logger = logger;
            _umbasMi3SyncService = service;
        }

        [HttpPost("list/mi3/insert")]
        public async Task<IActionResult> Sincronizar([FromBody] List<UnidadMedidaMi3Dto> ListUmbasDto, [FromServices] IConfiguration configuration) 
        {
            if (ListUmbasDto == null || ListUmbasDto.Count == 0)
            {
                _logger.LogWarning("UmbasMi3Dto recibido es nulo o viene vacio.");
                return BadRequest("El objeto UmbasMi3Dto no puede ser nulo o vacio.");
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

                            foreach (var dto in ListUmbasDto)
                            {
                                _umbasMi3SyncService.ProcesarUmbasMi3Dto(dto, connection, transaction);
                                resultados.Add(new { dto.Codigo, Procesado = true, Mensaje = "Procesado correctamente" });
                            }

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new { Exito = true, Resultados = resultados });

                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar UmbasMi3Dto");
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
