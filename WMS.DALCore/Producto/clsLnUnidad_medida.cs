using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnUnidad_medida
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeUnidad_medida oBeUnidad_medida, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeUnidad_medida.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeUnidad_medida.IdPropietario = GetInt("IdPropietario");
            oBeUnidad_medida.Nombre = GetString("Nombre");
            oBeUnidad_medida.Activo = GetBool("activo");
            oBeUnidad_medida.Fec_agr = GetDate("fec_agr");
            oBeUnidad_medida.User_mod = GetString("user_mod");
            oBeUnidad_medida.Fec_mod = GetDate("fec_mod");
            oBeUnidad_medida.User_agr = GetString("user_agr");
            oBeUnidad_medida.Codigo = GetString("codigo");
            oBeUnidad_medida.Es_um_cobro = GetBool("es_um_cobro");
            oBeUnidad_medida.Factor = GetDouble("factor");
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

    public static int Insertar(clsBeUnidad_medida oBeUnidad_medida, SqlConnection pConection, SqlTransaction pTransaction)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            Ins.Init("unidad_medida");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("es_um_cobro", "@es_um_cobro", "F");
            Ins.Add("factor", "@factor", "F");

            string sp = Ins.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeUnidad_medida);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            cmd?.Dispose();
        }
    }

    public static int Insertar(IConfiguration config, clsBeUnidad_medida oBeUnidad_medida)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("unidad_medida");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("es_um_cobro", "@es_um_cobro", "F");
            Ins.Add("factor", "@factor", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeUnidad_medida);

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

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
        return rowsAffected;
    }
    public static int Actualizar(clsBeUnidad_medida oBeUnidad_medida, SqlConnection pConection, SqlTransaction pTransaction)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("unidad_medida");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("es_um_cobro", "@es_um_cobro", "F");
            Upd.Add("factor", "@factor", "F");
            Upd.Where("IdUnidadMedida = @IdUnidadMedida");

            string sp = Upd.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeUnidad_medida);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            cmd?.Dispose();
        }
    }
    public int Eliminar(IConfiguration config, clsBeUnidad_medida oBeUnidad_medida, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Unidad_medida" +
             "  Where(IdUnidadMedida = @IdUnidadMedida)");

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

            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeUnidad_medida.IdUnidadMedida));

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
            const string sp = "Select * FROM Unidad_medida";
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

    public static bool GetSingle(IConfiguration config, ref clsBeUnidad_medida pBeUnidad_medida)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Unidad_medida" +
            " Where(IdUnidadMedida = @IdUnidadMedida)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedida", pBeUnidad_medida.IdUnidadMedida));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeUnidad_medida, r);
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

    public static List<clsBeUnidad_medida> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeUnidad_medida> lreturnList = new List<clsBeUnidad_medida>();

        try
        {
            const string sp = "Select * FROM Unidad_medida";

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

                        clsBeUnidad_medida vBeUnidad_medida = new clsBeUnidad_medida();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeUnidad_medida = new clsBeUnidad_medida();
                            Cargar(ref vBeUnidad_medida, dr);
                            lreturnList.Add(vBeUnidad_medida);
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

            const string sp = "Select ISNULL(Max(IdUnidadMedida),0) FROM Unidad_medida";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        var lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = Convert.ToInt32(lreturnValue);
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
        try
        {
            const string sp = "Select ISNULL(Max(IdUnidadMedida),0) FROM Unidad_medida";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                var lreturnValue = cmd.ExecuteScalar();

                if (lreturnValue != DBNull.Value && lreturnValue != null)
                {
                    return Convert.ToInt32(lreturnValue);
                }
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static bool Existe(int idUnidadMedida, SqlConnection conn, SqlTransaction? transaction = null)
    {
        string query = "SELECT COUNT(1) FROM unidad_medida WHERE IdUnidadMedida = @IdUnidadMedida";
        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@IdUnidadMedida", idUnidadMedida);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
    public static int InsertOrUpdate(List<clsBeUnidad_medida> entities, SqlConnection connection, SqlTransaction tx)
    {
        try
        {
            int total = 0;

            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdUnidadMedida, connection, tx);

                int result = existe
                    ? Actualizar(entity, connection, tx)
                    : Insertar(entity, connection, tx);

                total += result;
            }

            return total;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static void Bind(SqlCommand cmd, clsBeUnidad_medida oBeUnidad_medida)
    {
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeUnidad_medida.IdUnidadMedida));
        cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeUnidad_medida.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@Nombre", (object?)oBeUnidad_medida.Nombre ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", (object?)oBeUnidad_medida.Activo ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", (object?)oBeUnidad_medida.Fec_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", (object?)oBeUnidad_medida.User_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", (object?)oBeUnidad_medida.Fec_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", (object?)oBeUnidad_medida.User_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo", (object?)oBeUnidad_medida.Codigo ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@es_um_cobro", (object?)oBeUnidad_medida.Es_um_cobro ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@factor", (object?)oBeUnidad_medida.Factor ?? DBNull.Value));
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeUnidad_medida pBeUmbas, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = @"SELECT TOP 1 * FROM unidad_medida WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeUmbas, dt.Rows[0]);
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

    public static void Valida_Atributos(clsBeUnidad_medidaMi3 entity, SqlConnection connection, SqlTransaction tx)
    {
        try
        {
            var BeUmbas = new clsBeUnidad_medida();
            bool existe = Existe_By_Codigo(entity.Codigo, ref BeUmbas, connection, tx);

            var BeInavConfigEnc = new clsBeI_nav_config_enc();
            clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, connection, tx);

            if (BeInavConfigEnc == null)
                throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

            if (!existe)
            {
                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    BeUmbas.IdUnidadMedida = MaxID(connection, tx) + 1;
                    BeUmbas.IdPropietario = entity.IdPropietario;
                    BeUmbas.Codigo = entity.Codigo;
                    BeUmbas.Nombre = entity.Nombre ?? entity.Codigo;
                    BeUmbas.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                    BeUmbas.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                    BeUmbas.Fec_agr = DateTime.Now;
                    BeUmbas.Fec_mod = DateTime.Now;
                    BeUmbas.Activo = entity.Activo;
                    BeUmbas.Es_um_cobro = false;
                    BeUmbas.Factor = 1;
                    Insertar(BeUmbas, connection, tx);
                }
            }
            else
            {
                BeUmbas.Codigo = entity.Codigo;
                BeUmbas.Nombre = entity.Nombre ?? entity.Codigo;
                BeUmbas.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                BeUmbas.Fec_mod = DateTime.Now;
                BeUmbas.Activo = entity.Activo;
                BeUmbas.Es_um_cobro = false;
                BeUmbas.Factor = 1;
                Actualizar(BeUmbas, connection, tx);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeUnidad_medida? Existe_By_Codigo_And_IdPropietario(string pCodUnidadMedida,
                                                                         int pIdPropietario,
                                                                         SqlConnection Cnn,
                                                                         SqlTransaction pTransaction)
    {
        clsBeUnidad_medida? result = null;

        try
        {
            string vSQL = "SELECT * FROM unidad_medida WHERE Codigo = @Codigo AND IdPropietario = @IdPropietario";

            using (var lDTA = new SqlDataAdapter(vSQL, Cnn))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodUnidadMedida);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario);
                lDTA.SelectCommand.Transaction = pTransaction;

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    var objUM = new clsBeUnidad_medida();
                    Cargar(ref objUM, lRow);
                    result = objUM;
                }
            }
        }
        catch
        {
            throw;
        }

        return result;
    }

    public static clsBeUnidad_medida? GetSingle(int pIdUnidadMedida,
                                               SqlConnection lConnection,
                                               SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT TOP 1 * FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    clsBeUnidad_medida ObjUM = new clsBeUnidad_medida();
                    DataRow lRow = lDT.Rows[0];
                    Cargar(ref ObjUM, lRow);
                    return ObjUM;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Obtener(clsBeUnidad_medida oBeUnidad_medida,
                               SqlConnection lConnection,
                               SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Unidad_medida 
                       WHERE IdUnidadMedida = @IdUnidadMedida";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida);

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeUnidad_medida, dt.Rows[0]);
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

    public static List<clsBeUnidad_medida> GetByIds(IConfiguration configuration, List<int> umIds)
    {
        if (umIds == null || umIds.Count == 0)
            return new List<clsBeUnidad_medida>();

        // Limpieza: sin repetidos y sin ceros/negativos
        umIds = umIds.Where(id => id > 0).Distinct().ToList();
        if (umIds.Count == 0)
            return new List<clsBeUnidad_medida>();

        var result = new List<clsBeUnidad_medida>();

        using var lConnection = new SqlConnection(configuration.GetConnectionString("CST") ?? configuration["CST"]);
        SqlTransaction? lTransaction = null;

        try
        {
            lConnection.Open();
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            // IN (@p0,@p1,@p2,...)
            var paramNames = umIds.Select((_, i) => $"@p{i}").ToList();

            string sql = $@"
SELECT *
FROM Unidad_medida
WHERE IdUnidadMedida IN ({string.Join(",", paramNames)})";

            using var cmd = new SqlCommand(sql, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            for (int i = 0; i < umIds.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter(paramNames[i], SqlDbType.Int) { Value = umIds[i] });
            }

            using var dad = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            dad.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var be = new clsBeUnidad_medida();
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

}