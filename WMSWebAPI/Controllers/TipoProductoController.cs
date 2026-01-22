using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Productos;
using WMSWebAPI.Services.Producto.Tipo;

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
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

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
                            return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
                        }
                    }
                }
            }

        }
        // ✅ GET: api/TipoProducto/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var tipos = _syncService.Get_All(); // Asume existencia en la capa Ln/Service

                if (tipos == null || tipos.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron tipos de producto." });

                // Proyección opcional: ?fields=Codigo,Descripcion
                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = tipos.Select(t =>
                    {
                        var dict = new Dictionary<string, object?>();
                        foreach (var p in t.GetType().GetProperties())
                        {
                            if (fieldSet.Contains(p.Name.ToLower()))
                                dict[p.Name] = p.GetValue(t);
                        }
                        return dict;
                    });

                    return Ok(new { Exito = true, Data = data });
                }

                return Ok(new { Exito = true, Data = tipos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipos de producto (MI3).");
                return StatusCode(500, new { Exito = false, Mensaje = "Ocurrió un error al obtener los tipos de producto." });
            }
        }


    }
}
