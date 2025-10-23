using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Interface;
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
    public static int InsertOrUpdate(clsBeProducto_familia be, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            if (Existe(be.IdFamilia, conn, tx))
                return Actualizar(be, conn, tx);
            else
                return Insertar(be, conn, tx);
        }
        catch
        {
            throw;
        }
    }
    public static int Insertar(clsBeProducto_familia be, SqlConnection conn, SqlTransaction tx)
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

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            Bind(cmd, be);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static int Actualizar(clsBeProducto_familia be, SqlConnection conn, SqlTransaction tx)
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

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            Bind(cmd, be);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        catch (Exception)
        {
            throw;
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
    public static int MaxID(SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT ISNULL(MAX(IdFamilia), 0) FROM Producto_familia";

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
    public static void Valida_Atributos(clsBeProducto_familiaSimple entity, SqlConnection connection, SqlTransaction tx)
    {
        try
        {
            var Familia = new clsBeProducto_familia();
            bool existe = Existe_By_Codigo(entity.Codigo, ref Familia, connection, tx);

            var BeInavConfigEnc = new clsBeI_nav_config_enc();
            clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, connection, tx);

            if (BeInavConfigEnc == null)
                throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

            if (!existe)
            {
                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    Familia.IdFamilia = MaxID(connection, tx) + 1;
                    Familia.Codigo = entity.Codigo;
                    Familia.Nombre = entity.Nombre ?? entity.Codigo;
                    Familia.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                    Familia.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                    Familia.Fec_agr = DateTime.Now;
                    Familia.Fec_mod = DateTime.Now;
                    Familia.Activo = entity.Activo;
                    Familia.IdPropietario = entity.IdPropietario;
                    Insertar(Familia, connection, tx);
                }
            }
            else
            {
                Familia.Codigo = entity.Codigo;
                Familia.Nombre = entity.Nombre ?? entity.Codigo;
                Familia.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                Familia.Fec_mod = DateTime.Now;
                Familia.Activo = entity.Activo;
                Actualizar(Familia, connection, tx);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}