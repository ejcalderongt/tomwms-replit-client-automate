using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Picking;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_picking_ubic_stock
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_picking_ubic_stock oBeTrans_picking_ubic_stock, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_picking_ubic_stock.IdPickingUbicStock = GetInt("IdPickingUbicStock");
            oBeTrans_picking_ubic_stock.IdBodega = GetInt("IdBodega");
            oBeTrans_picking_ubic_stock.IdPickingUbic = GetInt("IdPickingUbic");
            oBeTrans_picking_ubic_stock.IdPickingDet = GetInt("IdPickingDet");
            oBeTrans_picking_ubic_stock.IdUbicacion = GetInt("IdUbicacion");
            oBeTrans_picking_ubic_stock.IdStock = GetInt("IdStock");
            oBeTrans_picking_ubic_stock.IdStockRes = GetInt("IdStockRes");
            oBeTrans_picking_ubic_stock.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_picking_ubic_stock.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_picking_ubic_stock.IdProductoEstado = GetInt("IdProductoEstado");
            oBeTrans_picking_ubic_stock.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_picking_ubic_stock.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeTrans_picking_ubic_stock.IdUbicacionAnterior = GetInt("IdUbicacionAnterior");
            oBeTrans_picking_ubic_stock.IdRecepcion = GetInt("IdRecepcion");
            oBeTrans_picking_ubic_stock.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_picking_ubic_stock.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_picking_ubic_stock.Idpickingenc = GetInt("idpickingenc");
            oBeTrans_picking_ubic_stock.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeTrans_picking_ubic_stock.IdOperadorBodega_Pickeo = GetInt("IdOperadorBodega_Pickeo");
            oBeTrans_picking_ubic_stock.IdOperadorBodega_Verifico = GetInt("IdOperadorBodega_Verifico");
            oBeTrans_picking_ubic_stock.Lote = GetString("lote");
            oBeTrans_picking_ubic_stock.Fecha_vence = GetDate("fecha_vence");
            oBeTrans_picking_ubic_stock.Fecha_minima = GetDate("fecha_minima");
            oBeTrans_picking_ubic_stock.Serial = GetString("serial");
            oBeTrans_picking_ubic_stock.Licencia = GetString("licencia");
            oBeTrans_picking_ubic_stock.Cantidad_recibida = GetDecimal("cantidad_recibida");
            oBeTrans_picking_ubic_stock.Cantidad_verificada = GetDecimal("cantidad_verificada");
            oBeTrans_picking_ubic_stock.Fecha_picking = GetDate("fecha_picking");
            oBeTrans_picking_ubic_stock.Fecha_verificado = GetDate("fecha_verificado");
            oBeTrans_picking_ubic_stock.Fecha_despachado = GetDate("fecha_despachado");
            oBeTrans_picking_ubic_stock.Cantidad_despachada = GetDecimal("cantidad_despachada");
            oBeTrans_picking_ubic_stock.User_agr = GetString("user_agr");
            oBeTrans_picking_ubic_stock.Fec_agr = GetDate("fec_agr");
            oBeTrans_picking_ubic_stock.User_mod = GetString("user_mod");
            oBeTrans_picking_ubic_stock.Fec_mod = GetDate("fec_mod");
            oBeTrans_picking_ubic_stock.Activo = GetBool("activo");
            oBeTrans_picking_ubic_stock.IdUbicacionTemporal = GetInt("IdUbicacionTemporal");
            oBeTrans_picking_ubic_stock.IdOperadorBodega_Asignado = GetInt("IdOperadorBodega_Asignado");
            oBeTrans_picking_ubic_stock.Cantidad_pickeada = GetDecimal("cantidad_pickeada");
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

    public static int Insertar(clsBeTrans_picking_ubic_stock oBeTrans_picking_ubic_stock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        bool Es_Transaccion_Remota = pTransaction != null;
        SqlTransaction? localTx = null;

        try
        {
            Ins.Init("trans_picking_ubic_stock");
            Ins.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_minima", "@fecha_minima", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("licencia", "@licencia", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("fecha_verificado", "@fecha_verificado", "F");
            Ins.Add("fecha_despachado", "@fecha_despachado", "F");
            Ins.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");
            Ins.Add("cantidad_pickeada", "@cantidad_pickeada", "F");

            string sp = Ins.SQL();

            if (!Es_Transaccion_Remota)
            {
                localTx = pConection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            SqlCommand cmd = Es_Transaccion_Remota
                ? new SqlCommand(sp, pConection, pTransaction)
                : new SqlCommand(sp, pConection, localTx);

            Bind(cmd, oBeTrans_picking_ubic_stock);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();

            if (!Es_Transaccion_Remota)
                localTx?.Commit();
        }
        catch (SqlException ex1)
        {
            if (!Es_Transaccion_Remota)
                localTx?.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (!Es_Transaccion_Remota)
            {
                localTx?.Dispose();
            }
        }
        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeTrans_picking_ubic_stock oBeTrans_picking_ubic_stock)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_ubic_stock");
            Ins.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_minima", "@fecha_minima", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("licencia", "@licencia", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("fecha_verificado", "@fecha_verificado", "F");
            Ins.Add("fecha_despachado", "@fecha_despachado", "F");
            Ins.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");
            Ins.Add("cantidad_pickeada", "@cantidad_pickeada", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_picking_ubic_stock);

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

    public static int Actualizar(clsBeTrans_picking_ubic_stock oBeTrans_picking_ubic_stock, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        bool Es_Transaccion_Remota = pTransaction != null;
        SqlTransaction? localTx = null;

        try
        {
            Upd.Init("trans_picking_ubic_stock");
            Upd.Add("idpickingubicstock", "@idpickingubicstock", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpickingubic", "@idpickingubic", "F");
            Upd.Add("idpickingdet", "@idpickingdet", "F");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("idstock", "@idstock", "F");
            Upd.Add("idstockres", "@idstockres", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Upd.Add("idrecepcion", "@idrecepcion", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Upd.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Upd.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("fecha_minima", "@fecha_minima", "F");
            Upd.Add("serial", "@serial", "F");
            Upd.Add("licencia", "@licencia", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Upd.Add("fecha_picking", "@fecha_picking", "F");
            Upd.Add("fecha_verificado", "@fecha_verificado", "F");
            Upd.Add("fecha_despachado", "@fecha_despachado", "F");
            Upd.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Upd.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");
            Upd.Add("cantidad_pickeada", "@cantidad_pickeada", "F");
            Upd.Where("IdPickingUbicStock = @IdPickingUbicStock");

            string sp = Upd.SQL();

            if (!Es_Transaccion_Remota)
            {
                localTx = pConection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            SqlCommand cmd = Es_Transaccion_Remota
                ? new SqlCommand(sp, pConection, pTransaction)
                : new SqlCommand(sp, pConection, localTx);

            Bind(cmd, oBeTrans_picking_ubic_stock);

            rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                localTx?.Commit();
        }
        catch (SqlException ex1)
        {
            if (!Es_Transaccion_Remota)
                localTx?.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (!Es_Transaccion_Remota)
            {
                localTx?.Dispose();
            }
        }
        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeTrans_picking_ubic_stock oBeTrans_picking_ubic_stock, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_picking_ubic_stock" +
             "  Where(IdPickingUbicStock = @IdPickingUbicStock)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPickingUbicStock", oBeTrans_picking_ubic_stock.IdPickingUbicStock));

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
            const string sp = "Select * FROM Trans_picking_ubic_stock";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_picking_ubic_stock pBeTrans_picking_ubic_stock)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_picking_ubic_stock" +
            " Where(IdPickingUbicStock = @IdPickingUbicStock)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingUbicStock", pBeTrans_picking_ubic_stock.IdPickingUbicStock));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_picking_ubic_stock, r);
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

    public static List<clsBeTrans_picking_ubic_stock> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_picking_ubic_stock> lreturnList = new List<clsBeTrans_picking_ubic_stock>();

        try
        {
            const string sp = "Select * FROM Trans_picking_ubic_stock";

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

                        clsBeTrans_picking_ubic_stock vBeTrans_picking_ubic_stock = new clsBeTrans_picking_ubic_stock();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_picking_ubic_stock = new clsBeTrans_picking_ubic_stock();
                            Cargar(ref vBeTrans_picking_ubic_stock, dr);
                            lreturnList.Add(vBeTrans_picking_ubic_stock);
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

            const string sp = "Select ISNULL(Max(IdPickingUbicStock),0) FROM Trans_picking_ubic_stock";

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


            const string sp = "Select ISNULL(Max(IdPickingUbicStock),0) FROM Trans_picking_ubic_stock";

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

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
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
    public static void Bind(SqlCommand cmd, clsBeTrans_picking_ubic_stock o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPickingUbicStock", o.IdPickingUbicStock != 0 ? o.IdPickingUbicStock : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", o.IdBodega != 0 ? o.IdBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", o.IdPickingUbic != 0 ? o.IdPickingUbic : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingDet", o.IdPickingDet != 0 ? o.IdPickingDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion", o.IdUbicacion != 0 ? o.IdUbicacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStock", o.IdStock != 0 ? o.IdStock : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStockRes", o.IdStockRes != 0 ? o.IdStockRes : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", o.IdPropietarioBodega != 0 ? o.IdPropietarioBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", o.IdProductoBodega != 0 ? o.IdProductoBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", o.IdProductoEstado != 0 ? o.IdProductoEstado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", o.IdPresentacion != 0 ? o.IdPresentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", o.IdUnidadMedida != 0 ? o.IdUnidadMedida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionAnterior", o.IdUbicacionAnterior != 0 ? o.IdUbicacionAnterior : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcion", o.IdRecepcion != 0 ? o.IdRecepcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@idpickingenc", o.Idpickingenc != 0 ? o.Idpickingenc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", o.IdOperadorBodega != 0 ? o.IdOperadorBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Pickeo", o.IdOperadorBodega_Pickeo != 0 ? o.IdOperadorBodega_Pickeo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Verifico", o.IdOperadorBodega_Verifico != 0 ? o.IdOperadorBodega_Verifico : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lote", !string.IsNullOrWhiteSpace(o.Lote) ? o.Lote : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence", o.Fecha_vence != DateTime.MinValue ? o.Fecha_vence : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_minima", o.Fecha_minima != DateTime.MinValue ? o.Fecha_minima : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@serial", !string.IsNullOrWhiteSpace(o.Serial) ? o.Serial : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@licencia", !string.IsNullOrWhiteSpace(o.Licencia) ? o.Licencia : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", o.Cantidad_recibida != 0 ? o.Cantidad_recibida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_verificada", o.Cantidad_verificada != 0 ? o.Cantidad_verificada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_picking", o.Fecha_picking != DateTime.MinValue ? o.Fecha_picking : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_verificado", o.Fecha_verificado != DateTime.MinValue ? o.Fecha_verificado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_despachado", o.Fecha_despachado != DateTime.MinValue ? o.Fecha_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_despachada", o.Cantidad_despachada != 0 ? o.Cantidad_despachada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionTemporal", o.IdUbicacionTemporal != 0 ? o.IdUbicacionTemporal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Asignado", o.IdOperadorBodega_Asignado != 0 ? o.IdOperadorBodega_Asignado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_pickeada", o.Cantidad_pickeada != 0 ? o.Cantidad_pickeada : DBNull.Value));
    }
    public static int InsertOrUpdate(List<clsBeTrans_picking_ubic_stock> entities, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdPickingUbicStock, entity.IdPickingUbic, conn, tx);

                int result = existe
                    ? Actualizar(entity, conn, tx)
                    : Insertar(entity, conn, tx);

                total += result;
            }

            return total;
        }
        catch
        {
            throw;
        }
    }
    public static bool Existe(int idPickingUbicStock, int idPickingUbic, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_picking_ubic_stock
        WHERE IdPickingUbicStock = @IdPickingUbicStock AND IdPickingUbic = @IdPickingUbic";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPickingUbicStock", idPickingUbicStock);
        cmd.Parameters.AddWithValue("@IdPickingUbic", idPickingUbic);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }
}