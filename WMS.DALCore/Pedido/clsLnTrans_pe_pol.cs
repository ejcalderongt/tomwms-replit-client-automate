using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Pedido;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_pe_pol
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_pe_pol oBeTrans_pe_pol, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_pe_pol.IdOrdenPedidoPol = GetInt("IdOrdenPedidoPol");
            oBeTrans_pe_pol.IdOrdenPedidoEnc = GetInt("IdOrdenPedidoEnc");
            oBeTrans_pe_pol.Bl_no = GetString("bl_no");
            oBeTrans_pe_pol.NoPoliza = GetString("NoPoliza");
            oBeTrans_pe_pol.Pto_descarga = GetString("pto_descarga");
            oBeTrans_pe_pol.Viaje_no = GetString("viaje_no");
            oBeTrans_pe_pol.Buque_no = GetString("buque_no");
            oBeTrans_pe_pol.Remitente = GetString("remitente");
            oBeTrans_pe_pol.Fecha_abordaje = GetDate("fecha_abordaje");
            oBeTrans_pe_pol.Destino = GetString("destino");
            oBeTrans_pe_pol.Dir_destino = GetString("dir_destino");
            oBeTrans_pe_pol.Descripcion = GetString("descripcion");
            oBeTrans_pe_pol.Po_number = GetString("po_number");
            oBeTrans_pe_pol.Cantidad = GetInt("cantidad");
            oBeTrans_pe_pol.Piezas = GetInt("piezas");
            oBeTrans_pe_pol.Total_kgs = GetDecimal("total_kgs");
            oBeTrans_pe_pol.Cbm = GetDecimal("cbm");
            oBeTrans_pe_pol.Dua = GetString("dua");
            oBeTrans_pe_pol.Fecha_poliza = GetDate("fecha_poliza");
            oBeTrans_pe_pol.Pais_procede = GetString("pais_procede");
            oBeTrans_pe_pol.Tipo_cambio = GetDecimal("tipo_cambio");
            oBeTrans_pe_pol.Total_valoraduana = GetDecimal("total_valoraduana");
            oBeTrans_pe_pol.Total_lineas = GetInt("total_lineas");
            oBeTrans_pe_pol.Total_bultos = GetInt("total_bultos");
            oBeTrans_pe_pol.Total_bultos_peso = GetDecimal("total_bultos_peso");
            oBeTrans_pe_pol.Total_usd = GetDecimal("total_usd");
            oBeTrans_pe_pol.Total_flete = GetDecimal("total_flete");
            oBeTrans_pe_pol.Total_seguro = GetDecimal("total_seguro");
            oBeTrans_pe_pol.User_agr = GetString("user_agr");
            oBeTrans_pe_pol.Fec_agr = GetDate("fec_agr");
            oBeTrans_pe_pol.User_mod = GetString("user_mod");
            oBeTrans_pe_pol.Fec_mod = GetDate("fec_mod");
            oBeTrans_pe_pol.Clave_aduana = GetString("clave_aduana");
            oBeTrans_pe_pol.Nit_imp_exp = GetString("nit_imp_exp");
            oBeTrans_pe_pol.Clase = GetString("clase");
            oBeTrans_pe_pol.Mod_transporte = GetString("mod_transporte");
            oBeTrans_pe_pol.Total_liquidar = GetDecimal("total_liquidar");
            oBeTrans_pe_pol.Total_general = GetDecimal("total_general");
            oBeTrans_pe_pol.Codigo_poliza = GetString("codigo_poliza");
            oBeTrans_pe_pol.Ticket = GetString("ticket");
            oBeTrans_pe_pol.Numero_orden = GetString("numero_orden");
            oBeTrans_pe_pol.Fecha_aceptacion = GetDate("fecha_aceptacion");
            oBeTrans_pe_pol.Fecha_llegada = GetDate("fecha_llegada");
            oBeTrans_pe_pol.Total_otros = GetDecimal("total_otros");
            oBeTrans_pe_pol.IdRegimen = GetInt("IdRegimen");
            oBeTrans_pe_pol.Activo = GetBool("activo");
            oBeTrans_pe_pol.Total_bultos_peso_neto = GetDecimal("total_bultos_peso_neto");
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

    public static int Insertar(IConfiguration config, clsBeTrans_pe_pol oBeTrans_pe_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_pol");
            Ins.Add("idordenpedidopol", "@idordenpedidopol", "F");
            Ins.Add("idordenpedidoenc", "@idordenpedidoenc", "F");
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
            Ins.Add("clave_aduana", "@clave_aduana", "F");
            Ins.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Ins.Add("clase", "@clase", "F");
            Ins.Add("mod_transporte", "@mod_transporte", "F");
            Ins.Add("total_liquidar", "@total_liquidar", "F");
            Ins.Add("total_general", "@total_general", "F");
            Ins.Add("codigo_poliza", "@codigo_poliza", "F");
            Ins.Add("ticket", "@ticket", "F");
            Ins.Add("numero_orden", "@numero_orden", "F");
            Ins.Add("fecha_aceptacion", "@fecha_aceptacion", "F");
            Ins.Add("fecha_llegada", "@fecha_llegada", "F");
            Ins.Add("total_otros", "@total_otros", "F");
            Ins.Add("idregimen", "@idregimen", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", "F");

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

            Bind(cmd, oBeTrans_pe_pol);

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

    public static int Insertar(IConfiguration config, clsBeTrans_pe_pol oBeTrans_pe_pol)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_pol");
            Ins.Add("idordenpedidopol", "@idordenpedidopol", "F");
            Ins.Add("idordenpedidoenc", "@idordenpedidoenc", "F");
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
            Ins.Add("clave_aduana", "@clave_aduana", "F");
            Ins.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Ins.Add("clase", "@clase", "F");
            Ins.Add("mod_transporte", "@mod_transporte", "F");
            Ins.Add("total_liquidar", "@total_liquidar", "F");
            Ins.Add("total_general", "@total_general", "F");
            Ins.Add("codigo_poliza", "@codigo_poliza", "F");
            Ins.Add("ticket", "@ticket", "F");
            Ins.Add("numero_orden", "@numero_orden", "F");
            Ins.Add("fecha_aceptacion", "@fecha_aceptacion", "F");
            Ins.Add("fecha_llegada", "@fecha_llegada", "F");
            Ins.Add("total_otros", "@total_otros", "F");
            Ins.Add("idregimen", "@idregimen", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_pe_pol);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_pe_pol oBeTrans_pe_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_pe_pol");
            Upd.Add("idordenpedidopol", "@idordenpedidopol", "F");
            Upd.Add("idordenpedidoenc", "@idordenpedidoenc", "F");
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
            Upd.Add("clave_aduana", "@clave_aduana", "F");
            Upd.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Upd.Add("clase", "@clase", "F");
            Upd.Add("mod_transporte", "@mod_transporte", "F");
            Upd.Add("total_liquidar", "@total_liquidar", "F");
            Upd.Add("total_general", "@total_general", "F");
            Upd.Add("codigo_poliza", "@codigo_poliza", "F");
            Upd.Add("ticket", "@ticket", "F");
            Upd.Add("numero_orden", "@numero_orden", "F");
            Upd.Add("fecha_aceptacion", "@fecha_aceptacion", "F");
            Upd.Add("fecha_llegada", "@fecha_llegada", "F");
            Upd.Add("total_otros", "@total_otros", "F");
            Upd.Add("idregimen", "@idregimen", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", "F");
            Upd.Where("IdOrdenPedidoPol = @IdOrdenPedidoPol" +
                " AND IdOrdenPedidoEnc = @IdOrdenPedidoEnc");

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

            Bind(cmd, oBeTrans_pe_pol);

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

    public int Eliminar(IConfiguration config, clsBeTrans_pe_pol oBeTrans_pe_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_pe_pol" +
             "  Where(IdOrdenPedidoPol = @IdOrdenPedidoPol)" +
             "  And (IdOrdenPedidoEnc = @IdOrdenPedidoEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenPedidoPol", oBeTrans_pe_pol.IdOrdenPedidoPol));

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
            const string sp = "Select * FROM Trans_pe_pol";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_pe_pol pBeTrans_pe_pol)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_pe_pol" +
            " Where(IdOrdenPedidoPol = @IdOrdenPedidoPol)" +
            " And (IdOrdenPedidoEnc = @IdOrdenPedidoEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenPedidoPol", pBeTrans_pe_pol.IdOrdenPedidoPol));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenPedidoEnc", pBeTrans_pe_pol.IdOrdenPedidoEnc));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_pe_pol, r);
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

    public static List<clsBeTrans_pe_pol> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_pe_pol> lreturnList = new List<clsBeTrans_pe_pol>();

        try
        {
            const string sp = "Select * FROM Trans_pe_pol";

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

                        clsBeTrans_pe_pol vBeTrans_pe_pol = new clsBeTrans_pe_pol();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_pe_pol = new clsBeTrans_pe_pol();
                            Cargar(ref vBeTrans_pe_pol, dr);
                            lreturnList.Add(vBeTrans_pe_pol);
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

            const string sp = "Select ISNULL(Max(IdOrdenPedidoPol),0) FROM Trans_pe_pol";

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


            const string sp = "Select ISNULL(Max(IdOrdenPedidoPol),0) FROM Trans_pe_pol";

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

    public static void Bind(SqlCommand cmd, clsBeTrans_pe_pol o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdOrdenPedidoPol", o.IdOrdenPedidoPol != 0 ? o.IdOrdenPedidoPol : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOrdenPedidoEnc", o.IdOrdenPedidoEnc != 0 ? o.IdOrdenPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@bl_no", !string.IsNullOrWhiteSpace(o.Bl_no) ? o.Bl_no : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@NoPoliza", !string.IsNullOrWhiteSpace(o.NoPoliza) ? o.NoPoliza : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@pto_descarga", !string.IsNullOrWhiteSpace(o.Pto_descarga) ? o.Pto_descarga : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@viaje_no", !string.IsNullOrWhiteSpace(o.Viaje_no) ? o.Viaje_no : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@buque_no", !string.IsNullOrWhiteSpace(o.Buque_no) ? o.Buque_no : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@remitente", !string.IsNullOrWhiteSpace(o.Remitente) ? o.Remitente : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_abordaje", o.Fecha_abordaje != DateTime.MinValue ? o.Fecha_abordaje : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@destino", !string.IsNullOrWhiteSpace(o.Destino) ? o.Destino : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dir_destino", !string.IsNullOrWhiteSpace(o.Dir_destino) ? o.Dir_destino : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@descripcion", !string.IsNullOrWhiteSpace(o.Descripcion) ? o.Descripcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@po_number", !string.IsNullOrWhiteSpace(o.Po_number) ? o.Po_number : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad", o.Cantidad != 0 ? o.Cantidad : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@piezas", o.Piezas != 0 ? o.Piezas : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_kgs", o.Total_kgs != 0 ? o.Total_kgs : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cbm", o.Cbm != 0 ? o.Cbm : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dua", !string.IsNullOrWhiteSpace(o.Dua) ? o.Dua : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_poliza", o.Fecha_poliza != DateTime.MinValue ? o.Fecha_poliza : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@pais_procede", !string.IsNullOrWhiteSpace(o.Pais_procede) ? o.Pais_procede : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@tipo_cambio", o.Tipo_cambio != 0 ? o.Tipo_cambio : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_valoraduana", o.Total_valoraduana != 0 ? o.Total_valoraduana : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_lineas", o.Total_lineas != 0 ? o.Total_lineas : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_bultos", o.Total_bultos != 0 ? o.Total_bultos : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_bultos_peso", o.Total_bultos_peso != 0 ? o.Total_bultos_peso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_usd", o.Total_usd != 0 ? o.Total_usd : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_flete", o.Total_flete != 0 ? o.Total_flete : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_seguro", o.Total_seguro != 0 ? o.Total_seguro : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@clave_aduana", !string.IsNullOrWhiteSpace(o.Clave_aduana) ? o.Clave_aduana : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nit_imp_exp", !string.IsNullOrWhiteSpace(o.Nit_imp_exp) ? o.Nit_imp_exp : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@clase", !string.IsNullOrWhiteSpace(o.Clase) ? o.Clase : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@mod_transporte", !string.IsNullOrWhiteSpace(o.Mod_transporte) ? o.Mod_transporte : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_liquidar", o.Total_liquidar != 0 ? o.Total_liquidar : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_general", o.Total_general != 0 ? o.Total_general : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo_poliza", !string.IsNullOrWhiteSpace(o.Codigo_poliza) ? o.Codigo_poliza : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ticket", !string.IsNullOrWhiteSpace(o.Ticket) ? o.Ticket : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@numero_orden", !string.IsNullOrWhiteSpace(o.Numero_orden) ? o.Numero_orden : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_aceptacion", o.Fecha_aceptacion != DateTime.MinValue ? o.Fecha_aceptacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_llegada", o.Fecha_llegada != DateTime.MinValue ? o.Fecha_llegada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@total_otros", o.Total_otros != 0 ? o.Total_otros : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdRegimen", o.IdRegimen != 0 ? o.IdRegimen : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@total_bultos_peso_neto", o.Total_bultos_peso_neto != 0 ? o.Total_bultos_peso_neto : DBNull.Value));
    }

    public static int InsertOrUpdate(IConfiguration config, List<clsBeTrans_pe_pol> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        int total = 0;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {

            foreach (var entity in entities)
            {
                if (entity.IdOrdenPedidoEnc != 0) {
                    bool existe = Existe(entity.IdOrdenPedidoPol, entity.IdOrdenPedidoEnc, connection, isExternalTx ? tx! : localTx!);
                    int resultado = existe
                  ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                  : Insertar(config, entity, connection, isExternalTx ? tx : localTx);
                    total += resultado;
                }
            }

            return total;

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
    public static bool Existe(int idOrdenPedidoPol, int idOrdenPedidoEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_pe_pol
        WHERE IdOrdenPedidoPol = @IdOrdenPedidoPol AND IdOrdenPedidoEnc = @IdOrdenPedidoEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdOrdenPedidoPol", idOrdenPedidoPol);
        cmd.Parameters.AddWithValue("@IdOrdenPedidoEnc", idOrdenPedidoEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }
}