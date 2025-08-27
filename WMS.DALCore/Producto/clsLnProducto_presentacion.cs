using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_presentacion
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_presentacion oBe, DataRow dr)
    {
        int GetInt(string col) => dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]);
        bool GetBool(string col) => dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]);
        string GetString(string col) => dr[col] is DBNull ? "" : Convert.ToString(dr[col]) ?? "";
        DateTime GetDate(string col) => dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]);
        double GetDouble(string col) => dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]);

        try
        {
            oBe.IdPresentacion = GetInt("IdPresentacion");
            oBe.IdProducto = GetInt("IdProducto");
            oBe.Codigo_barra = GetString("codigo_barra");
            oBe.Nombre = GetString("nombre");
            oBe.Imprime_barra = GetBool("imprime_barra");
            oBe.Peso = GetDouble("peso");
            oBe.Alto = GetDouble("alto");
            oBe.Largo = GetDouble("largo");
            oBe.Ancho = GetDouble("ancho");
            oBe.Factor = GetDouble("factor");
            oBe.MinimoExistencia = GetDouble("MinimoExistencia");
            oBe.MaximoExistencia = GetDouble("MaximoExistencia");
            oBe.User_agr = GetString("user_agr");
            oBe.Fec_agr = GetDate("fec_agr");
            oBe.User_mod = GetString("user_mod");
            oBe.Fec_mod = GetDate("fec_mod");
            oBe.Activo = GetBool("activo");
            oBe.EsPallet = GetBool("EsPallet");
            oBe.Precio = GetDouble("Precio");
            oBe.MinimoPeso = GetDouble("MinimoPeso");
            oBe.MaximoPeso = GetDouble("MaximoPeso");
            oBe.Costo = GetDouble("Costo");
            oBe.CamasPorTarima = GetDouble("CamasPorTarima");
            oBe.CajasPorCama = GetDouble("CajasPorCama");
            oBe.Genera_lp_auto = GetBool("genera_lp_auto");
            oBe.Permitir_paletizar = GetBool("permitir_paletizar");
            oBe.Sistema = GetBool("sistema");
            oBe.IdPresentacionPallet = GetInt("IdPresentacionPallet");
            oBe.Codigo = GetString("codigo");
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            string msg = string.Format("{0} {1}", method, ex.Message);
            throw new Exception(msg);
        }
    }
    public static int Insertar(IConfiguration config, clsBeProducto_presentacion e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        int rowsAffected;
        bool esTransaccionExterna = conn != null && tx != null;

        using var connection = esTransaccionExterna ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        if (!esTransaccionExterna)
        {
            connection.Open();
            localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            Ins.Init("producto_presentacion");
            Ins.Add("idpresentacion", "@IdPresentacion", "F");
            Ins.Add("idproducto", "@IdProducto", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("imprime_barra", "@imprime_barra", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("factor", "@factor", "F");
            Ins.Add("minimoexistencia", "@MinimoExistencia", "F");
            Ins.Add("maximoexistencia", "@MaximoExistencia", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("espallet", "@EsPallet", "F");
            Ins.Add("precio", "@Precio", "F");
            Ins.Add("minimopeso", "@MinimoPeso", "F");
            Ins.Add("maximopeso", "@MaximoPeso", "F");
            Ins.Add("costo", "@Costo", "F");
            Ins.Add("camasportarima", "@CamasPorTarima", "F");
            Ins.Add("cajasporcama", "@CajasPorCama", "F");
            Ins.Add("genera_lp_auto", "@genera_lp_auto", "F");
            Ins.Add("permitir_paletizar", "@permitir_paletizar", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("idpresentacionpallet", "@IdPresentacionPallet", "F");
            Ins.Add("codigo", "@codigo", "F");

            string sql = Ins.SQL();

            using var cmd = new SqlCommand(sql, connection, esTransaccionExterna ? tx : localTx)
            {
                CommandType = CommandType.Text
            };

            BindParameters(cmd, e);
            rowsAffected = cmd.ExecuteNonQuery();

            if (!esTransaccionExterna)
                localTx?.Commit();
        }
        catch (Exception ex)
        {
            if (!esTransaccionExterna)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method} {ex.Message}");
        }

        return rowsAffected;
    }
    public static int Insertar(IConfiguration config, clsBeProducto_presentacion e)
    {
        int rowsAffected = 0;
        SqlTransaction? transaction = null;

        using var connection = new SqlConnection(config.GetConnectionString("CST"));
        try
        {
            Ins.Init("producto_presentacion");
            Ins.Add("idpresentacion", "@IdPresentacion", "F");
            Ins.Add("idproducto", "@IdProducto", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("imprime_barra", "@imprime_barra", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("factor", "@factor", "F");
            Ins.Add("minimoexistencia", "@MinimoExistencia", "F");
            Ins.Add("maximoexistencia", "@MaximoExistencia", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("espallet", "@EsPallet", "F");
            Ins.Add("precio", "@Precio", "F");
            Ins.Add("minimopeso", "@MinimoPeso", "F");
            Ins.Add("maximopeso", "@MaximoPeso", "F");
            Ins.Add("costo", "@Costo", "F");
            Ins.Add("camasportarima", "@CamasPorTarima", "F");
            Ins.Add("cajasporcama", "@CajasPorCama", "F");
            Ins.Add("genera_lp_auto", "@genera_lp_auto", "F");
            Ins.Add("permitir_paletizar", "@permitir_paletizar", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("idpresentacionpallet", "@IdPresentacionPallet", "F");
            Ins.Add("codigo", "@codigo", "F");

            string sql = Ins.SQL();

            connection.Open();
            transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            BindParameters(cmd, e);

            rowsAffected = cmd.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (SqlException ex)
        {
            transaction?.Rollback();
            string methodName = new StackTrace().GetFrame(0)?.GetMethod()?.Name ?? "Insertar";
            throw new Exception($"{methodName}: {ex.Message}", ex);
        }

        return rowsAffected;
    }
    public static int Actualizar(IConfiguration config, clsBeProducto_presentacion e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        int rowsAffected = 0;
        bool esTransaccionExterna = conn != null && tx != null;

        using var connection = esTransaccionExterna ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            Upd.Init("producto_presentacion");
            Upd.Add("idpresentacion", "@IdPresentacion", "F");
            Upd.Add("idproducto", "@IdProducto", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("imprime_barra", "@imprime_barra", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("factor", "@factor", "F");
            Upd.Add("minimoexistencia", "@MinimoExistencia", "F");
            Upd.Add("maximoexistencia", "@MaximoExistencia", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("espallet", "@EsPallet", "F");
            Upd.Add("precio", "@Precio", "F");
            Upd.Add("minimopeso", "@MinimoPeso", "F");
            Upd.Add("maximopeso", "@MaximoPeso", "F");
            Upd.Add("costo", "@Costo", "F");
            Upd.Add("camasportarima", "@CamasPorTarima", "F");
            Upd.Add("cajasporcama", "@CajasPorCama", "F");
            Upd.Add("genera_lp_auto", "@genera_lp_auto", "F");
            Upd.Add("permitir_paletizar", "@permitir_paletizar", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("idpresentacionpallet", "@IdPresentacionPallet", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Where("IdPresentacion = @IdPresentacion");

            string sql = Upd.SQL();

            if (!esTransaccionExterna)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, connection, esTransaccionExterna ? tx : localTx)
            {
                CommandType = CommandType.Text
            };

            BindParameters(cmd, e);

            rowsAffected = cmd.ExecuteNonQuery();

            if (!esTransaccionExterna)
                localTx?.Commit();
        }
        catch (SqlException ex)
        {
            if (!esTransaccionExterna)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod()?.Name ?? "Actualizar";
            throw new Exception($"{method}: {ex.Message}", ex);
        }

        return rowsAffected;
    }
    public static int Eliminar(IConfiguration config, clsBeProducto_presentacion e, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        int rowsAffected = 0;
        bool esTransaccionExterna = conn != null && tx != null;

        using var connection = esTransaccionExterna ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            const string sql = "DELETE FROM Producto_presentacion WHERE IdPresentacion = @IdPresentacion";

            if (!esTransaccionExterna)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, connection, esTransaccionExterna ? tx : localTx)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@IdPresentacion", e.IdPresentacion);

            rowsAffected = cmd.ExecuteNonQuery();

            if (!esTransaccionExterna)
                localTx?.Commit();
        }
        catch (SqlException ex)
        {
            if (!esTransaccionExterna)
                localTx?.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod()?.Name ?? "Eliminar";
            throw new Exception($"{method}: {ex.Message}", ex);
        }

        return rowsAffected;
    }
    public DataTable Listar(IConfiguration config)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM Producto_presentacion";
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
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_presentacion pBe)
    {
        using var connection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? transaction = null;

        try
        {
            const string query = "SELECT * FROM Producto_presentacion WHERE IdPresentacion = @IdPresentacion";

            connection.Open();
            transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(query, connection, transaction);
            cmd.Parameters.AddWithValue("@IdPresentacion", pBe.IdPresentacion);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);

            transaction.Commit();

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBe, dt.Rows[0]);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            transaction?.Rollback();
            var method = new StackTrace().GetFrame(0)?.GetMethod()?.Name ?? "GetSingle";
            throw new Exception($"{method}: {ex.Message}", ex);
        }
    }
    public static List<clsBeProducto_presentacion> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProducto_presentacion> lreturnList = new List<clsBeProducto_presentacion>();

        try
        {
            const string sp = "Select * FROM Producto_presentacion";

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

                        clsBeProducto_presentacion vBeProducto_presentacion = new clsBeProducto_presentacion();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProducto_presentacion = new clsBeProducto_presentacion();
                            Cargar(ref vBeProducto_presentacion, dr);
                            lreturnList.Add(vBeProducto_presentacion);
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

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdPresentacion),0) FROM Producto_presentacion";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        Object lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((String)lreturnValue);
                        }
                    }
                    lTransaction.Commit();
                }

                lConnection.Close();
            }

            return lMax;

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
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdPresentacion), 0) FROM Producto_presentacion";
        bool transaccionExterna = pConection != null && pTransaction != null;

        using var connection = transaccionExterna ? pConection! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!transaccionExterna)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var cmd = new SqlCommand(sql, connection, transaccionExterna ? pTransaction! : localTx)
            {
                CommandType = CommandType.Text
            };

            object? result = cmd.ExecuteScalar();
            int max = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

            if (!transaccionExterna)
                localTx?.Commit();

            return max;
        }
        catch (Exception ex)
        {
            if (!transaccionExterna)
                localTx?.Rollback();

            string method = new StackTrace().GetFrame(0)?.GetMethod()?.Name ?? "MaxID";
            throw new Exception($"{method}: {ex.Message}", ex);
        }
    }
    private static void BindParameters(SqlCommand cmd, clsBeProducto_presentacion e)
    {
        cmd.Parameters.AddWithValue("@IdPresentacion", e.IdPresentacion);
        cmd.Parameters.AddWithValue("@IdProducto", e.IdProducto);
        cmd.Parameters.AddWithValue("@codigo_barra", e.Codigo_barra);
        cmd.Parameters.AddWithValue("@nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@imprime_barra", e.Imprime_barra);
        cmd.Parameters.AddWithValue("@peso", e.Peso);
        cmd.Parameters.AddWithValue("@alto", e.Alto);
        cmd.Parameters.AddWithValue("@largo", e.Largo);
        cmd.Parameters.AddWithValue("@ancho", e.Ancho);
        cmd.Parameters.AddWithValue("@factor", e.Factor);
        cmd.Parameters.AddWithValue("@MinimoExistencia", e.MinimoExistencia);
        cmd.Parameters.AddWithValue("@MaximoExistencia", e.MaximoExistencia);
        cmd.Parameters.AddWithValue("@user_agr", e.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", e.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", e.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", e.Fec_mod);
        cmd.Parameters.AddWithValue("@activo", e.Activo);
        cmd.Parameters.AddWithValue("@EsPallet", e.EsPallet);
        cmd.Parameters.AddWithValue("@Precio", e.Precio);
        cmd.Parameters.AddWithValue("@MinimoPeso", e.MinimoPeso);
        cmd.Parameters.AddWithValue("@MaximoPeso", e.MaximoPeso);
        cmd.Parameters.AddWithValue("@Costo", e.Costo);
        cmd.Parameters.AddWithValue("@CamasPorTarima", e.CamasPorTarima);
        cmd.Parameters.AddWithValue("@CajasPorCama", e.CajasPorCama);
        cmd.Parameters.AddWithValue("@genera_lp_auto", e.Genera_lp_auto);
        cmd.Parameters.AddWithValue("@permitir_paletizar", e.Permitir_paletizar);
        cmd.Parameters.AddWithValue("@sistema", e.Sistema);
        cmd.Parameters.AddWithValue("@IdPresentacionPallet", e.IdPresentacionPallet);
        cmd.Parameters.AddWithValue("@codigo", e.Codigo);
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto_presentacion oBe, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        bool externa = cn != null && tx != null;
        var lConn = externa ? cn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;

        if (!externa)
        {
            lConn.Open();
            lTx = lConn.BeginTransaction();
        }

        try
        {
            SqlConnection conn = externa ? lConn : lConn;
            SqlTransaction? tran = externa ? tx : lTx;

            if (Existe(oBe, conn, tran))
                return Actualizar(config, oBe, conn, tran);
            else
                return Insertar(config, oBe, conn, tran);
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
    public static bool Existe(clsBeProducto_presentacion oBe, IConfiguration config)
    {
        try
        {
            using var cn = new SqlConnection(config.GetConnectionString("CST"));
            const string sql = "SELECT COUNT(1) FROM Producto_presentacion WHERE IdPresentacion = @IdPresentacion";
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@IdPresentacion", oBe.IdPresentacion);

            cn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
    public static int InsertOrUpdate(IConfiguration config, List<clsBeProducto_presentacion> lista, SqlConnection? cn = null, SqlTransaction? tx = null)
    {
        bool externa = cn != null && tx != null;
        var lConn = externa ? cn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            int total = 0;
            foreach (var oBe in lista)
            {
                if (Existe(oBe, config))
                    total += Actualizar(config, oBe, lConn, externa ? tx : lTx);
                else
                    total += Insertar(config, oBe, lConn, externa ? tx : lTx);
            }

            if (!externa)
                lTx?.Commit();

            return total;
        }
        catch
        {
            if (!externa) lTx?.Rollback();
            throw;
        }
        finally
        {
            if (!externa)
                lConn.Close();
        }
    }
    public static bool Existe(clsBeProducto_presentacion oBe, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = "SELECT COUNT(1) FROM Producto_presentacion WHERE IdPresentacion = @IdPresentacion";
            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@IdPresentacion", oBe.IdPresentacion);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        catch (Exception ex)
        {
            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name} → {ex.Message}", ex);
        }
    }
}