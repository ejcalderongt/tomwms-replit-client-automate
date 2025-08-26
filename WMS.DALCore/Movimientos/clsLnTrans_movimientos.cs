using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Stock;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_movimientos
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_movimientos oBeTrans_movimientos, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_movimientos.IdMovimiento = GetInt("IdMovimiento");
            oBeTrans_movimientos.IdEmpresa = GetInt("IdEmpresa");
            oBeTrans_movimientos.IdBodegaOrigen = GetInt("IdBodegaOrigen");
            oBeTrans_movimientos.IdTransaccion = GetInt("IdTransaccion");
            oBeTrans_movimientos.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_movimientos.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_movimientos.IdUbicacionOrigen = GetInt("IdUbicacionOrigen");
            oBeTrans_movimientos.IdUbicacionDestino = GetInt("IdUbicacionDestino");
            oBeTrans_movimientos.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_movimientos.IdEstadoOrigen = GetInt("IdEstadoOrigen");
            oBeTrans_movimientos.IdEstadoDestino = GetInt("IdEstadoDestino");
            oBeTrans_movimientos.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeTrans_movimientos.IdTipoTarea = GetInt("IdTipoTarea");
            oBeTrans_movimientos.IdBodegaDestino = GetInt("IdBodegaDestino");
            oBeTrans_movimientos.IdRecepcion = GetInt("IdRecepcion");
            oBeTrans_movimientos.Cantidad = GetDouble("cantidad");
            oBeTrans_movimientos.Serie = GetString("serie");
            oBeTrans_movimientos.Peso = GetDouble("peso");
            oBeTrans_movimientos.Lote = GetString("lote");
            oBeTrans_movimientos.Fecha_vence = GetDate("fecha_vence");
            oBeTrans_movimientos.Fecha = GetDate("fecha");
            oBeTrans_movimientos.Barra_pallet = GetString("barra_pallet");
            oBeTrans_movimientos.Hora_ini = GetDate("hora_ini");
            oBeTrans_movimientos.Hora_fin = GetDate("hora_fin");
            oBeTrans_movimientos.Fecha_agr = GetDate("fecha_agr");
            oBeTrans_movimientos.Usuario_agr = GetString("usuario_agr");
            oBeTrans_movimientos.Cantidad_hist = GetDouble("cantidad_hist");
            oBeTrans_movimientos.Peso_hist = GetDouble("peso_hist");
            oBeTrans_movimientos.Lic_plate = GetString("lic_plate");
            oBeTrans_movimientos.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeTrans_movimientos.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeTrans_movimientos.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_movimientos.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_movimientos.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeTrans_movimientos.IdDespachoDet = GetInt("IdDespachoDet");
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

    public static int Insertar(IConfiguration config, clsBeTrans_movimientos oBeTrans_movimientos, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_movimientos");
            Ins.Add("idmovimiento", "@idmovimiento", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodegaorigen", "@idbodegaorigen", "F");
            Ins.Add("idtransaccion", "@idtransaccion", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idubicacionorigen", "@idubicacionorigen", "F");
            Ins.Add("idubicaciondestino", "@idubicaciondestino", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idestadoorigen", "@idestadoorigen", "F");
            Ins.Add("idestadodestino", "@idestadodestino", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idtipotarea", "@idtipotarea", "F");
            Ins.Add("idbodegadestino", "@idbodegadestino", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("serie", "@serie", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("barra_pallet", "@barra_pallet", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("fecha_agr", "@fecha_agr", "F");
            Ins.Add("usuario_agr", "@usuario_agr", "F");
            Ins.Add("cantidad_hist", "@cantidad_hist", "F");
            Ins.Add("peso_hist", "@peso_hist", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("iddespachodet", "@iddespachodet", "F");

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

            BindMovimientoParameters(cmd, oBeTrans_movimientos);

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

    public static int Insertar(IConfiguration config, clsBeTrans_movimientos oBeTrans_movimientos)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_movimientos");
            Ins.Add("idmovimiento", "@idmovimiento", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodegaorigen", "@idbodegaorigen", "F");
            Ins.Add("idtransaccion", "@idtransaccion", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idubicacionorigen", "@idubicacionorigen", "F");
            Ins.Add("idubicaciondestino", "@idubicaciondestino", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idestadoorigen", "@idestadoorigen", "F");
            Ins.Add("idestadodestino", "@idestadodestino", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idtipotarea", "@idtipotarea", "F");
            Ins.Add("idbodegadestino", "@idbodegadestino", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("serie", "@serie", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("barra_pallet", "@barra_pallet", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("fecha_agr", "@fecha_agr", "F");
            Ins.Add("usuario_agr", "@usuario_agr", "F");
            Ins.Add("cantidad_hist", "@cantidad_hist", "F");
            Ins.Add("peso_hist", "@peso_hist", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("iddespachodet", "@iddespachodet", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindMovimientoParameters(cmd, oBeTrans_movimientos);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_movimientos oBeTrans_movimientos, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_movimientos");
            Upd.Add("idmovimiento", "@idmovimiento", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idbodegaorigen", "@idbodegaorigen", "F");
            Upd.Add("idtransaccion", "@idtransaccion", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idubicacionorigen", "@idubicacionorigen", "F");
            Upd.Add("idubicaciondestino", "@idubicaciondestino", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idestadoorigen", "@idestadoorigen", "F");
            Upd.Add("idestadodestino", "@idestadodestino", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idtipotarea", "@idtipotarea", "F");
            Upd.Add("idbodegadestino", "@idbodegadestino", "F");
            Upd.Add("idrecepcion", "@idrecepcion", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("serie", "@serie", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("fecha", "@fecha", "F");
            Upd.Add("barra_pallet", "@barra_pallet", "F");
            Upd.Add("hora_ini", "@hora_ini", "F");
            Upd.Add("hora_fin", "@hora_fin", "F");
            Upd.Add("fecha_agr", "@fecha_agr", "F");
            Upd.Add("usuario_agr", "@usuario_agr", "F");
            Upd.Add("cantidad_hist", "@cantidad_hist", "F");
            Upd.Add("peso_hist", "@peso_hist", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Upd.Add("idrecepciondet", "@idrecepciondet", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("iddespachoenc", "@iddespachoenc", "F");
            Upd.Add("iddespachodet", "@iddespachodet", "F");
            Upd.Where("IdMovimiento = @IdMovimiento" +
                " AND IdEmpresa = @IdEmpresa" +
                " AND IdBodegaOrigen = @IdBodegaOrigen" +
                " AND IdTransaccion = @IdTransaccion");

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

            BindMovimientoParameters(cmd, oBeTrans_movimientos);

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

    public int Eliminar(IConfiguration config, clsBeTrans_movimientos oBeTrans_movimientos, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_movimientos" +
             "  Where(IdMovimiento = @IdMovimiento)" +
             "  And (IdEmpresa = @IdEmpresa)" +
             "  And (IdBodegaOrigen = @IdBodegaOrigen)" +
             "  And (IdTransaccion = @IdTransaccion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdMovimiento", oBeTrans_movimientos.IdMovimiento));

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
            const string sp = "Select * FROM Trans_movimientos";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_movimientos pBeTrans_movimientos)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_movimientos" +
            " Where(IdMovimiento = @IdMovimiento)" +
            " And (IdEmpresa = @IdEmpresa)" +
            " And (IdBodegaOrigen = @IdBodegaOrigen)" +
            " And (IdTransaccion = @IdTransaccion)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            BindMovimientoParameters(cmd, pBeTrans_movimientos);

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_movimientos, r);
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

    public static List<clsBeTrans_movimientos> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_movimientos> lreturnList = new List<clsBeTrans_movimientos>();

        try
        {
            const string sp = "Select * FROM Trans_movimientos";

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

                        clsBeTrans_movimientos vBeTrans_movimientos = new clsBeTrans_movimientos();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_movimientos = new clsBeTrans_movimientos();
                            Cargar(ref vBeTrans_movimientos, dr);
                            lreturnList.Add(vBeTrans_movimientos);
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

            const string sp = "Select ISNULL(Max(IdMovimiento),0) FROM Trans_movimientos";

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


            const string sp = "Select ISNULL(Max(IdMovimiento),0) FROM Trans_movimientos";

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
    public static void BindMovimientoParameters(SqlCommand cmd, clsBeTrans_movimientos oBeTrans_movimientos)
    {
        cmd.Parameters.AddWithValue("@IdMovimiento", oBeTrans_movimientos.IdMovimiento);
        cmd.Parameters.AddWithValue("@IdEmpresa", oBeTrans_movimientos.IdEmpresa);
        cmd.Parameters.AddWithValue("@IdBodegaOrigen", oBeTrans_movimientos.IdBodegaOrigen);
        cmd.Parameters.AddWithValue("@IdTransaccion", oBeTrans_movimientos.IdTransaccion == 0 ? DBNull.Value : oBeTrans_movimientos.IdTransaccion);
        cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBeTrans_movimientos.IdPropietarioBodega == 0 ? DBNull.Value : oBeTrans_movimientos.IdPropietarioBodega);
        cmd.Parameters.AddWithValue("@IdProductoBodega", oBeTrans_movimientos.IdProductoBodega == 0 ? DBNull.Value : oBeTrans_movimientos.IdProductoBodega);
        cmd.Parameters.AddWithValue("@IdUbicacionOrigen", oBeTrans_movimientos.IdUbicacionOrigen == 0 ? DBNull.Value : oBeTrans_movimientos.IdUbicacionOrigen);
        cmd.Parameters.AddWithValue("@IdUbicacionDestino", oBeTrans_movimientos.IdUbicacionDestino == 0 ? DBNull.Value : oBeTrans_movimientos.IdUbicacionDestino);
        cmd.Parameters.AddWithValue("@IdPresentacion", oBeTrans_movimientos.IdPresentacion == 0 ? DBNull.Value : oBeTrans_movimientos.IdPresentacion);
        cmd.Parameters.AddWithValue("@IdEstadoOrigen", oBeTrans_movimientos.IdEstadoOrigen == 0 ? DBNull.Value : oBeTrans_movimientos.IdEstadoOrigen);
        cmd.Parameters.AddWithValue("@IdEstadoDestino", oBeTrans_movimientos.IdEstadoDestino == 0 ? DBNull.Value : oBeTrans_movimientos.IdEstadoDestino);
        cmd.Parameters.AddWithValue("@IdUnidadMedida", oBeTrans_movimientos.IdUnidadMedida == 0 ? DBNull.Value : oBeTrans_movimientos.IdUnidadMedida);
        cmd.Parameters.AddWithValue("@IdTipoTarea", oBeTrans_movimientos.IdTipoTarea == 0 ? DBNull.Value : oBeTrans_movimientos.IdTipoTarea);
        cmd.Parameters.AddWithValue("@IdBodegaDestino", oBeTrans_movimientos.IdBodegaDestino == 0 ? DBNull.Value : oBeTrans_movimientos.IdBodegaDestino);
        cmd.Parameters.AddWithValue("@IdRecepcion", oBeTrans_movimientos.IdRecepcion == 0 ? DBNull.Value : oBeTrans_movimientos.IdRecepcion);

        cmd.Parameters.AddWithValue("@cantidad", oBeTrans_movimientos.Cantidad);
        cmd.Parameters.AddWithValue("@serie", string.IsNullOrEmpty(oBeTrans_movimientos.Serie) ? DBNull.Value : oBeTrans_movimientos.Serie);
        cmd.Parameters.AddWithValue("@peso", oBeTrans_movimientos.Peso);
        cmd.Parameters.AddWithValue("@lote", string.IsNullOrEmpty(oBeTrans_movimientos.Lote) ? DBNull.Value : oBeTrans_movimientos.Lote);
        cmd.Parameters.AddWithValue("@fecha_vence", oBeTrans_movimientos.Fecha_vence == DateTime.MinValue ? DBNull.Value : oBeTrans_movimientos.Fecha_vence);
        cmd.Parameters.AddWithValue("@fecha", oBeTrans_movimientos.Fecha == DateTime.MinValue ? DBNull.Value : oBeTrans_movimientos.Fecha);
        cmd.Parameters.AddWithValue("@barra_pallet", string.IsNullOrEmpty(oBeTrans_movimientos.Barra_pallet) ? DBNull.Value : oBeTrans_movimientos.Barra_pallet);
        cmd.Parameters.AddWithValue("@hora_ini", oBeTrans_movimientos.Hora_ini == DateTime.MinValue ? DBNull.Value : oBeTrans_movimientos.Hora_ini);
        cmd.Parameters.AddWithValue("@hora_fin", oBeTrans_movimientos.Hora_fin == DateTime.MinValue ? DBNull.Value : oBeTrans_movimientos.Hora_fin);
        cmd.Parameters.AddWithValue("@fecha_agr", oBeTrans_movimientos.Fecha_agr == DateTime.MinValue ? DBNull.Value : oBeTrans_movimientos.Fecha_agr);
        cmd.Parameters.AddWithValue("@usuario_agr", string.IsNullOrEmpty(oBeTrans_movimientos.Usuario_agr) ? DBNull.Value : oBeTrans_movimientos.Usuario_agr);
        cmd.Parameters.AddWithValue("@cantidad_hist", oBeTrans_movimientos.Cantidad_hist);
        cmd.Parameters.AddWithValue("@peso_hist", oBeTrans_movimientos.Peso_hist);
        cmd.Parameters.AddWithValue("@lic_plate", string.IsNullOrEmpty(oBeTrans_movimientos.Lic_plate) ? DBNull.Value : oBeTrans_movimientos.Lic_plate);

        cmd.Parameters.AddWithValue("@IdOperadorBodega", oBeTrans_movimientos.IdOperadorBodega == 0 ? DBNull.Value : oBeTrans_movimientos.IdOperadorBodega);
        cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeTrans_movimientos.IdRecepcionDet == 0 ? DBNull.Value : oBeTrans_movimientos.IdRecepcionDet);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", oBeTrans_movimientos.IdPedidoEnc == 0 ? DBNull.Value : oBeTrans_movimientos.IdPedidoEnc);
        cmd.Parameters.AddWithValue("@IdPedidoDet", oBeTrans_movimientos.IdPedidoDet == 0 ? DBNull.Value : oBeTrans_movimientos.IdPedidoDet);
        cmd.Parameters.AddWithValue("@IdDespachoEnc", oBeTrans_movimientos.IdDespachoEnc == 0 ? DBNull.Value : oBeTrans_movimientos.IdDespachoEnc);
        cmd.Parameters.AddWithValue("@IdDespachoDet", oBeTrans_movimientos.IdDespachoDet == 0 ? DBNull.Value : oBeTrans_movimientos.IdDespachoDet);
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeTrans_movimientos> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                bool existe = Existe(entity.IdMovimiento, connection, isExternalTx ? tx! : localTx!);

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
    public static bool Existe(int idMovimiento, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM Trans_movimientos WHERE IdMovimiento = @Id", conn, tx);
        cmd.Parameters.AddWithValue("@Id", idMovimiento);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

}