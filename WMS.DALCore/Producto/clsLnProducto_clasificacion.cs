using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Producto.ProductoSimple;
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
    public static int Insertar(clsBeProducto_clasificacion p, SqlConnection conn, SqlTransaction tran)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tran == null)
            throw new ArgumentNullException(nameof(tran));

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

        try
        {
            using var cmd = CrearComando(sql, conn, tran, p);
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
    }
    public static int Actualizar(clsBeProducto_clasificacion p, SqlConnection conn, SqlTransaction tran)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tran == null)
            throw new ArgumentNullException(nameof(tran));

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

        try
        {
            using var cmd = CrearComando(sql, conn, tran, p);
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
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
    public static int InsertarOActualizar(clsBeProducto_clasificacion entity, SqlConnection conn, SqlTransaction tx)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            bool existe = ExisteClasificacion(entity.IdClasificacion, conn, tx);

            return existe
                ? Actualizar(entity, conn, tx)
                : Insertar(entity, conn, tx);
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static int MaxID(SqlConnection conn, SqlTransaction tx)
    {
        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        const string sql = "SELECT ISNULL(MAX(IdClasificacion), 0) FROM Producto_clasificacion";

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
        catch
        {
            throw;
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
    public static int InsertOrUpdate(clsBeProducto_clasificacion oBe, SqlConnection cn, SqlTransaction tx)
    {
        if (oBe == null)
            throw new ArgumentNullException(nameof(oBe));

        if (cn == null)
            throw new ArgumentNullException(nameof(cn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            if (Existe(oBe, cn, tx))
                return Actualizar(oBe, cn, tx);
            else
                return Insertar(oBe, cn, tx);
        }
        catch
        {
            throw;
        }
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeProducto_clasificacion pBeClasificacion, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = @"SELECT TOP 1 * FROM producto_clasificacion WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeClasificacion, dt.Rows[0]);
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

    public static void Valida_Atributos(clsBeProducto_clasificacionSimple entity, SqlConnection conn, SqlTransaction tx)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (entity.Codigo == null)
            throw new ArgumentNullException(nameof(entity.Codigo), "El código no puede ser nulo.");

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        var Clasificacion = new clsBeProducto_clasificacion();
        bool existe = Existe_By_Codigo(entity.Codigo, ref Clasificacion, conn, tx);

        var BeInavConfigEnc = new clsBeI_nav_config_enc();
        clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, conn, tx);

        if (BeInavConfigEnc == null)
            throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

        if (!existe)
        {
            if (!string.IsNullOrEmpty(entity.Codigo))
            {
                Clasificacion.IdClasificacion = MaxID(conn, tx) + 1;
                Clasificacion.Codigo = entity.Codigo;
                Clasificacion.Nombre = entity.Nombre ?? entity.Codigo;
                Clasificacion.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                Clasificacion.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                Clasificacion.Fec_agr = DateTime.Now;
                Clasificacion.Fec_mod = DateTime.Now;
                Clasificacion.Activo = entity.Activo;
                Clasificacion.IdPropietario = entity.IdPropietario;
                Insertar(Clasificacion, conn, tx);
            }
        }
        else
        {
            Clasificacion.Codigo = entity.Codigo;
            Clasificacion.Nombre = entity.Nombre ?? entity.Codigo;
            Clasificacion.User_mod = BeInavConfigEnc.IdUsuario.ToString();
            Clasificacion.Fec_mod = DateTime.Now;
            Clasificacion.Activo = entity.Activo;
            Actualizar(Clasificacion, conn, tx);
        }
    }

    public static clsBeProducto_clasificacion? GetSingle(int pIdClasificacion,
                                                        SqlConnection lConnection,
                                                        SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT TOP 1 * FROM producto_clasificacion 
                       WHERE IdClasificacion = @IdClasificacion";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT?.Rows.Count > 0)
                {
                    clsBeProducto_clasificacion Obj = new clsBeProducto_clasificacion();
                    Cargar(ref Obj, lDT.Rows[0]);
                    return Obj;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
}