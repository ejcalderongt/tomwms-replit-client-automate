using Microsoft.Data.SqlClient;
using WMS.EntityCore.Prefactura;

namespace WMS.DALCore.Prefactura
{
    public class clsLnTrans_prefactura_det
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

        #region Métodos Auxiliares

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

        private static decimal GetDecimalValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? 0 : Convert.ToDecimal(dr[columnName]);
        }

        private static DateTime GetDateTimeValue(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr[columnName]);
        }

        private static bool GetBoolValue(DataRow dr, string columnName)
        {
            return dr[columnName] != DBNull.Value && Convert.ToBoolean(dr[columnName]);
        }

        #endregion

        #region Cargar

        public static void Cargar(ref clsBeTrans_prefactura_det oBePrefacturaDet, DataRow dr)
        {
            try
            {
                oBePrefacturaDet.IdTransPrefacturaDet = GetIntValue(dr, "IdTransPrefacturaDet");
                oBePrefacturaDet.IdTransPrefacturaEnc = GetIntValue(dr, "IdTransPrefacturaEnc");
                oBePrefacturaDet.IdAcuerdoEnc = GetIntValue(dr, "IdAcuerdoEnc");
                oBePrefacturaDet.Codigo_acuerdo_enc = GetIntValue(dr, "codigo_acuerdo_enc");
                oBePrefacturaDet.Codigo_producto_acuerdo_det = GetStringValue(dr, "codigo_producto_acuerdo_det");
                oBePrefacturaDet.IdAcuerdoDet = GetIntValue(dr, "IdAcuerdoDet");
                oBePrefacturaDet.Correlativo_detalle_acuerdo = GetIntValue(dr, "correlativo_detalle_acuerdo");
                oBePrefacturaDet.Numero_unidades_acuerdo_det = GetDecimalValue(dr, "numero_unidades_acuerdo_det");
                oBePrefacturaDet.Servicio = GetDoubleValue(dr, "servicio");
                oBePrefacturaDet.Descripcion = GetStringValue(dr, "descripcion");
                oBePrefacturaDet.Monto = GetDoubleValue(dr, "monto");
                oBePrefacturaDet.Porcentaje = GetDoubleValue(dr, "porcentaje");
                oBePrefacturaDet.Dias_eventos = GetIntValue(dr, "dias_eventos");
                oBePrefacturaDet.Valor = GetDoubleValue(dr, "valor");
                oBePrefacturaDet.User_agr = GetStringValue(dr, "user_agr");
                oBePrefacturaDet.Fec_agr = GetDateTimeValue(dr, "fec_agr");
                oBePrefacturaDet.User_mod = GetStringValue(dr, "user_mod");
                oBePrefacturaDet.Fec_mod = GetDateTimeValue(dr, "fec_mod");
                oBePrefacturaDet.Activo = GetBoolValue(dr, "activo");
                oBePrefacturaDet.Monto_Erp = GetDoubleValue(dr, "monto_erp");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Cargar: {ex.Message}", ex);
            }
        }

        #endregion

        #region Insertar

        public static int Insertar(clsBeTrans_prefactura_det oBePrefacturaDet, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("trans_prefactura_det");
                Ins.Add("IdTransPrefacturaDet", "@IdTransPrefacturaDet", "F");
                Ins.Add("IdTransPrefacturaEnc", "@IdTransPrefacturaEnc", "F");
                Ins.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Ins.Add("codigo_acuerdo_enc", "@codigo_acuerdo_enc", "F");
                Ins.Add("codigo_producto_acuerdo_det", "@codigo_producto_acuerdo_det", "F");
                Ins.Add("IdAcuerdoDet", "@IdAcuerdoDet", "F");
                Ins.Add("correlativo_detalle_acuerdo", "@correlativo_detalle_acuerdo", "F");
                Ins.Add("numero_unidades_acuerdo_det", "@numero_unidades_acuerdo_det", "F");
                Ins.Add("servicio", "@servicio", "F");
                Ins.Add("descripcion", "@descripcion", "F");
                Ins.Add("monto", "@monto", "F");
                Ins.Add("porcentaje", "@porcentaje", "F");
                Ins.Add("dias_eventos", "@dias_eventos", "F");
                Ins.Add("valor", "@valor", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("activo", "@activo", "F");
                Ins.Add("monto_erp", "@monto_erp", "F");

                var sp = Ins.SQL();
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdTransPrefacturaDet", oBePrefacturaDet.IdTransPrefacturaDet);
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", oBePrefacturaDet.IdTransPrefacturaEnc);
                cmd.Parameters.AddWithValue("@IdAcuerdoEnc", oBePrefacturaDet.IdAcuerdoEnc);
                cmd.Parameters.AddWithValue("@codigo_acuerdo_enc", oBePrefacturaDet.Codigo_acuerdo_enc);
                cmd.Parameters.AddWithValue("@codigo_producto_acuerdo_det", oBePrefacturaDet.Codigo_producto_acuerdo_det ?? "");
                cmd.Parameters.AddWithValue("@IdAcuerdoDet", oBePrefacturaDet.IdAcuerdoDet);
                cmd.Parameters.AddWithValue("@correlativo_detalle_acuerdo", oBePrefacturaDet.Correlativo_detalle_acuerdo);
                cmd.Parameters.AddWithValue("@numero_unidades_acuerdo_det", oBePrefacturaDet.Numero_unidades_acuerdo_det);
                cmd.Parameters.AddWithValue("@servicio", oBePrefacturaDet.Servicio);
                cmd.Parameters.AddWithValue("@descripcion", oBePrefacturaDet.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@monto", oBePrefacturaDet.Monto);
                cmd.Parameters.AddWithValue("@porcentaje", oBePrefacturaDet.Porcentaje);
                cmd.Parameters.AddWithValue("@dias_eventos", oBePrefacturaDet.Dias_eventos);
                cmd.Parameters.AddWithValue("@valor", oBePrefacturaDet.Valor);
                cmd.Parameters.AddWithValue("@user_agr", oBePrefacturaDet.User_agr ?? "");
                cmd.Parameters.AddWithValue("@fec_agr", oBePrefacturaDet.Fec_agr);
                cmd.Parameters.AddWithValue("@user_mod", oBePrefacturaDet.User_mod ?? "");
                cmd.Parameters.AddWithValue("@fec_mod", oBePrefacturaDet.Fec_mod);
                cmd.Parameters.AddWithValue("@activo", oBePrefacturaDet.Activo);
                cmd.Parameters.AddWithValue("@monto_erp", oBePrefacturaDet.Monto_Erp);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Insertar: {ex.Message}", ex);
            }
        }

        #endregion

        #region Actualizar

        public static int Actualizar(clsBeTrans_prefactura_det oBePrefacturaDet, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("trans_prefactura_det");
                Upd.Add("IdTransPrefacturaEnc", "@IdTransPrefacturaEnc", "F");
                Upd.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Upd.Add("codigo_acuerdo_enc", "@codigo_acuerdo_enc", "F");
                Upd.Add("codigo_producto_acuerdo_det", "@codigo_producto_acuerdo_det", "F");
                Upd.Add("IdAcuerdoDet", "@IdAcuerdoDet", "F");
                Upd.Add("correlativo_detalle_acuerdo", "@correlativo_detalle_acuerdo", "F");
                Upd.Add("numero_unidades_acuerdo_det", "@numero_unidades_acuerdo_det", "F");
                Upd.Add("servicio", "@servicio", "F");
                Upd.Add("descripcion", "@descripcion", "F");
                Upd.Add("monto", "@monto", "F");
                Upd.Add("porcentaje", "@porcentaje", "F");
                Upd.Add("dias_eventos", "@dias_eventos", "F");
                Upd.Add("valor", "@valor", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Add("monto_erp", "@monto_erp", "F");
                Upd.Where("IdTransPrefacturaDet = @IdTransPrefacturaDet");

                var sp = Upd.SQL();
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdTransPrefacturaDet", oBePrefacturaDet.IdTransPrefacturaDet);
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", oBePrefacturaDet.IdTransPrefacturaEnc);
                cmd.Parameters.AddWithValue("@IdAcuerdoEnc", oBePrefacturaDet.IdAcuerdoEnc);
                cmd.Parameters.AddWithValue("@codigo_acuerdo_enc", oBePrefacturaDet.Codigo_acuerdo_enc);
                cmd.Parameters.AddWithValue("@codigo_producto_acuerdo_det", oBePrefacturaDet.Codigo_producto_acuerdo_det ?? "");
                cmd.Parameters.AddWithValue("@IdAcuerdoDet", oBePrefacturaDet.IdAcuerdoDet);
                cmd.Parameters.AddWithValue("@correlativo_detalle_acuerdo", oBePrefacturaDet.Correlativo_detalle_acuerdo);
                cmd.Parameters.AddWithValue("@numero_unidades_acuerdo_det", oBePrefacturaDet.Numero_unidades_acuerdo_det);
                cmd.Parameters.AddWithValue("@servicio", oBePrefacturaDet.Servicio);
                cmd.Parameters.AddWithValue("@descripcion", oBePrefacturaDet.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@monto", oBePrefacturaDet.Monto);
                cmd.Parameters.AddWithValue("@porcentaje", oBePrefacturaDet.Porcentaje);
                cmd.Parameters.AddWithValue("@dias_eventos", oBePrefacturaDet.Dias_eventos);
                cmd.Parameters.AddWithValue("@valor", oBePrefacturaDet.Valor);
                cmd.Parameters.AddWithValue("@user_mod", oBePrefacturaDet.User_mod ?? "");
                cmd.Parameters.AddWithValue("@fec_mod", oBePrefacturaDet.Fec_mod);
                cmd.Parameters.AddWithValue("@activo", oBePrefacturaDet.Activo);
                cmd.Parameters.AddWithValue("@monto_erp", oBePrefacturaDet.Monto_Erp);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Actualizar: {ex.Message}", ex);
            }
        }

        #endregion

        #region Eliminar

        public static int Eliminar(int idTransPrefacturaDet, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM trans_prefactura_det WHERE IdTransPrefacturaDet = @IdTransPrefacturaDet";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@IdTransPrefacturaDet", idTransPrefacturaDet);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Eliminar: {ex.Message}", ex);
            }
        }

        public static int EliminarPorEncabezado(int idTransPrefacturaEnc, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM trans_prefactura_det WHERE IdTransPrefacturaEnc = @IdTransPrefacturaEnc";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", idTransPrefacturaEnc);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en EliminarPorEncabezado: {ex.Message}", ex);
            }
        }

        #endregion

        #region Métodos de Consulta

        public static DataTable Listar(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT * FROM trans_prefactura_det";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Listar: {ex.Message}", ex);
            }
        }

        public static DataTable GetByEncabezadoId(int idTransPrefacturaEnc, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT * FROM trans_prefactura_det WHERE IdTransPrefacturaEnc = @IdTransPrefacturaEnc";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", idTransPrefacturaEnc);
                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en GetByEncabezadoId: {ex.Message}", ex);
            }
        }

        public static List<clsBeTrans_prefactura_det> GetAllByEncabezadoId(int idTransPrefacturaEnc, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            var result = new List<clsBeTrans_prefactura_det>();
            try
            {
                var dt = GetByEncabezadoId(idTransPrefacturaEnc, pConnection, pTransaction);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new clsBeTrans_prefactura_det();
                    Cargar(ref item, dr);
                    result.Add(item);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en GetAllByEncabezadoId: {ex.Message}", ex);
            }
        }

        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT ISNULL(Max(IdTransPrefacturaDet),0) FROM trans_prefactura_det";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en MaxID: {ex.Message}", ex);
            }
        }

        public static int MaxIDByEncabezado(int idTransPrefacturaEnc, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT ISNULL(Max(IdTransPrefacturaDet),0) FROM trans_prefactura_det WHERE IdTransPrefacturaEnc = @IdTransPrefacturaEnc";
                using var cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@IdTransPrefacturaEnc", idTransPrefacturaEnc);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en MaxIDByEncabezado: {ex.Message}", ex);
            }
        }

        #endregion
    }
}