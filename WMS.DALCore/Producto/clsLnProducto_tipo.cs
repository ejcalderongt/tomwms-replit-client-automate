using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_tipo
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_tipo oBeProducto_tipo, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeProducto_tipo.IdTipoProducto = GetInt("IdTipoProducto");
            oBeProducto_tipo.IdPropietario = GetInt("IdPropietario");
            oBeProducto_tipo.NombreTipoProducto = GetString("NombreTipoProducto");
            oBeProducto_tipo.Activo = GetBool("Activo");
            oBeProducto_tipo.User_agr = GetString("user_agr");
            oBeProducto_tipo.Fec_agr = GetDate("fec_agr");
            oBeProducto_tipo.User_mod = GetString("user_mod");
            oBeProducto_tipo.Fec_mod = GetDate("fec_mod");
            oBeProducto_tipo.Codigo = GetString("codigo");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);

            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(clsBeProducto_tipo oBeProducto_tipo, IConfiguration config, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        SqlConnection? cnLocal = null;
        SqlTransaction? txLocal = null;
        bool remota = cn != null && tx != null;

        try
        {
            Ins.Init("producto_tipo");
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombretipoproducto", "@nombretipoproducto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("codigo", "@codigo", "F");

            string sql = Ins.SQL();

            if (!remota)
            {
                cnLocal = new SqlConnection(config.GetConnectionString("CST"));
                cnLocal.Open();
                txLocal = cnLocal.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, remota ? cn! : cnLocal!, remota ? tx! : txLocal!);
            Bind(cmd, oBeProducto_tipo);
            int result = cmd.ExecuteNonQuery();

            if (!remota)
                txLocal!.Commit();

            return result;
        }
        catch (Exception ex)
        {
            if (!remota && txLocal is not null)
                txLocal.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
        finally
        {
            if (!remota)
            {
                txLocal?.Dispose();
                if (cnLocal?.State == ConnectionState.Open)
                    cnLocal.Close();
                cnLocal?.Dispose();
            }
        }
    }

    public static int Insertar(IConfiguration config, clsBeProducto_tipo oBeProducto_tipo)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("producto_tipo");
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombretipoproducto", "@nombretipoproducto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("codigo", "@codigo", "F");

            string sql = Ins.SQL();
            using var cn = new SqlConnection(config.GetConnectionString("CST"));
            cn.Open();
            using var tx = cn.BeginTransaction();
            using var cmd = new SqlCommand(sql, cn, tx);
            Bind(cmd, oBeProducto_tipo);
            int result = cmd.ExecuteNonQuery();
            tx.Commit();
            return result;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public static int Actualizar(clsBeProducto_tipo oBeProducto_tipo, IConfiguration config, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        Upd.Init("producto_tipo");
        Upd.Add("idtipoproducto", "@idtipoproducto", "F");
        Upd.Add("idpropietario", "@idpropietario", "F");
        Upd.Add("nombretipoproducto", "@nombretipoproducto", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("codigo", "@codigo", "F");
        Upd.Where("IdTipoProducto = @IdTipoProducto");

        string sql = Upd.SQL();
        bool remota = cn != null && tx != null;

        using var cnLocal = remota ? null : new SqlConnection(config.GetConnectionString("CST"));
        if (!remota) cnLocal!.Open();
        using var txLocal = remota ? null : cnLocal!.BeginTransaction();

        using var cmd = new SqlCommand(sql, remota ? cn! : cnLocal!, remota ? tx! : txLocal!);
        Bind(cmd, oBeProducto_tipo);
        int result = cmd.ExecuteNonQuery();
        if (!remota) txLocal!.Commit();

        return result;
    }
    public int Eliminar(IConfiguration config, clsBeProducto_tipo oBeProducto_tipo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Producto_tipo" +
             "  Where(IdTipoProducto = @IdTipoProducto)");

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            cmd.Parameters.Add(new SqlParameter("@IdTipoProducto", oBeProducto_tipo.IdTipoProducto));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return rowsAffected;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public DataTable Listar(IConfiguration config)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM Producto_tipo";
            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            return dt;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public static bool GetSingle(IConfiguration config, ref clsBeProducto_tipo pBeProducto_tipo)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Producto_tipo" +
            " Where(IdTipoProducto = @IdTipoProducto)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoProducto", pBeProducto_tipo.IdTipoProducto));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProducto_tipo, r);
                return true;
            }

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeProducto_tipo> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProducto_tipo> lreturnList = new List<clsBeProducto_tipo>();

        try
        {
            const string sp = "Select * FROM Producto_tipo";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {

                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeProducto_tipo vBeProducto_tipo = new clsBeProducto_tipo();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProducto_tipo = new clsBeProducto_tipo();
                            Cargar(ref vBeProducto_tipo, dr);
                            lreturnList.Add(vBeProducto_tipo);
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();

                }

            }

            return lreturnList;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
    }

    public static int MaxID(IConfiguration config)
    {
        const string sql = "SELECT ISNULL(MAX(IdTipoProducto), 0) FROM producto_tipo";

        using var cn = new SqlConnection(config.GetConnectionString("CST"));
        cn.Open();

        SqlTransaction? tx = cn.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            using var cmd = new SqlCommand(sql, cn, tx);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            tx.Commit();
            return result;
        }
        catch (Exception ex)
        {
            tx?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int MaxID(IConfiguration config, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdTipoProducto), 0) FROM producto_tipo";
        bool remota = cn != null && tx != null;

        using var cnLocal = remota ? null : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? txLocal = null;

        try
        {
            if (!remota)
            {
                cnLocal!.Open();
                txLocal = cnLocal.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, remota ? cn! : cnLocal!, remota ? tx! : txLocal!);
            int result = Convert.ToInt32(cmd.ExecuteScalar());

            if (!remota) txLocal?.Commit();
            return result;
        }
        catch (Exception ex)
        {
            if (!remota) txLocal?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static void Bind(SqlCommand cmd, clsBeProducto_tipo e)
    {
        cmd.Parameters.AddWithValue("@IdTipoProducto", e.IdTipoProducto);
        cmd.Parameters.AddWithValue("@IdPropietario", e.IdPropietario);
        cmd.Parameters.AddWithValue("@NombreTipoProducto", e.NombreTipoProducto);
        cmd.Parameters.AddWithValue("@Activo", e.Activo);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@codigo", e.Codigo);
    }
    public static bool Existe(clsBeProducto_tipo oBe, IConfiguration config)
    {
        const string sql = "SELECT COUNT(1) FROM producto_tipo WHERE IdTipoProducto = @IdTipoProducto";

        try
        {
            using var cn = new SqlConnection(config.GetConnectionString("CST"));
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@IdTipoProducto", oBe.IdTipoProducto);

            cn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_tipo oBe, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        return Existe(oBe, config)
            ? Actualizar(oBe, config, cn, tx)
            : Insertar(oBe, config, cn, tx);
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeProducto_tipo pBeProductoTipo, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = "SELECT TOP 1 * FROM producto_tipo WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeProductoTipo, dt.Rows[0]);
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

    public static void Valida_Atributos(IConfiguration config, clsBeProducto_tipoMi3 entity, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            var BeProductoTipo = new clsBeProducto_tipo();
            bool existe = Existe_By_Codigo(entity.Codigo, ref BeProductoTipo, connection, isExternalTx ? tx! : localTx!);

            if (!existe)
            {

                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    var BeTipoProducto = new clsBeProducto_tipo();
                    BeTipoProducto.IdTipoProducto= MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
                    BeTipoProducto.IdPropietario = entity.IdPropietario;
                    BeTipoProducto.Codigo = entity.Codigo;
                    BeTipoProducto.NombreTipoProducto = entity.NombreTipoProducto ?? entity.Codigo;
                    BeTipoProducto.User_agr = "1";
                    BeTipoProducto.User_mod = "1";
                    BeTipoProducto.Fec_agr = DateTime.Now;
                    BeTipoProducto.Fec_mod = DateTime.Now;
                    BeTipoProducto.Activo = entity.Activo;
                    Insertar( BeTipoProducto, config, connection, isExternalTx ? tx : localTx);
                }

            }
            else
            {
                BeProductoTipo.Codigo = entity.Codigo;
                BeProductoTipo.NombreTipoProducto = entity.NombreTipoProducto ?? entity.Codigo;
                BeProductoTipo.User_mod = "1";
                BeProductoTipo.Fec_mod = DateTime.Now;
                BeProductoTipo.Activo = entity.Activo;
                Actualizar( BeProductoTipo, config, connection, isExternalTx ? tx : localTx);

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