using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Producto.ProductoSimple;
public class clsLnCliente
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeCliente oBeCliente, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        
        try
        {
            oBeCliente.IdCliente = GetInt("IdCliente");
            oBeCliente.IdEmpresa = GetInt("IdEmpresa");
            oBeCliente.IdPropietario = GetInt("IdPropietario");
            oBeCliente.IdTipoCliente = GetInt("IdTipoCliente");
            oBeCliente.IdUbicacionManufactura = GetInt("IdUbicacionManufactura");
            oBeCliente.Codigo = GetString("codigo");
            oBeCliente.Nombre_comercial = GetString("nombre_comercial");
            oBeCliente.Nombre_contacto = GetString("nombre_contacto");
            oBeCliente.Telefono = GetString("telefono");
            oBeCliente.Nit = GetString("nit");
            oBeCliente.Direccion = GetString("direccion");
            oBeCliente.Correo_electronico = GetString("correo_electronico");
            oBeCliente.Activo = GetBool("activo");
            oBeCliente.Realiza_manufactura = GetBool("realiza_manufactura");
            oBeCliente.User_agr = GetString("user_agr");
            oBeCliente.Fec_agr = GetDate("fec_agr");
            oBeCliente.User_mod = GetString("user_mod");
            oBeCliente.Fec_mod = GetDate("fec_mod");
            oBeCliente.Despachar_lotes_completos = GetBool("despachar_lotes_completos");
            oBeCliente.Sistema = GetBool("sistema");
            oBeCliente.Es_bodega_recepcion = GetBool("es_bodega_recepcion");
            oBeCliente.Es_bodega_traslado = GetBool("es_bodega_traslado");
            oBeCliente.Idubicacionvirtual = GetInt("idubicacionvirtual");
            oBeCliente.Referencia = GetString("referencia");
            oBeCliente.Control_ultimo_lote = GetBool("control_ultimo_lote");
            oBeCliente.Control_calidad = GetBool("control_calidad");
            oBeCliente.IdUbicacionAbastecerCon = GetInt("IdUbicacionAbastecerCon");
            oBeCliente.IdBodegaAreaSAP = GetInt("IdBodegaAreaSAP");
            oBeCliente.Es_proveedor = GetBool("es_proveedor");
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

    public static int Insertar(IConfiguration config, clsBeCliente oBeCliente, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("cliente");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idtipocliente", "@idtipocliente", "F");
            Ins.Add("idubicacionmanufactura", "@idubicacionmanufactura", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("nombre_contacto", "@nombre_contacto", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("correo_electronico", "@correo_electronico", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("realiza_manufactura", "@realiza_manufactura", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("despachar_lotes_completos", "@despachar_lotes_completos", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Ins.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Ins.Add("control_calidad", "@control_calidad", "F");
            Ins.Add("idubicacionabastecercon", "@idubicacionabastecercon", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("es_proveedor", "@es_proveedor", "F");

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

            Bind(cmd, oBeCliente);

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

    public static int Insertar(IConfiguration config, clsBeCliente oBeCliente)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("cliente");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idtipocliente", "@idtipocliente", "F");
            Ins.Add("idubicacionmanufactura", "@idubicacionmanufactura", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("nombre_contacto", "@nombre_contacto", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("nit", "@nit", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("correo_electronico", "@correo_electronico", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("realiza_manufactura", "@realiza_manufactura", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("despachar_lotes_completos", "@despachar_lotes_completos", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Ins.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Ins.Add("control_calidad", "@control_calidad", "F");
            Ins.Add("idubicacionabastecercon", "@idubicacionabastecercon", "F");
            Ins.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Ins.Add("es_proveedor", "@es_proveedor", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeCliente);

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

    public static int Actualizar(IConfiguration config, clsBeCliente oBeCliente, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("cliente");
            Upd.Add("idcliente", "@idcliente", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idtipocliente", "@idtipocliente", "F");
            Upd.Add("idubicacionmanufactura", "@idubicacionmanufactura", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("nombre_comercial", "@nombre_comercial", "F");
            Upd.Add("nombre_contacto", "@nombre_contacto", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("nit", "@nit", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("correo_electronico", "@correo_electronico", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("realiza_manufactura", "@realiza_manufactura", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("despachar_lotes_completos", "@despachar_lotes_completos", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("es_bodega_recepcion", "@es_bodega_recepcion", "F");
            Upd.Add("es_bodega_traslado", "@es_bodega_traslado", "F");
            Upd.Add("idubicacionvirtual", "@idubicacionvirtual", "F");
            Upd.Add("referencia", "@referencia", "F");
            Upd.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Upd.Add("control_calidad", "@control_calidad", "F");
            Upd.Add("idubicacionabastecercon", "@idubicacionabastecercon", "F");
            Upd.Add("idbodegaareasap", "@idbodegaareasap", "F");
            Upd.Add("es_proveedor", "@es_proveedor", "F");
            Upd.Where("IdCliente = @IdCliente");

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

            Bind(cmd, oBeCliente);

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

    public int Eliminar(IConfiguration config, clsBeCliente oBeCliente, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Cliente" +
             "  Where(IdCliente = @IdCliente)");

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

            cmd.Parameters.Add(new SqlParameter("@IdCliente", oBeCliente.IdCliente));

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
            const string sp = "Select * FROM Cliente";
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

    public static bool GetSingle(IConfiguration config, ref clsBeCliente pBeCliente)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Cliente" +
            " Where(IdCliente = @IdCliente)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdCliente", pBeCliente.IdCliente));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeCliente, r);
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

    public static List<clsBeCliente> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeCliente> lreturnList = new List<clsBeCliente>();

        try
        {
            const string sp = "Select * FROM Cliente";
            
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

                        clsBeCliente vBeCliente = new clsBeCliente();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeCliente = new clsBeCliente();
                            Cargar(ref vBeCliente, dr);
                            lreturnList.Add(vBeCliente);
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

            const string sp = "Select ISNULL(Max(IdCliente),0) FROM Cliente";

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


            const string sp = "Select ISNULL(Max(IdCliente),0) FROM Cliente";

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
    public static void Bind(SqlCommand cmd, clsBeCliente oBeCliente)
    {
        cmd.Parameters.Add(new SqlParameter("@IdCliente", oBeCliente.IdCliente != 0 ? oBeCliente.IdCliente : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeCliente.IdEmpresa != 0 ? oBeCliente.IdEmpresa : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeCliente.IdPropietario != 0 ? oBeCliente.IdPropietario : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoCliente", oBeCliente.IdTipoCliente != 0 ? oBeCliente.IdTipoCliente : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionManufactura", oBeCliente.IdUbicacionManufactura != 0 ? oBeCliente.IdUbicacionManufactura : DBNull.Value));

        cmd.Parameters.Add(new SqlParameter("@codigo", !string.IsNullOrWhiteSpace(oBeCliente.Codigo) ? oBeCliente.Codigo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre_comercial", !string.IsNullOrWhiteSpace(oBeCliente.Nombre_comercial) ? oBeCliente.Nombre_comercial : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre_contacto", !string.IsNullOrWhiteSpace(oBeCliente.Nombre_contacto) ? oBeCliente.Nombre_contacto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@telefono", !string.IsNullOrWhiteSpace(oBeCliente.Telefono) ? oBeCliente.Telefono : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nit", !string.IsNullOrWhiteSpace(oBeCliente.Nit) ? oBeCliente.Nit : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@direccion", !string.IsNullOrWhiteSpace(oBeCliente.Direccion) ? oBeCliente.Direccion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@correo_electronico", !string.IsNullOrWhiteSpace(oBeCliente.Correo_electronico) ? oBeCliente.Correo_electronico : DBNull.Value));

        cmd.Parameters.Add(new SqlParameter("@activo", oBeCliente.Activo));
        cmd.Parameters.Add(new SqlParameter("@realiza_manufactura", oBeCliente.Realiza_manufactura));

        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(oBeCliente.User_agr) ? oBeCliente.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeCliente.Fec_agr != DateTime.MinValue ? oBeCliente.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(oBeCliente.User_mod) ? oBeCliente.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeCliente.Fec_mod != DateTime.MinValue ? oBeCliente.Fec_mod : DBNull.Value));

        cmd.Parameters.Add(new SqlParameter("@despachar_lotes_completos", oBeCliente.Despachar_lotes_completos));
        cmd.Parameters.Add(new SqlParameter("@sistema", oBeCliente.Sistema));
        cmd.Parameters.Add(new SqlParameter("@es_bodega_recepcion", oBeCliente.Es_bodega_recepcion));
        cmd.Parameters.Add(new SqlParameter("@es_bodega_traslado", oBeCliente.Es_bodega_traslado));

        cmd.Parameters.Add(new SqlParameter("@idubicacionvirtual", oBeCliente.Idubicacionvirtual != 0 ? oBeCliente.Idubicacionvirtual : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@referencia", !string.IsNullOrWhiteSpace(oBeCliente.Referencia) ? oBeCliente.Referencia : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@control_ultimo_lote", oBeCliente.Control_ultimo_lote));
        cmd.Parameters.Add(new SqlParameter("@control_calidad", oBeCliente.Control_calidad));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionAbastecerCon", oBeCliente.IdUbicacionAbastecerCon != 0 ? oBeCliente.IdUbicacionAbastecerCon : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdBodegaAreaSAP", oBeCliente.IdBodegaAreaSAP != 0 ? oBeCliente.IdBodegaAreaSAP : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@es_proveedor", oBeCliente.Es_proveedor));
    }
    public static void InsertarOActualizar(IConfiguration config, List<clsBeCliente> entities, SqlConnection? conn = null, SqlTransaction? tx = null)
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
                bool existe = Existe(entity.IdCliente, connection, isExternalTx ? tx! : localTx!);

                if (existe)
                    Actualizar(config, entity, connection, isExternalTx ? tx : localTx);
                else
                    Insertar(config, entity, connection, isExternalTx ? tx : localTx);
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
    public static bool Existe(int idCliente, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = "SELECT COUNT(1) FROM cliente WHERE IdCliente = @IdCliente";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdCliente", idCliente);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }

    public static bool Existe_By_Codigo(string Codigo, ref clsBeCliente pBeCliente, SqlConnection cn, SqlTransaction? tx = null)
    {
        try
        {
            const string sql = @"SELECT TOP 1 * FROM cliente WHERE codigo = @codigo";

            using var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddWithValue("@codigo", Codigo);

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeCliente, dt.Rows[0]);
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

    public static void Valida_Atributos(IConfiguration config, clsBeClientesMi3 entity, SqlConnection? conn = null, SqlTransaction? tx = null)
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

            var Cliente = new clsBeCliente();
            bool existe = Existe_By_Codigo(entity.codigo, ref Cliente, connection, isExternalTx ? tx! : localTx!);

            if (!existe)
            {

                if (!string.IsNullOrEmpty(entity.codigo))
                {
                    Cliente.IdCliente = clsLnCliente.MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
                    Cliente.Codigo = entity.codigo;
                    Cliente.Nombre_comercial = entity.nombre_comercial ?? entity.codigo;
                    Cliente.User_agr = "1";
                    Cliente.User_mod = "1";
                    Cliente.Fec_agr = DateTime.Now;
                    Cliente.Fec_mod = DateTime.Now;
                    Cliente.Activo = entity.activo;
                    Cliente.IdPropietario = entity.IdPropietario;
                    clsLnCliente.Insertar(config, Cliente, connection, isExternalTx ? tx : localTx);
                }

            }
            else
            {

                Cliente.Codigo = entity.codigo;
                Cliente.Nombre_comercial = entity.nombre_comercial ?? entity.codigo;
                Cliente.User_mod = "1";
                Cliente.Fec_mod = DateTime.Now;
                Cliente.Activo = entity.activo;
                clsLnCliente.Actualizar(config, Cliente, connection, isExternalTx ? tx : localTx);

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