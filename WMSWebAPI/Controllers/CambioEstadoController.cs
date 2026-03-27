using Microsoft.AspNetCore.Mvc;
using WMS.EntityCore.Cambio_Ubicacion;
using WMS.EntityCore.Dtos.Cambio_Estado;
using WMSWebAPI.Services.Cambio_Estado;

namespace WMSWebAPI.Controllers
{    

    namespace WMSWebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        [Produces("application/json")]
        public class CambioEstadoController : ControllerBase
        {
            private readonly ICambioEstadoService _cambioEstadoService;
            private readonly ILogger<CambioEstadoController> _logger;

            public CambioEstadoController(
                ICambioEstadoService cambioEstadoService,
                ILogger<CambioEstadoController> logger)
            {
                _cambioEstadoService = cambioEstadoService;
                _logger = logger;
            }

            /// <summary>
            /// Obtiene todas las transacciones de cambio de estado pendientes de sincronización
            /// </summary>
            /// <returns>Lista de transacciones pendientes</returns>
            [HttpGet("mi3/pendientes-procesar")]
            [ProducesResponseType(typeof(List<CambioEstadoEncabezadoDto>), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetPendientesSincronizacion()
            {
                try
                {
                    _logger.LogInformation("Iniciando consulta de transacciones pendientes de sincronización");

                    var result = await _cambioEstadoService.GetAllPendientesSincronizacionSimplificadoAsync();

                    _logger.LogInformation("Se obtuvieron {Count} transacciones pendientes", result?.Count ?? 0);

                    return Ok(result ?? new List<CambioEstadoEncabezadoDto>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener transacciones pendientes de sincronización");
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error al obtener transacciones pendientes",
                        error = ex.Message
                    });
                }
            }

            /// <summary>
            /// Obtiene una transacción de cambio de estado por su ID
            /// </summary>
            /// <param name="id">ID de la transacción</param>
            /// <returns>Transacción encontrada</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(clsBeTrans_ubic_hh_enc), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetById(int id)
            {
                try
                {
                    _logger.LogInformation("Consultando transacción por ID: {Id}", id);

                    var result = await _cambioEstadoService.GetByIdAsync(id);

                    if (result == null)
                    {
                        _logger.LogWarning("Transacción no encontrada para ID: {Id}", id);
                        return NotFound(new { message = $"No se encontró transacción con ID {id}" });
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener transacción por ID: {Id}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error al obtener la transacción",
                        error = ex.Message
                    });
                }
            }

            /// <summary>
            /// Marca una transacción como sincronizada
            /// </summary>
            /// <param name="id">ID de la transacción</param>
            /// <returns>Resultado de la operación</returns>
            [HttpPatch("{id}/sincronizar")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> MarcarComoSincronizado(int id)
            {
                try
                {
                    _logger.LogInformation("Marcando transacción {Id} como sincronizada", id);

                    // Verificar que existe
                    var existe = await _cambioEstadoService.GetByIdAsync(id);
                    if (existe == null)
                    {
                        _logger.LogWarning("No se puede marcar como sincronizada - Transacción no encontrada: {Id}", id);
                        return NotFound(new { message = $"No se encontró transacción con ID {id}" });
                    }

                    await _cambioEstadoService.MarcarComoSincronizadoAsync(id);

                    _logger.LogInformation("Transacción {Id} marcada como sincronizada exitosamente", id);

                    return Ok(new { message = $"Transacción {id} marcada como sincronizada exitosamente" });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al marcar transacción {Id} como sincronizada", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error al marcar la transacción como sincronizada",
                        error = ex.Message
                    });
                }
            }

            /// <summary>
            /// Marca múltiples transacciones como sincronizadas
            /// </summary>
            /// <param name="request">Lista de IDs de transacciones</param>
            /// <returns>Resultado de la operación</returns>
            [HttpPatch("sincronizar-masivo")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> MarcarMultiplesComoSincronizados([FromBody] SincronizarMasivoRequest request)
            {
                try
                {
                    if (request?.Ids == null || request.Ids.Count == 0)
                    {
                        return BadRequest(new { message = "La lista de IDs no puede estar vacía" });
                    }

                    _logger.LogInformation("Marcando {Count} transacciones como sincronizadas", request.Ids.Count);

                    await _cambioEstadoService.MarcarMultiplesComoSincronizadosAsync(request.Ids);

                    _logger.LogInformation("{Count} transacciones marcadas como sincronizadas exitosamente", request.Ids.Count);

                    return Ok(new
                    {
                        message = $"{request.Ids.Count} transacciones marcadas como sincronizadas exitosamente",
                        sincronizados = request.Ids.Count
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al marcar múltiples transacciones como sincronizadas");
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error al marcar las transacciones como sincronizadas",
                        error = ex.Message
                    });
                }
            }

            /// <summary>
            /// Procesa y sincroniza todas las transacciones pendientes
            /// </summary>
            /// <returns>Resultado del procesamiento</returns>
            [HttpPost("procesar-pendientes")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> ProcesarPendientes()
            {
                try
                {
                    _logger.LogInformation("Iniciando procesamiento de transacciones pendientes");

                    var pendientes = await _cambioEstadoService.GetAllPendientesSincronizacionSimplificadoAsync();

                    if (pendientes == null || pendientes.Count == 0)
                    {
                        _logger.LogInformation("No hay transacciones pendientes para procesar");
                        return Ok(new { message = "No hay transacciones pendientes para procesar", procesados = 0 });
                    }

                    _logger.LogInformation("Procesando {Count} transacciones pendientes", pendientes.Count);

                    // Aquí puedes agregar la lógica de negocio para enviar a MI3
                    foreach (var transaccion in pendientes)
                    {
                        try
                        {
                            // TODO: Implementar lógica de envío a MI3
                            _logger.LogDebug("Procesando transacción ID: {Id}", transaccion.IdTareaUbicacionEnc);

                            // Después de procesar exitosamente, marcar como sincronizada
                            await _cambioEstadoService.MarcarComoSincronizadoAsync(transaccion.IdTareaUbicacionEnc);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al procesar transacción ID: {Id}", transaccion.IdTareaUbicacionEnc);
                            // Continuar con la siguiente transacción
                        }
                    }

                    return Ok(new
                    {
                        message = $"Procesamiento completado",
                        total = pendientes.Count
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar transacciones pendientes");
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        message = "Error al procesar transacciones pendientes",
                        error = ex.Message
                    });
                }
            }
        }

        /// <summary>
        /// Request model para sincronización masiva
        /// </summary>
        public class SincronizarMasivoRequest
        {
            /// <summary>
            /// Lista de IDs de transacciones a sincronizar
            /// </summary>
            public List<int> Ids { get; set; } = new();
        }
    }
}