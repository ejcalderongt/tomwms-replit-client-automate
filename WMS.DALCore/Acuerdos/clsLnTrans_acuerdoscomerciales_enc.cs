using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using WMS.EntityCore.AcuerdosComerciales;

namespace WMS.DALCore.AcuerdosComerciales
{
    public class clsLnTrans_acuerdoscomerciales_enc
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

        public static void Bind(SqlCommand cmd, clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo)
        {
            cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", oBeAcuerdo.IdAcuerdoEnc != 0 ? oBeAcuerdo.IdAcuerdoEnc : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdCliente", oBeAcuerdo.IdCliente != 0 ? oBeAcuerdo.IdCliente : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@codigo_acuerdo", oBeAcuerdo.Codigo_acuerdo != null && oBeAcuerdo.Codigo_acuerdo != 0 ? oBeAcuerdo.Codigo_acuerdo : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@descripcion", !string.IsNullOrWhiteSpace(oBeAcuerdo.Descripcion) ? oBeAcuerdo.Descripcion : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@tipo_cobro", !string.IsNullOrWhiteSpace(oBeAcuerdo.Tipo_cobro) ? oBeAcuerdo.Tipo_cobro : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@cod_moneda", oBeAcuerdo.Cod_moneda != null && oBeAcuerdo.Cod_moneda != 0 ? oBeAcuerdo.Cod_moneda : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@moneda", !string.IsNullOrWhiteSpace(oBeAcuerdo.Moneda) ? oBeAcuerdo.Moneda : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeAcuerdo.Estado != null ? oBeAcuerdo.Estado : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(oBeAcuerdo.User_agr) ? oBeAcuerdo.User_agr : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeAcuerdo.Fec_agr != null && oBeAcuerdo.Fec_agr != DateTime.MinValue ? oBeAcuerdo.Fec_agr : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(oBeAcuerdo.User_mod) ? oBeAcuerdo.User_mod : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeAcuerdo.Fec_mod != null && oBeAcuerdo.Fec_mod != DateTime.MinValue ? oBeAcuerdo.Fec_mod : DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_erp", oBeAcuerdo.Fec_erp != null && oBeAcuerdo.Fec_erp != DateTime.MinValue ? oBeAcuerdo.Fec_erp : DBNull.Value));
        }

        public static void Cargar(ref clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo, DataRow dr)
        {
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
            bool? GetBoolNullable(string col) { return dr[col] is DBNull ? (bool?)null : Convert.ToBoolean(dr[col]); }
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            DateTime? GetDateNullable(string col) { return dr[col] is DBNull ? (DateTime?)null : Convert.ToDateTime(dr[col]); }

            try
            {
                oBeAcuerdo.IdAcuerdoEnc = GetInt("IdAcuerdoEnc");
                oBeAcuerdo.IdCliente = GetInt("IdCliente");
                oBeAcuerdo.Codigo_acuerdo = dr["codigo_acuerdo"] is DBNull ? (int?)null : Convert.ToInt32(dr["codigo_acuerdo"]);
                oBeAcuerdo.Descripcion = GetString("descripcion");
                oBeAcuerdo.Tipo_cobro = GetString("tipo_cobro");
                oBeAcuerdo.Cod_moneda = dr["cod_moneda"] is DBNull ? (int?)null : Convert.ToInt32(dr["cod_moneda"]);
                oBeAcuerdo.Moneda = GetString("moneda");
                oBeAcuerdo.Estado = GetBoolNullable("estado");
                oBeAcuerdo.User_agr = GetString("user_agr");
                oBeAcuerdo.Fec_agr = GetDateNullable("fec_agr");
                oBeAcuerdo.User_mod = GetString("user_mod");
                oBeAcuerdo.Fec_mod = GetDateNullable("fec_mod");
                oBeAcuerdo.Fec_erp = GetDateNullable("fec_erp");
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

        public static int Insertar(clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeAcuerdo == null)
                throw new ArgumentNullException(nameof(oBeAcuerdo));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            int rowsAffected = 0;

            try
            {
                Ins.Init("trans_acuerdoscomerciales_enc");
                Ins.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Ins.Add("IdCliente", "@IdCliente", "F");
                Ins.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Ins.Add("descripcion", "@descripcion", "F");
                Ins.Add("tipo_cobro", "@tipo_cobro", "F");
                Ins.Add("cod_moneda", "@cod_moneda", "F");
                Ins.Add("moneda", "@moneda", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("fec_erp", "@fec_erp", "F");

                string sp = Ins.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    Bind(cmd, oBeAcuerdo);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Error en Insertar - {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        public static int Insertar(IConfiguration config, clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo)
        {
            int rowsAffected = 0;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                Ins.Init("trans_acuerdoscomerciales_enc");
                Ins.Add("IdAcuerdoEnc", "@IdAcuerdoEnc", "F");
                Ins.Add("IdCliente", "@IdCliente", "F");
                Ins.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Ins.Add("descripcion", "@descripcion", "F");
                Ins.Add("tipo_cobro", "@tipo_cobro", "F");
                Ins.Add("cod_moneda", "@cod_moneda", "F");
                Ins.Add("moneda", "@moneda", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("fec_erp", "@fec_erp", "F");

                string sp = Ins.SQL();

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                Bind(cmd, oBeAcuerdo);
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

        public static int Actualizar(clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeAcuerdo == null)
                throw new ArgumentNullException(nameof(oBeAcuerdo));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            int rowsAffected = 0;

            try
            {
                Upd.Init("trans_acuerdoscomerciales_enc");
                Upd.Add("IdCliente", "@IdCliente", "F");
                Upd.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Upd.Add("descripcion", "@descripcion", "F");
                Upd.Add("tipo_cobro", "@tipo_cobro", "F");
                Upd.Add("cod_moneda", "@cod_moneda", "F");
                Upd.Add("moneda", "@moneda", "F");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("fec_erp", "@fec_erp", "F");
                Upd.Where("IdAcuerdoEnc = @IdAcuerdoEnc");

                string sp = Upd.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    Bind(cmd, oBeAcuerdo);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Error en Actualizar - {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        public static int Actualizar(IConfiguration config, clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo)
        {
            int rowsAffected = 0;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                Upd.Init("trans_acuerdoscomerciales_enc");
                Upd.Add("IdCliente", "@IdCliente", "F");
                Upd.Add("codigo_acuerdo", "@codigo_acuerdo", "F");
                Upd.Add("descripcion", "@descripcion", "F");
                Upd.Add("tipo_cobro", "@tipo_cobro", "F");
                Upd.Add("cod_moneda", "@cod_moneda", "F");
                Upd.Add("moneda", "@moneda", "F");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("fec_erp", "@fec_erp", "F");
                Upd.Where("IdAcuerdoEnc = @IdAcuerdoEnc");

                string sp = Upd.SQL();

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                Bind(cmd, oBeAcuerdo);
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

        public static int Eliminar(IConfiguration config, clsBeTrans_acuerdoscomerciales_enc oBeAcuerdo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM trans_acuerdoscomerciales_enc WHERE IdAcuerdoEnc = @IdAcuerdoEnc";

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

                cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", oBeAcuerdo.IdAcuerdoEnc));

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
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_enc";
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

        public static bool GetSingle(IConfiguration config, ref clsBeTrans_acuerdoscomerciales_enc pBeAcuerdo)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_enc WHERE IdAcuerdoEnc = @IdAcuerdoEnc";

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);

                dad.SelectCommand.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", pBeAcuerdo.IdAcuerdoEnc));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();

                if (dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    Cargar(ref pBeAcuerdo, r);
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

        public static List<clsBeTrans_acuerdoscomerciales_enc> GetAll(IConfiguration config)
        {
            SqlTransaction? lTransaction = null;
            List<clsBeTrans_acuerdoscomerciales_enc> lreturnList = new List<clsBeTrans_acuerdoscomerciales_enc>();

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_enc";

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

                            clsBeTrans_acuerdoscomerciales_enc vBeAcuerdo = new clsBeTrans_acuerdoscomerciales_enc();

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeAcuerdo = new clsBeTrans_acuerdoscomerciales_enc();
                                Cargar(ref vBeAcuerdo, dr);
                                lreturnList.Add(vBeAcuerdo);
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

        public static List<clsBeTrans_acuerdoscomerciales_enc> GetAllByIdCliente(IConfiguration config, int pIdCliente)
        {
            SqlTransaction? lTransaction = null;
            List<clsBeTrans_acuerdoscomerciales_enc> lreturnList = new List<clsBeTrans_acuerdoscomerciales_enc>();

            try
            {
                const string sp = "SELECT * FROM trans_acuerdoscomerciales_enc WHERE IdCliente = @IdCliente";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.Add(new SqlParameter("@IdCliente", pIdCliente));
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_acuerdoscomerciales_enc vBeAcuerdo = new clsBeTrans_acuerdoscomerciales_enc();

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeAcuerdo = new clsBeTrans_acuerdoscomerciales_enc();
                                Cargar(ref vBeAcuerdo, dr);
                                lreturnList.Add(vBeAcuerdo);
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
                const string sp = "SELECT ISNULL(Max(IdAcuerdoEnc), 0) FROM trans_acuerdoscomerciales_enc";

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
                const string sp = "SELECT ISNULL(Max(IdAcuerdoEnc), 0) FROM trans_acuerdoscomerciales_enc";

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

        public static bool Existe(int idAcuerdoEnc, SqlConnection conn, SqlTransaction? tx = null)
        {
            const string sql = "SELECT COUNT(1) FROM trans_acuerdoscomerciales_enc WHERE IdAcuerdoEnc = @IdAcuerdoEnc";

            using SqlCommand cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@IdAcuerdoEnc", idAcuerdoEnc);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }

        public static void InsertarOActualizar(List<clsBeTrans_acuerdoscomerciales_enc> entities, SqlConnection conn, SqlTransaction tx)
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

                    bool existe = Existe(entity.IdAcuerdoEnc, conn, tx);

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