using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMSWebAPI.Services.Prefactura;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PreFacturaController : ControllerBase
    {
        private readonly IPrefacturaService _prefacturaService;
        private readonly ILogger<PreFacturaController> _logger;

        public PreFacturaController(
            IPrefacturaService prefacturaService,
            ILogger<PreFacturaController> logger)
        {
            _prefacturaService = prefacturaService;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene las prefacturas pendientes de enviar a ERP
        /// </summary>
        /// <param name="fechaDesde">Fecha inicial del período (opcional, formato: yyyy-MM-dd)</param>
        /// <param name="fechaHasta">Fecha final del período (opcional, formato: yyyy-MM-dd)</param>
        /// <param name="idCliente">ID del cliente (opcional)</param>
        /// <param name="pageNumber">Número de página (por defecto: 1)</param>
        /// <param name="pageSize">Tamaño de página (por defecto: 50, máximo: 100)</param>
        /// <returns>Lista paginada de prefacturas pendientes</returns>
        [HttpGet("GetPendientes")]
        public async Task<IActionResult> GetPendientes(
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] int? idCliente,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50)
        {
            try
            {
                _logger.LogInformation("GET GetPendientes - FechaDesde: {FechaDesde}, FechaHasta: {FechaHasta}, IdCliente: {IdCliente}, Page: {PageNumber}, Size: {PageSize}",
                    fechaDesde, fechaHasta, idCliente, pageNumber, pageSize);

                // Validar parámetros
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100;

                var result = await _prefacturaService.GetPrefacturasPendientesAsync(
                    fechaDesde, fechaHasta, idCliente, pageNumber, pageSize);

                if (!result.Success)
                {
                    return StatusCode(500, result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetPendientes");
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al obtener prefacturas pendientes: {ex.Message}",
                    timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Obtiene una prefactura específica por su ID
        /// </summary>
        /// <param name="id">ID de la prefactura</param>
        /// <returns>Detalle de la prefactura</returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET GetById - Id: {Id}", id);

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la prefactura debe ser mayor a cero"
                    });
                }

                var result = await _prefacturaService.GetPrefacturaByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"No se encontró la prefactura con ID {id}"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = result,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetById - Id: {Id}", id);
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al obtener la prefactura: {ex.Message}",
                    timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Marca una prefactura como procesada por el ERP
        /// </summary>
        /// <param name="id">ID de la prefactura</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("MarcarProcesado/{id}")]
        public async Task<IActionResult> MarcarProcesado(int id)
        {
            try
            {
                _logger.LogInformation("PUT MarcarProcesado - Id: {Id}", id);

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la prefactura debe ser mayor a cero"
                    });
                }

                var result = await _prefacturaService.MarcarComoProcesadaAsync(id);

                if (!result.Success)
                {
                    return StatusCode(500, result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en MarcarProcesado - Id: {Id}", id);
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al marcar la prefactura como procesada: {ex.Message}",
                    timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Obtiene el total de prefacturas pendientes
        /// </summary>
        /// <param name="fechaDesde">Fecha inicial del período (opcional, formato: yyyy-MM-dd)</param>
        /// <param name="fechaHasta">Fecha final del período (opcional, formato: yyyy-MM-dd)</param>
        /// <param name="idCliente">ID del cliente (opcional)</param>
        /// <returns>Total de prefacturas pendientes</returns>
        [HttpGet("GetTotalPendientes")]
        public async Task<IActionResult> GetTotalPendientes(
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] int? idCliente)
        {
            try
            {
                _logger.LogInformation("GET GetTotalPendientes - FechaDesde: {FechaDesde}, FechaHasta: {FechaHasta}, IdCliente: {IdCliente}",
                    fechaDesde, fechaHasta, idCliente);

                var total = await _prefacturaService.GetTotalPrefacturasPendientesAsync(fechaDesde, fechaHasta, idCliente);

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        total = total,
                        fechaDesde = fechaDesde,
                        fechaHasta = fechaHasta,
                        idCliente = idCliente
                    },
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetTotalPendientes");
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al obtener total de prefacturas pendientes: {ex.Message}",
                    timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Health check del controller
        /// </summary>
        [HttpGet("Health")]
        [AllowAnonymous]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "healthy",
                service = "PreFacturaController",
                timestamp = DateTime.UtcNow,
                endpoints = new[]
                {
                    "GET /GetPendientes",
                    "GET /GetById/{id}",
                    "PUT /MarcarProcesado/{id}",
                    "GET /GetTotalPendientes",
                    "GET /Health"
                }
            });
        }

        /// <summary>
        /// Obtiene todas las prefacturas pendientes (procesado_erp = 0) sin paginación
        /// </summary>
        [HttpGet("GetTodasPendientes")]
        public async Task<IActionResult> GetTodasPendientes()
        {
            try
            {
                _logger.LogInformation("GET GetTodasPendientes - Obteniendo todas las prefacturas pendientes");

                var result = await _prefacturaService.GetPrefacturasPendientesAsync(
                    fechaDesde: null,
                    fechaHasta: null,
                    idCliente: null,
                    pageNumber: 1,
                    pageSize: 10000);

                if (!result.Success)
                {
                    return StatusCode(500, new { success = false, message = result.Message });
                }

                return Ok(new
                {
                    success = true,
                    data = result.Data,
                    total = result.Data.Count,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetTodasPendientes");
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al obtener prefacturas pendientes: {ex.Message}",
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}