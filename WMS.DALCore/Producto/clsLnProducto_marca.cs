using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Interface;
public class clsLnProducto_Marca
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

    public static int Insert(clsBeProducto_marca entity, SqlConnection connection, SqlTransaction transaction)
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

        string sql = Inserter.SQL();

        try
        {
            using var cmd = new SqlCommand(sql, connection, transaction);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int Update(clsBeProducto_marca entity, SqlConnection connection, SqlTransaction transaction)
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

        string sql = Updater.SQL();

        try
        {
            using var cmd = new SqlCommand(sql, connection, transaction);
            BindParams(cmd, entity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(clsBeProducto_marca entity, SqlConnection connection, SqlTransaction transaction)
    {
        try
        {
            int result = Existe(entity.IdMarca, connection, transaction)
                ? Update(entity, connection, transaction)
                : Insert(entity, connection, transaction);

            return result;
        }
        catch
        {
            throw;
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

    public static int MaxId(SqlConnection connection, SqlTransaction transaction)
    {
        const string sql = "SELECT ISNULL(MAX(IdMarca), 0) FROM Producto_marca";

        try
        {
            using var cmd = CreateCommand(sql, connection, transaction);
            var result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeProducto_marca pBeMarca ,SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = "SELECT TOP 1 * FROM producto_marca WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);


            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeMarca, dt.Rows[0]);
                return true;
            }

            return false;
            
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static void Valida_Atributos(clsBeProducto_marcaSimple entity, SqlConnection connection, SqlTransaction transaction)
    {
        try
        {
            var Marca = new clsBeProducto_marca();
            bool existe = Existe_By_Codigo(entity.Codigo, ref Marca, connection, transaction);

            var BeInavConfigEnc = new clsBeI_nav_config_enc();
            clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, connection, transaction);

            if (BeInavConfigEnc == null)
                throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

            if (!existe)
            {
                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    Marca.IdMarca = MaxId(connection, transaction) + 1;
                    Marca.Codigo = entity.Codigo;
                    Marca.Nombre = entity.Nombre ?? entity.Codigo;
                    Marca.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                    Marca.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                    Marca.Fec_agr = DateTime.Now;
                    Marca.Fec_mod = DateTime.Now;
                    Marca.Activo = entity.Activo;
                    Marca.IdPropietario = entity.IdPropietario;
                    Insert(Marca, connection, transaction);
                }
            }
            else
            {
                Marca.Codigo = entity.Codigo;
                Marca.Nombre = entity.Nombre ?? entity.Codigo;
                Marca.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                Marca.Fec_mod = DateTime.Now;
                Marca.Activo = entity.Activo;
                Update(Marca, connection, transaction);
            }
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }


}