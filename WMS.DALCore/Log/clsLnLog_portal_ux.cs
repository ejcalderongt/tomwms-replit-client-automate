using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Log;
using Microsoft.Extensions.Configuration;

public class clsLnLog_portal_ux
{
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeLog_portal_ux oBelog_portal_ux, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        
        try
        {
            oBelog_portal_ux.LogUxId = GetInt("LogUxId");
            oBelog_portal_ux.Idpropietario = GetInt("Idpropietario");
            oBelog_portal_ux.Usuario = GetString("Usuario");
            oBelog_portal_ux.Email = GetString("Email");
            oBelog_portal_ux.Fecha = GetDate("Fecha");
            oBelog_portal_ux.IPAddress = GetString("IPAddress");
            oBelog_portal_ux.UserAgent = GetString("UserAgent");
            oBelog_portal_ux.Acceso = GetBool("Acceso");
            oBelog_portal_ux.MensajeError = GetString("MensajeError");
            oBelog_portal_ux.UrlAcceso = GetString("UrlAcceso");
        }
        catch (Exception ex)
        {
            var st = new System.Diagnostics.StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);
            
            throw new Exception(vMsgError);
        }
    }


    public static void BindingLogParameters(SqlCommand cmd, clsBeLog_portal_ux oBelog_portal_ux)
    {
      
        cmd.Parameters.Add(new SqlParameter("@LogUxId", oBelog_portal_ux.LogUxId));
        cmd.Parameters.Add(new SqlParameter("@Idpropietario", oBelog_portal_ux.Idpropietario));
        cmd.Parameters.Add(new SqlParameter("@Usuario", oBelog_portal_ux.Usuario));
        cmd.Parameters.Add(new SqlParameter("@Email", oBelog_portal_ux.Email ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Fecha", oBelog_portal_ux.Fecha));
        cmd.Parameters.Add(new SqlParameter("@IPAddress", oBelog_portal_ux.IPAddress ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@UserAgent", oBelog_portal_ux.UserAgent ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Acceso", oBelog_portal_ux.Acceso));
        cmd.Parameters.Add(new SqlParameter("@MensajeError", oBelog_portal_ux.MensajeError ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@UrlAcceso", oBelog_portal_ux.UrlAcceso ?? (object)DBNull.Value));
    }

    public static int _InsertOrUpdate(IConfiguration config, clsBeLog_portal_ux entity)
    {
        using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
        {
            lConnection.Open();
            using (SqlTransaction lTransaction = lConnection.BeginTransaction())
            {
                try
                {
                    entity.LogUxId = MaxID(config, lConnection, lTransaction) + 1;

                    int result = Insertar(config, entity, lConnection, lTransaction);

                    lTransaction.Commit();
                    return result;
                }
                catch (SqlException)
                {
                    lTransaction.Rollback();
                    // Silencioso: no lanzamos excepción para no afectar el login
                    return -1;
                }
                catch (Exception)
                {
                    lTransaction.Rollback();
                    return -1;
                }
            }
        }
    }


    public static int InsertOrUpdate(IConfiguration config, clsBeLog_portal_ux entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
                entity.LogUxId = MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
            
                return Insertar(config, entity, connection, isExternalTx ? tx : localTx);
        }
        catch
        {
            if (!isExternalTx) localTx?.Rollback();
            throw;
        }
        finally
        {
            if (!isExternalTx)
            {
                localTx?.Commit();
                connection.Close();
            }
        }
    }

    public static int Insertar(IConfiguration config, clsBeLog_portal_ux oBelog_portal_ux, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("log_portal_ux");
            Ins.Add("loguxid", "@loguxid", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("Usuario", "@Usuario", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("ipaddress", "@ipaddress", "F");
            Ins.Add("useragent", "@useragent", "F");
            Ins.Add("acceso", "@acceso", "F");
            Ins.Add("mensajeerror", "@mensajeerror", "F");
            Ins.Add("urlacceso", "@urlacceso", "F");

            string sp = Ins.SQL();

            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            BindingLogParameters(cmd, oBelog_portal_ux);
            
            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();

            if (!Es_Transaccion_Remota)
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
            if (lConnection is not null) lConnection.Dispose();
            if (lTransaction is not null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeLog_portal_ux oBelog_portal_ux)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("log_portal_ux");
            Ins.Add("loguxid", "@loguxid", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("Usuario", "@Usuario", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("ipaddress", "@ipaddress", "F");
            Ins.Add("useragent", "@useragent", "F");
            Ins.Add("acceso", "@acceso", "F");
            Ins.Add("mensajeerror", "@mensajeerror", "F");
            Ins.Add("urlacceso", "@urlacceso", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindingLogParameters(cmd, oBelog_portal_ux);

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

    public static int Actualizar(IConfiguration config, clsBeLog_portal_ux oBelog_portal_ux, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("log_portal_ux");
            Upd.Add("loguxid", "@loguxid", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("Usuario", "@Usuario", "F");
            Upd.Add("email", "@email", "F");
            Upd.Add("fecha", "@fecha", "F");
            Upd.Add("ipaddress", "@ipaddress", "F");
            Upd.Add("useragent", "@useragent", "F");
            Upd.Add("acceso", "@acceso", "F");
            Upd.Add("mensajeerror", "@mensajeerror", "F");
            Upd.Add("urlacceso", "@urlacceso", "F");
            Upd.Where("LogUxId = @LogUxId");

            string sp = Upd.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            BindingLogParameters(cmd, oBelog_portal_ux);

            rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
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

    public int Eliminar(IConfiguration config, clsBeLog_portal_ux oBelog_portal_ux, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from log_portal_ux" +
             "  Where(LogUxId = @LogUxId)");

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            cmd.Parameters.Add(new SqlParameter("@LogUxId", oBelog_portal_ux.LogUxId));

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
            const string sp = "Select * FROM log_portal_ux";
            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
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

    public static bool GetSingle(IConfiguration config, ref clsBeLog_portal_ux pBelog_portal_ux)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM log_portal_ux" +
            " Where(LogUxId = @LogUxId)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@LogUxId", pBelog_portal_ux.LogUxId));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Idpropietario", pBelog_portal_ux.Idpropietario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Usuario", pBelog_portal_ux.Usuario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Email", pBelog_portal_ux.Email));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Fecha", pBelog_portal_ux.Fecha));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IPAddress", pBelog_portal_ux.IPAddress));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@UserAgent", pBelog_portal_ux.UserAgent));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Acceso", pBelog_portal_ux.Acceso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@MensajeError", pBelog_portal_ux.MensajeError));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@UrlAcceso", pBelog_portal_ux.UrlAcceso));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBelog_portal_ux, r);
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

    public static List<clsBeLog_portal_ux> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeLog_portal_ux> lreturnList = new List<clsBeLog_portal_ux>();

        try
        {
            const string sp = "Select * FROM log_portal_ux";

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

                        clsBeLog_portal_ux vBelog_portal_ux = new clsBeLog_portal_ux();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBelog_portal_ux = new clsBeLog_portal_ux();
                            Cargar(ref vBelog_portal_ux, dr);
                            lreturnList.Add(vBelog_portal_ux);
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

            const string sp = "Select ISNULL(Max(LogUxId),0) FROM log_portal_ux";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        Object lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((String)lreturnValue);
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
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(LogUxId),0) FROM log_portal_ux";

            bool Es_Transaccion_Remota = pConection is not null && pTransaction is not null;
            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };
            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            Object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = Convert.ToInt32(lreturnValue);
            }

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

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

}

