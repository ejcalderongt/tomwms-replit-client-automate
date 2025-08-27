using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnTrans_picking_img
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_picking_img oBeTrans_picking_img, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }        

        try
        {
            oBeTrans_picking_img.IdImagen = GetInt("IdImagen");
            oBeTrans_picking_img.IdPickingEnc = GetInt("IdPickingEnc");
            oBeTrans_picking_img.IdPickingDet = GetInt("IdPickingDet");
            oBeTrans_picking_img.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_picking_img.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_picking_img.Imagen = GetBytes("Imagen");
            oBeTrans_picking_img.User_agr = GetString("user_agr");
            oBeTrans_picking_img.Fec_agr = GetDate("fec_agr");
            oBeTrans_picking_img.Observacion = GetString("observacion");
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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_img oBeTrans_picking_img, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_img");
            Ins.Add("idimagen", "@idimagen", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("observacion", "@observacion", "F");

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

            Bind(cmd, oBeTrans_picking_img);

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

    public static int Insertar(IConfiguration config, clsBeTrans_picking_img oBeTrans_picking_img)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_img");
            Ins.Add("idimagen", "@idimagen", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("observacion", "@observacion", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_picking_img);

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

    public static int Actualizar(IConfiguration config, clsBeTrans_picking_img oBeTrans_picking_img, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_picking_img");
            Upd.Add("idimagen", "@idimagen", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idpickingdet", "@idpickingdet", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("imagen", "@imagen", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Where("IdImagen = @IdImagen");

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

            Bind(cmd, oBeTrans_picking_img);

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

    public int Eliminar(IConfiguration config, clsBeTrans_picking_img oBeTrans_picking_img, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_picking_img" +
             "  Where(IdImagen = @IdImagen)");

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

            cmd.Parameters.Add(new SqlParameter("@IdImagen", oBeTrans_picking_img.IdImagen));

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
            const string sp = "Select * FROM Trans_picking_img";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_picking_img pBeTrans_picking_img)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_picking_img" +
            " Where(IdImagen = @IdImagen and IdPickingEnc =@IdPikcingEnc AND IdPickingDet=@IdPickingDet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdImagen", pBeTrans_picking_img.IdImagen));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingEnc", pBeTrans_picking_img.IdPickingEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingDet", pBeTrans_picking_img.IdPickingDet));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_picking_img, r);
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

    public static List<clsBeTrans_picking_img> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_picking_img> lreturnList = new List<clsBeTrans_picking_img>();

        try
        {
            const string sp = "Select * FROM Trans_picking_img";

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

                        clsBeTrans_picking_img vBeTrans_picking_img = new clsBeTrans_picking_img();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_picking_img = new clsBeTrans_picking_img();
                            Cargar(ref vBeTrans_picking_img, dr);
                            lreturnList.Add(vBeTrans_picking_img);
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

            const string sp = "Select ISNULL(Max(IdImagen),0) FROM Trans_picking_img";

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


            const string sp = "Select ISNULL(Max(IdImagen),0) FROM Trans_picking_img";

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
    public static void Bind(SqlCommand cmd, clsBeTrans_picking_img o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdImagen", o.IdImagen != 0 ? o.IdImagen : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", o.IdPickingEnc != 0 ? o.IdPickingEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingDet", o.IdPickingDet != 0 ? o.IdPickingDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        var paramImagen = new SqlParameter("@Imagen", SqlDbType.VarBinary);
        paramImagen.Value = (o.Imagen != null && o.Imagen.Length > 0) ? (object)o.Imagen : DBNull.Value;
        cmd.Parameters.Add(paramImagen);
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@observacion", !string.IsNullOrWhiteSpace(o.Observacion) ? o.Observacion : DBNull.Value));
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeTrans_picking_img entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            if (Existe(entity.IdImagen, entity.IdPickingEnc, connection, isExternalTx ? tx! : localTx!))
                return Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
            else
                return Insertar(config, entity, connection, isExternalTx ? tx : localTx);
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
    public static bool Existe(int idImagen, int idPickingEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_picking_img
        WHERE IdImagen = @IdImagen AND IdPickingEnc = @IdPickingEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdImagen", idImagen);
        cmd.Parameters.AddWithValue("@IdPickingEnc", idPickingEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }
    public static int InsertOrUpdate(IConfiguration config, List<clsBeTrans_picking_img> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                if (entity.IdImagen > 0) {
                    bool existe = Existe(entity.IdImagen, entity.IdPickingEnc, connection, isExternalTx ? tx! : localTx!);

                    int result = existe
                        ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                        : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

                    total += result;
                }                
            }
            if (!isExternalTx)
                localTx?.Commit();

            return total;
        }
        catch
        {
            if (!isExternalTx)
                localTx?.Rollback();
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
}
