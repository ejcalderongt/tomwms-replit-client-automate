using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Stock;
using Microsoft.Extensions.Configuration;
public class clsLnStock_rec
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeStock_rec oBeStock_rec, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) => dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]);

        try
        {
            oBeStock_rec.IdStockRec = GetInt("IdStockRec");
            oBeStock_rec.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeStock_rec.IdProductoBodega = GetInt("IdProductoBodega");
            oBeStock_rec.IdProductoEstado = GetInt("IdProductoEstado");
            oBeStock_rec.IdPresentacion = GetInt("IdPresentacion");
            oBeStock_rec.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeStock_rec.IdUbicacion = GetInt("IdUbicacion");
            oBeStock_rec.IdUbicacion_anterior = GetInt("IdUbicacion_anterior");
            oBeStock_rec.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeStock_rec.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeStock_rec.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeStock_rec.IdPickingEnc = GetInt("IdPickingEnc");
            oBeStock_rec.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeStock_rec.Lote = GetString("lote");
            oBeStock_rec.Lic_plate = GetString("lic_plate");
            oBeStock_rec.Serial = GetString("serial");
            oBeStock_rec.Cantidad = GetDouble("cantidad");
            oBeStock_rec.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeStock_rec.Fecha_vence = GetDate("fecha_vence");
            oBeStock_rec.Uds_lic_plate = GetDouble("uds_lic_plate");
            oBeStock_rec.No_bulto = GetInt("no_bulto");
            oBeStock_rec.Fecha_manufactura = GetDate("fecha_manufactura");
            oBeStock_rec.Añada = GetInt("añada");
            oBeStock_rec.User_agr = GetString("user_agr");
            oBeStock_rec.Fec_agr = GetDate("fec_agr");
            oBeStock_rec.User_mod = GetString("user_mod");
            oBeStock_rec.Fec_mod = GetDate("fec_mod");
            oBeStock_rec.Activo = GetBool("activo");
            oBeStock_rec.Peso = GetDouble("peso");
            oBeStock_rec.Temperatura = GetDouble("temperatura");
            oBeStock_rec.Regularizado = GetBool("regularizado");
            oBeStock_rec.Fecha_regularizacion = GetDate("fecha_regularizacion");
            oBeStock_rec.No_linea = GetInt("no_linea");
            oBeStock_rec.Atributo_variante_1 = GetString("atributo_variante_1");
            oBeStock_rec.Impreso = GetBool("impreso");
            oBeStock_rec.IdBodega = GetInt("IdBodega");
            oBeStock_rec.Pallet_no_estandar = GetBool("pallet_no_estandar");
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

    public static int Insertar(IConfiguration config, clsBeStock_rec oBeStock_rec, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock_rec");
            Ins.Add("idstockrec", "@idstockrec", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idubicacion_anterior", "@idubicacion_anterior", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Ins.Add("no_bulto", "@no_bulto", "F");
            Ins.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("temperatura", "@temperatura", "F");
            Ins.Add("regularizado", "@regularizado", "F");
            Ins.Add("fecha_regularizacion", "@fecha_regularizacion", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("impreso", "@impreso", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");

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

            Bind(cmd, oBeStock_rec);

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

    public static int Insertar(IConfiguration config, clsBeStock_rec oBeStock_rec)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock_rec");
            Ins.Add("idstockrec", "@idstockrec", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idubicacion_anterior", "@idubicacion_anterior", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Ins.Add("no_bulto", "@no_bulto", "F");
            Ins.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("temperatura", "@temperatura", "F");
            Ins.Add("regularizado", "@regularizado", "F");
            Ins.Add("fecha_regularizacion", "@fecha_regularizacion", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("impreso", "@impreso", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeStock_rec);

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

    public static int Actualizar(IConfiguration config, clsBeStock_rec oBeStock_rec, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("stock_rec");
            Upd.Add("idstockrec", "@idstockrec", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("idubicacion_anterior", "@idubicacion_anterior", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idrecepciondet", "@idrecepciondet", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("iddespachoenc", "@iddespachoenc", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("serial", "@serial", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Upd.Add("no_bulto", "@no_bulto", "F");
            Upd.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Upd.Add("añada", "@añada", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("temperatura", "@temperatura", "F");
            Upd.Add("regularizado", "@regularizado", "F");
            Upd.Add("fecha_regularizacion", "@fecha_regularizacion", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("impreso", "@impreso", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Upd.Where("IdStockRec = @IdStockRec");

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

            Bind(cmd, oBeStock_rec);

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

    public int Eliminar(IConfiguration config, clsBeStock_rec oBeStock_rec, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Stock_rec" +
             "  Where(IdStockRec = @IdStockRec)");

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

            cmd.Parameters.Add(new SqlParameter("@IdStockRec", oBeStock_rec.IdStockRec));

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
            const string sp = "Select * FROM Stock_rec";
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

    public static bool GetSingle(IConfiguration config, ref clsBeStock_rec pBeStock_rec)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Stock_rec" +
            " Where(IdStockRec = @IdStockRec)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdStockRec", pBeStock_rec.IdStockRec));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeStock_rec, r);
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

    public static List<clsBeStock_rec> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeStock_rec> lreturnList = new List<clsBeStock_rec>();

        try
        {
            const string sp = "Select * FROM Stock_rec";

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

                        clsBeStock_rec vBeStock_rec = new clsBeStock_rec();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeStock_rec = new clsBeStock_rec();
                            Cargar(ref vBeStock_rec, dr);
                            lreturnList.Add(vBeStock_rec);
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

            const string sp = "Select ISNULL(Max(IdStockRec),0) FROM Stock_rec";

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


            const string sp = "Select ISNULL(Max(IdStockRec),0) FROM Stock_rec";

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

    public static void Bind(SqlCommand cmd, clsBeStock_rec oBeStock_rec)
    {
        cmd.Parameters.Add(new SqlParameter("@IdStockRec", oBeStock_rec.IdStockRec == 0 ? DBNull.Value : oBeStock_rec.IdStockRec));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeStock_rec.IdPropietarioBodega == 0 ? DBNull.Value : oBeStock_rec.IdPropietarioBodega));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeStock_rec.IdProductoBodega == 0 ? DBNull.Value : oBeStock_rec.IdProductoBodega));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeStock_rec.IdProductoEstado == 0 ? DBNull.Value : oBeStock_rec.IdProductoEstado));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeStock_rec.IdPresentacion == 0 ? DBNull.Value : oBeStock_rec.IdPresentacion));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeStock_rec.IdUnidadMedida == 0 ? DBNull.Value : oBeStock_rec.IdUnidadMedida));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeStock_rec.IdUbicacion == 0 ? DBNull.Value : oBeStock_rec.IdUbicacion));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion_anterior", oBeStock_rec.IdUbicacion_anterior == 0 ? DBNull.Value : oBeStock_rec.IdUbicacion_anterior));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeStock_rec.IdRecepcionEnc == 0 ? DBNull.Value : oBeStock_rec.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", oBeStock_rec.IdRecepcionDet == 0 ? DBNull.Value : oBeStock_rec.IdRecepcionDet));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeStock_rec.IdPedidoEnc == 0 ? DBNull.Value : oBeStock_rec.IdPedidoEnc));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", oBeStock_rec.IdPickingEnc == 0 ? DBNull.Value : oBeStock_rec.IdPickingEnc));
        cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeStock_rec.IdDespachoEnc == 0 ? DBNull.Value : oBeStock_rec.IdDespachoEnc));
        //GT borrar comentario, el lote aqui tambien puede ir vacio
        cmd.Parameters.Add(new SqlParameter("@lote", oBeStock_rec.Lote));
        cmd.Parameters.Add(new SqlParameter("@lic_plate", string.IsNullOrWhiteSpace(oBeStock_rec.Lic_plate) ? DBNull.Value : oBeStock_rec.Lic_plate));
        cmd.Parameters.Add(new SqlParameter("@serial", string.IsNullOrWhiteSpace(oBeStock_rec.Serial) ? DBNull.Value : oBeStock_rec.Serial));
        cmd.Parameters.Add(new SqlParameter("@cantidad", oBeStock_rec.Cantidad == 0 ? DBNull.Value : oBeStock_rec.Cantidad));
        cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeStock_rec.Fecha_ingreso));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeStock_rec.Fecha_vence));
        cmd.Parameters.Add(new SqlParameter("@uds_lic_plate", oBeStock_rec.Uds_lic_plate == 0 ? DBNull.Value : oBeStock_rec.Uds_lic_plate));
        cmd.Parameters.Add(new SqlParameter("@no_bulto", oBeStock_rec.No_bulto == 0 ? DBNull.Value : oBeStock_rec.No_bulto));
        cmd.Parameters.Add(new SqlParameter("@fecha_manufactura", oBeStock_rec.Fecha_manufactura));
        cmd.Parameters.Add(new SqlParameter("@añada", oBeStock_rec.Añada == 0 ? DBNull.Value : oBeStock_rec.Añada));
        cmd.Parameters.Add(new SqlParameter("@user_agr", string.IsNullOrWhiteSpace(oBeStock_rec.User_agr) ? DBNull.Value : oBeStock_rec.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeStock_rec.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", string.IsNullOrWhiteSpace(oBeStock_rec.User_mod) ? DBNull.Value : oBeStock_rec.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeStock_rec.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeStock_rec.Activo));
        cmd.Parameters.Add(new SqlParameter("@peso", oBeStock_rec.Peso == 0 ? DBNull.Value : oBeStock_rec.Peso));
        cmd.Parameters.Add(new SqlParameter("@temperatura", oBeStock_rec.Temperatura == 0 ? DBNull.Value : oBeStock_rec.Temperatura));
        cmd.Parameters.Add(new SqlParameter("@regularizado", oBeStock_rec.Regularizado));
        cmd.Parameters.Add(new SqlParameter("@fecha_regularizacion", oBeStock_rec.Fecha_regularizacion));
        cmd.Parameters.Add(new SqlParameter("@no_linea", oBeStock_rec.No_linea == 0 ? DBNull.Value : oBeStock_rec.No_linea));
        cmd.Parameters.Add(new SqlParameter("@atributo_variante_1", string.IsNullOrWhiteSpace(oBeStock_rec.Atributo_variante_1) ? DBNull.Value : oBeStock_rec.Atributo_variante_1));
        cmd.Parameters.Add(new SqlParameter("@impreso", oBeStock_rec.Impreso));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeStock_rec.IdBodega == 0 ? DBNull.Value : oBeStock_rec.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@pallet_no_estandar", oBeStock_rec.Pallet_no_estandar));
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeStock_rec> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                if (entity.IdStockRec != 0) {
                    bool existe = Existe(entity.IdStockRec, connection, isExternalTx ? tx! : localTx!);

                    if (existe)
                        Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
                    else
                        Insertar(config, entity, connection, isExternalTx ? tx : localTx);
                }
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
    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM Stock_rec WHERE IdStockRec = @Id", conn, tx);
        cmd.Parameters.AddWithValue("@Id", id);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }
}