using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_re;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Operador;
using WMS.EntityCore.I_nav_Ped_Compra;
using WMS.DALCore.Transacciones;
using WMS.EntityCore.Producto;
using AppGlobal;
using WMS.EntityCore;
using WMS.DALCore.Stock;
using WMS.EntityCore.Ticket;
using WMS.DALCore.Ticket;
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
            oBeTrans_re_enc.IdEstado_Defecto_Recepcion = GetInt("IdEstado_Defecto_Recepcion");
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

    public static int Insertar(clsBeTrans_re_enc oBeTrans_re_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

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
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_re_enc);
            rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar_3pl(clsBeTrans_re_enc_3pl oBeTrans_re_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

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
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind_3pl(cmd, oBeTrans_re_enc);
            rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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

    public static int Actualizar(clsBeTrans_re_enc oBeTrans_re_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

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
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_re_enc);
            rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Actualizar_3pl(clsBeTrans_re_enc_3pl oBeTrans_re_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

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
            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind_3pl(cmd, oBeTrans_re_enc);
            rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
{
    const string sp = "Select ISNULL(Max(IdRecepcionEnc),0) FROM Trans_re_enc";
    
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

    public static void Bind_3pl(SqlCommand cmd, clsBeTrans_re_enc_3pl oBeTrans_re_enc)
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
    public static void InsertarOActualizar(List<clsBeTrans_re_enc> listBeReEnc, SqlConnection conn, SqlTransaction tx)
    {
        if (listBeReEnc == null)
            throw new ArgumentNullException(nameof(listBeReEnc));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in listBeReEnc)
            {
                if (entity == null)
                    continue;

                bool existe = Existe(entity.IdRecepcionEnc, conn, tx);

                if (existe)
                    Actualizar(entity, conn, tx);
                else
                    Insertar(entity, conn, tx);
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }

    public static void InsertarOActualizar_3pl(List<clsBeTrans_re_enc_3pl> listBeReEnc, SqlConnection conn, SqlTransaction tx)
    {
        if (listBeReEnc == null)
            throw new ArgumentNullException(nameof(listBeReEnc));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in listBeReEnc)
            {
                if (entity == null)
                    continue;

                bool existe = Existe(entity.IdRecepcionEnc, conn, tx);

                if (existe)
                    Actualizar_3pl(entity, conn, tx);
                else
                    Insertar_3pl(entity, conn, tx);
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
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

    public static int Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido(ref clsBeTrans_oc_enc BeOrdenCompraEnc,
                                                                           ref string Resultado,
                                                                           bool CrearRecepcionPorDefecto,
                                                                           clsBeI_nav_config_enc BeMI3Config,
                                                                           ref clsBeTrans_re_enc? OutBeRecepcionEnc,
                                                                           ref clsBeTrans_pe_enc? BePedidoEnc,
                                                                           SqlConnection lConnection,
                                                                           SqlTransaction lTransaction)
    {
        int result = 0;
        OutBeRecepcionEnc = null;
        
        clsBeTrans_re_oc OrdenCompraReOc = new clsBeTrans_re_oc();
        clsBeTarea_hh BeTareaHH = new clsBeTarea_hh();
        clsBeTrans_re_enc BeRecepcionEnc = new clsBeTrans_re_enc();
        int IdBodegaDestino = 0;        
        double TiempoMedioIngresoMinutos;
        List<clsBeTrans_re_det> lBeRecDet = new List<clsBeTrans_re_det>();
        List<clsBeStock_rec> lBeStockRec = new List<clsBeStock_rec>();

        try
        {
            if (!clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction))
            {
                IdBodegaDestino = BeOrdenCompraEnc.IdBodega;

                BeRecepcionEnc.IsNew = true;                
                BeRecepcionEnc.PropietarioBodega = new clsBePropietario_bodega();
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega;                
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega;
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega;
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario.ToString();
                BeRecepcionEnc.Fec_agr = DateTime.Now;
                BeRecepcionEnc.Activo = true;
                BeRecepcionEnc.Estado = "Nuevo";
                BeRecepcionEnc.OrdenCompraRec = new clsBeTrans_re_oc();
                BeRecepcionEnc.OrdenCompraRec.IsNew = true;                
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;

                if (BeRecepcionEnc.PropietarioBodega == null || BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0)
                {
                    throw new Exception("Propietario no válido al crear la recepción");
                }

                // Ingreso con referencia a orden de compra para procesar en HH
                BeRecepcionEnc.IdTipoTransaccion = "HCOC00";

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (BeRecepcionEnc.IdMuelle == 0)
                {
                    throw new Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " + IdBodegaDestino);
                }

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (BeRecepcionEnc.IdUbicacionRecepcion == 0)
                {
                    throw new Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " + IdBodegaDestino);
                }

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction);

                BeRecepcionEnc.Fecha_recepcion = DateTime.Now.Date;
                BeRecepcionEnc.Hora_ini_pc = DateTime.Now;
                BeRecepcionEnc.Hora_fin_pc = DateTime.Now.AddMinutes(TiempoMedioIngresoMinutos);
                BeRecepcionEnc.Muestra_precio = false;
                BeRecepcionEnc.Fec_mod = DateTime.Now;
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario.ToString();
                BeRecepcionEnc.Fecha_tarea = DateTime.Now;
                BeRecepcionEnc.Tomar_fotos = false;
                BeRecepcionEnc.Escanear_rec_ubic = false;
                BeRecepcionEnc.Para_por_codigo = false;
                BeRecepcionEnc.Observacion = "FROMMI3";
                BeRecepcionEnc.Idpiloto = 0;
                BeRecepcionEnc.Idvehiculo = 0;
                BeRecepcionEnc.Habilitar_Stock = false;
                BeRecepcionEnc.IdBodega = IdBodegaDestino;
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1;

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc;
                OrdenCompraReOc.IsNew = true;
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia;
                OrdenCompraReOc.OC = BeOrdenCompraEnc;
                OrdenCompraReOc.Recepcion_ciega = false;
                OrdenCompraReOc.Recepcion_manual = false;
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario.ToString();

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc;

                List<clsBeTrans_re_op> pListOperadorRecepcion = new List<clsBeTrans_re_op>();
                List<clsBeOperador_bodega> pListOperadorBodega = new List<clsBeOperador_bodega>();

                // #EJC20190711: Broadcast a todos los operadores de la bodega con la tarea.
                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (pListOperadorBodega != null)
                {
                    clsBeTrans_re_op BeTransReOp = new clsBeTrans_re_op();

                    foreach (var Op in pListOperadorBodega)
                    {
                        BeTransReOp = new clsBeTrans_re_op();
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega;
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario.ToString();
                        BeTransReOp.Fec_agr = DateTime.Now;
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario.ToString();
                        BeTransReOp.Fec_mod = DateTime.Now;
                        BeTransReOp.IsNew = true;
                        BeTransReOp.UsaHH = true;
                        pListOperadorRecepcion.Add(BeTransReOp);
                    }
                }

                BeTareaHH = new clsBeTarea_hh();
                BeTareaHH.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodegaDestino, BeOrdenCompraEnc.IdPropietarioBodega,lConnection,lTransaction);
                BeTareaHH.IdBodega = IdBodegaDestino;
                BeTareaHH.IdMuelle = BeRecepcionEnc.IdMuelle;
                BeTareaHH.IdEstado = 1;
                BeTareaHH.IdPrioridad = 1;
                BeTareaHH.IdTipoTarea = 1;
                BeTareaHH.IdTransaccion = BeRecepcionEnc.IdRecepcionEnc;
                BeTareaHH.Tipo = 0;
                BeTareaHH.FechaInicio = DateTime.Now;
                BeTareaHH.FechaFin = DateTime.Now.AddHours(2);
                BeTareaHH.DiaCompleto = false;
                BeTareaHH.Descripcion = "";
                BeTareaHH.CreaTarea = true;
                BeTareaHH.IsNew = true;

                switch (BeRecepcionEnc.IdTipoTransaccion.ToString())
                {
                    case "HSOC00":
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra ";
                        break;
                    case "HSOD00":
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia";
                        break;
                    case "HCOC00":
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra";
                        break;
                    case "HCOD00":
                        BeTareaHH.Asunto = "Devolución de Pedido";
                        break;
                    case "HHSR00":
                        BeTareaHH.Asunto = "Ingreso sin referencia";
                        break;
                    case "PICH000":
                        BeTareaHH.Asunto = "Pre-ingreso con HH";
                        break;
                    default:
                        break;
                }

                int i = 0;

                if (BePedidoEnc !=null)
                {

                    // #EJC20190719: Se verifica si en la lista de pedidos del despacho hay pedidos para sucursales WMS.
                    foreach (clsBeTrans_pe_det BePedidoDet in BePedidoEnc.Detalle)
                    {
                        if (BePedidoDet.ListaPickingUbic != null)
                        {
                            clsBeTrans_re_det BeTransReDet = new clsBeTrans_re_det();
                            clsBeI_nav_barras_pallet BeINavBarraPallet = new clsBeI_nav_barras_pallet();
                            clsBeI_nav_barras_pallet BeINavBarraPalletOriginal = new clsBeI_nav_barras_pallet();
                            clsBeStock BeStock = new clsBeStock();
                            clsBeStock_rec BeStockRec = new clsBeStock_rec();

                            foreach (var PickingUbic in BePedidoDet.ListaPickingUbic)
                            {
                                i++;
                                BeTransReDet = new clsBeTrans_re_det();
                                BeTransReDet.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;
                                BeTransReDet.IdProductoBodega = PickingUbic.IdProductoBodega;
                                BeTransReDet.IdPresentacion = PickingUbic.IdPresentacion;
                                BeTransReDet.IdUnidadMedida = PickingUbic.IdUnidadMedida;
                                BeTransReDet.UnidadMedida.IdUnidadMedida = PickingUbic.IdUnidadMedida;
                                BeTransReDet.IdProductoEstado = PickingUbic.IdProductoEstado;
                                BeTransReDet.ProductoEstado.IdEstado = PickingUbic.IdProductoEstado;
                                BeTransReDet.IdOperadorBodega = PickingUbic.IdOperadorBodega_Verifico; // corregir después.
                                BeTransReDet.No_Linea = BePedidoDet.No_linea;
                                BeTransReDet.Cantidad_recibida = PickingUbic.Cantidad_despachada;
                                BeTransReDet.Nombre_producto = PickingUbic.NombreProducto;
                                BeTransReDet.Nombre_presentacion = BePedidoDet.Nom_presentacion;
                                BeTransReDet.Nombre_unidad_medida = BePedidoDet.Nom_unid_med;
                                BeTransReDet.Nombre_producto_estado = BePedidoDet.Nom_estado;
                                BeTransReDet.Lote = PickingUbic.Lote;
                                BeTransReDet.Fecha_vence = PickingUbic.Fecha_vence;
                                BeTransReDet.Fecha_ingreso = DateTime.Now;
                                BeTransReDet.Peso = PickingUbic.Peso_despachado;
                                BeTransReDet.Peso = PickingUbic.Peso_despachado;
                                BeTransReDet.User_agr = OrdenCompraReOc.User_agr;
                                BeTransReDet.Fec_agr = DateTime.Now;
                                BeTransReDet.Observacion = "Reversión de despacho del pedido: " + BePedidoEnc.IdPedidoEnc;
                                BeTransReDet.Codigo_producto = PickingUbic.CodigoProducto;
                                BeTransReDet.Lic_plate = PickingUbic.Lic_plate;
                                BeTransReDet.Pallet_no_estandar = false;
                                BeTransReDet.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc;
                                BeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction) + i;

                                clsBeTrans_oc_det? BeTransOCDet = new clsBeTrans_oc_det();
                                BeTransOCDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(BeOrdenCompraEnc.IdOrdenCompraEnc,
                                                                                                          BePedidoDet.No_linea,
                                                                                                          PickingUbic.IdProductoBodega,
                                                                                                          lConnection,
                                                                                                          lTransaction);


                                if (BeTransOCDet != null)
                                    BeTransReDet.IdOrdenCompraDet = BeTransOCDet.IdOrdenCompraDet;

                                BeTransReDet.IdJornadaSistema = 0;
                                lBeRecDet.Add(BeTransReDet);

                                BeStockRec.IdStockRec = clsLnStock_rec.MaxID(lConnection, lTransaction) + 1;
                                BeStockRec.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega;
                                BeStockRec.IdProductoBodega = PickingUbic.IdProductoBodega;
                                BeStockRec.IdProductoEstado = PickingUbic.IdProductoEstado;
                                BeStockRec.ProductoEstado.IdEstado = PickingUbic.IdProductoEstado;
                                BeStockRec.IdPresentacion = PickingUbic.IdPresentacion;
                                BeStockRec.IdUnidadMedida = PickingUbic.IdUnidadMedida;
                                BeStockRec.IdUbicacion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(BeOrdenCompraEnc.IdBodega,
                                                                                                           lConnection,
                                                                                                           lTransaction);
                                BeStockRec.IdUbicacion_anterior = PickingUbic.IdUbicacion;
                                BeStockRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;
                                BeStockRec.IdRecepcionDet = BeTransReDet.IdRecepcionDet;
                                BeStockRec.Lote = PickingUbic.Lote;
                                BeStockRec.Lic_plate = PickingUbic.Lic_plate;
                                BeStockRec.Serial = PickingUbic.Serial;
                                BeStockRec.Cantidad = PickingUbic.Cantidad_despachada;
                                BeStockRec.Fecha_ingreso = DateTime.Now;
                                BeStockRec.Fecha_vence = PickingUbic.Fecha_vence;
                                BeStockRec.User_agr = BeOrdenCompraEnc.User_Agr;
                                BeStockRec.Fec_agr = DateTime.Now;
                                BeStockRec.User_mod = BeOrdenCompraEnc.User_Agr;
                                BeStockRec.Fec_mod = DateTime.Now;
                                BeStockRec.Activo = true;
                                BeStockRec.Peso = PickingUbic.Peso_despachado;
                                BeStockRec.No_linea = BePedidoDet.No_linea;
                                BeStockRec.Atributo_variante_1 = BePedidoDet.Atributo_variante_1;
                                BeStockRec.IdBodega = BePedidoEnc.IdBodega;
                                BeStockRec.Pallet_no_estandar = false;
                                lBeStockRec.Add(BeStockRec);
                            }
                        }
                    }
                }             

                Guardar(BeOrdenCompraEnc.IdBodega,
                        BeTareaHH,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        lBeRecDet,
                        null,
                        pListOperadorRecepcion,
                        null,
                        null,
                        null,
                        lBeStockRec,
                        null,
                        lConnection,
                        lTransaction);

                int vIdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeRecepcionEnc.IdBodega, lConnection, lTransaction);

                Finalizar_Recepcion(BeRecepcionEnc,
                                    false,
                                    BeOrdenCompraEnc.IdOrdenCompraEnc,
                                    BeRecepcionEnc.IdRecepcionEnc,
                                    vIdEmpresa,
                                    BeRecepcionEnc.IdBodega,
                                    BeRecepcionEnc.User_agr,
                                    lBeRecDet,
                                    false,
                                    lConnection,
                                    lTransaction);

                result = 1;
                OutBeRecepcionEnc = BeRecepcionEnc;
            }

            return result;
        }
        catch (Exception)
        {     
            throw;
        }
    }    

    public static int Guardar(int IdBodega,
                             clsBeTarea_hh pObjTareaHH,
                             clsBeTrans_re_enc pRecEnc,
                             clsBeTrans_re_oc pRecOrdenCompra,
                             List<clsBeTrans_re_det>? pListRecDet,
                             List<clsBeTrans_re_det_parametros>? pListRecDetParam,
                             List<clsBeTrans_re_op>? pListRecOpe,
                             List<clsBeTrans_re_fact>? pListRecFact,
                             List<clsBeTrans_re_img>? pListRecImg,
                             List<clsBeStock_se_rec>? pListStockRecSer,
                             List<clsBeStock_rec>? pListStockRec,
                             List<clsBeProducto_pallet>? pListProductoPallet,
                             SqlConnection lConnection,
                             SqlTransaction lTransaction)
    {
        try
        {
            Guarda_Trans_re_enc(pRecEnc, lConnection, lTransaction);

            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc, pRecOrdenCompra, lConnection, lTransaction);

            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, true, pRecEnc, lConnection, lTransaction);

            if (pRecEnc.IdTipoTransaccion != clsBeTrans_re_enc.pTipoTrans.PICH000.ToString())
            {
                clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra, pListRecDet, lConnection, lTransaction);
            }

            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc, pListRecDet, pListRecDetParam, lConnection, lTransaction);

            clsLnTrans_re_op.Guarda_Trans_Re_Op(pRecEnc.IdRecepcionEnc, pListRecOpe, lConnection, lTransaction);

            clsLnTrans_re_img.Guarda_Trans_Re_Img(pRecEnc.IdRecepcionEnc, pListRecImg, lConnection, lTransaction);

            clsLnTrans_re_fact.Guarda_facturas_asoc(pRecEnc.IdRecepcionEnc, pListRecFact, lConnection, lTransaction);

            clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc, IdBodega, pListStockRec, lConnection, lTransaction);

            clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc, pListProductoPallet, lConnection, lTransaction);

            clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer, pListStockRec, lConnection, lTransaction);

            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH, lConnection, lTransaction);

            return pRecEnc.IdRecepcionEnc;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void Guarda_Trans_re_enc(clsBeTrans_re_enc pRecEnc,
                                            SqlConnection lConnection,
                                            SqlTransaction lTransaction)
    {
        try
        {
            if (pRecEnc.IsNew)
            {
                Insertar(pRecEnc, lConnection, lTransaction);
            }
            else
            {
                Actualizar(pRecEnc, lConnection, lTransaction);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static double Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(int pIdRecepcionEnc,
                                                                                      int pIdRecepcionDet,
                                                                                      SqlConnection pConection,
                                                                                      SqlTransaction pTransaction)
    {
        double cantidadRecibida = 0;

        using (SqlCommand lCommand = new SqlCommand())
        {
            string vSQL = @"SELECT ISNULL(SUM(det.cantidad_recibida),0) as cant FROM Trans_re_det AS det
                        WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdRecepcionDet=@IdRecepcioDet";

            lCommand.CommandType = CommandType.Text;
            lCommand.CommandText = vSQL;
            lCommand.Connection = pConection;
            lCommand.Transaction = pTransaction;

            lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);
            lCommand.Parameters.AddWithValue("@IdRecepcioDet", pIdRecepcionDet);

            object lReturnValue = lCommand.ExecuteScalar();

            if (lReturnValue != DBNull.Value && lReturnValue != null)
            {
                cantidadRecibida = Convert.ToDouble(lReturnValue);
            }
        }

        return cantidadRecibida;
    }
    public static void Finalizar_Recepcion(clsBeTrans_re_enc pRecEnc,
                                          bool backOrder,
                                          int pIdOrdenCompraEnc,
                                          int pIdRecepcionEnc,
                                          int pIdEmpresa,
                                          int pIdBodega,
                                          string pIdUsuario,
                                          List<clsBeTrans_re_det> pListObjDetR,
                                          bool pHabilitarStock,
                                          SqlConnection lConnection,
                                          SqlTransaction lTransaction)
    {
        clsBeTms_ticket? BeTMSTicket = new clsBeTms_ticket();

        try
        {
            List<clsBeStock_rec> listaStockRec = clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc, lConnection, lTransaction);

            if (!Finalizada(pIdRecepcionEnc, lConnection, lTransaction))
            {
                Actualizar_Estado_Cerrado_Recepcion(pIdRecepcionEnc, lConnection, lTransaction);                

                if (!Registros_Pendientes_Push(pIdRecepcionEnc, lConnection, lTransaction))
                {
                    if (Reglas_De_Recepcion_Permiten_Ingreso(pRecEnc, lConnection, lTransaction))
                    {
                        int lMaxS = clsLnStock.MaxID(lConnection, lTransaction);

                        if (listaStockRec != null && listaStockRec.Count > 0)
                        {
                            if (!pHabilitarStock)
                            {                                

                                Habilitar_Stock_Desde_StockRec(pIdEmpresa, 
                                                               pIdBodega, 
                                                               pIdOrdenCompraEnc,
                                                               Convert.ToInt32(pIdUsuario),
                                                               listaStockRec, 
                                                               pListObjDetR, 
                                                               lConnection, 
                                                               lTransaction);
                            }
                            else
                            {
                                Habilitar_Stock_Desde_Detalle_Recepcion(pIdRecepcionEnc, 
                                                                        pIdOrdenCompraEnc, 
                                                                        Convert.ToInt32(pIdUsuario),
                                                                        pIdEmpresa, 
                                                                        pIdBodega, 
                                                                        pRecEnc, 
                                                                        lConnection, 
                                                                        lTransaction);
                            }
                        }

                        clsLnStock_rec.Actualiza_Stock_Rec(listaStockRec, lConnection, lTransaction);

                        Actualizar_Estado_Pedido_Ingreso(pIdOrdenCompraEnc, pIdRecepcionEnc, lConnection, lTransaction, backOrder);

                        Actualizar_Hora_Fin_Recepcion(pIdOrdenCompraEnc, pIdRecepcionEnc, lConnection, lTransaction);

                        clsBeTarea_hh BeTareaHH = clsLnTarea_hh.GetSingle(1, 
                                                                          pIdRecepcionEnc, 
                                                                          pRecEnc.PropietarioBodega.IdPropietario,
                                                                          lConnection, 
                                                                          lTransaction);

                        if (BeTareaHH != null)
                        {
                            if (BeTareaHH.IdEstado != 4)
                            {
                                clsLnTarea_hh.Finalizar_Tarea_Recepcion(pIdRecepcionEnc, lConnection, lTransaction);
                            }
                            else
                            {
                                throw new Exception("Error_202211011918: Al parecer la recepción ya fue finalizada.");
                            }
                        }

                        if (pIdOrdenCompraEnc != 0)
                        {
                            BeTMSTicket = clsLnTrans_oc_enc.Get_BeTicket_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, 
                                                                                             lConnection, 
                                                                                             lTransaction);

                            if (BeTMSTicket != null)
                            {
                                clsLnTms_ticket.Actualizar_Tms_Ticket_Finalizado(BeTMSTicket.IdTicket, lConnection, lTransaction);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Error_20220308: La recepción tiene registros pendientes de push");
                }
            }
            else
            {
                throw new Exception("Error_20220121_0004: La recepción fue finalizada previamente.");
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }

    

    private static bool Habilitar_Stock_Desde_Detalle_Recepcion(int pIdRecepcionEnc,
                                                               int pIdOrdenCompraEnc,
                                                               int pIdUsuario,
                                                               int pIdEmpresa,
                                                               int pIdBodega,
                                                               clsBeTrans_re_enc pBeReEnc,
                                                               SqlConnection lConnection,
                                                               SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            clsBeTrans_re_enc? BeTransReEnc = GetSingle(pIdRecepcionEnc, lConnection, lTransaction);

            bool lUsaHH = false;
            if (BeTransReEnc !=null)
            {
                lUsaHH = clsLnTrans_re_tr.UsaHH(BeTransReEnc.IdTipoTransaccion, lConnection, lTransaction);

                if (!lUsaHH && BeTransReEnc.IdTipoTransaccion != clsBeTrans_re_enc.pTipoTrans.MCOC00.ToString())
                {
                    if (clsLnStock.Insertar_Stock_Recepcion(BeTransReEnc.Detalle,
                                                            pIdUsuario,
                                                            pIdEmpresa,
                                                            pIdBodega,
                                                            lConnection,
                                                            lTransaction))
                    {
                        if (clsLnI_nav_transacciones_out.Insertar_Ingreso(pIdEmpresa,
                                                                         pIdBodega,
                                                                         BeTransReEnc.Detalle,
                                                                         pIdOrdenCompraEnc,
                                                                         pIdUsuario,
                                                                         BeTransReEnc.PropietarioBodega.IdPropietarioBodega,
                                                                         lConnection,
                                                                         lTransaction))
                        {
                            result = true;
                        }
                    }
                }
            }            
        }
        catch (Exception)
        {         
            throw;
        }

        return result;
    }

    private static bool Habilitar_Stock_Desde_StockRec(int pIdEmpresa,
                                                      int pIdBodega,
                                                      int pIdOrdenCompraEnc,
                                                      int pIdUsuario,
                                                      List<clsBeStock_rec> listaStockRec,
                                                      List<clsBeTrans_re_det> pListObjDetR,
                                                      SqlConnection lConnection,
                                                      SqlTransaction lTransaction)
    {
        try
        {
            int lMaxS = clsLnStock.MaxID(lConnection, lTransaction);
            int vIdPropietarioBodega = 0;

            clsBeEmpresa? BeEmpresa = clsLnEmpresa.GetSingle(pIdEmpresa, lConnection, lTransaction);

            foreach (clsBeStock_rec BeStockRec in listaStockRec)
            {
                BeStockRec.IdBodega = pIdBodega;
                clsBeStock BeStock = new clsBeStock();
                clsPublic.CopyObject(BeStockRec, ref BeStock);

                lMaxS += 1;
                BeStock.IdStock = lMaxS;

                clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                      pIdBodega,
                                                                      pIdUsuario,
                                                                      BeStockRec,
                                                                      lConnection,
                                                                      lTransaction);

                clsLnStock.Insertar(BeStock, lConnection, lTransaction);

                clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(BeStockRec,
                                                                        lMaxS,
                                                                        lConnection,
                                                                        lTransaction);

                clsLnStock_se.Insertar_Stock_Serializado_Recepcion(BeStockRec,
                                                                   lMaxS,
                                                                   lConnection,
                                                                   lTransaction);

                BeStockRec.Regularizado = true;
                BeStockRec.Fecha_regularizacion = DateTime.Now;

                vIdPropietarioBodega = BeStockRec.IdPropietarioBodega;
            }

            clsLnI_nav_transacciones_out.Insertar_Ingreso(pIdEmpresa,
                                                          pIdBodega,
                                                          pListObjDetR,
                                                          pIdOrdenCompraEnc,
                                                          pIdUsuario,
                                                          vIdPropietarioBodega,
                                                          lConnection,
                                                          lTransaction);

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Reglas_De_Recepcion_Permiten_Ingreso(clsBeTrans_re_enc pRecEnc,
                                                            SqlConnection lConnection,
                                                            SqlTransaction lTransaction)
    {
        try
        {
            if (pRecEnc?.OrdenCompraRec?.OC == null)
                return true;

            if (pRecEnc.OrdenCompraRec.OC.IdTipoIngresoOC == (int) clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Inventario_Inicial)
                return true;

            int idPropietario = clsLnPropietarios.Get_IdPropietario(pRecEnc.PropietarioBodega.IdBodega,
                                                                  pRecEnc.PropietarioBodega.IdPropietarioBodega,
                                                                  lConnection,
                                                                  lTransaction);

            if (idPropietario == 0)
                idPropietario = pRecEnc.PropietarioBodega.IdPropietario;

            List<clsBePropietario_reglas_enc>? ListaRegla = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(idPropietario,
                                                                                                                 lConnection,
                                                                                                                 lTransaction);

            ListaRegla = ListaRegla?.FindAll(x => x.ReglasDet?.Count > 0);

            var BeReglaRec = new clsBeReglas_RecepcionRes
            {
                PermitirProductoFaltantes = true,
                PermitirProductosAdicionales = true,
                PermitirCantidadFaltantePorProducto = true,
                PermitirCantidadSobrantePorProducto = true,
                PermitirCostoDiferentePorProducto = true,
                PermitirPesoMenor = true,
                PermitirPesoMayor = true
            };

            if (ListaRegla?.Count > 0)
            {
                var vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 1);
                BeReglaRec.PermitirProductoFaltantes = vReglaProp?.Regla?.Rechazar ?? true;

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 2);
                BeReglaRec.PermitirProductosAdicionales = !(vReglaProp?.Regla?.Rechazar ?? false);

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 3);
                BeReglaRec.PermitirCantidadFaltantePorProducto = vReglaProp?.Regla?.Rechazar ?? true;

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 4);
                BeReglaRec.PermitirCantidadSobrantePorProducto = !(vReglaProp?.Regla?.Rechazar ?? false);

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 5);
                BeReglaRec.PermitirCostoDiferentePorProducto = vReglaProp?.Regla?.Rechazar ?? true;

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 6);
                BeReglaRec.PermitirPesoMenor = vReglaProp?.Regla?.Rechazar ?? true;

                vReglaProp = ListaRegla.Find(x => x.IdReglaRecepcion == 7);
                BeReglaRec.PermitirPesoMayor = vReglaProp?.Regla?.Rechazar ?? true;
            }

            if (ListaRegla?.Count > 0 &&
                pRecEnc.OrdenCompraRec?.IdOrdenCompraEnc > 0 &&
                !pRecEnc.OrdenCompraRec.IsNew)
            {
                clsBeTrans_oc_enc? ObjOC = clsLnTrans_oc_enc.GetSingle(pRecEnc.OrdenCompraRec.IdOrdenCompraEnc,
                                                                       lConnection,
                                                                       lTransaction);

                if (ObjOC?.IdOrdenCompraEnc > 0 && !ObjOC.IsNew && ObjOC.DetalleOC?.Count > 0)
                {
                    pRecEnc.Detalle = clsLnTrans_re_det.Get_All_By_IdRecepcionEnc(pRecEnc.IdRecepcionEnc,
                                                                                  lConnection,
                                                                                  lTransaction) ?? new List<clsBeTrans_re_det>();

                    foreach (var ocd in ObjOC.DetalleOC)
                    {
                        double vCantidadOCUMBas = ocd.Cantidad;
                        clsBeProducto_presentacion? vPresRec = null;

                        if (ocd.IdPresentacion != 0)
                        {
                            vPresRec = clsLnProducto_presentacion.GetSingle(ocd.IdPresentacion, lConnection, lTransaction);
                        }

                        var lRecDetByProd = pRecEnc.Detalle.FindAll(x => x.IdProductoBodega == ocd.IdProductoBodega && x.No_Linea == ocd.No_Linea);
                        double vCantidadRecibidaUmBas = 0;
                        double vPesoRecibido = 0;
                        double vCostoRec = 0;

                        if (lRecDetByProd.Count > 0)
                        {
                            foreach (var RecDetProd in lRecDetByProd)
                            {
                                if (RecDetProd.IdPresentacion != 0)
                                {
                                    vPresRec = clsLnProducto_presentacion.GetSingle(RecDetProd.IdPresentacion, lConnection, lTransaction);
                                    if (vPresRec?.EsPallet == true)
                                    {
                                        vCantidadRecibidaUmBas += RecDetProd.Cantidad_recibida * vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima;
                                    }
                                    else
                                    {
                                        vCantidadRecibidaUmBas += RecDetProd.Cantidad_recibida;
                                    }
                                }
                                else
                                {
                                    vCantidadRecibidaUmBas += RecDetProd.Cantidad_recibida;
                                }

                                vCostoRec = RecDetProd.Costo;
                                vPesoRecibido += RecDetProd.Peso;
                            }
                        }
                        else if (!BeReglaRec.PermitirProductoFaltantes)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite faltante de producto. Producto en OC.: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Cantidad O.C.: {ocd.Cantidad} Cantidad Recepción.: {ocd.Cantidad_recibida}");
                        }

                        double vCantidadOriginalUMBas = vPresRec?.Factor > 0 ? vCantidadOCUMBas * vPresRec.Factor : vCantidadOCUMBas;
                        double vDiferenciaCantUmBas = Math.Round(vCantidadOriginalUMBas - vCantidadRecibidaUmBas, 6);
                        double vDiferenciaCosto = Math.Round(ocd.Costo - vCostoRec, 2);
                        double vDiferenciaPeso = ocd.Peso - vPesoRecibido;

                        if (vDiferenciaCantUmBas > 0 && !BeReglaRec.PermitirCantidadFaltantePorProducto)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite recibir menos producto que el indicado por la orden de compra. Producto: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Cantidad O.C.: {vCantidadOCUMBas} Cantidad Recepción.: {vCantidadRecibidaUmBas}");
                        }

                        if (vDiferenciaCantUmBas < 0 && !BeReglaRec.PermitirCantidadSobrantePorProducto)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite recibir más producto que el indicado por la orden de compra. Producto: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Cantidad O.C.: {vCantidadOCUMBas} Cantidad Recepción.: {vCantidadRecibidaUmBas}");
                        }

                        if (vDiferenciaCosto != 0 && !BeReglaRec.PermitirCostoDiferentePorProducto)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite recibir productos con costo diferente que el indicado por la orden de compra. Producto: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Costo en O.C.: {ocd.Costo} Costo en Recepción.: {vCostoRec}");
                        }

                        if (vDiferenciaPeso > 0 && !BeReglaRec.PermitirPesoMenor)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite recibir menos peso que el indicado por la orden de compra. Producto: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Peso O.C.: {ocd.Peso} Peso Recepción.: {vPesoRecibido}");
                        }

                        if (vDiferenciaPeso < 0 && !BeReglaRec.PermitirPesoMayor)
                        {
                            throw new Exception($"La regla de recepción configurada para el propietario no permite recibir más peso que el indicado por la orden de compra. Producto: \"{ocd.Producto?.codigo} - {ocd.Producto?.nombre}\" Peso O.C.: {ocd.Peso} Peso Recepción.: {vPesoRecibido}");
                        }
                    }
                }
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Finalizada(int pIdRecepcionEnc,
                                 SqlConnection lConnection,
                                 SqlTransaction lTransaction)
    {
        bool Finalizada = false;

        try
        {
            string vSQL = "SELECT Estado FROM trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

                object? lValue = lCommand.ExecuteScalar();
                if (lValue != null && Convert.ToString(lValue) == "Cerrado")
                {
                    Finalizada = true;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Finalizada;
    }
    public static void Actualizar_Estado_Cerrado_Recepcion(int pIdRecepcionEnc,
                                                          SqlConnection pConnection,
                                                          SqlTransaction pTransaction)  
    {
        using (SqlCommand lCommand = new SqlCommand())
        {
            string sp = @"UPDATE trans_re_enc SET 
                     Estado=@Estado 
                     WHERE IdRecepcionEnc=@IdRecepcionEnc";

            lCommand.CommandType = CommandType.Text;
            lCommand.CommandText = sp;
            lCommand.Connection = pConnection;
            lCommand.Transaction = pTransaction;

            lCommand.Parameters.AddWithValue("@Estado", "Cerrado");
            lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

            lCommand.ExecuteNonQuery();
        }
    }
    public static bool Registros_Pendientes_Push(int pIdRecepcionEnc,
                                                SqlConnection lConnection,
                                                SqlTransaction lTransaction)
    {
        bool Registros_Pendientes_Push = false;

        try
        {
            try
            {
                string pSQL = @"SELECT *
                           FROM trans_re_det
                           WHERE IdRecepcionEnc = @IdRecepcionEnc  AND
                                 lic_plate NOT IN (SELECT l.lic_plate
                           FROM i_nav_transacciones_push p inner join 
                                trans_oc_det_lote l on p.IdOrdenCompra = l.IdOrdenCompraEnc and 
                                l.Ubicacion = p.documento_ubicacion
                           WHERE IdRecepcionEnc  = @IdRecepcionEnc and l.lic_plate<>'' )";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(pSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        string vDocumentoUbicacion = "";
                        string vRecepcionAlmacen = "";
                        string vTipoPush = "Push_Recepcion_Produccion_To_NAV_For_BYB";
                        int IdRecepcionDet, pIdUsuario = 1;
                        clsBeTrans_re_det BeTransReDet;

                        foreach (DataRow dr in lDT.Rows)
                        {
                            IdRecepcionDet = (dr["IdRecepcionDet"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdRecepcionDet"]);

                            BeTransReDet = new clsBeTrans_re_det();
                            BeTransReDet.IdRecepcionDet = IdRecepcionDet;
                            BeTransReDet.IdOrdenCompraEnc = (dr["IdOrdenCompraEnc"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdOrdenCompraEnc"]);
                            BeTransReDet.IdProductoBodega = (dr["IdProductoBodega"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdProductoBodega"]);
                            BeTransReDet.No_Linea = (dr["No_Linea"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["No_Linea"]);                            
                            BeTransReDet.Lic_plate = (dr["Lic_plate"] == DBNull.Value || dr["Lic_plate"] == null) ? "" : Convert.ToString(dr["Lic_plate"])!;                            
                            BeTransReDet.Fecha_vence = (dr["Fecha_vence"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["Fecha_vence"]);

                            vDocumentoUbicacion = clsLnTrans_oc_det_lote.Get_Ubicacion_By_BeTransReDet(BeTransReDet, lConnection, lTransaction);

                            if (vDocumentoUbicacion != "")
                            {
                                clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                           vRecepcionAlmacen,
                                                                                           vTipoPush, "",
                                                                                           pIdRecepcionEnc,
                                                                                           IdRecepcionDet,
                                                                                           pIdUsuario,
                                                                                           lConnection,
                                                                                           lTransaction);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar la transaccion de push " + ex.Message);
            }

            string vSQL = @"SELECT count(IdTransaccionPush) AS Cant 
                       FROM i_nav_transacciones_push 
                       WHERE IdRecepcionEnc = @IdRecepcionEnc AND Enviado_A_ERP = 0";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);
                object lValue = lCommand.ExecuteScalar();
                Registros_Pendientes_Push = (Convert.ToInt32(lValue) > 0);
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Registros_Pendientes_Push;
    }

    public static clsBeTrans_re_enc? GetSingle(int pIdRecepcionEnc,
                                                SqlConnection pConnection,
                                                SqlTransaction pTransaction)
    {
        try
        {
            string vSQL = @"SELECT b.Descripcion AS UbicacionRecepcion, tr.Descripcion, enc.* 
                       FROM Trans_re_enc AS enc 
                       INNER JOIN trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion 
                       INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion 
                       WHERE IdRecepcionEnc = @IdRecepcionEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT?.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTrans_re_enc Obj = new clsBeTrans_re_enc();

                    Cargar(ref Obj, lRow);

                    if (lRow["IdPropietarioBodega"] != DBNull.Value)
                    {
                        Obj.PropietarioBodega.IdPropietarioBodega = Convert.ToInt32(lRow["IdPropietarioBodega"]);
                        clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, pConnection, pTransaction);
                    }

                    Obj.UbicacionRecepcion = lRow["UbicacionRecepcion"] != DBNull.Value
                        ? Convert.ToString(lRow["UbicacionRecepcion"]) ?? string.Empty
                        : string.Empty;

                    Obj.Descripcion = lRow["Descripcion"] != DBNull.Value
                        ? Convert.ToString(lRow["Descripcion"]) ?? string.Empty
                        : string.Empty;
                    Obj.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(Obj.IdRecepcionEnc, pConnection, pTransaction) ?? new clsBeTrans_re_oc();
                    Obj.IsNew = false;
                    Obj.Detalle = clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(Obj.IdRecepcionEnc, Obj.IdBodega, pConnection, pTransaction)
                        ?? new List<clsBeTrans_re_det>();                    
                    Obj.TareaHH = clsLnTarea_hh.GetSingle(1, Obj.IdRecepcionEnc, pTransaction, pConnection) ?? new clsBeTarea_hh();
                    Obj.DetalleImagenes = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                        ?? new List<clsBeTrans_re_img>();
                    Obj.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                        ?? new List<clsBeTrans_re_det_parametros>();
                    Obj.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                        ?? new List<clsBeTrans_re_fact>();                    

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

    public static bool Existe_Recepcion_No_Finalizada(int pIdOrdenCompraEnc,
                                                     SqlConnection lConnection,
                                                     SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT COUNT(1) FROM trans_re_oc AS reoc 
                       INNER JOIN trans_re_enc AS re ON reoc.IdRecepcionEnc = re.IdRecepcionEnc 
                       WHERE (UPPER(re.Estado) <> 'FINALIZADO' AND UPPER(re.Estado) <> 'CERRADO' AND UPPER(re.Estado) <> 'ANULADO') 
                       AND reoc.IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    return Convert.ToInt32(lReturnValue) > 0;
                }
            }

            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private static int Actualizar_Estado_Pedido_Ingreso(int pIdOrdenCompraEnc,
                                                        int pIdRecepcionEnc,
                                                        SqlConnection lConnection,
                                                        SqlTransaction lTransaction,
                                                        bool BackOrder)
    {
        int result = 0;

        try
        {
            clsBeTrans_re_oc? BeRecOrdenCompra = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                                                                  pIdRecepcionEnc,
                                                                                                                  lConnection,
                                                                                                                  lTransaction);

            if (BackOrder)
            {
                result = clsLnTrans_oc_enc.Actualizar_Estado_BackOrder(pIdOrdenCompraEnc,
                                                                       lConnection,
                                                                       lTransaction);
            }
            else
            {
                result = clsLnTrans_oc_enc.Actualizar_Estado_Cerrada(pIdOrdenCompraEnc,
                                                                    lConnection,
                                                                    lTransaction);
            }

            return result;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Generar_Tarea_Recepcion_By_OrdenCompraEnc(ref clsBeTrans_oc_enc BeOrdenCompraEnc,
                                                                ref string Resultado,
                                                                bool CrearRecepcionPorDefecto,
                                                                clsBeI_nav_config_enc BeMI3Config,
                                                                ref clsBeTrans_re_enc? OutBeRecepcionEnc,
                                                                SqlConnection lConnection,
                                                                SqlTransaction lTransaction)
    {
        int result = 0;
        OutBeRecepcionEnc = null;

        try
        {
            if (!clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction))
            {
                int IdBodegaDestino = BeOrdenCompraEnc.IdBodega;

                clsBeTrans_re_enc BeRecepcionEnc = new clsBeTrans_re_enc();
                BeRecepcionEnc.IsNew = true;
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection,  lTransaction) + 1;
                BeRecepcionEnc.PropietarioBodega = new clsBePropietario_bodega();
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega;
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega;
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega;
                BeRecepcionEnc.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega;
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario.ToString();
                BeRecepcionEnc.Fec_agr = DateTime.Now;
                BeRecepcionEnc.Activo = true;
                BeRecepcionEnc.Estado = "Nuevo";

                BeRecepcionEnc.OrdenCompraRec = new clsBeTrans_re_oc();
                BeRecepcionEnc.OrdenCompraRec.IsNew = true;
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;

                if (BeRecepcionEnc.PropietarioBodega == null || BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 || BeRecepcionEnc.IdPropietarioBodega==0)
                {
                    throw new Exception("Propietario no válido al crear la recepción");
                }

                
                BeRecepcionEnc.IdTipoTransaccion = "HCOC00";

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (BeRecepcionEnc.IdMuelle == 0)
                {
                    throw new Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " + IdBodegaDestino);
                }

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (BeRecepcionEnc.IdUbicacionRecepcion == 0)
                {
                    throw new Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " + IdBodegaDestino);
                }

                double TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction);

                BeRecepcionEnc.Fecha_recepcion = DateTime.Now.Date;
                BeRecepcionEnc.Hora_ini_pc = DateTime.Now;
                BeRecepcionEnc.Hora_fin_pc = DateTime.Now.AddMinutes(TiempoMedioIngresoMinutos);
                BeRecepcionEnc.Muestra_precio = false;
                BeRecepcionEnc.Fec_mod = DateTime.Now;
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario.ToString();
                BeRecepcionEnc.Fecha_tarea = DateTime.Now;
                BeRecepcionEnc.Tomar_fotos = false;
                BeRecepcionEnc.Escanear_rec_ubic = false;
                BeRecepcionEnc.Para_por_codigo = false;
                BeRecepcionEnc.Observacion = "FROMMI3";
                BeRecepcionEnc.IdPiloto = 0;
                BeRecepcionEnc.IdVehiculo = 0;
                BeRecepcionEnc.Habilitar_Stock = true;
                BeRecepcionEnc.IdBodega = IdBodegaDestino;

                BeOrdenCompraEnc.TipoIngreso = clsLnTrans_oc_ti.GetSingle(BeOrdenCompraEnc.IdTipoIngresoOC, lConnection, lTransaction);
                if (BeOrdenCompraEnc.TipoIngreso != null)
                {
                    if (BeOrdenCompraEnc.TipoIngreso.IdProductoEstado != 0)
                    {
                        BeRecepcionEnc.IdEstado_Defecto_Recepcion = BeOrdenCompraEnc.TipoIngreso.IdProductoEstado;
                    }
                }
                else
                {
                    clsBeTrans_oc_ti? BeTipoDocumentoIngreso = clsLnTrans_oc_ti.GetSingle(BeOrdenCompraEnc.IdTipoIngresoOC, lConnection, lTransaction);
                    if (BeTipoDocumentoIngreso != null && BeTipoDocumentoIngreso.IdProductoEstado != 0)
                    {
                        BeRecepcionEnc.IdEstado_Defecto_Recepcion = BeTipoDocumentoIngreso.IdProductoEstado;
                    }
                }

                clsBeTrans_re_oc OrdenCompraReOc = new clsBeTrans_re_oc();
                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc;
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc;
                OrdenCompraReOc.IsNew = true;
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia;
                OrdenCompraReOc.OC = BeOrdenCompraEnc;
                OrdenCompraReOc.Recepcion_ciega = false;
                OrdenCompraReOc.Recepcion_manual = false;
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario.ToString();

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc;

                List<clsBeTrans_re_op> pListOperadorRecepcion = new List<clsBeTrans_re_op>();
                List<clsBeOperador_bodega> pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction);

                if (pListOperadorBodega != null)
                {
                    foreach (var Op in pListOperadorBodega)
                    {
                        clsBeTrans_re_op BeTransReOp = new clsBeTrans_re_op();
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega;
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario.ToString();
                        BeTransReOp.Fec_agr = DateTime.Now;
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario.ToString();
                        BeTransReOp.Fec_mod = DateTime.Now;
                        BeTransReOp.IsNew = true;
                        BeTransReOp.UsaHH = true;
                        pListOperadorRecepcion.Add(BeTransReOp);
                    }
                }

                clsBeTarea_hh BeTareaHH = new clsBeTarea_hh();
                BeTareaHH.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodegaDestino, BeOrdenCompraEnc.IdPropietarioBodega, lConnection, lTransaction);
                BeTareaHH.IdBodega = IdBodegaDestino;
                BeTareaHH.IdMuelle = BeRecepcionEnc.IdMuelle;
                BeTareaHH.IdEstado = 1;
                BeTareaHH.IdPrioridad = 1;
                BeTareaHH.IdTipoTarea = 1;
                BeTareaHH.IdTransaccion = BeRecepcionEnc.IdRecepcionEnc;
                BeTareaHH.Tipo = 0;
                BeTareaHH.FechaInicio = DateTime.Now;
                BeTareaHH.FechaFin = DateTime.Now.AddHours(2);
                BeTareaHH.DiaCompleto = false;
                BeTareaHH.Descripcion = "";
                BeTareaHH.CreaTarea = true;
                BeTareaHH.IsNew = true;

                switch (BeRecepcionEnc.IdTipoTransaccion)
                {
                    case "HSOC00":
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra ";
                        break;
                    case "HSOD00":
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia";
                        break;
                    case "HCOC00":
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra";
                        break;
                    case "HCOD00":
                        BeTareaHH.Asunto = "Devolución de Pedido";
                        break;
                    case "HHSR00":
                        BeTareaHH.Asunto = "Ingreso sin referencia";
                        break;
                    case "PICH000":
                        BeTareaHH.Asunto = "Pre-ingreso con HH";
                        break;
                }

                Guardar(BeOrdenCompraEnc.IdBodega,
                        BeTareaHH,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        null,//aqui recepcion_det
                        null,
                        pListOperadorRecepcion,
                        null,
                        null,
                        null,
                        null,
                        null,
                        lConnection,
                        lTransaction);

                result = 1;
                OutBeRecepcionEnc = BeRecepcionEnc;
            }

            return result;
        }
        catch (Exception)
        {            
            throw;
        }
    }
    private static int Actualizar_Hora_Fin_Recepcion(int pIdOrdenCompraEnc,
                                                     int pIdRecepcionEnc,
                                                     SqlConnection lConnection,
                                                     SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            clsBeTrans_re_oc? BeTransReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                                                             pIdRecepcionEnc,
                                                                                                             lConnection,
                                                                                                             lTransaction);

            if (BeTransReOC != null)
            {
                BeTransReOC.Hora_fin_hh = DateTime.Now;
                result = clsLnTrans_re_oc.Actualizar_Hora_Fin_Recepcion(ref BeTransReOC, lConnection, lTransaction);
            }
        }
        catch (Exception)
        {            
            throw;
        }

        return result;
    }
}