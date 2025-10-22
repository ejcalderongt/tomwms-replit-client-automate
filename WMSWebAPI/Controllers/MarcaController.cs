using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMSWebAPI.Services.Producto.Marca;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMapper configuration;
        private readonly IProductoMarcaSyncService _syncService;

        public MarcaController(IMapper configuration, IProductoMarcaSyncService syncService)
        {
            this.configuration = configuration;
            _syncService = syncService;
        }

        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar([FromBody] List<ProductoMarcaMi3Dto> MarcaDto, [FromServices] IConfiguration configuration) 
        {
            if (MarcaDto == null || MarcaDto.Count == 0)
                return BadRequest("La lista de marcas está vacía.");

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
                            foreach (var dto in MarcaDto)
                            {
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                _syncService.ProcesarMarcaDesdeDto(dto, connection, transaction);
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
        // ✅ GET: api/Marca/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var marcas = _syncService.Get_All();

                if (marcas == null || marcas.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron marcas." });

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = marcas.Select(m =>
                    {
                        var dict = new Dictionary<string, object?>();
                        foreach (var p in m.GetType().GetProperties())
                        {
                            if (fieldSet.Contains(p.Name.ToLower()))
                                dict[p.Name] = p.GetValue(m);
                        }
                        return dict;
                    });

                    return Ok(new { Exito = true, Data = data });
                }

                return Ok(new { Exito = true, Data = marcas });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = "Error al obtener marcas → " + ex.Message });
            }
        }

    }
}
