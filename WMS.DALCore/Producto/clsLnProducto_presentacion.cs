using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using WMS.EntityCore.Producto;

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
    public static int Insertar(clsBeProducto_presentacion e, SqlConnection conn, SqlTransaction tx)
    {
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
            //Ins.Add("idpresentacionpallet", "@IdPresentacionPallet", "F");
            Ins.Add("codigo", "@codigo", "F");

            string sql = Ins.SQL();

            using var cmd = new SqlCommand(sql, conn, tx)
            {
                CommandType = CommandType.Text
            };

            BindParameters(cmd, e);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
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
    public static int Actualizar(clsBeProducto_presentacion e, SqlConnection conn, SqlTransaction tx)
    {
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
             //Upd.Add("idpresentacionpallet", "@IdPresentacionPallet", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Where("IdPresentacion = @IdPresentacion");

            string sql = Upd.SQL();

            using var cmd = new SqlCommand(sql, conn, tx)
            {
                CommandType = CommandType.Text
            };

            BindParameters(cmd, e);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sql = "SELECT ISNULL(MAX(IdPresentacion), 0) FROM Producto_presentacion";

        try
        {
            using var cmd = new SqlCommand(sql, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            object? result = cmd.ExecuteScalar();
            int max = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            return max;
        }
        catch (Exception)
        {
            throw;
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
        //cmd.Parameters.AddWithValue("@IdPresentacionPallet", e.IdPresentacionPallet);
        cmd.Parameters.AddWithValue("@codigo", e.Codigo);
    }
    public static int InsertOrUpdate(List<clsBeProducto_presentacion> lista, SqlConnection cn, SqlTransaction tx)
    {
        try
        {
            int total = 0;
            foreach (var oBe in lista)
            {
                if (Existe(oBe, cn, tx))
                    total += Actualizar(oBe, cn, tx);
                else
                    total += Insertar(oBe, cn, tx);
            }
            return total;
        }
        catch
        {
            throw;
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
    public static int InsertOrUpdate(clsBeProducto_presentacion oBe, SqlConnection cn, SqlTransaction tx)
    {
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

    public static bool Existe_By_Codigo(string Codigo, ref clsBeProducto_presentacion pBePresentacion, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = @"SELECT TOP 1 * FROM producto_presentacion WHERE codigo = @codigo AND IdProducto = @IdProducto";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);
            cmd.Parameters.AddWithValue("@IdProducto", pBePresentacion.IdProducto);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBePresentacion, dt.Rows[0]);
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

    public static void Valida_Atributos(clsBeProducto_presentacionMi3 pPresentacionMi3, SqlConnection connection, SqlTransaction tx)
    {
        if (pPresentacionMi3.Codigo_presentacion == null)
            throw new ArgumentNullException(nameof(pPresentacionMi3.Codigo_presentacion), "El código no puede ser nulo.");

        try
        {
            var Presentacion = new clsBeProducto_presentacion();
            bool existe = Existe_By_Codigo(pPresentacionMi3.Codigo_presentacion.Trim(), ref Presentacion, connection, tx);

            var BeInavConfigEnc = new clsBeI_nav_config_enc();
            clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, connection, tx);

            if (BeInavConfigEnc == null)
                throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

            if (!existe)
            {
                if (!string.IsNullOrEmpty(pPresentacionMi3.Codigo_presentacion))
                {
                    var BeProducto = new clsBeProducto();
                    var ExisteProducto = clsLnProducto.Existe_By_Codigo(pPresentacionMi3.Codigo_producto.Trim(), ref BeProducto, connection, tx);

                    if (ExisteProducto)
                    {
                        Presentacion.IdPresentacion = MaxID(connection, tx) + 1;
                        Presentacion.IdProducto = BeProducto.IdProducto;
                        Presentacion.Codigo = pPresentacionMi3.Codigo_presentacion.Trim();
                        Presentacion.Nombre = pPresentacionMi3.Nombre ?? pPresentacionMi3.Codigo_presentacion.Trim();
                        Presentacion.Factor = pPresentacionMi3.Factor;
                        Presentacion.Activo = pPresentacionMi3.Activo;
                        Presentacion.EsPallet = pPresentacionMi3.EsPallet;
                        Presentacion.Genera_lp_auto = pPresentacionMi3.Genera_lp_auto;
                        Presentacion.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                        Presentacion.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                        Presentacion.Fec_agr = DateTime.Now;
                        Presentacion.Fec_mod = DateTime.Now;
                        Presentacion.CamasPorTarima = pPresentacionMi3.CamasPorTarima;
                        Presentacion.CajasPorCama= pPresentacionMi3.CajasPorCama;
                        Insertar(Presentacion, connection, tx);
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(pPresentacionMi3.Codigo_producto), "El código de producto no existe.");
                    }
                }
            }
            else
            {
                Presentacion.Codigo = pPresentacionMi3.Codigo_presentacion;
                Presentacion.Nombre = pPresentacionMi3.Nombre ?? pPresentacionMi3.Codigo_presentacion;
                Presentacion.Factor = pPresentacionMi3.Factor;
                Presentacion.Activo = pPresentacionMi3.Activo;
                Presentacion.EsPallet = pPresentacionMi3.EsPallet;
                Presentacion.Genera_lp_auto = pPresentacionMi3.Genera_lp_auto;
                Presentacion.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                Presentacion.Fec_mod = DateTime.Now;
                Presentacion.CamasPorTarima = pPresentacionMi3.CamasPorTarima;
                Presentacion.CajasPorCama = pPresentacionMi3.CajasPorCama;
                Actualizar(Presentacion, connection, tx);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeProducto_presentacion? Existe_Presentacion_By_Nombre(int pIdProducto,
                                                                            string? pNombre,
                                                                            SqlConnection lConnection,
                                                                            SqlTransaction lTransaction)
    {
        if (string.IsNullOrWhiteSpace(pNombre))
            return null;

        clsBeProducto_presentacion? result = null;

        const string vSQL = @"
            SELECT * 
            FROM producto_presentacion 
            WHERE nombre = @pNombre 
              AND idproducto = @IdProducto";

        try
        {
            using (var cmd = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pNombre", pNombre.Trim());
                cmd.Parameters.AddWithValue("@IdProducto", pIdProducto);

                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        var obj = new clsBeProducto_presentacion();                        
                        Cargar(ref obj, row);
                        result = obj;
                    }
                }
            }
        }        
        catch (Exception)
        {         
            throw;
        }

        return result;
    }

    public static clsBeProducto_presentacion? Existe_Presentacion_By_Codigo(int pIdProducto,
                                                                            string pCodigo,
                                                                            SqlConnection lConnection,
                                                                            SqlTransaction lTransaction)
    {
        if (string.IsNullOrWhiteSpace(pCodigo))
            return null;

        clsBeProducto_presentacion? result = null;

        const string vSQL = @"
            SELECT * 
            FROM producto_presentacion 
            WHERE IdProducto = @pIdProducto 
              AND codigo = @pCodigo";

        try
        {
            using (var cmd = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pIdProducto", pIdProducto);
                cmd.Parameters.AddWithValue("@pCodigo", pCodigo.Trim());

                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        var obj = new clsBeProducto_presentacion();
                        Cargar(ref obj, row);
                        result = obj;
                    }
                }
            }
        }        
        catch (Exception)
        {            
            throw;
        }

        return result;
    }

    public static bool GetSingle(ref clsBeProducto_presentacion pBeProducto_presentacion,
                                 SqlConnection lConnection,
                                 SqlTransaction? lTransaction)
    {
        bool result = false;

        try
        {
            const string sp = @"SELECT * FROM Producto_presentacion 
                            WHERE (IdPresentacion = @IdPresentacion)";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPRESENTACION", pBeProducto_presentacion.IdPresentacion));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                pBeProducto_presentacion = new clsBeProducto_presentacion();
                Cargar(ref pBeProducto_presentacion, dt.Rows[0]);
                result = true;
            }

            return result;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static clsBeProducto_presentacion? Get_Presentacion_Defecto_By_IdProducto(int pIdProducto,
                                                                                    SqlConnection lConnection,
                                                                                    SqlTransaction? lTransaction)
    {
        string vSQL = "SELECT TOP(1) * FROM producto_presentacion WHERE (IdProducto = @pIdProducto)";

        try
        {
            using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(vSQL, lConnection))
            {
                lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                lDataAdapter.SelectCommand.Transaction = lTransaction;
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto);

                DataTable lDataTable = new DataTable();
                lDataAdapter.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    DataRow lRow = lDataTable.Rows[0];
                    clsBeProducto_presentacion BeProductoPresentacion = new clsBeProducto_presentacion();
                    Cargar(ref BeProductoPresentacion, lRow);
                    return BeProductoPresentacion;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static clsBeProducto_presentacion? GetSingle(int pIdPresentacion,
                                                       SqlConnection lConnection,
                                                       SqlTransaction lTransaction)
    {
        string vSQL = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion";

        using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(vSQL, lConnection))
        {
            lDataAdapter.SelectCommand.CommandType = CommandType.Text;
            lDataAdapter.SelectCommand.Transaction = lTransaction;
            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion);

            DataTable lDataTable = new DataTable();
            lDataAdapter.Fill(lDataTable);

            if (lDataTable != null && lDataTable.Rows.Count > 0)
            {
                DataRow lRow = lDataTable.Rows[0];
                clsBeProducto_presentacion Obj = new clsBeProducto_presentacion();
                Cargar(ref Obj, lRow);
                return Obj;
            }
        }

        return null;
    }

    public static bool Obtener(clsBeProducto_presentacion oBeProducto_presentacion,
                              SqlConnection lConnection,
                              SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Producto_presentacion 
                           WHERE IdPresentacion = @IdPresentacion";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion);

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeProducto_presentacion, dt.Rows[0]);
                        return true;
                    }
                }
            }

            return false;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static clsBeProducto_presentacion? Get_Presentacion_By_IdProductoBodega_And_CodPres(int pIdProductoBodega,
                                                                                         string pCodigo,
                                                                                         SqlConnection lConnection,
                                                                                         SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT TOP 1 pp.* FROM producto_presentacion pp inner join 
                producto_bodega pb on pp.idproducto = pb.idproducto
                WHERE (pp.codigo=@Codigo or pp.nombre=@Codigo) and pb.idproductobodega=@IdProductoBodega
                ORDER BY CASE WHEN pp.codigo=@Codigo THEN 0 ELSE 1 END";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeProducto_presentacion ObjUM = new clsBeProducto_presentacion();
                    Cargar(ref ObjUM, lRow);
                    return ObjUM;
                }
            }

            return null;
        }
        catch
        {
            throw;
        }
    }
    public static List<clsBeProducto_presentacion> GetByIds(IConfiguration configuration, List<int> presentacionIds)
    {
        if (presentacionIds == null || presentacionIds.Count == 0)
            return new List<clsBeProducto_presentacion>();

        presentacionIds = presentacionIds.Where(id => id > 0).Distinct().ToList();
        if (presentacionIds.Count == 0)
            return new List<clsBeProducto_presentacion>();

        var result = new List<clsBeProducto_presentacion>();

        using var lConnection = new SqlConnection(configuration.GetConnectionString("CST") ?? configuration["CST"]);
        SqlTransaction? lTransaction = null;

        try
        {
            lConnection.Open();
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            var paramNames = presentacionIds.Select((_, i) => $"@p{i}").ToList();

            string sql = $@"
SELECT *
FROM Producto_presentacion
WHERE IdPresentacion IN ({string.Join(",", paramNames)})";

            using var cmd = new SqlCommand(sql, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            for (int i = 0; i < presentacionIds.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int) { Value = presentacionIds[i] });
            }

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var be = new clsBeProducto_presentacion();
                Cargar(ref be, dr);
                result.Add(be);
            }

            lTransaction.Commit();
            return result;
        }
        catch
        {
            lTransaction?.Rollback();
            throw;
        }
        finally
        {
            lTransaction?.Dispose();
        }
    }

    public static async Task<Dictionary<int, clsBeProducto_presentacion>> GetByIdsAsync(IConfiguration configuration, List<int> idsPresentacion)
    {
        var result = new Dictionary<int, clsBeProducto_presentacion>();

        if (idsPresentacion == null || idsPresentacion.Count == 0)
            return result;

        idsPresentacion = idsPresentacion.Where(id => id > 0).Distinct().ToList();
        if (idsPresentacion.Count == 0)
            return result;

        const string sql = "SELECT * FROM Producto_presentacion WHERE IdPresentacion IN ({0})";

        using var conn = new SqlConnection(configuration.GetConnectionString("CST") ?? configuration["CST"]);
        await conn.OpenAsync();

        using var tran = (SqlTransaction) await conn.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

        try
        {
            var paramNames = idsPresentacion.Select((_, i) => $"@p{i}").ToList();
            var finalSql = string.Format(sql, string.Join(",", paramNames));

            using var cmd = new SqlCommand(finalSql, conn, tran)
            {
                CommandType = CommandType.Text
            };

            for (int i = 0; i < idsPresentacion.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int)
                {
                    Value = idsPresentacion[i]
                });
            }

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            await tran.CommitAsync();

            foreach (DataRow dr in dt.Rows)
            {
                var be = new clsBeProducto_presentacion();
                Cargar(ref be, dr);
                result[be.IdPresentacion] = be;
            }

            return result;
        }
        catch
        {
            await tran.RollbackAsync();
            throw;
        }
    }
    public static Task<Dictionary<int, clsBeProducto_presentacion>> GetByIdsAsync(
    SqlConnection connection,
    SqlTransaction transaction,
    List<int> idsPresentacion)
    {
        var result = new Dictionary<int, clsBeProducto_presentacion>();

        if (idsPresentacion == null || idsPresentacion.Count == 0)
            return Task.FromResult(result);

        idsPresentacion = idsPresentacion.Where(id => id > 0).Distinct().ToList();
        if (idsPresentacion.Count == 0)
            return Task.FromResult(result);

        const string sql = "SELECT * FROM Producto_presentacion WHERE IdPresentacion IN ({0})";

        try
        {
            var paramNames = idsPresentacion.Select((_, i) => $"@p{i}").ToList();
            var finalSql = string.Format(sql, string.Join(",", paramNames));

            using var cmd = new SqlCommand(finalSql, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            for (int i = 0; i < idsPresentacion.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int)
                {
                    Value = idsPresentacion[i]
                });
            }

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var be = new clsBeProducto_presentacion();
                Cargar(ref be, dr);
                result[be.IdPresentacion] = be;
            }

            return Task.FromResult(result);
        }
        catch
        {
            throw;
        }
    }
}
