using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Pedido;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_pe_tipo
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();
    public static void Cargar(ref clsBeTrans_pe_tipo oBeTrans_pe_tipo, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }

        try
        {
            oBeTrans_pe_tipo.IdTipoPedido = GetInt("IdTipoPedido");
            oBeTrans_pe_tipo.Nombre = GetString("Nombre");
            oBeTrans_pe_tipo.Descripcion = GetString("Descripcion");
            oBeTrans_pe_tipo.Preparar = GetBool("Preparar");
            oBeTrans_pe_tipo.Verificar = GetBool("Verificar");
            oBeTrans_pe_tipo.ReservaStock = GetBool("ReservaStock");
            oBeTrans_pe_tipo.ImprimeBarrasPicking = GetBool("ImprimeBarrasPicking");
            oBeTrans_pe_tipo.ImprimeBarrasPacking = GetBool("ImprimeBarrasPacking");
            oBeTrans_pe_tipo.Control_poliza = GetBool("control_poliza");
            oBeTrans_pe_tipo.Generar_pedido_ingreso_bodega_destino = GetBool("Generar_pedido_ingreso_bodega_destino");
            oBeTrans_pe_tipo.IdTipoIngresoOC = GetInt("IdTipoIngresoOC");
            oBeTrans_pe_tipo.Activo = GetBool("activo");
            oBeTrans_pe_tipo.Requerir_documento_ref = GetBool("requerir_documento_ref");
            oBeTrans_pe_tipo.Trasladar_lotes_doc_ingreso = GetBool("trasladar_lotes_doc_ingreso");
            oBeTrans_pe_tipo.Requerir_cliente_es_bodega_wms = GetBool("requerir_cliente_es_bodega_wms");
            oBeTrans_pe_tipo.Marcar_registros_enviados_mi3 = GetBool("marcar_registros_enviados_mi3");
            oBeTrans_pe_tipo.Generar_recepcion_auto_bodega_destino = GetBool("generar_recepcion_auto_bodega_destino");
            oBeTrans_pe_tipo.Recibir_producto_auto_bodega_destino = GetBool("recibir_producto_auto_bodega_destino");
            oBeTrans_pe_tipo.Control_Cliente_En_Detalle = GetBool("Control_Cliente_En_Detalle");
            oBeTrans_pe_tipo.Permitir_despacho_parcial = GetBool("permitir_despacho_parcial");
            oBeTrans_pe_tipo.Permitir_despacho_multiple = GetBool("permitir_despacho_multiple");
            oBeTrans_pe_tipo.Fotografia_verificacion = GetBool("fotografia_verificacion");
            oBeTrans_pe_tipo.Es_devolucion = GetBool("es_devolucion");
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
    public static int Insertar(IConfiguration config, clsBeTrans_pe_tipo oBeTrans_pe_tipo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_tipo");
            Ins.Add("idtipopedido", "@idtipopedido", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("preparar", "@preparar", "F");
            Ins.Add("verificar", "@verificar", "F");
            Ins.Add("reservastock", "@reservastock", "F");
            Ins.Add("imprimebarraspicking", "@imprimebarraspicking", "F");
            Ins.Add("imprimebarraspacking", "@imprimebarraspacking", "F");
            Ins.Add("control_poliza", "@control_poliza", "F");
            Ins.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Ins.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Ins.Add("trasladar_lotes_doc_ingreso", "@trasladar_lotes_doc_ingreso", "F");
            Ins.Add("requerir_cliente_es_bodega_wms", "@requerir_cliente_es_bodega_wms", "F");
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Ins.Add("recibir_producto_auto_bodega_destino", "@recibir_producto_auto_bodega_destino", "F");
            Ins.Add("control_cliente_en_detalle", "@control_cliente_en_detalle", "F");
            Ins.Add("permitir_despacho_parcial", "@permitir_despacho_parcial", "F");
            Ins.Add("permitir_despacho_multiple", "@permitir_despacho_multiple", "F");
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Ins.Add("es_devolucion", "@es_devolucion", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdTipoPedido", oBeTrans_pe_tipo.IdTipoPedido));
            cmd.Parameters.Add(new SqlParameter("@Nombre", oBeTrans_pe_tipo.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTrans_pe_tipo.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@Preparar", oBeTrans_pe_tipo.Preparar));
            cmd.Parameters.Add(new SqlParameter("@Verificar", oBeTrans_pe_tipo.Verificar));
            cmd.Parameters.Add(new SqlParameter("@ReservaStock", oBeTrans_pe_tipo.ReservaStock));
            cmd.Parameters.Add(new SqlParameter("@ImprimeBarrasPicking", oBeTrans_pe_tipo.ImprimeBarrasPicking));
            cmd.Parameters.Add(new SqlParameter("@ImprimeBarrasPacking", oBeTrans_pe_tipo.ImprimeBarrasPacking));
            cmd.Parameters.Add(new SqlParameter("@control_poliza", oBeTrans_pe_tipo.Control_poliza));
            cmd.Parameters.Add(new SqlParameter("@Generar_pedido_ingreso_bodega_destino", oBeTrans_pe_tipo.Generar_pedido_ingreso_bodega_destino));
            cmd.Parameters.Add(new SqlParameter("@IdTipoIngresoOC", oBeTrans_pe_tipo.IdTipoIngresoOC));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_pe_tipo.Activo));
            cmd.Parameters.Add(new SqlParameter("@requerir_documento_ref", oBeTrans_pe_tipo.Requerir_documento_ref));
            cmd.Parameters.Add(new SqlParameter("@trasladar_lotes_doc_ingreso", oBeTrans_pe_tipo.Trasladar_lotes_doc_ingreso));
            cmd.Parameters.Add(new SqlParameter("@requerir_cliente_es_bodega_wms", oBeTrans_pe_tipo.Requerir_cliente_es_bodega_wms));
            cmd.Parameters.Add(new SqlParameter("@marcar_registros_enviados_mi3", oBeTrans_pe_tipo.Marcar_registros_enviados_mi3));
            cmd.Parameters.Add(new SqlParameter("@generar_recepcion_auto_bodega_destino", oBeTrans_pe_tipo.Generar_recepcion_auto_bodega_destino));
            cmd.Parameters.Add(new SqlParameter("@recibir_producto_auto_bodega_destino", oBeTrans_pe_tipo.Recibir_producto_auto_bodega_destino));
            cmd.Parameters.Add(new SqlParameter("@Control_Cliente_En_Detalle", oBeTrans_pe_tipo.Control_Cliente_En_Detalle));
            cmd.Parameters.Add(new SqlParameter("@permitir_despacho_parcial", oBeTrans_pe_tipo.Permitir_despacho_parcial));
            cmd.Parameters.Add(new SqlParameter("@permitir_despacho_multiple", oBeTrans_pe_tipo.Permitir_despacho_multiple));
            cmd.Parameters.Add(new SqlParameter("@fotografia_verificacion", oBeTrans_pe_tipo.Fotografia_verificacion));
            cmd.Parameters.Add(new SqlParameter("@es_devolucion", oBeTrans_pe_tipo.Es_devolucion));

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
    public static int Insertar(IConfiguration config, clsBeTrans_pe_tipo oBeTrans_pe_tipo)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_tipo");
            Ins.Add("idtipopedido", "@idtipopedido", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("preparar", "@preparar", "F");
            Ins.Add("verificar", "@verificar", "F");
            Ins.Add("reservastock", "@reservastock", "F");
            Ins.Add("imprimebarraspicking", "@imprimebarraspicking", "F");
            Ins.Add("imprimebarraspacking", "@imprimebarraspacking", "F");
            Ins.Add("control_poliza", "@control_poliza", "F");
            Ins.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Ins.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Ins.Add("trasladar_lotes_doc_ingreso", "@trasladar_lotes_doc_ingreso", "F");
            Ins.Add("requerir_cliente_es_bodega_wms", "@requerir_cliente_es_bodega_wms", "F");
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Ins.Add("recibir_producto_auto_bodega_destino", "@recibir_producto_auto_bodega_destino", "F");
            Ins.Add("control_cliente_en_detalle", "@control_cliente_en_detalle", "F");
            Ins.Add("permitir_despacho_parcial", "@permitir_despacho_parcial", "F");
            Ins.Add("permitir_despacho_multiple", "@permitir_despacho_multiple", "F");
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Ins.Add("es_devolucion", "@es_devolucion", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_pe_tipo);

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
    public static int Actualizar(IConfiguration config, clsBeTrans_pe_tipo oBeTrans_pe_tipo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_pe_tipo");
            Upd.Add("idtipopedido", "@idtipopedido", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("preparar", "@preparar", "F");
            Upd.Add("verificar", "@verificar", "F");
            Upd.Add("reservastock", "@reservastock", "F");
            Upd.Add("imprimebarraspicking", "@imprimebarraspicking", "F");
            Upd.Add("imprimebarraspacking", "@imprimebarraspacking", "F");
            Upd.Add("control_poliza", "@control_poliza", "F");
            Upd.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Upd.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("requerir_documento_ref", "@requerir_documento_ref", "F");
            Upd.Add("trasladar_lotes_doc_ingreso", "@trasladar_lotes_doc_ingreso", "F");
            Upd.Add("requerir_cliente_es_bodega_wms", "@requerir_cliente_es_bodega_wms", "F");
            Upd.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", "F");
            Upd.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Upd.Add("recibir_producto_auto_bodega_destino", "@recibir_producto_auto_bodega_destino", "F");
            Upd.Add("control_cliente_en_detalle", "@control_cliente_en_detalle", "F");
            Upd.Add("permitir_despacho_parcial", "@permitir_despacho_parcial", "F");
            Upd.Add("permitir_despacho_multiple", "@permitir_despacho_multiple", "F");
            Upd.Add("fotografia_verificacion", "@fotografia_verificacion", "F");
            Upd.Add("es_devolucion", "@es_devolucion", "F");
            Upd.Where("IdTipoPedido = @IdTipoPedido");

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

            Bind(cmd, oBeTrans_pe_tipo);

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
    public int Eliminar(IConfiguration config, clsBeTrans_pe_tipo oBeTrans_pe_tipo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_pe_tipo" +
             "  Where(IdTipoPedido = @IdTipoPedido)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTipoPedido", oBeTrans_pe_tipo.IdTipoPedido));

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
            const string sp = "Select * FROM Trans_pe_tipo";
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
    public static bool GetSingle(IConfiguration config, ref clsBeTrans_pe_tipo pBeTrans_pe_tipo)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_pe_tipo" +
            " Where(IdTipoPedido = @IdTipoPedido)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoPedido", pBeTrans_pe_tipo.IdTipoPedido));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_pe_tipo, r);
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
    public static List<clsBeTrans_pe_tipo> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_pe_tipo> lreturnList = new List<clsBeTrans_pe_tipo>();

        try
        {
            const string sp = "Select * FROM Trans_pe_tipo";

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

                        clsBeTrans_pe_tipo vBeTrans_pe_tipo = new clsBeTrans_pe_tipo();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_pe_tipo = new clsBeTrans_pe_tipo();
                            Cargar(ref vBeTrans_pe_tipo, dr);
                            lreturnList.Add(vBeTrans_pe_tipo);
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

            const string sp = "Select ISNULL(Max(IdTipoPedido),0) FROM Trans_pe_tipo";

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


            const string sp = "Select ISNULL(Max(IdTipoPedido),0) FROM Trans_pe_tipo";

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
    public static void Bind(SqlCommand cmd, clsBeTrans_pe_tipo o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdTipoPedido", o.IdTipoPedido != 0 ? o.IdTipoPedido : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Nombre", !string.IsNullOrWhiteSpace(o.Nombre) ? o.Nombre : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Descripcion", !string.IsNullOrWhiteSpace(o.Descripcion) ? o.Descripcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Preparar", o.Preparar));
        cmd.Parameters.Add(new SqlParameter("@Verificar", o.Verificar));
        cmd.Parameters.Add(new SqlParameter("@ReservaStock", o.ReservaStock));
        cmd.Parameters.Add(new SqlParameter("@ImprimeBarrasPicking", o.ImprimeBarrasPicking));
        cmd.Parameters.Add(new SqlParameter("@ImprimeBarrasPacking", o.ImprimeBarrasPacking));
        cmd.Parameters.Add(new SqlParameter("@control_poliza", o.Control_poliza));
        cmd.Parameters.Add(new SqlParameter("@Generar_pedido_ingreso_bodega_destino", o.Generar_pedido_ingreso_bodega_destino));
        cmd.Parameters.Add(new SqlParameter("@IdTipoIngresoOC", o.IdTipoIngresoOC != 0 ? o.IdTipoIngresoOC : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@requerir_documento_ref", o.Requerir_documento_ref));
        cmd.Parameters.Add(new SqlParameter("@trasladar_lotes_doc_ingreso", o.Trasladar_lotes_doc_ingreso));
        cmd.Parameters.Add(new SqlParameter("@requerir_cliente_es_bodega_wms", o.Requerir_cliente_es_bodega_wms));
        cmd.Parameters.Add(new SqlParameter("@marcar_registros_enviados_mi3", o.Marcar_registros_enviados_mi3));
        cmd.Parameters.Add(new SqlParameter("@generar_recepcion_auto_bodega_destino", o.Generar_recepcion_auto_bodega_destino));
        cmd.Parameters.Add(new SqlParameter("@recibir_producto_auto_bodega_destino", o.Recibir_producto_auto_bodega_destino));
        cmd.Parameters.Add(new SqlParameter("@Control_Cliente_En_Detalle", o.Control_Cliente_En_Detalle));
        cmd.Parameters.Add(new SqlParameter("@permitir_despacho_parcial", o.Permitir_despacho_parcial));
        cmd.Parameters.Add(new SqlParameter("@permitir_despacho_multiple", o.Permitir_despacho_multiple));
        cmd.Parameters.Add(new SqlParameter("@fotografia_verificacion", o.Fotografia_verificacion));
        cmd.Parameters.Add(new SqlParameter("@es_devolucion", o.Es_devolucion));
    }
    public static bool Existe(int idTipoPedido, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = "SELECT COUNT(1) FROM trans_pe_tipo WHERE IdTipoPedido = @IdTipoPedido";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdTipoPedido", idTipoPedido);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeTrans_pe_tipo entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            if (Existe(entity.IdTipoPedido, connection, isExternalTx ? tx! : localTx!))
                return Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
            else
                return Insertar(config, entity, connection, isExternalTx ? tx : localTx);
        }
        catch
        {
            if (!isExternalTx) localTx?.Rollback();
            throw;
        }
        finally
        {
            if (!isExternalTx)
            {
                localTx?.Commit();
                connection.Close();
            }
        }
    }
}