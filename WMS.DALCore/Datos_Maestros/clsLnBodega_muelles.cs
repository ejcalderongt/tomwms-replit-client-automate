using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Datos_Maestros;
using Microsoft.Extensions.Configuration;
public class clsLnBodega_muelles
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeBodega_muelles oBeBodega_muelles, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }        

        try
        {
            oBeBodega_muelles.IdMuelle = GetInt("IdMuelle");
            oBeBodega_muelles.IdBodega = GetInt("IdBodega");
            oBeBodega_muelles.Codigo_barra = GetString("codigo_barra");
            oBeBodega_muelles.Nombre = GetString("nombre");
            oBeBodega_muelles.User_agr = GetString("user_agr");
            oBeBodega_muelles.Fec_agr = GetDate("fec_agr");
            oBeBodega_muelles.User_mod = GetString("user_mod");
            oBeBodega_muelles.Fec_mod = GetDate("fec_mod");
            oBeBodega_muelles.Color = GetInt("color");
            oBeBodega_muelles.Imagen = GetBytes("imagen");
            oBeBodega_muelles.Activo = GetBool("activo");
            oBeBodega_muelles.Entrada = GetBool("Entrada");
            oBeBodega_muelles.Salida = GetBool("Salida");
            oBeBodega_muelles.IdUbicacionDefecto = GetInt("IdUbicacionDefecto");
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

    public static int Insertar(IConfiguration config, clsBeBodega_muelles oBeBodega_muelles, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;            
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_muelles");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("color", "@color", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("entrada", "@entrada", "F");
            Ins.Add("salida", "@salida", "F");
            Ins.Add("idubicaciondefecto", "@idubicaciondefecto", "F");

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

            Bind(cmd, oBeBodega_muelles);

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

    public static int Insertar(IConfiguration config, clsBeBodega_muelles oBeBodega_muelles)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_muelles");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("color", "@color", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("entrada", "@entrada", "F");
            Ins.Add("salida", "@salida", "F");
            Ins.Add("idubicaciondefecto", "@idubicaciondefecto", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeBodega_muelles);

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

    public static int Actualizar(IConfiguration config, clsBeBodega_muelles oBeBodega_muelles, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("bodega_muelles");
            Upd.Add("idmuelle", "@idmuelle", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("color", "@color", "F");
            Upd.Add("imagen", "@imagen", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("entrada", "@entrada", "F");
            Upd.Add("salida", "@salida", "F");
            Upd.Add("idubicaciondefecto", "@idubicaciondefecto", "F");
            Upd.Where("IdMuelle = @IdMuelle");

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

            Bind(cmd, oBeBodega_muelles);

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

    public int Eliminar(IConfiguration config, clsBeBodega_muelles oBeBodega_muelles, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Bodega_muelles" +
             "  Where(IdMuelle = @IdMuelle)");

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

            cmd.Parameters.Add(new SqlParameter("@IdMuelle", oBeBodega_muelles.IdMuelle));

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
            const string sp = "Select * FROM Bodega_muelles";
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

    public static bool GetSingle(IConfiguration config, ref clsBeBodega_muelles pBeBodega_muelles)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Bodega_muelles" +
            " Where(IdMuelle = @IdMuelle AND IdBodega = @IdBodega)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdMuelle", pBeBodega_muelles.IdMuelle));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega_muelles.IdBodega));           

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeBodega_muelles, r);
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

    public static List<clsBeBodega_muelles> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeBodega_muelles> lreturnList = new List<clsBeBodega_muelles>();

        try
        {
            const string sp = "Select * FROM Bodega_muelles";

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

                        clsBeBodega_muelles vBeBodega_muelles = new clsBeBodega_muelles();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeBodega_muelles = new clsBeBodega_muelles();
                            Cargar(ref vBeBodega_muelles, dr);
                            lreturnList.Add(vBeBodega_muelles);
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

            const string sp = "Select ISNULL(Max(IdMuelle),0) FROM Bodega_muelles";

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


            const string sp = "Select ISNULL(Max(IdMuelle),0) FROM Bodega_muelles";

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
                lMax = int.Parse((String)lreturnValue);
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
    public static void Bind(SqlCommand cmd, clsBeBodega_muelles oBeBodegaMuelles)
    {
        cmd.Parameters.Add(new SqlParameter("@IdMuelle", oBeBodegaMuelles.IdMuelle));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodegaMuelles.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@codigo_barra", (object?)oBeBodegaMuelles.Codigo_barra ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre", (object?)oBeBodegaMuelles.Nombre ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", (object?)oBeBodegaMuelles.User_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", (object?)oBeBodegaMuelles.Fec_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", (object?)oBeBodegaMuelles.User_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", (object?)oBeBodegaMuelles.Fec_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@color",oBeBodegaMuelles.Color==0 ? (object)oBeBodegaMuelles.Color : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@imagen", SqlDbType.Image)
        {
            Value = (object?)oBeBodegaMuelles.Imagen ?? DBNull.Value
        });
        cmd.Parameters.Add(new SqlParameter("@activo",oBeBodegaMuelles.Activo ? (object)oBeBodegaMuelles.Activo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Entrada",oBeBodegaMuelles.Entrada ? (object)oBeBodegaMuelles.Entrada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Salida",(object)oBeBodegaMuelles.Salida ));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionDefecto",oBeBodegaMuelles.IdUbicacionDefecto==0 ? (object)oBeBodegaMuelles.IdUbicacionDefecto : DBNull.Value));
    }
    public static bool Existe(int idMuelle, SqlConnection conn, SqlTransaction? tx)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM bodega_muelles WHERE IdMuelle = @IdMuelle", conn, tx))
        {
            cmd.Parameters.Add(new SqlParameter("@IdMuelle", idMuelle));
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }

    public static int InsertOrUpdate(IConfiguration config, clsBeBodega_muelles entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        if (!isExternalTx)
        {
            connection.Open();
            localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            if (Existe(entity.IdMuelle, connection, isExternalTx ? tx! : localTx!))
                return Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
            else
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

    public static int Get_IdMuelle_Default_By_IdBodega(int IdBodega,
                                                   SqlConnection lConnection,
                                                   SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT TOP(1) IdMuelle FROM Bodega_muelles 
                            WHERE (IdBodega = @IdBodega)";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDBODEGA", IdBodega));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0]["IdMuelle"]);
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}