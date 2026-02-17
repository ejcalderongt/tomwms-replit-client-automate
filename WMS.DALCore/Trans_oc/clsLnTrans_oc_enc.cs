using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_oc;
using Microsoft.Extensions.Configuration;
using WMS.DALCore.Proveedor;
using WMS.DALCore.Trans_oc;
using WMS.EntityCore;
using WMS.EntityCore.Trans_re;
using WMS.EntityCore.Ticket;
using WMSWebAPI.Be;
using WMS.DALCore.Ticket;
public class clsLnTrans_oc_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_oc_enc oBeTrans_oc_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeTrans_oc_enc.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_oc_enc.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_oc_enc.IdProveedorBodega = GetInt("IdProveedorBodega");
            oBeTrans_oc_enc.IdTipoIngresoOC = GetInt("IdTipoIngresoOC");
            oBeTrans_oc_enc.IdEstadoOC = GetInt("IdEstadoOC");
            oBeTrans_oc_enc.IdMotivoDevolucion = GetInt("IdMotivoDevolucion");
            oBeTrans_oc_enc.Fecha_Creacion = GetDate("Fecha_Creacion");
            oBeTrans_oc_enc.Hora_Creacion = GetDate("Hora_Creacion");
            oBeTrans_oc_enc.No_Documento = GetString("No_Documento");
            oBeTrans_oc_enc.User_Agr = GetString("User_Agr");
            oBeTrans_oc_enc.Fec_Agr = GetDate("Fec_Agr");
            oBeTrans_oc_enc.User_Mod = GetString("User_Mod");
            oBeTrans_oc_enc.Fec_Mod = GetDate("Fec_Mod");
            oBeTrans_oc_enc.Procedencia = GetString("Procedencia");
            oBeTrans_oc_enc.No_Marchamo = GetString("No_Marchamo");
            oBeTrans_oc_enc.Referencia = GetString("Referencia");
            oBeTrans_oc_enc.Observacion = GetString("Observacion");
            oBeTrans_oc_enc.Control_Poliza = GetBool("Control_Poliza");
            oBeTrans_oc_enc.Activo = GetBool("Activo");
            oBeTrans_oc_enc.Fecha_Recepcion = GetDate("Fecha_Recepcion");
            oBeTrans_oc_enc.Hora_Inicio_Recepcion = GetDate("Hora_Inicio_Recepcion");
            oBeTrans_oc_enc.Hora_Fin_Recepcion = GetDate("Hora_Fin_Recepcion");
            oBeTrans_oc_enc.IdMuelleRecepcion = GetInt("IdMuelleRecepcion");
            oBeTrans_oc_enc.Programar_Recepcion = GetBool("Programar_Recepcion");
            oBeTrans_oc_enc.IdMotivoAnulacionBodega = GetInt("IdMotivoAnulacionBodega");
            oBeTrans_oc_enc.Enviado_A_ERP = GetBool("Enviado_A_ERP");
            oBeTrans_oc_enc.Serie = GetString("serie");
            oBeTrans_oc_enc.Correlativo = GetInt("correlativo");
            oBeTrans_oc_enc.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeTrans_oc_enc.No_ticket_tms = GetString("no_ticket_tms");
            oBeTrans_oc_enc.IdNoDocumentoRef = GetInt("IdNoDocumentoRef");
            oBeTrans_oc_enc.Idacuerdocomercial = GetInt("idacuerdocomercial");
            oBeTrans_oc_enc.IdOperadorBodegaDefecto = GetInt("IdOperadorBodegaDefecto");
            oBeTrans_oc_enc.IdBodega = GetInt("IdBodega");
            oBeTrans_oc_enc.No_documento_recepcion_erp = GetString("no_documento_recepcion_erp");
            oBeTrans_oc_enc.No_documento_devolucion = GetString("no_documento_devolucion");
            oBeTrans_oc_enc.IdPedidoEncDevolucion = GetInt("IdPedidoEncDevolucion");
            oBeTrans_oc_enc.Push_to_nav = GetBool("push_to_nav");
            oBeTrans_oc_enc.No_documento_ubicacion_erp = GetString("no_documento_ubicacion_erp");
            oBeTrans_oc_enc.PutAway_Registrado = GetBool("PutAway_Registrado");
            oBeTrans_oc_enc.Codigo_Empresa_ERP = GetString("Codigo_Empresa_ERP");
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

    public static int Insertar(clsBeTrans_oc_enc oBeTrans_oc_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTrans_oc_enc == null)
            throw new ArgumentNullException(nameof(oBeTrans_oc_enc));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_oc_enc");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproveedorbodega", "@idproveedorbodega", "F");
            Ins.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Ins.Add("idestadooc", "@idestadooc", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("fecha_creacion", "@fecha_creacion", "F");
            Ins.Add("hora_creacion", "@hora_creacion", "F");
            Ins.Add("no_documento", "@no_documento", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("procedencia", "@procedencia", "F");
            Ins.Add("no_marchamo", "@no_marchamo", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("control_poliza", "@control_poliza", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("fecha_recepcion", "@fecha_recepcion", "F");
            Ins.Add("hora_inicio_recepcion", "@hora_inicio_recepcion", "F");
            Ins.Add("hora_fin_recepcion", "@hora_fin_recepcion", "F");
            Ins.Add("idmuellerecepcion", "@idmuellerecepcion", "F");
            Ins.Add("programar_recepcion", "@programar_recepcion", "F");
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Ins.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Ins.Add("serie", "@serie", "F");
            Ins.Add("correlativo", "@correlativo", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("no_ticket_tms", "@no_ticket_tms", "F");
            Ins.Add("idnodocumentoref", "@idnodocumentoref", "F");
            Ins.Add("idacuerdocomercial", "@idacuerdocomercial", "F");
            Ins.Add("idoperadorbodegadefecto", "@idoperadorbodegadefecto", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("no_documento_recepcion_erp", "@no_documento_recepcion_erp", "F");
            Ins.Add("no_documento_devolucion", "@no_documento_devolucion", "F");
            Ins.Add("idpedidoencdevolucion", "@idpedidoencdevolucion", "F");
            Ins.Add("push_to_nav", "@push_to_nav", "F");
            Ins.Add("no_documento_ubicacion_erp", "@no_documento_ubicacion_erp", "F");
            Ins.Add("putaway_registrado", "@putaway_registrado", "F");
            Ins.Add("codigo_empresa_erp", "@codigo_empresa_erp", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindParameters(cmd, oBeTrans_oc_enc);

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

    public static int Actualizar(clsBeTrans_oc_enc oBeTrans_oc_enc,
                                SqlConnection pConnection,
                                SqlTransaction? pTransaction = null)
    {
        if (pConnection is null)
            throw new ArgumentNullException(nameof(pConnection));

        int rowsAffected = 0;

        SqlConnection cn = pConnection;
        SqlTransaction? tx = pTransaction;
        bool weOpenedConnection = false;
        bool createdLocalTx = false;

        try
        {
            Upd.Init("trans_oc_enc");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproveedorbodega", "@idproveedorbodega", "F");
            Upd.Add("idtipoingresooc", "@idtipoingresooc", "F");
            Upd.Add("idestadooc", "@idestadooc", "F");
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Upd.Add("fecha_creacion", "@fecha_creacion", "F");
            Upd.Add("hora_creacion", "@hora_creacion", "F");
            Upd.Add("no_documento", "@no_documento", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("procedencia", "@procedencia", "F");
            Upd.Add("no_marchamo", "@no_marchamo", "F");
            Upd.Add("referencia", "@referencia", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Add("control_poliza", "@control_poliza", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("fecha_recepcion", "@fecha_recepcion", "F");
            Upd.Add("hora_inicio_recepcion", "@hora_inicio_recepcion", "F");
            Upd.Add("hora_fin_recepcion", "@hora_fin_recepcion", "F");
            Upd.Add("idmuellerecepcion", "@idmuellerecepcion", "F");
            Upd.Add("programar_recepcion", "@programar_recepcion", "F");
            Upd.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Upd.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Upd.Add("serie", "@serie", "F");
            Upd.Add("correlativo", "@correlativo", "F");
            Upd.Add("iddespachoenc", "@iddespachoenc", "F");
            Upd.Add("no_ticket_tms", "@no_ticket_tms", "F");
            Upd.Add("idnodocumentoref", "@idnodocumentoref", "F");
            Upd.Add("idacuerdocomercial", "@idacuerdocomercial", "F");
            Upd.Add("idoperadorbodegadefecto", "@idoperadorbodegadefecto", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("no_documento_recepcion_erp", "@no_documento_recepcion_erp", "F");
            Upd.Add("no_documento_devolucion", "@no_documento_devolucion", "F");
            Upd.Add("idpedidoencdevolucion", "@idpedidoencdevolucion", "F");
            Upd.Add("push_to_nav", "@push_to_nav", "F");
            Upd.Add("no_documento_ubicacion_erp", "@no_documento_ubicacion_erp", "F");
            Upd.Add("putaway_registrado", "@putaway_registrado", "F");
            Upd.Add("codigo_empresa_erp", "@codigo_empresa_erp", "F");
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc");

            string sp = Upd.SQL();

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                weOpenedConnection = true;
            }

            if (tx == null)
            {
                tx = cn.BeginTransaction(IsolationLevel.ReadUncommitted);
                createdLocalTx = true;
            }

            using var cmd = new SqlCommand(sp, cn, tx);
            BindParameters(cmd, oBeTrans_oc_enc);

            rowsAffected = cmd.ExecuteNonQuery();

            if (createdLocalTx)
                tx.Commit();
        }
        catch (SqlException ex1)
        {
            if (createdLocalTx && tx != null)
                tx.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = $"{currentMethodName} {ex1.Message}";
            throw new Exception(vMsgError, ex1);
        }
        finally
        {
            if (weOpenedConnection && cn.State == ConnectionState.Open)
                cn.Close();

            // No se dispone pConnection/tx si son externos.
            if (createdLocalTx && tx != null)
                tx.Dispose();
        }

        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeTrans_oc_enc oBeTrans_oc_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_oc_enc" +
             "  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_oc_enc.IdOrdenCompraEnc));

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
            const string sp = "Select * FROM Trans_oc_enc";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_enc pBeTrans_oc_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_oc_enc" +
            " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", pBeTrans_oc_enc.IdOrdenCompraEnc));
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_oc_enc, r);
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

    public static List<clsBeTrans_oc_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_enc> lreturnList = new List<clsBeTrans_oc_enc>();

        try
        {
            const string sp = "Select * FROM Trans_oc_enc";

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

                        clsBeTrans_oc_enc vBeTrans_oc_enc = new clsBeTrans_oc_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_enc = new clsBeTrans_oc_enc();
                            Cargar(ref vBeTrans_oc_enc, dr);
                            lreturnList.Add(vBeTrans_oc_enc);
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

            const string sp = "Select ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_enc";

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
        int lMax = 0;

        try
        {
            const string sp = "Select ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_enc";

            var cmd = new SqlCommand(sp, pConection, pTransaction);
            cmd.CommandType = CommandType.Text;

            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = Convert.ToInt32(lreturnValue);
            }

            return lMax;
        }
        catch (SqlException ex1)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = $"{currentMethodName?.Name} {ex1.Message}";
            throw new Exception(vMsgError, ex1);
        }
    }
    public static void BindParameters(SqlCommand cmd, dynamic oBeTrans_oc_enc)
    {
        void AddParam(string name, object value) => cmd.Parameters.Add(new SqlParameter(name, value ?? DBNull.Value));
        object NullIfZero(object value) => (value is int intValue && intValue == 0) ? DBNull.Value : value;

        AddParam("@IdOrdenCompraEnc", NullIfZero(oBeTrans_oc_enc.IdOrdenCompraEnc));
        AddParam("@IdPropietarioBodega", NullIfZero(oBeTrans_oc_enc.IdPropietarioBodega));
        AddParam("@IdProveedorBodega", NullIfZero(oBeTrans_oc_enc.IdProveedorBodega));
        AddParam("@IdTipoIngresoOC", NullIfZero(oBeTrans_oc_enc.IdTipoIngresoOC));
        AddParam("@IdEstadoOC", NullIfZero(oBeTrans_oc_enc.IdEstadoOC));
        AddParam("@IdMotivoDevolucion", NullIfZero(oBeTrans_oc_enc.IdMotivoDevolucion));
        AddParam("@Fecha_Creacion", oBeTrans_oc_enc.Fecha_Creacion);
        AddParam("@Hora_Creacion", oBeTrans_oc_enc.Hora_Creacion);
        AddParam("@No_Documento", oBeTrans_oc_enc.No_Documento);
        AddParam("@User_Agr", oBeTrans_oc_enc.User_Agr);
        AddParam("@Fec_Agr", oBeTrans_oc_enc.Fec_Agr);
        AddParam("@User_Mod", oBeTrans_oc_enc.User_Mod);
        AddParam("@Fec_Mod", oBeTrans_oc_enc.Fec_Mod);
        AddParam("@Procedencia", oBeTrans_oc_enc.Procedencia);
        AddParam("@No_Marchamo", oBeTrans_oc_enc.No_Marchamo);
        AddParam("@Referencia", oBeTrans_oc_enc.Referencia);
        AddParam("@Observacion", oBeTrans_oc_enc.Observacion);
        AddParam("@Control_Poliza", oBeTrans_oc_enc.Control_Poliza);
        AddParam("@Activo", oBeTrans_oc_enc.Activo);
        AddParam("@Fecha_Recepcion", oBeTrans_oc_enc.Fecha_Recepcion);
        AddParam("@Hora_Inicio_Recepcion", oBeTrans_oc_enc.Hora_Inicio_Recepcion);
        AddParam("@Hora_Fin_Recepcion", oBeTrans_oc_enc.Hora_Fin_Recepcion);
        AddParam("@IdMuelleRecepcion", NullIfZero(oBeTrans_oc_enc.IdMuelleRecepcion));
        AddParam("@Programar_Recepcion", oBeTrans_oc_enc.Programar_Recepcion);
        AddParam("@IdMotivoAnulacionBodega", NullIfZero(oBeTrans_oc_enc.IdMotivoAnulacionBodega));
        AddParam("@Enviado_A_ERP", oBeTrans_oc_enc.Enviado_A_ERP);
        AddParam("@serie", oBeTrans_oc_enc.Serie);
        AddParam("@correlativo", oBeTrans_oc_enc.Correlativo);
        AddParam("@IdDespachoEnc", NullIfZero(oBeTrans_oc_enc.IdDespachoEnc));
        AddParam("@no_ticket_tms", oBeTrans_oc_enc.No_ticket_tms);
        AddParam("@IdNoDocumentoRef", NullIfZero(oBeTrans_oc_enc.IdNoDocumentoRef));
        AddParam("@idacuerdocomercial", NullIfZero(oBeTrans_oc_enc.Idacuerdocomercial));
        AddParam("@IdOperadorBodegaDefecto", NullIfZero(oBeTrans_oc_enc.IdOperadorBodegaDefecto));
        AddParam("@IdBodega", NullIfZero(oBeTrans_oc_enc.IdBodega));
        AddParam("@no_documento_recepcion_erp", oBeTrans_oc_enc.No_documento_recepcion_erp);
        AddParam("@no_documento_devolucion", oBeTrans_oc_enc.No_documento_devolucion);
        AddParam("@IdPedidoEncDevolucion", NullIfZero(oBeTrans_oc_enc.IdPedidoEncDevolucion));
        AddParam("@push_to_nav", oBeTrans_oc_enc.Push_to_nav);
        AddParam("@no_documento_ubicacion_erp", oBeTrans_oc_enc.No_documento_ubicacion_erp);
        AddParam("@PutAway_Registrado", oBeTrans_oc_enc.PutAway_Registrado);
        AddParam("@Codigo_Empresa_ERP", oBeTrans_oc_enc.Codigo_Empresa_ERP);
    }
    public static int InsertarOActualizar(clsBeTrans_oc_enc entity, SqlConnection conn, SqlTransaction tx)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            bool existe = Existe(entity.IdOrdenCompraEnc, conn, tx);

            return existe
                ? Actualizar(entity, conn, tx)
                : Insertar(entity, conn, tx);
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static bool Existe(int IdOrdenCompraEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Trans_oc_enc WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
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
    public static List<clsBeVWOrdenCompra> GetAll(IConfiguration config, bool pActivo, DateTime pFechaDel, DateTime pFechaAl, int pIdBodega = 0, int pIdPropietario = 0)
    {
        var lista = new List<clsBeVWOrdenCompra>();

        try
        {
            string query = "SELECT * FROM VW_OrdenCompra WHERE 1=1 ";

            if (pActivo)
                query += " AND Activo=1";
            else
                query += " AND Activo=0";

            query += " AND cast(Fecha AS DATE) BETWEEN @FechaDel AND @FechaAl";

            if (pIdBodega != 0)
                query += " AND IdBodega=@IdBodega";

            if (pIdPropietario != 0)
                query += " AND IdPropietario=@IdPropietario";

            using (var conn = new SqlConnection(config.GetConnectionString("CST")))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@FechaDel", pFechaDel.Date);
                    cmd.Parameters.AddWithValue("@FechaAl", pFechaAl.Date);
                    if (pIdBodega != 0)
                        cmd.Parameters.AddWithValue("@IdBodega", pIdBodega);
                    if (pIdPropietario != 0)
                        cmd.Parameters.AddWithValue("@IdPropietario", pIdPropietario);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new clsBeVWOrdenCompra
                            {
                                Codigo = reader["Código"]?.ToString() ?? "",
                                Bodega = reader["Bodega"]?.ToString() ?? "",
                                Propietario = reader["Propietario"]?.ToString() ?? "",
                                Proveedor = reader["Proveedor"]?.ToString() ?? "",
                                TipoIngreso = reader["Tipo Ingreso"]?.ToString() ?? "",
                                Estado = reader["Estado"]?.ToString() ?? "",
                                NoDocumento = reader["No. Documento"]?.ToString() ?? "",
                                Referencia = reader["Referencia"]?.ToString() ?? "",
                                Procedencia = reader["Procedencia"]?.ToString() ?? "",
                                IdPropietario = !reader.IsDBNull(reader.GetOrdinal("IdPropietario")) ? reader.GetInt32(reader.GetOrdinal("IdPropietario")) : 0,
                                Activo = !reader.IsDBNull(reader.GetOrdinal("Activo")) && reader.GetBoolean(reader.GetOrdinal("Activo")),
                                IdPropietarioBodega = !reader.IsDBNull(reader.GetOrdinal("IdPropietarioBodega")) ? reader.GetInt32(reader.GetOrdinal("IdPropietarioBodega")) : 0,
                                Fecha = !reader.IsDBNull(reader.GetOrdinal("Fecha")) ? reader.GetDateTime(reader.GetOrdinal("Fecha")) : DateTime.MinValue,
                                es_devolucion = !reader.IsDBNull(reader.GetOrdinal("es_devolucion")) && reader.GetBoolean(reader.GetOrdinal("es_devolucion")),
                                Enviado_A_ERP = !reader.IsDBNull(reader.GetOrdinal("Enviado_A_ERP")) && reader.GetBoolean(reader.GetOrdinal("Enviado_A_ERP")),
                                IdBodega = !reader.IsDBNull(reader.GetOrdinal("IdBodega")) ? reader.GetInt32(reader.GetOrdinal("IdBodega")) : 0,
                                NoPoliza = reader["NoPoliza"]?.ToString() ?? "",
                                NoOrden = reader["NoOrden"]?.ToString() ?? "",
                                No_Documento_Recepcion_ERP = reader["No_Documento_Recepcion_ERP"]?.ToString() ?? "",
                                No_Documento_Devolucion = reader["No_Documento_Devolucion"]?.ToString() ?? "",
                                No_Documento_Ubicacion_ERP = reader["No_Documento_Ubicacion_ERP"]?.ToString() ?? "",
                                No_Ticket_TMS = reader["No_Ticket_TMS"]?.ToString() ?? "",
                                No_Marchamo = reader["No_Marchamo"]?.ToString() ?? "",
                                Control_Poliza = !reader.IsDBNull(reader.GetOrdinal("Control_Poliza")) && reader.GetBoolean(reader.GetOrdinal("Control_Poliza"))
                            };

                            lista.Add(item);
                        }
                    }

                    cmd.Transaction.Commit();
                }
            }

            return lista;
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);
            throw new Exception(vMsgError, ex);
        }
    }

    public static clsBeTrans_oc_enc? Get_Single_By_Referencia(ref clsBeTrans_oc_enc pBeTrans_oc_enc,
                                                             SqlConnection pConnection,
                                                             SqlTransaction pTransaction,
                                                             bool Llenar_Lotes = false)
    {

        clsBeTrans_oc_enc? resultado = null;

        try
        {
            const string sp = @"SELECT * FROM Trans_oc_enc 
                                WHERE Referencia = @Referencia AND IdTipoIngresoOC = @IdTipoIngresoOC";

            using (var cmd = new SqlCommand(sp, pConnection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = pTransaction;

                cmd.Parameters.Add(new SqlParameter("@Referencia", pBeTrans_oc_enc.Referencia));
                cmd.Parameters.Add(new SqlParameter("@IdTipoIngresoOC", pBeTrans_oc_enc.IdTipoIngresoOC));

                using (var dad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        var BeOcEnc = new clsBeTrans_oc_enc();
                        Cargar(ref BeOcEnc, dt.Rows[0]);

                        if (Llenar_Lotes)
                        {
                            BeOcEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeOcEnc.IdOrdenCompraEnc,
                                                                                                  pConnection,
                                                                                                  pTransaction);
                        }

                        resultado = BeOcEnc;
                    }
                }
            }

            return resultado;
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);
            throw new Exception(vMsgError, ex);
        }
    }

    public static clsBeTrans_oc_enc? GetSingle(int pIdOrdenCompra,
                                               SqlConnection lConnection,
                                               SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT enc.*, ti.es_devolucion, ti.nombre AS TipoIngreso 
                       FROM Trans_oc_enc AS enc  
                       INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                       WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT?.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTrans_oc_enc Obj = new clsBeTrans_oc_enc();

                    Cargar(ref Obj, lRow);

                    if (lRow["IdPropietarioBodega"] != DBNull.Value)
                    {
                        Obj.PropietarioBodega.IdPropietarioBodega = Convert.ToInt32(lRow["IdPropietarioBodega"]);
                        clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, lConnection, lTransaction);
                    }

                    if (lRow["IdProveedorBodega"] != DBNull.Value)
                    {
                        Obj.ProveedorBodega.IdAsignacion = Convert.ToInt32(lRow["IdProveedorBodega"]);
                        clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction);
                    }

                    if (lRow["IdTipoIngresoOC"] != DBNull.Value)
                    {
                        Obj.IdTipoIngresoOC = Convert.ToInt32(lRow["IdTipoIngresoOC"]);
                        Obj.TipoIngreso = new clsBeTrans_oc_ti();
                        Obj.TipoIngreso.Nombre = Convert.ToString(lRow["TipoIngreso"]) ?? string.Empty;
                    }

                    Obj.IsNew = false;

                    Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(Obj.IdOrdenCompraEnc, lConnection, lTransaction);

                    Obj.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                        ?? new List<clsBeTrans_oc_det>();
                    Obj.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                        ?? new List<clsBeTrans_oc_det_lote>();
                    Obj.ObjPoliza = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc, lConnection, lTransaction);
                    Obj.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                        ?? new List<clsBeTrans_oc_imagen>();

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

    public static clsBeTrans_oc_enc? Get_Orden_Compra(int pIdOrdenCompra,
                                                      SqlConnection lConnection,
                                                      SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT enc.*, ti.es_devolucion, ti.nombre AS TipoIngreso 
                        FROM Trans_oc_enc AS enc 
                        INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega 
                        INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC 
                        WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTrans_oc_enc BeTransOcEnc1 = new clsBeTrans_oc_enc();

                    Cargar(ref BeTransOcEnc1, lRow);

                    BeTransOcEnc1.IdBodega = Convert.ToInt32(lRow["IdBodega"]);

                    var estadoTemp = BeTransOcEnc1.EstadoOC;
                    clsLnTrans_oc_estado.GetSingle(estadoTemp, lConnection, lTransaction);
                    BeTransOcEnc1.EstadoOC = estadoTemp;

                    if (lRow["IdPropietarioBodega"] != DBNull.Value && lRow["IdPropietarioBodega"] != null)
                    {
                        BeTransOcEnc1.PropietarioBodega.IdPropietarioBodega = Convert.ToInt32(lRow["IdPropietarioBodega"]);
                        clsLnPropietario_bodega.Obtener(BeTransOcEnc1.PropietarioBodega, lConnection, lTransaction);
                    }

                    if (lRow["IdProveedorBodega"] != DBNull.Value && lRow["IdProveedorBodega"] != null)
                    {
                        BeTransOcEnc1.ProveedorBodega.IdAsignacion = Convert.ToInt32(lRow["IdProveedorBodega"]);
                        clsLnProveedor_bodega.Obtener(BeTransOcEnc1.ProveedorBodega, lConnection, lTransaction);
                    }

                    if (BeTransOcEnc1.ProveedorBodega.IdProveedor > 0)
                    {
                        BeTransOcEnc1.ProveedorBodega.Proveedor.TiemposProveedor = clsLnProveedor_tiempos.Get_All_Tiempos_By_IdProveedor(BeTransOcEnc1.ProveedorBodega.IdProveedor, lConnection, lTransaction);
                    }

                    if (lRow["IdTipoIngresoOC"] != DBNull.Value && lRow["IdTipoIngresoOC"] != null)
                    {
                        BeTransOcEnc1.IdTipoIngresoOC = Convert.ToInt32(lRow["IdTipoIngresoOC"]);
                        if (BeTransOcEnc1.TipoIngreso == null)
                            BeTransOcEnc1.TipoIngreso = new clsBeTrans_oc_ti();
                        BeTransOcEnc1.TipoIngreso.Nombre = lRow["TipoIngreso"]?.ToString() ?? string.Empty;

                    }

                    BeTransOcEnc1.IsNew = false;
                    BeTransOcEnc1.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction);
                    BeTransOcEnc1.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction);
                    BeTransOcEnc1.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction);
                    BeTransOcEnc1.ObjPoliza = clsLnTrans_oc_pol.GetSingle(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction);
                    BeTransOcEnc1.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction);

                    return BeTransOcEnc1;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Actualizar_Estado_OC_By_Interface(int pIdOrdenCompraEnc,
                                                         int IdEstadoOC,
                                                         SqlConnection lConnection,
                                                         SqlTransaction lTransaction)
    {
        try
        {
            const string vSQL = @"UPDATE trans_oc_enc SET IdEstadoOC = @IdEstadoOC
                              WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

            clsBeTrans_oc_enc boOC = new clsBeTrans_oc_enc() { IdOrdenCompraEnc = pIdOrdenCompraEnc };
            Obtener(ref boOC, lConnection, lTransaction);

            if (boOC.IdEstadoOC != (int)clsDataContractDI.tEstadoOC.CERRADA && boOC.IdEstadoOC != (int)clsDataContractDI.tEstadoOC.ANULADA)
            {
                using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    lCommand.Parameters.AddWithValue("@IdEstadoOC", IdEstadoOC);
                    lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                    lCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Obtener(ref clsBeTrans_oc_enc oBeTrans_oc_enc,
                               SqlConnection lConnection,
                               SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            const string sp = @"SELECT * FROM Trans_oc_enc 
                           WHERE (IdOrdenCompraEnc = @IdOrdenCompraEnc)";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
            using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
            {
                dad.SelectCommand.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    var lrow = dt.Rows[0];
                    Cargar(ref oBeTrans_oc_enc, lrow);
                    oBeTrans_oc_enc.EstadoOC = clsLnTrans_oc_estado.GetSingle(oBeTrans_oc_enc.EstadoOC, lConnection, lTransaction);
                    result = true;
                }
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Actualizar_Estado_OC(clsBeTrans_re_enc pRecEnc,
                                          clsBeTrans_re_oc pRecOrdenCompra,
                                          SqlConnection lConnection,
                                          SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            clsBeTrans_oc_enc boOC = new clsBeTrans_oc_enc() { IdOrdenCompraEnc = pRecOrdenCompra.IdOrdenCompraEnc };

            Obtener(ref boOC, lConnection, lTransaction);

            if (pRecEnc.IsNew)
            {
                if (boOC.IdEstadoOC == 6) boOC.Enviado_A_ERP = false;

                boOC.IdEstadoOC = 2;
                result = Actualizar(boOC, lConnection, lTransaction);
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Actualizar_Estado_BackOrder(int pIdOrdenCompraEnc,
                                                  SqlConnection pConnection,
                                                  SqlTransaction pTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = @"UPDATE trans_oc_enc 
                       SET IdEstadoOC = @IdEstadoOC 
                       WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@IdEstadoOC", (int)clsDataContractDI.tEstadoOC.BACK_ORDER);
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                result = lCommand.ExecuteNonQuery();
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Actualizar_Estado_Cerrada(int pIdOrdenCompraEnc,
                                               SqlConnection pConnection,
                                               SqlTransaction pTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = @"UPDATE trans_oc_enc 
                       SET IdEstadoOC = 4,
                       Hora_Fin_Recepcion = GETDATE()
                       WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                result = lCommand.ExecuteNonQuery();
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeTms_ticket? Get_BeTicket_By_IdOrdenCompraEnc(int pIdOrdenCompraEnc,
                                                                    SqlConnection lConnection,
                                                                    SqlTransaction lTransaction)
    {
        clsBeTms_ticket? result = null;
        DataTable lTable = new DataTable("Result");

        try
        {
            string vSQL = "select tms.* from " +
                         "trans_oc_enc oc_enc inner join tms_ticket tms on oc_enc.no_ticket_tms=tms.IdTicket " +
                         "where oc_enc.Activo =1 and IdOrdenCompraEnc=@pIdOrdenCompraEnc ";

            using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(vSQL, lConnection))
            {
                lDataAdapter.SelectCommand.Transaction = lTransaction;
                lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc);
                lDataAdapter.Fill(lTable);

                if (lTable != null && lTable.Rows.Count > 0)
                {
                    DataRow lRow = lTable.Rows[0];
                    clsBeTms_ticket BeTMSTicket = new clsBeTms_ticket();

                    clsLnTms_ticket.Cargar(ref BeTMSTicket, lRow);

                    result = BeTMSTicket;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static bool Get_Parametros_Devol_By_IdOrdenCompraEnc(int pIdOrdenCompra,
                                                                ref int pIdPedidoEncDevol,
                                                                ref string pNoDocumentoRefDevol,
                                                                SqlConnection lConnection,
                                                                SqlTransaction lTransaction)
    {
        bool result = false;

        pIdPedidoEncDevol = 0;
        pNoDocumentoRefDevol = string.Empty;

        try
        {
            string vSQL = @" SELECT IdPedidoEncDevolucion, no_documento_devolucion 
                         FROM Trans_oc_enc 
                         WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc ";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    pIdPedidoEncDevol = (lRow["IdPedidoEncDevolucion"] == DBNull.Value) ? 0 : Convert.ToInt32(lRow["IdPedidoEncDevolucion"]);
                    pNoDocumentoRefDevol = (lRow["no_documento_devolucion"] == DBNull.Value) ? string.Empty : Convert.ToString(lRow["no_documento_devolucion"]) ?? string.Empty;
                    result = true;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static clsBeTrans_oc_ti? Get_BeTipoDocumento_By_IdOrdenCompraEnc(int pIdOrdenCompra,
                                                                           SqlConnection lConnection,
                                                                           SqlTransaction lTransaction)
    {
        clsBeTrans_oc_ti? result = null;

        try
        {
            string vSQL = @"SELECT B.* FROM Trans_oc_enc A INNER JOIN Trans_OC_Ti B 
                        ON a.IdTipoIngresoOC = b.IdTipoIngresoOC
                        WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTrans_oc_ti BeTransOCTI = new clsBeTrans_oc_ti();
                    clsLnTrans_oc_ti.Cargar(ref BeTransOCTI, lRow);
                    result = BeTransOCTI;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static string Get_No_Pedido(int pIdOrdenCompra,
                                      SqlConnection lConnection,
                                      SqlTransaction lTransaction)
    {
        string result = string.Empty;

        try
        {
            string vSQL = @"SELECT referencia FROM Trans_oc_enc AS enc                                               
                        WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    result = (lRow["Referencia"] == DBNull.Value) ? string.Empty : Convert.ToString(lRow["Referencia"]) ?? string.Empty;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static clsBeTrans_oc_enc? Get_Single_By_IdOrdenCompraEnc(IConfiguration config, int pIdOrdenCompra)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));

        var connStr = config.GetConnectionString("CST");
        if (string.IsNullOrWhiteSpace(connStr))
            throw new Exception("No se encontró la cadena de conexión 'CST' en ConnectionStrings.");

        const string sql = @"
        SELECT enc.*
        FROM Trans_oc_enc AS enc
        WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc;
    ";

        using var cn = new SqlConnection(connStr);
        cn.Open();

        using var tx = cn.BeginTransaction(IsolationLevel.ReadCommitted);

        try
        {
            clsBeTrans_oc_enc? resultado = null;

            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@IdOrdenCompraEnc", SqlDbType.Int).Value = pIdOrdenCompra;

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    var beOcEnc = new clsBeTrans_oc_enc();
                    Cargar(ref beOcEnc, dt.Rows[0]);
                    beOcEnc.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(beOcEnc.IdOrdenCompraEnc, cn, tx);
                    beOcEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(beOcEnc.IdOrdenCompraEnc, cn, tx);
                    resultado = beOcEnc;
                }
            }

            tx.Commit();
            return resultado;
        }
        catch (Exception ex)
        {
            try { tx.Rollback(); } catch { /* opcional: log */ }

            throw new Exception(
                $"Get_Single_By_IdOrdenCompraEnc falló. IdOrdenCompraEnc={pIdOrdenCompra}. {ex.Message}",
                ex
            );
        }
    }


}