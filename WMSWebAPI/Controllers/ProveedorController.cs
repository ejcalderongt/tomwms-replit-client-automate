using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ISyncProveedorService _syncProveedorService;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(IMapper mapper, ISyncProveedorService service, ILogger<ProveedorController> logger)
        {
            _mapper = mapper;
            _syncProveedorService = service;
            _logger = logger;
        }        

        [HttpPost("list/mi3/insert")]
        public async Task<IActionResult> Sincronizar([FromBody] List<ProveedorDto> proveedorDto, [FromServices] IConfiguration configuration)
        {
            if (proveedorDto == null || proveedorDto.Count==0)
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

                            foreach (var dto in proveedorDto)
                            {
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                _syncProveedorService.ProcesarProveedorDto(dto, connection, transaction);
                                resultados.Add(new { dto.Codigo, Procesado = true, Mensaje = "Procesado correctamente" });
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
                            return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
                        }
                    }
                }
            }
        }
        // GET: api/Proveedor/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var proveedores = _syncProveedorService.Get_All();

                if (proveedores == null || proveedores.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron proveedores." });

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = proveedores.Select(p =>
                    {
                        var dict = new Dictionary<string, object?>();
                        foreach (var prop in p.GetType().GetProperties())
                        {
                            if (fieldSet.Contains(prop.Name.ToLower()))
                                dict[prop.Name] = prop.GetValue(p);
                        }
                        return dict;
                    });

                    return Ok(new { Exito = true, Data = data });
                }

                return Ok(new { Exito = true, Data = proveedores });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedores (MI3).");
                return StatusCode(500, new { Exito = false, Mensaje = "Error al obtener proveedores → " + ex.Message });
            }
        }
    }
}
