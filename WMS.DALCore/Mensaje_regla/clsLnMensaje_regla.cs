using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnMensaje_regla
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeMensaje_regla oBeMensaje_regla, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeMensaje_regla.IdMensajeRegla = GetInt("IdMensajeRegla");
            oBeMensaje_regla.Nombre = GetString("Nombre");
            oBeMensaje_regla.Fec_agr = GetDate("fec_agr");
            oBeMensaje_regla.User_agr = GetString("user_agr");
            oBeMensaje_regla.Fec_mod = GetDate("fec_mod");
            oBeMensaje_regla.User_mod = GetString("user_mod");
            oBeMensaje_regla.Activo = GetBool("activo");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Insertar(clsBeMensaje_regla oBeMensaje_regla, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("mensaje_regla");
            Ins.Add("idmensajeregla", "@idmensajeregla", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdMensajeRegla", oBeMensaje_regla.IdMensajeRegla));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMensaje_regla.Nombre));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMensaje_regla.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMensaje_regla.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMensaje_regla.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMensaje_regla.User_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeMensaje_regla.Activo));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException)
        {
            throw;
        }
    }
    public static int Insertar(IConfiguration config, clsBeMensaje_regla oBeMensaje_regla)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("mensaje_regla");
            Ins.Add("idmensajeregla", "@idmensajeregla", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("activo", "@activo", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdMensajeRegla", oBeMensaje_regla.IdMensajeRegla));
            cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMensaje_regla.Nombre));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMensaje_regla.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMensaje_regla.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMensaje_regla.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMensaje_regla.User_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeMensaje_regla.Activo));

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

    public static int Actualizar(clsBeMensaje_regla oBeMensaje_regla, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("mensaje_regla");
            Upd.Add("idmensajeregla", "@idmensajeregla", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Where("IdMensajeRegla = @IdMensajeRegla");

            string sp = Upd.SQL();

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdMensajeRegla", oBeMensaje_regla.IdMensajeRegla));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMensaje_regla.Nombre));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMensaje_regla.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMensaje_regla.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMensaje_regla.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMensaje_regla.User_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeMensaje_regla.Activo));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public int Eliminar(IConfiguration config, clsBeMensaje_regla oBeMensaje_regla, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Mensaje_regla" +
             "  Where(IdMensajeRegla = @IdMensajeRegla)");

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

            cmd.Parameters.Add(new SqlParameter("@IdMensajeRegla", oBeMensaje_regla.IdMensajeRegla));

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
            const string sp = "Select * FROM Mensaje_regla";
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

    public static bool GetSingle(IConfiguration config, ref clsBeMensaje_regla pBeMensaje_regla)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Mensaje_regla" +
            " Where(IdMensajeRegla = @IdMensajeRegla)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdMensajeRegla", pBeMensaje_regla.IdMensajeRegla));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Nombre", pBeMensaje_regla.Nombre));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeMensaje_regla.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeMensaje_regla.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeMensaje_regla.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeMensaje_regla.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeMensaje_regla.Activo));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeMensaje_regla, r);
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

    public static List<clsBeMensaje_regla> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeMensaje_regla> lreturnList = new List<clsBeMensaje_regla>();

        try
        {
            const string sp = "Select * FROM Mensaje_regla";

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

                        clsBeMensaje_regla vBeMensaje_regla = new clsBeMensaje_regla();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeMensaje_regla = new clsBeMensaje_regla();
                            Cargar(ref vBeMensaje_regla, dr);
                            lreturnList.Add(vBeMensaje_regla);
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

            const string sp = "Select ISNULL(Max(IdMensajeRegla),0) FROM Mensaje_regla";

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
            const string sp = "Select ISNULL(Max(IdMensajeRegla),0) FROM Mensaje_regla";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
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

}