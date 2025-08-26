using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_clasificacion
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    private static string GetConnectionString(IConfiguration config) =>
    config.GetConnectionString("CST") ?? throw new InvalidOperationException("La cadena de conexión 'CST' no está configurada.");
    private static SqlCommand CrearComando(string sp, SqlConnection conn, SqlTransaction? tran, clsBeProducto_clasificacion p)
    {
        var cmd = new SqlCommand(sp, conn, tran) { CommandType = CommandType.Text };

        cmd.Parameters.AddRange(new[]
        {
            new SqlParameter("@IdClasificacion", p.IdClasificacion),
            new SqlParameter("@IdPropietario", p.IdPropietario),
            new SqlParameter("@nombre", p.Nombre),
            new SqlParameter("@activo", p.Activo),
            new SqlParameter("@sistema", p.Sistema),
            new SqlParameter("@user_agr", p.User_agr),
            new SqlParameter("@fec_agr", p.Fec_agr),
            new SqlParameter("@user_mod", p.User_mod),
            new SqlParameter("@fec_mod", p.Fec_mod),
            new SqlParameter("@codigo", p.Codigo)
        });

        return cmd;
    }
    public static void Cargar(ref clsBeProducto_clasificacion o, DataRow dr)
    {
        o.IdClasificacion = dr.Field<int>("IdClasificacion");
        o.IdPropietario = dr.Field<int>("IdPropietario");
        o.Nombre = dr.Field<string>("nombre") ?? "";
        o.Activo = dr.Field<bool>("activo");
        o.Sistema = dr.Field<bool>("sistema");
        o.User_agr = dr.Field<string>("user_agr") ?? "";
        o.Fec_agr = dr.Field<DateTime>("fec_agr");
        o.User_mod = dr.Field<string>("user_mod") ?? "";
        o.Fec_mod = dr.Field<DateTime>("fec_mod");
        o.Codigo = dr.Field<string>("codigo") ?? "";
    }
    public static int Insertar(IConfiguration config, clsBeProducto_clasificacion p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        Ins.Init("producto_clasificacion");
        Ins.Add("idclasificacion", "@idclasificacion", "F");
        Ins.Add("idpropietario", "@idpropietario", "F");
        Ins.Add("nombre", "@nombre", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("sistema", "@sistema", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("codigo", "@codigo", "F");

        string sql = Ins.SQL();
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        var conn = remoto ? extConn! : new SqlConnection(GetConnectionString(config));
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
    public static int Actualizar(IConfiguration config, clsBeProducto_clasificacion p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        Upd.Init("producto_clasificacion");
        Upd.Add("idclasificacion", "@idclasificacion", "F");
        Upd.Add("idpropietario", "@idpropietario", "F");
        Upd.Add("nombre", "@nombre", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("sistema", "@sistema", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("codigo", "@codigo", "F");
        Upd.Where("IdClasificacion = @IdClasificacion");

        string sql = Upd.SQL();
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        var conn = remoto ? extConn! : new SqlConnection(GetConnectionString(config));
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
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_clasificacion p)
    {
        const string sql = "SELECT * FROM Producto_clasificacion WHERE IdClasificacion = @IdClasificacion";
        using var conn = new SqlConnection(GetConnectionString(config));
        SqlTransaction? tran = null;

        try
        {
            conn.Open();
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.AddWithValue("@IdClasificacion", p.IdClasificacion);

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
        const string sql = "SELECT ISNULL(MAX(IdClasificacion), 0) FROM Producto_clasificacion";
        using var conn = new SqlConnection(GetConnectionString(config));
        SqlTransaction? tran = null;

        try
        {
            conn.Open();
            tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
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
    public static int Eliminar(IConfiguration config, clsBeProducto_clasificacion p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        const string sql = "DELETE FROM Producto_clasificacion WHERE IdClasificacion = @IdClasificacion";
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        using var conn = remoto ? extConn! : new SqlConnection(GetConnectionString(config));
        SqlTransaction? tran = null;

        try
        {
            if (!remoto) conn.Open();
            tran ??= remoto ? extTran! : conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.Add(new SqlParameter("@IdClasificacion", p.IdClasificacion));
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
    public static bool ExisteClasificacion(int IdClasificacion, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Producto_clasificacion WHERE IdClasificacion = @IdClasificacion";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdClasificacion", IdClasificacion));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }
    public static int InsertarOActualizar(IConfiguration config, clsBeProducto_clasificacion entity, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            bool existe = ExisteClasificacion(entity.IdClasificacion, connection, isExternalTx ? tx! : localTx!);

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
        const string sql = "SELECT ISNULL(MAX(IdClasificacion), 0) FROM Producto_clasificacion";
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
    public static List<clsBeProducto_clasificacion> GetAll(IConfiguration configuration)
    {
        const string sql = "SELECT * FROM Producto_clasificacion";
        var resultList = new List<clsBeProducto_clasificacion>();

        using var connection = new SqlConnection(configuration.GetConnectionString("CST"));
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, connection, transaction) { CommandType = CommandType.Text };
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var row = reader.ToDataRow();
            var item = new clsBeProducto_clasificacion();
            Cargar(ref item, row);
            resultList.Add(item);
        }

        transaction.Commit();
        return resultList;
    }
    public static bool Existe(clsBeProducto_clasificacion oBe, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = "SELECT COUNT(1) FROM producto_clasificacion WHERE IdClasificacion = @IdClasificacion";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@IdClasificacion", oBe.IdClasificacion);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_clasificacion oBe, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        bool externa = cn != null && tx != null;
        var lConn = externa ? cn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            if (Existe(oBe, lConn, externa ? tx : lTx))
                return Actualizar(config, oBe, lConn, externa ? tx : lTx);
            else
                return Insertar(config, oBe, lConn, externa ? tx : lTx);
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
}