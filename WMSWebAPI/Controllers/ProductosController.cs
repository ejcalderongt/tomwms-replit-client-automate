using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoMi3SyncService _mhsSyncService;

        public ProductosController(IMapper mapper, IProductoMi3SyncService mhsSyncService)
        {
            _mapper = mapper;
            _mhsSyncService = mhsSyncService;
        }

        [HttpPost("list/insert")]
        public IActionResult Sincronizar([FromBody] List<Producto3PL_Dto> productosDto, [FromServices] IConfiguration configuration)
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
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                syncService.ProcesarProducto3PLDesdeDto(dto, connection, transaction);
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
     

        // GET: api/Productos/list/mi3/all
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All([FromQuery] string? fields)
        {
            try
            {
                var productos = _mhsSyncService.Get_All();

                if (productos == null || productos.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron productos." });

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    var fieldSet = fields.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(f => f.Trim().ToLower())
                                         .ToHashSet();

                    var data = productos.Select(p =>
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

                return Ok(new { Exito = true, Data = productos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = "Error al obtener productos → " + ex.Message });
            }
        }
        [HttpPost("list/mi3/insert")]
        public IActionResult Sincronizar_mhs([FromBody] List<ProductoMi3Dto> productosDto, [FromServices] IConfiguration configuration)
        {
            if (productosDto == null || productosDto.Count == 0)
                return BadRequest("La lista de ProductosMi3 está vacía.");

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
                                if (string.IsNullOrEmpty(dto.Codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                // Modificado: Ahora retorna el IdProducto generado
                                int idProductoGenerado = _mhsSyncService.ProcesarProductoSingleDto(dto, connection, transaction);

                                // Actualizar el DTO con el ID generado
                                dto.IdProducto = idProductoGenerado;

                                resultados.Add(new
                                {
                                    IdProducto = idProductoGenerado,
                                    dto.Codigo,
                                    Procesado = true,
                                    Mensaje = "Procesado correctamente"
                                });
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

        // POST: 
        [HttpPost("/api/Productos/mi3/asociar_bodega")]
        public IActionResult AsociarProductoABodega(
            [FromBody] ProductoBodegaRequestDto request,
            [FromServices] IConfiguration configuration)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición es nulo.");

            if (string.IsNullOrWhiteSpace(request.CodigoProducto))
                return BadRequest("El código de producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.CodigoBodega))
                return BadRequest("El código de bodega es obligatorio.");

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
                            var effectiveTx = transaction;
                            var now = DateTime.Now;                            

                            // 1. Obtener producto por código
                            var productoActual = clsLnProducto.Get_By_Codigo(
                                request.CodigoProducto,
                                connection,
                                effectiveTx);

                            if (productoActual is null || productoActual.IdProducto <= 0)
                            {
                                transaction.Rollback();
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró el producto con código: '{request.CodigoProducto}'."
                                });
                            }

                            // 2. Obtener IdBodega por código
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

                            clsBeI_nav_config_enc? BeInavConfigEnc = new clsBeI_nav_config_enc();
                            BeInavConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(idBodega,connection,effectiveTx);

                            if (BeInavConfigEnc==null)
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró la configuración de interface para la bodega con código: '{request.CodigoBodega}'."
                                });

                            // 3. Obtener siguiente IdProductoBodega
                            int nextIdProductoBodega = clsLnProducto_bodega.MaxID(connection, effectiveTx) + 1;

                            // 4. Crear relación producto-bodega
                            var productoBodega = new clsBeProducto_bodega
                            {
                                IdProductoBodega = nextIdProductoBodega,
                                IdProducto = productoActual.IdProducto,
                                IdBodega = idBodega,
                                Activo = true,
                                Sistema = false,
                                Fec_agr = now,
                                Fec_mod = now,
                                User_agr = BeInavConfigEnc.IdUsuario.ToString(),
                                User_mod = BeInavConfigEnc.IdUsuario.ToString()
                            };

                            // 5. Insertar relación
                            clsLnProducto_bodega.Insertar(productoBodega, connection, effectiveTx);

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new
                            {
                                Exito = true,
                                Mensaje = "Producto asociado a la bodega correctamente.",
                                Data = new
                                {
                                    productoBodega.IdProductoBodega,
                                    productoBodega.IdProducto,
                                    request.CodigoProducto,
                                    productoBodega.IdBodega,
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
                                Mensaje = "Error al asociar el producto a la bodega → " + ex.Message
                            });
                        }
                    }
                }
            }
        }


    }
}