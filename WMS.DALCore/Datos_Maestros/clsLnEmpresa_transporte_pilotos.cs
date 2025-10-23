using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;
public class clsLnEmpresa_transporte_pilotos
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }        

        try
        {
            oBeEmpresa_transporte_pilotos.IdPiloto = GetInt("IdPiloto");
            oBeEmpresa_transporte_pilotos.IdEmpresaTransporte = GetInt("IdEmpresaTransporte");
            oBeEmpresa_transporte_pilotos.Nombres = GetString("nombres");
            oBeEmpresa_transporte_pilotos.Apellidos = GetString("apellidos");
            oBeEmpresa_transporte_pilotos.Telefono = GetString("telefono");
            oBeEmpresa_transporte_pilotos.Correo_electronico = GetString("correo_electronico");
            oBeEmpresa_transporte_pilotos.No_carnet = GetString("no_carnet");
            oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet = GetDate("fecha_expiracion_carnet");
            oBeEmpresa_transporte_pilotos.No_dpi = GetString("no_dpi");
            oBeEmpresa_transporte_pilotos.No_licencia = GetString("no_licencia");
            oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia = GetDate("fecha_expiracion_licencia");
            oBeEmpresa_transporte_pilotos.Codigo_barra = GetString("codigo_barra");
            oBeEmpresa_transporte_pilotos.Direccion = GetString("direccion");
            oBeEmpresa_transporte_pilotos.Foto = GetBytes("foto");
            oBeEmpresa_transporte_pilotos.Fecha_nacimiento = GetDate("fecha_nacimiento");
            oBeEmpresa_transporte_pilotos.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeEmpresa_transporte_pilotos.Fecha_salida = GetDate("fecha_salida");
            oBeEmpresa_transporte_pilotos.IdTipoLicencia = GetString("IdTipoLicencia");
            oBeEmpresa_transporte_pilotos.User_agr = GetString("user_agr");
            oBeEmpresa_transporte_pilotos.Fec_agr = GetDate("fec_agr");
            oBeEmpresa_transporte_pilotos.User_mod = GetString("user_mod");
            oBeEmpresa_transporte_pilotos.Fec_mod = GetDate("fec_mod");
            oBeEmpresa_transporte_pilotos.Activo = GetBool("activo");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Ins.Init("empresa_transporte_pilotos");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("idempresatransporte", "@idempresatransporte", "F");
            Ins.Add("nombres", "@nombres", "F");
            Ins.Add("apellidos", "@apellidos", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("correo_electronico", "@correo_electronico", "F");
            Ins.Add("no_carnet", "@no_carnet", "F");
            Ins.Add("fecha_expiracion_carnet", "@fecha_expiracion_carnet", "F");
            Ins.Add("no_dpi", "@no_dpi", "F");
            Ins.Add("no_licencia", "@no_licencia", "F");
            Ins.Add("fecha_expiracion_licencia", "@fecha_expiracion_licencia", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("foto", "@foto", "F");
            Ins.Add("fecha_nacimiento", "@fecha_nacimiento", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_salida", "@fecha_salida", "F");
            Ins.Add("idtipolicencia", "@idtipolicencia", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeEmpresa_transporte_pilotos.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresaTransporte", oBeEmpresa_transporte_pilotos.IdEmpresaTransporte));
            cmd.Parameters.Add(new SqlParameter("@nombres", oBeEmpresa_transporte_pilotos.Nombres));
            cmd.Parameters.Add(new SqlParameter("@apellidos", oBeEmpresa_transporte_pilotos.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@telefono", oBeEmpresa_transporte_pilotos.Telefono));
            cmd.Parameters.Add(new SqlParameter("@correo_electronico", oBeEmpresa_transporte_pilotos.Correo_electronico));
            cmd.Parameters.Add(new SqlParameter("@no_carnet", oBeEmpresa_transporte_pilotos.No_carnet));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_carnet", oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet));
            cmd.Parameters.Add(new SqlParameter("@no_dpi", oBeEmpresa_transporte_pilotos.No_dpi));
            cmd.Parameters.Add(new SqlParameter("@no_licencia", oBeEmpresa_transporte_pilotos.No_licencia));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_licencia", oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeEmpresa_transporte_pilotos.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@direccion", oBeEmpresa_transporte_pilotos.Direccion));
            cmd.Parameters.Add(new SqlParameter("@foto", oBeEmpresa_transporte_pilotos.Foto));
            cmd.Parameters.Add(new SqlParameter("@fecha_nacimiento", oBeEmpresa_transporte_pilotos.Fecha_nacimiento));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeEmpresa_transporte_pilotos.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_salida", oBeEmpresa_transporte_pilotos.Fecha_salida));
            cmd.Parameters.Add(new SqlParameter("@IdTipoLicencia", oBeEmpresa_transporte_pilotos.IdTipoLicencia));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeEmpresa_transporte_pilotos.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeEmpresa_transporte_pilotos.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeEmpresa_transporte_pilotos.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeEmpresa_transporte_pilotos.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeEmpresa_transporte_pilotos.Activo));

            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (SqlException)
        {            
            throw;
        }
        finally
        {
            cmd?.Dispose();
        }

        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("empresa_transporte_pilotos");
            Ins.Add("idpiloto", "@idpiloto", "F");
            Ins.Add("idempresatransporte", "@idempresatransporte", "F");
            Ins.Add("nombres", "@nombres", "F");
            Ins.Add("apellidos", "@apellidos", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("correo_electronico", "@correo_electronico", "F");
            Ins.Add("no_carnet", "@no_carnet", "F");
            Ins.Add("fecha_expiracion_carnet", "@fecha_expiracion_carnet", "F");
            Ins.Add("no_dpi", "@no_dpi", "F");
            Ins.Add("no_licencia", "@no_licencia", "F");
            Ins.Add("fecha_expiracion_licencia", "@fecha_expiracion_licencia", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("foto", "@foto", "F");
            Ins.Add("fecha_nacimiento", "@fecha_nacimiento", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_salida", "@fecha_salida", "F");
            Ins.Add("idtipolicencia", "@idtipolicencia", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeEmpresa_transporte_pilotos.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresaTransporte", oBeEmpresa_transporte_pilotos.IdEmpresaTransporte));
            cmd.Parameters.Add(new SqlParameter("@nombres", oBeEmpresa_transporte_pilotos.Nombres));
            cmd.Parameters.Add(new SqlParameter("@apellidos", oBeEmpresa_transporte_pilotos.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@telefono", oBeEmpresa_transporte_pilotos.Telefono));
            cmd.Parameters.Add(new SqlParameter("@correo_electronico", oBeEmpresa_transporte_pilotos.Correo_electronico));
            cmd.Parameters.Add(new SqlParameter("@no_carnet", oBeEmpresa_transporte_pilotos.No_carnet));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_carnet", oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet));
            cmd.Parameters.Add(new SqlParameter("@no_dpi", oBeEmpresa_transporte_pilotos.No_dpi));
            cmd.Parameters.Add(new SqlParameter("@no_licencia", oBeEmpresa_transporte_pilotos.No_licencia));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_licencia", oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeEmpresa_transporte_pilotos.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@direccion", oBeEmpresa_transporte_pilotos.Direccion));
            cmd.Parameters.Add(new SqlParameter("@foto", oBeEmpresa_transporte_pilotos.Foto));
            cmd.Parameters.Add(new SqlParameter("@fecha_nacimiento", oBeEmpresa_transporte_pilotos.Fecha_nacimiento));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeEmpresa_transporte_pilotos.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_salida", oBeEmpresa_transporte_pilotos.Fecha_salida));
            cmd.Parameters.Add(new SqlParameter("@IdTipoLicencia", oBeEmpresa_transporte_pilotos.IdTipoLicencia));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeEmpresa_transporte_pilotos.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeEmpresa_transporte_pilotos.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeEmpresa_transporte_pilotos.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeEmpresa_transporte_pilotos.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeEmpresa_transporte_pilotos.Activo));

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

    public static int Actualizar(clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("empresa_transporte_pilotos");
            Upd.Add("idpiloto", "@idpiloto", "F");
            Upd.Add("idempresatransporte", "@idempresatransporte", "F");
            Upd.Add("nombres", "@nombres", "F");
            Upd.Add("apellidos", "@apellidos", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("correo_electronico", "@correo_electronico", "F");
            Upd.Add("no_carnet", "@no_carnet", "F");
            Upd.Add("fecha_expiracion_carnet", "@fecha_expiracion_carnet", "F");
            Upd.Add("no_dpi", "@no_dpi", "F");
            Upd.Add("no_licencia", "@no_licencia", "F");
            Upd.Add("fecha_expiracion_licencia", "@fecha_expiracion_licencia", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("foto", "@foto", "F");
            Upd.Add("fecha_nacimiento", "@fecha_nacimiento", "F");
            Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Upd.Add("fecha_salida", "@fecha_salida", "F");
            Upd.Add("idtipolicencia", "@idtipolicencia", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdPiloto = @IdPiloto");

            string sp = Upd.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeEmpresa_transporte_pilotos.IdPiloto));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresaTransporte", oBeEmpresa_transporte_pilotos.IdEmpresaTransporte));
            cmd.Parameters.Add(new SqlParameter("@nombres", oBeEmpresa_transporte_pilotos.Nombres));
            cmd.Parameters.Add(new SqlParameter("@apellidos", oBeEmpresa_transporte_pilotos.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@telefono", oBeEmpresa_transporte_pilotos.Telefono));
            cmd.Parameters.Add(new SqlParameter("@correo_electronico", oBeEmpresa_transporte_pilotos.Correo_electronico));
            cmd.Parameters.Add(new SqlParameter("@no_carnet", oBeEmpresa_transporte_pilotos.No_carnet));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_carnet", oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet));
            cmd.Parameters.Add(new SqlParameter("@no_dpi", oBeEmpresa_transporte_pilotos.No_dpi));
            cmd.Parameters.Add(new SqlParameter("@no_licencia", oBeEmpresa_transporte_pilotos.No_licencia));
            cmd.Parameters.Add(new SqlParameter("@fecha_expiracion_licencia", oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeEmpresa_transporte_pilotos.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@direccion", oBeEmpresa_transporte_pilotos.Direccion));
            cmd.Parameters.Add(new SqlParameter("@foto", oBeEmpresa_transporte_pilotos.Foto));
            cmd.Parameters.Add(new SqlParameter("@fecha_nacimiento", oBeEmpresa_transporte_pilotos.Fecha_nacimiento));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeEmpresa_transporte_pilotos.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_salida", oBeEmpresa_transporte_pilotos.Fecha_salida));
            cmd.Parameters.Add(new SqlParameter("@IdTipoLicencia", oBeEmpresa_transporte_pilotos.IdTipoLicencia));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeEmpresa_transporte_pilotos.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeEmpresa_transporte_pilotos.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeEmpresa_transporte_pilotos.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeEmpresa_transporte_pilotos.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeEmpresa_transporte_pilotos.Activo));

            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (SqlException)
        {            
            throw;
        }
        finally
        {
            cmd?.Dispose();
        }

        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Empresa_transporte_pilotos" +
             "  Where(IdPiloto = @IdPiloto)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPiloto", oBeEmpresa_transporte_pilotos.IdPiloto));

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

    public DataTable Listar(IConfiguration config)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM Empresa_transporte_pilotos";
            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            return dt;

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

    public static bool GetSingle(IConfiguration config, ref clsBeEmpresa_transporte_pilotos pBeEmpresa_transporte_pilotos)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Empresa_transporte_pilotos" +
            " Where(IdPiloto = @IdPiloto)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPiloto", pBeEmpresa_transporte_pilotos.IdPiloto));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeEmpresa_transporte_pilotos, r);
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

    public static List<clsBeEmpresa_transporte_pilotos> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeEmpresa_transporte_pilotos> lreturnList = new List<clsBeEmpresa_transporte_pilotos>();

        try
        {
            const string sp = "Select * FROM Empresa_transporte_pilotos";

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

                        clsBeEmpresa_transporte_pilotos vBeEmpresa_transporte_pilotos = new clsBeEmpresa_transporte_pilotos();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeEmpresa_transporte_pilotos = new clsBeEmpresa_transporte_pilotos();
                            Cargar(ref vBeEmpresa_transporte_pilotos, dr);
                            lreturnList.Add(vBeEmpresa_transporte_pilotos);
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

            const string sp = "Select ISNULL(Max(IdPiloto),0) FROM Empresa_transporte_pilotos";

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
        int lMax = 0;

        try
        {
            const string sp = "Select ISNULL(Max(IdPiloto),0) FROM Empresa_transporte_pilotos";

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                object lreturnValue = cmd.ExecuteScalar();

                if (lreturnValue != DBNull.Value && lreturnValue != null)
                {
                    lMax = Convert.ToInt32(lreturnValue);
                }
            }

            return lMax;
        }
        catch (SqlException)
        {    
            throw;
        }
    }

    public static bool Existe_No_Licencia(string pNoLicencia, SqlConnection pConection, SqlTransaction pTransaction)
    {
        try
        {
            bool lExists = false;

            string vSQL = "SELECT COUNT(1) FROM empresa_transporte_pilotos WHERE no_licencia=@no_licencia";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@no_licencia", pNoLicencia);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lExists = Convert.ToInt32(lReturnValue) > 0;
                }
            }

            return lExists;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static clsBeEmpresa_transporte_pilotos Get_By_No_Documento(string pNoDocumento, SqlConnection pConection, SqlTransaction pTransaction)
    {
        clsBeEmpresa_transporte_pilotos oBeEmpresa_transporte_pilotos = new clsBeEmpresa_transporte_pilotos();

        try
        {
            string sp = "SELECT * FROM Empresa_transporte_pilotos" +
                       " Where(no_dpi = @no_documento OR no_licencia = @no_documento)";

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
            {
                dad.SelectCommand.Parameters.Add(new SqlParameter("@no_documento", pNoDocumento));
                DataTable dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    var lrow = dt.Rows[0];
                    Cargar(ref oBeEmpresa_transporte_pilotos, lrow);
                    oBeEmpresa_transporte_pilotos.IsNew = false;
                }
            }

            return oBeEmpresa_transporte_pilotos;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
