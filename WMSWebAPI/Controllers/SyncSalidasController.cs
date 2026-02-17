using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Transactions;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Services.Salidas;
using WMS.EntityCore.Dtos.Pedido;
using WMS.EntityCore.Dtos;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/salidas")]
    public class SyncSalidasController : ControllerBase
    {
        private readonly ISyncSalidasService _salidaService;

        public SyncSalidasController(ISyncSalidasService service)
        {
            _salidaService = service;
        }

        [HttpPost("documento-salida")]
        public async Task<IActionResult> PostDocumentoSalida([FromBody] List<SalidaTrans_3plDto> dto,
                                                             [FromServices] IConfiguration configuration,
                                                             [FromServices] ILogger<SyncSalidasController> _logger)
        {
            if (dto == null || dto.Count == 0)
            {
                _logger.LogWarning("Lista de SalidaTransDto es nula o vacía.");
                return BadRequest("Debe proporcionar al menos un documento de salida.");
            }

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }, TransactionScopeAsyncFlowOption.Enabled);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var salida in dto)
                {
                    _salidaService.ProcesarSalidaDesde_3plDto(salida, connection, transaction);
                }

                transaction.Commit();
                scope.Complete();

                _logger.LogInformation("Documento(s) de salida procesado(s) correctamente.");
                return Ok(new { Exito = true, Mensaje = "Documento(s) de salida procesado(s) correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar documento(s) de salida.");
                transaction.Rollback();

                var showStackTrace = configuration.GetValue<bool>("MostrarDetallesErrores");
                return StatusCode(500, new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalles = showStackTrace ? ex.ToString() : null
                });
            }
        }
        [HttpPost("documentos-salida/listar")]
        public IActionResult Listar([FromBody] DocumentoSalidaFiltroDto filtro)
        {
            try
            {
                var documentos = _salidaService.ObtenerDocumentosDeSalida(true,
                                                                        filtro.FechaInicio,
                                                                        filtro.FechaFin,
                                                                        filtro.IdBodega,
                                                                        filtro.IdPropietario
                                                                        );

                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("{IdPedidoEnc}/detalle-pe")]
        public IActionResult GetDetallePedido(int IdPedidoEnc, [FromServices] ILogger<SyncSalidasController> _logger)
        {
            try
            {
                var detallesOS = _salidaService.ObtenerDetallePedido(IdPedidoEnc);
                return Ok(detallesOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetDetalleOS");
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("{IdOrdenSalidaEnc}/despachos")]
        public IActionResult ObtenerDespachosPedido(int IdOrdenSalidaEnc)
        {
            try
            {
                var despachos = _salidaService.ObtenerDespachos(IdOrdenSalidaEnc, null, null);
                return Ok(despachos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
        
        [HttpPost("mi3/insertar")]
        public IActionResult PostMi3Documento([FromBody] NavPedTrasladoRequestDto request,
                                              [FromServices] IConfiguration configuration,
                                              [FromServices] ILogger<SyncSalidasController> _logger)
        {
            // Validar que el request y el documento interno no sean nulos
            if (request == null || request.beINavPedCompraEnc == null)
            {
                _logger.LogWarning("Request o documento clsBeI_nav_ped_traslado_enc es nulo.");
                return BadRequest("Debe proporcionar un documento válido con la estructura { beINavPedCompraEnc: {...} }");
            } 

            // MAPEO EXPLÍCITO: Extraer el documento del wrapper DTO
            // Esto asegura que documento.Lineas_Detalle ya esté poblado desde el JSON
            // ANTES de llamar a Datos_Validos() (replica patrón SAP HANA)
            var documento = request.beINavPedCompraEnc;

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            try
            {
                string resultado = string.Empty;
                var salidaService = _salidaService as SyncSalidasService;

                if (salidaService == null)
                {
                    throw new InvalidOperationException("El servicio no implementa la interfaz requerida");
                }

                // AHORA documento.Lineas_Detalle ya está poblado desde el JSON deserializado                
                int lineasProcesadas = salidaService.Insert_salida_mi3(ref documento, ref resultado);

                // Validar resultado de texto (errores explicitos)
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("exito"))
                {
                    throw new Exception(resultado);
                }

                // CRITICO: Si no se procesaron lineas, es un error (reserva fallo)
                if (lineasProcesadas == 0)
                {
                    string mensajeError = !string.IsNullOrEmpty(resultado)
                        ? resultado
                        : "No se procesaron lineas. Verifique stock disponible y configuracion de bodega.";
                    throw new Exception(mensajeError);
                }

                _logger.LogInformation("Documento MI3 procesado correctamente. Lineas procesadas: {LineasProcesadas}", lineasProcesadas);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Documento MI3 procesado correctamente.",
                    LineasProcesadas = lineasProcesadas,
                    Resultado = resultado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar documento MI3.");

                var showStackTrace = configuration.GetValue<bool>("MostrarDetallesErrores");
                return StatusCode(500, new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalles = showStackTrace ? ex.ToString() : null
                });
            }
        }
        [HttpGet("mi3/pendientes-procesar")]
        public IActionResult GetSalidasPendientesDeProcesar([FromServices] IConfiguration _configuration,
        [FromQuery] string? noPedido = null,
        [FromQuery] string fields = "full")
        {
            try
            {

                // 1) Traer pendientes
                var data = _salidaService.Get_Salidas_Pendientes_De_Procesar(noPedido);

                // 2) Filtro opcional por No_pedido (igual que ingresos)
                if (!string.IsNullOrWhiteSpace(noPedido))
                    data = data.Where(x => x.No_pedido != null &&
                                           x.No_pedido.Equals(noPedido, StringComparison.OrdinalIgnoreCase))
                               .ToList();

                // 3) Minimal
                if (fields.Equals("minimal", StringComparison.OrdinalIgnoreCase))
                {
                    var umIds = data.Select(x => x.Idunidadmedida).Where(id => id > 0).Distinct().ToList();
                    var presIds = data.Select(x => x.Idpresentacion).Where(id => id > 0).Distinct().ToList();

                    var ums = clsLnUnidad_medida.GetByIds(_configuration, umIds);
                    var presList = clsLnProducto_presentacion.GetByIds(_configuration, presIds);

                    var umById = ums.ToDictionary(u => u.IdUnidadMedida, u => u.Codigo);
                    var presCodigoById = presList.ToDictionary(p => p.IdPresentacion, p => p.Codigo ?? "");

                    var result = data.Select(x => new SalidaSimpleReturnDto
                    {
                        Idtransaccion = x.Idtransaccion,
                        No_pedido = x.No_pedido,
                        Codigo_producto = x.Codigo_producto,
                        Nombre_producto = x.Nombre_producto,
                        UM = umById.TryGetValue(x.Idunidadmedida, out var um) ? um : "",
                        Presentacion = presCodigoById.TryGetValue(x.Idpresentacion, out var codPres) ? codPres : "",
                        Cantidad = x.Cantidad,
                        Lote = x.Lote,
                        Vence = x.Fecha_vence,
                        Linea = int.TryParse(x.No_linea, out var ln) ? ln : 0,                        
                        Licencia = x.Lic_Plate,
                        Fecha = x.Fec_agr
                    }).ToList();

                    return Ok(result);
                }

                // 4) Full
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }
    }
}
