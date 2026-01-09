using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_re;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_re_op
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_op oBeTrans_re_op, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeTrans_re_op.IdOperadorRec = GetInt("IdOperadorRec");
            oBeTrans_re_op.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_op.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeTrans_re_op.User_agr = GetString("user_agr");
            oBeTrans_re_op.Fec_agr = GetDate("fec_agr");
            oBeTrans_re_op.User_mod = GetString("user_mod");
            oBeTrans_re_op.Fec_mod = GetDate("fec_mod");
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

    public static int Insertar(clsBeTrans_re_op oBeTrans_re_op,
                              SqlConnection pConection,
                              SqlTransaction pTransaction)
    {
        Ins.Init("trans_re_op");
        Ins.Add("idoperadorrec", "@idoperadorrec", "F");
        Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            BindParameters(cmd, oBeTrans_re_op);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar_3pl(clsBeTrans_re_op_3pl oBeTrans_re_op,
                                  SqlConnection pConection,
                                  SqlTransaction pTransaction)
    {
        Ins.Init("trans_re_op");
        Ins.Add("idoperadorrec", "@idoperadorrec", "F");
        Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            BindParameters_3pl(cmd, oBeTrans_re_op);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeTrans_re_op oBeTrans_re_op)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_op");
            Ins.Add("idoperadorrec", "@idoperadorrec", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindParameters(cmd, oBeTrans_re_op);

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

    public static int Actualizar(clsBeTrans_re_op oBeTrans_re_op,
                                SqlConnection pConection,
                                SqlTransaction pTransaction)
    {
        Upd.Init("trans_re_op");
        Upd.Add("idoperadorrec", "@idoperadorrec", "F");
        Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Where("IdOperadorRec = @IdOperadorRec" +
            " AND IdRecepcionEnc = @IdRecepcionEnc");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            BindParameters(cmd, oBeTrans_re_op);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Actualizar_3pl(clsBeTrans_re_op_3pl oBeTrans_re_op,
                                     SqlConnection pConection,
                                     SqlTransaction pTransaction)
    {
        Upd.Init("trans_re_op");
        Upd.Add("idoperadorrec", "@idoperadorrec", "F");
        Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Where("IdOperadorRec = @IdOperadorRec" +
            " AND IdRecepcionEnc = @IdRecepcionEnc");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            BindParameters_3pl(cmd, oBeTrans_re_op);
            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeTrans_re_op oBeTrans_re_op, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_op" +
             "  Where(IdOperadorRec = @IdOperadorRec)" +
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

            cmd.Parameters.Add(new SqlParameter("@IdOperadorRec", oBeTrans_re_op.IdOperadorRec));

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
            const string sp = "Select * FROM Trans_re_op";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_op pBeTrans_re_op)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_op" +
            " Where(IdOperadorRec = @IdOperadorRec)" +
            " And (IdRecepcionEnc = @IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOperadorRec", pBeTrans_re_op.IdOperadorRec));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_op.IdRecepcionEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_op, r);
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

    public static List<clsBeTrans_re_op> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_op> lreturnList = new List<clsBeTrans_re_op>();

        try
        {
            const string sp = "Select * FROM Trans_re_op";

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

                        clsBeTrans_re_op vBeTrans_re_op = new clsBeTrans_re_op();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_op = new clsBeTrans_re_op();
                            Cargar(ref vBeTrans_re_op, dr);
                            lreturnList.Add(vBeTrans_re_op);
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

            const string sp = "Select ISNULL(Max(IdOperadorRec),0) FROM Trans_re_op";

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


            const string sp = "Select ISNULL(Max(IdOperadorRec),0) FROM Trans_re_op";

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
    public static void BindParameters(SqlCommand cmd, clsBeTrans_re_op oBeTrans_re_op)
    {
        cmd.Parameters.Add(new SqlParameter("@IdOperadorRec", oBeTrans_re_op.IdOperadorRec));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_op.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", oBeTrans_re_op.IdOperadorBodega));
        cmd.Parameters.Add(new SqlParameter("@user_agr", string.IsNullOrWhiteSpace(oBeTrans_re_op.User_agr) ? DBNull.Value : oBeTrans_re_op.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_op.Fec_agr == default ? DBNull.Value : oBeTrans_re_op.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", string.IsNullOrWhiteSpace(oBeTrans_re_op.User_mod) ? DBNull.Value : oBeTrans_re_op.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_re_op.Fec_mod == default ? DBNull.Value : oBeTrans_re_op.Fec_mod));
    }

    public static void BindParameters_3pl(SqlCommand cmd, clsBeTrans_re_op_3pl oBeTrans_re_op)
    {
        cmd.Parameters.Add(new SqlParameter("@IdOperadorRec", oBeTrans_re_op.IdOperadorRec));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_op.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", oBeTrans_re_op.IdOperadorBodega));
        cmd.Parameters.Add(new SqlParameter("@user_agr", string.IsNullOrWhiteSpace(oBeTrans_re_op.User_agr) ? DBNull.Value : oBeTrans_re_op.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_op.Fec_agr == default ? DBNull.Value : oBeTrans_re_op.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", string.IsNullOrWhiteSpace(oBeTrans_re_op.User_mod) ? DBNull.Value : oBeTrans_re_op.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_re_op.Fec_mod == default ? DBNull.Value : oBeTrans_re_op.Fec_mod));
    }

    public static void InsertarOActualizar(List<clsBeTrans_re_op> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                if (entity.IdOperadorBodega != 0)
                {
                    bool existe = Existe(entity.IdOperadorRec, entity.IdRecepcionEnc, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }

    public static void InsertarOActualizar_3pl(List<clsBeTrans_re_op_3pl> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                if (entity.IdOperadorBodega != 0)
                {
                    bool existe = Existe(entity.IdOperadorRec, entity.IdRecepcionEnc, conn, tx);

                    if (existe)
                        Actualizar_3pl(entity, conn, tx);
                    else
                        Insertar_3pl(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static bool Existe(int idOperadorRec, int idRecepcionEnc, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = @"SELECT COUNT(1) FROM trans_re_op WHERE IdOperadorRec = @IdOperadorRec AND IdRecepcionEnc = @IdRecepcionEnc";

            using var cmd = new SqlCommand(query, conn, tx);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@IdOperadorRec", idOperadorRec));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", idRecepcionEnc));

            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
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

    public static void Guarda_Trans_Re_Op(int IdRecepcionEnc,
                                         List<clsBeTrans_re_op>? pListRecOpe,
                                         SqlConnection lConnection,
                                         SqlTransaction lTransaction)
    {
        try
        {
            int lMaxIdO = MaxID(IdRecepcionEnc, lConnection, lTransaction);

            if (pListRecOpe !=null)
                foreach (clsBeTrans_re_op Obj in pListRecOpe)
                {
                    if (Obj.IsNew && Obj.UsaHH)
                    {
                        lMaxIdO += 1;
                        Obj.IdOperadorRec = lMaxIdO;
                        Obj.IdRecepcionEnc = IdRecepcionEnc;
                        Insertar(Obj, lConnection, lTransaction);
                    }
                    else
                    {
                        if (Obj.UsaHH)
                        {
                            Actualizar(Obj, lConnection, lTransaction);
                        }
                    }
                }
        }
        catch (Exception)
        {         
            throw;
        }
    }
    public static int MaxID(int pIdRecepcionEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        string sp = "SELECT ISNULL(Max(IdOperadorRec),0) FROM trans_re_op WHERE IdRecepcionEnc=@IdRecepcionEnc";

        using (SqlCommand lCommand = new SqlCommand(sp, pConnection, pTransaction))
        {
            lCommand.CommandType = CommandType.Text;
            lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

            object lReturnValue = lCommand.ExecuteScalar();

            if (lReturnValue != DBNull.Value && lReturnValue != null)
            {
                return Convert.ToInt32(lReturnValue);
            }
        }

        return 0;
    }
}