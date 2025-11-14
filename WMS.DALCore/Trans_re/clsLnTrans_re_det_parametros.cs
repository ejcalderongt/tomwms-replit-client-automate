using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;

public class clsLnTrans_re_det_parametros
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_det_parametros oBeTrans_re_det_parametros, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_re_det_parametros.IdParametroDet = GetInt("IdParametroDet");
            oBeTrans_re_det_parametros.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeTrans_re_det_parametros.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_det_parametros.IdProductoParametro = GetInt("IdProductoParametro");
            oBeTrans_re_det_parametros.Valor_texto = GetString("valor_texto");
            oBeTrans_re_det_parametros.Valor_numerico = GetDecimal("valor_numerico");
            oBeTrans_re_det_parametros.Valor_fecha = GetDate("valor_fecha");
            oBeTrans_re_det_parametros.Valor_logico = GetBool("valor_logico");
            oBeTrans_re_det_parametros.User_agr = GetString("user_agr");
            oBeTrans_re_det_parametros.Fec_agr = GetDate("fec_agr");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeTrans_re_det_parametros oBeTrans_re_det_parametros,
                              SqlConnection pConection,
                              SqlTransaction pTransaction)
    {
        Ins.Init("trans_re_det_parametros");
        Ins.Add("idparametrodet", "@idparametrodet", "F");
        Ins.Add("idrecepciondet", "@idrecepciondet", "F");
        Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Ins.Add("idproductoparametro", "@idproductoparametro", "F");
        Ins.Add("valor_texto", "@valor_texto", "F");
        Ins.Add("valor_numerico", "@valor_numerico", "F");
        Ins.Add("valor_fecha", "@valor_fecha", "F");
        Ins.Add("valor_logico", "@valor_logico", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdParametroDet", oBeTrans_re_det_parametros.IdParametroDet);
            cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeTrans_re_det_parametros.IdRecepcionDet);
            cmd.Parameters.AddWithValue("@IdRecepcionEnc", oBeTrans_re_det_parametros.IdRecepcionEnc);
            cmd.Parameters.AddWithValue("@IdProductoParametro", oBeTrans_re_det_parametros.IdProductoParametro);
            cmd.Parameters.AddWithValue("@valor_texto", oBeTrans_re_det_parametros.Valor_texto);
            cmd.Parameters.AddWithValue("@valor_numerico", oBeTrans_re_det_parametros.Valor_numerico);
            cmd.Parameters.AddWithValue("@valor_fecha", oBeTrans_re_det_parametros.Valor_fecha);
            cmd.Parameters.AddWithValue("@valor_logico", oBeTrans_re_det_parametros.Valor_logico);
            cmd.Parameters.AddWithValue("@user_agr", oBeTrans_re_det_parametros.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeTrans_re_det_parametros.Fec_agr);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeTrans_re_det_parametros oBeTrans_re_det_parametros)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_det_parametros");
            Ins.Add("idparametrodet", "@idparametrodet", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idproductoparametro", "@idproductoparametro", "F");
            Ins.Add("valor_texto", "@valor_texto", "F");
            Ins.Add("valor_numerico", "@valor_numerico", "F");
            Ins.Add("valor_fecha", "@valor_fecha", "F");
            Ins.Add("valor_logico", "@valor_logico", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdParametroDet", oBeTrans_re_det_parametros.IdParametroDet));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", oBeTrans_re_det_parametros.IdRecepcionDet));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_det_parametros.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@IdProductoParametro", oBeTrans_re_det_parametros.IdProductoParametro));
            cmd.Parameters.Add(new SqlParameter("@valor_texto", oBeTrans_re_det_parametros.Valor_texto));
            cmd.Parameters.Add(new SqlParameter("@valor_numerico", oBeTrans_re_det_parametros.Valor_numerico));
            cmd.Parameters.Add(new SqlParameter("@valor_fecha", oBeTrans_re_det_parametros.Valor_fecha));
            cmd.Parameters.Add(new SqlParameter("@valor_logico", oBeTrans_re_det_parametros.Valor_logico));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_re_det_parametros.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_det_parametros.Fec_agr));

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

    public static int Actualizar(clsBeTrans_re_det_parametros oBeTrans_re_det_parametros,
                                SqlConnection pConection,
                                SqlTransaction pTransaction)
    {
        Upd.Init("trans_re_det_parametros");
        Upd.Add("idparametrodet", "@idparametrodet", "F");
        Upd.Add("idrecepciondet", "@idrecepciondet", "F");
        Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Upd.Add("idproductoparametro", "@idproductoparametro", "F");
        Upd.Add("valor_texto", "@valor_texto", "F");
        Upd.Add("valor_numerico", "@valor_numerico", "F");
        Upd.Add("valor_fecha", "@valor_fecha", "F");
        Upd.Add("valor_logico", "@valor_logico", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Where("IdParametroDet = @IdParametroDet" +
            " AND IdRecepcionDet = @IdRecepcionDet" +
            " AND IdRecepcionEnc = @IdRecepcionEnc");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdParametroDet", oBeTrans_re_det_parametros.IdParametroDet);
            cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeTrans_re_det_parametros.IdRecepcionDet);
            cmd.Parameters.AddWithValue("@IdRecepcionEnc", oBeTrans_re_det_parametros.IdRecepcionEnc);
            cmd.Parameters.AddWithValue("@IdProductoParametro", oBeTrans_re_det_parametros.IdProductoParametro);
            cmd.Parameters.AddWithValue("@valor_texto", oBeTrans_re_det_parametros.Valor_texto);
            cmd.Parameters.AddWithValue("@valor_numerico", oBeTrans_re_det_parametros.Valor_numerico);
            cmd.Parameters.AddWithValue("@valor_fecha", oBeTrans_re_det_parametros.Valor_fecha);
            cmd.Parameters.AddWithValue("@valor_logico", oBeTrans_re_det_parametros.Valor_logico);
            cmd.Parameters.AddWithValue("@user_agr", oBeTrans_re_det_parametros.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeTrans_re_det_parametros.Fec_agr);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeTrans_re_det_parametros oBeTrans_re_det_parametros, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_det_parametros" +
             "  Where(IdParametroDet = @IdParametroDet)" +
             "  And (IdRecepcionDet = @IdRecepcionDet)" +
             "  And (IdRecepcionEnc = @IdRecepcionEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdParametroDet", oBeTrans_re_det_parametros.IdParametroDet));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_det_parametros pBeTrans_re_det_parametros)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_det_parametros" +
            " Where(IdParametroDet = @IdParametroDet)" +
            " And (IdRecepcionDet = @IdRecepcionDet)" +
            " And (IdRecepcionEnc = @IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdParametroDet", pBeTrans_re_det_parametros.IdParametroDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionDet", pBeTrans_re_det_parametros.IdRecepcionDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_det_parametros.IdRecepcionEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoParametro", pBeTrans_re_det_parametros.IdProductoParametro));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@valor_texto", pBeTrans_re_det_parametros.Valor_texto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@valor_numerico", pBeTrans_re_det_parametros.Valor_numerico));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@valor_fecha", pBeTrans_re_det_parametros.Valor_fecha));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@valor_logico", pBeTrans_re_det_parametros.Valor_logico));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeTrans_re_det_parametros.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeTrans_re_det_parametros.Fec_agr));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_det_parametros, r);
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

    public static List<clsBeTrans_re_det_parametros> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_det_parametros> lreturnList = new List<clsBeTrans_re_det_parametros>();

        try
        {
            const string sp = "Select * FROM Trans_re_det_parametros";

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

                        clsBeTrans_re_det_parametros vBeTrans_re_det_parametros = new clsBeTrans_re_det_parametros();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_det_parametros = new clsBeTrans_re_det_parametros();
                            Cargar(ref vBeTrans_re_det_parametros, dr);
                            lreturnList.Add(vBeTrans_re_det_parametros);
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

            const string sp = "Select ISNULL(Max(IdParametroDet),0) FROM Trans_re_det_parametros";

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


            const string sp = "Select ISNULL(Max(IdParametroDet),0) FROM Trans_re_det_parametros";

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
    public static int MaxID(int pIdRecepcionEnc,
                       int pIdRecepcionDet,
                       SqlConnection pConnection,
                       SqlTransaction pTransaction)
    {
        string vSQL = @"SELECT ISNULL(Max(IdParametroDet),0) 
                   FROM trans_re_det_parametros 
                   WHERE IdRecepcionEnc=@IdRecepcionEnc 
                   AND IdRecepcionDet=@IdRecepcionDet";

        using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction))
        {
            lCommand.CommandType = CommandType.Text;
            lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);
            lCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet);

            object lReturnValue = lCommand.ExecuteScalar();

            if (lReturnValue != DBNull.Value && lReturnValue != null)
            {
                return Convert.ToInt32(lReturnValue);
            }
        }

        return 0;
    }
    public static int Guarda_Trans_Re_Det_Parametros(int IdRecepcionEnc,
                                                     List<clsBeTrans_re_det>? pListRecDet,
                                                     List<clsBeTrans_re_det_parametros>? pListRecDetParam,
                                                     SqlConnection lConnection,
                                                     SqlTransaction lTransaction)
    {
        int Guarda_Trans_Re_Det_Parametros = 0;

        try
        {
            int vFilas = 0;

            if (pListRecDet != null && pListRecDet.Count > 0 && pListRecDetParam != null)
            {
                foreach (clsBeTrans_re_det Obj1 in pListRecDet)
                {
                    int lMaxIdDP = MaxID(Obj1.IdRecepcionEnc, Obj1.IdRecepcionDet, lConnection, lTransaction);

                    foreach (clsBeTrans_re_det_parametros Obj2 in pListRecDetParam.FindAll(p => p.IdRecepcionDet == Obj1.IdRecepcionDet))
                    {
                        if (Obj2.IsNew)
                        {
                            Obj2.IdRecepcionEnc = IdRecepcionEnc;
                            lMaxIdDP += 1;
                            Obj2.IdParametroDet = lMaxIdDP;
                            vFilas += Insertar(Obj2, lConnection, lTransaction);
                        }
                        else
                        {
                            vFilas += Actualizar(Obj2, lConnection, lTransaction);
                        }
                    }
                }

                Guarda_Trans_Re_Det_Parametros = vFilas;
            }
        }
        catch (Exception)
        {            
            throw;
        }

        return Guarda_Trans_Re_Det_Parametros;
    }

    public static List<clsBeTrans_re_det_parametros> Get_Detalle_Parametros_By_RecepcionEnc(int pIdRecepcionEnc,
                                                                                           SqlConnection lConnection,
                                                                                           SqlTransaction lTransaction)
    {
        List<clsBeTrans_re_det_parametros> lReturnList = new List<clsBeTrans_re_det_parametros>();

        try
        {
            string vSQL = @"SELECT * FROM trans_re_det_parametros 
                       WHERE IdRecepcionEnc = @IdRecepcionEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeTrans_re_det_parametros Obj = new clsBeTrans_re_det_parametros();
                        Cargar(ref Obj, lRow);
                        Obj.IsNew = false;
                        lReturnList.Add(Obj);
                    }
                }
            }

            return lReturnList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static List<clsBeTrans_re_det_parametros> Get_All_By_IdRecepcionEnc_And_IdRecepcionDet(int pIdRecepcionEnc,
                                                                                                  int pIdRecepcionDet,
                                                                                                  SqlConnection lConnection,
                                                                                                  SqlTransaction lTransaction)
    {
        if (lConnection == null)
            throw new ArgumentNullException(nameof(lConnection));

        if (lTransaction == null)
            throw new ArgumentNullException(nameof(lTransaction));

        var lReturnList = new List<clsBeTrans_re_det_parametros>();

        try
        {
            string vSQL = "SELECT * FROM trans_re_det_parametros WHERE IdRecepcionEnc=@IdRecepcionEnc And IdRecepcionDet=@IdRecepcionDet";

            using (var lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet);

                var lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        var Obj = new clsBeTrans_re_det_parametros();
                        Cargar(ref Obj, lRow);
                        Obj.IsNew = false;
                        lReturnList.Add(Obj);
                    }
                }
            }

            return lReturnList;
        }
        catch (Exception)
        {
            throw;
        }
    }
}