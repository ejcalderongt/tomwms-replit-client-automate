using AppGlobal;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Propietario;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
public class clsLnPropietarios
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();
    public static void Cargar(ref clsBePropietarios oBePropietarios, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }

        try
        {
            oBePropietarios.IdPropietario = GetInt("IdPropietario");
            oBePropietarios.IdEmpresa = GetInt("IdEmpresa");
            oBePropietarios.IdTipoActualizacionCosto = GetInt("IdTipoActualizacionCosto");
            oBePropietarios.Contacto = GetString("contacto");
            oBePropietarios.Nombre_comercial = GetString("nombre_comercial");
            oBePropietarios.Imagen = GetBytes("imagen");
            oBePropietarios.Telefono = GetString("telefono");
            oBePropietarios.Direccion = GetString("direccion");
            oBePropietarios.Activo = GetBool("activo");
            oBePropietarios.User_agr = GetString("user_agr");
            oBePropietarios.Fec_agr = GetDate("fec_agr");
            oBePropietarios.User_mod = GetString("user_mod");
            oBePropietarios.Fec_mod = GetDate("fec_mod");
            oBePropietarios.Email = GetString("email");
            oBePropietarios.Actualiza_costo_oc = GetBool("actualiza_costo_oc");
            oBePropietarios.Color = GetInt("color");
            oBePropietarios.Codigo = GetString("codigo");
            oBePropietarios.Sistema = GetBool("sistema");
            oBePropietarios.NIT = GetString("NIT");
            oBePropietarios.Codigo_acceso = GetString("codigo_acceso");
            oBePropietarios.Clave_acceso = GetString("clave_acceso");
            oBePropietarios.IdBodegaAreaSAP = GetInt("IdBodegaAreaSAP");
            oBePropietarios.Es_consolidador = GetBool("es_consolidador");
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
    public static int Insertar(IConfiguration config, clsBePropietarios oBePropietarios, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("propietarios");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", "F");
            Ins.Add("contacto", "@contacto", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Ins.Add("color", "@color", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("codigo_acceso", "@codigo_acceso", "F");
            Ins.Add("clave_acceso", "@clave_acceso", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("es_consolidador", "@es_consolidador", "F");

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

            Bind(cmd, oBePropietarios);

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
    public static int Insertar(IConfiguration config, clsBePropietarios oBePropietarios)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("propietarios");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", "F");
            Ins.Add("contacto", "@contacto", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Ins.Add("color", "@color", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("codigo_acceso", "@codigo_acceso", "F");
            Ins.Add("clave_acceso", "@clave_acceso", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("es_consolidador", "@es_consolidador", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBePropietarios);

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
    public static int Actualizar(IConfiguration config, clsBePropietarios oBePropietarios, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        int rowsAffected = 0;
        bool isLocal = pConection == null || pTransaction == null;
        SqlConnection lConnection = isLocal ? new SqlConnection(config.GetConnectionString("CST")) : pConection!;
        SqlTransaction? lTransaction = pTransaction;

        try
        {
            if (isLocal)
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            Upd.Init("propietarios");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", "F");
            Upd.Add("contacto", "@contacto", "F");
            Upd.Add("nombre_comercial", "@nombre_comercial", "F");
            Upd.Add("imagen", "@imagen", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("email", "@email", "F");
            Upd.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Upd.Add("color", "@color", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("nit", "@nit", "F");
            //Upd.Add("codigo_acceso", "@codigo_acceso", "F");
            //Upd.Add("clave_acceso", "@clave_acceso", "F");
            Upd.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Upd.Add("es_consolidador", "@es_consolidador", "F");
            Upd.Where("IdPropietario = @IdPropietario");

            string sp = Upd.SQL();
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            Bind(cmd, oBePropietarios);
            rowsAffected = cmd.ExecuteNonQuery();

            if (isLocal)
                lTransaction?.Commit();
        }
        catch (SqlException ex1)
        {
            if (isLocal)
                lTransaction?.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (isLocal && lConnection.State == ConnectionState.Open)
            {
                lConnection.Close();
                lConnection.Dispose();
                lTransaction?.Dispose();
            }
        }

        return rowsAffected;
    }
    public int Eliminar(IConfiguration config, clsBePropietarios oBePropietarios, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Propietarios" +
             "  Where(IdPropietario = @IdPropietario)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBePropietarios.IdPropietario));

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
            const string sp = "Select * FROM Propietarios";
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
    public static bool GetSingle(IConfiguration config, ref clsBePropietarios pBePropietarios)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Propietarios" +
            " Where(IdPropietario = @IdPropietario)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", pBePropietarios.IdPropietario));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBePropietarios, r);
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
    public static List<clsBePropietarios> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBePropietarios> lreturnList = new List<clsBePropietarios>();

        try
        {
            const string sp = "Select * FROM Propietarios";

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

                        clsBePropietarios vBePropietarios = new clsBePropietarios();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBePropietarios = new clsBePropietarios();
                            Cargar(ref vBePropietarios, dr);
                            lreturnList.Add(vBePropietarios);
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

            const string sp = "Select ISNULL(Max(IdPropietario),0) FROM Propietarios";

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
                            lMax = int.Parse((string)lreturnValue);
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


            const string sp = "Select ISNULL(Max(IdPropietario),0) FROM Propietarios";

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
    public static void Bind(SqlCommand cmd, clsBePropietarios o)
    {
        cmd.Parameters.AddWithValue("@IdPropietario", o.IdPropietario);
        cmd.Parameters.AddWithValue("@IdEmpresa", o.IdEmpresa);
        cmd.Parameters.AddWithValue("@IdTipoActualizacionCosto", o.IdTipoActualizacionCosto);
        cmd.Parameters.AddWithValue("@contacto", string.IsNullOrEmpty(o.Contacto) ? DBNull.Value : o.Contacto);
        cmd.Parameters.AddWithValue("@nombre_comercial", string.IsNullOrEmpty(o.Nombre_comercial) ? DBNull.Value : o.Nombre_comercial);
        cmd.Parameters.AddWithValue("@imagen", o.Imagen ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(o.Telefono) ? DBNull.Value : o.Telefono);
        cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(o.Direccion) ? DBNull.Value : o.Direccion);
        cmd.Parameters.AddWithValue("@activo", o.Activo);
        cmd.Parameters.AddWithValue("@user_agr", string.IsNullOrEmpty(o.User_agr) ? DBNull.Value : o.User_agr);
        cmd.Parameters.AddWithValue("@fec_agr", o.Fec_agr == default ? DBNull.Value : o.Fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", string.IsNullOrEmpty(o.User_mod) ? DBNull.Value : o.User_mod);
        cmd.Parameters.AddWithValue("@fec_mod", o.Fec_mod == default ? DBNull.Value : o.Fec_mod);
        cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(o.Email) ? DBNull.Value : o.Email);
        cmd.Parameters.AddWithValue("@actualiza_costo_oc", o.Actualiza_costo_oc);
        cmd.Parameters.AddWithValue("@color", o.Color);
        cmd.Parameters.AddWithValue("@codigo", string.IsNullOrEmpty(o.Codigo) ? DBNull.Value : o.Codigo); 
        cmd.Parameters.AddWithValue("@sistema", o.Sistema);
        cmd.Parameters.AddWithValue("@NIT", string.IsNullOrEmpty(o.NIT) ? DBNull.Value : o.NIT);
        cmd.Parameters.AddWithValue("@codigo_acceso", string.IsNullOrEmpty(o.Codigo_acceso) ? DBNull.Value : o.Codigo_acceso);
        //cmd.Parameters.AddWithValue("@clave_acceso", string.IsNullOrEmpty(o.Clave_acceso) ? DBNull.Value : o.Clave_acceso);
        cmd.Parameters.AddWithValue("@IdBodegaAreaSAP", o.IdBodegaAreaSAP);
        cmd.Parameters.AddWithValue("@es_consolidador", o.Es_consolidador);
    }
    public static bool Existe(IConfiguration config, int idPropietario, SqlConnection? connection = null, SqlTransaction? transaction = null)
    {
        bool exists = false;
        bool isLocal = connection == null;

        SqlConnection conn = connection ?? new SqlConnection(config.GetConnectionString("CST"));
        SqlCommand cmd = new SqlCommand("SELECT COUNT(IdPropietario) FROM propietarios WHERE IdPropietario = @IdPropietario", conn);
        cmd.Parameters.AddWithValue("@IdPropietario", idPropietario);

        if (transaction != null)
            cmd.Transaction = transaction;

        try
        {
            if (isLocal && conn.State != ConnectionState.Open)
                conn.Open();

            exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        finally
        {
            if (isLocal && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        return exists;
    }
    public static int InsertOrUpdate(IConfiguration config, clsBePropietarios be, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool externa = conn != null && tx != null;
        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            if (Existe(config, be.IdPropietario, lConn, externa ? tx! : lTx!))
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
    public static bool EsPropietarioValido(IConfiguration config, string codigoAcceso, string claveAcceso, ref clsBePropietarios pBePropietarios)
    {
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "SELECT * FROM Propietarios " +
                              "WHERE codigo_acceso = @codigo_acceso AND clave_acceso = @clave_acceso AND activo = 1";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            cmd.Parameters.Add(new SqlParameter("@codigo_acceso", codigoAcceso));
            cmd.Parameters.Add(new SqlParameter("@clave_acceso", clsPublic.Encriptar(claveAcceso)));

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {                
                DataRow r = dt.Rows[0];
                Cargar(ref pBePropietarios, r);
                return true;
            }
        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
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

    public static bool EmailValido(IConfiguration config, string pEmail, ref clsBePropietarios pBePropietarios, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "SELECT * FROM Propietarios WHERE (email=@email AND controlux =1 and activo=1) ";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            cmd.Parameters.Add(new SqlParameter("@email", pEmail));

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();


            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBePropietarios, r);
                return true;
            }

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
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

    public static bool Actualizar_Password_Ux(IConfiguration config, clsBePropietarios oBePropietarios, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        int rowsAffected = 0;
        bool isLocal = pConection == null || pTransaction == null;
        SqlConnection lConnection = isLocal ? new SqlConnection(config.GetConnectionString("CST")) : pConection!;
        SqlTransaction? lTransaction = pTransaction;

        try
        {
            if (isLocal)
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            Upd.Init("propietarios");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("clave_acceso", "@clave_acceso", "F");
       
            Upd.Where("IdPropietario = @IdPropietario");

            string sp = Upd.SQL();
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@IdPropietario", oBePropietarios.IdPropietario);
            cmd.Parameters.AddWithValue("@clave_acceso", oBePropietarios.Clave_acceso);
            cmd.Parameters.AddWithValue("@fec_mod", oBePropietarios.Fec_mod);

            //Bind(cmd, oBePropietarios);
            rowsAffected = cmd.ExecuteNonQuery();

            if (isLocal)
                lTransaction?.Commit();

          
        }
        catch (SqlException ex1)
        {
            if (isLocal)
                lTransaction?.Rollback();

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }
        finally
        {
            if (isLocal && lConnection.State == ConnectionState.Open)
            {
                lConnection.Close();
                lConnection.Dispose();
                lTransaction?.Dispose();
            }
        }

        return rowsAffected > 0;

    }
}