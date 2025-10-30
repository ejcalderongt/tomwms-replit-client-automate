using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Proveedor;
using Microsoft.Extensions.Configuration;
public class clsLnProveedor_bodega
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProveedor_bodega oBeProveedor_bodega, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeProveedor_bodega.IdAsignacion = GetInt("IdAsignacion");
            oBeProveedor_bodega.IdProveedor = GetInt("IdProveedor");
            oBeProveedor_bodega.IdBodega = GetInt("IdBodega");
            oBeProveedor_bodega.Activo = GetBool("activo");
            oBeProveedor_bodega.User_agr = GetString("user_agr");
            oBeProveedor_bodega.Fec_agr = GetDate("fec_agr");
            oBeProveedor_bodega.User_mod = GetString("user_mod");
            oBeProveedor_bodega.Fec_mod = GetDate("fec_mod");
            oBeProveedor_bodega.IdAreaOrigen = GetInt("IdAreaOrigen");
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

    public static int Insertar(clsBeProveedor_bodega oBeProveedor_bodega, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeProveedor_bodega == null)
            throw new ArgumentNullException(nameof(oBeProveedor_bodega));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("proveedor_bodega");
            Ins.Add("idasignacion", "@idasignacion", "F");
            Ins.Add("idproveedor", "@idproveedor", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("idareaorigen", "@idareaorigen", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeProveedor_bodega);

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

    public static int Insertar(IConfiguration config, clsBeProveedor_bodega oBeProveedor_bodega)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("proveedor_bodega");
            Ins.Add("idasignacion", "@idasignacion", "F");
            Ins.Add("idproveedor", "@idproveedor", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("idareaorigen", "@idareaorigen", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeProveedor_bodega);

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

    public static void Bind(SqlCommand cmd, clsBeProveedor_bodega oBeProveedor_bodega)
    {
        cmd.Parameters.Add(new SqlParameter("@IdAsignacion", oBeProveedor_bodega.IdAsignacion == 0 ? DBNull.Value : oBeProveedor_bodega.IdAsignacion));
        cmd.Parameters.Add(new SqlParameter("@IdProveedor", oBeProveedor_bodega.IdProveedor == 0 ? DBNull.Value : oBeProveedor_bodega.IdProveedor));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeProveedor_bodega.IdBodega == 0 ? DBNull.Value : oBeProveedor_bodega.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeProveedor_bodega.Activo));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeProveedor_bodega.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeProveedor_bodega.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeProveedor_bodega.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeProveedor_bodega.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@IdAreaOrigen", oBeProveedor_bodega.IdAreaOrigen == 0 ? DBNull.Value : oBeProveedor_bodega.IdAreaOrigen)); //GT27062025 enviar null 
    }

    public static int Actualizar(clsBeProveedor_bodega oBeProveedor_bodega, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeProveedor_bodega == null)
            throw new ArgumentNullException(nameof(oBeProveedor_bodega));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Upd.Init("proveedor_bodega");
            Upd.Add("idasignacion", "@idasignacion", "F");
            Upd.Add("idproveedor", "@idproveedor", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("idareaorigen", "@idareaorigen", "F");
            Upd.Where("IdAsignacion = @IdAsignacion");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeProveedor_bodega);

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

    public int Eliminar(IConfiguration config, clsBeProveedor_bodega oBeProveedor_bodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Proveedor_bodega" +
             "  Where(IdAsignacion = @IdAsignacion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdAsignacion", oBeProveedor_bodega.IdAsignacion));

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
            const string sp = "Select * FROM Proveedor_bodega";
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

    public static bool GetSingle(IConfiguration config, ref clsBeProveedor_bodega pBeProveedor_bodega)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Proveedor_bodega" +
            " Where(IdAsignacion = @IdAsignacion)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            Bind(cmd, pBeProveedor_bodega);

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProveedor_bodega, r);
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

    public static List<clsBeProveedor_bodega> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProveedor_bodega> lreturnList = new List<clsBeProveedor_bodega>();

        try
        {
            const string sp = "Select * FROM Proveedor_bodega";

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

                        clsBeProveedor_bodega vBeProveedor_bodega = new clsBeProveedor_bodega();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProveedor_bodega = new clsBeProveedor_bodega();
                            Cargar(ref vBeProveedor_bodega, dr);
                            lreturnList.Add(vBeProveedor_bodega);
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

            const string sp = "Select ISNULL(Max(IdAsignacion),0) FROM Proveedor_bodega";

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
    public static int MaxID(SqlConnection? lConnection = null, SqlTransaction? pTransaction = null)
    {
                
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdAsignacion),0) FROM Proveedor_bodega";            
            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };
            cmd = new SqlCommand(sp, lConnection, pTransaction);
            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
            }
            
            return lMax;

        }
        catch (SqlException ex1)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            throw new Exception(vMsgError);
        }
    }

    public static void InsertarOActualizar(List<clsBeProveedor_bodega> entities, SqlConnection conn, SqlTransaction tx)
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

                if (entity.IdAsignacion != 0)
                {
                    bool existe = Existe(entity.IdAsignacion, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }

    public static bool Existe(int IdAsignacion, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM proveedor_bodega WHERE IdAsignacion = @IdAsignacion";

            using (SqlCommand cmd = new SqlCommand(query, conn, tx))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdAsignacion", IdAsignacion));

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

    public static bool Get_Single_By_IdBodega_And_IdProveedor(ref clsBeProveedor_bodega pBeProveedor_bodega,
                                                             SqlConnection lConnection,
                                                             SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            const string sp = @"SELECT * FROM Proveedor_bodega 
                            WHERE (IdBodega = @IdBodega) 
                            AND (IdProveedor = @Idproveedor) 
                            AND Activo = 1";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPROVEEDOR", pBeProveedor_bodega.IdProveedor));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDBODEGA", pBeProveedor_bodega.IdBodega));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Cargar(ref pBeProveedor_bodega, dt.Rows[0]);
                result = true;
            }            

            return result;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static bool Obtener(clsBeProveedor_bodega oBeProveedor_bodega,
                              SqlConnection lConnection,
                              SqlTransaction lTransaction)
    {
        try
        {
            string sp = @"SELECT * FROM Proveedor_bodega
                     WHERE IdAsignacion = @IdAsignacion";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion);

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeProveedor_bodega, dt.Rows[0]);
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
}