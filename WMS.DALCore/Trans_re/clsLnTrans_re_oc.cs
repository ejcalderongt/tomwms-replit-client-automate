using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_re;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Trans_oc;
public class clsLnTrans_re_oc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_oc oBeTrans_re_oc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }

        try
        {
            oBeTrans_re_oc.IdRecepcionOc = GetInt("IdRecepcionOc");
            oBeTrans_re_oc.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_oc.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_re_oc.Recepcion_ciega = GetBool("recepcion_ciega");
            oBeTrans_re_oc.Recepcion_manual = GetBool("recepcion_manual");
            oBeTrans_re_oc.No_docto = GetString("no_docto");
            oBeTrans_re_oc.Hora_ini_hh = GetDate("hora_ini_hh");
            oBeTrans_re_oc.Hora_fin_hh = GetDate("hora_fin_hh");
            oBeTrans_re_oc.User_agr = GetString("user_agr");
            oBeTrans_re_oc.Fec_agr = GetDate("fec_agr");
            oBeTrans_re_oc.Firma_operador = GetBytes("firma_operador");
        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(clsBeTrans_re_oc oBeTrans_re_oc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_re_oc");
            Ins.Add("idrecepcionoc", "@idrecepcionoc", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("recepcion_ciega", "@recepcion_ciega", "F");
            Ins.Add("recepcion_manual", "@recepcion_manual", "F");
            Ins.Add("no_docto", "@no_docto", "F");
            Ins.Add("hora_ini_hh", "@hora_ini_hh", "F");
            Ins.Add("hora_fin_hh", "@hora_fin_hh", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("firma_operador", "@firma_operador", "F");

            string sp = Ins.SQL();
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_re_oc);
            rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(IConfiguration config, clsBeTrans_re_oc oBeTrans_re_oc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_oc");
            Ins.Add("idrecepcionoc", "@idrecepcionoc", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("recepcion_ciega", "@recepcion_ciega", "F");
            Ins.Add("recepcion_manual", "@recepcion_manual", "F");
            Ins.Add("no_docto", "@no_docto", "F");
            Ins.Add("hora_ini_hh", "@hora_ini_hh", "F");
            Ins.Add("hora_fin_hh", "@hora_fin_hh", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("firma_operador", "@firma_operador", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_re_oc);

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

    public static int Actualizar(clsBeTrans_re_oc oBeTrans_re_oc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_re_oc");
            Upd.Add("idrecepcionoc", "@idrecepcionoc", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("recepcion_ciega", "@recepcion_ciega", "F");
            Upd.Add("recepcion_manual", "@recepcion_manual", "F");
            Upd.Add("no_docto", "@no_docto", "F");
            Upd.Add("hora_ini_hh", "@hora_ini_hh", "F");
            Upd.Add("hora_fin_hh", "@hora_fin_hh", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("firma_operador", "@firma_operador", "F");
            Upd.Where("IdRecepcionOc = @IdRecepcionOc AND IdRecepcionEnc = @IdRecepcionEnc");

            string sp = Upd.SQL();
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_re_oc);
            rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int Eliminar(IConfiguration config, clsBeTrans_re_oc oBeTrans_re_oc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_oc" +
             "  Where(IdRecepcionOc = @IdRecepcionOc)" +
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

            cmd.Parameters.Add(new SqlParameter("@IdRecepcionOc", oBeTrans_re_oc.IdRecepcionOc));

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
            const string sp = "Select * FROM Trans_re_oc";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_oc pBeTrans_re_oc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_oc" +
            " Where(IdRecepcionOc = @IdRecepcionOc)" +
            " And (IdRecepcionEnc = @IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionOc", pBeTrans_re_oc.IdRecepcionOc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_oc.IdRecepcionEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_oc, r);
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

    public static List<clsBeTrans_re_oc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_oc> lreturnList = new List<clsBeTrans_re_oc>();

        try
        {
            const string sp = "Select * FROM Trans_re_oc";

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

                        clsBeTrans_re_oc vBeTrans_re_oc = new clsBeTrans_re_oc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_oc = new clsBeTrans_re_oc();
                            Cargar(ref vBeTrans_re_oc, dr);
                            lreturnList.Add(vBeTrans_re_oc);
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

            const string sp = "Select ISNULL(Max(IdRecepcionOc),0) FROM Trans_re_oc";

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


            const string sp = "Select ISNULL(Max(IdRecepcionOc),0) FROM Trans_re_oc";

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

    public static void Bind(SqlCommand cmd, clsBeTrans_re_oc oBeTrans_re_oc)
    {
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionOc", oBeTrans_re_oc.IdRecepcionOc));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_oc.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_re_oc.IdOrdenCompraEnc));
        cmd.Parameters.Add(new SqlParameter("@recepcion_ciega", oBeTrans_re_oc.Recepcion_ciega));
        cmd.Parameters.Add(new SqlParameter("@recepcion_manual", oBeTrans_re_oc.Recepcion_manual));
        cmd.Parameters.Add(new SqlParameter("@no_docto", string.IsNullOrEmpty(oBeTrans_re_oc.No_docto) ? (object)DBNull.Value : oBeTrans_re_oc.No_docto));
        cmd.Parameters.Add(new SqlParameter("@hora_ini_hh", oBeTrans_re_oc.Hora_ini_hh == default ? (object)DBNull.Value : oBeTrans_re_oc.Hora_ini_hh));
        cmd.Parameters.Add(new SqlParameter("@hora_fin_hh", oBeTrans_re_oc.Hora_fin_hh == default ? (object)DBNull.Value : oBeTrans_re_oc.Hora_fin_hh));
        cmd.Parameters.Add(new SqlParameter("@user_agr", string.IsNullOrEmpty(oBeTrans_re_oc.User_agr) ? (object)DBNull.Value : oBeTrans_re_oc.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_oc.Fec_agr == default ? (object)DBNull.Value : oBeTrans_re_oc.Fec_agr));
        //cmd.Parameters.Add(new SqlParameter("@firma_operador", oBeTrans_re_oc.Firma_operador == null || oBeTrans_re_oc.Firma_operador.Length == 0 ? (object)DBNull.Value : oBeTrans_re_oc.Firma_operador));

        byte[] firmaBytes = Array.Empty<byte>();

        if (oBeTrans_re_oc.Firma_operador != null && oBeTrans_re_oc.Firma_operador.Length > 0)
        {
            firmaBytes = oBeTrans_re_oc.Firma_operador;
        }

        var param = new SqlParameter("@firma_operador", SqlDbType.Image);
        param.Value = (object)firmaBytes ?? DBNull.Value;
        cmd.Parameters.Add(param);

    }
    public static void InsertarOActualizar(List<clsBeTrans_re_oc> entities, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdRecepcionOc, entity.IdOrdenCompraEnc, conn, tx);

                if (existe)
                    Actualizar(entity, conn, tx);
                else
                    Insertar(entity, conn, tx);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static bool Existe(int idRecepcionOc,int IdOrdenCompraEnc, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM trans_re_oc WHERE (idrecepcionoc = @idrecepcionoc and IdOrdenCompraEnc=@IdOrdenCompraEnc) ";

            using (SqlCommand cmd = new SqlCommand(query, conn, tx))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@idrecepcionoc", idRecepcionOc));
                cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }

    public static bool Existe_Documento_By_IdOrdenCompraEnc(int pIdOrdenCompraEnc,
                                                            SqlConnection lConnection,
                                                            SqlTransaction lTransaction)
    {
        bool existe = false;

        try
        {
            const string sp = @"SELECT IdRecepcionOC FROM Trans_re_oc 
                            WHERE (IdOrdenCompraEnc = @IdOrdenCompraEnc)";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            existe = dt.Rows.Count > 0;

            return existe;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Guarda_Trans_Re_OC(clsBeTrans_re_enc pRecEnc,
                                         clsBeTrans_re_oc pRecOrdenCompra,
                                         SqlConnection lConnection,
                                         SqlTransaction lTransaction)
    {
        int vFilas = 0;

        try
        {
            if (pRecOrdenCompra != null && pRecOrdenCompra.IdOrdenCompraEnc > 0)
            {
                vFilas = clsLnTrans_oc_enc.Actualizar_Estado_OC(pRecEnc, pRecOrdenCompra, lConnection, lTransaction);

                if (pRecOrdenCompra.IsNew)
                {
                    pRecOrdenCompra.IdRecepcionOc = MaxID(pRecOrdenCompra.IdOrdenCompraEnc, lConnection, lTransaction) + 1;
                    vFilas += Insertar(pRecOrdenCompra, lConnection, lTransaction);
                }
                else
                {
                    vFilas += Actualizar(pRecOrdenCompra, lConnection, lTransaction);
                }

                return vFilas;
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int MaxID(int pIdOrdenCompraEnc, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            int lMax = 0;
            string vSQL = $"SELECT ISNULL(MAX(IdRecepcionOc), 0) FROM trans_re_oc WHERE IdOrdenCompraEnc = {pIdOrdenCompraEnc}";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
            {
                object lReturnValue = lCommand.ExecuteScalar();
                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lMax = Convert.ToInt32(lReturnValue) + 1;
                }
            }

            return lMax;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeTrans_re_oc? GetSingle(int pIdRecepcionEnc,
                                             SqlConnection lConnection,
                                             SqlTransaction lTransaction)
{
    try
    {
        string vSQL = "SELECT TOP 1 * FROM Trans_re_oc WHERE IdRecepcionEnc = @IdRecepcionEnc";

        using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
        {
            lDTA.SelectCommand.CommandType = CommandType.Text;
            lDTA.SelectCommand.Transaction = lTransaction;
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

            DataTable lDT = new DataTable();
            lDTA.Fill(lDT);

            if (lDT?.Rows.Count > 0)
            {
                DataRow lRow = lDT.Rows[0];
                clsBeTrans_re_oc BeTransReOC = new clsBeTrans_re_oc();

                Cargar(ref BeTransReOC, lRow);

                BeTransReOC.IsNew = false;
                BeTransReOC.OC.IdOrdenCompraEnc = BeTransReOC.IdOrdenCompraEnc;
                
                clsBeTrans_oc_enc? ordenCompra = clsLnTrans_oc_enc.Get_Orden_Compra(BeTransReOC.OC.IdOrdenCompraEnc, lConnection, lTransaction);
                if (ordenCompra != null)
                {
                    BeTransReOC.OC = ordenCompra;
                                        
                    clsBeTrans_oc_ti? tipoIngreso = clsLnTrans_oc_ti.GetSingle(BeTransReOC.OC.IdTipoIngresoOC, lConnection, lTransaction);
                    if (tipoIngreso != null)
                    {
                        BeTransReOC.OC.TipoIngreso = tipoIngreso;
                    }
                }

                return BeTransReOC;
            }
        }

        return null;
    }
    catch (Exception)
    {
        throw;
    }
}

    public static clsBeTrans_re_oc? Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(int pIdOrdenCompraEnc,
                                                                                     int pIdRecepcionEnc,
                                                                                     SqlConnection lConnection,
                                                                                     SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT * FROM Trans_re_oc 
                       WHERE IdRecepcionEnc = @IdRecepcionEnc 
                       AND IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTrans_re_oc Obj = new clsBeTrans_re_oc();
                    
                    Cargar(ref Obj, lRow);

                    Obj.IsNew = false;
                    Obj.OC.IdOrdenCompraEnc = Obj.IdOrdenCompraEnc;

                    return Obj;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Actualizar_Hora_Fin_Recepcion(ref clsBeTrans_re_oc oBeTrans_re_oc,
                                                    SqlConnection pConection,
                                                    SqlTransaction pTransaction)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("trans_re_oc");
            Upd.Add("hora_fin_hh", "@Hora_Fin_Recepcion ", "F");
            Upd.Where("IdRecepcionOc = @IdRecepcionOc AND IdRecepcionEnc = @IdRecepcionEnc");

            string sp = Upd.SQL();

            cmd = new SqlCommand(sp, pConection, pTransaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc));
            cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@Hora_Fin_Recepcion ", oBeTrans_re_oc.Hora_fin_hh));

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            cmd?.Dispose();
        }
    }
}