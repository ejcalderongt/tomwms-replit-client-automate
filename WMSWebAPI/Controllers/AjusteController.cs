using WMS.EntityCore.Dtos.Ajustes.WMS.Core.Entities;

namespace WMSWebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/sync/ajustes")]
    public sealed class SyncAjustesController : ControllerBase
    {
        private readonly IAjustesEnvioService _service;
        private readonly ILogger<SyncAjustesController> _logger;
        private readonly IConfiguration _configuration;

        public SyncAjustesController(
            IAjustesEnvioService service,
            ILogger<SyncAjustesController> logger,
            IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("mi3/pendientes-envio")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<AjusteSimpleReturnDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAjustesPendientesEnvio([FromQuery] string? noDocumento = null,
                                                                   [FromQuery] string fields = "full",
                                                                   CancellationToken ct = default)
        {
            try
            {
                var resp = await _service.GetAjustesPendientesEnvioAsync(ct);
                var data = resp.Ajustes ?? new List<clsBeAjustesMI3>();

                if (!string.IsNullOrWhiteSpace(noDocumento))
                    data = data.Where(x => x.NoDocumento != null &&
                                           x.NoDocumento.Equals(noDocumento, StringComparison.OrdinalIgnoreCase))
                               .ToList();

                if (fields.Equals("minimal", StringComparison.OrdinalIgnoreCase))
                {
                    var result = data.Select(x => new AjusteSimpleReturnDto
                    {
                        IdAjusteEnc = x.IdAjusteEnc,
                        IdAjusteDet = x.IdAjusteDet,
                        NoDocumento = x.NoDocumento,
                        CodigoProducto = x.Codigo_Producto,
                        CodigoBodega = x.Codigo_Bodega,
                        CodigoBodegaErp = x.Codigo_Bodega_ERP,
                        TipoAjusteWms = x.TipoAjusteWMS,
                        TipoAjusteErp = x.TipoAjusteERP,
                        Cantidad = x.Cantidad,
                        Lote = x.Lote,
                        Motivo = x.Motivo_Ajuste,
                        Observacion = x.Observacion,
                        Seccion = x.Seccion,
                        CentroCosto = x.Codigo_Centro_Costo,
                        Talla = x.Talla,
                        Color = x.Color
                    }).ToList();

                    return Ok(result);
                }

                return Ok(new
                {
                    resultado = resp.Resultado,
                    ajustes = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ajustes pendientes de envío.");
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }
    }

    public sealed class AjusteSimpleReturnDto
    {
        public int IdAjusteEnc { get; set; }
        public int IdAjusteDet { get; set; }
        public string NoDocumento { get; set; } = "";
        public string CodigoProducto { get; set; } = "";
        public string CodigoBodega { get; set; } = "";
        public string CodigoBodegaErp { get; set; } = "";
        public string TipoAjusteWms { get; set; } = "";
        public string TipoAjusteErp { get; set; } = "";
        public double Cantidad { get; set; }
        public string Lote { get; set; } = "";
        public string Motivo { get; set; } = "";
        public string Observacion { get; set; } = "";
        public string Seccion { get; set; } = "";
        public string CentroCosto { get; set; } = "";
        public string Talla { get; set; } = "";
        public string Color { get; set; } = "";
    }

}
