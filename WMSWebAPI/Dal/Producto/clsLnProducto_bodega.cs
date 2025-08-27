using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMSWebAPI.AppGlobal;
using WMSWebAPI.Entity.Producto;

public class clsLnProducto_bodega
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    private static SqlCommand CrearComando(string sp, SqlConnection conn, SqlTransaction? tran, clsBeProducto_bodega p)
    {
        var cmd = new SqlCommand(sp, conn, tran) { CommandType = CommandType.Text };

        cmd.Parameters.AddRange(new[]
        {
            new SqlParameter("@IdProductoBodega", p.IdProductoBodega),
            new SqlParameter("@IdProducto", p.IdProducto),
            new SqlParameter("@IdBodega", p.IdBodega),
            new SqlParameter("@activo", p.Activo),
            new SqlParameter("@sistema", p.Sistema),
            new SqlParameter("@user_agr", p.User_agr),
            new SqlParameter("@fec_agr", p.Fec_agr),
            new SqlParameter("@user_mod", p.User_mod),
            new SqlParameter("@fec_mod", p.Fec_mod)
        });

        return cmd;
    }
    public static void Cargar(ref clsBeProducto_bodega o, DataRow dr)
    {
        try
        {
            o.IdProductoBodega = dr.Field<int>("IdProductoBodega");
            o.IdProducto = dr.Field<int>("IdProducto");
            o.IdBodega = dr.Field<int>("IdBodega");
            o.Activo = dr.Field<bool>("activo");
            o.Sistema = dr.Field<bool>("sistema");
            o.User_agr = dr.Field<string>("user_agr") ?? "";
            o.Fec_agr = dr.Field<DateTime>("fec_agr");
            o.User_mod = dr.Field<string>("user_mod") ?? "";
            o.Fec_mod = dr.Field<DateTime>("fec_mod");
        }
        catch (Exception ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
    }
    public static int Insertar(IConfiguration config, clsBeProducto_bodega p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        Ins.Init("producto_bodega");
        Ins.Add("idproductobodega", "@idproductobodega", "F");
        Ins.Add("idproducto", "@idproducto", "F");
        Ins.Add("idbodega", "@idbodega", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("sistema", "@sistema", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");

        string sql = Ins.SQL();
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        using var conn = remoto ? extConn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? tran = null;

        try
        {
            if (!remoto) conn.Open();
            tran ??= remoto ? extTran! : conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = CrearComando(sql, conn, tran, p);
            rows = cmd.ExecuteNonQuery();

            if (!remoto) tran.Commit();
        }
        catch (SqlException ex)
        {
            if (!remoto) tran?.Rollback();
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
        return rows;
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_bodega p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        Upd.Init("producto_bodega");
        Upd.Add("idproductobodega", "@idproductobodega", "F");
        Upd.Add("idproducto", "@idproducto", "F");
        Upd.Add("idbodega", "@idbodega", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("sistema", "@sistema", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Where("IdProductoBodega = @IdProductoBodega");

        string sql = Upd.SQL();
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        using var conn = remoto ? extConn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? tran = null;

        try
        {
            if (!remoto) conn.Open();
            tran ??= remoto ? extTran! : conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = CrearComando(sql, conn, tran, p);
            rows = cmd.ExecuteNonQuery();

            if (!remoto) tran.Commit();
        }
        catch (SqlException ex)
        {
            if (!remoto) tran?.Rollback();
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
        return rows;
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_bodega p)
    {
        const string sql = "SELECT * FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, conn, tran) { CommandType = CommandType.Text };
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", p.IdProductoBodega));

        using var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);
        tran.Commit();

        if (dt.Rows.Count == 1)
        {
            Cargar(ref p, dt.Rows[0]);
            return true;
        }
        return false;
    }
    public static int MaxID(IConfiguration config)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoBodega), 0) FROM Producto_bodega";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, conn, tran);
        var result = cmd.ExecuteScalar();
        tran.Commit();

        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
    }
    public static int Eliminar(IConfiguration config, clsBeProducto_bodega p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        const string sql = "DELETE FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        using var conn = remoto ? extConn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? tran = null;

        try
        {
            if (!remoto) conn.Open();
            tran ??= remoto ? extTran! : conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", p.IdProductoBodega));
            rows = cmd.ExecuteNonQuery();

            if (!remoto) tran.Commit();
        }
        catch (SqlException ex)
        {
            if (!remoto) tran?.Rollback();
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }

        return rows;
    }
    public static bool Existe(int IdProductoBodega, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", IdProductoBodega));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethod = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethod?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_bodega entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        using var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            bool existe = Existe(entity.IdProductoBodega, connection, isExternalTx ? tx! : localTx!);

            return existe
                ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                : Insertar(config, entity, connection, isExternalTx ? tx : localTx);
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
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoBodega), 0) FROM Producto_bodega";
        bool externa = conn != null && tx != null;

        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;

        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            var result = cmd.ExecuteScalar();
            if (!externa) lTx?.Commit();
            return Convert.ToInt32(result);
        }
        catch
        {
            if (!externa) lTx?.Rollback();
            throw;
        }
        finally
        {
            if (!externa) lConn.Close();
        }
    }
    public static List<clsBeProducto_bodega> GetAll(IConfiguration configuration)
    {
        const string sql = "SELECT * FROM Producto_bodega";
        var resultList = new List<clsBeProducto_bodega>();

        using var connection = new SqlConnection(configuration.GetConnectionString("CST"));
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, connection, transaction) { CommandType = CommandType.Text };
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var row = reader.ToDataRow();
            var item = new clsBeProducto_bodega();
            Cargar(ref item, row);
            resultList.Add(item);
        }

        transaction.Commit();
        return resultList;
    }
    public static int InsertarOActualizar(IConfiguration config, List<clsBeProducto_bodega> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        int total = 0;

        using var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
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
                bool existe = Existe(entity.IdProductoBodega, connection, isExternalTx ? tx! : localTx!);

                int result = existe
                    ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                    : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

                total += result;
            }

            if (!isExternalTx && localTx is not null)
                localTx.Commit();

            return total;
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
}