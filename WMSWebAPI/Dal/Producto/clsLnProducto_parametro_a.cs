using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMSWebAPI.Entity.Producto;

public class clsLnProductoParametroA
{
    private static readonly clsInsert Inserter = new clsInsert();
    private static readonly clsUpdate Updater = new clsUpdate();

    private static string GetConnectionString(IConfiguration config) =>
        config.GetConnectionString("CST") ?? throw new InvalidOperationException("Cadena de conexión 'CST' no está configurada.");

    private static SqlCommand CreateCommand(string sql, SqlConnection conn, SqlTransaction? tx = null) =>
        new SqlCommand(sql, conn, tx) { CommandType = CommandType.Text };
    public static void Cargar(ref clsBeProducto_parametro_a entity, DataRow row)
    {
        entity.IdProductoParametroA = row.Field<int?>("IdProductoParametroA") ?? 0;
        entity.Codigo = row.Field<string>("Codigo") ?? "";
        entity.Nombre = row.Field<string>("Nombre") ?? "";
        entity.Fec_agr = row.Field<DateTime?>("fec_agr") ?? DateTime.UtcNow;
        entity.User_agr = row.Field<string>("user_agr") ?? "";
        entity.Fec_mod = row.Field<DateTime?>("fec_mod") ?? DateTime.UtcNow;
        entity.User_mod = row.Field<string>("user_mod") ?? "";
        entity.Activo = row.Field<bool?>("activo") ?? false;
    }

    private static void BindParams(SqlCommand cmd, clsBeProducto_parametro_a e)
    {
        cmd.Parameters.AddWithValue("@IdProductoParametroA", e.IdProductoParametroA);
        cmd.Parameters.AddWithValue("@Codigo", e.Codigo);
        cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
    }

    public static int Insert(IConfiguration config, clsBeProducto_parametro_a entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Inserter.Init("producto_parametro_a");
        Inserter.Add("idproductoparametroa", "@idproductoparametroa", "F");
        Inserter.Add("codigo", "@codigo", "F");
        Inserter.Add("nombre", "@nombre", "F");
        Inserter.Add("fec_agr", "@fec_agr", "F");
        Inserter.Add("user_agr", "@user_agr", "F");
        Inserter.Add("fec_mod", "@fec_mod", "F");
        Inserter.Add("user_mod", "@user_mod", "F");
        Inserter.Add("activo", "@activo", "F");

        var sql = Inserter.SQL();
        bool isExternalTx = conn != null && tx != null;

        using var connection = isExternalTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        if (!isExternalTx)
        {
            connection.Open();
            localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = CreateCommand(sql, connection, isExternalTx ? tx : localTx);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();

            if (!isExternalTx)
                localTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!isExternalTx)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int Update(IConfiguration config, clsBeProducto_parametro_a entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Updater.Init("producto_parametro_a");
        Updater.Add("idproductoparametroa", "@idproductoparametroa", "F");
        Updater.Add("codigo", "@codigo", "F");
        Updater.Add("nombre", "@nombre", "F");
        Updater.Add("fec_agr", "@fec_agr", "F");
        Updater.Add("user_agr", "@user_agr", "F");
        Updater.Add("fec_mod", "@fec_mod", "F");
        Updater.Add("user_mod", "@user_mod", "F");
        Updater.Add("activo", "@activo", "F");
        Updater.Where("IdProductoParametroA = @IdProductoParametroA");

        var sql = Updater.SQL();
        bool isExternalTx = conn != null && tx != null;

        using var connection = isExternalTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        if (!isExternalTx)
        {
            connection.Open();
            localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = CreateCommand(sql, connection, isExternalTx ? tx : localTx);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();

            if (!isExternalTx)
                localTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!isExternalTx)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT COUNT(1) FROM Producto_parametro_a WHERE IdProductoParametroA = @IdProductoParametroA";

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@IdProductoParametroA", id);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_parametro_a entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;
        if (!isExternalTx) { connection.Open(); localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            if (Existe(entity.IdProductoParametroA, connection, isExternalTx ? tx! : localTx!))
                return Update(config, entity, connection, isExternalTx ? tx : localTx);
            else
                return Insert(config, entity, connection, isExternalTx ? tx : localTx);
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
    public static List<clsBeProducto_parametro_a> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_parametro_a";
        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();

        SqlTransaction? tx = null;
        try
        {
            tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = CreateCommand(sql, connection, tx);
            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            tx.Commit();

            var list = new List<clsBeProducto_parametro_a>();
            foreach (DataRow row in table.Rows)
            {
                var item = new clsBeProducto_parametro_a();
                Cargar(ref item, row);
                list.Add(item);
            }
            return list;
        }
        catch (Exception ex)
        {
            tx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_parametro_a entity)
    {
        const string sql = "SELECT * FROM Producto_parametro_a WHERE IdProductoParametroA = @IdProductoParametroA";

        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();

        SqlTransaction? tx = null;
        try
        {
            tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = CreateCommand(sql, connection, tx);
            cmd.Parameters.AddWithValue("@IdProductoParametroA", entity.IdProductoParametroA);

            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            tx.Commit();

            if (table.Rows.Count == 1)
            {
                Cargar(ref entity, table.Rows[0]);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            tx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int MaxId(IConfiguration config)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoParametroA), 0) FROM Producto_parametro_a";

        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();

        SqlTransaction? tx = null;
        try
        {
            tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = CreateCommand(sql, connection, tx);
            var result = cmd.ExecuteScalar();
            tx.Commit();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            tx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int MaxId(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoParametroA), 0) FROM Producto_parametro_a";
        bool isExternalTx = conn != null && tx != null;

        using var connection = isExternalTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        if (!isExternalTx)
        {
            connection.Open();
            localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = CreateCommand(sql, connection, isExternalTx ? tx : localTx);
            var result = cmd.ExecuteScalar();
            if (!isExternalTx)
                localTx?.Commit();

            return result != null ? Convert.ToInt32(result) : 0;
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
                connection.Close();
        }
    }
}