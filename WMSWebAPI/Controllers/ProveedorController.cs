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

        //#GT26092025: no recuerdo si tiene uso o se agregó sin considerar DMS
        //[HttpPost("list/insert")]
        //public async Task<IActionResult> Sincronizar([FromBody] List<ProveedorDto> proveedorDto, [FromServices] IConfiguration configuration)
        //{
        //    if (proveedorDto == null)
        //    {
        //        _logger.LogWarning("proveedorDto recibido es nulo.");
        //        return BadRequest("El objeto proveedorDto no puede ser nulo.");
        //    }

        //    string? connectionString = _configuration.GetConnectionString("CST");
        //    if (string.IsNullOrEmpty(connectionString))
        //    {
        //        _logger.LogError("Cadena de conexión 'CST' no configurada.");
        //        return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
        //    }

        //    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
        //    {
        //        IsolationLevel = IsolationLevel.ReadCommitted
        //    }, TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (var transaction = connection.BeginTransaction())
        //            {
        //                try
        //                {
                            
        //                    _syncProveedorService.ProcesarProveedorListDto(proveedorDto, connection, transaction);

        //                    transaction.Commit();
        //                    scope.Complete();

        //                    _logger.LogInformation("Lista proveedor procesado correctamente.");
        //                    return Ok(new { Exito = true, Mensaje = "Lista proveedor procesado correctamente." });
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.LogError(ex, "Error al procesar proveedorDto");
        //                    transaction.Rollback();

        //                    var showStackTrace = _configuration.GetValue<bool>("MostrarDetallesErrores");
        //                    return StatusCode(500, new
        //                    {
        //                        Exito = false,
        //                        Mensaje = ex.Message,
        //                        Detalles = showStackTrace ? ex.ToString() : null
        //                    });
        //                }
        //            }
        //        }
        //    }
        //}

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
