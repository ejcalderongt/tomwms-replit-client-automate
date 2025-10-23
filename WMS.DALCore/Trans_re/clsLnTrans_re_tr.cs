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
    public static int Insertar(clsBeTrans_re_tr oBeTrans_re_tr, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTrans_re_tr == null)
            throw new ArgumentNullException(nameof(oBeTrans_re_tr));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", oBeTrans_re_tr.IdTipoTransaccion));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTrans_re_tr.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Funcionalidad", oBeTrans_re_tr.Funcionalidad));
                cmd.Parameters.Add(new SqlParameter("@UsaHH", oBeTrans_re_tr.UsaHH));
                cmd.Parameters.Add(new SqlParameter("@DescDev", oBeTrans_re_tr.DescDev));
                cmd.Parameters.Add(new SqlParameter("@TipoTrans", oBeTrans_re_tr.TipoTrans));
                cmd.Parameters.Add(new SqlParameter("@ConRef", oBeTrans_re_tr.ConRef));
                cmd.Parameters.Add(new SqlParameter("@Activo", oBeTrans_re_tr.Activo));

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
    public static int Actualizar(clsBeTrans_re_tr oBeTrans_re_tr, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTrans_re_tr == null)
            throw new ArgumentNullException(nameof(oBeTrans_re_tr));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindParameters(cmd, oBeTrans_re_tr);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Actualizar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
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
    public static void InsertarOActualizar(clsBeTrans_re_tr entity, SqlConnection conn, SqlTransaction tx)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            bool existe = Existe(entity.IdTipoTransaccion, conn, tx);

            if (existe)
                Actualizar(entity, conn, tx);
            else
                Insertar(entity, conn, tx);
        }
        catch (SqlException ex)
        {
            var method = MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
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
    public static void InsertarOActualizar(List<clsBeTrans_re_tr> entities, SqlConnection conn, SqlTransaction tx)
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

                if (!string.IsNullOrEmpty(entity.IdTipoTransaccion))
                {
                    bool existe = Existe(entity.IdTipoTransaccion, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }    
    public List<clsBeTrans_re_tr> Get_All_For_HH(IConfiguration config)
    {
        try
        {
            string sp = " SELECT trans_re_tr.IdTipoTransaccion, trans_re_tr.Descripcion, trans_re_tr.TipoTrans, SUBSTRING(trans_re_tr.Funcionalidad,1,1000) AS Funcionalidad, " +
                       " trans_re_tr.UsaHH,SUBSTRING(trans_re_tr.DescDev,1,1000) AS DescDev, trans_re_tr.ConRef, " +
                       " COUNT(trans_re_enc.IdRecepcionEnc) AS Cantidad " +
                       " FROM trans_re_tr LEFT JOIN " +
                       " trans_re_enc ON trans_re_enc.IdTipoTransaccion = trans_re_tr.IdTipoTransaccion " +
                       " WHERE trans_re_tr.UsaHH = 1 AND trans_re_tr.Activo = 1  " +
                       " GROUP BY trans_re_tr.IdTipoTransaccion, trans_re_tr.Descripcion, trans_re_tr.TipoTrans,trans_re_tr.UsaHH,trans_re_tr.ConRef, " +
                       " SUBSTRING(trans_re_tr.DescDev,1,1000), SUBSTRING(trans_re_tr.Funcionalidad,1,1000) ";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            using (SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
            using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                dad.Fill(dt);

                List<clsBeTrans_re_tr> lTr = new List<clsBeTrans_re_tr>();
                clsBeTrans_re_tr Tr;

                foreach (DataRow dr in dt.Rows)
                {
                    Tr = new clsBeTrans_re_tr();
                    Cargar(ref Tr, dr);
                    lTr.Add(Tr);
                }

                return lTr;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Trans_re_tr_Listar: " + ex.Message);
        }
    }    
    public static List<clsBeTrans_re_tr> GetAllFiltro(IConfiguration config, bool pConRef, string pTipoTrans, string pFiltro)
    {
        try
        {
            List<clsBeTrans_re_tr> lReturnList = new List<clsBeTrans_re_tr>();

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                string vSQL = "SELECT * FROM trans_re_tr WHERE Activo = 1 ";

                if (!string.IsNullOrEmpty(pFiltro))
                {
                    if (pConRef)
                        vSQL += "AND ConRef=1 ";
                    else
                        vSQL += "AND ConRef=0 ";
                }

                if (!string.IsNullOrEmpty(pTipoTrans))
                {
                    vSQL += " AND TipoTrans= @TipoTrans";
                }

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(pTipoTrans))
                        lDTA.SelectCommand.Parameters.AddWithValue("@TipoTrans", pTipoTrans);

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            clsBeTrans_re_tr Obj = new clsBeTrans_re_tr();
                            Obj.IdTipoTransaccion = (string)lRow["IdTipoTransaccion"];

                            if (lRow["Descripcion"] != DBNull.Value && lRow["Descripcion"] != null)
                                Obj.Descripcion = (string)lRow["Descripcion"];

                            if (lRow["Funcionalidad"] != DBNull.Value && lRow["Funcionalidad"] != null)
                                Obj.Funcionalidad = (string)lRow["Funcionalidad"];

                            if (lRow["UsaHH"] != DBNull.Value && lRow["UsaHH"] != null)
                                Obj.UsaHH = (bool)lRow["UsaHH"];

                            lReturnList.Add(Obj);
                        }
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
    public static clsBeTrans_re_tr? GetSingle(string pIdTipoTransaccion, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        DataTable lDT = new DataTable();

        try
        {
            string vSQL = "SELECT * FROM trans_re_tr WHERE IdTipoTransaccion=@IdTipoTransaccion";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion);

                using (SqlDataAdapter lDTA = new SqlDataAdapter(lCommand))
                {
                    lDTA.Fill(lDT);
                }
            }

            clsBeTrans_re_tr? Obj = null;

            if (lDT != null && lDT.Rows.Count > 0)
            {
                DataRow lRow = lDT.Rows[0];
                Obj = new clsBeTrans_re_tr();
                Obj.IdTipoTransaccion = (string)lRow["IdTipoTransaccion"];

                if (lRow["Descripcion"] != DBNull.Value && lRow["Descripcion"] != null)
                    Obj.Descripcion = (string)lRow["Descripcion"];

                if (lRow["Funcionalidad"] != DBNull.Value && lRow["Funcionalidad"] != null)
                    Obj.Funcionalidad = (string)lRow["Funcionalidad"];

                if (lRow["UsaHH"] != DBNull.Value && lRow["UsaHH"] != null)
                    Obj.UsaHH = (bool)lRow["UsaHH"];

                if (lRow["ConRef"] != DBNull.Value && lRow["ConRef"] != null)
                    Obj.ConRef = (bool)lRow["ConRef"];
            }

            return Obj;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static List<clsBeTrans_re_tr> GetAll(IConfiguration config, bool pIngreso, bool pUsaHH, bool pRefer)
    {
        try
        {
            List<clsBeTrans_re_tr> lReturnList = new List<clsBeTrans_re_tr>();

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                string vSQL = "SELECT IdTipoTransaccion AS Código, " +
                        "Descripcion AS Descripción, " +
                        "UsaHH, " +
                        "ConRef " +
                        "FROM trans_re_tr WHERE Activo = 1";

                if (pIngreso)
                    vSQL += " AND TipoTrans='INGRESO' ";
                else
                    vSQL += " AND TipoTrans='DEVOLUCION' ";

                if (pUsaHH)
                    vSQL += " AND UsaHH=1 ";
                else
                    vSQL += " AND UsaHH=0 ";

                if (pRefer)
                    vSQL += " AND ConRef=1 ";
                else
                    vSQL += " AND ConRef=0 ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            clsBeTrans_re_tr Obj = new clsBeTrans_re_tr();
                            Obj.IdTipoTransaccion = (string)lRow["Código"];

                            if (lRow["Descripción"] != DBNull.Value && lRow["Descripción"] != null)
                                Obj.Descripcion = (string)lRow["Descripción"];

                            if (lRow["UsaHH"] != DBNull.Value && lRow["UsaHH"] != null)
                                Obj.UsaHH = (bool)lRow["UsaHH"];

                            if (lRow["ConRef"] != DBNull.Value && lRow["ConRef"] != null)
                                Obj.ConRef = (bool)lRow["ConRef"];

                            lReturnList.Add(Obj);
                        }
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
    public static bool UsaHH(string pIdTipoTrans, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            bool lValue = false;
            string vSQL = "SELECT UsaHH FROM trans_re_tr WHERE IdTipoTransaccion = @pIdTipoTrans ";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@pIdTipoTrans", pIdTipoTrans);
                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                    lValue = Convert.ToBoolean(lReturnValue);
            }

            return lValue;
        }
        catch (Exception)
        {
            throw;
        }
    }
}