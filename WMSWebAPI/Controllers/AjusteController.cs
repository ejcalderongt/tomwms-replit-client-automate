using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WMS.EntityCore.Dtos.Ajustes.WMS.Core.Entities;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/ajustes")]
    [Produces("application/json")]
    public sealed class SyncAjustesController : ControllerBase
    {
        private readonly IAjustesEnvioService _service;
        private readonly ILogger<SyncAjustesController> _logger;

        public SyncAjustesController(
            IAjustesEnvioService service,
            ILogger<SyncAjustesController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("mi3/pendientes-envio")]
        [ProducesResponseType(typeof(List<AjusteSimpleReturnDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAjustesPendientesEnvio(
            [FromQuery] string? noDocumento = null,
            [FromQuery] string fields = "full",
            CancellationToken ct = default)
        {
            try
            {
                var resp = await _service.GetAjustesPendientesEnvioAsync(ct);
                var data = resp.Ajustes ?? new List<clsBeAjustesMI3>();

                if (!string.IsNullOrWhiteSpace(noDocumento))
                {
                    data = data.Where(x => x.NoDocumento != null &&
                                           x.NoDocumento.Equals(noDocumento, StringComparison.OrdinalIgnoreCase))
                               .ToList();
                }

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

        /// <summary>
        /// Marca como enviados a ERP los ajustes (Enviado_A_ERP = 1) cuando cumplen las reglas de detalle enviado.
        /// </summary>
        [HttpPut("mi3/ajustes/enviados-erp")]
        [ProducesResponseType(typeof(MarcarAjustesEnviadosErpResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarcarAjustesEnviadosErpAsync(
            [FromBody] MarcarAjustesEnviadosErpRequestDto request,
            CancellationToken ct)
        {
            if (request == null)
                return BadRequest(new { message = "Request es nulo." });

            if (request.IdsAjusteEnc == null || request.IdsAjusteEnc.Count == 0)
                return BadRequest(new { message = "Debe enviar al menos un IdAjusteEnc." });

            try
            {
                var afectados = await _service.MarcarAjustesEnviadosErpAsync(request.IdsAjusteEnc, ct);

                return Ok(new MarcarAjustesEnviadosErpResponseDto
                {
                    FilasAfectadas = afectados,
                    CantidadSolicitada = request.IdsAjusteEnc.Where(x => x > 0).Distinct().Count()
                });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, new { message = "Request cancelado." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en MarcarAjustesEnviadosErpAsync");
                return StatusCode(500, new { message = $"{nameof(MarcarAjustesEnviadosErpAsync)} {ex.Message}" });
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

    public sealed class MarcarAjustesEnviadosErpRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Debe enviar al menos un IdAjusteEnc.")]
        public List<int> IdsAjusteEnc { get; set; } = new();
    }

    public sealed class MarcarAjustesEnviadosErpResponseDto
    {
        public int FilasAfectadas { get; set; }
        public int CantidadSolicitada { get; set; }
    }
}