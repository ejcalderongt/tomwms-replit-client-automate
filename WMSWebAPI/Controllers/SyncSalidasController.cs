using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;
using WMS.EntityCore.Dtos;
using WMS.EntityCore.Dtos.Pedido;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Services.Salidas;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/sync/salidas")]
    public class SyncSalidasController(ISyncSalidasService service) : ControllerBase
    {
        private readonly ISyncSalidasService _salidaService = service;

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
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex, "Error en GetDetalleOS");
                }

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

        [HttpPost("anular-salida")]
        public IActionResult AnularSalida([FromBody] AnularSalidaRequestDto request,
                                          [FromServices] IConfiguration configuration,
                                          [FromServices] ILogger<SyncSalidasController> _logger)
        {
            try
            {
                var resultado = _salidaService.Anular_salida(request);

                if (resultado.Exito)
                {
                    _logger.LogInformation("Salida anulada. IdPedidoEnc={IdPedidoEnc}, Referencia={Referencia}, StockLiberado={StockLiberado}",
                                           resultado.IdPedidoEnc,
                                           resultado.Referencia,
                                           resultado.StockReservadoLiberado);
                    return Ok(resultado);
                }

                return resultado.Codigo switch
                {
                    "REQUEST_INVALID" => BadRequest(resultado),
                    "PEDIDO_NO_ENCONTRADO" => NotFound(resultado),
                    "PEDIDO_AMBIGUO" => Conflict(resultado),
                    "PEDIDO_YA_ANULADO" => Conflict(resultado),
                    "PICKING_ASOCIADO" => UnprocessableEntity(resultado),
                    "ESTADO_NO_ANULABLE" => UnprocessableEntity(resultado),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, resultado)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al anular documento de salida.");
                var showStackTrace = configuration.GetValue<bool>("MostrarDetallesErrores");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Exito = false,
                    Codigo = "ERROR_INTERNO",
                    Mensaje = ex.Message,
                    Detalles = showStackTrace ? ex.ToString() : null
                });
            }
        }

        /// <summary>
        /// Inserta un documento de traslado/pedido desde MI3.
        /// ACTUALIZADO: Ahora usa NavPedTrasladoRequestDto para mapear el JSON correctamente
        /// ANTES de llamar a Datos_Validos(), replicando el patrón de integración SAP HANA.
        /// </summary>
        [HttpPost("mi3/insertar")]
        public IActionResult PostMi3Documento([FromBody] NavPedTrasladoRequestDto request,
                                              [FromServices] IConfiguration configuration,
                                              [FromServices] ILogger<SyncSalidasController> _logger)
        {
            var requestWatch = Stopwatch.StartNew();

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
            var noDocumento = documento.No ?? "";
            var lineCount = documento.lineas_Detalle?.Count ?? 0;

            _logger.LogInformation(
                "#MI3_PERF_API_START | Documento={Documento} | Lineas={Lineas}",
                noDocumento,
                lineCount);

            string? connectionString = configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Cadena de conexión 'CST' no configurada.");
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            try
            {
                var resultado = _salidaService.Insert_salida_mi3(documento);
                requestWatch.Stop();

                _logger.LogInformation(
                    "#MI3_PERF_API_END | Documento={Documento} | Ms={ElapsedMs} | Exito={Exito} | LineasProcesadas={LineasProcesadas} | TotalSolicitado={TotalSolicitado} | TotalReservado={TotalReservado}",
                    noDocumento,
                    requestWatch.ElapsedMilliseconds,
                    resultado.Exito,
                    resultado.LineasProcesadas,
                    resultado.TotalSolicitado,
                    resultado.TotalReservado);

                if (!resultado.Exito)
                {
                    if (_logger.IsEnabled(LogLevel.Warning))
                    {
                        _logger.LogWarning("Documento MI3 procesado con errores: {Mensaje}", resultado.Mensaje);
                    }

                    return StatusCode(422, resultado);
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Documento MI3 procesado correctamente. Lineas procesadas: {LineasProcesadas}", resultado.LineasProcesadas);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                requestWatch.Stop();
                _logger.LogError(ex, "Error al procesar documento MI3.");
                _logger.LogError(
                    "#MI3_PERF_API_ERROR | Documento={Documento} | Ms={ElapsedMs} | Error={Error}",
                    noDocumento,
                    requestWatch.ElapsedMilliseconds,
                    ex.Message);

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
                                                            [FromQuery] int? idTipoDocumento = null)
        {
            try
            {
                // 1) Traer pendientes
                var data = _salidaService.Get_Salidas_Pendientes_De_Procesar(noPedido, idTipoDocumento);

                // 2) Filtro opcional por NoPedido
                if (!string.IsNullOrWhiteSpace(noPedido))
                {
                    data = [.. data.Where(x => x.No_pedido != null &&
                                           x.No_pedido.Equals(noPedido, StringComparison.OrdinalIgnoreCase))];
                }

                // 3) Filtro opcional por IdTipoDocumento
                if (idTipoDocumento.HasValue)
                {
                    data = [.. data.Where(x => x.IdTipoDocumento == idTipoDocumento.Value)];
                }

                // 4) Minimal por defecto
                var umIds = data.Select(x => x.Idunidadmedida)
                                .Where(id => id > 0)
                                .Distinct()
                                .ToList();

                var presIds = data.Select(x => x.Idpresentacion)
                                  .Where(id => id > 0)
                                  .Distinct()
                                  .ToList();

                var bodegaIds = data.Select(x => x.Idbodega)
                                    .Where(id => id > 0)
                                    .Distinct()
                                    .ToList();

                var pedidoIds = data.Select(x => x.Idpedidoenc)
                                    .Where(id => id > 0)
                                    .Distinct()
                                    .ToList();

                var ums = clsLnUnidad_medida.GetByIds(_configuration, umIds);
                var presList = clsLnProducto_presentacion.GetByIds(_configuration, presIds);
                var bodegas = clsLnBodega.GetByIds(_configuration, bodegaIds);
                var codigoClienteByPedidoId = clsLnTrans_pe_enc.Get_Codigos_Cliente_By_IdsPedidoEnc(_configuration, pedidoIds);
                var usuariosByPedidoId = clsLnTrans_pe_enc.Get_Usuarios_Documento_By_IdsPedidoEnc_Tuple(_configuration, pedidoIds);
                var umById = ums.ToDictionary(u => u.IdUnidadMedida, u => u.Codigo ?? "");
                var presCodigoById = presList.ToDictionary(p => p.IdPresentacion, p => p.Codigo ?? "");
                var bodegaCodigoById = bodegas.ToDictionary(b => b.IdBodega, b => b.Codigo ?? "");

                var result = data.Select(x =>
                {
                    var esTraslado = x.IdTipoDocumento == 6;

                    var codigoBodegaOrigen = bodegaCodigoById.TryGetValue(x.Idbodega, out var codBodOrigen)
                        ? codBodOrigen
                        : "";

                    var codigoDestino = codigoClienteByPedidoId.TryGetValue(x.Idpedidoenc, out var codDestino)
                        ? codDestino
                        : "";

                    var idDocIngresoBodDestino =
                        esTraslado &&
                        x.Iddespachoenc > 0 &&
                        !string.IsNullOrWhiteSpace(codigoBodegaOrigen)
                            ? clsLnTrans_oc_enc.Get_IdOrdenCompraEnc_By_IdDespachoEnc_And_CodigoBodegaOrigen(
                                _configuration,
                                x.Iddespachoenc,
                                codigoBodegaOrigen)
                            : null;

                    string usuarioDocumento = "";
                    string usuarioDespacho = "";

                    // Inicializa siempre con valores seguros
                    usuarioDocumento = string.Empty;
                    usuarioDespacho = string.Empty;

                    if (usuariosByPedidoId.TryGetValue(x.Idpedidoenc, out var usuarios) && usuarios != null)
                    {
                        usuarioDocumento = usuarios.Item1 ?? string.Empty;
                        usuarioDespacho = usuarios.Item2 ?? string.Empty;
                    }


                    return new SalidaSimpleReturnDto
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
                        Fecha = x.Fec_agr,
                        Codigo_Bodega_Origen = codigoBodegaOrigen,
                        Codigo_Bodega_Destino = esTraslado ? codigoDestino : "",
                        Codigo_Cliente = esTraslado ? "" : codigoDestino,
                        IdDocIngresoBodDestino = idDocIngresoBodDestino,
                        IdDocSalidaBodOrigen = x.Idpedidoenc,
                        UsuarioDocumento = usuarioDocumento,
                        UsuarioDespacho = usuarioDespacho
                    };
                }).ToList();

                return Ok(result);

                // Opción para retornar full:
                // return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }

        [HttpPatch("mi3/pendientes-procesar/marcar-enviadas")]
        public IActionResult MarcarSalidasComoEnviadas([FromServices] IConfiguration configuration,
                                                       [FromBody] MarcarTransaccionesEnviadasRequestDto request)
        {
            try
            {
                if (request?.IdTransacciones == null || request.IdTransacciones.Count == 0)
                {
                    return BadRequest(new
                    {
                        ok = false,
                        message = "Debe enviar IdTransacciones (uno o más)."
                    });
                }

                int marcadas = _salidaService.Marcar_Salidas_Como_Enviadas(configuration, request.IdTransacciones);

                return Ok(new
                {
                    ok = true,
                    marcadas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ok = false,
                    message = ex.Message
                });
            }
        }
    }
}
