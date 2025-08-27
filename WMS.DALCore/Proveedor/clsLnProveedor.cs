using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Proveedor;
using Microsoft.Extensions.Configuration;
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

    public static int Insertar(IConfiguration config, clsBeProveedor oBeProveedor, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
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

            Bind(cmd, oBeProveedor);

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

    public static int Actualizar(IConfiguration config, clsBeProveedor oBeProveedor, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

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

            Bind(cmd, oBeProveedor);

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
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;


        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdProveedor),0) FROM Proveedor";

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

            Object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((String)lreturnValue);
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

    public static void InsertarOActualizar(IConfiguration config, List<clsBeProveedor> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            foreach (var entity in entities)
            {
                if (entity.IdProveedor != 0)
                {
                    bool existe = Existe(entity.IdProveedor, connection, isExternalTx ? tx! : localTx!);

                    if (existe)
                        Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
                    else
                        Insertar(config, entity, connection, isExternalTx ? tx : localTx);
                }
            }

            if (!isExternalTx)
                localTx?.Commit();
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

}
