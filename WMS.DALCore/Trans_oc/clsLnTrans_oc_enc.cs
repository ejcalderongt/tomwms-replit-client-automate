using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_oc;
using Microsoft.Extensions.Configuration;
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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_enc oBeTrans_oc_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

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

            BindParameters(cmd, oBeTrans_oc_enc);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_oc_enc oBeTrans_oc_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

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

            BindParameters(cmd, oBeTrans_oc_enc);

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
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_enc";

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
    public static int InsertarOActualizar(IConfiguration config, clsBeTrans_oc_enc entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        int totalOperaciones = 0;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            bool existe = Existe(entity.IdOrdenCompraEnc, connection, isExternalTx ? tx! : localTx!);

            totalOperaciones += existe
                ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

            if (!isExternalTx)
                localTx?.Commit();

            return totalOperaciones;
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
}