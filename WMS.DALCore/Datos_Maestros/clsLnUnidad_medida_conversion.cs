using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnUnidad_medida_conversion
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeUnidad_medida_conversion oBeUnidad_medida_conversion, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeUnidad_medida_conversion.IdConversion = GetInt("IdConversion");
            oBeUnidad_medida_conversion.IdUnidadMedidaOrigen = GetInt("IdUnidadMedidaOrigen");
            oBeUnidad_medida_conversion.IdUnidadMedidaDestino = GetInt("IdUnidadMedidaDestino");
            oBeUnidad_medida_conversion.Factor = GetDouble("Factor");
            oBeUnidad_medida_conversion.Activo = GetBool("activo");
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

    public static int Insertar(IConfiguration config, clsBeUnidad_medida_conversion oBeUnidad_medida_conversion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("unidad_medida_conversion");
            Ins.Add("idconversion", "@idconversion", "F");
            Ins.Add("idunidadmedidaorigen", "@idunidadmedidaorigen", "F");
            Ins.Add("idunidadmedidadestino", "@idunidadmedidadestino", "F");
            Ins.Add("factor", "@factor", "F");
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

            cmd.Parameters.Add(new SqlParameter("@IdConversion", oBeUnidad_medida_conversion.IdConversion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", oBeUnidad_medida_conversion.IdUnidadMedidaOrigen));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", oBeUnidad_medida_conversion.IdUnidadMedidaDestino));
            cmd.Parameters.Add(new SqlParameter("@Factor", oBeUnidad_medida_conversion.Factor));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeUnidad_medida_conversion.Activo));

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

    public static int Insertar(IConfiguration config, clsBeUnidad_medida_conversion oBeUnidad_medida_conversion)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("unidad_medida_conversion");
            Ins.Add("idconversion", "@idconversion", "F");
            Ins.Add("idunidadmedidaorigen", "@idunidadmedidaorigen", "F");
            Ins.Add("idunidadmedidadestino", "@idunidadmedidadestino", "F");
            Ins.Add("factor", "@factor", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdConversion", oBeUnidad_medida_conversion.IdConversion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", oBeUnidad_medida_conversion.IdUnidadMedidaOrigen));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", oBeUnidad_medida_conversion.IdUnidadMedidaDestino));
            cmd.Parameters.Add(new SqlParameter("@Factor", oBeUnidad_medida_conversion.Factor));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeUnidad_medida_conversion.Activo));

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

    public static int Actualizar(IConfiguration config, clsBeUnidad_medida_conversion oBeUnidad_medida_conversion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("unidad_medida_conversion");
            Upd.Add("idconversion", "@idconversion", "F");
            Upd.Add("idunidadmedidaorigen", "@idunidadmedidaorigen", "F");
            Upd.Add("idunidadmedidadestino", "@idunidadmedidadestino", "F");
            Upd.Add("factor", "@factor", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdConversion = @IdConversion");

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

            cmd.Parameters.Add(new SqlParameter("@IdConversion", oBeUnidad_medida_conversion.IdConversion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", oBeUnidad_medida_conversion.IdUnidadMedidaOrigen));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", oBeUnidad_medida_conversion.IdUnidadMedidaDestino));
            cmd.Parameters.Add(new SqlParameter("@Factor", oBeUnidad_medida_conversion.Factor));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeUnidad_medida_conversion.Activo));

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

    public int Eliminar(IConfiguration config, clsBeUnidad_medida_conversion oBeUnidad_medida_conversion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Unidad_medida_conversion" +
             "  Where(IdConversion = @IdConversion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdConversion", oBeUnidad_medida_conversion.IdConversion));

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

    public static bool GetSingle(IConfiguration config, ref clsBeUnidad_medida_conversion pBeUnidad_medida_conversion)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Unidad_medida_conversion" +
            " Where(IdConversion = @IdConversion)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdConversion", pBeUnidad_medida_conversion.IdConversion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", pBeUnidad_medida_conversion.IdUnidadMedidaOrigen));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", pBeUnidad_medida_conversion.IdUnidadMedidaDestino));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Factor", pBeUnidad_medida_conversion.Factor));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeUnidad_medida_conversion.Activo));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeUnidad_medida_conversion, r);
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

    public static List<clsBeUnidad_medida_conversion> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeUnidad_medida_conversion> lreturnList = new List<clsBeUnidad_medida_conversion>();

        try
        {
            const string sp = "Select * FROM Unidad_medida_conversion";
            
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

                        clsBeUnidad_medida_conversion vBeUnidad_medida_conversion = new clsBeUnidad_medida_conversion();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeUnidad_medida_conversion = new clsBeUnidad_medida_conversion();
                            Cargar(ref vBeUnidad_medida_conversion, dr);
                            lreturnList.Add(vBeUnidad_medida_conversion);
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

            const string sp = "Select ISNULL(Max(IdConversion),0) FROM Unidad_medida_conversion";

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


            const string sp = "Select ISNULL(Max(IdConversion),0) FROM Unidad_medida_conversion";

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
    public static bool Existe_Factor(int IdUnidadMedidaOrigen, int IdUnidadMedidaDestino,
                                      ref SqlConnection lConection,
                                      ref SqlTransaction lTransaction)
    {
        bool existeFactor = false;

        try
        {
            const string sp = @"SELECT * FROM unidad_medida_conversion 
                                WHERE (IdUnidadMedidaOrigen = @IdUnidadMedidaOrigen 
                                AND IdUnidadMedidaDestino = @IdUnidadMedidaDestino)";

            SqlCommand cmd = new SqlCommand(sp, lConection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", IdUnidadMedidaOrigen));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", IdUnidadMedidaDestino));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            existeFactor = (dt.Rows.Count > 0);

            return existeFactor;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static double Get_Factor(int IdUnidadMedidaOrigen,
                                   int IdUnidadMedidaDestino,
                                   SqlConnection lConection,
                                   SqlTransaction lTransaction)
    {
        double factor = 0;

        try
        {
            const string sp = @"SELECT Factor FROM unidad_medida_conversion 
                                WHERE (IdUnidadMedidaOrigen = @IdUnidadMedidaOrigen 
                                AND IdUnidadMedidaDestino = @IdUnidadMedidaDestino)";

            SqlCommand cmd = new SqlCommand(sp, lConection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaOrigen", IdUnidadMedidaOrigen));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaDestino", IdUnidadMedidaDestino));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                factor = dt.Rows[0]["Factor"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["Factor"]);
            }

            return factor;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
