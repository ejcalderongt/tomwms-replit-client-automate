using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class clsLnTalla
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTalla oBeTalla, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        
        try
        {
            oBeTalla.IdTalla = GetInt("IdTalla");
            oBeTalla.Nombre = GetString("Nombre");
            oBeTalla.Descripcion = GetString("Descripcion");
            oBeTalla.IdPropietario = GetInt("IdPropietario");
            oBeTalla.Fec_agr = GetDate("fec_agr");
            oBeTalla.User_agr = GetString("user_agr");
            oBeTalla.Fec_mod = GetDate("fec_mod");
            oBeTalla.User_mod = GetString("user_mod");
            oBeTalla.Activo = GetBool("activo");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeTalla oBeTalla, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTalla == null)
            throw new ArgumentNullException(nameof(oBeTalla));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("talla");
            Ins.Add("idtalla", "@idtalla", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdTalla", oBeTalla.IdTalla));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeTalla.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTalla.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeTalla.IdPropietario));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTalla.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTalla.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTalla.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTalla.User_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeTalla.Activo));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";         
            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(IConfiguration config, clsBeTalla oBeTalla)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("talla");
            Ins.Add("idtalla", "@idtalla", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdTalla", oBeTalla.IdTalla));
            cmd.Parameters.Add(new SqlParameter("@Nombre", oBeTalla.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTalla.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeTalla.IdPropietario));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTalla.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTalla.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTalla.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTalla.User_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTalla.Activo));

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

    public static int Actualizar(clsBeTalla oBeTalla, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeTalla == null)
            throw new ArgumentNullException(nameof(oBeTalla));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Upd.Init("talla");
            Upd.Add("idtalla", "@idtalla", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdTalla = @IdTalla");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdTalla", oBeTalla.IdTalla));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeTalla.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTalla.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeTalla.IdPropietario));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTalla.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTalla.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTalla.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTalla.User_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeTalla.Activo));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public int Eliminar(IConfiguration config, clsBeTalla oBeTalla, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Talla" +
             "  Where(IdTalla = @IdTalla)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTalla", oBeTalla.IdTalla));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTalla pBeTalla)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Talla" +
            " Where(IdTalla = @IdTalla)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTalla", pBeTalla.IdTalla));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTalla, r);
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

    public static List<clsBeTalla> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTalla> lreturnList = new List<clsBeTalla>();

        try
        {
            const string sp = "Select * FROM Talla";

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

                        clsBeTalla vBeTalla = new clsBeTalla();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTalla = new clsBeTalla();
                            Cargar(ref vBeTalla, dr);
                            lreturnList.Add(vBeTalla);
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

            const string sp = "Select ISNULL(Max(IdTalla),0) FROM Talla";

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
        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        try
        {
            const string sp = "Select ISNULL(Max(IdTalla),0) FROM Talla";

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
        catch (SqlException)
        {
            throw;
        }
    }

    public static clsBeTalla? GetSingle(int IdTalla,
                                       SqlConnection lConnection,
                                       SqlTransaction lTransaction) 
    {
        try
        {
            const string sp = @"SELECT * FROM talla 
                           WHERE IdTalla = @IdTalla";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTalla", IdTalla);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    clsBeTalla BeTalla = new clsBeTalla();
                    Cargar(ref BeTalla, lDataTable.Rows[0]);
                    return BeTalla;
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
