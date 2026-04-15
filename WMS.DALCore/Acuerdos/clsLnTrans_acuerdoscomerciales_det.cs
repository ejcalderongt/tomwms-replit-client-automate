using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using WMS.EntityCore.AcuerdosComerciales;

namespace WMS.DALCore.AcuerdosComerciales
{
    public class clsLnTrans_acuerdoscomerciales_det
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

        public static void Bind(SqlCommand cmd, clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet)
        {
            cmd.Parameters.Add(new SqlParameter("@IdAcuerdoDet", oBeAcuerdoDet.IdAcuerdoDet != 0 ? oBeAcuerdoDet.IdAcuerdoDet : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", oBeAcuerdoDet.IdAcuerdoEnc != null && oBeAcuerdoDet.IdAcuerdoEnc != 0 ? oBeAcuerdoDet.IdAcuerdoEnc : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@codigo_producto", !string.IsNullOrWhiteSpace(oBeAcuerdoDet.Codigo_producto) ? oBeAcuerdoDet.Codigo_producto : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@servicio", !string.IsNullOrWhiteSpace(oBeAcuerdoDet.Servicio) ? oBeAcuerdoDet.Servicio : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@nemonico", !string.IsNullOrWhiteSpace(oBeAcuerdoDet.Nemonico) ? oBeAcuerdoDet.Nemonico : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@codigo_acuerdo", oBeAcuerdoDet.Codigo_acuerdo != null && oBeAcuerdoDet.Codigo_acuerdo != 0 ? oBeAcuerdoDet.Codigo_acuerdo : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@correlativo_detalleacuerdo", oBeAcuerdoDet.Correlativo_detalleacuerdo != null && oBeAcuerdoDet.Correlativo_detalleacuerdo != 0 ? oBeAcuerdoDet.Correlativo_detalleacuerdo : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@descripcion", !string.IsNullOrWhiteSpace(oBeAcuerdoDet.Descripcion) ? oBeAcuerdoDet.Descripcion : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@numero_unidades", oBeAcuerdoDet.Numero_unidades != null && oBeAcuerdoDet.Numero_unidades != 0 ? oBeAcuerdoDet.Numero_unidades : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@monto", oBeAcuerdoDet.Monto != null && oBeAcuerdoDet.Monto != 0 ? oBeAcuerdoDet.Monto : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@porcentaje", oBeAcuerdoDet.Porcentaje != null && oBeAcuerdoDet.Porcentaje != 0 ? oBeAcuerdoDet.Porcentaje : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@dias_eventos", oBeAcuerdoDet.Dias_eventos != null && oBeAcuerdoDet.Dias_eventos != 0 ? oBeAcuerdoDet.Dias_eventos : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@corre_cbcatalogoproductos", oBeAcuerdoDet.Corre_cbcatalogoproductos != null && oBeAcuerdoDet.Corre_cbcatalogoproductos != 0 ? oBeAcuerdoDet.Corre_cbcatalogoproductos : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeAcuerdoDet.Estado != null ? oBeAcuerdoDet.Estado : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@prioridad", oBeAcuerdoDet.Prioridad != null ? oBeAcuerdoDet.Prioridad : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeAcuerdoDet.IdBodega != null && oBeAcuerdoDet.IdBodega != 0 ? oBeAcuerdoDet.IdBodega : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdTipoCobro", oBeAcuerdoDet.IdTipoCobro != null && oBeAcuerdoDet.IdTipoCobro != 0 ? oBeAcuerdoDet.IdTipoCobro : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeAcuerdoDet.User_agr != null && oBeAcuerdoDet.User_agr != 0 ? oBeAcuerdoDet.User_agr : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeAcuerdoDet.Fec_agr != null && oBeAcuerdoDet.Fec_agr != DateTime.MinValue ? oBeAcuerdoDet.Fec_agr : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeAcuerdoDet.User_mod != null && oBeAcuerdoDet.User_mod != 0 ? oBeAcuerdoDet.User_mod : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeAcuerdoDet.Fec_mod != null && oBeAcuerdoDet.Fec_mod != DateTime.MinValue ? oBeAcuerdoDet.Fec_mod : DBNull.Value));
        }

        public static void Cargar(ref clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet, DataRow dr)
        {
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
            int? GetIntNullable(string col) { return dr[col] is DBNull ? (int?)null : Convert.ToInt32(dr[col]); }
            decimal? GetDecimalNullable(string col) { return dr[col] is DBNull ? (decimal?)null : Convert.ToDecimal(dr[col]); }
            bool? GetBoolNullable(string col) { return dr[col] is DBNull ? (bool?)null : Convert.ToBoolean(dr[col]); }
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            byte? GetByteNullable(string col) { return dr[col] is DBNull ? (byte?)null : Convert.ToByte(dr[col]); }
            DateTime? GetDateNullable(string col) { return dr[col] is DBNull ? (DateTime?)null : Convert.ToDateTime(dr[col]); }

            try
            {
                oBeAcuerdoDet.IdAcuerdoDet = GetInt("IdAcuerdoDet");
                oBeAcuerdoDet.IdAcuerdoEnc = GetIntNullable("IdAcuerdoEnc");
                oBeAcuerdoDet.Codigo_producto = GetString("codigo_producto");
                oBeAcuerdoDet.Servicio = GetString("servicio");
                oBeAcuerdoDet.Nemonico = GetString("nemonico");
                oBeAcuerdoDet.Codigo_acuerdo = GetIntNullable("codigo_acuerdo");
                oBeAcuerdoDet.Correlativo_detalleacuerdo = GetIntNullable("correlativo_detalleacuerdo");
                oBeAcuerdoDet.Descripcion = GetString("descripcion");
                oBeAcuerdoDet.Numero_unidades = GetDecimalNullable("numero_unidades");
                oBeAcuerdoDet.Monto = GetDecimalNullable("monto");
                oBeAcuerdoDet.Porcentaje = GetDecimalNullable("porcentaje");
                oBeAcuerdoDet.Dias_eventos = GetIntNullable("dias_eventos");
                oBeAcuerdoDet.Corre_cbcatalogoproductos = GetIntNullable("corre_cbcatalogoproductos");
                oBeAcuerdoDet.Estado = GetBoolNullable("estado");
                oBeAcuerdoDet.Prioridad = GetByteNullable("prioridad");
                oBeAcuerdoDet.IdBodega = GetIntNullable("IdBodega");
                oBeAcuerdoDet.IdTipoCobro = GetIntNullable("IdTipoCobro");
                oBeAcuerdoDet.User_agr = GetIntNullable("user_agr");
                oBeAcuerdoDet.Fec_agr = GetDateNullable("fec_agr");
                oBeAcuerdoDet.User_mod = GetIntNullable("user_mod");
                oBeAcuerdoDet.Fec_mod = GetDateNullable("fec_mod");
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = sf?.GetMethod();
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
                throw new Exception(vMsgError);
            }
        }

        public static int Insertar(clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeAcuerdoDet == null)
                throw new ArgumentNullException(nameof(oBeAcuerdoDet));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            int rowsAffected = 0;

            try
            {
                Ins.Init("trans_acuerdoscomerciales_det");
                Ins.Add("IdAcuerdoDet", "@IdAcuerdoDet", "F");
                Ins.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Ins.Add("codigo_producto", "@codigo_producto", "F");
                Ins.Add("servicio", "@servicio", "F");
                Ins.Add("nemonico", "@nemonico", "F");
                Ins.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Ins.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", "F");
                Ins.Add("descripcion", "@descripcion", "F");
                Ins.Add("numero_unidades", "@numero_unidades", "F");
                Ins.Add("monto", "@monto", "F");
                Ins.Add("porcentaje", "@porcentaje", "F");
                Ins.Add("dias_eventos", "@dias_eventos", "F");
                Ins.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("prioridad", "@prioridad", "F");
                Ins.Add("IdBodega", "@IdBodega", "F");
                Ins.Add("IdTipoCobro", "@IdTipoCobro", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");

                string sp = Ins.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    Bind(cmd, oBeAcuerdoDet);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Error en Insertar Detalle - {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        public static int Insertar(IConfiguration config, clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet)
        {
            int rowsAffected = 0;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                Ins.Init("trans_acuerdoscomerciales_det");
                Ins.Add("IdAcuerdoDet", "@IdAcuerdoDet", "F");
                Ins.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Ins.Add("codigo_producto", "@codigo_producto", "F");
                Ins.Add("servicio", "@servicio", "F");
                Ins.Add("nemonico", "@nemonico", "F");
                Ins.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Ins.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", "F");
                Ins.Add("descripcion", "@descripcion", "F");
                Ins.Add("numero_unidades", "@numero_unidades", "F");
                Ins.Add("monto", "@monto", "F");
                Ins.Add("porcentaje", "@porcentaje", "F");
                Ins.Add("dias_eventos", "@dias_eventos", "F");
                Ins.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("prioridad", "@prioridad", "F");
                Ins.Add("IdBodega", "@IdBodega", "F");
                Ins.Add("IdTipoCobro", "@IdTipoCobro", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");

                string sp = Ins.SQL();

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                Bind(cmd, oBeAcuerdoDet);
                rowsAffected = cmd.ExecuteNonQuery();

                if (lTransaction != null)
                    lTransaction.Commit();
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
            return rowsAffected;
        }

        public static int Actualizar(clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeAcuerdoDet == null)
                throw new ArgumentNullException(nameof(oBeAcuerdoDet));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            int rowsAffected = 0;

            try
            {
                Upd.Init("trans_acuerdoscomerciales_det");
                Upd.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Upd.Add("codigo_producto", "@codigo_producto", "F");
                Upd.Add("servicio", "@servicio", "F");
                Upd.Add("nemonico", "@nemonico", "F");
                Upd.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Upd.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", "F");
                Upd.Add("descripcion", "@descripcion", "F");
                Upd.Add("numero_unidades", "@numero_unidades", "F");
                Upd.Add("monto", "@monto", "F");
                Upd.Add("porcentaje", "@porcentaje", "F");
                Upd.Add("dias_eventos", "@dias_eventos", "F");
                Upd.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", "F");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("prioridad", "@prioridad", "F");
                Upd.Add("IdBodega", "@IdBodega", "F");
                Upd.Add("IdTipoCobro", "@IdTipoCobro", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Where("IdAcuerdoDet = @IdAcuerdoDet");

                string sp = Upd.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    Bind(cmd, oBeAcuerdoDet);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Error en Actualizar Detalle - {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        public static int Actualizar(IConfiguration config, clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet)
        {
            int rowsAffected = 0;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                Upd.Init("trans_acuerdoscomerciales_det");
                Upd.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Upd.Add("codigo_producto", "@codigo_producto", "F");
                Upd.Add("servicio", "@servicio", "F");
                Upd.Add("nemonico", "@nemonico", "F");
                Upd.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Upd.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", "F");
                Upd.Add("descripcion", "@descripcion", "F");
                Upd.Add("numero_unidades", "@numero_unidades", "F");
                Upd.Add("monto", "@monto", "F");
                Upd.Add("porcentaje", "@porcentaje", "F");
                Upd.Add("dias_eventos", "@dias_eventos", "F");
                Upd.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", "F");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("prioridad", "@prioridad", "F");
                Upd.Add("IdBodega", "@IdBodega", "F");
                Upd.Add("IdTipoCobro", "@IdTipoCobro", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Where("IdAcuerdoDet = @IdAcuerdoDet");

                string sp = Upd.SQL();

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                Bind(cmd, oBeAcuerdoDet);
                rowsAffected = cmd.ExecuteNonQuery();

                if (lTransaction != null)
                    lTransaction.Commit();
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
            return rowsAffected;
        }

        public int Eliminar(IConfiguration config, clsBeTrans_acuerdoscomerciales_det oBeAcuerdoDet, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoDet = @IdAcuerdoDet";

                bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

                SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

                if (Es_Transaccion_Remota)
                {
                    cmd = new SqlCommand(sp, pConection, pTransaction);
                }
                else
                {
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.Parameters.Add(new SqlParameter("@IdAcuerdoDet", oBeAcuerdoDet.IdAcuerdoDet));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (!Es_Transaccion_Remota)
                    if (lTransaction != null)
                        lTransaction.Commit();

                return rowsAffected;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
        }

        public static int EliminarPorIdAcuerdoEnc(IConfiguration config, int pIdAcuerdoEnc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoEnc = @IdAcuerdoEnc";

                bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

                SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

                if (Es_Transaccion_Remota)
                {
                    cmd = new SqlCommand(sp, pConection, pTransaction);
                }
                else
                {
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", pIdAcuerdoEnc));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (!Es_Transaccion_Remota)
                    if (lTransaction != null)
                        lTransaction.Commit();

                return rowsAffected;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
        }

        public DataTable Listar(IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_det";
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();

                return dt;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
        }

        public DataTable ListarPorIdAcuerdoEnc(IConfiguration config, int pIdAcuerdoEnc)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoEnc = @IdAcuerdoEnc";
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", pIdAcuerdoEnc));
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();

                return dt;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
        }

        public static bool GetSingle(IConfiguration config, ref clsBeTrans_acuerdoscomerciales_det pBeAcuerdoDet)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoDet = @IdAcuerdoDet";

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);

                dad.SelectCommand.Parameters.Add(new SqlParameter("@IdAcuerdoDet", pBeAcuerdoDet.IdAcuerdoDet));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();

                if (dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    Cargar(ref pBeAcuerdoDet, r);
                    return true;
                }
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lConnection != null) lConnection.Dispose();
                if (lTransaction != null) lTransaction.Dispose();
            }
            return false;
        }

        public static List<clsBeTrans_acuerdoscomerciales_det> GetAll(IConfiguration config)
        {
            SqlTransaction? lTransaction = null;
            List<clsBeTrans_acuerdoscomerciales_det> lreturnList = new List<clsBeTrans_acuerdoscomerciales_det>();

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_det";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_acuerdoscomerciales_det vBeAcuerdoDet = new clsBeTrans_acuerdoscomerciales_det();

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeAcuerdoDet = new clsBeTrans_acuerdoscomerciales_det();
                                Cargar(ref vBeAcuerdoDet, dr);
                                lreturnList.Add(vBeAcuerdoDet);
                            }

                            lTransaction.Commit();
                        }

                        lConnection.Close();
                    }
                }

                return lreturnList;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
        }

        public static List<clsBeTrans_acuerdoscomerciales_det> GetAllByIdAcuerdoEnc(IConfiguration config, int pIdAcuerdoEnc)
        {
            SqlTransaction? lTransaction = null;
            List<clsBeTrans_acuerdoscomerciales_det> lreturnList = new List<clsBeTrans_acuerdoscomerciales_det>();

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoEnc = @IdAcuerdoEnc";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", pIdAcuerdoEnc));
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_acuerdoscomerciales_det vBeAcuerdoDet = new clsBeTrans_acuerdoscomerciales_det();

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeAcuerdoDet = new clsBeTrans_acuerdoscomerciales_det();
                                Cargar(ref vBeAcuerdoDet, dr);
                                lreturnList.Add(vBeAcuerdoDet);
                            }

                            lTransaction.Commit();
                        }

                        lConnection.Close();
                    }
                }

                return lreturnList;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
        }

        public static int MaxID(IConfiguration config)
        {
            SqlTransaction? lTransaction = null;

            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdAcuerdoDet), 0) FROM trans_acuerdoscomerciales_det";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                        {
                            object lreturnValue = lCommand.ExecuteScalar();
                            if (lreturnValue != DBNull.Value && lreturnValue != null)
                            {
                                lMax = Convert.ToInt32(lreturnValue);
                            }
                        }
                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }

                return lMax;
            }
            catch (SqlException ex1)
            {
                if (lTransaction is not null)
                    lTransaction.Rollback();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = null;
                if (sf != null) { currentMethodName = sf.GetMethod(); }
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
                throw new Exception(vMsgError);
            }
        }

        public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = "SELECT ISNULL(Max(IdAcuerdoDet), 0) FROM trans_acuerdoscomerciales_det";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    object lreturnValue = cmd.ExecuteScalar();

                    if (lreturnValue != DBNull.Value && lreturnValue != null)
                    {
                        return Convert.ToInt32(lreturnValue);
                    }

                    return 0;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Error en MaxID - {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        public static bool Existe(int idAcuerdoDet, SqlConnection conn, SqlTransaction? tx = null)
        {
            const string sql = "SELECT COUNT(1) FROM trans_acuerdoscomerciales_det WHERE IdAcuerdoDet = @IdAcuerdoDet";

            using SqlCommand cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@IdAcuerdoDet", idAcuerdoDet);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }

        public static void InsertarOActualizar(List<clsBeTrans_acuerdoscomerciales_det> entities, SqlConnection conn, SqlTransaction tx)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (conn == null)
                throw new ArgumentNullException(nameof(conn));

            if (tx == null)
                throw new ArgumentNullException(nameof(tx));

            try
            {
                foreach (var entity in entities)
                {
                    if (entity == null)
                        continue;

                    bool existe = Existe(entity.IdAcuerdoDet, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
            catch (SqlException ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
            }
        }
    }
}