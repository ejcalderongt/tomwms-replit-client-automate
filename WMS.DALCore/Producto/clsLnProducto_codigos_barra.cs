using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_codigos_barra
{
    private static readonly clsInsert Ins = new clsInsert();
    private static readonly clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_codigos_barra be, DataRow dr)
    {
        be.IdProductoCodigoBarra = dr.Field<int?>("IdProductoCodigoBarra") ?? 0;
        be.IdProducto = dr.Field<int?>("IdProducto") ?? 0;
        be.IdProveedor = dr.Field<int?>("IdProveedor") ?? 0;
        be.Codigo_barra = dr.Field<string>("codigo_barra") ?? string.Empty;
        be.Fec_agr = dr.Field<DateTime?>("fec_agr") ?? DateTime.Now;
        be.User_mod = dr.Field<string>("user_mod") ?? string.Empty;
        be.Fec_mod = dr.Field<DateTime?>("fec_mod") ?? DateTime.Now;
        be.User_agr = dr.Field<string>("user_agr") ?? string.Empty;
        be.Activo = dr.Field<bool?>("activo") ?? false;
    }
    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string query = "SELECT COUNT(1) FROM producto_codigos_barra WHERE IdProductoCodigoBarra = @id";
        using var cmd = new SqlCommand(query, conn, tx);
        cmd.Parameters.AddWithValue("@id", id);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }
    public static int InsertarOActualizar(IConfiguration config, clsBeProducto_codigos_barra be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        using var connection = conn ?? new SqlConnection(config.GetConnectionString("CST"));
        if (conn == null) connection.Open();
        var transaction = tx ?? connection.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            if (Existe(be.IdProductoCodigoBarra, connection, transaction))
                return Actualizar(config, be, connection, transaction);
            else
                return Insertar(config, be, connection, transaction);
        }
        finally
        {
            if (tx == null) transaction.Commit();
            if (conn == null) connection.Close();
        }
    }
    public static int Insertar(IConfiguration config, clsBeProducto_codigos_barra be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Ins.Init("producto_codigos_barra");
        Ins.Add("idproductocodigobarra", "@idproductocodigobarra", "F");
        Ins.Add("idproducto", "@idproducto", "F");
        Ins.Add("idproveedor", "@idproveedor", "F");
        Ins.Add("codigo_barra", "@codigo_barra", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("activo", "@activo", "F");

        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;

        if (!externa)
        {
            lConn.Open();
            lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = new SqlCommand(Ins.SQL(), lConn, externa ? tx! : lTx!)
            {
                CommandType = CommandType.Text
            };
            CrearComando(cmd, be);
            int result = cmd.ExecuteNonQuery();

            if (!externa)
                lTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!externa)
                lTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa && lConn.State == ConnectionState.Open)
                lConn.Close();
        }
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_codigos_barra be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Upd.Init("producto_codigos_barra");
        Upd.Add("idproductocodigobarra", "@idproductocodigobarra", "F");
        Upd.Add("idproducto", "@idproducto", "F");
        Upd.Add("idproveedor", "@idproveedor", "F");
        Upd.Add("codigo_barra", "@codigo_barra", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Where("IdProductoCodigoBarra = @idproductocodigobarra");

        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;

        if (!externa)
        {
            lConn.Open();
            lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = new SqlCommand(Upd.SQL(), lConn, externa ? tx! : lTx!)
            {
                CommandType = CommandType.Text
            };
            CrearComando(cmd, be);
            int result = cmd.ExecuteNonQuery();

            if (!externa)
                lTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!externa)
                lTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa && lConn.State == ConnectionState.Open)
                lConn.Close();
        }
    }
    public static int Eliminar(IConfiguration config, int id, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string query = "DELETE FROM producto_codigos_barra WHERE IdProductoCodigoBarra = @id";
        bool externa = conn != null && tx != null;

        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;

        if (!externa)
        {
            lConn.Open();
            lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            using var cmd = new SqlCommand(query, lConn, externa ? tx! : lTx!);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();

            if (!externa)
                lTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!externa)
                lTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa && lConn.State == ConnectionState.Open)
                lConn.Close();
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_codigos_barra be)
    {
        const string query = "SELECT * FROM producto_codigos_barra WHERE IdProductoCodigoBarra = @id";
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.AddWithValue("@id", be.IdProductoCodigoBarra);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            tran.Commit();

            if (dt.Rows.Count == 1)
            {
                Cargar(ref be, dt.Rows[0]);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            tran?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public static List<clsBeProducto_codigos_barra> GetAll(IConfiguration config)
    {
        var list = new List<clsBeProducto_codigos_barra>();
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand("SELECT * FROM producto_codigos_barra", conn, tran);
            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            tran.Commit();

            foreach (DataRow row in dt.Rows)
            {
                var be = new clsBeProducto_codigos_barra();
                Cargar(ref be, row);
                list.Add(be);
            }

            return list;
        }
        catch (Exception ex)
        {
            tran?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public static DataTable Listar(IConfiguration config)
    {
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand("SELECT * FROM producto_codigos_barra", conn, tran);
            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            tran.Commit();
            return dt;
        }
        catch (Exception ex)
        {
            tran?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public static int MaxID(IConfiguration config)
    {
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand("SELECT ISNULL(MAX(IdProductoCodigoBarra), 0) FROM producto_codigos_barra", conn, tran);
            var result = cmd.ExecuteScalar();
            tran.Commit();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            tran?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    private static void CrearComando(SqlCommand cmd, clsBeProducto_codigos_barra be)
    {
        cmd.Parameters.AddWithValue("@idproductocodigobarra", be.IdProductoCodigoBarra);
        cmd.Parameters.AddWithValue("@idproducto", be.IdProducto);
        cmd.Parameters.AddWithValue("@idproveedor", be.IdProveedor);
        cmd.Parameters.AddWithValue("@codigo_barra", be.Codigo_barra);
        cmd.Parameters.AddWithValue("@fec_agr", be.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", be.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", be.Fec_mod);
        cmd.Parameters.AddWithValue("@user_agr", be.User_agr);
        cmd.Parameters.AddWithValue("@activo", be.Activo);
    }
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoCodigoBarra), 0) FROM Producto_codigos_barra";
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
}