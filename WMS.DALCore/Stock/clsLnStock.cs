using System.Reflection;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Stock;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Producto;
using WMS.DALCore.Stock;
using WMS.EntityCore.Trans_re;
using AppGlobal;
using WMS.EntityCore.Datos_Maestros;
using WMSWebAPI.Be;
using System.Globalization;

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
            oBeStock.Fecha_Ingreso = GetDate("fecha_ingreso");
            oBeStock.Fecha_vence = GetDate("fecha_vence");
            oBeStock.Uds_lic_plate = GetDouble("uds_lic_plate");
            oBeStock.No_bulto = GetInt("no_bulto");
            oBeStock.Fecha_Manufactura = GetDate("fecha_manufactura");
            oBeStock.Añada = GetInt("añada");
            oBeStock.User_agr = GetString("user_agr");
            oBeStock.Fec_agr = GetDate("fec_agr");
            oBeStock.User_mod = GetString("user_mod");
            oBeStock.Fec_mod = GetDate("fec_mod");
            oBeStock.Activo = GetBool("activo");
            oBeStock.Peso = GetDouble("peso");
            oBeStock.Temperatura = GetDouble("temperatura");
            oBeStock.Atributo_Variante_1 = GetString("atributo_variante_1");
            oBeStock.Pallet_No_Estandar = GetBool("pallet_no_estandar");
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

    public static int Insertar(clsBeStock oBeStock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeStock == null)
            throw new ArgumentNullException(nameof(oBeStock));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindStockParameters(cmd, oBeStock);

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

    public static int Insertar_3pl(clsBeStock_3pl oBeStock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeStock == null)
            throw new ArgumentNullException(nameof(oBeStock));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindStockParameters_3pl(cmd, oBeStock);

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

    public static int Actualizar(clsBeStock oBeStock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeStock == null)
            throw new ArgumentNullException(nameof(oBeStock));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindStockParameters(cmd, oBeStock);

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

    public static int Actualizar_3pl(clsBeStock_3pl oBeStock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeStock == null)
            throw new ArgumentNullException(nameof(oBeStock));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

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

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindStockParameters_3pl(cmd, oBeStock);

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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sp = "Select ISNULL(Max(IdStock),0) FROM Stock";

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                return Convert.ToInt32(lreturnValue);
            }
        }

        return 0;
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
        cmd.Parameters.AddWithValue("@fecha_ingreso", oBeStock.Fecha_Ingreso == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_Ingreso);
        cmd.Parameters.AddWithValue("@fecha_vence", oBeStock.Fecha_vence == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_vence);
        cmd.Parameters.AddWithValue("@uds_lic_plate", oBeStock.Uds_lic_plate);
        cmd.Parameters.AddWithValue("@no_bulto", oBeStock.No_bulto);
        cmd.Parameters.AddWithValue("@fecha_manufactura", oBeStock.Fecha_Manufactura == DateTime.MinValue ? DBNull.Value : oBeStock.Fecha_Manufactura);
        cmd.Parameters.AddWithValue("@añada", oBeStock.Añada);

        cmd.Parameters.AddWithValue("@user_agr", string.IsNullOrEmpty(oBeStock.User_agr) ? DBNull.Value : oBeStock.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", oBeStock.Fec_agr == DateTime.MinValue ? DBNull.Value : oBeStock.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", string.IsNullOrEmpty(oBeStock.User_mod) ? DBNull.Value : oBeStock.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", oBeStock.Fec_mod == DateTime.MinValue ? DBNull.Value : oBeStock.Fec_mod);

        cmd.Parameters.AddWithValue("@activo", oBeStock.Activo);
        cmd.Parameters.AddWithValue("@peso", oBeStock.Peso);
        cmd.Parameters.AddWithValue("@temperatura", oBeStock.Temperatura);
        cmd.Parameters.AddWithValue("@atributo_variante_1", string.IsNullOrEmpty(oBeStock.Atributo_Variante_1) ? DBNull.Value : oBeStock.Atributo_Variante_1);
        cmd.Parameters.AddWithValue("@pallet_no_estandar", oBeStock.Pallet_No_Estandar);

        cmd.Parameters.AddWithValue("@IdPickingUbicStock", oBeStock.IdPickingUbicStock == 0 ? DBNull.Value : oBeStock.IdPickingUbicStock);
        cmd.Parameters.AddWithValue("@IdPickingUbic", oBeStock.IdPickingUbic == 0 ? DBNull.Value : oBeStock.IdPickingUbic);
        cmd.Parameters.AddWithValue("@IdPedidoDet", oBeStock.IdPedidoDet == 0 ? DBNull.Value : oBeStock.IdPedidoDet);
    }

    public static void BindStockParameters_3pl(SqlCommand cmd, clsBeStock_3pl oBeStock)
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
    public static void InsertarOActualizar(List<clsBeStock> entities, SqlConnection conn, SqlTransaction tx)
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

                if (entity.IdStock != 0)
                {
                    bool existe = Existe(entity.IdStock, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public static void InsertarOActualizar_3pl(List<clsBeStock_3pl> entities, SqlConnection conn, SqlTransaction tx)
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

                if (entity.IdStock != 0)
                {
                    bool existe = Existe(entity.IdStock, conn, tx);

                    if (existe)
                        Actualizar_3pl(entity, conn, tx);
                    else
                        Insertar_3pl(entity, conn, tx);
                }
            }
        }
        catch (SqlException)
        {
            throw;
        }
    }
    public static bool Existe(int idStock, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM Stock WHERE IdStock = @Id", conn, tx);
        cmd.Parameters.AddWithValue("@Id", idStock);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    public static void Get_Existencia_By_IdProductoBodega(ref clsBeStock_rec BeStockRec,
                                                         SqlConnection pConnection,
                                                         SqlTransaction pTransaction)
    {
        try
        {
            string Lote;
            int Aniada;
            DateTime Fecha_Vence;
            List<clsBeVW_stock_res> lStockConFiltro;

            lStockConFiltro = Get_All_By_IdProductoBodega(BeStockRec.IdProductoBodega,
                                                          pConnection,
                                                          pTransaction);

            if (lStockConFiltro == null)
            {
                BeStockRec.CantidadEnStock = 0;
                BeStockRec.PesoEnStock = 0;
            }
            else
            {
                clsBeProducto? BeProducto = clsLnProducto.Get_Single_Producto_Bodega(BeStockRec.IdProductoBodega,
                                                                                     pConnection,
                                                                                     pTransaction);

                if (BeProducto != null)
                {
                    if (BeProducto.control_lote)
                    {
                        Lote = string.Format("'{0}'", BeStockRec.Lote);
                        Lote = BeStockRec.Lote;
                        lStockConFiltro = lStockConFiltro.FindAll(x => x.Lote == Lote);
                    }
                    else
                    {
                        Lote = "";
                    }

                    if (lStockConFiltro.Count > 0)
                    {
                        if (BeProducto.capturar_aniada)
                        {
                            Aniada = BeStockRec.Añada;
                            lStockConFiltro = lStockConFiltro.FindAll(x => x.Añada == Aniada);
                        }
                        else
                        {
                            Aniada = 0;
                        }
                    }

                    if (lStockConFiltro.Count > 0)
                    {
                        if (BeProducto.control_vencimiento)
                        {
                            Fecha_Vence = BeStockRec.Fecha_vence.Date;
                            lStockConFiltro = lStockConFiltro.FindAll(x => x.Fecha_Vence == Fecha_Vence);
                        }
                    }

                    int vIdPresentacion = BeStockRec.Presentacion.IdPresentacion;
                    lStockConFiltro = lStockConFiltro.FindAll(x => x.IdPresentacion == vIdPresentacion);

                    int vIdUnidadMedida = BeStockRec.IdUnidadMedida;
                    lStockConFiltro = lStockConFiltro.FindAll(x => x.IdUnidadMedida == vIdUnidadMedida);

                    int vIdEstado = BeStockRec.ProductoEstado.IdEstado;
                    lStockConFiltro = lStockConFiltro.FindAll(x => x.IdProductoEstado == vIdEstado);

                    BeStockRec.CantidadEnStock = lStockConFiltro.Sum(x => x.CantidadUmBas);
                    BeStockRec.PesoEnStock = lStockConFiltro.Sum(x => x.Peso);
                }
            }
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public static List<clsBeVW_stock_res> Get_All_By_IdProductoBodega(int pIdProductoBodega,
                                                                      SqlConnection lConnection,
                                                                      SqlTransaction lTransaction)
    {
        List<clsBeVW_stock_res> lReturnList = new List<clsBeVW_stock_res>();

        try
        {
            string vSQL = "Select * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);
                lDTA.SelectCommand.Transaction = lTransaction;

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeVW_stock_res Obj = new clsBeVW_stock_res();
                        clsLnVW_stock_res.Cargar(ref Obj, lRow);
                        lReturnList.Add(Obj);
                    }
                    return lReturnList;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return lReturnList;
    }

    public static bool Insertar_Stock_Recepcion(List<clsBeTrans_re_det> pListBeTransReDet,
                                                int pIdUsuario,
                                                int pIdEmpresa,
                                                int pIdBodega,
                                                SqlConnection lConnection,
                                                SqlTransaction lTransaction)
    {
        bool Insertar_Stock_Recepcion = false;

        try
        {
            if (pListBeTransReDet != null)
            {
                int lMaxS = MaxID(lConnection, lTransaction);
                clsBeStock BeStock;
                clsBeStock_rec BeStockRec;
                clsBeVW_stock_res? BeVWStockRec;
                bool vExisteEnReDet = false;

                foreach (clsBeTrans_re_det BeTransReDet in pListBeTransReDet)
                {
                    BeStock = new clsBeStock();

                    vExisteEnReDet = clsLnTrans_re_det.Existe_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet, lConnection, lTransaction);

                    if (!vExisteEnReDet)
                    {
                        clsPublic.CopyObject(BeTransReDet, ref BeStock);

                        lMaxS += 1;

                        BeStock.IdBodega = pIdBodega;
                        BeStock.IdStock = lMaxS;
                        BeStock.IdPropietarioBodega = BeTransReDet.IdPropietarioBodega;
                        BeStock.IdProductoBodega = BeTransReDet.IdProductoBodega;
                        BeStock.ProductoEstado = new clsBeProducto_estado();
                        BeStock.Presentacion = new clsBeProducto_presentacion();
                        BeStock.ProductoEstado.IdEstado = BeTransReDet.ProductoEstado.IdEstado;
                        BeStock.Presentacion.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion;
                        BeStock.IdUnidadMedida = BeTransReDet.UnidadMedida.IdUnidadMedida;
                        BeStock.IdUbicacion = BeTransReDet.IdUbicacion;
                        BeStock.IdUbicacion_anterior = BeTransReDet.IdUbicacion;
                        BeStock.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc;
                        BeStock.IdRecepcionDet = BeTransReDet.IdRecepcionDet;

                        if (BeTransReDet.Presentacion.IdPresentacion != 0)
                        {
                            clsBeProducto_presentacion BePres = new clsBeProducto_presentacion { IdPresentacion = BeTransReDet.Presentacion.IdPresentacion };
                            clsLnProducto_presentacion.GetSingle(ref BePres, lConnection, lTransaction);

                            if (BePres != null)
                            {
                                if (BePres.EsPallet)
                                {
                                    BeStock.Cantidad = Math.Round(BeTransReDet.Cantidad_recibida * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima, 6);
                                }
                                else
                                {
                                    BeStock.Cantidad = Math.Round(BeTransReDet.Cantidad_recibida * BePres.Factor, 6);
                                }
                            }
                            else
                            {
                                if (BePres != null)
                                    throw new Exception("20200329_0939: No se pudo obtener la presentación para: " + BePres.IdPresentacion);
                            }
                        }
                        else
                        {
                            BeStock.Cantidad = BeTransReDet.Cantidad_recibida;
                        }

                        BeStock.Fecha_Ingreso = BeTransReDet.Fecha_ingreso;
                        BeStock.Fecha_vence = BeTransReDet.Fecha_vence;
                        BeStock.Fecha_Manufactura = new DateTime(1900, 1, 1);
                        BeStock.User_agr = pIdUsuario.ToString();
                        BeStock.Fec_agr = DateTime.Now;
                        BeStock.User_mod = pIdUsuario.ToString();
                        BeStock.Fec_mod = DateTime.Now;
                        BeStock.Activo = true;

                        BeVWStockRec = Get_Single_By_BeRecepcionDet(BeTransReDet,
                                                                    pIdBodega,
                                                                    lConnection,
                                                                    lTransaction);

                        if (BeVWStockRec != null)
                        {
                            throw new Exception("ERROR_202302222350: EL sistema detectó una condición no válida para la finalización de la tarea, el stock podría duplicarse, para prevenir esto no se finalizará la recepción. Reporte este error a desarrollo, lamentamos el inconveniente, Gracias.");
                        }

                        Insertar(BeStock, lConnection, lTransaction);

                        BeStockRec = new clsBeStock_rec();
                        clsPublic.CopyObject(BeStock, ref BeStockRec);

                        clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                              pIdBodega,
                                                                              pIdUsuario,
                                                                              BeStockRec,
                                                                              lConnection,
                                                                              lTransaction);
                    }
                }

                Insertar_Stock_Recepcion = true;
            }

            return Insertar_Stock_Recepcion;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeVW_stock_res? Get_Single_By_BeRecepcionDet(clsBeTrans_re_det pBeRecepcionDet,
                                                                  int pIdBodega,
                                                                  SqlConnection lConnection,
                                                                  SqlTransaction lTransaction)
    {
        clsBeVW_stock_res? result = null;

        try
        {
            string vSQL = "SELECT * FROM VW_Stock_Res " +
                          "WHERE IdBodega = @IdBodega " +
                          "AND IdRecepcionEnc = @IdRecepcionEnc " +
                          "AND IdRecepcionDet = @IdRecepcionDet " +
                          "AND IdProductoBodega = @IdProductoBodega ";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeRecepcionDet.IdRecepcionEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pBeRecepcionDet.IdRecepcionDet);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeRecepcionDet.IdProductoBodega);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    DataRow lRow = lDataTable.Rows[0];
                    if (lRow != null)
                    {
                        clsBeVW_stock_res BeVWStockRes = new clsBeVW_stock_res();
                        clsLnVW_stock_res.Cargar(ref BeVWStockRes, lRow);
                        result = BeVWStockRes;
                    }
                }
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static bool IsNumeric(string value)
    {
        return double.TryParse(value, out _);
    }
    public static List<clsBeStock>? lStock(ref clsBeStock_res pBeStockRes,
                                           ref clsBeProducto? pBeProductoOutput,
                                           int DiasVencimiento,
                                           clsBeI_nav_config_enc pBeConfigEnc,
                                           SqlConnection lConnection,
                                           SqlTransaction? ltransaction,
                                           bool pExcluirUbicacionPicking = false,
                                           bool Conmutar_Umbas_A_Presentacion = false,
                                           bool pTarea_Reabasto = false,
                                           bool pEs_Devolucion = false)
    {
        try
        {
            clsBeStock vBeStock = new clsBeStock();
            List<clsBeStock> lBeStock = new List<clsBeStock>();
            int vIdProductoBodega = 0;

            if (pBeStockRes == null)
            {
                return null;
            }

            vIdProductoBodega = pBeStockRes.IdProductoBodega;

            pBeProductoOutput = clsLnProducto.Get_Single_By_IdProductoBodega(
                pBeStockRes.IdProductoBodega,
                lConnection,
                ltransaction);

            if (pBeProductoOutput != null)
            {
                pBeProductoOutput.IdProductoBodega = pBeStockRes.IdProductoBodega;
            }

            clsBeBodega? BeBodega = clsLnBodega.GetSingle_By_Idbodega(
                pBeConfigEnc.Idbodega,
                lConnection,
                ltransaction);

            // Construcción de la consulta SQL
            string vSQL = @"SELECT stock.*,
                    bodega_ubicacion.indice_x, 
                    bodega_ubicacion.nivel, 
                    bodega_ubicacion.orientacion_pos, 
                    bodega_tramo.descripcion AS Nombre_Tramo,
                    bodega_ubicacion.ubicacion_picking
                    FROM stock INNER JOIN
                    producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                    bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    AND stock.IdBodega = bodega_ubicacion.IdBodega
                    INNER JOIN
                    bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                    AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                    AND bodega_ubicacion.IdSector = bodega_tramo.IdSector ";

            if (pBeStockRes.Control_Ultimo_Lote)
            {
                vSQL += @" LEFT OUTER JOIN
                  trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
                  AND stock.lote = trans_re_det_lote_num.Lote ";
            }

            vSQL += @" WHERE bodega_ubicacion.activo = 1 
               and bodega_ubicacion.bloqueada = 0
               and producto_bodega.idproductobodega = @idproductobodega                     
               and stock.idunidadmedida = @idunidadmedida ";

            // Validación para devolución a proveedor
            if (!pEs_Devolucion && pBeStockRes.IdUbicacionAbastecerCon == 0)
            {
                vSQL += " and stock.idproductoestado = @idproductoestado ";
            }

            if (BeBodega !=null)
            if (!BeBodega.Permitir_decimales)
            {
                if (pBeStockRes.IdPresentacion != 0 || pBeStockRes.Atributo_Variante_1 != null)
                {
                    if (pBeStockRes.Atributo_Variante_1 != null)
                    {
                        if (pBeStockRes.Atributo_Variante_1 != "" || pBeStockRes.IdPresentacion != 0)
                        {
                            vSQL += "and (stock.idpresentacion = @idpresentacion) ";
                        }
                        else
                        {
                            if (pBeConfigEnc.Explosion_Automatica ||
                                pBeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking ||
                                pBeStockRes.IdPresentacion == 0)
                            {
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) ";
                            }
                            if (!Conmutar_Umbas_A_Presentacion)
                            {
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) ";
                            }
                        }
                    }
                }
                else
                {
                    // Reservar UMBAS aunque solo tenga producto en PRES
                    if (!Conmutar_Umbas_A_Presentacion)
                    {
                        vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) ";
                    }
                }
            }

            if (DiasVencimiento != 0)
            {
                vSQL += " And DATEDIFF (DAY, GETDATE(), stock.fecha_vence) >= @DiasVencimientoCliente ";
            }

            // Validación de vencimiento
            if (pBeStockRes.IdUbicacionAbastecerCon == 0)
            {
                if (BeBodega != null && pBeProductoOutput != null)
                {
                    if (pBeProductoOutput.control_vencimiento)
                    {
                        // Validación para devolución a proveedor
                        if (!BeBodega.Despachar_producto_vencido && !pEs_Devolucion)
                        {
                            vSQL += " And stock.fecha_vence > GETDATE() ";
                        }
                    }
                }
            }

            // Excluir ubicaciones de despacho
            if (pBeStockRes.IdUbicacionAbastecerCon == 0)
            {
                vSQL += @" AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
                        FROM bodega_ubicacion bu
                        WHERE (bu.ubicacion_despacho = 1) AND (bu.IdBodega = @IdBodega))";
            }

            // Excluir ubicaciones de picking
            if (pExcluirUbicacionPicking)
            {
                vSQL += @" AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
                                                FROM bodega_ubicacion bu
                                                WHERE (bu.ubicacion_picking = 1) AND (bu.IdBodega = @IdBodega))";
            }

            // Excluir ubicaciones de recepción
            if (pBeConfigEnc.Excluir_Recepcion_Picking)
            {
                vSQL += @" AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
                                                FROM bodega_ubicacion bu
                                                WHERE (bu.ubicacion_recepcion = 1) AND (bu.IdBodega = @IdBodega))";
            }

            // Excluir ubicaciones de reabasto
            if (pTarea_Reabasto)
            {
                if (pBeConfigEnc.Excluir_Ubicaciones_Reabasto)
                {
                    vSQL += @" AND stock.IdUbicacion NOT IN (SELECT pr.IdUbicacion
                     FROM producto_rellenado pr
                     WHERE (pr.IdBodega = @IdBodega) AND (pr.Activo = 1))";
                }
            }
            // Control de último lote
            if (!string.IsNullOrEmpty(pBeStockRes?.Ultimo_Lote) && IsNumeric(pBeStockRes.Ultimo_Lote!))
            {
                vSQL += " And (trans_re_det_lote_num.lote_numerico >= @UltimoLote)";
            }

            // Ubicación específica para abastecer
            if (pBeStockRes?.IdUbicacionAbastecerCon != null && pBeStockRes.IdUbicacionAbastecerCon != 0)
            {
                vSQL += " and stock.idubicacion = @IdUbicacionAbastecerCon ";
            }

            
            // Restricciones para explosión automática
            if (Conmutar_Umbas_A_Presentacion || (pBeStockRes?.IdPresentacion ?? 0) == 0)
            {
                // Validar si NO se debe excluir la ubicación de picking
                if (!pExcluirUbicacionPicking)
                {
                    // Validar configuración de explosión automática desde ubicación de picking
                    if (pBeConfigEnc?.Explosion_Automatica_Desde_Ubicacion_Picking == true)
                    {
                        vSQL += " and bodega_ubicacion.ubicacion_picking = 1 ";
                    }
                }

                // Obtener nivel máximo configurado (si existe)
                int nivelMax = pBeConfigEnc?.Explosion_Automatica_Nivel_Max ?? 0;
                if (nivelMax > 0)
                {
                    // Añadir condición SQL de nivel
                    vSQL += $" and bodega_ubicacion.nivel = {nivelMax}";
                }
            }

            // Control de talla y color
            if (BeBodega?.Control_Talla_Color == true)
            {
                int idTallaColor = pBeStockRes?.IdProductoTallaColor ?? 0;

                if (idTallaColor != 0)
                {
                    vSQL += " and stock.IdProductoTallaColor = @IdProductoTallaColor";
                }
            }

            // Ordenamiento por tipo de rotación
            int IdTipoRotacion = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(vIdProductoBodega, lConnection, ltransaction);

            // Aplicar ordenamiento según tipo de rotación
            switch (IdTipoRotacion)
            {
                case 1: // FIFO
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack, bodega_ubicacion.IdTramo, indice_x, nivel, orientacion_pos, cantidad";
                    break;
                case 2: // LIFO
                    vSQL += " ORDER BY fecha_ingreso desc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack, bodega_ubicacion.IdTramo, indice_x, nivel, orientacion_pos, cantidad";
                    break;
                case 3: // FEFO
                    vSQL += " ORDER BY fecha_vence, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack, bodega_ubicacion.IdTramo, indice_x, nivel, orientacion_pos, cantidad";
                    break;
                case 4: // UPSR (Ubicación prioritaria sobre rotación)
                    vSQL += " ORDER BY indice_x, bodega_tramo.es_rack, bodega_ubicacion.IdTramo, nivel, orientacion_pos, cantidad";
                    break;
                default: // Default FIFO
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack, bodega_ubicacion.IdTramo, indice_x, nivel, orientacion_pos, cantidad";
                    break;
            }

            // Ejecutar consulta
            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, ltransaction))
            {
                lCommand.CommandType = CommandType.Text;

                // Validación segura antes de usar propiedades del objeto
                if (pBeStockRes?.IdProductoBodega != null && pBeStockRes.IdProductoBodega != 0)
                {
                    lCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega);
                }


                // Validar atributos y presentación
                if (pBeStockRes is not null)
                {
                    if (!string.IsNullOrEmpty(pBeStockRes.Atributo_Variante_1) || pBeStockRes.IdPresentacion >= 0)
                    {
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion);
                    }
                }

                // Validar objetos antes de usar sus propiedades
                if (pBeStockRes is not null)
                {
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado);
                    lCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida);
                }              

                if (pBeConfigEnc is not null)
                {
                    lCommand.Parameters.AddWithValue("@IdBodega", pBeConfigEnc.Idbodega);
                }               

                if (DiasVencimiento != 0)
                {
                    lCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento);
                }

                // Control de último lote
                if (pBeStockRes?.Control_Ultimo_Lote == true &&
                    !string.IsNullOrWhiteSpace(pBeStockRes.Ultimo_Lote))
                {
                    if (double.TryParse(pBeStockRes.Ultimo_Lote, NumberStyles.Any, CultureInfo.InvariantCulture, out double ultimoLote))
                    {
                        lCommand.Parameters.AddWithValue("@UltimoLote", ultimoLote);
                    }                  
                }

                // Validar si existe ubicación de abastecimiento
                if (pBeStockRes?.IdUbicacionAbastecerCon != null && pBeStockRes.IdUbicacionAbastecerCon != 0)
                {
                    lCommand.Parameters.AddWithValue("@IdUbicacionAbastecerCon", pBeStockRes.IdUbicacionAbastecerCon);
                }


                // Validar si existe un Id de ProductoTallaColor válido
                if (pBeStockRes?.IdProductoTallaColor != null && pBeStockRes.IdProductoTallaColor != 0)
                {
                    lCommand.Parameters.AddWithValue("@IdProductoTallaColor", pBeStockRes.IdProductoTallaColor);
                }

                using (SqlDataReader dr = lCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vBeStock = new clsBeStock();

                        // Mapear campos desde el DataReader
                        vBeStock.IdBodega = dr["IdBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdBodega"]);
                        vBeStock.IdStock = dr["IdStock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdStock"]);
                        vBeStock.IdPropietarioBodega = dr["IdPropietarioBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPropietarioBodega"]);
                        vBeStock.IdProductoBodega = dr["IdProductoBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoBodega"]);
                        vBeStock.IdProductoEstado = dr["IdProductoEstado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoEstado"]);
                        vBeStock.ProductoEstado.IdEstado = dr["IdProductoEstado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoEstado"]);
                        vBeStock.Presentacion.IdPresentacion = dr["IdPresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPresentacion"]);
                        vBeStock.IdPresentacion = dr["IdPresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPresentacion"]);
                        vBeStock.IdUnidadMedida = dr["IdUnidadMedida"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUnidadMedida"]);
                        vBeStock.IdUbicacion = dr["IdUbicacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacion"]);
                        vBeStock.IdUbicacion_anterior = dr["IdUbicacion_anterior"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacion_anterior"]);
                        vBeStock.IdRecepcionEnc = dr["IdRecepcionEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionEnc"]);
                        vBeStock.IdRecepcionDet = dr["IdRecepcionDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionDet"]);
                        vBeStock.IdPedidoEnc = dr["IdPedidoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPedidoEnc"]);
                        vBeStock.IdPickingEnc = dr["IdPickingEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPickingEnc"]);
                        vBeStock.IdDespachoEnc = dr["IdDespachoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdDespachoEnc"]);
                        vBeStock.Cantidad = dr["cantidad"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad"]);
                        vBeStock.Fecha_Ingreso = dr["fecha_ingreso"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_ingreso"]);
                        vBeStock.Fecha_vence = dr["fecha_vence"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_vence"]);
                        vBeStock.Uds_lic_plate = dr["uds_lic_plate"] == DBNull.Value ? 0 : Convert.ToInt32(dr["uds_lic_plate"]);
                        vBeStock.No_bulto = dr["no_bulto"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no_bulto"]);
                        vBeStock.Fecha_Manufactura = dr["fecha_manufactura"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_manufactura"]);
                        vBeStock.Añada = dr["añada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["añada"]);
                        vBeStock.Fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);
                        vBeStock.Fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_mod"]);
                        vBeStock.Activo = dr["activo"] == DBNull.Value ? false : Convert.ToBoolean(dr["activo"]);
                        vBeStock.Peso = dr["Peso"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["Peso"]);
                        vBeStock.Temperatura = dr["temperatura"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["temperatura"]);
                        vBeStock.UbicacionPicking = dr["ubicacion_picking"] == DBNull.Value ? false : Convert.ToBoolean(dr["ubicacion_picking"]);
                        vBeStock.UbicacionNivel = dr["nivel"] == DBNull.Value ? 0 : Convert.ToInt32(dr["nivel"]);
                        vBeStock.IdProductoTallaColor = dr["IdProductoTallaColor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoTallaColor"]);

                        // Para campos string
                        vBeStock.Lote = dr["lote"] == DBNull.Value ? string.Empty : dr.GetString(dr.GetOrdinal("lote"));
                        vBeStock.Serial = dr["serial"] == DBNull.Value ? string.Empty : dr.GetString(dr.GetOrdinal("serial"));
                        vBeStock.User_agr = dr["user_agr"] == DBNull.Value ? string.Empty : dr.GetString(dr.GetOrdinal("user_agr"));
                        vBeStock.User_mod = dr["user_mod"] == DBNull.Value ? string.Empty : dr.GetString(dr.GetOrdinal("user_mod"));

                        // Para campos numéricos - IMPORTANTE: lic_plate parece ser string, no int
                        vBeStock.Lic_plate = dr["lic_plate"] == DBNull.Value ? string.Empty : dr.GetString(dr.GetOrdinal("lic_plate"));

                        lBeStock.Add(vBeStock);
                    }
                }
            }

            return lBeStock;
        }
        catch (Exception)
        {
            throw;
        }
    }
}