using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMSWebAPI.Be;

public class clsLnTrans_oc_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_oc_det oBeTrans_oc_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_oc_det.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_oc_det.IdOrdenCompraDet = GetInt("IdOrdenCompraDet");
            oBeTrans_oc_det.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_oc_det.IdArancel = GetInt("IdArancel");
            oBeTrans_oc_det.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_oc_det.IdUnidadMedidaBasica = GetInt("IdUnidadMedidaBasica");
            oBeTrans_oc_det.IdMotivoDevolucion = GetInt("IdMotivoDevolucion");
            oBeTrans_oc_det.No_Linea = GetInt("No_Linea");
            oBeTrans_oc_det.Nombre_producto = GetString("nombre_producto");
            oBeTrans_oc_det.Nombre_presentacion = GetString("nombre_presentacion");
            oBeTrans_oc_det.Nombre_arancel = GetString("nombre_arancel");
            oBeTrans_oc_det.Porcentaje_arancel = GetDouble("porcentaje_arancel");
            oBeTrans_oc_det.Nombre_unidad_medida_basica = GetString("nombre_unidad_medida_basica");
            oBeTrans_oc_det.Cantidad = GetDouble("cantidad");
            oBeTrans_oc_det.Cantidad_recibida = GetDouble("cantidad_recibida");
            oBeTrans_oc_det.Costo = GetDouble("costo");
            oBeTrans_oc_det.Total_linea = GetDouble("total_linea");
            oBeTrans_oc_det.User_agr = GetString("user_agr");
            oBeTrans_oc_det.Fec_agr = GetDate("fec_agr");
            oBeTrans_oc_det.User_mod = GetString("user_mod");
            oBeTrans_oc_det.Fec_mod = GetDate("fec_mod");
            oBeTrans_oc_det.Activo = GetBool("activo");
            oBeTrans_oc_det.Peso = GetDouble("peso");
            oBeTrans_oc_det.Peso_recibido = GetDouble("peso_recibido");
            oBeTrans_oc_det.Atributo_variante_1 = GetString("atributo_variante_1");
            oBeTrans_oc_det.Codigo_producto = GetString("codigo_producto");
            oBeTrans_oc_det.Valor_aduana = GetDouble("valor_aduana");
            oBeTrans_oc_det.Valor_fob = GetDouble("valor_fob");
            oBeTrans_oc_det.Valor_iva = GetDouble("valor_iva");
            oBeTrans_oc_det.Valor_dai = GetDouble("valor_dai");
            oBeTrans_oc_det.Valor_seguro = GetDouble("valor_seguro");
            oBeTrans_oc_det.Valor_flete = GetDouble("valor_flete");
            oBeTrans_oc_det.Peso_neto = GetDouble("peso_neto");
            oBeTrans_oc_det.Peso_bruto = GetDouble("peso_bruto");
            oBeTrans_oc_det.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_oc_det.Nombre_propietario = GetString("nombre_propietario");
            oBeTrans_oc_det.IdOrdenCompraDetPadre = GetInt("IdOrdenCompraDetPadre");
            oBeTrans_oc_det.IdEmbarcador = GetInt("IdEmbarcador");
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

    public static int Insertar(IConfiguration config, clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_oc_det");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idordencompradet", "@idordencompradet", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idarancel", "@idarancel", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nombre_presentacion", "@nombre_presentacion", "F");
            Ins.Add("nombre_arancel", "@nombre_arancel", "F");
            Ins.Add("porcentaje_arancel", "@porcentaje_arancel", "F");
            Ins.Add("nombre_unidad_medida_basica", "@nombre_unidad_medida_basica", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("total_linea", "@total_linea", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("peso_recibido", "@peso_recibido", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("valor_aduana", "@valor_aduana", "F");
            Ins.Add("valor_fob", "@valor_fob", "F");
            Ins.Add("valor_iva", "@valor_iva", "F");
            Ins.Add("valor_dai", "@valor_dai", "F");
            Ins.Add("valor_seguro", "@valor_seguro", "F");
            Ins.Add("valor_flete", "@valor_flete", "F");
            Ins.Add("peso_neto", "@peso_neto", "F");
            Ins.Add("peso_bruto", "@peso_bruto", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("nombre_propietario", "@nombre_propietario", "F");
            Ins.Add("idordencompradetpadre", "@idordencompradetpadre", "F");
            Ins.Add("idembarcador", "@idembarcador", "F");

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

            BindParameters(cmd, oBeTrans_oc_det);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_oc_det");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("idordencompradet", "@idordencompradet", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idarancel", "@idarancel", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("nombre_producto", "@nombre_producto", "F");
            Upd.Add("nombre_presentacion", "@nombre_presentacion", "F");
            Upd.Add("nombre_arancel", "@nombre_arancel", "F");
            Upd.Add("porcentaje_arancel", "@porcentaje_arancel", "F");
            Upd.Add("nombre_unidad_medida_basica", "@nombre_unidad_medida_basica", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("costo", "@costo", "F");
            Upd.Add("total_linea", "@total_linea", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("peso_recibido", "@peso_recibido", "F");
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("codigo_producto", "@codigo_producto", "F");
            Upd.Add("valor_aduana", "@valor_aduana", "F");
            Upd.Add("valor_fob", "@valor_fob", "F");
            Upd.Add("valor_iva", "@valor_iva", "F");
            Upd.Add("valor_dai", "@valor_dai", "F");
            Upd.Add("valor_seguro", "@valor_seguro", "F");
            Upd.Add("valor_flete", "@valor_flete", "F");
            Upd.Add("peso_neto", "@peso_neto", "F");
            Upd.Add("peso_bruto", "@peso_bruto", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("nombre_propietario", "@nombre_propietario", "F");
            Upd.Add("idordencompradetpadre", "@idordencompradetpadre", "F");
            Upd.Add("idembarcador", "@idembarcador", "F");
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc AND IdOrdenCompraDet = @IdOrdenCompraDet");

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

            BindParameters(cmd, oBeTrans_oc_det);

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

    public int Eliminar(IConfiguration config, clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_oc_det" +
             "  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)" +
             "  And (IdOrdenCompraDet = @IdOrdenCompraDet)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", oBeTrans_oc_det.IdOrdenCompraEnc));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_det pBeTrans_oc_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_oc_det" +
            " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)" +
            " And (IdOrdenCompraDet = @IdOrdenCompraDet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", pBeTrans_oc_det.IdOrdenCompraEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", pBeTrans_oc_det.IdOrdenCompraDet));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_oc_det, r);
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

    public static List<clsBeTrans_oc_det> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_det> lreturnList = new List<clsBeTrans_oc_det>();

        try
        {
            const string sp = "Select * FROM Trans_oc_det ";

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

                        clsBeTrans_oc_det vBeTrans_oc_det = new clsBeTrans_oc_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_det = new clsBeTrans_oc_det();
                            Cargar(ref vBeTrans_oc_det, dr);
                            lreturnList.Add(vBeTrans_oc_det);
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

            const string sp = "Select ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_det";

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


            const string sp = "Select ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_det";

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
    public static void BindParameters(SqlCommand cmd, dynamic oBeTrans_oc_det)
    {
        void AddParam(string name, object value) => cmd.Parameters.Add(new SqlParameter(name, value ?? DBNull.Value));
        object NullIfZero(object value) => (value is int intValue && intValue == 0) ? DBNull.Value : value;

        AddParam("@IdOrdenCompraEnc", NullIfZero(oBeTrans_oc_det.IdOrdenCompraEnc));
        AddParam("@IdOrdenCompraDet", NullIfZero(oBeTrans_oc_det.IdOrdenCompraDet));
        AddParam("@IdProductoBodega", NullIfZero(oBeTrans_oc_det.IdProductoBodega));
        AddParam("@IdArancel", NullIfZero(oBeTrans_oc_det.IdArancel));
        AddParam("@IdPresentacion", NullIfZero(oBeTrans_oc_det.IdPresentacion));
        AddParam("@IdUnidadMedidaBasica", NullIfZero(oBeTrans_oc_det.IdUnidadMedidaBasica));
        AddParam("@IdMotivoDevolucion", NullIfZero(oBeTrans_oc_det.IdMotivoDevolucion));
        AddParam("@No_Linea", oBeTrans_oc_det.No_Linea);
        AddParam("@nombre_producto", oBeTrans_oc_det.Nombre_producto);
        AddParam("@nombre_presentacion", oBeTrans_oc_det.Nombre_presentacion);
        AddParam("@nombre_arancel", oBeTrans_oc_det.Nombre_arancel);
        AddParam("@porcentaje_arancel", oBeTrans_oc_det.Porcentaje_arancel);
        AddParam("@nombre_unidad_medida_basica", oBeTrans_oc_det.Nombre_unidad_medida_basica);
        AddParam("@cantidad", oBeTrans_oc_det.Cantidad);
        AddParam("@cantidad_recibida", oBeTrans_oc_det.Cantidad_recibida);
        AddParam("@costo", oBeTrans_oc_det.Costo);
        AddParam("@total_linea", oBeTrans_oc_det.Total_linea);
        AddParam("@user_agr", oBeTrans_oc_det.User_agr);
        AddParam("@fec_agr", oBeTrans_oc_det.Fec_agr);
        AddParam("@user_mod", oBeTrans_oc_det.User_mod);
        AddParam("@fec_mod", oBeTrans_oc_det.Fec_mod);
        AddParam("@activo", oBeTrans_oc_det.Activo);
        AddParam("@peso", oBeTrans_oc_det.Peso);
        AddParam("@peso_recibido", oBeTrans_oc_det.Peso_recibido);
        AddParam("@atributo_variante_1", oBeTrans_oc_det.Atributo_variante_1);
        AddParam("@codigo_producto", oBeTrans_oc_det.Codigo_producto);
        AddParam("@valor_aduana", oBeTrans_oc_det.Valor_aduana);
        AddParam("@valor_fob", oBeTrans_oc_det.Valor_fob);
        AddParam("@valor_iva", oBeTrans_oc_det.Valor_iva);
        AddParam("@valor_dai", oBeTrans_oc_det.Valor_dai);
        AddParam("@valor_seguro", oBeTrans_oc_det.Valor_seguro);
        AddParam("@valor_flete", oBeTrans_oc_det.Valor_flete);
        AddParam("@peso_neto", oBeTrans_oc_det.Peso_neto);
        AddParam("@peso_bruto", oBeTrans_oc_det.Peso_bruto);
        AddParam("@IdPropietarioBodega", NullIfZero(oBeTrans_oc_det.IdPropietarioBodega));
        AddParam("@nombre_propietario", oBeTrans_oc_det.Nombre_propietario);
        AddParam("@IdOrdenCompraDetPadre", NullIfZero(oBeTrans_oc_det.IdOrdenCompraDetPadre));
        AddParam("@IdEmbarcador", NullIfZero(oBeTrans_oc_det.IdEmbarcador));
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeTrans_oc_det> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            foreach (var entity in entities)
            {
                //GT08072025: borrar comentario... se valida no solo el iddet sino tambien la oc asociada.
                bool existe = Existe(entity.IdOrdenCompraDet, entity.IdOrdenCompraEnc, connection, isExternalTx ? tx! : localTx!);

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
    public static bool Existe(int IdOrdenCompraDet,int IdOrdenCompraEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Trans_oc_det WHERE IdOrdenCompraDet = @IdOrdenCompraDet and IdOrdenCompraEnc=@IdOrdenCompraEnc";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", IdOrdenCompraDet));
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

    public static List<clsBeTrans_oc_det> Get_All_By_IdOrdenCompraEnc(IConfiguration config, int IdOrdenCompraEnc)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_oc_det> lreturnList = new List<clsBeTrans_oc_det>();

        try
        {
            const string sp = "Select * FROM Trans_oc_det WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";
            
            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {

                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc);

                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeTrans_oc_det vBeTrans_oc_det = new clsBeTrans_oc_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_oc_det = new clsBeTrans_oc_det();
                            Cargar(ref vBeTrans_oc_det, dr);
                            lreturnList.Add(vBeTrans_oc_det);
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