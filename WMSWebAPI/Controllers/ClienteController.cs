using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Dtos.Clientes;
using WMSWebAPI.Services.Cliente;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IClienteMi3SyncService _syncService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IMapper mapper, IClienteMi3SyncService syncService, ILogger<ClienteController> logger)
        {
            _mapper = mapper;
            _syncService = syncService;
            _logger = logger;
        }

        [HttpPost("list/mi3/insert")]
        public async Task<IActionResult> Sincronizar([FromBody] List<ClienteMi3Dto> ClienteMi3Dto, [FromServices] IConfiguration configuration) 
        {
            if (ClienteMi3Dto == null || ClienteMi3Dto.Count == 0)
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

                            foreach (var dto in ClienteMi3Dto)
                            {
                                if (string.IsNullOrEmpty(dto.codigo))
                                    return StatusCode(500, new { Exito = false, Mensaje = "El código no puede estar vacio." });

                                _syncService.ProcesarClienteDesdeDto(dto, connection, transaction);
                                resultados.Add(new { dto.codigo, Procesado = true, Mensaje = "Procesado correctamente" });
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
        [HttpGet("list/mi3/all")]
        public IActionResult Get_All()
        {
            try
            {
                var clientes = _syncService.Get_All();

                if (clientes == null || clientes.Count == 0)
                    return NotFound(new { Exito = false, Mensaje = "No se encontraron clientes." });

                return Ok(new { Exito = true, Data = clientes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de clientes MI3");
                return StatusCode(500, new { Exito = false, Mensaje = "Ocurrió un error al obtener los clientes." });
            }
        }

        // POST:
        [HttpPost("/api/Clientes/mi3/asociar_bodega")]
        public IActionResult AsociarClienteABodega([FromBody] ClienteBodegaRequestDto request,
                                                   [FromServices] IConfiguration configuration)
        {
            if (request == null)
                return BadRequest("El cuerpo de la petición es nulo.");

            if (string.IsNullOrWhiteSpace(request.CodigoCliente))
                return BadRequest("El código de cliente es obligatorio.");

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

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var effectiveTx = transaction;
                            var now = DateTime.Now;

                            // 1) Obtener cliente por código                            
                            var clienteActual = clsLnCliente.Get_Single_By_Codigo(request.CodigoCliente,
                                                                                  connection,
                                                                                  effectiveTx);

                            if (clienteActual is null || clienteActual.IdCliente <= 0)
                            {
                                transaction.Rollback();
                                return NotFound(new
                                {
                                    Exito = false,
                                    Mensaje = $"No se encontró el cliente con código: '{request.CodigoCliente}'."
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

                            // 3) Config de interface (para setear User_agr/mod como en tu ejemplo)
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

                            // 4) Validar si ya existe relación Cliente-Bodega                            
                            bool yaExiste = clsLnCliente_bodega.ExisteRelacion(
                                clienteActual.IdCliente,
                                idBodega,
                                connection,
                                effectiveTx);

                            if (yaExiste)
                            {
                                transaction.Rollback();
                                return Conflict(new
                                {
                                    Exito = false,
                                    Mensaje = "El cliente ya está asociado a esa bodega."
                                });
                            }

                            // 5) Obtener siguiente IdClienteBodega (MaxID + 1)
                            int nextIdClienteBodega = clsLnCliente_bodega.MaxID(connection, effectiveTx) + 1;

                            // 6) Crear relación cliente-bodega
                            var clienteBodega = new clsBeCliente_bodega
                            {
                                IdClienteBodega = nextIdClienteBodega,
                                IdCliente = clienteActual.IdCliente,
                                IdBodega = idBodega,
                                Activo = true,                                
                                Fec_agr = now,
                                Fec_mod = now,
                                User_agr = navConfigEnc.IdUsuario.ToString(),
                                User_mod = navConfigEnc.IdUsuario.ToString()
                            };

                            // 7) Insertar relación
                            clsLnCliente_bodega.Insertar(clienteBodega, connection, effectiveTx);

                            transaction.Commit();
                            scope.Complete();

                            return Ok(new
                            {
                                Exito = true,
                                Mensaje = "Cliente asociado a la bodega correctamente.",
                                Data = new
                                {
                                    clienteBodega.IdClienteBodega,
                                    clienteBodega.IdCliente,
                                    request.CodigoCliente,
                                    clienteBodega.IdBodega,
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
                                Mensaje = "Error al asociar el cliente a la bodega → " + ex.Message
                            });
                        }
                    }
                }
            }
        }
        // DTO equivalente
        public class ClienteBodegaRequestDto
        {
            public string CodigoCliente { get; set; } = string.Empty;
            public string CodigoBodega { get; set; } = string.Empty;
        }
    }
}