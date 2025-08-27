using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMSWebAPI.Be;

public class clsLnTrans_re_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_enc oBeTrans_re_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }

        try
        {
            oBeTrans_re_enc.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_enc.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_re_enc.IdMuelle = GetInt("IdMuelle");
            oBeTrans_re_enc.IdUbicacionRecepcion = GetInt("IdUbicacionRecepcion");
            oBeTrans_re_enc.IdTipoTransaccion = GetString("IdTipoTransaccion");
            oBeTrans_re_enc.Fecha_recepcion = GetDate("fecha_recepcion");
            oBeTrans_re_enc.Hora_ini_pc = GetDate("hora_ini_pc");
            oBeTrans_re_enc.Hora_fin_pc = GetDate("hora_fin_pc");
            oBeTrans_re_enc.Muestra_precio = GetBool("muestra_precio");
            oBeTrans_re_enc.Estado = GetString("estado");
            oBeTrans_re_enc.User_agr = GetString("user_agr");
            oBeTrans_re_enc.Fec_agr = GetDate("fec_agr");
            oBeTrans_re_enc.User_mod = GetString("user_mod");
            oBeTrans_re_enc.Fec_mod = GetDate("fec_mod");
            oBeTrans_re_enc.Fecha_tarea = GetDate("fecha_tarea");
            oBeTrans_re_enc.Tomar_fotos = GetBool("tomar_fotos");
            oBeTrans_re_enc.Escanear_rec_ubic = GetBool("escanear_rec_ubic");
            oBeTrans_re_enc.Para_por_codigo = GetBool("para_por_codigo");
            oBeTrans_re_enc.Observacion = GetString("observacion");
            oBeTrans_re_enc.Firma_piloto = GetBytes("firma_piloto");
            oBeTrans_re_enc.Activo = GetBool("activo");
            oBeTrans_re_enc.NoGuia = GetString("NoGuia");
            oBeTrans_re_enc.CorreoEnviado = GetBool("CorreoEnviado");
            oBeTrans_re_enc.Revision_Inconsistencia = GetBool("Revision_Inconsistencia");
            oBeTrans_re_enc.Bloqueada = GetBool("bloqueada");
            oBeTrans_re_enc.Bloqueada_por = GetString("bloqueada_por");
            oBeTrans_re_enc.Idusuariobloqueo = GetInt("idusuariobloqueo");
            oBeTrans_re_enc.Idmotivoanulacionbodega = GetInt("idmotivoanulacionbodega");
            oBeTrans_re_enc.Habilitar_Stock = GetBool("Habilitar_Stock");
            oBeTrans_re_enc.Idvehiculo = GetInt("idvehiculo");
            oBeTrans_re_enc.Idpiloto = GetInt("idpiloto");
            oBeTrans_re_enc.No_Marchamo = GetString("No_Marchamo");
            oBeTrans_re_enc.Mostrar_cantidad_esperada = GetBool("mostrar_cantidad_esperada");
            oBeTrans_re_enc.IdBodega = GetInt("IdBodega");
            oBeTrans_re_enc.Carta_cupo = GetString("carta_cupo");
            oBeTrans_re_enc.IdEstado_defecto_recepcion = GetInt("IdEstado_defecto_recepcion");
            oBeTrans_re_enc.No_contenedor = GetString("no_contenedor");
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

    public static int Insertar(IConfiguration config, clsBeTrans_re_enc oBeTrans_re_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_enc");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idubicacionrecepcion", "@idubicacionrecepcion", "F");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("fecha_recepcion", "@fecha_recepcion", "F");
            Ins.Add("hora_ini_pc", "@hora_ini_pc", "F");
            Ins.Add("hora_fin_pc", "@hora_fin_pc", "F");
            Ins.Add("muestra_precio", "@muestra_precio", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("fecha_tarea", "@fecha_tarea", "F");
            Ins.Add("tomar_fotos", "@tomar_fotos", "F");
            Ins.Add("escanear_rec_ubic", "@escanear_rec_ubic", "F");
            Ins.Add("para_por_codigo", "@para_por_codigo", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("firma_piloto", "@firma_piloto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("noguia", "@noguia", "F");
            Ins.Add("correoenviado", "@correoenviado", "F");
            Ins.Add("revision_inconsistencia", "@revision_inconsistencia", "F");
            Ins.Add("bloqueada", "@bloqueada", "F");
            Ins.Add("bloqueada_por", "@bloqueada_por", "F");
            Ins.Add("idusuariobloqueo", "@idusuariobloqueo", "F");
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Ins.Add("habilitar_stock", "@habilitar_stock", "F");
            Ins.Add("idvehiculo", "@idvehiculo", "F");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("no_marchamo", "@no_marchamo", "F");
            Ins.Add("mostrar_cantidad_esperada", "@mostrar_cantidad_esperada", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("carta_cupo", "@carta_cupo", "F");
            Ins.Add("idestado_defecto_recepcion", "@idestado_defecto_recepcion", "F");
            Ins.Add("no_contenedor", "@no_contenedor", "F");

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

            Bind(cmd, oBeTrans_re_enc);

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

    public static int Insertar(IConfiguration config, clsBeTrans_re_enc oBeTrans_re_enc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_enc");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idubicacionrecepcion", "@idubicacionrecepcion", "F");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("fecha_recepcion", "@fecha_recepcion", "F");
            Ins.Add("hora_ini_pc", "@hora_ini_pc", "F");
            Ins.Add("hora_fin_pc", "@hora_fin_pc", "F");
            Ins.Add("muestra_precio", "@muestra_precio", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("fecha_tarea", "@fecha_tarea", "F");
            Ins.Add("tomar_fotos", "@tomar_fotos", "F");
            Ins.Add("escanear_rec_ubic", "@escanear_rec_ubic", "F");
            Ins.Add("para_por_codigo", "@para_por_codigo", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("firma_piloto", "@firma_piloto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("noguia", "@noguia", "F");
            Ins.Add("correoenviado", "@correoenviado", "F");
            Ins.Add("revision_inconsistencia", "@revision_inconsistencia", "F");
            Ins.Add("bloqueada", "@bloqueada", "F");
            Ins.Add("bloqueada_por", "@bloqueada_por", "F");
            Ins.Add("idusuariobloqueo", "@idusuariobloqueo", "F");
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Ins.Add("habilitar_stock", "@habilitar_stock", "F");
            Ins.Add("idvehiculo", "@idvehiculo", "F");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("no_marchamo", "@no_marchamo", "F");
            Ins.Add("mostrar_cantidad_esperada", "@mostrar_cantidad_esperada", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("carta_cupo", "@carta_cupo", "F");
            Ins.Add("idestado_defecto_recepcion", "@idestado_defecto_recepcion", "F");
            Ins.Add("no_contenedor", "@no_contenedor", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_re_enc);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_re_enc oBeTrans_re_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_re_enc");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idmuelle", "@idmuelle", "F");
            Upd.Add("idubicacionrecepcion", "@idubicacionrecepcion", "F");
            Upd.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Upd.Add("fecha_recepcion", "@fecha_recepcion", "F");
            Upd.Add("hora_ini_pc", "@hora_ini_pc", "F");
            Upd.Add("hora_fin_pc", "@hora_fin_pc", "F");
            Upd.Add("muestra_precio", "@muestra_precio", "F");
            Upd.Add("estado", "@estado", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("fecha_tarea", "@fecha_tarea", "F");
            Upd.Add("tomar_fotos", "@tomar_fotos", "F");
            Upd.Add("escanear_rec_ubic", "@escanear_rec_ubic", "F");
            Upd.Add("para_por_codigo", "@para_por_codigo", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Add("firma_piloto", "@firma_piloto", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("noguia", "@noguia", "F");
            Upd.Add("correoenviado", "@correoenviado", "F");
            Upd.Add("revision_inconsistencia", "@revision_inconsistencia", "F");
            Upd.Add("bloqueada", "@bloqueada", "F");
            Upd.Add("bloqueada_por", "@bloqueada_por", "F");
            Upd.Add("idusuariobloqueo", "@idusuariobloqueo", "F");
            Upd.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Upd.Add("habilitar_stock", "@habilitar_stock", "F");
            Upd.Add("idvehiculo", "@idvehiculo", "F");
            Upd.Add("idpiloto", "@idpiloto", "F");
            Upd.Add("no_marchamo", "@no_marchamo", "F");
            Upd.Add("mostrar_cantidad_esperada", "@mostrar_cantidad_esperada", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("carta_cupo", "@carta_cupo", "F");
            Upd.Add("idestado_defecto_recepcion", "@idestado_defecto_recepcion", "F");
            Upd.Add("no_contenedor", "@no_contenedor", "F");
            Upd.Where("IdRecepcionEnc = @IdRecepcionEnc");

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

            Bind(cmd, oBeTrans_re_enc);

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

    public int Eliminar(IConfiguration config, clsBeTrans_re_enc oBeTrans_re_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_enc" +
             "  Where(IdRecepcionEnc = @IdRecepcionEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_enc.IdRecepcionEnc));

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
            const string sp = "Select * FROM Trans_re_enc";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_enc pBeTrans_re_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_enc" +
            " Where(IdRecepcionEnc = @IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_enc.IdRecepcionEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_enc, r);
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

    public static List<clsBeTrans_re_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_enc> lreturnList = new List<clsBeTrans_re_enc>();

        try
        {
            const string sp = "Select * FROM Trans_re_enc";

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

                        clsBeTrans_re_enc vBeTrans_re_enc = new clsBeTrans_re_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_enc = new clsBeTrans_re_enc();
                            Cargar(ref vBeTrans_re_enc, dr);
                            lreturnList.Add(vBeTrans_re_enc);
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

            const string sp = "Select ISNULL(Max(IdRecepcionEnc),0) FROM Trans_re_enc";

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


            const string sp = "Select ISNULL(Max(IdRecepcionEnc),0) FROM Trans_re_enc";

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
    public static void Bind(SqlCommand cmd, clsBeTrans_re_enc oBeTrans_re_enc)
    {
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_enc.IdRecepcionEnc == 0 ? DBNull.Value : oBeTrans_re_enc.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_re_enc.IdPropietarioBodega == 0 ? DBNull.Value : oBeTrans_re_enc.IdPropietarioBodega));
        cmd.Parameters.Add(new SqlParameter("@IdMuelle", oBeTrans_re_enc.IdMuelle == 0 ? DBNull.Value : oBeTrans_re_enc.IdMuelle));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionRecepcion", oBeTrans_re_enc.IdUbicacionRecepcion == 0 ? DBNull.Value : oBeTrans_re_enc.IdUbicacionRecepcion));
        cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", string.IsNullOrEmpty(oBeTrans_re_enc.IdTipoTransaccion) ? DBNull.Value : oBeTrans_re_enc.IdTipoTransaccion));
        cmd.Parameters.Add(new SqlParameter("@fecha_recepcion", oBeTrans_re_enc.Fecha_recepcion));
        cmd.Parameters.Add(new SqlParameter("@hora_ini_pc", oBeTrans_re_enc.Hora_ini_pc));
        cmd.Parameters.Add(new SqlParameter("@hora_fin_pc", oBeTrans_re_enc.Hora_fin_pc));
        cmd.Parameters.Add(new SqlParameter("@muestra_precio", oBeTrans_re_enc.Muestra_precio));
        cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_re_enc.Estado));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_re_enc.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_re_enc.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_re_enc.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_re_enc.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@fecha_tarea", oBeTrans_re_enc.Fecha_tarea));
        cmd.Parameters.Add(new SqlParameter("@tomar_fotos", oBeTrans_re_enc.Tomar_fotos));
        cmd.Parameters.Add(new SqlParameter("@escanear_rec_ubic", oBeTrans_re_enc.Escanear_rec_ubic));
        cmd.Parameters.Add(new SqlParameter("@para_por_codigo", oBeTrans_re_enc.Para_por_codigo));
        cmd.Parameters.Add(new SqlParameter("@observacion", oBeTrans_re_enc.Observacion));
        cmd.Parameters.Add(new SqlParameter("@firma_piloto", oBeTrans_re_enc.Firma_piloto ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_re_enc.Activo));
        cmd.Parameters.Add(new SqlParameter("@NoGuia", oBeTrans_re_enc.NoGuia));
        cmd.Parameters.Add(new SqlParameter("@CorreoEnviado", oBeTrans_re_enc.CorreoEnviado));
        cmd.Parameters.Add(new SqlParameter("@Revision_Inconsistencia", oBeTrans_re_enc.Revision_Inconsistencia));
        cmd.Parameters.Add(new SqlParameter("@bloqueada", oBeTrans_re_enc.Bloqueada));
        cmd.Parameters.Add(new SqlParameter("@bloqueada_por", oBeTrans_re_enc.Bloqueada_por));
        cmd.Parameters.Add(new SqlParameter("@idusuariobloqueo", oBeTrans_re_enc.Idusuariobloqueo == 0 ? DBNull.Value : oBeTrans_re_enc.Idusuariobloqueo));
        cmd.Parameters.Add(new SqlParameter("@idmotivoanulacionbodega", oBeTrans_re_enc.Idmotivoanulacionbodega == 0 ? DBNull.Value : oBeTrans_re_enc.Idmotivoanulacionbodega));
        cmd.Parameters.Add(new SqlParameter("@Habilitar_Stock", oBeTrans_re_enc.Habilitar_Stock));
        cmd.Parameters.Add(new SqlParameter("@idvehiculo", oBeTrans_re_enc.Idvehiculo == 0 ? DBNull.Value : oBeTrans_re_enc.Idvehiculo));
        cmd.Parameters.Add(new SqlParameter("@idpiloto", oBeTrans_re_enc.Idpiloto == 0 ? DBNull.Value : oBeTrans_re_enc.Idpiloto));
        cmd.Parameters.Add(new SqlParameter("@No_Marchamo", oBeTrans_re_enc.No_Marchamo));
        cmd.Parameters.Add(new SqlParameter("@mostrar_cantidad_esperada", oBeTrans_re_enc.Mostrar_cantidad_esperada));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_re_enc.IdBodega == 0 ? DBNull.Value : oBeTrans_re_enc.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@carta_cupo", oBeTrans_re_enc.Carta_cupo));
        cmd.Parameters.Add(new SqlParameter("@IdEstado_defecto_recepcion", oBeTrans_re_enc.IdEstado_defecto_recepcion == 0 ? DBNull.Value : oBeTrans_re_enc.IdEstado_defecto_recepcion));
        cmd.Parameters.Add(new SqlParameter("@no_contenedor", oBeTrans_re_enc.No_contenedor));
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeTrans_re_enc> listBeReEnc, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            foreach (var entity in listBeReEnc)
            {
                bool existe = Existe(entity.IdRecepcionEnc, connection, isExternalTx ? tx! : localTx!);

                if (existe)
                    Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
                else
                    Insertar(config, entity, connection, isExternalTx ? tx : localTx);
            }

            if (!isExternalTx)
                localTx?.Commit();
        }
        catch (SqlException ex)
        {
            if (!isExternalTx && localTx is not null)
                localTx.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
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

    public static bool Existe(int IdRecepcionEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", IdRecepcionEnc));

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
    public static List<clsBeTrans_re_enc> Get_All_By_IdOrdenCompraEnc(IConfiguration config, int IdOrdenCompraEnc)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_enc> lreturnList = new List<clsBeTrans_re_enc>();

        try
        {
            const string sp = "Select * FROM Trans_re_enc";

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

                        clsBeTrans_re_enc vBeTrans_re_enc = new clsBeTrans_re_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_enc = new clsBeTrans_re_enc();
                            Cargar(ref vBeTrans_re_enc, dr);
                            lreturnList.Add(vBeTrans_re_enc);
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
}