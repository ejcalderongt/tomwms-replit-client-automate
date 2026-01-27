using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Catalogos;
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
        public async Task<IActionResult> Sincronizar([FromBody] List<mi3ProveedorDto> proveedorDto, [FromServices] IConfiguration configuration)
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

                                _syncProveedorService.Procesarmi3ProveedorDto(dto, connection, transaction);
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
        [HttpPost("/api/Proveedores/mi3/asociar_bodega")]
        public IActionResult AsociarProveedorABodega(
        [FromBody] ProveedorBodegaRequestDto request,
        [FromServices] IConfiguration configuration)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición es nulo.");

            if (string.IsNullOrWhiteSpace(request.CodigoProveedor))
                return BadRequest("El código de proveedor es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.CodigoBodega))
                return BadRequest("El código de bodega es obligatorio.");

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrEmpty(connectionString))
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction((System.Data.IsolationLevel)IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            var effectiveTx = transaction;
                            var now = DateTime.Now;

                            // 1) Obtener proveedor por código
                            // AJUSTA: método LN real (ej. Get_By_Codigo / Get_Single_By_Codigo / etc.)
                            var proveedorActual = clsLnProveedor.Get_By_Codigo(
                                request.CodigoProveedor,
                                connection,
                                effectiveTx);

                            if (proveedorActual is null || proveedorActual.IdProveedor <= 0)
                            {
                                transaction.Rollback();
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró el proveedor con código: '{request.CodigoProveedor}'."
                                });
                            }

                            // 2) Obtener IdBodega por código
                            int idBodega = clsLnBodega.Get_IdBodega_By_Codigo(
                                request.CodigoBodega,
                                connection,
                                effectiveTx);

                            if (idBodega <= 0)
                            {
                                transaction.Rollback();
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró la bodega con código: '{request.CodigoBodega}'."
                                });
                            }

                            // 3) Config de interface (para User_agr/mod)
                            var navConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(
                                idBodega,
                                connection,
                                effectiveTx);

                            if (navConfigEnc == null)
                            {
                                transaction.Rollback();
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró la configuración de interface para la bodega con código: '{request.CodigoBodega}'."
                                });
                            }

                            // 4) Validar si ya existe relación Proveedor-Bodega
                            // AJUSTA: método LN real
                            bool yaExiste = clsLnProveedor_bodega.ExisteRelacion(
                                proveedorActual.IdProveedor,
                                idBodega,
                                connection,
                                effectiveTx);

                            if (yaExiste)
                            {
                                transaction.Rollback();
                                return Conflict(new
                                {
                                    Exito = false,
                                    Mensaje = "El proveedor ya está asociado a esa bodega."
                                });
                            }

                            // 5) Obtener siguiente IdProveedorBodega (MaxID + 1)
                            int nextIdProveedorBodega = clsLnProveedor_bodega.MaxID(connection, effectiveTx) + 1;

                            // 6) Crear relación proveedor-bodega
                            var proveedorBodega = new WMS.EntityCore.Proveedor.clsBeProveedor_bodega
                            {
                                IdAsignacion = nextIdProveedorBodega,
                                IdProveedor = proveedorActual.IdProveedor,
                                IdBodega = idBodega,
                                Activo = true,                                
                                Fec_agr = now,
                                Fec_mod = now,
                                User_agr = navConfigEnc.IdUsuario.ToString(),
                                User_mod = navConfigEnc.IdUsuario.ToString()
                            };

                            // 7) Insertar relación
                            clsLnProveedor_bodega.Insertar(proveedorBodega, connection, effectiveTx);

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new
                            {
                                Exito = true,
                                Mensaje = "Proveedor asociado a la bodega correctamente.",
                                Data = new
                                {
                                    proveedorBodega.IdAsignacion,
                                    proveedorBodega.IdProveedor,
                                    request.CodigoProveedor,
                                    proveedorBodega.IdBodega,
                                    request.CodigoBodega
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return StatusCode(500, new
                            {
                                Exito = false,
                                Mensaje = "Error al asociar el proveedor a la bodega → " + ex.Message
                            });
                        }
                    }
                }
            }
        }
    }

    // DTO equivalente
    public class ProveedorBodegaRequestDto
    {
        public string CodigoProveedor { get; set; } = string.Empty;
        public string CodigoBodega { get; set; } = string.Empty;
    }
}