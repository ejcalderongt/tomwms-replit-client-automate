using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Propietario;
using Microsoft.Extensions.Configuration;
public class clsLnPropietario_bodega
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBePropietario_bodega oBePropietario_bodega, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBePropietario_bodega.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBePropietario_bodega.IdPropietario = GetInt("IdPropietario");
            oBePropietario_bodega.IdBodega = GetInt("IdBodega");
            oBePropietario_bodega.User_agr = GetString("user_agr");
            oBePropietario_bodega.Fec_agr = GetDate("fec_agr");
            oBePropietario_bodega.User_mod = GetString("user_mod");
            oBePropietario_bodega.Fec_mod = GetDate("fec_mod");
            oBePropietario_bodega.Activo = GetBool("activo");
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
    public static int Insertar(clsBePropietario_bodega oBePropietario_bodega, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("propietario_bodega");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                Bind(cmd, oBePropietario_bodega);
                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (Exception)
        {
           throw;
        }
    }
    public static int Insertar(IConfiguration config, clsBePropietario_bodega oBePropietario_bodega)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("propietario_bodega");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBePropietario_bodega);

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
    public static int Actualizar(clsBePropietario_bodega oBePropietario_bodega, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("propietario_bodega");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdPropietarioBodega = @IdPropietarioBodega");

            string sp = Upd.SQL();

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                Bind(cmd, oBePropietario_bodega);
                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (Exception)
        {            
            throw;
        }
    }
    public int Eliminar(IConfiguration config, clsBePropietario_bodega oBePropietario_bodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Propietario_bodega" +
             "  Where(IdPropietarioBodega = @IdPropietarioBodega)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBePropietario_bodega.IdPropietarioBodega));

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
            const string sp = "Select * FROM Propietario_bodega";
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
    public static bool GetSingle(IConfiguration config, ref clsBePropietario_bodega pBePropietario_bodega)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Propietario_bodega" +
            " Where(IdPropietarioBodega = @IdPropietarioBodega)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietarioBodega", pBePropietario_bodega.IdPropietarioBodega));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBePropietario_bodega, r);
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
    public static List<clsBePropietario_bodega> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBePropietario_bodega> lreturnList = new List<clsBePropietario_bodega>();

        try
        {
            const string sp = "Select * FROM Propietario_bodega";

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

                        clsBePropietario_bodega vBePropietario_bodega = new clsBePropietario_bodega();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBePropietario_bodega = new clsBePropietario_bodega();
                            Cargar(ref vBePropietario_bodega, dr);
                            lreturnList.Add(vBePropietario_bodega);
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

            const string sp = "Select ISNULL(Max(IdPropietarioBodega),0) FROM Propietario_bodega";

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


            const string sp = "Select ISNULL(Max(IdPropietarioBodega),0) FROM Propietario_bodega";

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
    public static void Bind(SqlCommand cmd, clsBePropietario_bodega o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", o.IdPropietarioBodega));
        cmd.Parameters.Add(new SqlParameter("@IdPropietario", o.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", o.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@user_agr", o.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", o.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
    }
    public static int InsertOrUpdate(clsBePropietario_bodega be, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            if (Existe(be.IdPropietarioBodega, conn, tx))
                return Actualizar(be, conn, tx);
            else
                return Insertar(be, conn, tx);
        }
        catch
        {
            throw;
        }
    }
    public static bool Existe(int idPropietarioBodega, SqlConnection connection, SqlTransaction transaction)
    {
        bool exists = false;

        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(idPropietarioBodega) FROM propietario_bodega WHERE idPropietarioBodega = @idPropietarioBodega", connection, transaction))
        {
            cmd.Parameters.AddWithValue("@idPropietarioBodega", idPropietarioBodega);
            exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        return exists;
    }

    //#GT20062025: metodo para manejar listas, el otro metodo recibe solo un objeto.
    public static int InsertOrUpdate(List<clsBePropietario_bodega> be, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var e in be)
            {
                if (Existe(e.IdPropietarioBodega, conn, tx))
                    total += Actualizar(e, conn, tx);
                else
                    total += Insertar(e, conn, tx);
            }

            return total;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Obtener(clsBePropietario_bodega oBePropietario_bodega,
                              SqlConnection lConnection,
                              SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Propietario_bodega 
                           WHERE IdPropietarioBodega = @IdPropietarioBodega";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDPROPIETARIOBODEGA", oBePropietario_bodega.IdPropietarioBodega);

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBePropietario_bodega, dt.Rows[0]);
                        return true;
                    }
                }
            }

            return false;
        }        
        catch (Exception)
        {         
            throw;
        }
    }

    public static int GetIdEmpresa_By_IdPropietarioBodega(int pIdPropietarioBodega,
                                                         SqlConnection lConnection,
                                                         SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT p.IdEmpresa 
                       FROM propietario_bodega AS pb 
                       INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                       WHERE pb.IdPropietarioBodega = @IdPropietarioBodega";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    return Convert.ToInt32(lReturnValue);
                }
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Get_IdPropietario_By_IdBodega_IdPropietarioBodega(int pIdBodega,
                                                                        int pIdPropietarioBodega,
                                                                        SqlConnection lConnection,
                                                                        SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT pb.IdPropietario
                      FROM propietario_bodega AS pb 
                      INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                      WHERE p.activo=1 AND pb.IdBodega=@IdBodega AND pb.IdPropietarioBodega=@IdPropietarioBodega";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    return Convert.ToInt32(lReturnValue);
                }
            }

            return 0;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(int pIdPropietario,
                                                                        int pIdBodega,
                                                                        SqlConnection lConnection,
                                                                        SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = @"SELECT pb.IdPropietarioBodega 
                       FROM propietario_bodega AS pb  
                       INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                       WHERE p.activo=1 AND pb.IdPropietario = @IdPropietario 
                       AND pb.IdBodega = @IdBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    result = Convert.ToInt32(lDataTable.Rows[0]["IdPropietarioBodega"]);
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