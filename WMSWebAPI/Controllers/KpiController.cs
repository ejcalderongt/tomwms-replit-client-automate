using Microsoft.AspNetCore.Mvc;
using WMS.EntityCore.Dtos.KPI;
using WMSWebAPI.Services.KPI;

namespace WMSWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KpiController : ControllerBase
    {
        private readonly IKpiReportService _service;

        public KpiController(IKpiReportService service)
        {
            _service = service;
        }

        [HttpGet("picking")]
        public async Task<ActionResult<List<KpiPickingRowDto>>> Get_Picking([FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken ct)
        {
            try
            {
                var data = await _service.GetPickingAsync(from, to, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint KPI Picking",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("verificacion")]
        public async Task<ActionResult<List<KpiVerificacionRowDto>>> Get_Verificacion(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken ct)
        {
            try
            {
                var data = await _service.GetVerificacionAsync(from, to, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint KPI Verificación",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("recepcion")]
        public async Task<ActionResult<List<KpiRecepcionRowDto>>> Get_Recepcion(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken ct)
        {
            try
            {
                var data = await _service.GetRecepcionAsync(from, to, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint KPI Recepción",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("despacho")]
        public async Task<ActionResult<List<KpiDespachoRowDto>>> Get_Despacho(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken ct)
        {
            try
            {
                var data = await _service.GetDespachoAsync(from, to, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint KPI Despacho",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("tendencias/despacho")]
        public async Task<ActionResult<List<KpiTendenciaDespachoRowDto>>> Get_Tendencia_Despacho(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] string? gran,
        [FromQuery] int? top,
        CancellationToken ct)
        {
            try
            {
                var data = await _service.GetTendenciaDespachoProductoFamiliaAsync(from, to, gran, top, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint tendencia despacho",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("tendencias/operacion/heatmap-diahora")]
        public async Task<ActionResult<List<KpiHeatmapDiaHoraDto>>> Get_Heatmap_Operacion_DiaHora(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken ct)
        {
            try
            {                
                var data = await _service.GetHeatmapDespachoDiaHoraAsync(from, to, ct);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint heatmap operación día/hora",
                    detail = ex.Message
                });
            }
        }
        [HttpGet("stock/res")]
        public async Task<ActionResult<List<KpiStockResRowDto>>> Get_Stock_Res(
       [FromQuery] int? idBodega,
       [FromQuery] string? codigo,
       [FromQuery] string? familia,
       [FromQuery] string? estado,
       [FromQuery] string? licencia,
       [FromQuery] string? lote,
       [FromQuery] DateTime? venceFrom,
       [FromQuery] DateTime? venceTo,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 200,
       CancellationToken ct = default)
        {
            try
            {                
                var data = await _service.GetStockResAsync(idBodega, codigo, familia, estado, licencia, lote,venceFrom, venceTo, page, pageSize, ct);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error en endpoint Stock Res",
                    detail = ex.Message
                });
            }
        }
    }
}