using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProductoMarca
{
    private static readonly clsInsert Inserter = new clsInsert();
    private static readonly clsUpdate Updater = new clsUpdate();

    private static string GetConnectionString(IConfiguration config) =>
        config.GetConnectionString("CST") ?? throw new InvalidOperationException("Cadena de conexión 'CST' no está configurada.");

    private static SqlCommand CreateCommand(string sql, SqlConnection conn, SqlTransaction? tx = null) =>
        new SqlCommand(sql, conn, tx) { CommandType = CommandType.Text };
    public static void Cargar(ref clsBeProducto_marca entity, DataRow row)
    {
        entity.IdMarca = row.Field<int?>("IdMarca") ?? 0;
        entity.IdPropietario = row.Field<int?>("IdPropietario") ?? 0;
        entity.Nombre = row.Field<string>("nombre") ?? "";
        entity.Activo = row.Field<bool?>("activo") ?? false;
        entity.User_agr = row.Field<string>("user_agr") ?? "";
        entity.Fec_agr = row.Field<DateTime?>("fec_agr") ?? DateTime.UtcNow;
        entity.User_mod = row.Field<string>("user_mod") ?? "";
        entity.Fec_mod = row.Field<DateTime?>("fec_mod") ?? DateTime.UtcNow;
        entity.Codigo = row.Field<string>("codigo") ?? "";
    }

    private static void BindParams(SqlCommand cmd, clsBeProducto_marca e)
    {
        cmd.Parameters.AddWithValue("@IdMarca", e.IdMarca);
        cmd.Parameters.AddWithValue("@IdPropietario", e.IdPropietario);
        cmd.Parameters.AddWithValue("@nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@codigo", e.Codigo);
    }

    public static int Insert(IConfiguration config, clsBeProducto_marca entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Inserter.Init("producto_marca");
        Inserter.Add("idmarca", "@idmarca", "F");
        Inserter.Add("idpropietario", "@idpropietario", "F");
        Inserter.Add("nombre", "@nombre", "F");
        Inserter.Add("activo", "@activo", "F");
        Inserter.Add("user_agr", "@user_agr", "F");
        Inserter.Add("fec_agr", "@fec_agr", "F");
        Inserter.Add("user_mod", "@user_mod", "F");
        Inserter.Add("fec_mod", "@fec_mod", "F");
        Inserter.Add("codigo", "@codigo", "F");

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
            using var cmd = new SqlCommand(sql, connection, isExternalTx ? tx! : localTx!);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();

            if (!isExternalTx)
                localTx!.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!isExternalTx)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
    public static int Update(IConfiguration config, clsBeProducto_marca entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Updater.Init("producto_marca");
        Updater.Add("idpropietario", "@idpropietario", "F");
        Updater.Add("nombre", "@nombre", "F");
        Updater.Add("activo", "@activo", "F");
        Updater.Add("user_agr", "@user_agr", "F");
        Updater.Add("fec_agr", "@fec_agr", "F");
        Updater.Add("user_mod", "@user_mod", "F");
        Updater.Add("fec_mod", "@fec_mod", "F");
        Updater.Add("codigo", "@codigo", "F");
        Updater.Where("idmarca = @idmarca");

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
            using var cmd = new SqlCommand(sql, connection, isExternalTx ? tx! : localTx!);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();

            if (!isExternalTx)
                localTx!.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!isExternalTx)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_marca entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternal = conn != null && tx != null;
        SqlConnection connection = isExternal ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? transaction = tx;

        try
        {
            if (!isExternal)
            {
                connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction), "Transacción requerida no fue inicializada correctamente.");

            int result = Existe(entity.IdMarca, connection, transaction)
                ? Update(config, entity, connection, transaction)
                : Insert(config, entity, connection, transaction);

            if (!isExternal)
                transaction.Commit();

            return result;
        }
        catch
        {
            if (!isExternal)
                transaction?.Rollback();
            throw;
        }
        finally
        {
            if (!isExternal && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
                transaction?.Dispose();
            }
        }
    }
    public static bool Existe(int idMarca, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string sql = "SELECT COUNT(1) FROM Producto_marca WHERE IdMarca = @IdMarca";
            using var cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@IdMarca", idMarca);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static List<clsBeProducto_marca> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_marca";
        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();
        using var tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = CreateCommand(sql, connection, tx);
            using var adapter = new SqlDataAdapter(cmd);

            var table = new DataTable();
            adapter.Fill(table);

            tx.Commit();

            var list = new List<clsBeProducto_marca>();
            foreach (DataRow row in table.Rows)
            {
                var item = new clsBeProducto_marca();
                Cargar(ref item, row);
                list.Add(item);
            }

            return list;
        }
        catch (Exception ex)
        {
            tx.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static clsBeProducto_marca? GetById(IConfiguration config, int idMarca)
    {
        const string sql = "SELECT * FROM Producto_marca WHERE IdMarca = @IdMarca";
        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();
        using var tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = CreateCommand(sql, connection, tx);
            cmd.Parameters.AddWithValue("@IdMarca", idMarca);

            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);

            tx.Commit();

            if (table.Rows.Count == 0) return null;

            var result = new clsBeProducto_marca();
            Cargar(ref result, table.Rows[0]);
            return result;
        }
        catch (Exception ex)
        {
            tx.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int MaxId(IConfiguration config)
    {
        const string sql = "SELECT ISNULL(MAX(IdMarca), 0) FROM Producto_marca";
        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();
        using var tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = CreateCommand(sql, connection, tx);
            var result = cmd.ExecuteScalar();
            tx.Commit();

            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            tx.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
}