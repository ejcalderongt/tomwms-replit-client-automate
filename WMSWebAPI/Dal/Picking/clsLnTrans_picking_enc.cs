using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnTrans_picking_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_picking_enc oBeTrans_picking_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeTrans_picking_enc.IdPickingEnc = GetInt("IdPickingEnc");
            oBeTrans_picking_enc.IdBodega = GetInt("IdBodega");
            oBeTrans_picking_enc.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_picking_enc.IdUbicacionPicking = GetInt("IdUbicacionPicking");
            oBeTrans_picking_enc.Fecha_picking = GetDate("fecha_picking");
            oBeTrans_picking_enc.Hora_ini = GetDate("hora_ini");
            oBeTrans_picking_enc.Hora_fin = GetDate("hora_fin");
            oBeTrans_picking_enc.Estado = GetString("estado");
            oBeTrans_picking_enc.User_agr = GetString("user_agr");
            oBeTrans_picking_enc.Fec_agr = GetDate("fec_agr");
            oBeTrans_picking_enc.User_mod = GetString("user_mod");
            oBeTrans_picking_enc.Fec_mod = GetDate("fec_mod");
            oBeTrans_picking_enc.Detalle_operador = GetBool("detalle_operador");
            oBeTrans_picking_enc.Activo = GetBool("activo");
            oBeTrans_picking_enc.Verifica_auto = GetBool("verifica_auto");
            oBeTrans_picking_enc.Procesado_bof = GetBool("procesado_bof");
            oBeTrans_picking_enc.Requiere_preparacion = GetBool("requiere_preparacion");
            oBeTrans_picking_enc.Tipo_preparacion = GetString("tipo_preparacion");
            oBeTrans_picking_enc.Estado_preparacion = GetString("estado_preparacion");
            oBeTrans_picking_enc.Fecha_inicio_preparacion = GetDate("fecha_inicio_preparacion");
            oBeTrans_picking_enc.Fecha_fin_preparacion = GetDate("fecha_fin_preparacion");
            oBeTrans_picking_enc.Referencia = GetString("referencia");
            oBeTrans_picking_enc.Fotografia_verificacion = GetBool("fotografia_verificacion");
            oBeTrans_picking_enc.IdBodegaMuelle = GetInt("IdBodegaMuelle");
            oBeTrans_picking_enc.IdPrioridadPicking = GetInt("IdPrioridadPicking");
            oBeTrans_picking_enc.IdTipoPicking = GetInt("IdTipoPicking");
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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_enc oBeTrans_picking_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {


        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_enc");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idubicacionpicking", "@idubicacionpicking", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("detalle_operador", "@detalle_operador", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("verifica_auto", "@verifica_auto", "F");
            Ins.Add("procesado_bof", "@procesado_bof", "F");
            Ins.Add("requiere_preparacion", "@requiere_preparacion", "F");
            Ins.Add("tipo_preparacion", "@tipo_preparacion", "F");
            Ins.Add("estado_preparacion", "@estado_preparacion", "F");
            Ins.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", "F");
            Ins.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Ins.Add("idbodegamuelle", "@idbodegamuelle", "F");
            Ins.Add("idprioridadpicking", "@idprioridadpicking", "F");
            Ins.Add("idtipopicking", "@idtipopicking", "F");

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

            Bind(cmd, oBeTrans_picking_enc);

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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_enc oBeTrans_picking_enc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_enc");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idubicacionpicking", "@idubicacionpicking", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("detalle_operador", "@detalle_operador", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("verifica_auto", "@verifica_auto", "F");
            Ins.Add("procesado_bof", "@procesado_bof", "F");
            Ins.Add("requiere_preparacion", "@requiere_preparacion", "F");
            Ins.Add("tipo_preparacion", "@tipo_preparacion", "F");
            Ins.Add("estado_preparacion", "@estado_preparacion", "F");
            Ins.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", "F");
            Ins.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Ins.Add("idbodegamuelle", "@idbodegamuelle", "F");
            Ins.Add("idprioridadpicking", "@idprioridadpicking", "F");
            Ins.Add("idtipopicking", "@idtipopicking", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_picking_enc);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_picking_enc oBeTrans_picking_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_picking_enc");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idubicacionpicking", "@idubicacionpicking", "F");
            Upd.Add("fecha_picking", "@fecha_picking", "F");
            Upd.Add("hora_ini", "@hora_ini", "F");
            Upd.Add("hora_fin", "@hora_fin", "F");
            Upd.Add("estado", "@estado", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("detalle_operador", "@detalle_operador", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("verifica_auto", "@verifica_auto", "F");
            Upd.Add("procesado_bof", "@procesado_bof", "F");
            Upd.Add("requiere_preparacion", "@requiere_preparacion", "F");
            Upd.Add("tipo_preparacion", "@tipo_preparacion", "F");
            Upd.Add("estado_preparacion", "@estado_preparacion", "F");
            Upd.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", "F");
            Upd.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", "F");
            Upd.Add("referencia", "@referencia", "F");
            Upd.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Upd.Add("idbodegamuelle", "@idbodegamuelle", "F");
            Upd.Add("idprioridadpicking", "@idprioridadpicking", "F");
            Upd.Add("idtipopicking", "@idtipopicking", "F");
            Upd.Where("IdPickingEnc = @IdPickingEnc");

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

            Bind(cmd, oBeTrans_picking_enc);

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

    public int Eliminar(IConfiguration config, clsBeTrans_picking_enc oBeTrans_picking_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_picking_enc" +
             "  Where(IdPickingEnc = @IdPickingEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", oBeTrans_picking_enc.IdPickingEnc));

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
            const string sp = "Select * FROM Trans_picking_enc";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_picking_enc pBeTrans_picking_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_picking_enc" +
            " Where(IdPickingEnc = @IdPickingEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingEnc", pBeTrans_picking_enc.IdPickingEnc));


            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_picking_enc, r);
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

    public static List<clsBeTrans_picking_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_picking_enc> lreturnList = new List<clsBeTrans_picking_enc>();

        try
        {
            const string sp = "Select * FROM Trans_picking_enc";

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

                        clsBeTrans_picking_enc vBeTrans_picking_enc = new clsBeTrans_picking_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_picking_enc = new clsBeTrans_picking_enc();
                            Cargar(ref vBeTrans_picking_enc, dr);
                            lreturnList.Add(vBeTrans_picking_enc);
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

            const string sp = "Select ISNULL(Max(IdPickingEnc),0) FROM Trans_picking_enc";

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


            const string sp = "Select ISNULL(Max(IdPickingEnc),0) FROM Trans_picking_enc";

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


    public static void Bind(SqlCommand cmd, clsBeTrans_picking_enc oBeTrans_picking_enc)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", oBeTrans_picking_enc.IdPickingEnc));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_picking_enc.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_picking_enc.IdPropietarioBodega));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionPicking", oBeTrans_picking_enc.IdUbicacionPicking));
        cmd.Parameters.Add(new SqlParameter("@fecha_picking", oBeTrans_picking_enc.Fecha_picking));
        cmd.Parameters.Add(new SqlParameter("@hora_ini", oBeTrans_picking_enc.Hora_ini));
        cmd.Parameters.Add(new SqlParameter("@hora_fin", oBeTrans_picking_enc.Hora_fin));
        cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_picking_enc.Estado));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_picking_enc.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_picking_enc.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_picking_enc.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_picking_enc.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@detalle_operador", oBeTrans_picking_enc.Detalle_operador));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_picking_enc.Activo));
        cmd.Parameters.Add(new SqlParameter("@verifica_auto", oBeTrans_picking_enc.Verifica_auto));
        cmd.Parameters.Add(new SqlParameter("@procesado_bof", oBeTrans_picking_enc.Procesado_bof));
        cmd.Parameters.Add(new SqlParameter("@requiere_preparacion", oBeTrans_picking_enc.Requiere_preparacion));
        cmd.Parameters.Add(new SqlParameter("@tipo_preparacion", oBeTrans_picking_enc.Tipo_preparacion));
        cmd.Parameters.Add(new SqlParameter("@estado_preparacion", oBeTrans_picking_enc.Estado_preparacion));
        cmd.Parameters.Add(new SqlParameter("@fecha_inicio_preparacion", oBeTrans_picking_enc.Fecha_inicio_preparacion));
        cmd.Parameters.Add(new SqlParameter("@fecha_fin_preparacion", oBeTrans_picking_enc.Fecha_fin_preparacion));
        cmd.Parameters.Add(new SqlParameter("@referencia", oBeTrans_picking_enc.Referencia));
        cmd.Parameters.Add(new SqlParameter("@fotografia_verificacion", oBeTrans_picking_enc.Fotografia_verificacion));
        cmd.Parameters.Add(new SqlParameter("@IdBodegaMuelle", oBeTrans_picking_enc.IdBodegaMuelle));
        cmd.Parameters.Add(new SqlParameter("@IdPrioridadPicking", oBeTrans_picking_enc.IdPrioridadPicking));
        cmd.Parameters.Add(new SqlParameter("@IdTipoPicking", oBeTrans_picking_enc.IdTipoPicking));
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeTrans_picking_enc entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        int total = 0;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            bool existe = Existe(entity.IdPickingEnc, connection, isExternalTx ? tx! : localTx!);

            int resultado = existe
                ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

            total += resultado;

            if (!isExternalTx)
                localTx?.Commit();

            return total;
        }
        catch
        {
            if (!isExternalTx)
                localTx?.Rollback();

            throw;
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
    public static bool Existe(int idPickingEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_picking_enc
        WHERE IdPickingEnc = @IdPickingEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPickingEnc", idPickingEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }

}