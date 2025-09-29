using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Services;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoSimpleSyncService _mhsSyncService;

        public ProductosController(IMapper mapper, IProductoSimpleSyncService mhsSyncService)
        {
            _mapper = mapper;
            _mhsSyncService = mhsSyncService;
        }

        [HttpPost("list/insert")]
        public IActionResult Sincronizar([FromBody] List<ProductoTransDto> productosDto, [FromServices] IConfiguration configuration)
        {
            if (productosDto == null || productosDto.Count == 0)
                return BadRequest("La lista de productos está vacía.");

            var resultados = new List<object>();
            string? connectionString = configuration.GetConnectionString("CST");

            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            var syncService = new ProductoSyncService(configuration, _mapper);

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
                            foreach (var dto in productosDto)
                            {
                                syncService.ProcesarProductoDesdeDto(dto, connection, transaction);
                                resultados.Add(new { dto.IdProducto, Procesado = true, Mensaje = "Procesado correctamente" });
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

        [HttpPost("single/insert")]
        public IActionResult Sincronizar_single([FromBody] ProductoDto productosDto, [FromServices] IConfiguration configuration)
        {
            if (productosDto == null)
                return BadRequest("El producto está vacío.");

            var resultados = new List<object>();
            string? connectionString = configuration.GetConnectionString("CST");

            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            var syncService = new ProductoSyncService(configuration, _mapper);

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
                            syncService.ProcesarProductoSingleDto(productosDto, connection, transaction);
                            resultados.Add(new { productosDto.IdProducto, Procesado = true, Mensaje = "Procesado correctamente" });

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


        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar_mhs([FromBody] List<ProductoSimpleDto> productosDto, [FromServices] IConfiguration configuration)
        {
            if (productosDto == null || productosDto.Count == 0)
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
                            foreach (var dto in productosDto)
                            {
                                _mhsSyncService.ProcesarProductoSingleDto(dto, connection, transaction);
                               resultados.Add(new { dto.IdProducto, Procesado = true, Mensaje = "Procesado correctamente" });
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