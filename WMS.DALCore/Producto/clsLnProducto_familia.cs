using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Producto.ProductoSimple;
public class clsLnProducto_familia
{
    private static readonly clsInsert ins = new clsInsert();
    private static readonly clsUpdate upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_familia be, DataRow dr)
    {
        be.IdFamilia = dr.Field<int?>("IdFamilia") ?? 0;
        be.IdPropietario = dr.Field<int?>("IdPropietario") ?? 0;
        be.Nombre = dr.Field<string>("nombre") ?? "";
        be.Activo = dr.Field<bool?>("activo") ?? false;
        be.User_agr = dr.Field<string>("user_agr") ?? "";
        be.Fec_agr = dr.Field<DateTime?>("fec_agr") ?? DateTime.Now;
        be.User_mod = dr.Field<string>("user_mod") ?? "";
        be.Fec_mod = dr.Field<DateTime?>("fec_mod") ?? DateTime.Now;
        be.Codigo = dr.Field<string>("codigo") ?? "";
    }

    public static bool Existe(int id, SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT COUNT(1) FROM Producto_familia WHERE IdFamilia = @Id";
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
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_familia be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            if (Existe(be.IdFamilia, lConn, externa ? tx! : lTx!))
                return Actualizar(config, be, lConn, externa ? tx : lTx);
            else
                return Insertar(config, be, lConn, externa ? tx : lTx);
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
    public static int Insertar(IConfiguration config, clsBeProducto_familia be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        ins.Init("producto_familia");
        ins.Add("idfamilia", "@idfamilia", "F");
        ins.Add("idpropietario", "@idpropietario", "F");
        ins.Add("nombre", "@nombre", "F");
        ins.Add("activo", "@activo", "F");
        ins.Add("user_agr", "@user_agr", "F");
        ins.Add("fec_agr", "@fec_agr", "F");
        ins.Add("user_mod", "@user_mod", "F");
        ins.Add("fec_mod", "@fec_mod", "F");
        ins.Add("codigo", "@codigo", "F");

        string sql = ins.SQL();
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
            Bind(cmd, be);
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
    public static int Actualizar(IConfiguration config, clsBeProducto_familia be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        upd.Init("producto_familia");
        upd.Add("idpropietario", "@idpropietario", "F");
        upd.Add("nombre", "@nombre", "F");
        upd.Add("activo", "@activo", "F");
        upd.Add("user_agr", "@user_agr", "F");
        upd.Add("fec_agr", "@fec_agr", "F");
        upd.Add("user_mod", "@user_mod", "F");
        upd.Add("fec_mod", "@fec_mod", "F");
        upd.Add("codigo", "@codigo", "F");
        upd.Where("IdFamilia = @IdFamilia");

        string sql = upd.SQL();
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
            Bind(cmd, be);
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
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_familia be)
    {
        const string sql = "SELECT * FROM Producto_familia WHERE IdFamilia = @Id";
        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.AddWithValue("@Id", be.IdFamilia);

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
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static List<clsBeProducto_familia> GetAll(IConfiguration config)
    {
        const string sql = "SELECT * FROM Producto_familia";
        var list = new List<clsBeProducto_familia>();

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var item = new clsBeProducto_familia();
                Cargar(ref item, row);
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
        const string sql = "SELECT * FROM Producto_familia";
        var dt = new DataTable();

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();
        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand(sql, conn, tran);
            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }

        return dt;
    }
    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdFamilia), 0) FROM Producto_familia";
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
    private static void Bind(SqlCommand cmd, clsBeProducto_familia e)
    {
        cmd.Parameters.AddWithValue("@idfamilia", e.IdFamilia);
        cmd.Parameters.AddWithValue("@idpropietario", e.IdPropietario);
        cmd.Parameters.AddWithValue("@nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@codigo", e.Codigo);
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeProducto_familia pBeFamilia ,SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = "SELECT TOP 1 * FROM producto_familia WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeFamilia, dt.Rows[0]);
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
    public static void Valida_Atributos(IConfiguration config, clsBeProducto_familiaSimple entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            var Familia = new clsBeProducto_familia();
            bool existe = Existe_By_Codigo(entity.Codigo, ref Familia ,connection, isExternalTx ? tx! : localTx!);

            if (!existe)
            {
                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    Familia.IdFamilia = clsLnProducto_familia.MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
                    Familia.Codigo = entity.Codigo;
                    Familia.Nombre = entity.Nombre ?? entity.Codigo;
                    Familia.User_agr = "1";
                    Familia.User_mod = "1";
                    Familia.Fec_agr = DateTime.Now;
                    Familia.Fec_agr = DateTime.Now;
                    Familia.Activo = entity.Activo;
                    Familia.IdPropietario = entity.IdPropietario;
                    clsLnProducto_familia.Insertar(config, Familia, connection, isExternalTx ? tx : localTx);
                }

            }
            else
            {
                Familia.Codigo = entity.Codigo;
                Familia.Nombre = entity.Nombre ?? entity.Codigo;
                Familia.User_mod = "1";
                Familia.Fec_agr = DateTime.Now;
                Familia.Activo = entity.Activo;
                clsLnProducto_familia.Actualizar(config, Familia, connection, isExternalTx ? tx : localTx);
            }

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