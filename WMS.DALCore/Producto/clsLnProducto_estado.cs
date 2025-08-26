using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_estado
{
    private static readonly clsInsert Ins = new clsInsert();
    private static readonly clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_estado oBe, DataRow dr)
    {
        oBe.IdEstado = dr.Field<int?>("IdEstado") ?? 0;
        oBe.IdPropietario = dr.Field<int?>("IdPropietario") ?? 0;
        oBe.Nombre = dr.Field<string>("nombre") ?? "";
        oBe.IdUbicacionDefecto = dr.Field<int?>("IdUbicacionDefecto") ?? 0;
        oBe.Utilizable = dr.Field<bool?>("utilizable") ?? false;
        oBe.Activo = dr.Field<bool?>("activo") ?? false;
        oBe.User_agr = dr.Field<string>("user_agr") ?? "";
        oBe.Fec_agr = dr.Field<DateTime?>("fec_agr") ?? DateTime.Now;
        oBe.User_mod = dr.Field<string>("user_mod") ?? "";
        oBe.Fec_mod = dr.Field<DateTime?>("fec_mod") ?? DateTime.Now;
        oBe.Dañado = dr.Field<bool?>("dañado") ?? false;
        oBe.Codigo_bodega_erp = dr.Field<string>("codigo_bodega_erp") ?? "";
        oBe.Sistema = dr.Field<bool?>("sistema") ?? false;
        oBe.Dias_vencimiento_clasificacion = dr.Field<int?>("dias_vencimiento_clasificacion") ?? 0;
        oBe.Tolerancia_dias_vencimiento = dr.Field<int?>("tolerancia_dias_vencimiento") ?? 0;
    }
    public static bool Existe(int IdEstado, SqlConnection conn, SqlTransaction tx)
    {
        const string query = "SELECT COUNT(1) FROM Producto_estado WHERE IdEstado = @IdEstado";
        using var cmd = new SqlCommand(query, conn, tx);
        cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_estado e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            if (Existe(e.IdEstado, lConn, externa ? tx! : lTx!))
                return Actualizar(config, e, lConn, externa ? tx : lTx);
            else
                return Insertar(config, e, lConn, externa ? tx : lTx);
        }
        catch (Exception)
        {
            if (!externa && lTx != null) lTx.Rollback();
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
    public static int Insertar(IConfiguration config, clsBeProducto_estado e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Ins.Init("producto_estado");
        Ins.Add("idestado", "@idestado", "F");
        Ins.Add("idpropietario", "@idpropietario", "F");
        Ins.Add("nombre", "@nombre", "F");
        Ins.Add("idubicaciondefecto", "@idubicaciondefecto", "F");
        Ins.Add("utilizable", "@utilizable", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("dañado", "@dañado", "F");
        Ins.Add("codigo_bodega_erp", "@codigo_bodega_erp", "F");
        Ins.Add("sistema", "@sistema", "F");
        Ins.Add("dias_vencimiento_clasificacion", "@dias_vencimiento_clasificacion", "F");
        Ins.Add("tolerancia_dias_vencimiento", "@tolerancia_dias_vencimiento", "F");

        string sql = Ins.SQL();
        bool externa = conn != null && tx != null;

        using var localConn = externa ? null : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!externa)
            {
                localConn!.Open();
                localTx = localConn.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, externa ? conn! : localConn!, externa ? tx! : localTx!);
            CrearComando(cmd, e);
            int result = cmd.ExecuteNonQuery();

            if (!externa)
                localTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!externa)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa && localConn?.State == ConnectionState.Open)
                localConn.Close();
        }
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_estado e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        Upd.Init("producto_estado");
        Upd.Add("idpropietario", "@idpropietario", "F");
        Upd.Add("nombre", "@nombre", "F");
        Upd.Add("idubicaciondefecto", "@idubicaciondefecto", "F");
        Upd.Add("utilizable", "@utilizable", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("dañado", "@dañado", "F");
        Upd.Add("codigo_bodega_erp", "@codigo_bodega_erp", "F");
        Upd.Add("sistema", "@sistema", "F");
        Upd.Add("dias_vencimiento_clasificacion", "@dias_vencimiento_clasificacion", "F");
        Upd.Add("tolerancia_dias_vencimiento", "@tolerancia_dias_vencimiento", "F");
        Upd.Where("IdEstado = @IdEstado");

        string sql = Upd.SQL();
        bool externa = conn != null && tx != null;

        using var localConn = externa ? null : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!externa)
            {
                localConn!.Open();
                localTx = localConn.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, externa ? conn! : localConn!, externa ? tx! : localTx!);
            CrearComando(cmd, e);
            int result = cmd.ExecuteNonQuery();

            if (!externa)
                localTx?.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!externa)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!externa && localConn?.State == ConnectionState.Open)
                localConn.Close();
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_estado be)
    {
        const string sql = "SELECT * FROM Producto_estado WHERE IdEstado = @IdEstado";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        SqlTransaction? tran = null;

        try
        {
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.AddWithValue("@IdEstado", be.IdEstado);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

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
    public static List<clsBeProducto_estado> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_estado";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        SqlTransaction? tran = null;
        try
        {
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            tran.Commit();

            var list = new List<clsBeProducto_estado>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new clsBeProducto_estado();
                Cargar(ref item, row);
                list.Add(item);
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
        const string sql = "SELECT * FROM Producto_estado";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        SqlTransaction? tran = null;
        try
        {
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
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
        const string sql = "SELECT ISNULL(MAX(IdEstado), 0) FROM Producto_estado";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        SqlTransaction? tran = null;
        try
        {
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            using var cmd = new SqlCommand(sql, conn, tran);
            object? result = cmd.ExecuteScalar();
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
    private static void CrearComando(SqlCommand cmd, clsBeProducto_estado e)
    {
        cmd.Parameters.AddWithValue("@IdEstado", e.IdEstado);
        cmd.Parameters.AddWithValue("@IdPropietario", e.IdPropietario);
        cmd.Parameters.AddWithValue("@nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@IdUbicacionDefecto", e.IdUbicacionDefecto);
        cmd.Parameters.AddWithValue("@utilizable", e.Utilizable);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@dañado", e.Dañado);
        cmd.Parameters.AddWithValue("@codigo_bodega_erp", e.Codigo_bodega_erp);
        cmd.Parameters.AddWithValue("@sistema", e.Sistema);
        cmd.Parameters.AddWithValue("@dias_vencimiento_clasificacion", e.Dias_vencimiento_clasificacion);
        cmd.Parameters.AddWithValue("@tolerancia_dias_vencimiento", e.Tolerancia_dias_vencimiento);
    }
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdEstado), 0) FROM Producto_estado";
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
            if (!externa && lTx != null) lTx.Rollback();
            throw;
        }
        finally
        {
            if (!externa) lConn.Close();
        }
    }
    public static int InsertOrUpdate(IConfiguration config, List<clsBeProducto_estado> estados, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        int total = 0;

        try
        {
            if (!externa)
            {
                lConn.Open();
                lTx = lConn.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            foreach (var e in estados)
            {
                if (Existe(e.IdEstado, lConn, externa ? tx! : lTx!))
                    total += Actualizar(config, e, lConn, externa ? tx : lTx);
                else
                    total += Insertar(config, e, lConn, externa ? tx : lTx);
            }

            if (!externa)
                lTx?.Commit();

            return total;
        }
        catch (Exception)
        {
            if (!externa && lTx != null)
                lTx.Rollback();
            throw;
        }
        finally
        {
            if (!externa)
            {
                lConn.Close();
                lConn.Dispose();
                lTx?.Dispose();
            }
        }
    }
}