using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Stock;
using Microsoft.Extensions.Configuration;
public class clsLnStock
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeStock oBeStock, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeStock.IdBodega = GetInt("IdBodega");
            oBeStock.IdStock = GetInt("IdStock");
            oBeStock.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeStock.IdProductoBodega = GetInt("IdProductoBodega");
            oBeStock.IdProductoEstado = GetInt("IdProductoEstado");
            oBeStock.IdPresentacion = GetInt("IdPresentacion");
            oBeStock.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeStock.IdUbicacion = GetInt("IdUbicacion");
            oBeStock.IdUbicacion_anterior = GetInt("IdUbicacion_anterior");
            oBeStock.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeStock.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeStock.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeStock.IdPickingEnc = GetInt("IdPickingEnc");
            oBeStock.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeStock.Lote = GetString("lote");
            oBeStock.Lic_plate = GetString("lic_plate");
            oBeStock.Serial = GetString("serial");
            oBeStock.Cantidad = GetDouble("cantidad");
            oBeStock.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeStock.Fecha_vence = GetDate("fecha_vence");
            oBeStock.Uds_lic_plate = GetDouble("uds_lic_plate");
            oBeStock.No_bulto = GetInt("no_bulto");
            oBeStock.Fecha_manufactura = GetDate("fecha_manufactura");
            oBeStock.Añada = GetInt("añada");
            oBeStock.User_agr = GetString("user_agr");
            oBeStock.Fec_agr = GetDate("fec_agr");
            oBeStock.User_mod = GetString("user_mod");
            oBeStock.Fec_mod = GetDate("fec_mod");
            oBeStock.Activo = GetBool("activo");
            oBeStock.Peso = GetDouble("peso");
            oBeStock.Temperatura = GetDouble("temperatura");
            oBeStock.Atributo_variante_1 = GetString("atributo_variante_1");
            oBeStock.Pallet_no_estandar = GetBool("pallet_no_estandar");
            oBeStock.IdPickingUbicStock = GetInt("IdPickingUbicStock");
            oBeStock.IdPickingUbic = GetInt("IdPickingUbic");
            oBeStock.IdPedidoDet = GetInt("IdPedidoDet");
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

    public static int Insertar(IConfiguration config, clsBeStock oBeStock, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idstock", "@idstock", "F");
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
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Ins.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");

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

            BindStockParameters(cmd, oBeStock);

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

    public static int Insertar(IConfiguration config, clsBeStock oBeStock)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idstock", "@idstock", "F");
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
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Ins.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindStockParameters(cmd, oBeStock);

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

    public static int Actualizar(IConfiguration config, clsBeStock oBeStock, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("stock");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idstock", "@idstock", "F");
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
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Upd.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Upd.Add("idpickingubic", "@idpickingubic", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Where("IdStock = @IdStock");

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

            BindStockParameters(cmd, oBeStock);

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

    public int Eliminar(IConfiguration config, clsBeStock oBeStock, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Stock" +
             "  Where(IdStock = @IdStock)");

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

            cmd.Parameters.Add(new SqlParameter("@IdStock", oBeStock.IdStock));

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
            const string sp = "Select * FROM Stock";
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

    public static bool GetSingle(IConfiguration config, ref clsBeStock pBeStock)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Stock" +
            " Where(IdStock = @IdStock)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeStock.IdBodega));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeStock, r);
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

    public static List<clsBeStock> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeStock> lreturnList = new List<clsBeStock>();

        try
        {
            const string sp = "Select * FROM Stock";

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

                        clsBeStock vBeStock = new clsBeStock();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeStock = new clsBeStock();
                            Cargar(ref vBeStock, dr);
                            lreturnList.Add(vBeStock);
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

            const string sp = "Select ISNULL(Max(IdStock),0) FROM Stock";

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


            const string sp = "Select ISNULL(Max(IdStock),0) FROM Stock";

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
    public static void BindStockParameters(SqlCommand cmd, clsBeStock oBeStock)
    {
        cmd.Parameters.AddWithValue("@IdBodega", oBeStock.IdBodega);
        cmd.Parameters.AddWithValue("@IdStock", oBeStock.IdStock);
        cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBeStock.IdPropietarioBodega == 0 ? DBNull.Value : oBeStock.IdPropietarioBodega);
        cmd.Parameters.AddWithValue("@IdProductoBodega", oBeStock.IdProductoBodega == 0 ? DBNull.Value : oBeStock.IdProductoBodega);
        cmd.Parameters.AddWithValue("@IdProductoEstado", oBeStock.IdProductoEstado == 0 ? DBNull.Value : oBeStock.IdProductoEstado);
        cmd.Parameters.AddWithValue("@IdPresentacion", oBeStock.IdPresentacion == 0 ? DBNull.Value : oBeStock.IdPresentacion);
        cmd.Parameters.AddWithValue("@IdUnidadMedida", oBeStock.IdUnidadMedida == 0 ? DBNull.Value : oBeStock.IdUnidadMedida);
        cmd.Parameters.AddWithValue("@IdUbicacion", oBeStock.IdUbicacion == 0 ? DBNull.Value : oBeStock.IdUbicacion);
        cmd.Parameters.AddWithValue("@IdUbicacion_anterior", oBeStock.IdUbicacion_anterior == 0 ? DBNull.Value : oBeStock.IdUbicacion_anterior);
        cmd.Parameters.AddWithValue("@IdRecepcionEnc", oBeStock.IdRecepcionEnc == 0 ? DBNull.Value : oBeStock.IdRecepcionEnc);
        cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeStock.IdRecepcionDet == 0 ? DBNull.Value : oBeStock.IdRecepcionDet);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", oBeStock.IdPedidoEnc == 0 ? DBNull.Value : oBeStock.IdPedidoEnc);
        cmd.Parameters.AddWithValue("@IdPickingEnc", oBeStock.IdPickingEnc == 0 ? DBNull.Value : oBeStock.IdPickingEnc);
        cmd.Parameters.AddWithValue("@IdDespachoEnc", oBeStock.IdDespachoEnc == 0 ? DBNull.Value : oBeStock.IdDespachoEnc);
        //GT01072025: borrar comentario, lote no puede insertar null
        cmd.Parameters.AddWithValue("@lote", oBeStock.Lote);
        cmd.Parameters.AddWithValue("@lic_plate", string.IsNullOrEmpty(oBeStock.Lic_plate) ? DBNull.Value : oBeStock.Lic_plate);
        cmd.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(oBeStock.Serial) ? DBNull.Value : oBeStock.Serial);

        cmd.Parameters.AddWithValue("@cantidad", oBeStock.Cantidad);
        cmd.Parameters.AddWithValue("@fecha_ingreso", oBeStock.Fecha_ingreso == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_ingreso);
        cmd.Parameters.AddWithValue("@fecha_vence", oBeStock.Fecha_vence == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_vence);
        cmd.Parameters.AddWithValue("@uds_lic_plate", oBeStock.Uds_lic_plate);
        cmd.Parameters.AddWithValue("@no_bulto", oBeStock.No_bulto);
        cmd.Parameters.AddWithValue("@fecha_manufactura", oBeStock.Fecha_manufactura == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_manufactura);
        cmd.Parameters.AddWithValue("@añada", oBeStock.Añada);

        cmd.Parameters.AddWithValue("@user_agr", string.IsNullOrEmpty(oBeStock.User_agr) ? DBNull.Value : oBeStock.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", oBeStock.Fec_agr == DateTime.MinValue ? DBNull.Value : oBeStock.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", string.IsNullOrEmpty(oBeStock.User_mod) ? DBNull.Value : oBeStock.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", oBeStock.Fec_mod == DateTime.MinValue ? DBNull.Value : oBeStock.Fec_mod);

        cmd.Parameters.AddWithValue("@activo", oBeStock.Activo);
        cmd.Parameters.AddWithValue("@peso", oBeStock.Peso);
        cmd.Parameters.AddWithValue("@temperatura", oBeStock.Temperatura);
        cmd.Parameters.AddWithValue("@atributo_variante_1", string.IsNullOrEmpty(oBeStock.Atributo_variante_1) ? DBNull.Value : oBeStock.Atributo_variante_1);
        cmd.Parameters.AddWithValue("@pallet_no_estandar", oBeStock.Pallet_no_estandar);

        cmd.Parameters.AddWithValue("@IdPickingUbicStock", oBeStock.IdPickingUbicStock == 0 ? DBNull.Value : oBeStock.IdPickingUbicStock);
        cmd.Parameters.AddWithValue("@IdPickingUbic", oBeStock.IdPickingUbic == 0 ? DBNull.Value : oBeStock.IdPickingUbic);
        cmd.Parameters.AddWithValue("@IdPedidoDet", oBeStock.IdPedidoDet == 0 ? DBNull.Value : oBeStock.IdPedidoDet);
    }

    public static void InsertarOActualizar(IConfiguration config, List<clsBeStock> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
             
                if (entity.IdStock != 0)
                {
                    bool existe = Existe(entity.IdStock, connection, isExternalTx ? tx! : localTx!);

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
    public static bool Existe(int idStock, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM Stock WHERE IdStock = @Id", conn, tx);
        cmd.Parameters.AddWithValue("@Id", idStock);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

}