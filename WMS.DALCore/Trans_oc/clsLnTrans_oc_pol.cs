using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_oc;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_oc_pol
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_oc_pol oBeTrans_oc_pol, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_oc_pol.IdOrdenCompraPol = GetInt("IdOrdenCompraPol");
            oBeTrans_oc_pol.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_oc_pol.Bl_no = GetString("bl_no");
            oBeTrans_oc_pol.NoPoliza = GetString("NoPoliza");
            oBeTrans_oc_pol.Pto_descarga = GetString("pto_descarga");
            oBeTrans_oc_pol.Viaje_no = GetString("viaje_no");
            oBeTrans_oc_pol.Buque_no = GetString("buque_no");
            oBeTrans_oc_pol.Remitente = GetString("remitente");
            oBeTrans_oc_pol.Fecha_abordaje = GetDate("fecha_abordaje");
            oBeTrans_oc_pol.Destino = GetString("destino");
            oBeTrans_oc_pol.Dir_destino = GetString("dir_destino");
            oBeTrans_oc_pol.Descripcion = GetString("descripcion");
            oBeTrans_oc_pol.Po_number = GetString("po_number");
            oBeTrans_oc_pol.Cantidad = GetDouble("cantidad");
            oBeTrans_oc_pol.Piezas = GetInt("piezas");
            oBeTrans_oc_pol.Total_kgs = GetDouble("total_kgs");
            oBeTrans_oc_pol.Cbm = GetDouble("cbm");
            oBeTrans_oc_pol.Dua = GetString("dua");
            oBeTrans_oc_pol.Fecha_poliza = GetDate("fecha_poliza");
            oBeTrans_oc_pol.Pais_procede = GetString("pais_procede");
            oBeTrans_oc_pol.Tipo_cambio = GetDouble("tipo_cambio");
            oBeTrans_oc_pol.Total_valoraduana = GetDouble("total_valoraduana");
            oBeTrans_oc_pol.Total_lineas = GetInt("total_lineas");
            oBeTrans_oc_pol.Total_bultos = GetInt("total_bultos");
            oBeTrans_oc_pol.Total_bultos_peso = GetDouble("total_bultos_peso");
            oBeTrans_oc_pol.Total_usd = GetDouble("total_usd");
            oBeTrans_oc_pol.Total_flete = GetDouble("total_flete");
            oBeTrans_oc_pol.Total_seguro = GetDouble("total_seguro");
            oBeTrans_oc_pol.User_agr = GetString("user_agr");
            oBeTrans_oc_pol.Fec_agr = GetDate("fec_agr");
            oBeTrans_oc_pol.User_mod = GetString("user_mod");
            oBeTrans_oc_pol.Fec_mod = GetDate("fec_mod");
            oBeTrans_oc_pol.Codigo_poliza = GetString("codigo_poliza");
            oBeTrans_oc_pol.Ticket = GetString("ticket");
            oBeTrans_oc_pol.Numero_orden = GetString("numero_orden");
            oBeTrans_oc_pol.Fecha_aceptacion = GetDate("fecha_aceptacion");
            oBeTrans_oc_pol.Fecha_llegada = GetDate("fecha_llegada");
            oBeTrans_oc_pol.Total_otros = GetDouble("total_otros");
            oBeTrans_oc_pol.IdRegimen = GetInt("IdRegimen");
            oBeTrans_oc_pol.Total_bultos_peso_neto = GetDouble("total_bultos_peso_neto");
            oBeTrans_oc_pol.Clave_aduana = GetString("clave_aduana");
            oBeTrans_oc_pol.Nit_imp_exp = GetString("nit_imp_exp");
            oBeTrans_oc_pol.Clase = GetString("clase");
            oBeTrans_oc_pol.Mod_transporte = GetString("mod_transporte");
            oBeTrans_oc_pol.Total_liquidar = GetDouble("total_liquidar");
            oBeTrans_oc_pol.Total_general = GetDouble("total_general");
            oBeTrans_oc_pol.Codigo_Barra = GetString("Codigo_Barra");
            oBeTrans_oc_pol.Activo = GetBool("activo");
            oBeTrans_oc_pol.IdBodega = GetInt("IdBodega");
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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_pol oBeTrans_oc_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_pol");
            Ins.Add("idordencomprapol", "@idordencomprapol", "F");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("bl_no", "@bl_no", "F");
            Ins.Add("nopoliza", "@nopoliza", "F");
            Ins.Add("pto_descarga", "@pto_descarga", "F");
            Ins.Add("viaje_no", "@viaje_no", "F");
            Ins.Add("buque_no", "@buque_no", "F");
            Ins.Add("remitente", "@remitente", "F");
            Ins.Add("fecha_abordaje", "@fecha_abordaje", "F");
            Ins.Add("destino", "@destino", "F");
            Ins.Add("dir_destino", "@dir_destino", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("po_number", "@po_number", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("piezas", "@piezas", "F");
            Ins.Add("total_kgs", "@total_kgs", "F");
            Ins.Add("cbm", "@cbm", "F");
            Ins.Add("dua", "@dua", "F");
            Ins.Add("fecha_poliza", "@fecha_poliza", "F");
            Ins.Add("pais_procede", "@pais_procede", "F");
            Ins.Add("tipo_cambio", "@tipo_cambio", "F");
            Ins.Add("total_valoraduana", "@total_valoraduana", "F");
            Ins.Add("total_lineas", "@total_lineas", "F");
            Ins.Add("total_bultos", "@total_bultos", "F");
            Ins.Add("total_bultos_peso", "@total_bultos_peso", "F");
            Ins.Add("total_usd", "@total_usd", "F");
            Ins.Add("total_flete", "@total_flete", "F");
            Ins.Add("total_seguro", "@total_seguro", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("codigo_poliza", "@codigo_poliza", "F");
            Ins.Add("ticket", "@ticket", "F");
            Ins.Add("numero_orden", "@numero_orden", "F");
            Ins.Add("fecha_aceptacion", "@fecha_aceptacion", "F");
            Ins.Add("fecha_llegada", "@fecha_llegada", "F");
            Ins.Add("total_otros", "@total_otros", "F");
            Ins.Add("idregimen", "@idregimen", "F");
            Ins.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", "F");
            Ins.Add("clave_aduana", "@clave_aduana", "F");
            Ins.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Ins.Add("clase", "@clase", "F");
            Ins.Add("mod_transporte", "@mod_transporte", "F");
            Ins.Add("total_liquidar", "@total_liquidar", "F");
            Ins.Add("total_general", "@total_general", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idbodega", "@idbodega", "F");

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

            BindParameters(cmd, oBeTrans_oc_pol);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_oc_pol oBeTrans_oc_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_oc_pol");
            Upd.Add("idordencomprapol", "@idordencomprapol", "F");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("bl_no", "@bl_no", "F");
            Upd.Add("nopoliza", "@nopoliza", "F");
            Upd.Add("pto_descarga", "@pto_descarga", "F");
            Upd.Add("viaje_no", "@viaje_no", "F");
            Upd.Add("buque_no", "@buque_no", "F");
            Upd.Add("remitente", "@remitente", "F");
            Upd.Add("fecha_abordaje", "@fecha_abordaje", "F");
            Upd.Add("destino", "@destino", "F");
            Upd.Add("dir_destino", "@dir_destino", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("po_number", "@po_number", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("piezas", "@piezas", "F");
            Upd.Add("total_kgs", "@total_kgs", "F");
            Upd.Add("cbm", "@cbm", "F");
            Upd.Add("dua", "@dua", "F");
            Upd.Add("fecha_poliza", "@fecha_poliza", "F");
            Upd.Add("pais_procede", "@pais_procede", "F");
            Upd.Add("tipo_cambio", "@tipo_cambio", "F");
            Upd.Add("total_valoraduana", "@total_valoraduana", "F");
            Upd.Add("total_lineas", "@total_lineas", "F");
            Upd.Add("total_bultos", "@total_bultos", "F");
            Upd.Add("total_bultos_peso", "@total_bultos_peso", "F");
            Upd.Add("total_usd", "@total_usd", "F");
            Upd.Add("total_flete", "@total_flete", "F");
            Upd.Add("total_seguro", "@total_seguro", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("codigo_poliza", "@codigo_poliza", "F");
            Upd.Add("ticket", "@ticket", "F");
            Upd.Add("numero_orden", "@numero_orden", "F");
            Upd.Add("fecha_aceptacion", "@fecha_aceptacion", "F");
            Upd.Add("fecha_llegada", "@fecha_llegada", "F");
            Upd.Add("total_otros", "@total_otros", "F");
            Upd.Add("idregimen", "@idregimen", "F");
            Upd.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", "F");
            Upd.Add("clave_aduana", "@clave_aduana", "F");
            Upd.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Upd.Add("clase", "@clase", "F");
            Upd.Add("mod_transporte", "@mod_transporte", "F");
            Upd.Add("total_liquidar", "@total_liquidar", "F");
            Upd.Add("total_general", "@total_general", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Where("IdOrdenCompraPol = @IdOrdenCompraPol AND IdOrdenCompraEnc = @IdOrdenCompraEnc");

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

            BindParameters(cmd, oBeTrans_oc_pol);

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

    public int Eliminar(IConfiguration config, clsBeTrans_oc_pol oBeTrans_oc_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_oc_pol" +
             "  Where(IdOrdenCompraPol = @IdOrdenCompraPol)" +
             "  And (IdOrdenCompraEnc = @IdOrdenCompraEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraPol", oBeTrans_oc_pol.IdOrdenCompraPol));

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
            const string sp = "Select * FROM Trans_oc_pol";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_pol pBeTrans_oc_pol)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_oc_pol" +
            " Where(IdOrdenCompraPol = @IdOrdenCompraPol)" +
            " And (IdOrdenCompraEnc = @IdOrdenCompraEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraPol", pBeTrans_oc_pol.IdOrdenCompraPol));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", pBeTrans_oc_pol.IdOrdenCompraEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_oc_pol, r);
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

    public static List<clsBeTrans_oc_pol> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_pol> lreturnList = new List<clsBeTrans_oc_pol>();

        try
        {
            const string sp = "Select * FROM Trans_oc_pol";

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

                        clsBeTrans_oc_pol vBeTrans_oc_pol = new clsBeTrans_oc_pol();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_pol = new clsBeTrans_oc_pol();
                            Cargar(ref vBeTrans_oc_pol, dr);
                            lreturnList.Add(vBeTrans_oc_pol);
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

            const string sp = "Select ISNULL(Max(IdOrdenCompraPol),0) FROM Trans_oc_pol";

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


            const string sp = "Select ISNULL(Max(IdOrdenCompraPol),0) FROM Trans_oc_pol";

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
    public static void BindParameters(SqlCommand cmd, dynamic oBeTrans_oc_pol)
    {
        void AddParam(string name, object value) => cmd.Parameters.Add(new SqlParameter(name, value ?? DBNull.Value));
        object NullIfZero(object value) => (value is int intValue && intValue == 0) ? DBNull.Value : value;

        AddParam("@IdOrdenCompraPol", NullIfZero(oBeTrans_oc_pol.IdOrdenCompraPol));
        AddParam("@IdOrdenCompraEnc", NullIfZero(oBeTrans_oc_pol.IdOrdenCompraEnc));
        AddParam("@bl_no", oBeTrans_oc_pol.Bl_no);
        AddParam("@NoPoliza", oBeTrans_oc_pol.NoPoliza);
        AddParam("@pto_descarga", oBeTrans_oc_pol.Pto_descarga);
        AddParam("@viaje_no", oBeTrans_oc_pol.Viaje_no);
        AddParam("@buque_no", oBeTrans_oc_pol.Buque_no);
        AddParam("@remitente", oBeTrans_oc_pol.Remitente);
        AddParam("@fecha_abordaje", oBeTrans_oc_pol.Fecha_abordaje);
        AddParam("@destino", oBeTrans_oc_pol.Destino);
        AddParam("@dir_destino", oBeTrans_oc_pol.Dir_destino);
        AddParam("@descripcion", oBeTrans_oc_pol.Descripcion);
        AddParam("@po_number", oBeTrans_oc_pol.Po_number);
        AddParam("@cantidad", oBeTrans_oc_pol.Cantidad);
        AddParam("@piezas", oBeTrans_oc_pol.Piezas);
        AddParam("@total_kgs", oBeTrans_oc_pol.Total_kgs);
        AddParam("@cbm", oBeTrans_oc_pol.Cbm);
        AddParam("@dua", oBeTrans_oc_pol.Dua);
        AddParam("@fecha_poliza", oBeTrans_oc_pol.Fecha_poliza);
        AddParam("@pais_procede", oBeTrans_oc_pol.Pais_procede);
        AddParam("@tipo_cambio", oBeTrans_oc_pol.Tipo_cambio);
        AddParam("@total_valoraduana", oBeTrans_oc_pol.Total_valoraduana);
        AddParam("@total_lineas", oBeTrans_oc_pol.Total_lineas);
        AddParam("@total_bultos", oBeTrans_oc_pol.Total_bultos);
        AddParam("@total_bultos_peso", oBeTrans_oc_pol.Total_bultos_peso);
        AddParam("@total_usd", oBeTrans_oc_pol.Total_usd);
        AddParam("@total_flete", oBeTrans_oc_pol.Total_flete);
        AddParam("@total_seguro", oBeTrans_oc_pol.Total_seguro);
        AddParam("@user_agr", oBeTrans_oc_pol.User_agr);
        AddParam("@fec_agr", oBeTrans_oc_pol.Fec_agr);
        AddParam("@user_mod", oBeTrans_oc_pol.User_mod);
        AddParam("@fec_mod", oBeTrans_oc_pol.Fec_mod);
        AddParam("@codigo_poliza", oBeTrans_oc_pol.Codigo_poliza);
        AddParam("@ticket", oBeTrans_oc_pol.Ticket);
        AddParam("@numero_orden", oBeTrans_oc_pol.Numero_orden);
        AddParam("@fecha_aceptacion", oBeTrans_oc_pol.Fecha_aceptacion);
        AddParam("@fecha_llegada", oBeTrans_oc_pol.Fecha_llegada);
        AddParam("@total_otros", oBeTrans_oc_pol.Total_otros);
        AddParam("@IdRegimen", NullIfZero(oBeTrans_oc_pol.IdRegimen));
        AddParam("@total_bultos_peso_neto", oBeTrans_oc_pol.Total_bultos_peso_neto);
        AddParam("@clave_aduana", oBeTrans_oc_pol.Clave_aduana);
        AddParam("@nit_imp_exp", oBeTrans_oc_pol.Nit_imp_exp);
        AddParam("@clase", oBeTrans_oc_pol.Clase);
        AddParam("@mod_transporte", oBeTrans_oc_pol.Mod_transporte);
        AddParam("@total_liquidar", oBeTrans_oc_pol.Total_liquidar);
        AddParam("@total_general", oBeTrans_oc_pol.Total_general);
        AddParam("@Codigo_Barra", oBeTrans_oc_pol.Codigo_Barra);
        AddParam("@activo", oBeTrans_oc_pol.Activo);
        AddParam("@IdBodega", NullIfZero(oBeTrans_oc_pol.IdBodega));
    }
}