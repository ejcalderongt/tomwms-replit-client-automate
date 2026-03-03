using Microsoft.Data.SqlClient;
using System.Data;
using WMS.EntityCore.Cambio_Ubicacion;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Dtos.Cambio_Estado;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMSWebAPI.Services.Cambio_Estado
{
    public sealed class CambioEstadoService : ICambioEstadoService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CambioEstadoService> _logger;

        public CambioEstadoService(IConfiguration configuration, ILogger<CambioEstadoService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las transacciones de cambio de estado activas y no sincronizadas (sync_mi3 = 0)
        /// </summary>        

        /// <summary>
        /// Obtiene una transacción específica por su Id
        /// </summary>
        public async Task<clsBeTrans_ubic_hh_enc?> GetByIdAsync(int idTareaUbicacionEnc)
        {
            SqlTransaction? lTransaction = null;

            try
            {
                using var lConnection = new SqlConnection(_configuration.GetConnectionString("CST") ?? _configuration["CST"]);
                await lConnection.OpenAsync();
                lTransaction = (SqlTransaction?)await lConnection.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

                string vSQL = @"
                SELECT * 
                FROM VW_TransUbicacionHhEnc 
                WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc
                    AND cambio_estado = 1 
                    AND Activo = 1";

                using var cmd = new SqlCommand(vSQL, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@IdTareaUbicacionEnc", idTareaUbicacionEnc);

                using var dad = new SqlDataAdapter(cmd);
                var lDataTable = new DataTable();
                dad.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    var obj = new clsBeTrans_ubic_hh_enc();
                    var lRow = lDataTable.Rows[0];
                    CargarEncabezado(ref obj, lRow);

                    if (lRow["DescripcionMotivo"] != DBNull.Value)
                    {
                        obj.DescripcionMotivo = Convert.ToString(lRow["DescripcionMotivo"]) ?? string.Empty;
                    }

                    // Cargar detalles y stock
                    obj.Detalles = await CargarDetallesPorEncabezadoAsync(lConnection, lTransaction!, idTareaUbicacionEnc);
                    obj.StockUbicHH = CargarStockPorEncabezado(lConnection, lTransaction!, idTareaUbicacionEnc);

                    if (lTransaction != null)
                    {
                        await lTransaction.CommitAsync();
                    }
                    return obj;
                }

                if (lTransaction != null)
                {
                    await lTransaction.CommitAsync();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener transacción de cambio de estado por Id: {Id}", idTareaUbicacionEnc);
                lTransaction?.Rollback();
                throw;
            }
            finally
            {
                lTransaction?.Dispose();
            }
        }

        /// <summary>
        /// Marca una transacción como sincronizada (sync_mi3 = 1)
        /// </summary>
        public async Task MarcarComoSincronizadoAsync(int idTareaUbicacionEnc)
        {
            SqlTransaction? lTransaction = null;

            try
            {
                using var lConnection = new SqlConnection(_configuration.GetConnectionString("CST") ?? _configuration["CST"]);
                await lConnection.OpenAsync();
                lTransaction = (SqlTransaction)await lConnection.BeginTransactionAsync();

                string vSQL = @"
                UPDATE TransUbicacionHhEnc 
                SET sync_mi3 = 1,
                    Fec_mod = @Fec_mod,
                    User_mod = @User_mod
                WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc";

                using var cmd = new SqlCommand(vSQL, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@IdTareaUbicacionEnc", idTareaUbicacionEnc);
                cmd.Parameters.AddWithValue("@Fec_mod", DateTime.Now);
                cmd.Parameters.AddWithValue("@User_mod", "SINCRONIZACION_MI3");

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (lTransaction != null)
                {
                    await lTransaction.CommitAsync();
                }

                _logger.LogInformation("Transacción {Id} marcada como sincronizada. Filas afectadas: {RowsAffected}",
                    idTareaUbicacionEnc, rowsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar transacción {Id} como sincronizada", idTareaUbicacionEnc);
                lTransaction?.Rollback();
                throw;
            }
            finally
            {
                lTransaction?.Dispose();
            }
        }

        /// <summary>
        /// Marca múltiples transacciones como sincronizadas
        /// </summary>
        public async Task MarcarMultiplesComoSincronizadosAsync(List<int> idsTareaUbicacionEnc)
        {
            if (idsTareaUbicacionEnc == null || idsTareaUbicacionEnc.Count == 0)
                return;

            SqlTransaction? lTransaction = null;

            try
            {
                using var lConnection = new SqlConnection(_configuration.GetConnectionString("CST") ?? _configuration["CST"]);
                await lConnection.OpenAsync();
                lTransaction = (SqlTransaction?)await lConnection.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

                var paramNames = idsTareaUbicacionEnc.Select((_, i) => $"@p{i}").ToList();

                string vSQL = $@"
                UPDATE TransUbicacionHhEnc 
                SET sync_mi3 = 1,
                    Fec_mod = @Fec_mod,
                    User_mod = @User_mod
                WHERE IdTareaUbicacionEnc IN ({string.Join(",", paramNames)})";

                using var cmd = new SqlCommand(vSQL, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };

                for (int i = 0; i < idsTareaUbicacionEnc.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int)
                    {
                        Value = idsTareaUbicacionEnc[i]
                    });
                }

                cmd.Parameters.AddWithValue("@Fec_mod", DateTime.Now);
                cmd.Parameters.AddWithValue("@User_mod", "SINCRONIZACION_MI3");

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (lTransaction != null)
                    await lTransaction.CommitAsync();

                _logger.LogInformation("{Count} transacciones marcadas como sincronizadas. Filas afectadas: {RowsAffected}",
                    idsTareaUbicacionEnc.Count, rowsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar múltiples transacciones como sincronizadas");
                lTransaction?.Rollback();
                throw;
            }
            finally
            {
                lTransaction?.Dispose();
            }
        }

        #region Métodos Privados de Carga


        private List<clsBeTrans_ubic_hh_stock> CargarStockPorEncabezado(
            SqlConnection connection,
            SqlTransaction transaction,
            int idEncabezado)
        {
            var resultado = new List<clsBeTrans_ubic_hh_stock>();

            string sql = @"
            SELECT * 
            FROM trans_ubic_hh_stock 
            WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc";

            using var cmd = new SqlCommand(sql, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@IdTareaUbicacionEnc", idEncabezado);

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var stock = new clsBeTrans_ubic_hh_stock();
                CargarStock(ref stock, dr);
                resultado.Add(stock);
            }

            return resultado;
        }

        #endregion

        #region Métodos de Carga de DataRows (Asume que existen en la capa correspondiente)

        private void CargarEncabezado(ref clsBeTrans_ubic_hh_enc obj, DataRow row)
        {
            obj.IdTareaUbicacionEnc = Convert.ToInt32(row["IdTareaUbicacionEnc"]);
            obj.IdPropietarioBodega = Convert.ToInt32(row["IdPropietarioBodega"]);
            obj.IdMotivoUbicacion = Convert.ToInt32(row["IdMotivoUbicacion"]);
            obj.DescripcionMotivo = row["DescripcionMotivo"] == DBNull.Value ? string.Empty : Convert.ToString(row["DescripcionMotivo"]) ?? string.Empty;
            obj.FechaInicio = Convert.ToDateTime(row["FechaInicio"]);
            obj.HoraInicio = Convert.ToDateTime(row["HoraInicio"]);
            obj.FechaFin = row["FechaFin"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(row["FechaFin"]);
            obj.HoraFin = row["HoraFin"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["HoraFin"]);
            obj.User_agr = row["user_agr"]?.ToString() ?? "";
            obj.Fec_agr = Convert.ToDateTime(row["fec_agr"]);
            obj.User_mod = row["user_mod"]?.ToString() ?? "";
            obj.Fec_mod = row["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["fec_mod"]);
            obj.Observacion = row["Observacion"]?.ToString() ?? "";
            obj.Activo = Convert.ToBoolean(row["activo"]);
            obj.Operador_por_linea = Convert.ToBoolean(row["operador_por_linea"]);
            obj.Ubicacion_con_hh = Convert.ToBoolean(row["ubicacion_con_hh"]);
            obj.Estado = row["estado"]?.ToString() ?? "";
            obj.Cambio_estado = Convert.ToBoolean(row["cambio_estado"]);
            obj.IdReabastecimientoLog = row["IdReabastecimientoLog"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdReabastecimientoLog"]);
            obj.Es_Traslado_SAP = row["es_traslado_sap"] != DBNull.Value && Convert.ToBoolean(row["es_traslado_sap"]);
            obj.No_Documento = row["no_documento"]?.ToString() ?? "";
            obj.Usuario = row["Usuario"]?.ToString() ?? "";
            obj.Rol = row["Rol"]?.ToString() ?? "";
            obj.IdBodega = row["IdBodega"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdBodega"]);
            obj.CodigoBodega= row["CodigoBodega"]?.ToString() ?? "";
            // Estos campos no están en la vista, se inicializan en 0 o vacío
            obj.IdPrioridad = 0;
            obj.IdTipoTarea = 0;            
            obj.Asunto = string.Empty;
            obj.Nombre_Operador = string.Empty;
        }
        private void CargarDetalle(ref clsBeTrans_ubic_hh_det obj, DataRow row)
        {
            // Datos base del detalle
            obj.IdTareaUbicacionEnc = Convert.ToInt32(row["IdTareaUbicacionEnc"]);
            obj.IdTareaUbicacionDet = Convert.ToInt32(row["IdTareaUbicacionDet"]);
            obj.IdStock = row["IdStock"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdStock"]);
            obj.IdUbicacionOrigen = row["IdUbicacionOrigen"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdUbicacionOrigen"]);
            obj.IdUbicacionDestino = row["IdUbicacionDestino"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdUbicacionDestino"]);
            obj.IdEstadoOrigen = row["IdEstadoOrigen"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdEstadoOrigen"]);
            obj.IdEstadoDestino = row["IdEstadoDestino"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdEstadoDestino"]);
            obj.IdOperadorBodega = row["IdOperadorBodega"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdOperadorBodega"]);
            obj.HoraInicio = row["HoraInicio"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["HoraInicio"]);
            obj.HoraFin = row["HoraFin"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["HoraFin"]);
            obj.Realizado = row["Realizado"] != DBNull.Value && Convert.ToBoolean(row["Realizado"]);
            obj.Cantidad = row["Cantidad"] == DBNull.Value ? 0.0 : Convert.ToDouble(row["Cantidad"]);
            obj.Activo = row["Activo"] != DBNull.Value && Convert.ToBoolean(row["Activo"]);
            obj.Recibido = row["Recibido"] == DBNull.Value ? 0.0 : Convert.ToDouble(row["Recibido"]);
            obj.Estado = row["Estado"]?.ToString() ?? "";            
            obj.IdBodega = row["IdBodega"] == DBNull.Value ? 0 : Convert.ToInt32(row["IdBodega"]);
            obj.No_Linea = row["No_Linea"] == DBNull.Value ? 0 : Convert.ToInt32(row["No_Linea"]);

            // Asignar nombres de ubicaciones directamente desde la vista
            obj.NombreUbicacionOrigen = row["NombreCompletoUbicaiconOrigen"] == DBNull.Value
                ? string.Empty
                : Convert.ToString(row["NombreCompletoUbicaiconOrigen"]) ?? string.Empty;

            obj.NombreUbicacionDestino = row["NombreCompletoUbicaiconDestino"] == DBNull.Value
                ? string.Empty
                : Convert.ToString(row["NombreCompletoUbicaiconDestino"]) ?? string.Empty;

            // Datos de producto (ya vienen en la vista)
            if (row["codigo"] != DBNull.Value)
            {
                obj.CodigoProducto = Convert.ToString(row["codigo"]) ?? string.Empty;
            }

            if (row["nombre"] != DBNull.Value)
            {
                obj.NombreProducto = Convert.ToString(row["nombre"]) ?? string.Empty;
            }

            // Datos de stock (ya vienen en la vista)
            if (row["lic_plate"] != DBNull.Value)
            {
                obj.Licencia = Convert.ToString(row["lic_plate"]) ?? string.Empty;
            }

            if (row["lote"] != DBNull.Value)
            {
                obj.Lote = Convert.ToString(row["lote"]) ?? string.Empty;
            }

            if (row["fecha_vence"] != DBNull.Value)
            {
                obj.FechaVence = DateOnly.FromDateTime(Convert.ToDateTime(row["fecha_vence"]));
            }            

            if (row["fecha_ingreso"] != DBNull.Value)
            {
                obj.FechaIngreso = Convert.ToDateTime(row["fecha_ingreso"]);
            }

            if (row["serial"] != DBNull.Value)
            {
                obj.Serial = Convert.ToString(row["serial"]) ?? string.Empty;
            }

            // Unidad de Medida y Presentación
            if (row["UnidadMedida"] != DBNull.Value)
            {
                obj.UnidadMedida = Convert.ToString(row["UnidadMedida"]) ?? string.Empty;
            }

            if (row["Presentacion"] != DBNull.Value)
            {
                obj.Presentacion = Convert.ToString(row["Presentacion"]) ?? string.Empty;
            }

            // Estados (nombres)
            if (row["NomEstado_Origen"] != DBNull.Value)
            {
                obj.EstadoOrigen = Convert.ToString(row["NomEstado_Origen"]) ?? string.Empty;
            }

            // El estado destino podría venir de otra columna si existe en la vista
            // Por ahora usamos el mismo, ajustar según la vista real
            if (row["NomEstado_Destino"] != DBNull.Value)
            {
                obj.EstadoDestino = Convert.ToString(row["NomEstado_Destino"]) ?? string.Empty;
            }

            // Operador
            if (row["nombres"] != DBNull.Value)
            {
                obj.NombreOperador = Convert.ToString(row["nombres"]) ?? string.Empty;
            }
          
        }
        public static void CargarStock(ref clsBeTrans_ubic_hh_stock oBeTrans_ubic_hh_stock, DataRow dr)
        {
            try
            {
                oBeTrans_ubic_hh_stock.IdStockTransUbicHHDet = dr["IdStockTransUbicHHDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdStockTransUbicHHDet"]);
                oBeTrans_ubic_hh_stock.IdTareaUbicacionEnc = dr["IdTareaUbicacionEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTareaUbicacionEnc"]);
                oBeTrans_ubic_hh_stock.IdTareaUbicacionDet = dr["IdTareaUbicacionDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTareaUbicacionDet"]);
                oBeTrans_ubic_hh_stock.IdStock = dr["IdStock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdStock"]);
                oBeTrans_ubic_hh_stock.IdPropietarioBodega = dr["IdPropietarioBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPropietarioBodega"]);
                oBeTrans_ubic_hh_stock.IdProductoBodega = dr["IdProductoBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoBodega"]);
                oBeTrans_ubic_hh_stock.IdProductoEstado = dr["IdProductoEstado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoEstado"]);
                oBeTrans_ubic_hh_stock.IdPresentacion = dr["IdPresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPresentacion"]);
                oBeTrans_ubic_hh_stock.IdUnidadMedida = dr["IdUnidadMedida"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUnidadMedida"]);
                oBeTrans_ubic_hh_stock.IdUbicacion = dr["IdUbicacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacion"]);
                oBeTrans_ubic_hh_stock.IdUbicacion_anterior = dr["IdUbicacion_anterior"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacion_anterior"]);
                oBeTrans_ubic_hh_stock.IdRecepcionEnc = dr["IdRecepcionEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionEnc"]);
                oBeTrans_ubic_hh_stock.IdRecepcionDet = dr["IdRecepcionDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionDet"]);
                oBeTrans_ubic_hh_stock.IdPedidoEnc = dr["IdPedidoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPedidoEnc"]);
                oBeTrans_ubic_hh_stock.IdPickingEnc = dr["IdPickingEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPickingEnc"]);
                oBeTrans_ubic_hh_stock.IdDespachoEnc = dr["IdDespachoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdDespachoEnc"]);
                oBeTrans_ubic_hh_stock.Lote = dr["lote"] == DBNull.Value ? string.Empty : Convert.ToString(dr["lote"]) ?? string.Empty;
                oBeTrans_ubic_hh_stock.Lic_plate = dr["lic_plate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["lic_plate"]) ?? string.Empty;
                oBeTrans_ubic_hh_stock.Serial = dr["serial"] == DBNull.Value ? string.Empty : Convert.ToString(dr["serial"]) ?? string.Empty;
                oBeTrans_ubic_hh_stock.Cantidad = dr["cantidad"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad"]);
                oBeTrans_ubic_hh_stock.Fecha_ingreso =dr["fecha_ingreso"] == DBNull.Value? new DateOnly(1900, 1, 1): DateOnly.FromDateTime(Convert.ToDateTime(dr["fecha_ingreso"]));
                oBeTrans_ubic_hh_stock.Fecha_vence =dr["fecha_vence"] == DBNull.Value? DateOnly.FromDateTime(DateTime.Now): DateOnly.FromDateTime(Convert.ToDateTime(dr["fecha_vence"]));
                oBeTrans_ubic_hh_stock.Uds_lic_plate = dr["uds_lic_plate"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["uds_lic_plate"]);
                oBeTrans_ubic_hh_stock.No_bulto = dr["no_bulto"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no_bulto"]);
                oBeTrans_ubic_hh_stock.Fecha_manufactura =dr["fecha_manufactura"] == DBNull.Value? DateOnly.FromDateTime(DateTime.Now): DateOnly.FromDateTime(Convert.ToDateTime(dr["fecha_manufactura"]));
                oBeTrans_ubic_hh_stock.añada = dr["añada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["añada"]);
                oBeTrans_ubic_hh_stock.User_agr = dr["user_agr"] == DBNull.Value ? string.Empty : Convert.ToString(dr["user_agr"]) ?? string.Empty;
                oBeTrans_ubic_hh_stock.Fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);
                oBeTrans_ubic_hh_stock.User_mod = dr["user_mod"] == DBNull.Value ? string.Empty : Convert.ToString(dr["user_mod"]) ?? string.Empty;
                oBeTrans_ubic_hh_stock.Fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_mod"]);
                oBeTrans_ubic_hh_stock.Activo = dr["activo"] != DBNull.Value && Convert.ToBoolean(dr["activo"]);
                oBeTrans_ubic_hh_stock.Peso = dr["peso"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso"]);
                oBeTrans_ubic_hh_stock.Temperatura = dr["temperatura"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["temperatura"]);
                oBeTrans_ubic_hh_stock.Fecha_mov_hist = dr["fecha_mov_hist"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_mov_hist"]);
                oBeTrans_ubic_hh_stock.Atributo_variante_1 = dr["atributo_variante_1"] == DBNull.Value ? string.Empty : Convert.ToString(dr["atributo_variante_1"]) ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion        

        private Task<List<clsBeTrans_ubic_hh_det>> CargarDetallesPorEncabezadoAsync(SqlConnection connection,SqlTransaction transaction,int idEncabezado)
        {
            var resultado = new List<clsBeTrans_ubic_hh_det>();

            string sql = @"SELECT * 
                           FROM trans_ubic_hh_det 
                           WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc
                           ORDER BY IdTareaUbicacionDet";

            using var cmd = new SqlCommand(sql, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@IdTareaUbicacionEnc", idEncabezado);

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var detalle = new clsBeTrans_ubic_hh_det();
                CargarDetalle(ref detalle, dr);
                resultado.Add(detalle);
            }

            return Task.FromResult(resultado);
        }

        public async Task<List<CambioEstadoEncabezadoDto>> GetAllPendientesSincronizacionSimplificadoAsync()
        {
            var lReturnList = new List<CambioEstadoEncabezadoDto>();
            SqlTransaction? lTransaction = null;

            try
            {
                using var lConnection = new SqlConnection(_configuration.GetConnectionString("CST") ?? _configuration["CST"]);
                await lConnection.OpenAsync();
                lTransaction = (SqlTransaction?)await lConnection.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

                // 1) Encabezados
                const string vSQL = @"SELECT * 
                              FROM VW_TransUbicacionHhEnc 
                              WHERE cambio_estado = 1 
                                AND Activo = 1
                                AND (sync_mi3 = 0 OR sync_mi3 IS NULL)";

                var cmd = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text };
                var dad = new SqlDataAdapter(cmd);
                var lDataTable = new DataTable();
                dad.Fill(lDataTable);

                if (lDataTable.Rows.Count == 0)
                {
                    if (lTransaction!= null)
                        await lTransaction.CommitAsync();
                    return lReturnList;
                }

                var idsEncabezados = new List<int>(lDataTable.Rows.Count);
                var encabezadosDict = new Dictionary<int, clsBeTrans_ubic_hh_enc>(lDataTable.Rows.Count);

                foreach (DataRow lRow in lDataTable.Rows)
                {
                    var obj = new clsBeTrans_ubic_hh_enc();
                    CargarEncabezado(ref obj, lRow);
                    obj.DescripcionMotivo = lRow["DescripcionMotivo"] == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(lRow["DescripcionMotivo"]) ?? string.Empty;

                    idsEncabezados.Add(obj.IdTareaUbicacionEnc);
                    encabezadosDict[obj.IdTareaUbicacionEnc] = obj;
                }

                // 2) Detalles
                var todosDetalles = await CargarDetallesPorEncabezadosAsync(lConnection, lTransaction!, idsEncabezados);
                
                var todoStock = await CargarStockPorEncabezadosAsync(lConnection, lTransaction!, idsEncabezados);

                foreach (var listaDetalles in todosDetalles.Values)
                {
                    if (listaDetalles is null) continue;

                    foreach (var detalle in listaDetalles)
                    {
                        if (detalle is null) continue;

                        if (!todoStock.TryGetValue(detalle.IdTareaUbicacionEnc, out var stocks) || stocks is null)
                            continue;

                        var idStock = detalle.IdStock;
                        if (idStock <= 0) continue;

                        var encontrado = stocks.FirstOrDefault(s => s is not null && s.IdStock == idStock);
                        if (encontrado is null) continue;

                        detalle.Stock = encontrado;
                    }
                }
                
                lReturnList = idsEncabezados
                    .Distinct()
                    .Select(idEnc =>
                    {
                        if (!encabezadosDict.TryGetValue(idEnc, out var encabezado))
                            return null;

                        var dto = new CambioEstadoEncabezadoDto
                        {
                            IdTareaUbicacionEnc = encabezado.IdTareaUbicacionEnc,
                            Fecha = encabezado.FechaInicio.Date,
                            Usuario = encabezado.Usuario,
                            Bodega = encabezado.CodigoBodega ?? encabezado.IdBodega.ToString(),
                            Detalles = new List<CambioEstadoDetalleDto>()
                        };

                        if (!todosDetalles.TryGetValue(idEnc, out var detalles) || detalles == null)
                            return dto;

                        foreach (var detalle in detalles)
                        {
                            dto.Detalles.Add(new CambioEstadoDetalleDto
                            {
                                CodigoProducto = detalle.CodigoProducto ?? string.Empty,
                                NombreProducto = detalle.NombreProducto ?? string.Empty,

                                // ahora salen directo del stock (ya viene con nombre/código desde tu view o loader)
                                UMBas = detalle.UnidadMedida ?? detalle.UnidadMedida ?? string.Empty,
                                Presentacion = detalle.Presentacion ?? detalle.Presentacion ?? string.Empty,

                                Cantidad = detalle.Cantidad,

                                Lote = detalle.Stock?.Lote ?? string.Empty,
                                Licencia = detalle.Stock?.Lic_plate ?? string.Empty,
                                FechaVence = detalle.Stock?.Fecha_vence,
                                FechaProduccion = detalle.Stock?.Fecha_manufactura,
                                FechaIngreso = detalle.Stock?.Fecha_ingreso,    

                                // estados ya resueltos en detalle
                                EstadoOrigen = detalle.EstadoOrigen ?? string.Empty,
                                EstadoDestino = detalle.EstadoDestino ?? string.Empty,

                                UbicacionOrigen = detalle.NombreUbicacionOrigen
                                                  ?? detalle.UbicacionOrigen?.Descripcion
                                                  ?? detalle.IdUbicacionOrigen.ToString(),
                                UbicacionDestino = detalle.NombreUbicacionDestino
                                                   ?? detalle.UbicacionDestino?.Descripcion
                                                   ?? detalle.IdUbicacionDestino.ToString()
                            });
                        }

                        return dto;
                    })
                    .Where(x => x != null)
                    .ToList()!;

                if (lTransaction !=null)
                    await lTransaction.CommitAsync();
                _logger.LogInformation("Se obtuvieron {Count} transacciones de cambio de estado pendientes de sincronización", lReturnList.Count);
                return lReturnList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener transacciones de cambio de estado pendientes");
                lTransaction?.Rollback();
                throw;
            }
            finally
            {
                lTransaction?.Dispose();
            }
        }
        private Task<Dictionary<int, List<clsBeTrans_ubic_hh_stock>>> CargarStockPorEncabezadosAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        List<int> idsEncabezados)
        {
            var resultado = new Dictionary<int, List<clsBeTrans_ubic_hh_stock>>();

            if (idsEncabezados == null || idsEncabezados.Count == 0)
                return Task.FromResult(resultado);

            var paramNames = idsEncabezados.Select((_, i) => $"@p{i}").ToList();

            string sql = $@"
            SELECT * 
            FROM trans_ubic_hh_stock 
            WHERE IdTareaUbicacionEnc IN ({string.Join(",", paramNames)})";

            var cmd = new SqlCommand(sql, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            for (int i = 0; i < idsEncabezados.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int)
                {
                    Value = idsEncabezados[i]
                });
            }

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var stock = new clsBeTrans_ubic_hh_stock();
                CargarStock(ref stock, dr);

                var idEnc = Convert.ToInt32(dr["IdTareaUbicacionEnc"]);

                if (!resultado.ContainsKey(idEnc))
                {
                    resultado[idEnc] = new List<clsBeTrans_ubic_hh_stock>();
                }

                resultado[idEnc].Add(stock);
            }

            return Task.FromResult(resultado);
        }

        private Task<Dictionary<int, List<clsBeTrans_ubic_hh_det>>> CargarDetallesPorEncabezadosAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            List<int> idsEncabezados)
        {
            var resultado = new Dictionary<int, List<clsBeTrans_ubic_hh_det>>();

            if (idsEncabezados == null || idsEncabezados.Count == 0)
                return Task.FromResult(resultado);

            var paramNames = idsEncabezados.Select((_, i) => $"@p{i}").ToList();
            string sql = $@"
                        SELECT * 
                        FROM VW_TransUbicHhDet 
                        WHERE IdTareaUbicacionEnc IN ({string.Join(",", paramNames)})
                        ORDER BY IdTareaUbicacionEnc, Tramo, Indice_x, Nivel";

            var cmd = new SqlCommand(sql, connection, transaction) { CommandType = CommandType.Text };

            for (int i = 0; i < idsEncabezados.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int) { Value = idsEncabezados[i] });
            }

            SqlDataAdapter dad = new SqlDataAdapter(cmd);            
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var detalle = new clsBeTrans_ubic_hh_det();
                CargarDetalle(ref detalle, dr);

                var idEnc = Convert.ToInt32(dr["IdTareaUbicacionEnc"]);

                if (!resultado.ContainsKey(idEnc))
                    resultado[idEnc] = new List<clsBeTrans_ubic_hh_det>();

                resultado[idEnc].Add(detalle);
            }            

            return Task.FromResult(resultado);
        }
    }
}