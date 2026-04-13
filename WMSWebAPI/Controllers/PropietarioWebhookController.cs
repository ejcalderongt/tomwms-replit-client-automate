using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Propietario;

namespace WMSWebAPI.Controllers.Webhooks
{
    [ApiController]
    [Route("api/webhooks/[controller]")]
    public class PropietarioWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPropietarioService _propietarioService;
        private readonly ILogger<PropietarioWebhookController> _logger;

        public PropietarioWebhookController(
            IConfiguration configuration,
            IPropietarioService propietarioService,
            ILogger<PropietarioWebhookController> logger)
        {
            _configuration = configuration;
            _propietarioService = propietarioService;
            _logger = logger;
        }

        [HttpPost("recibir")]
        public async Task<IActionResult> RecibirPropietario([FromBody] PropietarioDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, error = "El payload no puede estar vacío" });

            _logger.LogInformation("WebHook recibido - Procesando propietario: {@Propietario}", request);

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.ProcesarPropietarioDesdeDto(request, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Propietario procesado exitosamente",
                    idPropietario = request.IdPropietario,
                    codigo = request.Codigo
                });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en webhook");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpPost("recibir-lote")]
        public async Task<IActionResult> RecibirPropietariosLote([FromBody] List<PropietarioDto> requests)
        {
            if (requests == null || !requests.Any())
                return BadRequest(new { success = false, error = "La lista no puede estar vacía" });

            _logger.LogInformation("WebHook recibido - Procesando lote de {Cantidad} propietarios", requests.Count);

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.ProcesarListaPropietariosDesdeDto(requests, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, total = requests.Count, message = "Lote procesado exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en webhook lote");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpPut("actualizar/{idPropietario}")]
        public async Task<IActionResult> ActualizarPropietario(int idPropietario, [FromBody] PropietarioDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, error = "El payload no puede estar vacío" });

            if (idPropietario != request.IdPropietario)
                return BadRequest(new { success = false, error = "El ID no coincide" });

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                var existe = _propietarioService.ExistePropietario(idPropietario, connection, transaction);
                if (!existe)
                    return NotFound(new { success = false, error = $"No se encontró propietario con ID {idPropietario}" });

                _propietarioService.ProcesarPropietarioDesdeDto(request, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, message = "Propietario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en webhook actualizar");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpDelete("eliminar/{idPropietario}")]
        public async Task<IActionResult> EliminarPropietario(int idPropietario)
        {
            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                var existe = _propietarioService.ExistePropietario(idPropietario, connection, transaction);
                if (!existe)
                    return NotFound(new { success = false, error = $"No se encontró propietario con ID {idPropietario}" });

                _propietarioService.EliminarPropietario(idPropietario, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, message = "Propietario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en webhook eliminar");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpGet("consultar/{idPropietario}")]
        public IActionResult ConsultarPropietario(int idPropietario)
        {
            try
            {
                var propietario = _propietarioService.GetById(idPropietario);
                if (propietario == null)
                    return NotFound(new { success = false, error = $"No se encontró propietario con ID {idPropietario}" });

                return Ok(new { success = true, data = propietario });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook consultar");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("consultar-por-codigo/{codigo}")]
        public IActionResult ConsultarPropietarioPorCodigo(string codigo)
        {
            try
            {
                var propietario = _propietarioService.GetByCodigo(codigo);
                if (propietario == null)
                    return NotFound(new { success = false, error = $"No se encontró propietario con código {codigo}" });

                return Ok(new { success = true, data = propietario });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook consultar por código");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("consultar-bodegas/{idPropietario}")]
        public IActionResult ConsultarBodegasPropietario(int idPropietario)
        {
            try
            {
                var bodegas = _propietarioService.GetBodegasCompletasByPropietarioId(idPropietario);
                return Ok(new { success = true, data = bodegas, count = bodegas.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en webhook consultar bodegas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost("sincronizar-bodegas/{idPropietario}")]
        public async Task<IActionResult> SincronizarBodegasPropietario(int idPropietario)
        {
            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.SincronizarBodegasPropietario(idPropietario, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, message = "Bodegas sincronizadas exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en webhook sincronizar bodegas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                status = "healthy",
                service = "PropietarioWebhook",
                timestamp = DateTime.UtcNow,
                endpoints = new[]
                {
                    "POST /recibir", "POST /recibir-lote", "PUT /actualizar/{id}",
                    "DELETE /eliminar/{id}", "GET /consultar/{id}", "GET /consultar-por-codigo/{codigo}",
                    "GET /consultar-bodegas/{id}", "POST /sincronizar-bodegas/{id}", "GET /health"
                }
            });
        }
    }
}