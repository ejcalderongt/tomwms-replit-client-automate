using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Entity.Despacho;

public class clsLnTrans_despacho_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_despacho_enc oBeTrans_despacho_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_despacho_enc.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeTrans_despacho_enc.IdBodega = GetInt("IdBodega");
            oBeTrans_despacho_enc.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_despacho_enc.IdVehiculo = GetInt("IdVehiculo");
            oBeTrans_despacho_enc.IdPiloto = GetInt("IdPiloto");
            oBeTrans_despacho_enc.IdRuta = GetInt("IdRuta");
            oBeTrans_despacho_enc.Fecha = GetDate("fecha");
            oBeTrans_despacho_enc.No_pase = GetInt("no_pase");
            oBeTrans_despacho_enc.Observacion = GetString("observacion");
            oBeTrans_despacho_enc.Hora_ini = GetDate("hora_ini");
            oBeTrans_despacho_enc.Hora_fin = GetDate("hora_fin");
            oBeTrans_despacho_enc.Estado = GetString("estado");
            oBeTrans_despacho_enc.Numero = GetInt("numero");
            oBeTrans_despacho_enc.Marchamo = GetString("marchamo");
            oBeTrans_despacho_enc.Cant_bultos = GetDouble("cant_bultos");
            oBeTrans_despacho_enc.User_agr = GetString("user_agr");
            oBeTrans_despacho_enc.Fec_agr = GetDate("fec_agr");
            oBeTrans_despacho_enc.User_mod = GetString("user_mod");
            oBeTrans_despacho_enc.Fec_mod = GetDate("fec_mod");
            oBeTrans_despacho_enc.Activo = GetBool("activo");
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

    public static int Insertar(IConfiguration config, clsBeTrans_despacho_enc oBeTrans_despacho_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_despacho_enc");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idvehiculo", "@idvehiculo", "F");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("idruta", "@idruta", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("no_pase", "@no_pase", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("numero", "@numero", "F");
            Ins.Add("marchamo", "@marchamo", "F");
            Ins.Add("cant_bultos", "@cant_bultos", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_despacho_enc.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_despacho_enc.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeTrans_despacho_enc.IdVehiculo));
            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeTrans_despacho_enc.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdRuta", oBeTrans_despacho_enc.IdRuta));
            cmd.Parameters.Add(new SqlParameter("@fecha", oBeTrans_despacho_enc.Fecha));
            cmd.Parameters.Add(new SqlParameter("@no_pase", oBeTrans_despacho_enc.No_pase));
            cmd.Parameters.Add(new SqlParameter("@observacion", oBeTrans_despacho_enc.Observacion));
            cmd.Parameters.Add(new SqlParameter("@hora_ini", oBeTrans_despacho_enc.Hora_ini));
            cmd.Parameters.Add(new SqlParameter("@hora_fin", oBeTrans_despacho_enc.Hora_fin));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_despacho_enc.Estado));
            cmd.Parameters.Add(new SqlParameter("@numero", oBeTrans_despacho_enc.Numero));
            cmd.Parameters.Add(new SqlParameter("@marchamo", oBeTrans_despacho_enc.Marchamo));
            cmd.Parameters.Add(new SqlParameter("@cant_bultos", oBeTrans_despacho_enc.Cant_bultos));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_enc.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_enc.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_enc.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_enc.Activo));

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

    public static int Insertar(IConfiguration config, clsBeTrans_despacho_enc oBeTrans_despacho_enc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_despacho_enc");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idvehiculo", "@idvehiculo", "F");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("idruta", "@idruta", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("no_pase", "@no_pase", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("numero", "@numero", "F");
            Ins.Add("marchamo", "@marchamo", "F");
            Ins.Add("cant_bultos", "@cant_bultos", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_despacho_enc.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_despacho_enc.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeTrans_despacho_enc.IdVehiculo));
            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeTrans_despacho_enc.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdRuta", oBeTrans_despacho_enc.IdRuta));
            cmd.Parameters.Add(new SqlParameter("@fecha", oBeTrans_despacho_enc.Fecha));
            cmd.Parameters.Add(new SqlParameter("@no_pase", oBeTrans_despacho_enc.No_pase));
            cmd.Parameters.Add(new SqlParameter("@observacion", oBeTrans_despacho_enc.Observacion));
            cmd.Parameters.Add(new SqlParameter("@hora_ini", oBeTrans_despacho_enc.Hora_ini));
            cmd.Parameters.Add(new SqlParameter("@hora_fin", oBeTrans_despacho_enc.Hora_fin));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_despacho_enc.Estado));
            cmd.Parameters.Add(new SqlParameter("@numero", oBeTrans_despacho_enc.Numero));
            cmd.Parameters.Add(new SqlParameter("@marchamo", oBeTrans_despacho_enc.Marchamo));
            cmd.Parameters.Add(new SqlParameter("@cant_bultos", oBeTrans_despacho_enc.Cant_bultos));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_enc.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_enc.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_enc.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_enc.Activo));

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

    public static int Actualizar(IConfiguration config, clsBeTrans_despacho_enc oBeTrans_despacho_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_despacho_enc");
            Upd.Add("iddespachoenc", "@iddespachoenc", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idvehiculo", "@idvehiculo", "F");
            Upd.Add("idpiloto", "@idpiloto", "F");
            Upd.Add("idruta", "@idruta", "F");
            Upd.Add("fecha", "@fecha", "F");
            Upd.Add("no_pase", "@no_pase", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Add("hora_ini", "@hora_ini", "F");
            Upd.Add("hora_fin", "@hora_fin", "F");
            Upd.Add("estado", "@estado", "F");
            Upd.Add("numero", "@numero", "F");
            Upd.Add("marchamo", "@marchamo", "F");
            Upd.Add("cant_bultos", "@cant_bultos", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdDespachoEnc = @IdDespachoEnc");

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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_despacho_enc.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_despacho_enc.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeTrans_despacho_enc.IdVehiculo));
            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeTrans_despacho_enc.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdRuta", oBeTrans_despacho_enc.IdRuta));
            cmd.Parameters.Add(new SqlParameter("@fecha", oBeTrans_despacho_enc.Fecha));
            cmd.Parameters.Add(new SqlParameter("@no_pase", oBeTrans_despacho_enc.No_pase));
            cmd.Parameters.Add(new SqlParameter("@observacion", oBeTrans_despacho_enc.Observacion));
            cmd.Parameters.Add(new SqlParameter("@hora_ini", oBeTrans_despacho_enc.Hora_ini));
            cmd.Parameters.Add(new SqlParameter("@hora_fin", oBeTrans_despacho_enc.Hora_fin));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_despacho_enc.Estado));
            cmd.Parameters.Add(new SqlParameter("@numero", oBeTrans_despacho_enc.Numero));
            cmd.Parameters.Add(new SqlParameter("@marchamo", oBeTrans_despacho_enc.Marchamo));
            cmd.Parameters.Add(new SqlParameter("@cant_bultos", oBeTrans_despacho_enc.Cant_bultos));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_enc.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_enc.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_enc.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_enc.Activo));

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

    public int Eliminar(IConfiguration config, clsBeTrans_despacho_enc oBeTrans_despacho_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_despacho_enc" +
             "  Where(IdDespachoEnc = @IdDespachoEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_despacho_enc pBeTrans_despacho_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_despacho_enc" +
            " Where(IdDespachoEnc = @IdDespachoEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdDespachoEnc", pBeTrans_despacho_enc.IdDespachoEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_despacho_enc, r);
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

    public static List<clsBeTrans_despacho_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_despacho_enc> lreturnList = new List<clsBeTrans_despacho_enc>();

        try
        {
            const string sp = "Select * FROM Trans_despacho_enc";

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

                        clsBeTrans_despacho_enc vBeTrans_despacho_enc = new clsBeTrans_despacho_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_despacho_enc = new clsBeTrans_despacho_enc();
                            Cargar(ref vBeTrans_despacho_enc, dr);
                            lreturnList.Add(vBeTrans_despacho_enc);
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

            const string sp = "Select ISNULL(Max(IdDespachoEnc),0) FROM Trans_despacho_enc";

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


            const string sp = "Select ISNULL(Max(IdDespachoEnc),0) FROM Trans_despacho_enc";

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

    public static List<clsBeTrans_despacho_enc> Get_All_By_IdPedidoEnc(IConfiguration config, int idPedidoEnc, SqlConnection? pConnection = null, SqlTransaction? pTransaction = null)
    {
        var listTransDespachoEnc = new List<clsBeTrans_despacho_enc>();
        SqlConnection? lConnection = null;
        SqlTransaction? lTransaction = null;

        try
        {
            const string query = @"
            SELECT 
                enc.IdDespachoEnc, enc.IdBodega, enc.IdPropietarioBodega, enc.IdVehiculo, 
                enc.IdPiloto, enc.IdRuta, enc.fecha, enc.no_pase, enc.observacion, enc.hora_ini, 
                enc.hora_fin, enc.estado, enc.numero, enc.marchamo, enc.cant_bultos, enc.user_agr, 
                enc.fec_agr, enc.user_mod, enc.fec_mod, enc.activo, enc.no_documento_externo
            FROM trans_pe_det det
            INNER JOIN trans_pe_enc ped ON det.IdPedidoEnc = ped.IdPedidoEnc
            INNER JOIN trans_despacho_det dd ON det.IdPedidoDet = dd.IdPedidoDet
            INNER JOIN trans_despacho_enc enc ON dd.IdDespachoEnc = enc.IdDespachoEnc
            WHERE ped.IdPedidoEnc = @IdPedidoEnc
            GROUP BY 
                enc.IdDespachoEnc, enc.IdBodega, enc.IdPropietarioBodega, enc.IdVehiculo, 
                enc.IdPiloto, enc.IdRuta, enc.fecha, enc.no_pase, enc.observacion, enc.hora_ini, 
                enc.hora_fin, enc.estado, enc.numero, enc.marchamo, enc.cant_bultos, enc.user_agr, 
                enc.fec_agr, enc.user_mod, enc.fec_mod, enc.activo, enc.no_documento_externo
            ORDER BY enc.fecha ASC";

            bool externalTransaction = pConnection is not null && pTransaction is not null;

            if (externalTransaction)
            {
                lConnection = pConnection;
                lTransaction = pTransaction;
            }
            else
            {
                lConnection = new SqlConnection(config.GetConnectionString("CST"));
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var adapter = new SqlDataAdapter(query, lConnection);
            adapter.SelectCommand.Transaction = lTransaction;
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);

            var dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var item = new clsBeTrans_despacho_enc();
                    Cargar(ref item, row);
                    item.Detalle = clsLnTrans_despacho_det.Get_All_By_IdPedidoEnc(config, idPedidoEnc, item.IdDespachoEnc, pConnection, pTransaction);
                    listTransDespachoEnc.Add(item);
                }
            }

            if (!externalTransaction && lTransaction != null)
                lTransaction.Commit();

            if (!externalTransaction && lConnection != null)
                lConnection.Close();

            return listTransDespachoEnc;
        }
        catch (Exception ex)
        {
            if (lTransaction is not null && pTransaction is null)
                lTransaction.Rollback();

            throw new Exception($"Get_All_By_IdPedidoEnc → {ex.Message}", ex);
        }
    }

}