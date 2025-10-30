using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class clsLnColor
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeColor oBeColor, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        
        try
        {
            oBeColor.IdColor = GetInt("IdColor");
            oBeColor.Nombre = GetString("Nombre");
            oBeColor.CodigoHex = GetString("CodigoHex");
            oBeColor.IdPropietario = GetInt("IdPropietario");
            oBeColor.Fec_agr = GetDate("fec_agr");
            oBeColor.User_agr = GetString("user_agr");
            oBeColor.Fec_mod = GetDate("fec_mod");
            oBeColor.User_mod = GetString("user_mod");
            oBeColor.Activo = GetBool("activo");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeTalla oBeTalla,
                              SqlConnection pConection,
                              SqlTransaction pTransaction)
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

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdTalla", oBeTalla.IdTalla);
            cmd.Parameters.AddWithValue("@Nombre", oBeTalla.Nombre ?? string.Empty);
            cmd.Parameters.AddWithValue("@Descripcion", oBeTalla.Descripcion ?? string.Empty);
            cmd.Parameters.AddWithValue("@IdPropietario", oBeTalla.IdPropietario);
            cmd.Parameters.AddWithValue("@fec_agr", oBeTalla.Fec_agr);
            cmd.Parameters.AddWithValue("@user_agr", oBeTalla.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeTalla.Fec_mod);
            cmd.Parameters.AddWithValue("@user_mod", oBeTalla.User_mod ?? string.Empty);
            cmd.Parameters.AddWithValue("@activo", oBeTalla.Activo);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeColor oBeColor)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("color");
            Ins.Add("idcolor", "@idcolor", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("codigohex", "@codigohex", "F");
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

            cmd.Parameters.Add(new SqlParameter("@IdColor", oBeColor.IdColor));
            cmd.Parameters.Add(new SqlParameter("@Nombre", oBeColor.Nombre));
            cmd.Parameters.Add(new SqlParameter("@CodigoHex", oBeColor.CodigoHex));
            cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeColor.IdPropietario));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeColor.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeColor.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeColor.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeColor.User_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeColor.Activo));

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

    public static int Actualizar(clsBeColor oBeColor,
                                SqlConnection pConection,
                                SqlTransaction pTransaction)
    {
        Upd.Init("color");
        Upd.Add("idcolor", "@idcolor", "F");
        Upd.Add("nombre", "@nombre", "F");
        Upd.Add("codigohex", "@codigohex", "F");
        Upd.Add("idpropietario", "@idpropietario", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Where("IdColor = @IdColor");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdColor", oBeColor.IdColor);
            cmd.Parameters.AddWithValue("@Nombre", oBeColor.Nombre ?? string.Empty);
            cmd.Parameters.AddWithValue("@CodigoHex", oBeColor.CodigoHex ?? string.Empty);
            cmd.Parameters.AddWithValue("@IdPropietario", oBeColor.IdPropietario);
            cmd.Parameters.AddWithValue("@fec_agr", oBeColor.Fec_agr);
            cmd.Parameters.AddWithValue("@user_agr", oBeColor.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeColor.Fec_mod);
            cmd.Parameters.AddWithValue("@user_mod", oBeColor.User_mod ?? string.Empty);
            cmd.Parameters.AddWithValue("@activo", oBeColor.Activo);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeColor oBeColor, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Color" +
             "  Where(IdColor = @IdColor)");

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

            cmd.Parameters.Add(new SqlParameter("@IdColor", oBeColor.IdColor));

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

    public static bool GetSingle(IConfiguration config, ref clsBeColor pBeColor)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Color" +
            " Where(IdColor = @IdColor)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdColor", pBeColor.IdColor));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeColor, r);
                return true;
            }

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();            
            throw ;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeColor> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeColor> lreturnList = new List<clsBeColor>();

        try
        {
            const string sp = "Select * FROM Color";

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

                        clsBeColor vBeColor = new clsBeColor();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeColor = new clsBeColor();
                            Cargar(ref vBeColor, dr);
                            lreturnList.Add(vBeColor);
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

            const string sp = "Select ISNULL(Max(IdColor),0) FROM Color";

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
        const string sp = "Select ISNULL(Max(IdColor),0) FROM Color";

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
    public static clsBeColor? GetSingle(int IdColor,
                                   SqlConnection lConnection,
                                   SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM color 
                           WHERE IdColor = @IdColor";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdColor", IdColor);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    clsBeColor BeColor = new clsBeColor();
                    Cargar(ref BeColor, lDataTable.Rows[0]);
                    return BeColor;
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
