using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;

public class clsLnTrans_oc_det_lote
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_oc_det_lote oBeTrans_oc_det_lote, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        Double GetDouble(string col) { return dr[col] is DBNull ? 0 : (Convert.ToDouble(dr[col])); }        

        try
        {
            oBeTrans_oc_det_lote.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_oc_det_lote.IdOrdenCompraDet = GetInt("IdOrdenCompraDet");
            oBeTrans_oc_det_lote.IdOrdenCompraDetLote = GetInt("IdOrdenCompraDetLote");
            oBeTrans_oc_det_lote.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_oc_det_lote.No_linea = GetInt("no_linea");
            oBeTrans_oc_det_lote.Codigo_producto = GetString("codigo_producto");
            oBeTrans_oc_det_lote.Cantidad = GetDouble("cantidad");
            oBeTrans_oc_det_lote.Cantidad_recibida = GetDouble("cantidad_recibida");
            oBeTrans_oc_det_lote.Lote = GetString("lote");
            oBeTrans_oc_det_lote.Fecha_vence = GetDate("fecha_vence");
            oBeTrans_oc_det_lote.Lic_plate = GetString("lic_plate");
            oBeTrans_oc_det_lote.Ubicacion = GetString("Ubicacion");
            oBeTrans_oc_det_lote.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_oc_det_lote.IdUnidadMedidaBasica = GetInt("IdUnidadMedidaBasica");
            oBeTrans_oc_det_lote.User_agr = GetString("user_agr");
            oBeTrans_oc_det_lote.Fec_agr = GetDate("fec_agr");
            oBeTrans_oc_det_lote.User_mod = GetString("user_mod");
            oBeTrans_oc_det_lote.Fec_mod = GetDate("fec_mod");
            oBeTrans_oc_det_lote.Reclasificar = GetBool("reclasificar");
            oBeTrans_oc_det_lote.Activo = GetBool("activo");
            oBeTrans_oc_det_lote.No_documento = GetString("no_documento");
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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_det_lote oBeTrans_oc_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_det_lote");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idordencompradet", "@idordencompradet", "F");
            Ins.Add("idordencompradetlote", "@idordencompradetlote", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("ubicacion", "@ubicacion", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("reclasificar", "@reclasificar", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("no_documento", "@no_documento", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_oc_det_lote.IdOrdenCompraEnc));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", oBeTrans_oc_det_lote.IdOrdenCompraDet));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDetLote", oBeTrans_oc_det_lote.IdOrdenCompraDetLote));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_oc_det_lote.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@no_linea", oBeTrans_oc_det_lote.No_linea));
            cmd.Parameters.Add(new SqlParameter("@codigo_producto", oBeTrans_oc_det_lote.Codigo_producto));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeTrans_oc_det_lote.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", oBeTrans_oc_det_lote.Cantidad_recibida));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeTrans_oc_det_lote.Lote));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeTrans_oc_det_lote.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeTrans_oc_det_lote.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@Ubicacion", oBeTrans_oc_det_lote.Ubicacion));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_oc_det_lote.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_oc_det_lote.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_oc_det_lote.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_oc_det_lote.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_oc_det_lote.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_oc_det_lote.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@reclasificar", oBeTrans_oc_det_lote.Reclasificar));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_oc_det_lote.Activo));
            cmd.Parameters.Add(new SqlParameter("@no_documento", oBeTrans_oc_det_lote.No_documento));

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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_det_lote oBeTrans_oc_det_lote)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_det_lote");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idordencompradet", "@idordencompradet", "F");
            Ins.Add("idordencompradetlote", "@idordencompradetlote", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("ubicacion", "@ubicacion", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("reclasificar", "@reclasificar", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("no_documento", "@no_documento", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_oc_det_lote.IdOrdenCompraEnc));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", oBeTrans_oc_det_lote.IdOrdenCompraDet));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDetLote", oBeTrans_oc_det_lote.IdOrdenCompraDetLote));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_oc_det_lote.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@no_linea", oBeTrans_oc_det_lote.No_linea));
            cmd.Parameters.Add(new SqlParameter("@codigo_producto", oBeTrans_oc_det_lote.Codigo_producto));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeTrans_oc_det_lote.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", oBeTrans_oc_det_lote.Cantidad_recibida));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeTrans_oc_det_lote.Lote));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeTrans_oc_det_lote.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeTrans_oc_det_lote.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@Ubicacion", oBeTrans_oc_det_lote.Ubicacion));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_oc_det_lote.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_oc_det_lote.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_oc_det_lote.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_oc_det_lote.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_oc_det_lote.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_oc_det_lote.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@reclasificar", oBeTrans_oc_det_lote.Reclasificar));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_oc_det_lote.Activo));
            cmd.Parameters.Add(new SqlParameter("@no_documento", oBeTrans_oc_det_lote.No_documento));

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

    public static int Actualizar(IConfiguration config, clsBeTrans_oc_det_lote oBeTrans_oc_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_oc_det_lote");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("idordencompradet", "@idordencompradet", "F");
            Upd.Add("idordencompradetlote", "@idordencompradetlote", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("codigo_producto", "@codigo_producto", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("ubicacion", "@ubicacion", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("reclasificar", "@reclasificar", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("no_documento", "@no_documento", "F");
            Upd.Where("IdOrdenCompraDetLote = @IdOrdenCompraDetLote");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_oc_det_lote.IdOrdenCompraEnc));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", oBeTrans_oc_det_lote.IdOrdenCompraDet));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDetLote", oBeTrans_oc_det_lote.IdOrdenCompraDetLote));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_oc_det_lote.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@no_linea", oBeTrans_oc_det_lote.No_linea));
            cmd.Parameters.Add(new SqlParameter("@codigo_producto", oBeTrans_oc_det_lote.Codigo_producto));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeTrans_oc_det_lote.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", oBeTrans_oc_det_lote.Cantidad_recibida));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeTrans_oc_det_lote.Lote));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeTrans_oc_det_lote.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeTrans_oc_det_lote.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@Ubicacion", oBeTrans_oc_det_lote.Ubicacion));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_oc_det_lote.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_oc_det_lote.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_oc_det_lote.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_oc_det_lote.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_oc_det_lote.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_oc_det_lote.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@reclasificar", oBeTrans_oc_det_lote.Reclasificar));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_oc_det_lote.Activo));
            cmd.Parameters.Add(new SqlParameter("@no_documento", oBeTrans_oc_det_lote.No_documento));

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

    public int Eliminar(IConfiguration config, clsBeTrans_oc_det_lote oBeTrans_oc_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_oc_det_lote" +
             "  Where(IdOrdenCompraDetLote = @IdOrdenCompraDetLote)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDetLote", oBeTrans_oc_det_lote.IdOrdenCompraDetLote));

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
            const string sp = "Select * FROM Trans_oc_det_lote";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_det_lote pBeTrans_oc_det_lote)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_oc_det_lote" +
            " Where(IdOrdenCompraDetLote = @IdOrdenCompraDetLote)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", pBeTrans_oc_det_lote.IdOrdenCompraEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", pBeTrans_oc_det_lote.IdOrdenCompraDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraDetLote", pBeTrans_oc_det_lote.IdOrdenCompraDetLote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoBodega", pBeTrans_oc_det_lote.IdProductoBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@no_linea", pBeTrans_oc_det_lote.No_linea));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_producto", pBeTrans_oc_det_lote.Codigo_producto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@cantidad", pBeTrans_oc_det_lote.Cantidad));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@cantidad_recibida", pBeTrans_oc_det_lote.Cantidad_recibida));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lote", pBeTrans_oc_det_lote.Lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_vence", pBeTrans_oc_det_lote.Fecha_vence));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lic_plate", pBeTrans_oc_det_lote.Lic_plate));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Ubicacion", pBeTrans_oc_det_lote.Ubicacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPresentacion", pBeTrans_oc_det_lote.IdPresentacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", pBeTrans_oc_det_lote.IdUnidadMedidaBasica));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeTrans_oc_det_lote.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeTrans_oc_det_lote.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeTrans_oc_det_lote.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeTrans_oc_det_lote.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@reclasificar", pBeTrans_oc_det_lote.Reclasificar));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeTrans_oc_det_lote.Activo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@no_documento", pBeTrans_oc_det_lote.No_documento));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_oc_det_lote, r);
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

    public static List<clsBeTrans_oc_det_lote> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_det_lote> lreturnList = new List<clsBeTrans_oc_det_lote>();

        try
        {
            const string sp = "Select * FROM Trans_oc_det_lote";

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

                        clsBeTrans_oc_det_lote vBeTrans_oc_det_lote = new clsBeTrans_oc_det_lote();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_det_lote = new clsBeTrans_oc_det_lote();
                            Cargar(ref vBeTrans_oc_det_lote, dr);
                            lreturnList.Add(vBeTrans_oc_det_lote);
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

            const string sp = "Select ISNULL(Max(IdOrdenCompraDetLote),0) FROM Trans_oc_det_lote";

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


            const string sp = "Select ISNULL(Max(IdOrdenCompraDetLote),0) FROM Trans_oc_det_lote";

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

    internal static List<clsBeTrans_oc_det_lote> Get_By_IdOrdenCompraEnc(int IdOrdenCompraEnc,
                                                                         SqlConnection lConnection,
                                                                         SqlTransaction lTransaction)
    {
        try
        {
            List<clsBeTrans_oc_det_lote> lReturnList = new List<clsBeTrans_oc_det_lote>();
            const string sp = @"SELECT * FROM Trans_oc_det_lote 
                                WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc AND Activo = 1";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc);
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                clsBeTrans_oc_det_lote vBeTrans_oc_det_lote = new clsBeTrans_oc_det_lote();
                Cargar(ref vBeTrans_oc_det_lote, dr);

                if (vBeTrans_oc_det_lote.Presentacion != null && vBeTrans_oc_det_lote.Presentacion.IdPresentacion != 0)
                {
                    var tempPresentacion = new clsBeProducto_presentacion();
                    if (lConnection != null && lTransaction != null)
                        clsLnProducto_presentacion.GetSingle(ref tempPresentacion,
                                                         lConnection,
                                                         lTransaction);
                    vBeTrans_oc_det_lote.Presentacion = tempPresentacion;
                }

                if (vBeTrans_oc_det_lote.UnidadMedida != null && lConnection != null && lTransaction != null)
                {
                    var unidadMedida = clsLnUnidad_medida.GetSingle(vBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida,
                                                                    lConnection,
                                                                    lTransaction);
                    if (unidadMedida != null)
                    {
                        vBeTrans_oc_det_lote.UnidadMedida = unidadMedida;
                    }
                }

                lReturnList.Add(vBeTrans_oc_det_lote);
            }

            return lReturnList;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static string Get_Ubicacion_By_BeTransReDet(clsBeTrans_re_det BeTransReDet,
                                                       SqlConnection pConnection,
                                                       SqlTransaction pTransaction)
    {
        string ubicacion = "";

        string vSQL = @"SELECT ubicacion
                       FROM trans_oc_det_lote
                       WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc  
                             And IdProductoBodega=@IdProductoBodega 
                             And no_linea = @NoLinea  
                             And lic_plate = @Lic_Plate  
                             And lote = @Lote  
                             And fecha_vence =@Fecha_Vence";

        using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
        {
            lDTA.SelectCommand.CommandType = CommandType.Text;
            lDTA.SelectCommand.Transaction = pTransaction;
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", BeTransReDet.IdOrdenCompraEnc);
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", BeTransReDet.IdProductoBodega);
            lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", BeTransReDet.No_Linea);
            lDTA.SelectCommand.Parameters.AddWithValue("@Lote", BeTransReDet.Lote);
            lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", BeTransReDet.Lic_plate);
            lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", BeTransReDet.Fecha_vence);

            DataTable lDataTable = new DataTable();
            lDTA.Fill(lDataTable);

            if (lDataTable != null && lDataTable.Rows.Count > 0)
            {
                ubicacion = lDataTable.Rows[0]["ubicacion"]?.ToString() ?? string.Empty;
            }
        }

        return ubicacion;
    }
}
