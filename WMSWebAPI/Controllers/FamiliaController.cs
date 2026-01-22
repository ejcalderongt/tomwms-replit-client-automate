using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Familia;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoFamiliaSyncService _syncService;

        public FamiliaController(IMapper mapper, IProductoFamiliaSyncService syncService)
        {
            _mapper = mapper;
            _syncService = syncService;
        }

        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar([FromBody] List<ProductoFamiliaMi3Dto> FamiliaDto, [FromServices] IConfiguration configuration)
        {
            if (FamiliaDto == null || FamiliaDto.Count == 0)
                return BadRequest("La lista de familias está vacía.");

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
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                _syncService.ProcesarFamiliaDesdeDto(dto, connection, transaction);
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
        //GET: api/Familia/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var familias = _syncService.Get_All();

                if (familias == null || familias.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron familias de producto." });

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = familias.Select(f =>
                    {
                        var dict = new Dictionary<string, object?>();
                        foreach (var p in f.GetType().GetProperties())
                        {
                            if (fieldSet.Contains(p.Name.ToLower()))
                                dict[p.Name] = p.GetValue(f);
                        }
                        return dict;
                    });

                    return Ok(new { Exito = true, Data = data });
                }

                return Ok(new { Exito = true, Data = familias });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = "Error al obtener familias → " + ex.Message });
            }
        }


    }
}
