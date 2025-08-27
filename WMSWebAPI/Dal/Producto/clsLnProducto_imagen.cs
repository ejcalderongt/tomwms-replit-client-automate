using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMSWebAPI.Entity.Producto;
public class clsLnProducto_imagen
{
    private static readonly clsInsert Ins = new clsInsert();
    private static readonly clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_imagen oBe, DataRow dr)
    {
        oBe.IdProductoImagen = dr.Field<int?>("IdProductoImagen") ?? 0;
        oBe.IdProducto = dr.Field<int?>("IdProducto") ?? 0;
        oBe.Etiqueta = dr.Field<string>("Etiqueta") ?? "";
        oBe.Imagen = dr.Field<byte[]>("Imagen") ?? Array.Empty<byte>();
        oBe.User_agr = dr.Field<string>("user_agr") ?? "";
        oBe.Fec_agr = dr.Field<DateTime?>("fec_agr") ?? DateTime.Now;
    }

    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT COUNT(1) FROM Producto_imagen WHERE IdProductoImagen = @Id";

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@Id", id);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int Insertar(IConfiguration config, clsBeProducto_imagen e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            Ins.Init("producto_imagen");
            Ins.Add("idproductoimagen", "@idproductoimagen", "F");
            Ins.Add("idproducto", "@idproducto", "F");
            Ins.Add("etiqueta", "@etiqueta", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");

            var sql = Ins.SQL();
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            Bind(cmd, e);
            int result = cmd.ExecuteNonQuery();

            if (!externa) lTx?.Commit();
            return result;
        }
        catch (Exception ex)
        {
            if (!externa) lTx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa)
            {
                lTx?.Dispose();
                lConn.Close();
                lConn.Dispose();
            }
        }
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_imagen e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted); }

        try
        {
            Upd.Init("producto_imagen");
            Upd.Add("idproducto", "@idproducto", "F");
            Upd.Add("etiqueta", "@etiqueta", "F");
            Upd.Add("imagen", "@imagen", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Where("IdProductoImagen = @IdProductoImagen");

            var sql = Upd.SQL();
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            Bind(cmd, e);
            int result = cmd.ExecuteNonQuery();

            if (!externa) lTx?.Commit();
            return result;
        }
        catch (Exception ex)
        {
            if (!externa) lTx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa)
            {
                lTx?.Dispose();
                lConn.Close();
                lConn.Dispose();
            }
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_imagen e)
    {
        const string sql = "SELECT * FROM Producto_imagen WHERE IdProductoImagen = @Id";
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.AddWithValue("@Id", e.IdProductoImagen);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            tran.Commit();

            if (dt.Rows.Count == 1)
            {
                Cargar(ref e, dt.Rows[0]);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    public static List<clsBeProducto_imagen> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_imagen";
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            tran.Commit();

            var list = new List<clsBeProducto_imagen>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new clsBeProducto_imagen();
                Cargar(ref item, row);
                list.Add(item);
            }

            return list;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    public static DataTable Listar(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_imagen";
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            tran.Commit();
            return dt;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoImagen), 0) FROM Producto_imagen";
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
    private static void Bind(SqlCommand cmd, clsBeProducto_imagen e)
    {
        cmd.Parameters.AddWithValue("@idproductoimagen", e.IdProductoImagen);
        cmd.Parameters.AddWithValue("@idproducto", e.IdProducto);
        cmd.Parameters.AddWithValue("@etiqueta", e.Etiqueta);
        cmd.Parameters.AddWithValue("@imagen", e.Imagen);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
    }
}