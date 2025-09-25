using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Familia;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliaController : ControllerBase
    {

        private readonly MapperConfiguration _config;
        private readonly IProductoFamiliaSyncService _syncService;

        public FamiliaController(MapperConfiguration config, IProductoFamiliaSyncService syncService)
        {
            _config = config;
            _syncService = syncService;
        }

        [HttpPost("sincronizar")]
        public IActionResult Sincronizar([FromBody] List<ProductoFamiliaDto> FamiliaDto, [FromServices] IConfiguration configuration)
        {
            if (FamiliaDto == null || FamiliaDto.Count == 0)
                return BadRequest("La lista de productos está vacía.");

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
                            foreach (var dto in FamiliaDto)
                            {
                                _syncService.ProcesarFamiliaDesdeDto(dto, connection, transaction);
                                resultados.Add(new { dto.IdFamilia, Procesado = true, Mensaje = "Procesado correctamente" });
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
