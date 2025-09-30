using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Presentacion;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentacionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPresentacionMi3SyncService _presentacionMi3SyncService;

        public PresentacionController(IMapper mapper, IPresentacionMi3SyncService presentacionMi3SyncService)
        {
            _mapper = mapper;
            _presentacionMi3SyncService = presentacionMi3SyncService;
        }

        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar([FromBody] List<ProductoPresentacionMi3Dto> listPresentacionMi3dto, [FromServices] IConfiguration configuration)
        {
            if (listPresentacionMi3dto == null || listPresentacionMi3dto.Count == 0)
                return BadRequest("La lista de presentación está vacía.");

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
                            foreach (var dto in listPresentacionMi3dto)
                            {
                                _presentacionMi3SyncService.ProcesarPresentacionMi3Dto(dto, connection, transaction);
                                resultados.Add(new { dto.Codigo_presentacion, Procesado = true, Mensaje = "Procesado correctamente" });
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
