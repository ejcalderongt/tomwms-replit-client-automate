using AppGlobal;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Proveedor;
public class clsLnProveedor
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProveedor oBeProveedor, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeProveedor.IdEmpresa = GetInt("IdEmpresa");
            oBeProveedor.IdPropietario = GetInt("IdPropietario");
            oBeProveedor.IdProveedor = GetInt("IdProveedor");
            oBeProveedor.Codigo = GetString("codigo");
            oBeProveedor.Nombre = GetString("nombre");
            oBeProveedor.Telefono = GetString("telefono");
            oBeProveedor.Nit = GetString("nit");
            oBeProveedor.Direccion = GetString("direccion");
            oBeProveedor.Email = GetString("email");
            oBeProveedor.Contacto = GetString("contacto");
            oBeProveedor.Activo = GetBool("activo");
            oBeProveedor.Muestra_precio = GetBool("muestra_precio");
            oBeProveedor.User_agr = GetString("user_agr");
            oBeProveedor.Fec_agr = GetDate("fec_agr");
            oBeProveedor.User_mod = GetString("user_mod");
            oBeProveedor.Fec_mod = GetDate("fec_mod");
            oBeProveedor.Actualiza_costo_oc = GetBool("actualiza_costo_oc");
            oBeProveedor.Idubicacionvirtual = GetInt("idubicacionvirtual");
            oBeProveedor.Es_bodega_recepcion = GetBool("es_bodega_recepcion");
            oBeProveedor.Es_bodega_traslado = GetBool("es_bodega_traslado");
            oBeProveedor.Referencia = GetString("referencia");
            oBeProveedor.Sistema = GetBool("sistema");
            oBeProveedor.IdConfiguracionBarraPallet = GetInt("IdConfiguracionBarraPallet");
            oBeProveedor.Es_proveedor_servicio = GetBool("es_proveedor_servicio");
            oBeProveedor.IdBodegaAreaSAP = GetInt("IdBodegaAreaSAP");
            oBeProveedor.IdPais = GetInt("IdPais");
            oBeProveedor.Codigo_Empresa_ERP = GetString("Codigo_Empresa_ERP");
        }
        catch (Exception ex)
        {
            var st = new System.Diagnostics.StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }
    }
    public static int Insertar(clsBeProveedor oBeProveedor, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeProveedor == null)
            throw new ArgumentNullException(nameof(oBeProveedor));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("proveedor");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idproveedor", "@idproveedor", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("contacto", "@contacto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("muestra_precio", "@muestra_precio", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Ins.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("idconfiguracionbarrapallet", "@idconfiguracionbarrapallet", "F");
            Ins.Add("es_proveedor_servicio", "@es_proveedor_servicio", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("idpais", "@idpais", "F");
            Ins.Add("codigo_empresa_erp", "@codigo_empresa_erp", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeProveedor);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Insertar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
    public static int Insertar(IConfiguration config, clsBeProveedor oBeProveedor)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("proveedor");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idproveedor", "@idproveedor", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("contacto", "@contacto", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("muestra_precio", "@muestra_precio", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Ins.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("idconfiguracionbarrapallet", "@idconfiguracionbarrapallet", "F");
            Ins.Add("es_proveedor_servicio", "@es_proveedor_servicio", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("idpais", "@idpais", "F");
            Ins.Add("codigo_empresa_erp", "@codigo_empresa_erp", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeProveedor);

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
    public static void Bind(SqlCommand cmd, clsBeProveedor oBeProveedor)
    {
        cmd.Parameters.Add(new SqlParameter("@idempresa", oBeProveedor.IdEmpresa == 0 ? DBNull.Value : oBeProveedor.IdEmpresa));
        cmd.Parameters.Add(new SqlParameter("@idpropietario", oBeProveedor.IdPropietario == 0 ? DBNull.Value : oBeProveedor.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@idproveedor", oBeProveedor.IdProveedor == 0 ? DBNull.Value : oBeProveedor.IdProveedor));
        cmd.Parameters.Add(new SqlParameter("@codigo", oBeProveedor.Codigo));
        cmd.Parameters.Add(new SqlParameter("@nombre", oBeProveedor.Nombre));
        cmd.Parameters.Add(new SqlParameter("@telefono", oBeProveedor.Telefono));
        cmd.Parameters.Add(new SqlParameter("@nit", oBeProveedor.Nit));
        cmd.Parameters.Add(new SqlParameter("@direccion", oBeProveedor.Direccion));
        cmd.Parameters.Add(new SqlParameter("@email", oBeProveedor.Email));
        cmd.Parameters.Add(new SqlParameter("@contacto", oBeProveedor.Contacto));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeProveedor.Activo));
        cmd.Parameters.Add(new SqlParameter("@muestra_precio", oBeProveedor.Muestra_precio));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeProveedor.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeProveedor.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeProveedor.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeProveedor.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@actualiza_costo_oc", oBeProveedor.Actualiza_costo_oc));
        cmd.Parameters.Add(new SqlParameter("@idubicacionvirtual", oBeProveedor.Idubicacionvirtual));
        cmd.Parameters.Add(new SqlParameter("@es_bodega_recepcion", oBeProveedor.Es_bodega_recepcion));
        cmd.Parameters.Add(new SqlParameter("@es_bodega_traslado", oBeProveedor.Es_bodega_traslado));
        cmd.Parameters.Add(new SqlParameter("@referencia", oBeProveedor.Referencia));
        cmd.Parameters.Add(new SqlParameter("@sistema", oBeProveedor.Sistema));
        cmd.Parameters.Add(new SqlParameter("@idconfiguracionbarrapallet", oBeProveedor.IdConfiguracionBarraPallet));
        cmd.Parameters.Add(new SqlParameter("@es_proveedor_servicio", oBeProveedor.Es_proveedor_servicio));
        cmd.Parameters.Add(new SqlParameter("@idbodegaareasap", oBeProveedor.IdBodegaAreaSAP));
        cmd.Parameters.Add(new SqlParameter("@idpais", oBeProveedor.IdPais));
        cmd.Parameters.Add(new SqlParameter("@codigo_empresa_erp", oBeProveedor.Codigo_Empresa_ERP));
    }
    public static int Actualizar(clsBeProveedor oBeProveedor, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeProveedor == null)
            throw new ArgumentNullException(nameof(oBeProveedor));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Upd.Init("proveedor");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idproveedor", "@idproveedor", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("nit", "@nit", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("email", "@email", "F");
            Upd.Add("contacto", "@contacto", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("muestra_precio", "@muestra_precio", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("actualiza_costo_oc", "@actualiza_costo_oc", "F");
            Upd.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Upd.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Upd.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Upd.Add("referencia", "@referencia", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("idconfiguracionbarrapallet", "@idconfiguracionbarrapallet", "F");
            Upd.Add("es_proveedor_servicio", "@es_proveedor_servicio", "F");
            Upd.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Upd.Add("idpais", "@idpais", "F");
            Upd.Add("codigo_empresa_erp", "@codigo_empresa_erp", "F");
            Upd.Where("IdProveedor = @IdProveedor");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeProveedor);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Actualizar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
    public int Eliminar(IConfiguration config, clsBeProveedor oBeProveedor, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Proveedor" +
             "  Where(IdProveedor = @IdProveedor)");

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

            cmd.Parameters.Add(new SqlParameter("@IdProveedor", oBeProveedor.IdProveedor));

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
            const string sp = "Select * FROM Proveedor";
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
    public static bool GetSingle(IConfiguration config, ref clsBeProveedor pBeProveedor)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Proveedor" +
            " Where(IdProveedor = @IdProveedor)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProveedor, r);
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
    public static List<clsBeProveedor> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProveedor> lreturnList = new List<clsBeProveedor>();

        try
        {
            const string sp = "Select * FROM Proveedor";

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

                        clsBeProveedor vBeProveedor = new clsBeProveedor();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProveedor = new clsBeProveedor();
                            Cargar(ref vBeProveedor, dr);
                            lreturnList.Add(vBeProveedor);
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

            const string sp = "Select ISNULL(Max(IdProveedor),0) FROM Proveedor";

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
        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        try
        {
            const string sp = "Select ISNULL(Max(IdProveedor),0) FROM Proveedor";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                object lreturnValue = cmd.ExecuteScalar();

                if (lreturnValue != DBNull.Value && lreturnValue != null)
                {
                    return Convert.ToInt32(lreturnValue);
                }

                return 0;
            }
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en MaxID - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
    public static void InsertarOActualizar(List<clsBeProveedor> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                if (entity.IdProveedor != 0)
                {
                    bool existe = Existe(entity.IdProveedor, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static bool Existe(int IdProveedor, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM proveedor WHERE IdProveedor = @IdProveedor";

            using (SqlCommand cmd = new SqlCommand(query, conn, tx))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdProveedor", IdProveedor));

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
    public static bool Existe_By_Codigo(string Codigo, ref clsBeProveedor pBeProveedor, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = @"SELECT TOP 1 * FROM proveedor WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeProveedor, dt.Rows[0]);
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
    public static void Valida_Atributos(clsBeProveedor pBeProveedor, SqlConnection conn, SqlTransaction tx)
    {
        if (pBeProveedor == null)
            throw new ArgumentNullException(nameof(pBeProveedor));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        var BeProveedor = new clsBeProveedor();
        var BeProveedor_Bodega = new clsBeProveedor_bodega();
        bool existe = Existe_By_Codigo(pBeProveedor.Codigo, ref BeProveedor, conn, tx);

        var BeInavConfigEnc = new clsBeI_nav_config_enc();
        clsLnI_nav_config_enc.GetSingle(BeInavConfigEnc, conn, tx);

        if (BeInavConfigEnc == null)
            throw new ArgumentNullException(nameof(BeInavConfigEnc), "No se encuentra interface para definir propiedades de auditoria.");

        if (!existe)
        {
            if (!string.IsNullOrEmpty(pBeProveedor.Codigo))
            {
                BeProveedor.IdProveedor = MaxID(conn, tx) + 1;
                BeProveedor.IdEmpresa = BeInavConfigEnc.Idempresa;
                BeProveedor.IdPropietario = BeInavConfigEnc.IdPropietario;
                BeProveedor.Codigo = pBeProveedor.Codigo;
                BeProveedor.Nombre = pBeProveedor.Nombre ?? pBeProveedor.Codigo;
                BeProveedor.Nit = pBeProveedor.Nit;
                BeProveedor.Contacto = pBeProveedor.Contacto;
                BeProveedor.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                BeProveedor.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                BeProveedor.Fec_agr = DateTime.Now;
                BeProveedor.Fec_mod = DateTime.Now;
                BeProveedor.Activo = pBeProveedor.Activo;               
                Insertar(BeProveedor, conn, tx);

                var listBeBodega = clsLnBodega.GetAll(conn, tx);

                if (listBeBodega.Count == 0)
                    throw new ArgumentNullException(nameof(listBeBodega), "No se encontraron bodegas activas para asociar proveedores");

                foreach (clsBeBodega BeBodega in listBeBodega)
                {
                    if (BeBodega == null)
                        continue;

                    BeProveedor_Bodega = new clsBeProveedor_bodega();
                    BeProveedor_Bodega.IdAsignacion = clsLnProveedor_bodega.MaxID(conn, tx) + 1; 
                    BeProveedor_Bodega.IdProveedor = BeProveedor.IdProveedor;
                    BeProveedor_Bodega.IdBodega = BeBodega.IdBodega;
                    BeProveedor_Bodega.IdAreaOrigen = 0;
                    BeProveedor_Bodega.User_agr = BeInavConfigEnc.IdUsuario.ToString();
                    BeProveedor_Bodega.User_mod = BeInavConfigEnc.IdUsuario.ToString();
                    BeProveedor_Bodega.Fec_agr = DateTime.Now;
                    BeProveedor_Bodega.Fec_mod = DateTime.Now;
                    BeProveedor_Bodega.Activo = true;
                    clsLnProveedor_bodega.Insertar(BeProveedor_Bodega, conn, tx);
                }
            }
        }
        else
        {
            BeProveedor.Codigo = pBeProveedor.Codigo;
            BeProveedor.Nombre = pBeProveedor.Nombre ?? pBeProveedor.Codigo;
            BeProveedor.User_mod = BeInavConfigEnc.IdUsuario.ToString();
            BeProveedor.Fec_mod = DateTime.Now;
            BeProveedor.Activo = pBeProveedor.Activo;
            Actualizar(BeProveedor, conn, tx);
        }
    }
    public static clsBeProveedor_bodega Get_ProveedorBodega_By_Codigo_Proveedor(string pCodigo,
                                                                                int pIdBodega,
                                                                                SqlConnection lConection,
                                                                                SqlTransaction lTransaction)
    {
        clsBeProveedor_bodega result = new clsBeProveedor_bodega();

        try
        {
            string vSQL = "SELECT TOP 1 * FROM proveedor WHERE Codigo = @Codigo";

            using (var lDTA = new SqlDataAdapter(vSQL, lConection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    var beProveedor = new clsBeProveedor();

                    Cargar(ref beProveedor, lRow);

                    var beProveedorBodega = new clsBeProveedor_bodega
                    {
                        IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1,
                        IdProveedor = beProveedor.IdProveedor,
                        IdBodega = pIdBodega,
                        Proveedor = beProveedor
                    };

                    bool existeAsociacion = clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(ref beProveedorBodega, 
                                                                                                         lConection, 
                                                                                                         lTransaction);

                    if (!existeAsociacion)
                    {
                        string vMensaje = string.Format(
                            "El proveedor: {0} existe con identificador: {1}, pero no fue posible obtener la asociación del proveedor con el IdBodega: {2} ",
                            pCodigo, beProveedorBodega.IdProveedor, pIdBodega);

                        throw new Exception(vMensaje);
                    }

                    result = beProveedorBodega;
                }
            }
        }
        catch
        {
            throw;
        }

        return result;
    }

    public static clsBeProveedor_bodega? Get_ProveedorBodega_By_Codigo_Proveedor(string pCodigo,
                                                                                int pIdBodega,
                                                                                ref SqlConnection lConection,
                                                                                ref SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT TOP 1 * FROM proveedor WHERE Codigo = @Codigo";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeProveedor BeProveedor = new clsBeProveedor();

                    Cargar(ref BeProveedor, lRow);

                    clsBeProveedor_bodega BeProveedorBodega = new clsBeProveedor_bodega();
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1;
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor;
                    BeProveedorBodega.IdBodega = pIdBodega;
                    BeProveedorBodega.Proveedor = BeProveedor;

                    if (!clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(ref BeProveedorBodega, lConection, lTransaction))
                    {
                        string vMensaje = string.Format("El proveedor: {0} existe con identificador: {1}, pero no fue posible obtener la asociación del proveedor con el IdBodega: {2}",
                                                       pCodigo, BeProveedorBodega.IdProveedor, pIdBodega);
                        throw new Exception(vMensaje);
                    }
                    else
                    {
                        return BeProveedorBodega;
                    }
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeProveedor_bodega? Get_ProveedorBodega_By_Codigo_Proveedor(IConfiguration config,
                                                                                 string pCodigo,
                                                                                 int pIdBodega,
                                                                                 clsBeI_nav_config_enc BeConfigEnc,
                                                                                 SqlConnection lConection,
                                                                                 SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT TOP 1 * FROM proveedor WHERE Codigo = @Codigo";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeProveedor BeProveedor = new clsBeProveedor();

                    Cargar(ref BeProveedor, lRow);

                    clsBeProveedor_bodega BeProveedorBodega = new clsBeProveedor_bodega();
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1;
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor;
                    BeProveedorBodega.IdBodega = pIdBodega;
                    BeProveedorBodega.Proveedor = BeProveedor;

                    clsBeProveedor_bodega BeProveedorBodegaNuevo = new clsBeProveedor_bodega();
                    clsPublic.CopyObject(BeProveedorBodega, ref BeProveedorBodegaNuevo);

                    if (!clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(ref BeProveedorBodega, lConection, lTransaction))
                    {
                        BeProveedorBodega = BeProveedorBodegaNuevo;
                        BeProveedorBodega.User_agr = BeConfigEnc.User_agr;
                        BeProveedorBodega.User_mod = BeConfigEnc.User_mod;
                        BeProveedorBodega.Fec_agr = DateTime.Now;
                        BeProveedorBodega.Fec_mod = DateTime.Now;
                        BeProveedorBodega.Activo = true;
                        clsLnProveedor_bodega.Insertar(BeProveedorBodega,lConection,lTransaction);
                    }

                    return BeProveedorBodega;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Existe_Proveedor(string pCodigo, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                return (lDT != null && lDT.Rows.Count > 0);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeProveedor_bodega? Get_ProveedorBodega_By_Codigo_Proveedor(string pCodigo,
                                                                                int pIdBodega,
                                                                                clsBeI_nav_config_enc BeConfigEnc,
                                                                                SqlConnection lConection,
                                                                                SqlTransaction lTransaction)
    {
        clsBeProveedor_bodega? result = null;

        try
        {
            string vSQL = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo ";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeProveedor BeProveedor = new clsBeProveedor();

                    Cargar(ref BeProveedor, lRow);

                    clsBeProveedor_bodega BeProveedorBodega = new clsBeProveedor_bodega();                    
                    BeProveedorBodega.IdBodega = pIdBodega;
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor;

                    if (!clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdAsignacion(ref BeProveedorBodega, lConection, lTransaction))
                    {

                        BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1;
                        BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor;
                        BeProveedorBodega.Proveedor = BeProveedor;
                        BeProveedorBodega.User_agr = BeConfigEnc.User_agr;
                        BeProveedorBodega.User_mod = BeConfigEnc.User_mod;
                        BeProveedorBodega.Fec_agr = DateTime.Now;
                        BeProveedorBodega.Fec_mod = DateTime.Now;
                        BeProveedorBodega.Activo = true;
                        clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConection, lTransaction);

                    }
                    
                     result = BeProveedorBodega;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
}