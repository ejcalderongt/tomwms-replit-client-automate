using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Productos;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Tipo;
using WMSWebAPI.Services.Producto.Umbas;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoTipoMi3SyncService _syncService;
        private readonly ILogger<UmbasController> _logger;

        public TipoProductoController(IMapper mapper, ILogger<UmbasController> logger, IProductoTipoMi3SyncService service)
        {
            _mapper = mapper;
            _logger = logger;
            _syncService = service;
        }


        [HttpPost("list/mi3/insert")]
        public async Task<IActionResult> Sincronizar([FromBody] List<Producto_tipoMi3Dto> ListTipoProductoMi3, [FromServices] IConfiguration configuration)
        {
            if (ListTipoProductoMi3 == null || ListTipoProductoMi3.Count == 0)
            {
                _logger.LogWarning("TipoProductoMi3Dto recibido es nulo o viene vacio.");
                return BadRequest("El objeto TipoProductoMi3Dto no puede ser nulo o vacio.");
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

                            foreach (var dto in ListTipoProductoMi3)
                            {
                                _syncService.ProcesarTipoProductoMi3Dto(dto, connection, transaction);
                                resultados.Add(new { dto.Codigo, Procesado = true, Mensaje = "Procesado correctamente" });
                            }


                            transaction.Commit();
                            scope.Complete();

                            return Ok(new { Exito = true, Resultados = resultados });

                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar TipoProductoMi3Dto");
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
