using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_oc;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_oc_ti
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_oc_ti oBeTrans_oc_ti, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeTrans_oc_ti.IdTipoIngresoOC = GetInt("IdTipoIngresoOC");
            oBeTrans_oc_ti.Nombre = GetString("Nombre");
            oBeTrans_oc_ti.Es_devolucion = GetBool("es_devolucion");
            oBeTrans_oc_ti.User_agr = GetString("user_agr");
            oBeTrans_oc_ti.Fec_agr = GetDate("fec_agr");
            oBeTrans_oc_ti.User_mod = GetString("user_mod");
            oBeTrans_oc_ti.Fec_mod = GetDate("fec_mod");
            oBeTrans_oc_ti.Activo = GetBool("activo");
            oBeTrans_oc_ti.Control_poliza = GetBool("control_poliza");
            oBeTrans_oc_ti.Requerir_documento_ref = GetBool("requerir_documento_ref");
            oBeTrans_oc_ti.Es_poliza_consolidada = GetBool("es_poliza_consolidada");
            oBeTrans_oc_ti.Genera_tarea_ingreso = GetBool("genera_tarea_ingreso");
            oBeTrans_oc_ti.Requerir_proveedor_es_bodega_wms = GetBool("requerir_proveedor_es_bodega_wms");
            oBeTrans_oc_ti.Requerir_documento_ref_wms = GetBool("requerir_documento_ref_wms");
            oBeTrans_oc_ti.Requerir_ubic_rec_ingreso = GetBool("requerir_ubic_rec_ingreso");
            oBeTrans_oc_ti.Exigir_campo_referencia = GetBool("exigir_campo_referencia");
            oBeTrans_oc_ti.Marcar_registros_enviados_mi3 = GetBool("marcar_registros_enviados_mi3");
            oBeTrans_oc_ti.Preguntar_en_backorder = GetBool("preguntar_en_backorder");
            oBeTrans_oc_ti.Bloquear_lotes = GetBool("bloquear_lotes");
            oBeTrans_oc_ti.Permitir_excedente_lotes = GetBool("permitir_excedente_lotes");
            oBeTrans_oc_ti.Permitir_vencido_ingreso = GetBool("permitir_vencido_ingreso");
            oBeTrans_oc_ti.Es_importacion = GetBool("es_importacion");
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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_ti oBeTrans_oc_ti, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_ti");
            Ins.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("es_devolucion", "@es_devolucion", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("control_poliza", "@control_poliza", "F");
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Ins.Add("es_poliza_consolidada", "@es_poliza_consolidada", "F");
            Ins.Add("genera_tarea_ingreso", "@genera_tarea_ingreso", "F");
            Ins.Add("requerir_proveedor_es_bodega_wms", "@requerir_proveedor_es_bodega_wms", "F");
            Ins.Add("requerir_documento_ref_wms", "@requerir_documento_ref_wms", "F");
            Ins.Add("requerir_ubic_rec_ingreso", "@requerir_ubic_rec_ingreso", "F");
            Ins.Add("exigir_campo_referencia", "@exigir_campo_referencia", "F");
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Ins.Add("preguntar_en_backorder", "@preguntar_en_backorder", "F");
            Ins.Add("bloquear_lotes", "@bloquear_lotes", "F");
            Ins.Add("permitir_excedente_lotes", "@permitir_excedente_lotes", "F");
            Ins.Add("permitir_vencido_ingreso", "@permitir_vencido_ingreso", "F");
            Ins.Add("es_importacion", "@es_importacion", "F");

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

            BindParameters(cmd, oBeTrans_oc_ti);

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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_ti oBeTrans_oc_ti)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_ti");
            Ins.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("es_devolucion", "@es_devolucion", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("control_poliza", "@control_poliza", "F");
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Ins.Add("es_poliza_consolidada", "@es_poliza_consolidada", "F");
            Ins.Add("genera_tarea_ingreso", "@genera_tarea_ingreso", "F");
            Ins.Add("requerir_proveedor_es_bodega_wms", "@requerir_proveedor_es_bodega_wms", "F");
            Ins.Add("requerir_documento_ref_wms", "@requerir_documento_ref_wms", "F");
            Ins.Add("requerir_ubic_rec_ingreso", "@requerir_ubic_rec_ingreso", "F");
            Ins.Add("exigir_campo_referencia", "@exigir_campo_referencia", "F");
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Ins.Add("preguntar_en_backorder", "@preguntar_en_backorder", "F");
            Ins.Add("bloquear_lotes", "@bloquear_lotes", "F");
            Ins.Add("permitir_excedente_lotes", "@permitir_excedente_lotes", "F");
            Ins.Add("permitir_vencido_ingreso", "@permitir_vencido_ingreso", "F");
            Ins.Add("es_importacion", "@es_importacion", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindParameters(cmd, oBeTrans_oc_ti);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_oc_ti oBeTrans_oc_ti, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_oc_ti");
            Upd.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("es_devolucion", "@es_devolucion", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("control_poliza", "@control_poliza", "F");
            Upd.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Upd.Add("es_poliza_consolidada", "@es_poliza_consolidada", "F");
            Upd.Add("genera_tarea_ingreso", "@genera_tarea_ingreso", "F");
            Upd.Add("requerir_proveedor_es_bodega_wms", "@requerir_proveedor_es_bodega_wms", "F");
            Upd.Add("requerir_documento_ref_wms", "@requerir_documento_ref_wms", "F");
            Upd.Add("requerir_ubic_rec_ingreso", "@requerir_ubic_rec_ingreso", "F");
            Upd.Add("exigir_campo_referencia", "@exigir_campo_referencia", "F");
            Upd.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Upd.Add("preguntar_en_backorder", "@preguntar_en_backorder", "F");
            Upd.Add("bloquear_lotes", "@bloquear_lotes", "F");
            Upd.Add("permitir_excedente_lotes", "@permitir_excedente_lotes", "F");
            Upd.Add("permitir_vencido_ingreso", "@permitir_vencido_ingreso", "F");
            Upd.Add("es_importacion", "@es_importacion", "F");
            Upd.Where("IdTipoIngresoOC = @IdTipoIngresoOC");

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

            BindParameters(cmd, oBeTrans_oc_ti);

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

    public int Eliminar(IConfiguration config, clsBeTrans_oc_ti oBeTrans_oc_ti, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_oc_ti" +
             "  Where(IdTipoIngresoOC = @IdTipoIngresoOC)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTipoIngresoOC", oBeTrans_oc_ti.IdTipoIngresoOC));

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
            const string sp = "Select * FROM Trans_oc_ti";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_ti pBeTrans_oc_ti)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_oc_ti" +
            " Where(IdTipoIngresoOC = @IdTipoIngresoOC)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoIngresoOC", pBeTrans_oc_ti.IdTipoIngresoOC));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_oc_ti, r);
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

    public static List<clsBeTrans_oc_ti> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_ti> lreturnList = new List<clsBeTrans_oc_ti>();

        try
        {
            const string sp = "Select * FROM Trans_oc_ti";

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

                        clsBeTrans_oc_ti vBeTrans_oc_ti = new clsBeTrans_oc_ti();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_ti = new clsBeTrans_oc_ti();
                            Cargar(ref vBeTrans_oc_ti, dr);
                            lreturnList.Add(vBeTrans_oc_ti);
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

            const string sp = "Select ISNULL(Max(IdTipoIngresoOC),0) FROM Trans_oc_ti";

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


            const string sp = "Select ISNULL(Max(IdTipoIngresoOC),0) FROM Trans_oc_ti";

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
    public static void BindParameters(SqlCommand cmd, dynamic oBeTrans_oc_ti)
    {
        void AddParam(string name, object value) => cmd.Parameters.Add(new SqlParameter(name, value ?? DBNull.Value));
        object NullIfZero(object value) => (value is int intValue && intValue == 0) ? DBNull.Value : value;

        AddParam("@IdTipoIngresoOC", NullIfZero(oBeTrans_oc_ti.IdTipoIngresoOC));
        AddParam("@Nombre", oBeTrans_oc_ti.Nombre);
        AddParam("@es_devolucion", oBeTrans_oc_ti.Es_devolucion);
        AddParam("@user_agr", oBeTrans_oc_ti.User_agr);
        AddParam("@fec_agr", oBeTrans_oc_ti.Fec_agr);
        AddParam("@user_mod", oBeTrans_oc_ti.User_mod);
        AddParam("@fec_mod", oBeTrans_oc_ti.Fec_mod);
        AddParam("@activo", oBeTrans_oc_ti.Activo);
        AddParam("@control_poliza", oBeTrans_oc_ti.Control_poliza);
        AddParam("@requerir_documento_ref", oBeTrans_oc_ti.Requerir_documento_ref);
        AddParam("@es_poliza_consolidada", oBeTrans_oc_ti.Es_poliza_consolidada);
        AddParam("@genera_tarea_ingreso", oBeTrans_oc_ti.Genera_tarea_ingreso);
        AddParam("@requerir_proveedor_es_bodega_wms", oBeTrans_oc_ti.Requerir_proveedor_es_bodega_wms);
        AddParam("@requerir_documento_ref_wms", oBeTrans_oc_ti.Requerir_documento_ref_wms);
        AddParam("@requerir_ubic_rec_ingreso", oBeTrans_oc_ti.Requerir_ubic_rec_ingreso);
        AddParam("@exigir_campo_referencia", oBeTrans_oc_ti.Exigir_campo_referencia);
        AddParam("@marcar_registros_enviados_mi3", oBeTrans_oc_ti.Marcar_registros_enviados_mi3);
        AddParam("@preguntar_en_backorder", oBeTrans_oc_ti.Preguntar_en_backorder);
        AddParam("@bloquear_lotes", oBeTrans_oc_ti.Bloquear_lotes);
        AddParam("@permitir_excedente_lotes", oBeTrans_oc_ti.Permitir_excedente_lotes);
        AddParam("@permitir_vencido_ingreso", oBeTrans_oc_ti.Permitir_vencido_ingreso);
        AddParam("@es_importacion", oBeTrans_oc_ti.Es_importacion);
    }

}
