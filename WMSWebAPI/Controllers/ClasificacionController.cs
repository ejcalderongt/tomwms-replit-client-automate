using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Clasificacion;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoClasificacionSyncService _syncService;

        public ClasificacionController(IMapper mapper, IProductoClasificacionSyncService syncService)
        {
            _mapper = mapper;
            _syncService = syncService;
        }


        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar([FromBody] List<ProductoClasificacionSimpleDto> Clasificaciondto, [FromServices] IConfiguration configuration) 
        {
            if (Clasificaciondto == null || Clasificaciondto.Count == 0)
                return BadRequest("La lista de clasificación está vacía.");

            var resultados = new List<object>();
            string? connectionString = configuration.GetConnectionString("CST");

            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }, TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var dto in Clasificaciondto)
                            {
                                _syncService.ProcesarClasificacionDesdeDto(dto, connection, transaction);
                                resultados.Add(new { dto.Codigo, Procesado = true, Mensaje = "Procesado correctamente" });
                            }

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new { Exito = true, Resultados = resultados });
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
                        }
                    }
                }
            }


        }
    }
}
