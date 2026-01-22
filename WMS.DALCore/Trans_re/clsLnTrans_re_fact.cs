using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_re;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_re_fact
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_fact oBeTrans_re_fact, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeTrans_re_fact.IdFacturaRecepcion = GetInt("IdFacturaRecepcion");
            oBeTrans_re_fact.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_fact.Orden = GetInt("Orden");
            oBeTrans_re_fact.NoFactura = GetString("NoFactura");
            oBeTrans_re_fact.Observacion = GetString("Observacion");
            oBeTrans_re_fact.Fec_agr = GetDate("fec_agr");
            oBeTrans_re_fact.User_agr = GetString("user_agr");
            oBeTrans_re_fact.Fec_mod = GetDate("fec_mod");
            oBeTrans_re_fact.User_mod = GetString("user_mod");
            oBeTrans_re_fact.Completa = GetBool("Completa");
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

    public static int Insertar(IConfiguration config, clsBeTrans_re_fact oBeTrans_re_fact, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_fact");
            Ins.Add("idfacturarecepcion", "@idfacturarecepcion", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("orden", "@orden", "F");
            Ins.Add("nofactura", "@nofactura", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("completa", "@completa", "F");

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

            Bind(cmd, oBeTrans_re_fact);

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

    public static int Insertar(clsBeTrans_re_fact oBeTrans_re_fact,
                              SqlConnection pConection,
                              SqlTransaction pTransaction)
    {
        Ins.Init("trans_re_fact");
        Ins.Add("idfacturarecepcion", "@idfacturarecepcion", "F");
        Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Ins.Add("orden", "@orden", "F");
        Ins.Add("nofactura", "@nofactura", "F");
        Ins.Add("observacion", "@observacion", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("completa", "@completa", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            Bind(cmd, oBeTrans_re_fact);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Actualizar(clsBeTrans_re_fact oBeTrans_re_fact,
                            SqlConnection pConection,
                            SqlTransaction pTransaction)
    {
        Upd.Init("trans_re_fact");
        Upd.Add("idfacturarecepcion", "@idfacturarecepcion", "F");
        Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Upd.Add("orden", "@orden", "F");
        Upd.Add("nofactura", "@nofactura", "F");
        Upd.Add("observacion", "@observacion", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("completa", "@completa", "F");
        Upd.Where("IdFacturaRecepcion = @IdFacturaRecepcion");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            Bind(cmd, oBeTrans_re_fact);
            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeTrans_re_fact oBeTrans_re_fact, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_fact" +
             "  Where(IdFacturaRecepcion = @IdFacturaRecepcion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdFacturaRecepcion", oBeTrans_re_fact.IdFacturaRecepcion));

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
            const string sp = "Select * FROM Trans_re_fact";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_fact pBeTrans_re_fact)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_fact" +
            " Where(IdFacturaRecepcion = @IdFacturaRecepcion AND IdRecepcionEnc =@IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdFacturaRecepcion", pBeTrans_re_fact.IdFacturaRecepcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_fact.IdRecepcionEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_fact, r);
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

    public static List<clsBeTrans_re_fact> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_fact> lreturnList = new List<clsBeTrans_re_fact>();

        try
        {
            const string sp = "Select * FROM Trans_re_fact";

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

                        clsBeTrans_re_fact vBeTrans_re_fact = new clsBeTrans_re_fact();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_fact = new clsBeTrans_re_fact();
                            Cargar(ref vBeTrans_re_fact, dr);
                            lreturnList.Add(vBeTrans_re_fact);
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

            const string sp = "Select ISNULL(Max(IdFacturaRecepcion),0) FROM Trans_re_fact";

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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sp = "Select ISNULL(Max(IdFacturaRecepcion),0) FROM Trans_re_fact";

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                return Convert.ToInt32(lreturnValue);
            }
        }

        return 0;
    }
    public static void Bind(SqlCommand cmd, clsBeTrans_re_fact oBeTrans_re_fact)
    {
        cmd.Parameters.Add(new SqlParameter("@IdFacturaRecepcion", oBeTrans_re_fact.IdFacturaRecepcion == 0 ? DBNull.Value : oBeTrans_re_fact.IdFacturaRecepcion));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_fact.IdRecepcionEnc == 0 ? DBNull.Value : oBeTrans_re_fact.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@Orden", oBeTrans_re_fact.Orden));
        cmd.Parameters.Add(new SqlParameter("@NoFactura", oBeTrans_re_fact.NoFactura));
        cmd.Parameters.Add(new SqlParameter("@Observacion", oBeTrans_re_fact.Observacion));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_fact.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_re_fact.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_re_fact.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_re_fact.User_mod));
        cmd.Parameters.Add(new SqlParameter("@Completa", oBeTrans_re_fact.Completa));
    }

    public static void Guarda_facturas_asoc(int IdRecepcionEnc,
                                           List<clsBeTrans_re_fact>? pListRecFact,
                                           SqlConnection lConnection,
                                           SqlTransaction lTransaction)
    {
        try
        {
            if (pListRecFact != null)
            {
                int lMaxIdF = MaxID(lConnection, lTransaction);

                foreach (clsBeTrans_re_fact ObjF in pListRecFact)
                {
                    if (ObjF.IsNew)
                    {
                        lMaxIdF += 1;
                        ObjF.IdFacturaRecepcion = lMaxIdF;
                        ObjF.IdRecepcionEnc = IdRecepcionEnc;
                        Insertar(ObjF, lConnection, lTransaction);
                    }
                    else
                    {
                        Actualizar(ObjF, lConnection, lTransaction);
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static List<clsBeTrans_re_fact> Get_Detalle_Facturas_By_IdRecepcionEnc(int pIdRecepcionEnc,
                                                                                 SqlConnection lConnection,
                                                                                 SqlTransaction lTransaction)
    {
        List<clsBeTrans_re_fact> lReturnList = new List<clsBeTrans_re_fact>();

        try
        {
            string vSQL = @"SELECT * FROM Trans_re_fact 
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
                        clsBeTrans_re_fact Obj = new clsBeTrans_re_fact();
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