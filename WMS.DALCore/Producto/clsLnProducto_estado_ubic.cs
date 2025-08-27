using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_estado_ubic
{
    private static readonly clsInsert Ins = new clsInsert();
    private static readonly clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_estado_ubic oBe, DataRow dr)
    {
        oBe.IdProductoEstadUbic = dr.Field<int?>("IdProductoEstadUbic") ?? 0;
        oBe.IdEstado = dr.Field<int?>("IdEstado") ?? 0;
        oBe.IdUbicacionDefecto = dr.Field<int?>("IdUbicacionDefecto") ?? 0;
        oBe.Fec_agr = dr.Field<DateTime?>("fec_agr") ?? DateTime.Now;
        oBe.User_agr = dr.Field<string>("user_agr") ?? "";
        oBe.Fec_mod = dr.Field<DateTime?>("fec_mod") ?? DateTime.Now;
        oBe.User_mod = dr.Field<string>("user_mod") ?? "";
        oBe.Activo = dr.Field<bool?>("activo") ?? false;
        oBe.IdBodega = dr.Field<int?>("IdBodega") ?? 0;
    }
    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT COUNT(1) FROM Producto_estado_ubic WHERE IdProductoEstadUbic = @Id";
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
    public static int InsertarOActualizar(IConfiguration config, clsBeProducto_estado_ubic e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            if (Existe(e.IdProductoEstadUbic, lConn, externa ? tx! : lTx!))
                return Actualizar(config, e, lConn, externa ? tx : lTx);
            else
                return Insertar(config, e, lConn, externa ? tx : lTx);
        }
        catch
        {
            if (!externa) lTx?.Rollback();
            throw;
        }
        finally
        {
            if (!externa)
            {
                lTx?.Commit();
                lConn.Close();
            }
        }
    }
    public static int Insertar(IConfiguration config, clsBeProducto_estado_ubic e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Ins.Init("producto_estado_ubic");
        Ins.Add("idproductoestadubic", "@idproductoestadubic", "F");
        Ins.Add("idestado", "@idestado", "F");
        Ins.Add("idubicaciondefecto", "@idubicaciondefecto", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("idbodega", "@idbodega", "F");

        string sql = Ins.SQL();
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
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            Bind(cmd, e);
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
            if (!externa)
                lConn.Close();
        }
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_estado_ubic e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Upd.Init("producto_estado_ubic");
        Upd.Add("idestado", "@idestado", "F");
        Upd.Add("idubicaciondefecto", "@idubicaciondefecto", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("idbodega", "@idbodega", "F");
        Upd.Where("IdProductoEstadUbic = @IdProductoEstadUbic");

        string sql = Upd.SQL();
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
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            Bind(cmd, e);
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
            if (!externa)
                lConn.Close();
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_estado_ubic e)
    {
        const string sql = "SELECT * FROM Producto_estado_ubic WHERE IdProductoEstadUbic = @Id";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.AddWithValue("@Id", e.IdProductoEstadUbic);

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
    }
    public static List<clsBeProducto_estado_ubic> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_estado_ubic";
        var list = new List<clsBeProducto_estado_ubic>();

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var item = new clsBeProducto_estado_ubic();
                Cargar(ref item, dr);
                list.Add(item);
            }

            tran.Commit();
            return list;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static DataTable Listar(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_estado_ubic";

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
    }
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoEstadUbic), 0) FROM Producto_estado_ubic";
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

    private static void Bind(SqlCommand cmd, clsBeProducto_estado_ubic e)
    {
        cmd.Parameters.AddWithValue("@IdProductoEstadUbic", e.IdProductoEstadUbic);
        cmd.Parameters.AddWithValue("@IdEstado", e.IdEstado);
        cmd.Parameters.AddWithValue("@IdUbicacionDefecto", e.IdUbicacionDefecto);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
        cmd.Parameters.AddWithValue("@idbodega", e.IdBodega);
    }
    public static List<clsBeProducto_estado_ubic> GetAll(SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT * FROM Producto_estado_ubic";
        using var cmd = new SqlCommand(sql, conn, tx);
        using var adapter = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adapter.Fill(dt);
        var list = new List<clsBeProducto_estado_ubic>();
        foreach (DataRow dr in dt.Rows)
        {
            var item = new clsBeProducto_estado_ubic();
            Cargar(ref item, dr);
            list.Add(item);
        }
        return list;
    }
}