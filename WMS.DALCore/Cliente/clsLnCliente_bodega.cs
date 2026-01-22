using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Cliente;

public class clsLnCliente_bodega
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeCliente_bodega oBeCliente_bodega, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        //byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }
        //decimal GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDecimal(dr[col]); }

        try
        {
            oBeCliente_bodega.IdClienteBodega = GetInt("IdClienteBodega");
            oBeCliente_bodega.IdBodega = GetInt("IdBodega");
            oBeCliente_bodega.IdCliente = GetInt("IdCliente");
            oBeCliente_bodega.User_agr = GetString("user_agr");
            oBeCliente_bodega.Fec_agr = GetDate("fec_agr");
            oBeCliente_bodega.User_mod = GetString("user_mod");
            oBeCliente_bodega.Fec_mod = GetDate("fec_mod");
            oBeCliente_bodega.Activo = GetBool("activo");
            oBeCliente_bodega.IdAreaDestino = GetInt("IdAreaDestino");
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

    public static void BindClienteBodegaParameters(SqlCommand cmd, clsBeCliente_bodega oBeCliente_bodega)
    {


        cmd.Parameters.Clear();
        cmd.Parameters.Add(new SqlParameter("@IdClienteBodega", oBeCliente_bodega.IdClienteBodega));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeCliente_bodega.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@IdCliente", oBeCliente_bodega.IdCliente));
        cmd.Parameters.Add(new SqlParameter("@user_agr", (object?)oBeCliente_bodega.User_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeCliente_bodega.Fec_agr == DateTime.MinValue ? DBNull.Value : oBeCliente_bodega.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", (object?)oBeCliente_bodega.User_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeCliente_bodega.Fec_mod == DateTime.MinValue ? DBNull.Value : oBeCliente_bodega.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeCliente_bodega.Activo));
        cmd.Parameters.Add(new SqlParameter("@IdAreaDestino", oBeCliente_bodega.IdAreaDestino));
    }
    public static int Insertar(clsBeCliente_bodega oBeCliente_bodega, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeCliente_bodega == null)
            throw new ArgumentNullException(nameof(oBeCliente_bodega));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("cliente_bodega");
            Ins.Add("idclientebodega", "@idclientebodega", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idareadestino", "@idareadestino", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindClienteBodegaParameters(cmd, oBeCliente_bodega);

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

    public static int Insertar(IConfiguration config, clsBeCliente_bodega beClienteBodega, SqlConnection connection, clsBeCliente_bodega oBeCliente_bodega)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("cliente_bodega");
            Ins.Add("idclientebodega", "@idclientebodega", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idareadestino", "@idareadestino", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindClienteBodegaParameters(cmd, oBeCliente_bodega);

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

    public static int Actualizar(IConfiguration config, clsBeCliente_bodega oBeCliente_bodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("cliente_bodega");
            Upd.Add("idclientebodega", "@idclientebodega", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idcliente", "@idcliente", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idareadestino", "@idareadestino", "F");
            Upd.Where("IdClienteBodega = @IdClienteBodega");

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

            BindClienteBodegaParameters(cmd, oBeCliente_bodega);

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

    public int Eliminar(IConfiguration config, clsBeCliente_bodega oBeCliente_bodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Cliente_bodega" +
             "  Where(IdClienteBodega = @IdClienteBodega)");

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

            cmd.Parameters.Add(new SqlParameter("@IdClienteBodega", oBeCliente_bodega.IdClienteBodega));

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
            const string sp = "Select * FROM Cliente_bodega";
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

    public static bool GetSingle(IConfiguration config, ref clsBeCliente_bodega pBeCliente_bodega)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Cliente_bodega" +
            " Where(IdClienteBodega = @IdClienteBodega)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdClienteBodega", pBeCliente_bodega.IdClienteBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeCliente_bodega.IdBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdCliente", pBeCliente_bodega.IdCliente));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeCliente_bodega.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeCliente_bodega.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeCliente_bodega.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeCliente_bodega.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeCliente_bodega.Activo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdAreaDestino", pBeCliente_bodega.IdAreaDestino));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeCliente_bodega, r);
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

    public static List<clsBeCliente_bodega> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeCliente_bodega> lreturnList = new List<clsBeCliente_bodega>();

        try
        {
            const string sp = "Select * FROM Cliente_bodega";

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

                        clsBeCliente_bodega vBeCliente_bodega = new clsBeCliente_bodega();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeCliente_bodega = new clsBeCliente_bodega();
                            Cargar(ref vBeCliente_bodega, dr);
                            lreturnList.Add(vBeCliente_bodega);
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

            const string sp = "Select ISNULL(Max(IdClienteBodega),0) FROM Cliente_bodega";

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
            const string sp = "Select ISNULL(Max(IdClienteBodega),0) FROM Cliente_bodega";

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

}
