using AutoMapper;
using System.Data;
using WMS.DALCore.Prefactura;
using WMS.EntityCore.Dtos.Prefactura;

namespace WMSWebAPI.Services.Prefactura
{
    public class PrefacturaService : IPrefacturaService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<PrefacturaService> _logger;

        public PrefacturaService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<PrefacturaService> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PrefacturaPendienteResponseDto> GetPrefacturasPendientesAsync(
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            int? idCliente,
            int pageNumber = 1,
            int pageSize = 50)
        {
            var result = new PrefacturaPendienteResponseDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Success = true
            };

            try
            {
                _logger.LogInformation("Obteniendo prefacturas pendientes - FechaDesde: {FechaDesde}, FechaHasta: {FechaHasta}, IdCliente: {IdCliente}, Pagina: {PageNumber}, PageSize: {PageSize}",
                    fechaDesde, fechaHasta, idCliente, pageNumber, pageSize);

                // Obtener total de registros
                var totalCount = await Task.Run(() => clsLnTrans_prefactura_enc.GetTotalPrefacturasPendientes(
                    _configuration, fechaDesde, fechaHasta, idCliente));
                result.TotalCount = totalCount;
                result.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (totalCount == 0)
                {
                    result.Message = "No se encontraron prefacturas pendientes";
                    return result;
                }

                // Obtener datos paginados
                var dt = await Task.Run(() => clsLnTrans_prefactura_enc.GetPrefacturasPendientes(
                    _configuration, fechaDesde, fechaHasta, idCliente, pageNumber, pageSize));

                // Agrupar por prefactura
                var prefacturasDict = new Dictionary<int, PrefacturaPendienteDto>();

                foreach (DataRow dr in dt.Rows)
                {
                    var idPrefactura = Convert.ToInt32(dr["IdTransPrefacturaEnc"]);

                    if (!prefacturasDict.ContainsKey(idPrefactura))
                    {
                        prefacturasDict[idPrefactura] = new PrefacturaPendienteDto
                        {
                            IdPrefacturaEnc = idPrefactura,
                            Nit = dr["Nit"]?.ToString() ?? "",
                            IdClienteFacturar = Convert.ToInt32(dr["IdClienteFacturar"]),
                            CodigoAcuerdo = Convert.ToInt32(dr["CodigoAcuerdo"]),
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            Moneda = dr["Moneda"]?.ToString() ?? "USD",
                            Periodo = dr["Periodo"]?.ToString() ?? "",
                            Mercaderia = dr["Mercaderia"]?.ToString() ?? "",
                            TipoCambio = Convert.ToDouble(dr["Tipo_Cambio"]),
                            Observaciones = dr["Observacion"]?.ToString() ?? "",
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            FechaDesde = Convert.ToDateTime(dr["FechaDesde"]),
                            FechaHasta = Convert.ToDateTime(dr["FechaHasta"]),
                            Detalle = new List<PrefacturaPendienteDetDto>()
                        };
                    }

                    // Agregar detalle
                    prefacturasDict[idPrefactura].Detalle.Add(new PrefacturaPendienteDetDto
                    {
                        CorrelativoDetalleAcuerdo = Convert.ToInt32(dr["CorrelativoDetalleAcuerdo"]),
                        CodigoProducto = dr["CodigoProducto"]?.ToString() ?? "",
                        Dias = Convert.ToInt32(dr["Dias"]),
                        Monto = Convert.ToDouble(dr["Monto"]),
                        Descripcion = dr["Descripcion"]?.ToString() ?? "",
                        NumeroUnidades = Convert.ToDecimal(dr["NumeroUnidades"])
                    });
                }

                result.Data = prefacturasDict.Values.ToList();
                result.Message = $"Se encontraron {result.Data.Count} prefacturas pendientes";
                result.Success = true;

                _logger.LogInformation("Se obtuvieron {Count} prefacturas pendientes", result.Data.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener prefacturas pendientes");
                result.Success = false;
                result.Message = $"Error al obtener prefacturas pendientes: {ex.Message}";
            }

            return result;
        }

        public async Task<PrefacturaPendienteDto?> GetPrefacturaByIdAsync(int idPrefacturaEnc)
        {
            try
            {
                _logger.LogInformation("Obteniendo prefactura por ID: {IdPrefacturaEnc}", idPrefacturaEnc);

                var dt = await Task.Run(() => clsLnTrans_prefactura_enc.GetPrefacturaById(_configuration, idPrefacturaEnc));

                if (dt.Rows.Count == 0)
                {
                    _logger.LogWarning("No se encontró prefactura con ID: {IdPrefacturaEnc}", idPrefacturaEnc);
                    return null;
                }

                var result = new PrefacturaPendienteDto
                {
                    IdPrefacturaEnc = idPrefacturaEnc,
                    Detalle = new List<PrefacturaPendienteDetDto>()
                };

                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(result.Nit))
                    {
                        result.Nit = dr["Nit"]?.ToString() ?? "";
                        result.IdClienteFacturar = Convert.ToInt32(dr["IdClienteFacturar"]);
                        result.CodigoAcuerdo = Convert.ToInt32(dr["CodigoAcuerdo"]);
                        result.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                        result.Moneda = dr["Moneda"]?.ToString() ?? "USD";
                        result.Periodo = dr["Periodo"]?.ToString() ?? "";
                        result.Mercaderia = dr["Mercaderia"]?.ToString() ?? "";
                        result.TipoCambio = Convert.ToDouble(dr["Tipo_Cambio"]);
                        result.Observaciones = dr["Observacion"]?.ToString() ?? "";
                        result.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                        result.FechaDesde = Convert.ToDateTime(dr["FechaDesde"]);
                        result.FechaHasta = Convert.ToDateTime(dr["FechaHasta"]);
                    }

                    result.Detalle.Add(new PrefacturaPendienteDetDto
                    {
                        CorrelativoDetalleAcuerdo = Convert.ToInt32(dr["CorrelativoDetalleAcuerdo"]),
                        CodigoProducto = dr["CodigoProducto"]?.ToString() ?? "",
                        Dias = Convert.ToInt32(dr["Dias"]),
                        Monto = Convert.ToDouble(dr["Monto"]),
                        Descripcion = dr["Descripcion"]?.ToString() ?? "",
                        NumeroUnidades = Convert.ToDecimal(dr["NumeroUnidades"])
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener prefactura por ID: {IdPrefacturaEnc}", idPrefacturaEnc);
                throw new Exception($"Error al obtener prefactura: {ex.Message}", ex);
            }
        }

        public async Task<MarcarProcesadoResponseDto> MarcarComoProcesadaAsync(int idPrefacturaEnc, string usuarioModificacion = "odoo")
        {
            var result = new MarcarProcesadoResponseDto
            {
                IdPrefacturaEnc = idPrefacturaEnc,
                Success = false
            };

            try
            {
                _logger.LogInformation("Marcando prefactura como procesada - ID: {IdPrefacturaEnc}, Usuario: {Usuario}",
                    idPrefacturaEnc, usuarioModificacion);

                // Verificar si existe
                var existe = await ExistePrefacturaAsync(idPrefacturaEnc);
                if (!existe)
                {
                    result.Message = $"No se encontró la prefactura con ID {idPrefacturaEnc}";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                // Marcar como procesada
                var success = await Task.Run(() => clsLnTrans_prefactura_enc.MarcarComoProcesada(
                    _configuration, idPrefacturaEnc, usuarioModificacion));

                if (success)
                {
                    result.Success = true;
                    result.Message = $"Prefactura {idPrefacturaEnc} marcada como procesada exitosamente";
                    _logger.LogInformation(result.Message);
                }
                else
                {
                    result.Message = $"No se pudo marcar la prefactura {idPrefacturaEnc} como procesada";
                    _logger.LogWarning(result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar prefactura como procesada - ID: {IdPrefacturaEnc}", idPrefacturaEnc);
                result.Message = $"Error al marcar prefactura como procesada: {ex.Message}";
            }

            return result;
        }

        public async Task<bool> ExistePrefacturaAsync(int idPrefacturaEnc)
        {
            try
            {
                var dt = await Task.Run(() => clsLnTrans_prefactura_enc.GetPrefacturaById(_configuration, idPrefacturaEnc));
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de prefactura - ID: {IdPrefacturaEnc}", idPrefacturaEnc);
                return false;
            }
        }

        public async Task<int> GetTotalPrefacturasPendientesAsync(DateTime? fechaDesde, DateTime? fechaHasta, int? idCliente)
        {
            try
            {
                _logger.LogInformation("Obteniendo total de prefacturas pendientes - FechaDesde: {FechaDesde}, FechaHasta: {FechaHasta}, IdCliente: {IdCliente}",
                    fechaDesde, fechaHasta, idCliente);

                return await Task.Run(() => clsLnTrans_prefactura_enc.GetTotalPrefacturasPendientes(
                    _configuration, fechaDesde, fechaHasta, idCliente));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener total de prefacturas pendientes");
                throw new Exception($"Error al obtener total de prefacturas pendientes: {ex.Message}", ex);
            }
        }
    }
}