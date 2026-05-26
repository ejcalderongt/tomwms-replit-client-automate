using Microsoft.AspNetCore.Mvc;
using WMS.EntityCore.Dtos.Inventario;
using WMSWebAPI.Services.Inventario;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _service;

        public InventarioController(IInventarioService service)
        {
            _service = service;
        }

        [HttpGet("existencias-lote")]
        public async Task<ActionResult<InventarioPagedResultDto<InventarioExistenciaLoteDto>>> GetExistenciasPorLote(
            [FromQuery] InventarioFiltroDto filtro,
            CancellationToken ct)
        {
            try
            {
                var result = await _service.GetExistenciasPorLoteAsync(filtro, ct);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { codigo = "FILTRO_INVALIDO", mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { codigo = "ERROR_INTERNO", mensaje = "Error al consultar inventario.", detalle = ex.Message });
            }
        }

        [HttpGet("resumen")]
        public async Task<ActionResult<IReadOnlyList<InventarioResumenDto>>> GetResumen(
            [FromQuery] InventarioFiltroDto filtro,
            [FromQuery] string grupo = "producto-lote",
            CancellationToken ct = default)
        {
            try
            {
                var result = await _service.GetResumenAsync(filtro, grupo, ct);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { codigo = "FILTRO_INVALIDO", mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { codigo = "ERROR_INTERNO", mensaje = "Error al resumir inventario.", detalle = ex.Message });
            }
        }
    }
}
