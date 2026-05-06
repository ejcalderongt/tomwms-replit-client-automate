using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

public class clsLnEmpresa
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeEmpresa oBeEmpresa, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeEmpresa.IdEmpresa = GetInt("IdEmpresa");
            oBeEmpresa.Nombre = GetString("nombre");
            oBeEmpresa.Direccion = GetString("direccion");
            oBeEmpresa.Telefono = GetString("telefono");
            oBeEmpresa.Email = GetString("email");
            oBeEmpresa.Razon_social = GetString("razon_social");
            oBeEmpresa.Representante = GetString("representante");
            oBeEmpresa.Corr_cod_barra = GetInt("corr_cod_barra");
            oBeEmpresa.Path_printer = GetString("path_printer");
            oBeEmpresa.Activo = GetBool("activo");
            oBeEmpresa.User_agr = GetString("user_agr");
            oBeEmpresa.Fec_agr = GetDate("fec_agr");
            oBeEmpresa.User_mod = GetString("user_mod");
            oBeEmpresa.Fec_mod = GetDate("fec_mod");
            oBeEmpresa.ClienteRapido = GetBool("clienteRapido");
            oBeEmpresa.Imagen = GetBytes("imagen");
            oBeEmpresa.Operador_logistico = GetBool("operador_logistico");
            oBeEmpresa.Puerto_escaner = GetInt("puerto_escaner");
            oBeEmpresa.Control_presentaciones = GetBool("control_presentaciones");
            oBeEmpresa.Anulaciones_por_supervisor = GetBool("anulaciones_por_supervisor");
            oBeEmpresa.Codigo = GetString("codigo");
            oBeEmpresa.Clave = GetString("clave");
            oBeEmpresa.Intento = GetInt("intento");
            oBeEmpresa.Duracionclave = GetInt("duracionclave");
            oBeEmpresa.Duracionclavetemporal = GetInt("duracionclavetemporal");
            oBeEmpresa.Codigo_automatico = GetBool("codigo_automatico");
            oBeEmpresa.Politica_contraseñas = GetBool("politica_contraseñas");
            oBeEmpresa.IdMotivoAjusteInventario = GetInt("IdMotivoAjusteInventario");
            oBeEmpresa.Cantidad_decimales_despliegue = GetInt("cantidad_decimales_despliegue");
            oBeEmpresa.Cantidad_decimales_calculo = GetInt("cantidad_decimales_calculo");
            oBeEmpresa.Minutos_timer_jornada_sistema = GetDecimal("minutos_timer_jornada_sistema");
            oBeEmpresa.Hora_corte_jornada_sistema = GetDate("hora_corte_jornada_sistema");
            oBeEmpresa.Generar_stock_jornada = GetBool("generar_stock_jornada");
            oBeEmpresa.Buscar_actualizacion_hh = GetBool("buscar_actualizacion_hh");
            oBeEmpresa.Version_bd = GetString("version_bd");
            oBeEmpresa.Aws_token = GetString("aws_token");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Insertar(clsBeEmpresa oBeEmpresa,
                          SqlConnection pConection,
                          SqlTransaction pTransaction)
    {
        Ins.Init("empresa");
        Ins.Add("idempresa", "@idempresa", "F");
        Ins.Add("nombre", "@nombre", "F");
        Ins.Add("direccion", "@direccion", "F");
        Ins.Add("telefono", "@telefono", "F");
        Ins.Add("email", "@email", "F");
        Ins.Add("razon_social", "@razon_social", "F");
        Ins.Add("representante", "@representante", "F");
        Ins.Add("corr_cod_barra", "@corr_cod_barra", "F");
        Ins.Add("path_printer", "@path_printer", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("clienterapido", "@clienterapido", "F");
        Ins.Add("imagen", "@imagen", "F");
        Ins.Add("operador_logistico", "@operador_logistico", "F");
        Ins.Add("puerto_escaner", "@puerto_escaner", "F");
        Ins.Add("control_presentaciones", "@control_presentaciones", "F");
        Ins.Add("anulaciones_por_supervisor", "@anulaciones_por_supervisor", "F");
        Ins.Add("codigo", "@codigo", "F");
        Ins.Add("clave", "@clave", "F");
        Ins.Add("intento", "@intento", "F");
        Ins.Add("duracionclave", "@duracionclave", "F");
        Ins.Add("duracionclavetemporal", "@duracionclavetemporal", "F");
        Ins.Add("codigo_automatico", "@codigo_automatico", "F");
        Ins.Add("politica_contraseñas", "@politica_contraseñas", "F");
        Ins.Add("idmotivoajusteinventario", "@idmotivoajusteinventario", "F");
        Ins.Add("cantidad_decimales_despliegue", "@cantidad_decimales_despliegue", "F");
        Ins.Add("cantidad_decimales_calculo", "@cantidad_decimales_calculo", "F");
        Ins.Add("minutos_timer_jornada_sistema", "@minutos_timer_jornada_sistema", "F");
        Ins.Add("hora_corte_jornada_sistema", "@hora_corte_jornada_sistema", "F");
        Ins.Add("generar_stock_jornada", "@generar_stock_jornada", "F");
        Ins.Add("buscar_actualizacion_hh", "@buscar_actualizacion_hh", "F");
        Ins.Add("version_bd", "@version_bd", "F");
        Ins.Add("aws_token", "@aws_token", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdEmpresa", oBeEmpresa.IdEmpresa);
            cmd.Parameters.AddWithValue("@nombre", oBeEmpresa.Nombre ?? string.Empty);
            cmd.Parameters.AddWithValue("@direccion", oBeEmpresa.Direccion ?? string.Empty);
            cmd.Parameters.AddWithValue("@telefono", oBeEmpresa.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("@email", oBeEmpresa.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@razon_social", oBeEmpresa.Razon_social ?? string.Empty);
            cmd.Parameters.AddWithValue("@representante", oBeEmpresa.Representante ?? string.Empty);
            cmd.Parameters.AddWithValue("@corr_cod_barra", oBeEmpresa.Corr_cod_barra);
            cmd.Parameters.AddWithValue("@path_printer", oBeEmpresa.Path_printer ?? string.Empty);
            cmd.Parameters.AddWithValue("@activo", oBeEmpresa.Activo);
            cmd.Parameters.AddWithValue("@user_agr", oBeEmpresa.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_agr", oBeEmpresa.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeEmpresa.User_mod ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeEmpresa.Fec_mod);
            cmd.Parameters.AddWithValue("@clienteRapido", oBeEmpresa.ClienteRapido);
            cmd.Parameters.AddWithValue("@imagen", oBeEmpresa.Imagen ?? Array.Empty<byte>());
            cmd.Parameters.AddWithValue("@operador_logistico", oBeEmpresa.Operador_logistico);
            cmd.Parameters.AddWithValue("@puerto_escaner", oBeEmpresa.Puerto_escaner.ToString() ?? string.Empty);
            cmd.Parameters.AddWithValue("@control_presentaciones", oBeEmpresa.Control_presentaciones);
            cmd.Parameters.AddWithValue("@anulaciones_por_supervisor", oBeEmpresa.Anulaciones_por_supervisor);
            cmd.Parameters.AddWithValue("@codigo", oBeEmpresa.Codigo ?? string.Empty);
            cmd.Parameters.AddWithValue("@clave", oBeEmpresa.Clave ?? string.Empty);
            cmd.Parameters.AddWithValue("@intento", oBeEmpresa.Intento);
            cmd.Parameters.AddWithValue("@duracionclave", oBeEmpresa.Duracionclave);
            cmd.Parameters.AddWithValue("@duracionclavetemporal", oBeEmpresa.Duracionclavetemporal);
            cmd.Parameters.AddWithValue("@codigo_automatico", oBeEmpresa.Codigo_automatico);
            cmd.Parameters.AddWithValue("@politica_contraseñas", oBeEmpresa.Politica_contraseñas);
            cmd.Parameters.AddWithValue("@IdMotivoAjusteInventario", oBeEmpresa.IdMotivoAjusteInventario);
            cmd.Parameters.AddWithValue("@cantidad_decimales_despliegue", oBeEmpresa.Cantidad_decimales_despliegue);
            cmd.Parameters.AddWithValue("@cantidad_decimales_calculo", oBeEmpresa.Cantidad_decimales_calculo);
            cmd.Parameters.AddWithValue("@minutos_timer_jornada_sistema", oBeEmpresa.Minutos_timer_jornada_sistema);            
            cmd.Parameters.AddWithValue("@hora_corte_jornada_sistema", oBeEmpresa.Hora_corte_jornada_sistema == default(DateTime) ? DBNull.Value : oBeEmpresa.Hora_corte_jornada_sistema);
            cmd.Parameters.AddWithValue("@generar_stock_jornada", oBeEmpresa.Generar_stock_jornada);
            cmd.Parameters.AddWithValue("@buscar_actualizacion_hh", oBeEmpresa.Buscar_actualizacion_hh);
            cmd.Parameters.AddWithValue("@version_bd", oBeEmpresa.Version_bd ?? string.Empty);
            cmd.Parameters.AddWithValue("@aws_token", oBeEmpresa.Aws_token ?? string.Empty);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeEmpresa oBeEmpresa)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("empresa");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("razon_social", "@razon_social", "F");
            Ins.Add("representante", "@representante", "F");
            Ins.Add("corr_cod_barra", "@corr_cod_barra", "F");
            Ins.Add("path_printer", "@path_printer", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("clienterapido", "@clienterapido", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("operador_logistico", "@operador_logistico", "F");
            Ins.Add("puerto_escaner", "@puerto_escaner", "F");
            Ins.Add("control_presentaciones", "@control_presentaciones", "F");
            Ins.Add("anulaciones_por_supervisor", "@anulaciones_por_supervisor", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("clave", "@clave", "F");
            Ins.Add("intento", "@intento", "F");
            Ins.Add("duracionclave", "@duracionclave", "F");
            Ins.Add("duracionclavetemporal", "@duracionclavetemporal", "F");
            Ins.Add("codigo_automatico", "@codigo_automatico", "F");
            Ins.Add("politica_contraseñas", "@politica_contraseñas", "F");
            Ins.Add("idmotivoajusteinventario", "@idmotivoajusteinventario", "F");
            Ins.Add("cantidad_decimales_despliegue", "@cantidad_decimales_despliegue", "F");
            Ins.Add("cantidad_decimales_calculo", "@cantidad_decimales_calculo", "F");
            Ins.Add("minutos_timer_jornada_sistema", "@minutos_timer_jornada_sistema", "F");
            Ins.Add("hora_corte_jornada_sistema", "@hora_corte_jornada_sistema", "F");
            Ins.Add("generar_stock_jornada", "@generar_stock_jornada", "F");
            Ins.Add("buscar_actualizacion_hh", "@buscar_actualizacion_hh", "F");
            Ins.Add("version_bd", "@version_bd", "F");
            Ins.Add("aws_token", "@aws_token", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.AddWithValue("@IdEmpresa", oBeEmpresa.IdEmpresa);
            cmd.Parameters.AddWithValue("@nombre", oBeEmpresa.Nombre ?? string.Empty);
            cmd.Parameters.AddWithValue("@direccion", oBeEmpresa.Direccion ?? string.Empty);
            cmd.Parameters.AddWithValue("@telefono", oBeEmpresa.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("@email", oBeEmpresa.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@razon_social", oBeEmpresa.Razon_social ?? string.Empty);
            cmd.Parameters.AddWithValue("@representante", oBeEmpresa.Representante ?? string.Empty);
            cmd.Parameters.AddWithValue("@corr_cod_barra", oBeEmpresa.Corr_cod_barra);
            cmd.Parameters.AddWithValue("@path_printer", oBeEmpresa.Path_printer ?? string.Empty);
            cmd.Parameters.AddWithValue("@activo", oBeEmpresa.Activo);
            cmd.Parameters.AddWithValue("@user_agr", oBeEmpresa.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_agr", oBeEmpresa.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeEmpresa.User_mod ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeEmpresa.Fec_mod);
            cmd.Parameters.AddWithValue("@clienteRapido", oBeEmpresa.ClienteRapido);
            cmd.Parameters.AddWithValue("@imagen", oBeEmpresa.Imagen ?? Array.Empty<byte>());
            cmd.Parameters.AddWithValue("@operador_logistico", oBeEmpresa.Operador_logistico);
            cmd.Parameters.AddWithValue("@puerto_escaner", oBeEmpresa.Puerto_escaner.ToString() ?? string.Empty);
            cmd.Parameters.AddWithValue("@control_presentaciones", oBeEmpresa.Control_presentaciones);
            cmd.Parameters.AddWithValue("@anulaciones_por_supervisor", oBeEmpresa.Anulaciones_por_supervisor);
            cmd.Parameters.AddWithValue("@codigo", oBeEmpresa.Codigo ?? string.Empty);
            cmd.Parameters.AddWithValue("@clave", oBeEmpresa.Clave ?? string.Empty);
            cmd.Parameters.AddWithValue("@intento", oBeEmpresa.Intento);
            cmd.Parameters.AddWithValue("@duracionclave", oBeEmpresa.Duracionclave);
            cmd.Parameters.AddWithValue("@duracionclavetemporal", oBeEmpresa.Duracionclavetemporal);
            cmd.Parameters.AddWithValue("@codigo_automatico", oBeEmpresa.Codigo_automatico);
            cmd.Parameters.AddWithValue("@politica_contraseñas", oBeEmpresa.Politica_contraseñas);
            cmd.Parameters.AddWithValue("@IdMotivoAjusteInventario", oBeEmpresa.IdMotivoAjusteInventario);
            cmd.Parameters.AddWithValue("@cantidad_decimales_despliegue", oBeEmpresa.Cantidad_decimales_despliegue);
            cmd.Parameters.AddWithValue("@cantidad_decimales_calculo", oBeEmpresa.Cantidad_decimales_calculo);
            cmd.Parameters.AddWithValue("@minutos_timer_jornada_sistema", oBeEmpresa.Minutos_timer_jornada_sistema);
            cmd.Parameters.AddWithValue("@hora_corte_jornada_sistema", oBeEmpresa.Hora_corte_jornada_sistema == default(DateTime) ? DBNull.Value : oBeEmpresa.Hora_corte_jornada_sistema);
            cmd.Parameters.AddWithValue("@generar_stock_jornada", oBeEmpresa.Generar_stock_jornada);
            cmd.Parameters.AddWithValue("@buscar_actualizacion_hh", oBeEmpresa.Buscar_actualizacion_hh);
            cmd.Parameters.AddWithValue("@version_bd", oBeEmpresa.Version_bd ?? string.Empty);
            cmd.Parameters.AddWithValue("@aws_token", oBeEmpresa.Aws_token ?? string.Empty);

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(clsBeEmpresa oBeEmpresa,
                                SqlConnection pConection,
                                SqlTransaction pTransaction)
    {
        Upd.Init("empresa");
        Upd.Add("idempresa", "@idempresa", "F");
        Upd.Add("nombre", "@nombre", "F");
        Upd.Add("direccion", "@direccion", "F");
        Upd.Add("telefono", "@telefono", "F");
        Upd.Add("email", "@email", "F");
        Upd.Add("razon_social", "@razon_social", "F");
        Upd.Add("representante", "@representante", "F");
        Upd.Add("corr_cod_barra", "@corr_cod_barra", "F");
        Upd.Add("path_printer", "@path_printer", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("clienterapido", "@clienterapido", "F");
        Upd.Add("imagen", "@imagen", "F");
        Upd.Add("operador_logistico", "@operador_logistico", "F");
        Upd.Add("puerto_escaner", "@puerto_escaner", "F");
        Upd.Add("control_presentaciones", "@control_presentaciones", "F");
        Upd.Add("anulaciones_por_supervisor", "@anulaciones_por_supervisor", "F");
        Upd.Add("codigo", "@codigo", "F");
        Upd.Add("clave", "@clave", "F");
        Upd.Add("intento", "@intento", "F");
        Upd.Add("duracionclave", "@duracionclave", "F");
        Upd.Add("duracionclavetemporal", "@duracionclavetemporal", "F");
        Upd.Add("codigo_automatico", "@codigo_automatico", "F");
        Upd.Add("politica_contraseñas", "@politica_contraseñas", "F");
        Upd.Add("idmotivoajusteinventario", "@idmotivoajusteinventario", "F");
        Upd.Add("cantidad_decimales_despliegue", "@cantidad_decimales_despliegue", "F");
        Upd.Add("cantidad_decimales_calculo", "@cantidad_decimales_calculo", "F");
        Upd.Add("minutos_timer_jornada_sistema", "@minutos_timer_jornada_sistema", "F");
        Upd.Add("hora_corte_jornada_sistema", "@hora_corte_jornada_sistema", "F");
        Upd.Add("generar_stock_jornada", "@generar_stock_jornada", "F");
        Upd.Add("buscar_actualizacion_hh", "@buscar_actualizacion_hh", "F");
        Upd.Add("version_bd", "@version_bd", "F");
        Upd.Add("aws_token", "@aws_token", "F");
        Upd.Where("IdEmpresa = @IdEmpresa");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdEmpresa", oBeEmpresa.IdEmpresa);
            cmd.Parameters.AddWithValue("@nombre", oBeEmpresa.Nombre ?? string.Empty);
            cmd.Parameters.AddWithValue("@direccion", oBeEmpresa.Direccion ?? string.Empty);
            cmd.Parameters.AddWithValue("@telefono", oBeEmpresa.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("@email", oBeEmpresa.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@razon_social", oBeEmpresa.Razon_social ?? string.Empty);
            cmd.Parameters.AddWithValue("@representante", oBeEmpresa.Representante ?? string.Empty);
            cmd.Parameters.AddWithValue("@corr_cod_barra", oBeEmpresa.Corr_cod_barra);
            cmd.Parameters.AddWithValue("@path_printer", oBeEmpresa.Path_printer ?? string.Empty);
            cmd.Parameters.AddWithValue("@activo", oBeEmpresa.Activo);
            cmd.Parameters.AddWithValue("@user_agr", oBeEmpresa.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_agr", oBeEmpresa.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeEmpresa.User_mod ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeEmpresa.Fec_mod);
            cmd.Parameters.AddWithValue("@clienteRapido", oBeEmpresa.ClienteRapido);
            cmd.Parameters.AddWithValue("@imagen", oBeEmpresa.Imagen ?? Array.Empty<byte>());
            cmd.Parameters.AddWithValue("@operador_logistico", oBeEmpresa.Operador_logistico);
            cmd.Parameters.AddWithValue("@puerto_escaner", oBeEmpresa.Puerto_escaner.ToString() ?? string.Empty);
            cmd.Parameters.AddWithValue("@control_presentaciones", oBeEmpresa.Control_presentaciones);
            cmd.Parameters.AddWithValue("@anulaciones_por_supervisor", oBeEmpresa.Anulaciones_por_supervisor);
            cmd.Parameters.AddWithValue("@codigo", oBeEmpresa.Codigo ?? string.Empty);
            cmd.Parameters.AddWithValue("@clave", oBeEmpresa.Clave ?? string.Empty);
            cmd.Parameters.AddWithValue("@intento", oBeEmpresa.Intento);
            cmd.Parameters.AddWithValue("@duracionclave", oBeEmpresa.Duracionclave);
            cmd.Parameters.AddWithValue("@duracionclavetemporal", oBeEmpresa.Duracionclavetemporal);
            cmd.Parameters.AddWithValue("@codigo_automatico", oBeEmpresa.Codigo_automatico);
            cmd.Parameters.AddWithValue("@politica_contraseñas", oBeEmpresa.Politica_contraseñas);
            cmd.Parameters.AddWithValue("@IdMotivoAjusteInventario", oBeEmpresa.IdMotivoAjusteInventario);
            cmd.Parameters.AddWithValue("@cantidad_decimales_despliegue", oBeEmpresa.Cantidad_decimales_despliegue);
            cmd.Parameters.AddWithValue("@cantidad_decimales_calculo", oBeEmpresa.Cantidad_decimales_calculo);
            cmd.Parameters.AddWithValue("@minutos_timer_jornada_sistema", oBeEmpresa.Minutos_timer_jornada_sistema);
            cmd.Parameters.AddWithValue("@hora_corte_jornada_sistema", oBeEmpresa.Hora_corte_jornada_sistema == default(DateTime) ? DBNull.Value : oBeEmpresa.Hora_corte_jornada_sistema);
            cmd.Parameters.AddWithValue("@generar_stock_jornada", oBeEmpresa.Generar_stock_jornada);
            cmd.Parameters.AddWithValue("@buscar_actualizacion_hh", oBeEmpresa.Buscar_actualizacion_hh);
            cmd.Parameters.AddWithValue("@version_bd", oBeEmpresa.Version_bd ?? string.Empty);
            cmd.Parameters.AddWithValue("@aws_token", oBeEmpresa.Aws_token ?? string.Empty);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeEmpresa oBeEmpresa, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Empresa" +
             "  Where(IdEmpresa = @IdEmpresa)");

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

            cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeEmpresa.IdEmpresa));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return rowsAffected;

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }    

    public static bool GetSingle(IConfiguration config, ref clsBeEmpresa pBeEmpresa)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Empresa" +
            " Where(IdEmpresa = @IdEmpresa)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdEmpresa", pBeEmpresa.IdEmpresa));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeEmpresa, r);
                return true;
            }

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeEmpresa> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeEmpresa> lreturnList = new List<clsBeEmpresa>();

        try
        {
            const string sp = "Select * FROM Empresa";

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

                        clsBeEmpresa vBeEmpresa = new clsBeEmpresa();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeEmpresa = new clsBeEmpresa();
                            Cargar(ref vBeEmpresa, dr);
                            lreturnList.Add(vBeEmpresa);
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();

                }

            }

            return lreturnList;

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdEmpresa),0) FROM Empresa";

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
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
    }
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sp = "Select ISNULL(Max(IdEmpresa),0) FROM Empresa";

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                return Convert.ToInt32(lreturnValue);
            }
        }

        return 0;
    }
    public static clsBeEmpresa? GetSingle(int pIdEmpresa,
                                         SqlConnection lConnection,
                                         SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Empresa 
                           WHERE IdEmpresa = @IdEmpresa";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT?.Rows.Count > 0)
                {
                    clsBeEmpresa BeEmpresa = new clsBeEmpresa();
                    Cargar(ref BeEmpresa, lDT.Rows[0]);
                    return BeEmpresa;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeEmpresa? GetSingle_By_IdBodega(int pIdBodega,
                                                     SqlConnection lConnection,
                                                     SqlTransaction lTransaction)
    {
        try
        {
            const string sp = "SELECT Empresa.* " +
                             "FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa " +
                             "WHERE (IdBodega = @IdBodega)";

            using SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            using SqlDataAdapter dad = new SqlDataAdapter(cmd);
            {
                dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pIdBodega));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    clsBeEmpresa BeEmpresa = new clsBeEmpresa();
                    Cargar(ref BeEmpresa, dt.Rows[0]);
                    return BeEmpresa;
                }

                return null;
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }
}