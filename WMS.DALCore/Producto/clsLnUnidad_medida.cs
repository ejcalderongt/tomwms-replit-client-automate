using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
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

    public static int Insertar(IConfiguration config, clsBeUnidad_medida oBeUnidad_medida, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
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

            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            Bind(cmd, oBeUnidad_medida);

             rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();

            if (!Es_Transaccion_Remota)
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
            if (lConnection is not null) lConnection.Dispose();
            if (lTransaction is not null) lTransaction.Dispose();
        }
        return rowsAffected;
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

    public static int Actualizar(IConfiguration config, clsBeUnidad_medida oBeUnidad_medida, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

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

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            Bind(cmd, oBeUnidad_medida);

            rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
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

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdUnidadMedida),0) FROM Unidad_medida";

            bool Es_Transaccion_Remota = pConection is not null && pTransaction is not null;
            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };
            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
            }

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

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
    public static int InsertOrUpdate(IConfiguration config, List<clsBeUnidad_medida> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        int total = 0;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdUnidadMedida, connection, isExternalTx ? tx! : localTx!);

                int result = existe
                    ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                    : Insertar(config, entity, connection, isExternalTx ? tx : localTx);

                total += result;
            }

            if (!isExternalTx && localTx is not null)
                localTx.Commit();

            return total;
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

    public static void Valida_Atributos(IConfiguration config, clsBeUnidad_medidaMi3 entity, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            var BeUmbas = new clsBeUnidad_medida();
            bool existe = Existe_By_Codigo(entity.Codigo, ref BeUmbas, connection, isExternalTx ? tx! : localTx!);

            if (!existe)
            {

                if (!string.IsNullOrEmpty(entity.Codigo))
                {
                    BeUmbas.IdPropietario = MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
                    BeUmbas.Codigo = entity.Codigo;
                    BeUmbas.Nombre = entity.Nombre ?? entity.Codigo;
                    
                    BeUmbas.User_agr = "1";
                    BeUmbas.User_mod = "1";
                    BeUmbas.Fec_agr = DateTime.Now;
                    BeUmbas.Fec_mod = DateTime.Now;
                    BeUmbas.Activo = entity.Activo;
                    BeUmbas.IdPropietario = entity.IdPropietario;
                    BeUmbas.Es_um_cobro = false; //Propiedad no solicitada en el json
                    BeUmbas.Factor = 1; //Propiedad no solicitada en el json
                    Insertar(config, BeUmbas, connection, isExternalTx ? tx : localTx);
                }

            }
            else
            {
                BeUmbas.Codigo = entity.Codigo;
                BeUmbas.Nombre = entity.Nombre ?? entity.Codigo;
                BeUmbas.User_mod = "1";
                BeUmbas.Fec_mod = DateTime.Now;
                BeUmbas.Activo = entity.Activo;
                BeUmbas.Es_um_cobro = false;//Propiedad no solicitada en el json
                BeUmbas.Factor = 1;//Propiedad no solicitada en el json
                Actualizar(config, BeUmbas, connection, isExternalTx ? tx : localTx);

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