using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Picking;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_picking_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_picking_det oBeTrans_picking_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_picking_det.IdPickingDet = GetInt("IdPickingDet");
            oBeTrans_picking_det.IdPickingEnc = GetInt("IdPickingEnc");
            oBeTrans_picking_det.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_picking_det.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_picking_det.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeTrans_picking_det.Cantidad = GetDecimal("cantidad");
            oBeTrans_picking_det.Cliente_dias = GetInt("cliente_dias");
            oBeTrans_picking_det.Cantidad_recibida = GetDecimal("cantidad_recibida");
            oBeTrans_picking_det.User_agr = GetString("user_agr");
            oBeTrans_picking_det.Fec_agr = GetDate("fec_agr");
            oBeTrans_picking_det.User_mod = GetString("user_mod");
            oBeTrans_picking_det.Fec_mod = GetDate("fec_mod");
            oBeTrans_picking_det.Activo = GetBool("activo");
            oBeTrans_picking_det.Codigo = GetString("codigo");
            oBeTrans_picking_det.Nombre = GetString("nombre");
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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_det oBeTrans_picking_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {        
        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_det");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("cliente_dias", "@cliente_dias", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");

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

            Bind(cmd, oBeTrans_picking_det);

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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_det oBeTrans_picking_det)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_det");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("cliente_dias", "@cliente_dias", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_picking_det);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_picking_det oBeTrans_picking_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_picking_det");
            Upd.Add("idpickingdet", "@idpickingdet", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("cliente_dias", "@cliente_dias", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Where("IdPickingDet = @IdPickingDet");

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

            Bind(cmd, oBeTrans_picking_det);

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

    public int Eliminar(IConfiguration config, clsBeTrans_picking_det oBeTrans_picking_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_picking_det" +
             "  Where(IdPickingDet = @IdPickingDet)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPickingDet", oBeTrans_picking_det.IdPickingDet));

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
            const string sp = "Select * FROM Trans_picking_det";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_picking_det pBeTrans_picking_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_picking_det" +
            " Where(IdPickingDet = @IdPickingDet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingDet", pBeTrans_picking_det.IdPickingDet));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_picking_det, r);
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

    public static List<clsBeTrans_picking_det> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_picking_det> lreturnList = new List<clsBeTrans_picking_det>();

        try
        {
            const string sp = "Select * FROM Trans_picking_det";

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

                        clsBeTrans_picking_det vBeTrans_picking_det = new clsBeTrans_picking_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_picking_det = new clsBeTrans_picking_det();
                            Cargar(ref vBeTrans_picking_det, dr);
                            lreturnList.Add(vBeTrans_picking_det);
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

            const string sp = "Select ISNULL(Max(IdPickingDet),0) FROM Trans_picking_det";

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


            const string sp = "Select ISNULL(Max(IdPickingDet),0) FROM Trans_picking_det";

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
    public static void Bind(SqlCommand cmd, clsBeTrans_picking_det o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPickingDet", o.IdPickingDet != 0 ? o.IdPickingDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", o.IdPickingEnc != 0 ? o.IdPickingEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", o.IdOperadorBodega == 0 ? DBNull.Value : o.IdOperadorBodega)); //GT27062025 enviar null si operador es 0 
        cmd.Parameters.Add(new SqlParameter("@cantidad", o.Cantidad != 0 ? o.Cantidad : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cliente_dias", o.Cliente_dias != 0 ? o.Cliente_dias : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", o.Cantidad_recibida != 0 ? o.Cantidad_recibida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@codigo", !string.IsNullOrWhiteSpace(o.Codigo) ? o.Codigo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre", !string.IsNullOrWhiteSpace(o.Nombre) ? o.Nombre : DBNull.Value));
    }
    public static int InsertOrUpdate(IConfiguration config, List<clsBeTrans_picking_det> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                bool existe = Existe(entity.IdPickingDet, entity.IdPickingEnc, connection, isExternalTx ? tx! : localTx!);

                int result = existe
                    ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                    : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

                total += result;
            }

            if (!isExternalTx)
                localTx?.Commit();

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
                connection.Close();
                connection.Dispose();
                localTx?.Dispose();
            }
        }
    }
    public static bool Existe(int idPickingDet, int idPickingEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_picking_det
        WHERE IdPickingDet = @IdPickingDet AND IdPickingEnc = @IdPickingEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPickingDet", idPickingDet);
        cmd.Parameters.AddWithValue("@IdPickingEnc", idPickingEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }
}
