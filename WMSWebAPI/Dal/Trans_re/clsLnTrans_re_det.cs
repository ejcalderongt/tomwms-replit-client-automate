using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMSWebAPI.Be;

public class clsLnTrans_re_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_det oBeTrans_re_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_re_det.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeTrans_re_det.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_det.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_re_det.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_re_det.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeTrans_re_det.IdProductoEstado = GetInt("IdProductoEstado");
            oBeTrans_re_det.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeTrans_re_det.IdMotivoDevolucion = GetInt("IdMotivoDevolucion");
            oBeTrans_re_det.No_Linea = GetInt("No_Linea");
            oBeTrans_re_det.Cantidad_recibida = GetDouble("cantidad_recibida");
            oBeTrans_re_det.Nombre_producto = GetString("nombre_producto");
            oBeTrans_re_det.Nombre_presentacion = GetString("nombre_presentacion");
            oBeTrans_re_det.Nombre_unidad_medida = GetString("nombre_unidad_medida");
            oBeTrans_re_det.Nombre_producto_estado = GetString("nombre_producto_estado");
            oBeTrans_re_det.Lote = GetString("lote");
            oBeTrans_re_det.Fecha_vence = GetDate("fecha_vence");
            oBeTrans_re_det.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeTrans_re_det.Peso = GetDouble("peso");
            oBeTrans_re_det.Peso_estadistico = GetDouble("peso_estadistico");
            oBeTrans_re_det.Peso_minimo = GetDouble("peso_minimo");
            oBeTrans_re_det.Peso_maximo = GetDouble("peso_maximo");
            oBeTrans_re_det.Peso_unitario = GetDouble("peso_unitario");
            oBeTrans_re_det.User_agr = GetString("user_agr");
            oBeTrans_re_det.Fec_agr = GetDate("fec_agr");
            oBeTrans_re_det.Observacion = GetString("observacion");
            oBeTrans_re_det.Añada = GetInt("añada");
            oBeTrans_re_det.Costo = GetDouble("costo");
            oBeTrans_re_det.Costo_oc = GetDouble("costo_oc");
            oBeTrans_re_det.Costo_estadistico = GetDouble("costo_estadistico");
            oBeTrans_re_det.Atributo_variante_1 = GetString("atributo_variante_1");
            oBeTrans_re_det.Codigo_producto = GetString("codigo_producto");
            oBeTrans_re_det.Lic_plate = GetString("lic_plate");
            oBeTrans_re_det.Pallet_no_estandar = GetBool("pallet_no_estandar");
            oBeTrans_re_det.IdOrdenCompraEnc = GetInt("IdOrdenCompraEnc");
            oBeTrans_re_det.IdOrdenCompraDet = GetInt("IdOrdenCompraDet");
            oBeTrans_re_det.IdJornadaSistema = GetInt("IdJornadaSistema");
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

    public static int Insertar(IConfiguration config, clsBeTrans_re_det oBeTrans_re_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_det");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nombre_presentacion", "@nombre_presentacion", "F");
            Ins.Add("nombre_unidad_medida", "@nombre_unidad_medida", "F");
            Ins.Add("nombre_producto_estado", "@nombre_producto_estado", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("peso_estadistico", "@peso_estadistico", "F");
            Ins.Add("peso_minimo", "@peso_minimo", "F");
            Ins.Add("peso_maximo", "@peso_maximo", "F");
            Ins.Add("peso_unitario", "@peso_unitario", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("costo_oc", "@costo_oc", "F");
            Ins.Add("costo_estadistico", "@costo_estadistico", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idordencompradet", "@idordencompradet", "F");
            Ins.Add("idjornadasistema", "@idjornadasistema", "F");

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

            Bind(cmd, oBeTrans_re_det);

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

    public static int Insertar(IConfiguration config, clsBeTrans_re_det oBeTrans_re_det)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_det");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nombre_presentacion", "@nombre_presentacion", "F");
            Ins.Add("nombre_unidad_medida", "@nombre_unidad_medida", "F");
            Ins.Add("nombre_producto_estado", "@nombre_producto_estado", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("peso_estadistico", "@peso_estadistico", "F");
            Ins.Add("peso_minimo", "@peso_minimo", "F");
            Ins.Add("peso_maximo", "@peso_maximo", "F");
            Ins.Add("peso_unitario", "@peso_unitario", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("costo_oc", "@costo_oc", "F");
            Ins.Add("costo_estadistico", "@costo_estadistico", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Ins.Add("idordencompraenc", "@idordencompraenc", "F");
            Ins.Add("idordencompradet", "@idordencompradet", "F");
            Ins.Add("idjornadasistema", "@idjornadasistema", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);
            Bind(cmd, oBeTrans_re_det);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_re_det oBeTrans_re_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_re_det");
            Upd.Add("idrecepciondet", "@idrecepciondet", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("nombre_producto", "@nombre_producto", "F");
            Upd.Add("nombre_presentacion", "@nombre_presentacion", "F");
            Upd.Add("nombre_unidad_medida", "@nombre_unidad_medida", "F");
            Upd.Add("nombre_producto_estado", "@nombre_producto_estado", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("peso_estadistico", "@peso_estadistico", "F");
            Upd.Add("peso_minimo", "@peso_minimo", "F");
            Upd.Add("peso_maximo", "@peso_maximo", "F");
            Upd.Add("peso_unitario", "@peso_unitario", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Add("añada", "@añada", "F");
            Upd.Add("costo", "@costo", "F");
            Upd.Add("costo_oc", "@costo_oc", "F");
            Upd.Add("costo_estadistico", "@costo_estadistico", "F");
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("codigo_producto", "@codigo_producto", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Upd.Add("idordencompraenc", "@idordencompraenc", "F");
            Upd.Add("idordencompradet", "@idordencompradet", "F");
            Upd.Add("idjornadasistema", "@idjornadasistema", "F");
            Upd.Where("IdRecepcionDet = @IdRecepcionDet AND IdRecepcionEnc = @IdRecepcionEnc");

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

            Bind(cmd, oBeTrans_re_det);

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

    public int Eliminar(IConfiguration config, clsBeTrans_re_det oBeTrans_re_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_det" +
             "  Where(IdRecepcionDet = @IdRecepcionDet)" +
             "  And (IdRecepcionEnc = @IdRecepcionEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", oBeTrans_re_det.IdRecepcionDet));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_det pBeTrans_re_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_det" +
            " Where(IdRecepcionDet = @IdRecepcionDet)" +
            " And (IdRecepcionEnc = @IdRecepcionEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionDet", pBeTrans_re_det.IdRecepcionDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeTrans_re_det.IdRecepcionEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_det, r);
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

    public static List<clsBeTrans_re_det> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_det> lreturnList = new List<clsBeTrans_re_det>();

        try
        {
            const string sp = "Select * FROM Trans_re_det";

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

                        clsBeTrans_re_det vBeTrans_re_det = new clsBeTrans_re_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_det = new clsBeTrans_re_det();
                            Cargar(ref vBeTrans_re_det, dr);
                            lreturnList.Add(vBeTrans_re_det);
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

            const string sp = "Select ISNULL(Max(IdRecepcionDet),0) FROM Trans_re_det";

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


            const string sp = "Select ISNULL(Max(IdRecepcionDet),0) FROM Trans_re_det";

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

    public static void Bind(SqlCommand cmd, clsBeTrans_re_det o)
    {
        object NullIfZero(int value) => value == 0 ? DBNull.Value : value;
        object NullIfEmpty(string? value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

        cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", NullIfZero(o.IdRecepcionDet)));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", NullIfZero(o.IdRecepcionEnc)));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", NullIfZero(o.IdProductoBodega)));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", NullIfZero(o.IdPresentacion)));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", NullIfZero(o.IdUnidadMedida)));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", NullIfZero(o.IdProductoEstado)));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", NullIfZero(o.IdOperadorBodega)));
        cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", NullIfZero(o.IdMotivoDevolucion)));
        cmd.Parameters.Add(new SqlParameter("@No_Linea", o.No_Linea));
        cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", o.Cantidad_recibida));
        cmd.Parameters.Add(new SqlParameter("@nombre_producto", NullIfEmpty(o.Nombre_producto)));
        cmd.Parameters.Add(new SqlParameter("@nombre_presentacion", NullIfEmpty(o.Nombre_presentacion)));
        cmd.Parameters.Add(new SqlParameter("@nombre_unidad_medida", NullIfEmpty(o.Nombre_unidad_medida)));
        cmd.Parameters.Add(new SqlParameter("@nombre_producto_estado", NullIfEmpty(o.Nombre_producto_estado)));
        cmd.Parameters.Add(new SqlParameter("@lote", NullIfEmpty(o.Lote)));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence", o.Fecha_vence));
        cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", o.Fecha_ingreso));
        cmd.Parameters.Add(new SqlParameter("@peso", o.Peso));
        cmd.Parameters.Add(new SqlParameter("@peso_estadistico", o.Peso_estadistico));
        cmd.Parameters.Add(new SqlParameter("@peso_minimo", o.Peso_minimo));
        cmd.Parameters.Add(new SqlParameter("@peso_maximo", o.Peso_maximo));
        cmd.Parameters.Add(new SqlParameter("@peso_unitario", o.Peso_unitario));
        cmd.Parameters.Add(new SqlParameter("@user_agr", NullIfEmpty(o.User_agr)));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@observacion", NullIfEmpty(o.Observacion)));
        cmd.Parameters.Add(new SqlParameter("@añada", NullIfZero(o.Añada)));
        cmd.Parameters.Add(new SqlParameter("@costo", o.Costo));
        cmd.Parameters.Add(new SqlParameter("@costo_oc", o.Costo_oc));
        cmd.Parameters.Add(new SqlParameter("@costo_estadistico", o.Costo_estadistico));
        cmd.Parameters.Add(new SqlParameter("@atributo_variante_1", NullIfEmpty(o.Atributo_variante_1)));
        cmd.Parameters.Add(new SqlParameter("@codigo_producto", NullIfEmpty(o.Codigo_producto)));
        cmd.Parameters.Add(new SqlParameter("@lic_plate", NullIfEmpty(o.Lic_plate)));
        cmd.Parameters.Add(new SqlParameter("@pallet_no_estandar", o.Pallet_no_estandar));
        cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraEnc", NullIfZero(o.IdOrdenCompraEnc)));
        cmd.Parameters.Add(new SqlParameter("@IdOrdenCompraDet", NullIfZero(o.IdOrdenCompraDet)));
        cmd.Parameters.Add(new SqlParameter("@IdJornadaSistema", NullIfZero(o.IdJornadaSistema)));
    }

    public static void InsertarOActualizar(IConfiguration config, List<clsBeTrans_re_det> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                bool existe = Existe(entity.IdRecepcionDet, entity.IdRecepcionEnc, connection, isExternalTx ? tx! : localTx!);

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

    public static bool Existe(int IdRecepcionDet, int IdRecepcionEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Trans_re_det WHERE IdRecepcionDet = @IdRecepcionDet AND IdRecepcionEnc = @IdRecepcionEnc";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", IdRecepcionDet));
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
}