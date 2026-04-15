using Microsoft.Data.SqlClient;
using WMS.EntityCore.Prefactura;
using Microsoft.Extensions.Configuration;

namespace WMS.DALCore.Prefactura
{
    public class clsLnTrans_prefactura_enc
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

        #region Métodos Base (Insertar, Actualizar, Eliminar, etc.)

        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT ISNULL(Max(IdTransPrefacturaEnc),0) FROM trans_prefactura_enc";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en MaxID: {ex.Message}", ex);
            }
        }

        public static int Insertar(clsBeTrans_prefactura_enc oBePrefactura, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("trans_prefactura_enc");
                Ins.Add("IdTransPrefacturaEnc", "@IdTransPrefacturaEnc", "F");
                Ins.Add("IdPropietarioBodega", "@IdPropietarioBodega", "F");
                Ins.Add("IdClienteBodega", "@IdClienteBodega", "F");
                Ins.Add("IdBodega", "@IdBodega", "F");
                Ins.Add("Fecha", "@Fecha", "F");
                Ins.Add("Fecha_desde", "@Fecha_desde", "F");
                Ins.Add("Fecha_hasta", "@Fecha_hasta", "F");
                Ins.Add("Tipo_Cambio", "@Tipo_Cambio", "F");
                Ins.Add("Observacion", "@Observacion", "F");
                Ins.Add("procesado_erp", "@procesado_erp", "F");
                Ins.Add("anulada", "@anulada", "F");
                Ins.Add("es_consolidador", "@es_consolidador", "F");
                Ins.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", "F");
                Ins.Add("poliza_pe_numero_orden", "@poliza_pe_numero_orden", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("IdTipoCuenta", "@IdTipoCuenta", "F");

                var sp = Ins.SQL();
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", oBePrefactura.IdTransPrefacturaEnc);
                cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBePrefactura.IdPropietarioBodega);
                cmd.Parameters.AddWithValue("@IdClienteBodega", oBePrefactura.IdClienteBodega);
                cmd.Parameters.AddWithValue("@IdBodega", oBePrefactura.IdBodega);
                cmd.Parameters.AddWithValue("@Fecha", oBePrefactura.Fecha);
                cmd.Parameters.AddWithValue("@Fecha_desde", oBePrefactura.Fecha_desde);
                cmd.Parameters.AddWithValue("@Fecha_hasta", oBePrefactura.Fecha_hasta);
                cmd.Parameters.AddWithValue("@Tipo_Cambio", oBePrefactura.Tipo_Cambio);
                cmd.Parameters.AddWithValue("@Observacion", oBePrefactura.Observacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@procesado_erp", oBePrefactura.procesado_erp);
                cmd.Parameters.AddWithValue("@anulada", oBePrefactura.anulada);
                cmd.Parameters.AddWithValue("@es_consolidador", oBePrefactura.es_consolidador);
                cmd.Parameters.AddWithValue("@poliza_oc_numero_orden", oBePrefactura.poliza_oc_numero_orden ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@poliza_pe_numero_orden", oBePrefactura.poliza_pe_numero_orden ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_agr", oBePrefactura.User_agr ?? "");
                cmd.Parameters.AddWithValue("@fec_agr", oBePrefactura.Fec_agr);
                cmd.Parameters.AddWithValue("@user_mod", oBePrefactura.User_mod ?? "");
                cmd.Parameters.AddWithValue("@fec_mod", oBePrefactura.Fec_mod);
                cmd.Parameters.AddWithValue("@IdTipoCuenta", oBePrefactura.IdTipoCuenta);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Insertar: {ex.Message}", ex);
            }
        }

        public static int Actualizar(clsBeTrans_prefactura_enc oBePrefactura, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("trans_prefactura_enc");
                Upd.Add("IdPropietarioBodega", "@IdPropietarioBodega", "F");
                Upd.Add("IdClienteBodega", "@IdClienteBodega", "F");
                Upd.Add("IdBodega", "@IdBodega", "F");
                Upd.Add("Fecha", "@Fecha", "F");
                Upd.Add("Fecha_desde", "@Fecha_desde", "F");
                Upd.Add("Fecha_hasta", "@Fecha_hasta", "F");
                Upd.Add("Tipo_Cambio", "@Tipo_Cambio", "F");
                Upd.Add("Observacion", "@Observacion", "F");
                Upd.Add("procesado_erp", "@procesado_erp", "F");
                Upd.Add("anulada", "@anulada", "F");
                Upd.Add("es_consolidador", "@es_consolidador", "F");
                Upd.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", "F");
                Upd.Add("poliza_pe_numero_orden", "@poliza_pe_numero_orden", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("IdTipoCuenta", "@IdTipoCuenta", "F");
                Upd.Where("IdTransPrefacturaEnc = @IdTransPrefacturaEnc");

                var sp = Upd.SQL();
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", oBePrefactura.IdTransPrefacturaEnc);
                cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBePrefactura.IdPropietarioBodega);
                cmd.Parameters.AddWithValue("@IdClienteBodega", oBePrefactura.IdClienteBodega);
                cmd.Parameters.AddWithValue("@IdBodega", oBePrefactura.IdBodega);
                cmd.Parameters.AddWithValue("@Fecha", oBePrefactura.Fecha);
                cmd.Parameters.AddWithValue("@Fecha_desde", oBePrefactura.Fecha_desde);
                cmd.Parameters.AddWithValue("@Fecha_hasta", oBePrefactura.Fecha_hasta);
                cmd.Parameters.AddWithValue("@Tipo_Cambio", oBePrefactura.Tipo_Cambio);
                cmd.Parameters.AddWithValue("@Observacion", oBePrefactura.Observacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@procesado_erp", oBePrefactura.procesado_erp);
                cmd.Parameters.AddWithValue("@anulada", oBePrefactura.anulada);
                cmd.Parameters.AddWithValue("@es_consolidador", oBePrefactura.es_consolidador);
                cmd.Parameters.AddWithValue("@poliza_oc_numero_orden", oBePrefactura.poliza_oc_numero_orden ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@poliza_pe_numero_orden", oBePrefactura.poliza_pe_numero_orden ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_mod", oBePrefactura.User_mod ?? "");
                cmd.Parameters.AddWithValue("@fec_mod", oBePrefactura.Fec_mod);
                cmd.Parameters.AddWithValue("@IdTipoCuenta", oBePrefactura.IdTipoCuenta);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Actualizar: {ex.Message}", ex);
            }
        }

        private static string GetStringValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? string.Empty : (dr[columnName]?.ToString() ?? string.Empty);
        }

        private static int GetIntValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? 0 : Convert.ToInt32(dr[columnName]);
        }

        private static double GetDoubleValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? 0 : Convert.ToDouble(dr[columnName]);
        }

        private static DateTime GetDateTimeValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr[columnName]);
        }

        private static bool GetBoolValue(DataRow dr, string columnName)
        {
            return dr[columnName] != DBNull.Value && Convert.ToBoolean(dr[columnName]);
        }

        public static void Cargar(ref clsBeTrans_prefactura_enc oBePrefactura, DataRow dr)
        {
            try
            {
                oBePrefactura.IdTransPrefacturaEnc = GetIntValue(dr, "IdTransPrefacturaEnc");
                oBePrefactura.IdPropietarioBodega = GetIntValue(dr, "IdPropietarioBodega");
                oBePrefactura.IdClienteBodega = GetIntValue(dr, "IdClienteBodega");
                oBePrefactura.IdBodega = GetIntValue(dr, "IdBodega");
                oBePrefactura.Fecha = GetDateTimeValue(dr, "Fecha");
                oBePrefactura.Fecha_desde = GetDateTimeValue(dr, "Fecha_desde");
                oBePrefactura.Fecha_hasta = GetDateTimeValue(dr, "Fecha_hasta");
                oBePrefactura.Tipo_Cambio = GetDoubleValue(dr, "Tipo_Cambio");
                oBePrefactura.Observacion = GetStringValue(dr, "Observacion");
                oBePrefactura.procesado_erp = GetBoolValue(dr, "procesado_erp");
                oBePrefactura.anulada = GetBoolValue(dr, "anulada");
                oBePrefactura.es_consolidador = GetBoolValue(dr, "es_consolidador");
                oBePrefactura.poliza_oc_numero_orden = GetStringValue(dr, "poliza_oc_numero_orden");
                oBePrefactura.poliza_pe_numero_orden = GetStringValue(dr, "poliza_pe_numero_orden");
                oBePrefactura.User_agr = GetStringValue(dr, "user_agr");
                oBePrefactura.Fec_agr = GetDateTimeValue(dr, "fec_agr");
                oBePrefactura.User_mod = GetStringValue(dr, "user_mod");
                oBePrefactura.Fec_mod = GetDateTimeValue(dr, "fec_mod");
                oBePrefactura.IdTipoCuenta = GetIntValue(dr, "IdTipoCuenta");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Cargar: {ex.Message}", ex);
            }
        }

        #endregion

        #region Métodos para Odoo (GetPrefacturasPendientes, MarcarComoProcesada, etc.)

        /// <summary>
        /// Obtiene prefacturas pendientes de enviar a ERP (procesado_erp = 0 y anulada = 0)
        /// </summary>
        public static DataTable GetPrefacturasPendientes(IConfiguration config, DateTime? fechaDesde, DateTime? fechaHasta, int? idCliente, int pageNumber = 1, int pageSize = 50)
        {
            var lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                var offset = (pageNumber - 1) * pageSize;

                var sp = @"
                    SELECT DISTINCT 
                        enc.IdTransPrefacturaEnc,
                        enc.fec_agr as FechaCreacion,
                        enc.fecha_desde as FechaDesde,
                        enc.fecha_hasta as FechaHasta,
                        enc.Tipo_Cambio,
                        enc.Observacion,
                        enc.procesado_erp,
                        enc.anulada,
                        CASE WHEN enc.es_consolidador = 1 
                             THEN 'TO ' + enc.poliza_oc_numero_orden + ' - ID ' + enc.poliza_pe_numero_orden 
                             ELSE ISNULL(acuerdo.descripcion, '')
                        END as Mercaderia,
                        'del ' + CONVERT(varchar(25), enc.fecha_desde, 105) + ' al ' + CONVERT(varchar(25), enc.fecha_hasta, 105) as Periodo,
                        acuerdo.codigo_acuerdo as CodigoAcuerdo,
                        ISNULL(acuerdo.moneda, 'USD') as Moneda,
                        CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pr.NIT, '') ELSE ISNULL(pr2.NIT, '') END as Nit,
                        CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pr.IdPropietario, 0) ELSE ISNULL(pr2.IdPropietario, 0) END as IdClienteFacturar,
                        CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pb.IdPropietario, 0) ELSE ISNULL(cl.IdPropietario, 0) END as IdCliente,
                        det.codigo_producto_acuerdo_det as CodigoProducto,
                        det.correlativo_detalle_acuerdo as CorrelativoDetalleAcuerdo,
                        det.dias_eventos as Dias,
                        det.monto_erp as Monto,
                        ISNULL(det.descripcion, '') as Descripcion,
                        det.numero_unidades_acuerdo_det as NumeroUnidades
                    FROM trans_prefactura_enc enc
                    INNER JOIN trans_prefactura_det det ON enc.IdTransPrefacturaEnc = det.IdTransPrefacturaEnc
                    INNER JOIN trans_acuerdoscomerciales_enc acuerdo ON det.IdAcuerdoEnc = acuerdo.IdAcuerdoEnc
                    INNER JOIN propietario_bodega pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega
                    INNER JOIN propietarios pr ON pb.IdPropietario = pr.IdPropietario
                    LEFT JOIN cliente_bodega cb ON enc.IdClienteBodega = cb.IdClienteBodega
                    LEFT JOIN cliente cl ON cb.IdCliente = cl.IdCliente
                    LEFT JOIN propietarios pr2 ON cl.IdPropietario = pr2.IdPropietario
                    WHERE enc.procesado_erp = 0 AND enc.anulada = 0";

                var condiciones = new List<string>();

                if (fechaDesde.HasValue)
                    condiciones.Add($"enc.fecha_desde >= '{fechaDesde.Value:yyyy-MM-dd}'");

                if (fechaHasta.HasValue)
                    condiciones.Add($"enc.fecha_hasta <= '{fechaHasta.Value:yyyy-MM-dd}'");

                if (idCliente.HasValue && idCliente.Value > 0)
                    condiciones.Add($"(CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN pb.IdPropietario ELSE cl.IdPropietario END) = {idCliente.Value}");

                if (condiciones.Any())
                    sp += " AND " + string.Join(" AND ", condiciones);

                sp += $" ORDER BY enc.IdTransPrefacturaEnc OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                throw new Exception($"Error en GetPrefacturasPendientes: {ex.Message}", ex);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
                lTransaction?.Dispose();
            }
        }

        /// <summary>
        /// Obtiene el total de prefacturas pendientes (para paginación)
        /// </summary>
        public static int GetTotalPrefacturasPendientes(IConfiguration config, DateTime? fechaDesde, DateTime? fechaHasta, int? idCliente)
        {
            var lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                var sp = @"SELECT COUNT(DISTINCT enc.IdTransPrefacturaEnc)
                           FROM trans_prefactura_enc enc
                           INNER JOIN propietario_bodega pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega
                           LEFT JOIN cliente_bodega cb ON enc.IdClienteBodega = cb.IdClienteBodega
                           LEFT JOIN cliente cl ON cb.IdCliente = cl.IdCliente
                           WHERE enc.procesado_erp = 0 AND enc.anulada = 0";

                var condiciones = new List<string>();

                if (fechaDesde.HasValue)
                    condiciones.Add($"enc.fecha_desde >= '{fechaDesde.Value:yyyy-MM-dd}'");

                if (fechaHasta.HasValue)
                    condiciones.Add($"enc.fecha_hasta <= '{fechaHasta.Value:yyyy-MM-dd}'");

                if (idCliente.HasValue && idCliente.Value > 0)
                    condiciones.Add($"(CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN pb.IdPropietario ELSE cl.IdPropietario END) = {idCliente.Value}");

                if (condiciones.Any())
                    sp += " AND " + string.Join(" AND ", condiciones);

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                var total = Convert.ToInt32(cmd.ExecuteScalar());

                lTransaction.Commit();
                return total;
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                throw new Exception($"Error en GetTotalPrefacturasPendientes: {ex.Message}", ex);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
                lTransaction?.Dispose();
            }
        }

        /// <summary>
        /// Marca una prefactura como procesada por ERP
        /// </summary>
        public static bool MarcarComoProcesada(IConfiguration config, int idPrefacturaEnc, string usuarioModificacion = "odoo")
        {
            var lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                const string sp = @"UPDATE trans_prefactura_enc 
                                    SET procesado_erp = 1, 
                                        user_mod = @user_mod, 
                                        fec_mod = @fec_mod 
                                    WHERE IdTransPrefacturaEnc = @IdTransPrefacturaEnc";

                using var cmd = new SqlCommand(sp, lConnection, lTransaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", idPrefacturaEnc);
                cmd.Parameters.AddWithValue("@user_mod", usuarioModificacion);
                cmd.Parameters.AddWithValue("@fec_mod", DateTime.Now);

                var rowsAffected = cmd.ExecuteNonQuery();

                lTransaction.Commit();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                throw new Exception($"Error en MarcarComoProcesada: {ex.Message}", ex);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
                lTransaction?.Dispose();
            }
        }

        /// <summary>
        /// Obtiene una prefactura específica por ID
        /// </summary>
        public static DataTable GetPrefacturaById(IConfiguration config, int idPrefacturaEnc)
        {
            var lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                var sp = @"SELECT enc.IdTransPrefacturaEnc,
                                  enc.fec_agr as FechaCreacion,
                                  enc.fecha_desde as FechaDesde,
                                  enc.fecha_hasta as FechaHasta,
                                  enc.Tipo_Cambio,
                                  enc.Observacion,
                                  CASE WHEN enc.es_consolidador = 1 
                                       THEN 'TO ' + enc.poliza_oc_numero_orden + ' - ID ' + enc.poliza_pe_numero_orden 
                                       ELSE ISNULL(acuerdo.descripcion, '')
                                  END as Mercaderia,
                                  'del ' + CONVERT(varchar(25), enc.fecha_desde, 105) + ' al ' + CONVERT(varchar(25), enc.fecha_hasta, 105) as Periodo,
                                  acuerdo.codigo_acuerdo as CodigoAcuerdo,
                                  ISNULL(acuerdo.moneda, 'USD') as Moneda,
                                  CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pr.NIT, '') ELSE ISNULL(pr2.NIT, '') END as Nit,
                                  CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pr.IdPropietario, 0) ELSE ISNULL(pr2.IdPropietario, 0) END as IdClienteFacturar,
                                  CASE WHEN enc.IdClienteBodega IS NULL OR enc.IdClienteBodega = 0 THEN ISNULL(pb.IdPropietario, 0) ELSE ISNULL(cl.IdPropietario, 0) END as IdCliente,
                                  det.codigo_producto_acuerdo_det as CodigoProducto,
                                  det.correlativo_detalle_acuerdo as CorrelativoDetalleAcuerdo,
                                  det.dias_eventos as Dias,
                                  det.monto_erp as Monto,
                                  ISNULL(det.descripcion, '') as Descripcion,
                                  det.numero_unidades_acuerdo_det as NumeroUnidades
                           FROM trans_prefactura_enc enc
                           INNER JOIN trans_prefactura_det det ON enc.IdTransPrefacturaEnc = det.IdTransPrefacturaEnc
                           INNER JOIN trans_acuerdoscomerciales_enc acuerdo ON det.IdAcuerdoEnc = acuerdo.IdAcuerdoEnc
                           INNER JOIN propietario_bodega pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega
                           INNER JOIN propietarios pr ON pb.IdPropietario = pr.IdPropietario
                           LEFT JOIN cliente_bodega cb ON enc.IdClienteBodega = cb.IdClienteBodega
                           LEFT JOIN cliente cl ON cb.IdCliente = cl.IdCliente
                           LEFT JOIN propietarios pr2 ON cl.IdPropietario = pr2.IdPropietario
                           WHERE enc.IdTransPrefacturaEnc = @IdTransPrefacturaEnc";

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", idPrefacturaEnc);

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                throw new Exception($"Error en GetPrefacturaById: {ex.Message}", ex);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
                lTransaction?.Dispose();
            }
        }

        #endregion
    }
}