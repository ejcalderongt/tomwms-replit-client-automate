using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

public class clsLnEmpresa_transporte_vehiculos
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeEmpresa_transporte_vehiculos oBeEmpresa_transporte_vehiculos, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }     
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeEmpresa_transporte_vehiculos.IdVehiculo = GetInt("IdVehiculo");
            oBeEmpresa_transporte_vehiculos.IdEmpresaTransporte = GetInt("IdEmpresaTransporte");
            oBeEmpresa_transporte_vehiculos.IdTipoContenedor = GetInt("IdTipoContenedor");
            oBeEmpresa_transporte_vehiculos.Placa = GetString("placa");
            oBeEmpresa_transporte_vehiculos.Marca = GetString("marca");
            oBeEmpresa_transporte_vehiculos.Modelo = GetString("modelo");
            oBeEmpresa_transporte_vehiculos.Peso = GetDecimal("peso");
            oBeEmpresa_transporte_vehiculos.Volumen = GetDecimal("volumen");
            oBeEmpresa_transporte_vehiculos.Activo = GetBool("activo");
            oBeEmpresa_transporte_vehiculos.User_agr = GetString("user_agr");
            oBeEmpresa_transporte_vehiculos.Fec_agr = GetDate("fec_agr");
            oBeEmpresa_transporte_vehiculos.User_mod = GetString("user_mod");
            oBeEmpresa_transporte_vehiculos.Fec_mod = GetDate("fec_mod");
            oBeEmpresa_transporte_vehiculos.Tipo = GetString("tipo");
            oBeEmpresa_transporte_vehiculos.Alto = GetDecimal("alto");
            oBeEmpresa_transporte_vehiculos.Largo = GetDecimal("largo");
            oBeEmpresa_transporte_vehiculos.Ancho = GetDecimal("ancho");
            oBeEmpresa_transporte_vehiculos.Placa_comercial = GetString("placa_comercial");
            oBeEmpresa_transporte_vehiculos.Es_contedor = GetInt("es_contedor");
        }
        catch (Exception)
        {            
            throw;
        }
    }    
    public static int Insertar(clsBeEmpresa_transporte_vehiculos oBeEmpresa_transporte_vehiculos, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Ins.Init("empresa_transporte_vehiculos");
            Ins.Add("idvehiculo", "@idvehiculo", "F");
            Ins.Add("idempresatransporte", "@idempresatransporte", "F");
            Ins.Add("idtipocontenedor", "@idtipocontenedor", "F");
            Ins.Add("placa", "@placa", "F");
            Ins.Add("marca", "@marca", "F");
            Ins.Add("modelo", "@modelo", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("volumen", "@volumen", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("tipo", "@tipo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("placa_comercial", "@placa_comercial", "F");
            Ins.Add("es_contedor", "@es_contedor", "F");

            string sp = Ins.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeEmpresa_transporte_vehiculos.IdVehiculo));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresaTransporte", oBeEmpresa_transporte_vehiculos.IdEmpresaTransporte));
            cmd.Parameters.Add(new SqlParameter("@IdTipoContenedor", oBeEmpresa_transporte_vehiculos.IdTipoContenedor));
            cmd.Parameters.Add(new SqlParameter("@placa", oBeEmpresa_transporte_vehiculos.Placa));
            cmd.Parameters.Add(new SqlParameter("@marca", oBeEmpresa_transporte_vehiculos.Marca));
            cmd.Parameters.Add(new SqlParameter("@modelo", oBeEmpresa_transporte_vehiculos.Modelo));
            cmd.Parameters.Add(new SqlParameter("@peso", oBeEmpresa_transporte_vehiculos.Peso));
            cmd.Parameters.Add(new SqlParameter("@volumen", oBeEmpresa_transporte_vehiculos.Volumen));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeEmpresa_transporte_vehiculos.Activo));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeEmpresa_transporte_vehiculos.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeEmpresa_transporte_vehiculos.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeEmpresa_transporte_vehiculos.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeEmpresa_transporte_vehiculos.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@tipo", oBeEmpresa_transporte_vehiculos.Tipo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeEmpresa_transporte_vehiculos.Alto));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeEmpresa_transporte_vehiculos.Largo));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeEmpresa_transporte_vehiculos.Ancho));
            cmd.Parameters.Add(new SqlParameter("@placa_comercial", oBeEmpresa_transporte_vehiculos.Placa_comercial));
            cmd.Parameters.Add(new SqlParameter("@es_contedor", oBeEmpresa_transporte_vehiculos.Es_contedor));

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

    public static int Actualizar(clsBeEmpresa_transporte_vehiculos oBeEmpresa_transporte_vehiculos, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("empresa_transporte_vehiculos");
            Upd.Add("idvehiculo", "@idvehiculo", "F");
            Upd.Add("idempresatransporte", "@idempresatransporte", "F");
            Upd.Add("idtipocontenedor", "@idtipocontenedor", "F");
            Upd.Add("placa", "@placa", "F");
            Upd.Add("marca", "@marca", "F");
            Upd.Add("modelo", "@modelo", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("volumen", "@volumen", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("tipo", "@tipo", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("placa_comercial", "@placa_comercial", "F");
            Upd.Add("es_contedor", "@es_contedor", "F");
            Upd.Where("IdVehiculo = @IdVehiculo");

            string sp = Upd.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeEmpresa_transporte_vehiculos.IdVehiculo));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresaTransporte", oBeEmpresa_transporte_vehiculos.IdEmpresaTransporte));
            cmd.Parameters.Add(new SqlParameter("@IdTipoContenedor", oBeEmpresa_transporte_vehiculos.IdTipoContenedor));
            cmd.Parameters.Add(new SqlParameter("@placa", oBeEmpresa_transporte_vehiculos.Placa));
            cmd.Parameters.Add(new SqlParameter("@marca", oBeEmpresa_transporte_vehiculos.Marca));
            cmd.Parameters.Add(new SqlParameter("@modelo", oBeEmpresa_transporte_vehiculos.Modelo));
            cmd.Parameters.Add(new SqlParameter("@peso", oBeEmpresa_transporte_vehiculos.Peso));
            cmd.Parameters.Add(new SqlParameter("@volumen", oBeEmpresa_transporte_vehiculos.Volumen));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeEmpresa_transporte_vehiculos.Activo));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeEmpresa_transporte_vehiculos.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeEmpresa_transporte_vehiculos.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeEmpresa_transporte_vehiculos.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeEmpresa_transporte_vehiculos.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@tipo", oBeEmpresa_transporte_vehiculos.Tipo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeEmpresa_transporte_vehiculos.Alto));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeEmpresa_transporte_vehiculos.Largo));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeEmpresa_transporte_vehiculos.Ancho));
            cmd.Parameters.Add(new SqlParameter("@placa_comercial", oBeEmpresa_transporte_vehiculos.Placa_comercial));
            cmd.Parameters.Add(new SqlParameter("@es_contedor", oBeEmpresa_transporte_vehiculos.Es_contedor));

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

    public int Eliminar(IConfiguration config, clsBeEmpresa_transporte_vehiculos oBeEmpresa_transporte_vehiculos, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Empresa_transporte_vehiculos" +
             "  Where(IdVehiculo = @IdVehiculo)");

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

            cmd.Parameters.Add(new SqlParameter("@IdVehiculo", oBeEmpresa_transporte_vehiculos.IdVehiculo));

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
            const string sp = "Select * FROM Empresa_transporte_vehiculos";
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

    public static bool GetSingle(IConfiguration config, ref clsBeEmpresa_transporte_vehiculos pBeEmpresa_transporte_vehiculos)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Empresa_transporte_vehiculos" +
            " Where(IdVehiculo = @IdVehiculo)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdVehiculo", pBeEmpresa_transporte_vehiculos.IdVehiculo));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeEmpresa_transporte_vehiculos, r);
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

    public static List<clsBeEmpresa_transporte_vehiculos> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeEmpresa_transporte_vehiculos> lreturnList = new List<clsBeEmpresa_transporte_vehiculos>();

        try
        {
            const string sp = "Select * FROM Empresa_transporte_vehiculos";

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

                        clsBeEmpresa_transporte_vehiculos vBeEmpresa_transporte_vehiculos = new clsBeEmpresa_transporte_vehiculos();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeEmpresa_transporte_vehiculos = new clsBeEmpresa_transporte_vehiculos();
                            Cargar(ref vBeEmpresa_transporte_vehiculos, dr);
                            lreturnList.Add(vBeEmpresa_transporte_vehiculos);
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

            const string sp = "Select ISNULL(Max(IdVehiculo),0) FROM Empresa_transporte_vehiculos";

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
            const string sp = "Select ISNULL(Max(IdVehiculo),0) FROM Empresa_transporte_vehiculos";

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

    public static bool Existe_Placa(string pPlaca, SqlConnection pConection, SqlTransaction pTransaction)
    {
        try
        {
            bool lExists = false;

            string vSQL = "SELECT COUNT(1) FROM empresa_transporte_vehiculos WHERE Placa=@Placa";

            using (SqlCommand lCommand = new SqlCommand(vSQL, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@Placa", pPlaca);

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

    public static clsBeEmpresa_transporte_vehiculos? Get_Single_By_No_Placa(string pPlaca,
                                                                           SqlConnection lConnection,
                                                                           SqlTransaction lTransaction)
    {
        clsBeEmpresa_transporte_vehiculos? oBeEmpresa_transporte_vehiculos = new clsBeEmpresa_transporte_vehiculos();

        try
        {
            string sp = "SELECT * FROM Empresa_transporte_vehiculos " +
                       " Where(Placa = @Placa)";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
            using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
            {
                dad.SelectCommand.Parameters.Add(new SqlParameter("@PLACA", pPlaca));
                DataTable dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    var lrow = dt.Rows[0];
                    Cargar(ref oBeEmpresa_transporte_vehiculos, lrow);
                    return oBeEmpresa_transporte_vehiculos;
                }
            }

            return null;
        }
        catch (Exception)
        {     
            throw;
        }
    }
}
