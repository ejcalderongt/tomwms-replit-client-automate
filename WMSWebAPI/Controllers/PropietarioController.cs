using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services.Propietario;

namespace WMSWebAPI.Controllers.Catalogos
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropietarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPropietarioService _propietarioService;
        private readonly ILogger<PropietarioController> _logger;

        public PropietarioController(
            IConfiguration configuration,
            IPropietarioService propietarioService,
            ILogger<PropietarioController> logger)
        {
            _configuration = configuration;
            _propietarioService = propietarioService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var propietarios = _propietarioService.GetAll();
                return Ok(new { success = true, data = propietarios, count = propietarios.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAll");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetById/{idPropietario}")]
        public IActionResult GetById(int idPropietario)
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
                _logger.LogError(ex, "Error en GetById");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetByCodigo/{codigo}")]
        public IActionResult GetByCodigo(string codigo)
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
                _logger.LogError(ex, "Error en GetByCodigo");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetActivos")]
        public IActionResult GetActivos()
        {
            try
            {
                var propietarios = _propietarioService.GetActivos();
                return Ok(new { success = true, data = propietarios, count = propietarios.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetActivos");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] PropietarioFilterDto filter)
        {
            try
            {
                var result = _propietarioService.GetByFilter(filter ?? new PropietarioFilterDto());
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByFilter");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] PropietarioDto propietarioDto)
        {
            if (propietarioDto == null)
                return BadRequest(new { success = false, error = "Los datos no pueden estar vacíos" });

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.ProcesarPropietarioDesdeDto(propietarioDto, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Propietario creado exitosamente",
                    idPropietario = propietarioDto.IdPropietario
                });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en Crear");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpPut("Actualizar/{idPropietario}")]
        public async Task<IActionResult> Actualizar(int idPropietario, [FromBody] PropietarioDto propietarioDto)
        {
            if (propietarioDto == null)
                return BadRequest(new { success = false, error = "Los datos no pueden estar vacíos" });

            if (idPropietario != propietarioDto.IdPropietario)
                return BadRequest(new { success = false, error = "El ID de la URL no coincide con el ID del payload" });

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

                _propietarioService.ProcesarPropietarioDesdeDto(propietarioDto, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, message = "Propietario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en Actualizar");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpDelete("Eliminar/{idPropietario}")]
        public async Task<IActionResult> Eliminar(int idPropietario)
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
                _logger.LogError(ex, "Error en Eliminar");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] PropietarioDto propietarioDto)
        {
            if (propietarioDto == null)
                return BadRequest(new { success = false, error = "Los datos no pueden estar vacíos" });

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.ProcesarPropietarioDesdeDto(propietarioDto, connection, transaction);

                await transaction.CommitAsync();

                var mensaje = propietarioDto.IdPropietario > 0 ? "actualizado" : "creado";
                return Ok(new { success = true, message = $"Propietario {mensaje} exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en Upsert");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpPost("ProcesarLote")]
        public async Task<IActionResult> ProcesarLote([FromBody] List<PropietarioDto> propietariosDto)
        {
            if (propietariosDto == null || !propietariosDto.Any())
                return BadRequest(new { success = false, error = "La lista no puede estar vacía" });

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                await connection.OpenAsync();
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                _propietarioService.ProcesarListaPropietariosDesdeDto(propietariosDto, connection, transaction);

                await transaction.CommitAsync();

                return Ok(new { success = true, message = $"Lote de {propietariosDto.Count} propietarios procesado exitosamente" });
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                _logger.LogError(ex, "Error en ProcesarLote");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpGet("GetBodegas/{idPropietario}")]
        public IActionResult GetBodegas(int idPropietario)
        {
            try
            {
                var bodegasIds = _propietarioService.GetBodegasByPropietarioId(idPropietario);
                return Ok(new { success = true, data = bodegasIds, count = bodegasIds.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetBodegas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetBodegasCompletas/{idPropietario}")]
        public IActionResult GetBodegasCompletas(int idPropietario)
        {
            try
            {
                var bodegas = _propietarioService.GetBodegasCompletasByPropietarioId(idPropietario);
                return Ok(new { success = true, data = bodegas, count = bodegas.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetBodegasCompletas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost("SincronizarBodegas/{idPropietario}")]
        public async Task<IActionResult> SincronizarBodegas(int idPropietario)
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
                _logger.LogError(ex, "Error en SincronizarBodegas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                if (connection != null && connection.State == ConnectionState.Open) await connection.CloseAsync();
                if (connection != null) await connection.DisposeAsync();
            }
        }

        [HttpGet("GetAllWithBodegas")]
        public IActionResult GetAllWithBodegas()
        {
            try
            {
                var resultado = _propietarioService.GetAllWithBodegas();
                return Ok(new { success = true, data = resultado, count = resultado.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllWithBodegas");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("Health")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                status = "healthy",
                service = "PropietarioController",
                timestamp = DateTime.UtcNow,
                endpoints = new[]
                {
                    "GET /GetAll", "GET /GetById/{id}", "GET /GetByCodigo/{codigo}",
                    "GET /GetActivos", "POST /GetByFilter", "POST /Crear",
                    "PUT /Actualizar/{id}", "DELETE /Eliminar/{id}", "POST /Upsert",
                    "POST /ProcesarLote", "GET /GetBodegas/{id}", "GET /GetBodegasCompletas/{id}",
                    "POST /SincronizarBodegas/{id}", "GET /GetAllWithBodegas", "GET /Health"
                }
            });
        }
    }
}