using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_re;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_re_tr
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_tr oBeTrans_re_tr, DataRow dr)
    {
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }

        try
        {
            oBeTrans_re_tr.IdTipoTransaccion = GetString("IdTipoTransaccion");
            oBeTrans_re_tr.Descripcion = GetString("Descripcion");
            oBeTrans_re_tr.Funcionalidad = GetString("Funcionalidad");
            oBeTrans_re_tr.UsaHH = GetBool("UsaHH");
            oBeTrans_re_tr.DescDev = GetString("DescDev");
            oBeTrans_re_tr.TipoTrans = GetString("TipoTrans");
            oBeTrans_re_tr.ConRef = GetBool("ConRef");
            oBeTrans_re_tr.Activo = GetBool("Activo");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);

            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(IConfiguration config, clsBeTrans_re_tr oBeTrans_re_tr, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_tr");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("funcionalidad", "@funcionalidad", "F");
            Ins.Add("usahh", "@usahh", "F");
            Ins.Add("descdev", "@descdev", "F");
            Ins.Add("tipotrans", "@tipotrans", "F");
            Ins.Add("conref", "@conref", "F");
            Ins.Add("activo", "@activo", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", oBeTrans_re_tr.IdTipoTransaccion));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTrans_re_tr.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@Funcionalidad", oBeTrans_re_tr.Funcionalidad));
            cmd.Parameters.Add(new SqlParameter("@UsaHH", oBeTrans_re_tr.UsaHH));
            cmd.Parameters.Add(new SqlParameter("@DescDev", oBeTrans_re_tr.DescDev));
            cmd.Parameters.Add(new SqlParameter("@TipoTrans", oBeTrans_re_tr.TipoTrans));
            cmd.Parameters.Add(new SqlParameter("@ConRef", oBeTrans_re_tr.ConRef));
            cmd.Parameters.Add(new SqlParameter("@Activo", oBeTrans_re_tr.Activo));

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

    public static int Insertar(IConfiguration config, clsBeTrans_re_tr oBeTrans_re_tr)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_tr");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("funcionalidad", "@funcionalidad", "F");
            Ins.Add("usahh", "@usahh", "F");
            Ins.Add("descdev", "@descdev", "F");
            Ins.Add("tipotrans", "@tipotrans", "F");
            Ins.Add("conref", "@conref", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindParameters(cmd, oBeTrans_re_tr);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_re_tr oBeTrans_re_tr, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_re_tr");
            Upd.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("funcionalidad", "@funcionalidad", "F");
            Upd.Add("usahh", "@usahh", "F");
            Upd.Add("descdev", "@descdev", "F");
            Upd.Add("tipotrans", "@tipotrans", "F");
            Upd.Add("conref", "@conref", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdTipoTransaccion = @IdTipoTransaccion");

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

            BindParameters(cmd, oBeTrans_re_tr);

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

    public int Eliminar(IConfiguration config, clsBeTrans_re_tr oBeTrans_re_tr, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_tr" +
             "  Where(IdTipoTransaccion = @IdTipoTransaccion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", oBeTrans_re_tr.IdTipoTransaccion));

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
            const string sp = "Select * FROM Trans_re_tr";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_tr pBeTrans_re_tr)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_tr" +
            " Where(IdTipoTransaccion = @IdTipoTransaccion)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoTransaccion", pBeTrans_re_tr.IdTipoTransaccion));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_tr, r);
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

    public static List<clsBeTrans_re_tr> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_tr> lreturnList = new List<clsBeTrans_re_tr>();

        try
        {
            const string sp = "Select * FROM Trans_re_tr";

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

                        clsBeTrans_re_tr vBeTrans_re_tr = new clsBeTrans_re_tr();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_tr = new clsBeTrans_re_tr();
                            Cargar(ref vBeTrans_re_tr, dr);
                            lreturnList.Add(vBeTrans_re_tr);
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

            const string sp = "Select ISNULL(Max(IdTipoTransaccion),0) FROM Trans_re_tr";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        var lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((string)lreturnValue);
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


            const string sp = "Select ISNULL(Max(IdTipoTransaccion),0) FROM Trans_re_tr";

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

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
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
    public static void BindParameters(SqlCommand cmd, clsBeTrans_re_tr oBeTrans_re_tr)
    {
        cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", string.IsNullOrEmpty(oBeTrans_re_tr.IdTipoTransaccion) ? DBNull.Value : oBeTrans_re_tr.IdTipoTransaccion));
        cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(oBeTrans_re_tr.Descripcion) ? DBNull.Value : oBeTrans_re_tr.Descripcion));
        cmd.Parameters.Add(new SqlParameter("@Funcionalidad", string.IsNullOrEmpty(oBeTrans_re_tr.Funcionalidad) ? DBNull.Value : oBeTrans_re_tr.Funcionalidad));
        cmd.Parameters.Add(new SqlParameter("@UsaHH", oBeTrans_re_tr.UsaHH));
        cmd.Parameters.Add(new SqlParameter("@DescDev", string.IsNullOrEmpty(oBeTrans_re_tr.DescDev) ? DBNull.Value : oBeTrans_re_tr.DescDev));
        cmd.Parameters.Add(new SqlParameter("@TipoTrans", string.IsNullOrEmpty(oBeTrans_re_tr.TipoTrans) ? DBNull.Value : oBeTrans_re_tr.TipoTrans));
        cmd.Parameters.Add(new SqlParameter("@ConRef", oBeTrans_re_tr.ConRef));
        cmd.Parameters.Add(new SqlParameter("@Activo", oBeTrans_re_tr.Activo));
    }
    public static void InsertarOActualizar(IConfiguration config, clsBeTrans_re_tr entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            bool existe = Existe(entity.IdTipoTransaccion, connection, isExternalTx ? tx! : localTx!);

            if (existe)
                Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
            else
                Insertar(config, entity, connection, isExternalTx ? tx : localTx);

            if (!isExternalTx)
                localTx?.Commit();
        }
        catch (SqlException ex)
        {
            if (!isExternalTx && localTx is not null)
                localTx.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
                localTx?.Dispose();
            }
        }
    }
    public static bool Existe(string idTipoTransaccion, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM trans_re_tr WHERE IdTipoTransaccion = @IdTipoTransaccion";

            using var cmd = new SqlCommand(query, conn, tx);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", idTipoTransaccion));

            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }
        catch (SqlException ex)
        {
            var sf = new StackTrace().GetFrame(0);
            var method = sf?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeTrans_re_tr> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            foreach (var entity in entities)
            {

                if (string.IsNullOrEmpty(entity.IdTipoTransaccion)) {

                    bool existe = Existe(entity.IdTipoTransaccion, connection, isExternalTx ? tx! : localTx!);

                    if (existe)
                        Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
                    else
                        Insertar(config, entity, connection, isExternalTx ? tx : localTx);
                }
               
            }

            if (!isExternalTx)
                localTx?.Commit();
        }
        catch (SqlException ex)
        {
            if (!isExternalTx && localTx is not null)
                localTx.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
                localTx?.Dispose();
            }
        }
    }
}