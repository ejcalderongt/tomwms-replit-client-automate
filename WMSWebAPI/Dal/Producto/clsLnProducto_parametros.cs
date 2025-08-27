using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMSWebAPI.Entity.Producto;

public class clsLnProductoParametros
{
    private static readonly clsInsert Inserter = new clsInsert();
    private static readonly clsUpdate Updater = new clsUpdate();

    private static string GetConnectionString(IConfiguration config) =>
        config.GetConnectionString("CST") ?? throw new InvalidOperationException("Cadena de conexión 'CST' no está configurada.");
    private static SqlCommand CreateCommand(string sql, SqlConnection conn, SqlTransaction? tx = null) =>
        new SqlCommand(sql, conn, tx) { CommandType = CommandType.Text };
    private static void BindParams(SqlCommand cmd, clsBeProducto_parametros e)
    {
        cmd.Parameters.AddWithValue("@IdProductoParametro", e.IdProductoParametro);
        cmd.Parameters.AddWithValue("@IdParametro", e.IdParametro);
        cmd.Parameters.AddWithValue("@IdProducto", e.IdProducto);
        cmd.Parameters.AddWithValue("@valor_texto", e.Valor_texto);
        cmd.Parameters.AddWithValue("@valor_numerico", e.Valor_numerico);
        cmd.Parameters.AddWithValue("@valor_fecha", e.Valor_fecha);
        cmd.Parameters.AddWithValue("@valor_logico", e.Valor_logico);
        cmd.Parameters.AddWithValue("@capturar_siempre", e.Capturar_siempre);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
    }
    private static void LoadFromRow(ref clsBeProducto_parametros e, DataRow r)
    {
        e.IdProductoParametro = r.Field<int>("IdProductoParametro");
        e.IdParametro = r.Field<int>("IdParametro");
        e.IdProducto = r.Field<int>("IdProducto");
        e.Valor_texto = r.Field<string>("valor_texto") ?? "";
        e.Valor_numerico = r.Field<double?>("valor_numerico") ?? 0;
        e.Valor_fecha = r.Field<DateTime?>("valor_fecha") ?? DateTime.Now;
        e.Valor_logico = r.Field<bool?>("valor_logico") ?? false;
        e.Capturar_siempre = r.Field<bool?>("capturar_siempre") ?? false;
        e.User_agr = r.Field<string>("user_agr") ?? "";
        e.Fec_agr = r.Field<DateTime?>("fec_agr") ?? DateTime.Now;
        e.User_mod = r.Field<string>("user_mod") ?? "";
        e.Fec_mod = r.Field<DateTime?>("fec_mod") ?? DateTime.Now;
        e.Activo = r.Field<bool?>("activo") ?? false;
    }
    private static void LoadFromReader(ref clsBeProducto_parametros e, IDataRecord r)
    {
        int GetInt(string col) => r.IsDBNull(r.GetOrdinal(col)) ? 0 : r.GetInt32(r.GetOrdinal(col));
        double GetDouble(string col) => r.IsDBNull(r.GetOrdinal(col)) ? 0 : r.GetDouble(r.GetOrdinal(col));
        string GetString(string col) => r.IsDBNull(r.GetOrdinal(col)) ? "" : r.GetString(r.GetOrdinal(col));
        DateTime GetDate(string col) => r.IsDBNull(r.GetOrdinal(col)) ? DateTime.Now : r.GetDateTime(r.GetOrdinal(col));
        bool GetBool(string col) => !r.IsDBNull(r.GetOrdinal(col)) && r.GetBoolean(r.GetOrdinal(col));

        e.IdProductoParametro = GetInt("IdProductoParametro");
        e.IdParametro = GetInt("IdParametro");
        e.IdProducto = GetInt("IdProducto");
        e.Valor_texto = GetString("valor_texto");
        e.Valor_numerico = GetDouble("valor_numerico");
        e.Valor_fecha = GetDate("valor_fecha");
        e.Valor_logico = GetBool("valor_logico");
        e.Capturar_siempre = GetBool("capturar_siempre");
        e.User_agr = GetString("user_agr");
        e.Fec_agr = GetDate("fec_agr");
        e.User_mod = GetString("user_mod");
        e.Fec_mod = GetDate("fec_mod");
        e.Activo = GetBool("activo");
    }
    public static int Insert(IConfiguration config, clsBeProducto_parametros e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Inserter.Init("producto_parametros");
        Inserter.Add("idproductoparametro", "@IdProductoParametro", "F");
        Inserter.Add("idparametro", "@IdParametro", "F");
        Inserter.Add("idproducto", "@IdProducto", "F");
        Inserter.Add("valor_texto", "@valor_texto", "F");
        Inserter.Add("valor_numerico", "@valor_numerico", "F");
        Inserter.Add("valor_fecha", "@valor_fecha", "F");
        Inserter.Add("valor_logico", "@valor_logico", "F");
        Inserter.Add("capturar_siempre", "@capturar_siempre", "F");
        Inserter.Add("user_agr", "@user_agr", "F");
        Inserter.Add("fec_agr", "@fec_agr", "F");
        Inserter.Add("user_mod", "@user_mod", "F");
        Inserter.Add("fec_mod", "@fec_mod", "F");
        Inserter.Add("activo", "@activo", "F");

        var sql = Inserter.SQL();
        bool isTx = conn != null && tx != null;

        using var connection = isTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        try
        {
            if (!isTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            var cmd = CreateCommand(sql, connection, isTx ? tx : localTx);
            BindParams(cmd, e);

            int result = cmd.ExecuteNonQuery();

            if (!isTx) localTx?.Commit();
            return result;
        }
        catch (Exception ex)
        {
            if (!isTx) localTx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!isTx && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
    public static int Update(IConfiguration config, clsBeProducto_parametros e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Updater.Init("producto_parametros");
        Updater.Add("idparametro", "@IdParametro", "F");
        Updater.Add("idproducto", "@IdProducto", "F");
        Updater.Add("valor_texto", "@valor_texto", "F");
        Updater.Add("valor_numerico", "@valor_numerico", "F");
        Updater.Add("valor_fecha", "@valor_fecha", "F");
        Updater.Add("valor_logico", "@valor_logico", "F");
        Updater.Add("capturar_siempre", "@capturar_siempre", "F");
        Updater.Add("user_agr", "@user_agr", "F");
        Updater.Add("fec_agr", "@fec_agr", "F");
        Updater.Add("user_mod", "@user_mod", "F");
        Updater.Add("fec_mod", "@fec_mod", "F");
        Updater.Add("activo", "@activo", "F");
        Updater.Where("IdProductoParametro = @IdProductoParametro");

        var sql = Updater.SQL();
        bool isTx = conn != null && tx != null;

        using var connection = isTx ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        try
        {
            if (!isTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            var cmd = CreateCommand(sql, connection, isTx ? tx : localTx);
            BindParams(cmd, e);

            int result = cmd.ExecuteNonQuery();

            if (!isTx) localTx?.Commit();
            return result;
        }
        catch (Exception ex)
        {
            if (!isTx) localTx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!isTx && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT COUNT(1) FROM Producto_parametros WHERE IdProductoParametro = @IdProductoParametro";

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@IdProductoParametro", id);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_parametros e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        using var connection = conn ?? new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = tx ?? connection.BeginTransaction();
        if (conn == null) connection.Open();

        try
        {
            if (Existe(e.IdProductoParametro, connection, localTx))
                return Update(config, e, connection, localTx);
            else
                return Insert(config, e, connection, localTx);
        }
        catch
        {
            if (conn == null) localTx.Rollback();
            throw;
        }
        finally
        {
            if (conn == null) { localTx.Commit(); connection.Close(); }
        }
    }
    public static List<clsBeProducto_parametros> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_parametros";
        var list = new List<clsBeProducto_parametros>();

        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();

        SqlTransaction? tx = null;
        try
        {
            tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = new SqlCommand(sql, connection, tx);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var obj = new clsBeProducto_parametros();
                LoadFromReader(ref obj, reader);
                list.Add(obj);
            }

            tx.Commit();
            return list;
        }
        catch (Exception ex)
        {
            tx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_parametros entity)
    {
        const string sql = "SELECT * FROM Producto_parametros WHERE IdProductoParametro = @IdProductoParametro";

        using var connection = new SqlConnection(GetConnectionString(config));
        connection.Open();

        SqlTransaction? tx = null;
        try
        {
            tx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = new SqlCommand(sql, connection, tx);
            cmd.Parameters.AddWithValue("@IdProductoParametro", entity.IdProductoParametro);

            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);

            tx.Commit();

            if (table.Rows.Count == 1)
            {
                LoadFromRow(ref entity, table.Rows[0]);
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
    public static int MaxId(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoParametro), 0) FROM Producto_parametros";
        bool isExternal = conn != null && tx != null;

        using var connection = isExternal ? conn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternal)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, connection, isExternal ? tx! : localTx!);
            var result = cmd.ExecuteScalar();

            if (!isExternal) localTx?.Commit();

            return result != null ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            if (!isExternal) localTx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
}