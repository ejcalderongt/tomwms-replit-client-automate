using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Trans_oc;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Trans_re;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;
using WMS.DALCore.Trans_oc;
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
    public static int Insertar(clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTrans_oc_det == null)
            throw new ArgumentNullException(nameof(oBeTrans_oc_det));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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
            Ins.Add("camas_tarima", "@camas_tarima", "F");
            Ins.Add("cajas_cama", "@cajas_cama", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindParameters(cmd, oBeTrans_oc_det);

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
    public static int Actualizar(clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTrans_oc_det == null)
            throw new ArgumentNullException(nameof(oBeTrans_oc_det));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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
            Upd.Add("camas_tarima", "@camas_tarima", "F");
            Upd.Add("cajas_cama", "@cajas_cama", "F");
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc AND IdOrdenCompraDet = @IdOrdenCompraDet");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindParameters(cmd, oBeTrans_oc_det);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Actualizar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
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
    public static int MaxID(SqlConnection? pConnection = null, SqlTransaction? pTransaction = null)
    {
        int lMax = 0;

        try
        {
            const string sql = "SELECT ISNULL(MAX(IdOrdenCompraEnc), 0) FROM Trans_oc_det";

            bool esTransaccionRemota = pConnection is not null && pTransaction is not null;

            SqlCommand? cmd=null;

            if (esTransaccionRemota)
            {
                cmd = new SqlCommand(sql, pConnection, pTransaction);
            }

            object lReturnValue = 0;
            if (cmd != null)
                lReturnValue= cmd.ExecuteScalar();

            if (lReturnValue != null && lReturnValue != DBNull.Value)
                lMax = Convert.ToInt32(lReturnValue);            

            return lMax;
        }
        catch (SqlException ex1)
        {            
            string vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} SQL Error: {ex1.Message}";
            throw new Exception(vMsgError, ex1);
        }       
    }
    public static void BindParameters(SqlCommand cmd, clsBeTrans_oc_det oBeTrans_oc_det)
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
        AddParam("@camas_tarima", NullIfZero(oBeTrans_oc_det.Camas_Tarima));
        AddParam("@cajas_cama", NullIfZero(oBeTrans_oc_det.Cajas_Cama));
    }
    public static void InsertarOActualizar(List<clsBeTrans_oc_det> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                bool existe = Existe(entity.IdOrdenCompraDet, entity.IdOrdenCompraEnc, conn, tx);

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

    public static clsBeTrans_oc_det? Exist(
       int pOCEncabezado,
       int pNoLinea,
       SqlConnection pConnection,
       SqlTransaction pTransaction)
    {
        clsBeTrans_oc_det? result = null;

        try
        {
            const string sp = @"
                SELECT * 
                FROM trans_oc_det 
                WHERE IdOrdenCompraEnc = @pOCEncabezado 
                  AND No_Linea = @No_Linea";

            using (var cmd = new SqlCommand(sp, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@pOCEncabezado", pOCEncabezado));
                cmd.Parameters.Add(new SqlParameter("@No_Linea", pNoLinea));

                using (var dad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        DataRow lRow = dt.Rows[0];
                        var objUM = new clsBeTrans_oc_det();
                        Cargar(ref objUM, lRow);
                        result = objUM;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string vMsgError = ex.Message;            
            throw;
        }

        return result;
    }
    public static int MaxID(int pIdOrdenCompraEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            int lMax = 0;
            string sql = $"SELECT ISNULL(MAX(IdOrdenCompraDet), 0) FROM trans_oc_det WHERE IdOrdenCompraEnc = {pIdOrdenCompraEnc}";

            using (var cmd = new SqlCommand(sql, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                object? result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    lMax = Convert.ToInt32(result);
            }

            return lMax;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(MaxID)} → {ex.Message}", ex);
        }
    }

    public static int Actualizar_Desde_Interface(ref clsBeTrans_oc_det oBeTrans_oc_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        try
        {
            Upd.Init("trans_oc_det");
            Upd.Add("idordencompradet", "@idordencompradet", "F");

            // EJC_18092016
            if (oBeTrans_oc_det.Arancel != null)
            {
                Ins.Add("idarancel", "@idarancel", "F");
            }

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
            Upd.Add("peso", "@peso", "F");
            Upd.Add("peso_recibido", "@peso_recibido", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("camas_tarima", "@camas_tarima", "F");
            Upd.Add("cajas_cama", "@cajas_cama", "F");

            if (oBeTrans_oc_det.Atributo_variante_1 != null)
            {
                Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            }

            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc " +
                "AND IdOrdenCompraDet = @IdOrdenCompraDet");

            string sp = Upd.SQL();
            SqlCommand cmd = new SqlCommand(sp, pConection)
            {
                CommandType = CommandType.Text,
                Transaction = pTransaction
            };

            cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc));
            cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet));
            cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det.IdProductoBodega));

            // ejc_18092016: object_reference throws when nothing value is in arancel
            if (oBeTrans_oc_det.Arancel != null)
            {
                cmd.Parameters.Add(new SqlParameter("@IDARANCEL", oBeTrans_oc_det.Arancel.IdArancel == 0 ? DBNull.Value : (object)oBeTrans_oc_det.Arancel.IdArancel));
            }

            cmd.Parameters.Add(new SqlParameter("@IDPRESENTACION", oBeTrans_oc_det.Presentacion.IdPresentacion == 0 ? DBNull.Value : (object)oBeTrans_oc_det.Presentacion.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_oc_det.UnidadMedida.IdUnidadMedida == 0 ? DBNull.Value : (object)oBeTrans_oc_det.UnidadMedida.IdUnidadMedida));
            cmd.Parameters.Add(new SqlParameter("@IDMOTIVODEVOLUCION", oBeTrans_oc_det.IdMotivoDevolucion == 0 ? DBNull.Value : (object)oBeTrans_oc_det.IdMotivoDevolucion));
            cmd.Parameters.Add(new SqlParameter("@NO_LINEA", oBeTrans_oc_det.No_Linea == 0 ? oBeTrans_oc_det.No_Linea : oBeTrans_oc_det.No_Linea));
            cmd.Parameters.Add(new SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_oc_det.Nombre_producto ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@NOMBRE_PRESENTACION", oBeTrans_oc_det.Nombre_presentacion ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@NOMBRE_ARANCEL", oBeTrans_oc_det.Nombre_arancel ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@PORCENTAJE_ARANCEL", oBeTrans_oc_det.Porcentaje_arancel));
            cmd.Parameters.Add(new SqlParameter("@NOMBRE_UNIDAD_MEDIDA_BASICA", oBeTrans_oc_det.Nombre_unidad_medida_basica ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@CANTIDAD", oBeTrans_oc_det.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida));
            cmd.Parameters.Add(new SqlParameter("@COSTO", oBeTrans_oc_det.Costo));
            cmd.Parameters.Add(new SqlParameter("@TOTAL_LINEA", oBeTrans_oc_det.Total_linea));
            cmd.Parameters.Add(new SqlParameter("@PESO", oBeTrans_oc_det.Peso));
            cmd.Parameters.Add(new SqlParameter("@PESO_RECIBIDO", oBeTrans_oc_det.Peso_recibido));
            cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeTrans_oc_det.User_agr));
            cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeTrans_oc_det.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeTrans_oc_det.User_mod));
            cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeTrans_oc_det.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeTrans_oc_det.Activo));
            cmd.Parameters.Add(new SqlParameter("@camas_tarima", oBeTrans_oc_det.Camas_Tarima));
            cmd.Parameters.Add(new SqlParameter("@cajas_cama", oBeTrans_oc_det.Cajas_Cama));


            if (oBeTrans_oc_det.Atributo_variante_1 != null)
            {
                cmd.Parameters.Add(new SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_oc_det.Atributo_variante_1));
            }

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeTrans_oc_det? Get_Single_By_IdOrdenCompraEnc_And_Linea(int IdOrdenCompraEnc,
                                                                        int No_Linea,
                                                                        int IdProductoBodega,
                                                                        SqlConnection lConnection,
                                                                        SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Trans_oc_det 
                            WHERE (IdOrdenCompraEnc = @IdOrdenCompraEnc
                            AND No_Linea = @NO_LINEA 
                            AND IdProductoBodega = @IdProductoBodega)";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@NO_LINEA", No_Linea));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoBodega", IdProductoBodega));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                clsBeTrans_oc_det pBeTrans_oc_det = new clsBeTrans_oc_det();
                Cargar(ref pBeTrans_oc_det, dt.Rows[0]);
                return pBeTrans_oc_det;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Actualiza_Cantidad_Recibida_OC(clsBeTrans_re_oc pRecOrdenCompra,
                                                    List<clsBeTrans_re_det>? pListRecDet,
                                                    SqlConnection lConnection,
                                                    SqlTransaction lTransaction)
    {
        int Actualiza_Cantidad_Recibida_OC = 0;
        clsBeProducto_presentacion? BePresentacion = new clsBeProducto_presentacion();
        clsBeTrans_oc_det? BeTransOcDet = new clsBeTrans_oc_det();

        try
        {
            if (pRecOrdenCompra != null)
            {
                double CantidadRecibidaActual = 0;

                if (pListRecDet != null)
                {
                    foreach (clsBeTrans_re_det BeTransReDet in pListRecDet)
                    {
                        if (pRecOrdenCompra.IdOrdenCompraEnc > 0)
                        {
                            CantidadRecibidaActual = clsLnTrans_re_enc.Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet.IdRecepcionEnc,
                                                                                                                                         BeTransReDet.IdRecepcionDet,
                                                                                                                                         lConnection,
                                                                                                                                         lTransaction);

                            if (BeTransReDet.IdPresentacion == 0)
                            {
                                BeTransReDet.IdPresentacion = -1;
                            }

                            BeTransOcDet = Get_Detalle_By_IdOrdenCompraEnc(pRecOrdenCompra.IdOrdenCompraEnc,
                                                                           BeTransReDet.IdProductoBodega,
                                                                           BeTransReDet.IdPresentacion,
                                                                           BeTransReDet.No_Linea,
                                                                           BeTransReDet.IdOrdenCompraDet,
                                                                           lConnection,
                                                                           lTransaction);

                            if (BeTransOcDet != null)
                            {
                                if (BeTransOcDet.IdOrdenCompraDet > 0)
                                {                                    
                                    if (BeTransReDet.IsNew)
                                    {
                                        if (BeTransReDet.IdPresentacion == 0 || BeTransReDet.IdPresentacion == -1)
                                        {
                                            if (BeTransOcDet.IdPresentacion == 0)
                                            {
                                                BeTransOcDet.Cantidad_recibida += BeTransReDet.Cantidad_recibida;
                                            }
                                            else
                                            {
                                                BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                                      lConnection,
                                                                                                      lTransaction);

                                                if (BePresentacion != null)
                                                {
                                                    if (BePresentacion.Factor > 0)
                                                    {
                                                        BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.Cantidad_recibida / BePresentacion.Factor, 6);
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (BeTransOcDet.IdPresentacion != 0)
                                            {
                                                BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                                      lConnection,
                                                                                                      lTransaction);

                                                if (BePresentacion != null)
                                                {
                                                    if (BePresentacion.Factor > 0)
                                                    {
                                                        BeTransOcDet.Cantidad_recibida += BeTransReDet.Cantidad_recibida;
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.");
                                                }
                                            }
                                            else
                                            {
                                                BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.Cantidad_recibida * BePresentacion.Factor, 6);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        BeTransOcDet.Cantidad_recibida = BeTransOcDet.Cantidad_recibida - CantidadRecibidaActual;
                                        BeTransOcDet.Cantidad_recibida += BeTransReDet.Cantidad_recibida;
                                    }

                                    Actualiza_Cantidad_Recibida_OC = Actualizar_Cantidad_Recibida(BeTransOcDet,
                                                                                                  lConnection,
                                                                                                  lTransaction);
                                }
                            }
                            else
                            {
                                throw new Exception("ERROR_202210051048: No se obtuvo el objeto de detalle del documento de ingreso, no se podrá actualizar la cantidad recibida.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {            
            throw;
        }

        return Actualiza_Cantidad_Recibida_OC;
    }

    public static int Actualizar_Cantidad_Recibida(clsBeTrans_oc_det oBeTrans_oc_det,
                                                  SqlConnection pConection,
                                                  SqlTransaction pTransaction)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            string sp = @"UPDATE trans_oc_det 
                     SET cantidad_recibida = @CANTIDAD_RECIBIDA 
                     WHERE IdOrdenCompraEnc = @IDORDENCOMPRAENC 
                     AND IdOrdenCompraDet = @IDORDENCOMPRADET";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sp;
            cmd.Connection = pConection;
            cmd.Transaction = pTransaction;

            cmd.Parameters.AddWithValue("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc);
            cmd.Parameters.AddWithValue("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet);
            cmd.Parameters.AddWithValue("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida);

            return cmd.ExecuteNonQuery();
        }
    }

    public static clsBeTrans_oc_det? Get_Detalle_By_IdOrdenCompraEnc(int pIdOrdenCompraEnc,
                                                               int pIdProductoBodega,
                                                               int pIdPresentacion,
                                                               int pNoLinea,
                                                               int pIdOrdenCompraDet,
                                                               SqlConnection pConnection,
                                                               SqlTransaction pTransaction)
    {
        clsBeTrans_oc_det? BeTransOCDet = null;

        string vSQL = "";

        if (pIdPresentacion == 0)
        {
            vSQL = @"SELECT * FROM trans_oc_det 
                WHERE IdOrdenCompraenc = @IdOrdenCompraenc 
                And IdProductoBodega = @IdProductoBodega
                And IdPresentacion IS NULL
                And No_Linea = @NoLinea
                And IdOrdenCompraDet = @IdOrdenCompraDet";
        }
        else if (pIdPresentacion == -1)
        {
            vSQL = @"SELECT *
                FROM trans_oc_det 
                WHERE IdOrdenCompraenc = @IdOrdenCompraenc 
                And IdProductoBodega = @IdProductoBodega                        
                And No_Linea = @NoLinea 
                And IdOrdenCompraDet = @IdOrdenCompraDet";
        }
        else
        {
            vSQL = @"SELECT *
                FROM trans_oc_det 
                WHERE (IdOrdenCompraenc=@IdOrdenCompraenc 
                And IdProductoBodega=@IdProductoBodega 
                And IdPresentacion = @IdPresentacion)
                And No_Linea = @NoLinea
                And IdOrdenCompraDet = @IdOrdenCompraDet";
        }

        using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
        {
            lDTA.SelectCommand.CommandType = CommandType.Text;
            lDTA.SelectCommand.Transaction = pTransaction;
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraenc", pIdOrdenCompraEnc);
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);
            lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", pNoLinea);
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet);

            if (pIdPresentacion != 0 && pIdPresentacion != -1)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion);

            DataTable lDataTable = new DataTable();
            lDTA.Fill(lDataTable);

            if (lDataTable != null && lDataTable.Rows.Count > 0)
            {
                DataRow lRow = lDataTable.Rows[0];
                BeTransOCDet = new clsBeTrans_oc_det();

                Cargar(ref BeTransOCDet, lRow);

                if (lRow["IdArancel"] != DBNull.Value && lRow["IdArancel"] != null)
                    BeTransOCDet.Arancel.IdArancel = Convert.ToInt32(lRow["IdArancel"]);

                if (lRow["IdPresentacion"] != DBNull.Value && lRow["IdPresentacion"] != null)
                    BeTransOCDet.Presentacion.IdPresentacion = Convert.ToInt32(lRow["IdPresentacion"]);

                if (lRow["IdUnidadMedidaBasica"] != DBNull.Value && lRow["IdUnidadMedidaBasica"] != null)
                    BeTransOCDet.UnidadMedida.IdUnidadMedida = Convert.ToInt32(lRow["IdUnidadMedidaBasica"]);

                BeTransOCDet.IsNew = false;
                return BeTransOCDet;
            }
            else if (pIdPresentacion != 0 && pIdPresentacion != -1)
            {
                BeTransOCDet = Get_Detalle_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, pIdProductoBodega, 0, pNoLinea, pIdOrdenCompraDet, pConnection, pTransaction);

                if (BeTransOCDet == null)
                {
                    BeTransOCDet = Get_Detalle_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, pIdProductoBodega, -1, pNoLinea, pIdOrdenCompraDet, pConnection, pTransaction);
                }

                return BeTransOCDet;
            }
        }

        return null;
    }

    public static List<clsBeTrans_oc_det> Get_Detalle_OC_By_IdOrdenCompraEnc(int pIdOrdenCompraEnc,
                                                                        SqlConnection lConnection,
                                                                        SqlTransaction lTransaction)
    {
        List<clsBeTrans_oc_det> lReturnList = new List<clsBeTrans_oc_det>();

        try
        {
            string vSQL = @"SELECT p.IdProducto, det.IdOrdenCompraEnc, det.IdOrdenCompraDet, det.IdProductoBodega, det.IdArancel, det.IdPresentacion, 
                               det.IdUnidadMedidaBasica, det.IdMotivoDevolucion, det.No_Linea, det.nombre_producto, det.nombre_presentacion, 
                               det.nombre_arancel, det.porcentaje_arancel, det.nombre_unidad_medida_basica, det.cantidad, det.cantidad_recibida, 
                               det.costo, det.total_linea, det.user_agr, det.fec_agr, det.user_mod, det.fec_mod, det.activo, det.peso, 
                               det.peso_recibido, det.atributo_variante_1,	   
                               det.codigo_producto,	   
                               det.valor_aduana, det.valor_fob, det.valor_iva, 
                               det.valor_dai, det.valor_seguro, det.valor_flete, det.peso_neto, det.peso_bruto, det.IdPropietarioBodega, 
                               det.nombre_propietario, det.IdOrdenCompraDetPadre, det.IdEmbarcador, det.IdProductoTallaColor
                        FROM trans_oc_enc as enc  
                        INNER JOIN trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc 
                        INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega 
                        INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto  
                        WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeTrans_oc_det BeTransOcDet = new clsBeTrans_oc_det();
                        Cargar(ref BeTransOcDet, lRow);

                        if (lRow["IdProducto"] != DBNull.Value)
                        {
                            BeTransOcDet.Producto.IdProducto = Convert.ToInt32(lRow["IdProducto"]);
                            clsLnProducto.Obtener(BeTransOcDet.Producto, lConnection, lTransaction);
                        }

                        if (BeTransOcDet.Producto.IdClasificacion != 0)
                        {
                            var clasificacion = clsLnProducto_clasificacion.GetSingle(BeTransOcDet.Producto.IdClasificacion,
                                                                                    lConnection,
                                                                                    lTransaction);
                            if (clasificacion != null)
                            {
                                BeTransOcDet.Producto.Clasificacion = clasificacion;
                            }
                        }

                        if (lRow["IdProductoBodega"] != DBNull.Value)
                        {
                            BeTransOcDet.IdProductoBodega = Convert.ToInt32(lRow["IdProductoBodega"]);
                        }

                        if (lRow["IdPresentacion"] != DBNull.Value)
                        {
                            BeTransOcDet.Presentacion.IdPresentacion = Convert.ToInt32(lRow["IdPresentacion"]);
                            clsLnProducto_presentacion.Obtener(BeTransOcDet.Presentacion, lConnection, lTransaction);
                        }

                        if (lRow["IdUnidadMedidaBasica"] != DBNull.Value)
                        {
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = Convert.ToInt32(lRow["IdUnidadMedidaBasica"]);
                            clsLnUnidad_medida.Obtener(BeTransOcDet.UnidadMedida, lConnection, lTransaction);
                        }

                        if (lRow["IdMotivoDevolucion"] != DBNull.Value)
                        {
                            BeTransOcDet.IdMotivoDevolucion = Convert.ToInt32(lRow["IdMotivoDevolucion"]);
                        }

                        BeTransOcDet.IsNew = false;

                        if (BeTransOcDet.IdEmbarcador != 0)
                        {
                            clsBeTrans_oc_embarcador? pBeTrans_oc_embarcador = clsLnTrans_oc_embarcador.Get_Single_By_IdEmbarcador(BeTransOcDet.IdEmbarcador,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction);
                            if (pBeTrans_oc_embarcador != null)
                            {
                                BeTransOcDet.Nombre_Embarcador = pBeTrans_oc_embarcador.Nombre ?? string.Empty;
                            }
                        }

                        if (BeTransOcDet.IdProductoTallaColor != 0)
                        {
                            clsBeProducto_talla_color? BeProductoTallaColor = clsLnProducto_talla_color.GetSingle(BeTransOcDet.IdProductoTallaColor,
                                                                                                                  lConnection,
                                                                                                                  lTransaction);
                            if (BeProductoTallaColor != null)
                            {
                                var vTalla = clsLnTalla.GetSingle(BeProductoTallaColor.IdTalla, lConnection, lTransaction);
                                if (vTalla != null)
                                    BeTransOcDet.Talla = vTalla;

                                        var vColor = clsLnColor.GetSingle(BeProductoTallaColor.IdColor, lConnection, lTransaction);
                                if (vColor != null)
                                    BeTransOcDet.Color = vColor;
                            }
                        }

                        if (BeTransOcDet.IdOrdenCompraDetPadre != 0)
                        {
                            clsBeTrans_oc_det? ObjPadre = lReturnList.Find(x => x.IdProductoBodega == BeTransOcDet.IdOrdenCompraDetPadre);
                            if (ObjPadre != null)
                            {
                                ObjPadre.lProductosHijosKit.Add(BeTransOcDet);
                            }
                        }
                        else
                        {
                            lReturnList.Add(BeTransOcDet);
                        }
                    }
                }
            }

            return lReturnList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string Get_Cod_Variante_Nav(int pIdOrdenCompraEnc,
                                              int pNo_Linea,
                                              SqlConnection lConnection,
                                              SqlTransaction lTransaction)
    {
        string result = string.Empty;

        try
        {
            string vSQL = @"SELECT atributo_variante_1 FROM trans_oc_det                         
                        WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc 
                        AND No_Linea = @No_Linea";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@No_Linea", pNo_Linea);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    result = (lDataTable.Rows[0]["atributo_variante_1"] == DBNull.Value) ? string.Empty : Convert.ToString(lDataTable.Rows[0]["atributo_variante_1"]) ?? string.Empty;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
}