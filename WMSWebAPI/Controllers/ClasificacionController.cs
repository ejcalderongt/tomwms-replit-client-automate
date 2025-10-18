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
        public IActionResult Sincronizar([FromBody] List<ProductoClasificacionMi3Dto> Clasificaciondto, [FromServices] IConfiguration configuration) 
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
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

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
        // GET: api/Clasificacion/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var clasificaciones = _syncService.Get_All();

                if (clasificaciones == null || clasificaciones.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron clasificaciones de producto." });

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = clasificaciones.Select(c =>
                    {
                        var dict = new Dictionary<string, object?>();
                        foreach (var p in c.GetType().GetProperties())
                        {
                            if (fieldSet.Contains(p.Name.ToLower()))
                                dict[p.Name] = p.GetValue(c);
                        }
                        return dict;
                    });

                    return Ok(new { Exito = true, Data = data });
                }

                return Ok(new { Exito = true, Data = clasificaciones });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = "Error al obtener las clasificaciones → " + ex.Message });
            }
        }

    }
}
