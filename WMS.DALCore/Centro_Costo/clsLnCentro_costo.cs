using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

namespace WMSWebAPI.Ln
{
    public class clsLnCentro_costo
    {
        private static readonly clsInsert Ins = new();
        private static readonly clsUpdate Upd = new();

        public static void Cargar(ref clsBeCentro_costo oBeCentro_costo, DataRow dr)
        {
            if (oBeCentro_costo == null) throw new ArgumentNullException(nameof(oBeCentro_costo));
            if (dr == null) throw new ArgumentNullException(nameof(dr));

            // Helper methods with null safety
            int GetInt(string col) => dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]);
            bool GetBool(string col) => dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]);
            string GetString(string col) => dr[col] is DBNull ? string.Empty : (Convert.ToString(dr[col]) ?? string.Empty);
            DateTime GetDate(string col) => dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]);            

            try
            {
                oBeCentro_costo.IdCentroCosto = GetInt("IdCentroCosto");
                oBeCentro_costo.IdEmpresa = GetInt("IdEmpresa");
                oBeCentro_costo.Codigo = GetString("Codigo");
                oBeCentro_costo.Nombre = GetString("Nombre");
                oBeCentro_costo.Fec_agr = GetDate("fec_agr");
                oBeCentro_costo.User_agr = GetString("user_agr");
                oBeCentro_costo.Fec_mod = GetDate("fec_mod");
                oBeCentro_costo.User_mod = GetString("user_mod");
                oBeCentro_costo.Activo = GetBool("activo");
                oBeCentro_costo.Referencia = GetString("referencia");
                oBeCentro_costo.Control_inventario = GetBool("control_inventario");
            }
            catch (Exception ex)
            {
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
        }

        public static int Insertar(IConfiguration config, clsBeCentro_costo oBeCentro_costo,
                                  SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (oBeCentro_costo == null) throw new ArgumentNullException(nameof(oBeCentro_costo));

            int rowsAffected = 0;
            SqlConnection? lConnection = null;
            SqlTransaction? lTransaction = null;

            try
            {
                Ins.Init("centro_costo");
                Ins.Add("idcentrocosto", "@idcentrocosto", "F");
                Ins.Add("idempresa", "@idempresa", "F");
                Ins.Add("codigo", "@codigo", "F");
                Ins.Add("nombre", "@nombre", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("activo", "@activo", "F");
                Ins.Add("referencia", "@referencia", "F");
                Ins.Add("control_inventario", "@control_inventario", "F");

                string sp = Ins.SQL() ?? throw new InvalidOperationException("SQL statement is null");

                bool esTransaccionRemota = pConection != null && pTransaction != null;
                SqlCommand cmd;

                if (esTransaccionRemota)
                {
                    cmd = new SqlCommand(sp, pConection!, pTransaction!);
                }
                else
                {
                    string? connectionString = config.GetConnectionString("CST");
                    if (string.IsNullOrEmpty(connectionString))
                        throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                    lConnection = new SqlConnection(connectionString);
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.CommandType = CommandType.Text;

                // Add parameters with null handling
                cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", oBeCentro_costo.IdCentroCosto));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeCentro_costo.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Codigo", oBeCentro_costo.Codigo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeCentro_costo.Nombre ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeCentro_costo.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeCentro_costo.User_agr ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeCentro_costo.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeCentro_costo.User_mod ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeCentro_costo.Activo));
                cmd.Parameters.Add(new SqlParameter("@referencia", oBeCentro_costo.Referencia ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@control_inventario", oBeCentro_costo.Control_inventario));

                rowsAffected = cmd.ExecuteNonQuery();

                if (!esTransaccionRemota)
                {
                    lTransaction?.Commit();
                }
            }
            catch (SqlException ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            finally
            {
                if (!(pConection != null && pTransaction != null)) // Solo cerramos si creamos la conexión
                {
                    lTransaction?.Dispose();

                    if (lConnection?.State == ConnectionState.Open)
                        lConnection.Close();
                    lConnection?.Dispose();
                }
            }

            return rowsAffected;
        }

        public static int Insertar(IConfiguration config, clsBeCentro_costo oBeCentro_costo)
        {
            return Insertar(config, oBeCentro_costo, null, null);
        }

        public static int Actualizar(IConfiguration config, clsBeCentro_costo oBeCentro_costo,
                                    SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (oBeCentro_costo == null) throw new ArgumentNullException(nameof(oBeCentro_costo));

            int rowsAffected = 0;
            SqlConnection? lConnection = null;
            SqlTransaction? lTransaction = null;

            try
            {
                Upd.Init("centro_costo");
                Upd.Add("idcentrocosto", "@idcentrocosto", "F");
                Upd.Add("idempresa", "@idempresa", "F");
                Upd.Add("codigo", "@codigo", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Add("referencia", "@referencia", "F");
                Upd.Add("control_inventario", "@control_inventario", "F");
                Upd.Where("IdCentroCosto = @IdCentroCosto");

                string sp = Upd.SQL() ?? throw new InvalidOperationException("SQL statement is null");

                bool esTransaccionRemota = pConection != null && pTransaction != null;
                SqlCommand cmd;

                if (esTransaccionRemota)
                {
                    cmd = new SqlCommand(sp, pConection!, pTransaction!);
                }
                else
                {
                    string? connectionString = config.GetConnectionString("CST");
                    if (string.IsNullOrEmpty(connectionString))
                        throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                    lConnection = new SqlConnection(connectionString);
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", oBeCentro_costo.IdCentroCosto));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeCentro_costo.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Codigo", oBeCentro_costo.Codigo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeCentro_costo.Nombre ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeCentro_costo.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeCentro_costo.User_agr ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeCentro_costo.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeCentro_costo.User_mod ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeCentro_costo.Activo));
                cmd.Parameters.Add(new SqlParameter("@referencia", oBeCentro_costo.Referencia ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@control_inventario", oBeCentro_costo.Control_inventario));

                rowsAffected = cmd.ExecuteNonQuery();

                if (!esTransaccionRemota)
                {
                    lTransaction?.Commit();
                }
            }
            catch (SqlException ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            finally
            {
                if (!(pConection != null && pTransaction != null))
                {
                    lTransaction?.Dispose();

                    if (lConnection?.State == ConnectionState.Open)
                        lConnection.Close();
                    lConnection?.Dispose();
                }
            }

            return rowsAffected;
        }

        public static int Eliminar(IConfiguration config, clsBeCentro_costo oBeCentro_costo,
                                  SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (oBeCentro_costo == null) throw new ArgumentNullException(nameof(oBeCentro_costo));

            SqlConnection? lConnection = null;
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM Centro_costo WHERE (IdCentroCosto = @IdCentroCosto)";

                bool esTransaccionRemota = pConection != null && pTransaction != null;
                SqlCommand cmd;

                if (esTransaccionRemota)
                {
                    cmd = new SqlCommand(sp, pConection!, pTransaction!);
                }
                else
                {
                    string? connectionString = config.GetConnectionString("CST");
                    if (string.IsNullOrEmpty(connectionString))
                        throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                    lConnection = new SqlConnection(connectionString);
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", oBeCentro_costo.IdCentroCosto));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (!esTransaccionRemota)
                {
                    lTransaction?.Commit();
                }

                return rowsAffected;
            }
            catch (SqlException ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            finally
            {
                if (!(pConection != null && pTransaction != null))
                {
                    lTransaction?.Dispose();

                    if (lConnection?.State == ConnectionState.Open)
                        lConnection.Close();
                    lConnection?.Dispose();
                }
            }
        }

        public static bool GetSingle(IConfiguration config, ref clsBeCentro_costo pBeCentro_costo)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (pBeCentro_costo == null) throw new ArgumentNullException(nameof(pBeCentro_costo));

            SqlConnection? lConnection = null;
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT * FROM Centro_costo WHERE (IdCentroCosto = @IdCentroCosto)";

                string? connectionString = config.GetConnectionString("CST");
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                lConnection = new SqlConnection(connectionString);
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IdCentroCosto", pBeCentro_costo.IdCentroCosto));

                    using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        dad.Fill(dt);

                        lTransaction.Commit();

                        if (dt.Rows.Count == 1)
                        {
                            DataRow r = dt.Rows[0];
                            Cargar(ref pBeCentro_costo, r);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (SqlException ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            finally
            {
                lTransaction?.Dispose();

                if (lConnection?.State == ConnectionState.Open)
                    lConnection.Close();
                lConnection?.Dispose();
            }
        }

        public static List<clsBeCentro_costo> GetAll(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            List<clsBeCentro_costo> lreturnList = new();

            try
            {
                const string sp = "SELECT * FROM Centro_costo";

                string? connectionString = config.GetConnectionString("CST");
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                using (SqlConnection lConnection = new SqlConnection(connectionString))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                clsBeCentro_costo vBeCentro_costo = new clsBeCentro_costo();
                                Cargar(ref vBeCentro_costo, dr);
                                lreturnList.Add(vBeCentro_costo);
                            }

                            lTransaction.Commit();
                        }
                    }
                }

                return lreturnList;
            }
            catch (SqlException ex)
            {
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
        }

        public static int MaxID(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            try
            {
                const string sp = "SELECT ISNULL(MAX(IdCentroCosto), 0) FROM Centro_costo";

                string? connectionString = config.GetConnectionString("CST");
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                using (SqlConnection lConnection = new SqlConnection(connectionString))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction))
                        {
                            lCommand.CommandType = CommandType.Text;

                            object? lreturnValue = lCommand.ExecuteScalar();
                            lTransaction.Commit();

                            if (lreturnValue != null && lreturnValue != DBNull.Value)
                            {
                                return Convert.ToInt32(lreturnValue);
                            }
                        }
                    }
                }

                return 0;
            }
            catch (SqlException ex)
            {
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
        }

        public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            SqlConnection? lConnection = null;
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "SELECT ISNULL(MAX(IdCentroCosto), 0) FROM Centro_costo";

                bool esTransaccionRemota = pConection != null && pTransaction != null;
                SqlCommand cmd;

                if (esTransaccionRemota)
                {
                    cmd = new SqlCommand(sp, pConection!, pTransaction!);
                }
                else
                {
                    string? connectionString = config.GetConnectionString("CST");
                    if (string.IsNullOrEmpty(connectionString))
                        throw new InvalidOperationException("Connection string 'CST' not found in configuration");

                    lConnection = new SqlConnection(connectionString);
                    lConnection.Open();
                    lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.CommandType = CommandType.Text;

                object? lreturnValue = cmd.ExecuteScalar();

                if (!esTransaccionRemota)
                {
                    lTransaction?.Commit();
                }

                if (lreturnValue != null && lreturnValue != DBNull.Value)
                {
                    return Convert.ToInt32(lreturnValue);
                }

                return 0;
            }
            catch (SqlException ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            catch (Exception ex)
            {
                lTransaction?.Rollback();
                var currentMethodName = MethodBase.GetCurrentMethod()?.Name ?? "UnknownMethod";
                string vMsgError = $"{currentMethodName} {ex.Message}";
                throw new Exception(vMsgError, ex);
            }
            finally
            {
                if (!(pConection != null && pTransaction != null))
                {
                    lTransaction?.Dispose();

                    if (lConnection?.State == ConnectionState.Open)
                        lConnection.Close();
                    lConnection?.Dispose();
                }
            }
        }
    }
}