using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Ticket;

public class clsLnTms_ticket_pol
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTms_ticket_pol oBeTms_ticket_pol, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTms_ticket_pol.IdTicket = GetInt("IdTicket");
            oBeTms_ticket_pol.IdOrdenTmsEnc = GetInt("IdOrdenTmsEnc");
            oBeTms_ticket_pol.NoPoliza = GetString("NoPoliza");
            oBeTms_ticket_pol.Dua = GetString("dua");
            oBeTms_ticket_pol.Fecha_poliza = GetDate("fecha_poliza");
            oBeTms_ticket_pol.Pais_procede = GetString("pais_procede");
            oBeTms_ticket_pol.Tipo_cambio = GetDecimal("tipo_cambio");
            oBeTms_ticket_pol.Total_valoraduana = GetDecimal("total_valoraduana");
            oBeTms_ticket_pol.Total_bultos_peso = GetDecimal("total_bultos_peso");
            oBeTms_ticket_pol.Total_usd = GetDecimal("total_usd");
            oBeTms_ticket_pol.Total_flete = GetDecimal("total_flete");
            oBeTms_ticket_pol.Total_seguro = GetDecimal("total_seguro");
            oBeTms_ticket_pol.User_agr = GetString("user_agr");
            oBeTms_ticket_pol.Fec_agr = GetDate("fec_agr");
            oBeTms_ticket_pol.User_mod = GetString("user_mod");
            oBeTms_ticket_pol.Fec_mod = GetDate("fec_mod");
            oBeTms_ticket_pol.Clave_aduana = GetString("clave_aduana");
            oBeTms_ticket_pol.Nit_imp_exp = GetString("nit_imp_exp");
            oBeTms_ticket_pol.Clase = GetString("clase");
            oBeTms_ticket_pol.Mod_transporte = GetString("mod_transporte");
            oBeTms_ticket_pol.Total_liquidar = GetDecimal("total_liquidar");
            oBeTms_ticket_pol.Total_general = GetDecimal("total_general");
            oBeTms_ticket_pol.IdRegimen = GetInt("IdRegimen");
            oBeTms_ticket_pol.Codigo_Barra = GetString("Codigo_Barra");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeTms_ticket_pol oBeTms_ticket_pol, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Ins.Init("tms_ticket_pol");
            Ins.Add("idticket", "@idticket", "F");
            Ins.Add("idordentmsenc", "@idordentmsenc", "F");
            Ins.Add("nopoliza", "@nopoliza", "F");
            Ins.Add("dua", "@dua", "F");
            Ins.Add("fecha_poliza", "@fecha_poliza", "F");
            Ins.Add("pais_procede", "@pais_procede", "F");
            Ins.Add("tipo_cambio", "@tipo_cambio", "F");
            Ins.Add("total_valoraduana", "@total_valoraduana", "F");
            Ins.Add("total_bultos_peso", "@total_bultos_peso", "F");
            Ins.Add("total_usd", "@total_usd", "F");
            Ins.Add("total_flete", "@total_flete", "F");
            Ins.Add("total_seguro", "@total_seguro", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("clave_aduana", "@clave_aduana", "F");
            Ins.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Ins.Add("clase", "@clase", "F");
            Ins.Add("mod_transporte", "@mod_transporte", "F");
            Ins.Add("total_liquidar", "@total_liquidar", "F");
            Ins.Add("total_general", "@total_general", "F");
            Ins.Add("idregimen", "@idregimen", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");

            string sp = Ins.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdTicket", oBeTms_ticket_pol.IdTicket));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenTmsEnc", oBeTms_ticket_pol.IdOrdenTmsEnc));
            cmd.Parameters.Add(new SqlParameter("@NoPoliza", oBeTms_ticket_pol.NoPoliza));
            cmd.Parameters.Add(new SqlParameter("@dua", oBeTms_ticket_pol.Dua));
            cmd.Parameters.Add(new SqlParameter("@fecha_poliza", oBeTms_ticket_pol.Fecha_poliza));
            cmd.Parameters.Add(new SqlParameter("@pais_procede", oBeTms_ticket_pol.Pais_procede));
            cmd.Parameters.Add(new SqlParameter("@tipo_cambio", oBeTms_ticket_pol.Tipo_cambio));
            cmd.Parameters.Add(new SqlParameter("@total_valoraduana", oBeTms_ticket_pol.Total_valoraduana));
            cmd.Parameters.Add(new SqlParameter("@total_bultos_peso", oBeTms_ticket_pol.Total_bultos_peso));
            cmd.Parameters.Add(new SqlParameter("@total_usd", oBeTms_ticket_pol.Total_usd));
            cmd.Parameters.Add(new SqlParameter("@total_flete", oBeTms_ticket_pol.Total_flete));
            cmd.Parameters.Add(new SqlParameter("@total_seguro", oBeTms_ticket_pol.Total_seguro));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTms_ticket_pol.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTms_ticket_pol.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTms_ticket_pol.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTms_ticket_pol.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@clave_aduana", oBeTms_ticket_pol.Clave_aduana));
            cmd.Parameters.Add(new SqlParameter("@nit_imp_exp", oBeTms_ticket_pol.Nit_imp_exp));
            cmd.Parameters.Add(new SqlParameter("@clase", oBeTms_ticket_pol.Clase));
            cmd.Parameters.Add(new SqlParameter("@mod_transporte", oBeTms_ticket_pol.Mod_transporte));
            cmd.Parameters.Add(new SqlParameter("@total_liquidar", oBeTms_ticket_pol.Total_liquidar));
            cmd.Parameters.Add(new SqlParameter("@total_general", oBeTms_ticket_pol.Total_general));
            cmd.Parameters.Add(new SqlParameter("@IdRegimen", oBeTms_ticket_pol.IdRegimen));
            cmd.Parameters.Add(new SqlParameter("@Codigo_Barra", oBeTms_ticket_pol.Codigo_Barra));

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

    public static int Insertar(IConfiguration config, clsBeTms_ticket_pol oBeTms_ticket_pol)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("tms_ticket_pol");
            Ins.Add("idticket", "@idticket", "F");
            Ins.Add("idordentmsenc", "@idordentmsenc", "F");
            Ins.Add("nopoliza", "@nopoliza", "F");
            Ins.Add("dua", "@dua", "F");
            Ins.Add("fecha_poliza", "@fecha_poliza", "F");
            Ins.Add("pais_procede", "@pais_procede", "F");
            Ins.Add("tipo_cambio", "@tipo_cambio", "F");
            Ins.Add("total_valoraduana", "@total_valoraduana", "F");
            Ins.Add("total_bultos_peso", "@total_bultos_peso", "F");
            Ins.Add("total_usd", "@total_usd", "F");
            Ins.Add("total_flete", "@total_flete", "F");
            Ins.Add("total_seguro", "@total_seguro", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("clave_aduana", "@clave_aduana", "F");
            Ins.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Ins.Add("clase", "@clase", "F");
            Ins.Add("mod_transporte", "@mod_transporte", "F");
            Ins.Add("total_liquidar", "@total_liquidar", "F");
            Ins.Add("total_general", "@total_general", "F");
            Ins.Add("idregimen", "@idregimen", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdTicket", oBeTms_ticket_pol.IdTicket));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenTmsEnc", oBeTms_ticket_pol.IdOrdenTmsEnc));
            cmd.Parameters.Add(new SqlParameter("@NoPoliza", oBeTms_ticket_pol.NoPoliza));
            cmd.Parameters.Add(new SqlParameter("@dua", oBeTms_ticket_pol.Dua));
            cmd.Parameters.Add(new SqlParameter("@fecha_poliza", oBeTms_ticket_pol.Fecha_poliza));
            cmd.Parameters.Add(new SqlParameter("@pais_procede", oBeTms_ticket_pol.Pais_procede));
            cmd.Parameters.Add(new SqlParameter("@tipo_cambio", oBeTms_ticket_pol.Tipo_cambio));
            cmd.Parameters.Add(new SqlParameter("@total_valoraduana", oBeTms_ticket_pol.Total_valoraduana));
            cmd.Parameters.Add(new SqlParameter("@total_bultos_peso", oBeTms_ticket_pol.Total_bultos_peso));
            cmd.Parameters.Add(new SqlParameter("@total_usd", oBeTms_ticket_pol.Total_usd));
            cmd.Parameters.Add(new SqlParameter("@total_flete", oBeTms_ticket_pol.Total_flete));
            cmd.Parameters.Add(new SqlParameter("@total_seguro", oBeTms_ticket_pol.Total_seguro));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTms_ticket_pol.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTms_ticket_pol.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTms_ticket_pol.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTms_ticket_pol.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@clave_aduana", oBeTms_ticket_pol.Clave_aduana));
            cmd.Parameters.Add(new SqlParameter("@nit_imp_exp", oBeTms_ticket_pol.Nit_imp_exp));
            cmd.Parameters.Add(new SqlParameter("@clase", oBeTms_ticket_pol.Clase));
            cmd.Parameters.Add(new SqlParameter("@mod_transporte", oBeTms_ticket_pol.Mod_transporte));
            cmd.Parameters.Add(new SqlParameter("@total_liquidar", oBeTms_ticket_pol.Total_liquidar));
            cmd.Parameters.Add(new SqlParameter("@total_general", oBeTms_ticket_pol.Total_general));
            cmd.Parameters.Add(new SqlParameter("@IdRegimen", oBeTms_ticket_pol.IdRegimen));
            cmd.Parameters.Add(new SqlParameter("@Codigo_Barra", oBeTms_ticket_pol.Codigo_Barra));

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

    public static int Actualizar(clsBeTms_ticket_pol oBeTms_ticket_pol, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("tms_ticket_pol");
            Upd.Add("idticket", "@idticket", "F");
            Upd.Add("idordentmsenc", "@idordentmsenc", "F");
            Upd.Add("nopoliza", "@nopoliza", "F");
            Upd.Add("dua", "@dua", "F");
            Upd.Add("fecha_poliza", "@fecha_poliza", "F");
            Upd.Add("pais_procede", "@pais_procede", "F");
            Upd.Add("tipo_cambio", "@tipo_cambio", "F");
            Upd.Add("total_valoraduana", "@total_valoraduana", "F");
            Upd.Add("total_bultos_peso", "@total_bultos_peso", "F");
            Upd.Add("total_usd", "@total_usd", "F");
            Upd.Add("total_flete", "@total_flete", "F");
            Upd.Add("total_seguro", "@total_seguro", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("clave_aduana", "@clave_aduana", "F");
            Upd.Add("nit_imp_exp", "@nit_imp_exp", "F");
            Upd.Add("clase", "@clase", "F");
            Upd.Add("mod_transporte", "@mod_transporte", "F");
            Upd.Add("total_liquidar", "@total_liquidar", "F");
            Upd.Add("total_general", "@total_general", "F");
            Upd.Add("idregimen", "@idregimen", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Where("IdTicket = @IdTicket AND IdOrdenTmsEnc = @IdOrdenTmsEnc");

            string sp = Upd.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdTicket", oBeTms_ticket_pol.IdTicket));
            cmd.Parameters.Add(new SqlParameter("@IdOrdenTmsEnc", oBeTms_ticket_pol.IdOrdenTmsEnc));
            cmd.Parameters.Add(new SqlParameter("@NoPoliza", oBeTms_ticket_pol.NoPoliza));
            cmd.Parameters.Add(new SqlParameter("@dua", oBeTms_ticket_pol.Dua));
            cmd.Parameters.Add(new SqlParameter("@fecha_poliza", oBeTms_ticket_pol.Fecha_poliza));
            cmd.Parameters.Add(new SqlParameter("@pais_procede", oBeTms_ticket_pol.Pais_procede));
            cmd.Parameters.Add(new SqlParameter("@tipo_cambio", oBeTms_ticket_pol.Tipo_cambio));
            cmd.Parameters.Add(new SqlParameter("@total_valoraduana", oBeTms_ticket_pol.Total_valoraduana));
            cmd.Parameters.Add(new SqlParameter("@total_bultos_peso", oBeTms_ticket_pol.Total_bultos_peso));
            cmd.Parameters.Add(new SqlParameter("@total_usd", oBeTms_ticket_pol.Total_usd));
            cmd.Parameters.Add(new SqlParameter("@total_flete", oBeTms_ticket_pol.Total_flete));
            cmd.Parameters.Add(new SqlParameter("@total_seguro", oBeTms_ticket_pol.Total_seguro));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTms_ticket_pol.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTms_ticket_pol.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTms_ticket_pol.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTms_ticket_pol.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@clave_aduana", oBeTms_ticket_pol.Clave_aduana));
            cmd.Parameters.Add(new SqlParameter("@nit_imp_exp", oBeTms_ticket_pol.Nit_imp_exp));
            cmd.Parameters.Add(new SqlParameter("@clase", oBeTms_ticket_pol.Clase));
            cmd.Parameters.Add(new SqlParameter("@mod_transporte", oBeTms_ticket_pol.Mod_transporte));
            cmd.Parameters.Add(new SqlParameter("@total_liquidar", oBeTms_ticket_pol.Total_liquidar));
            cmd.Parameters.Add(new SqlParameter("@total_general", oBeTms_ticket_pol.Total_general));
            cmd.Parameters.Add(new SqlParameter("@IdRegimen", oBeTms_ticket_pol.IdRegimen));
            cmd.Parameters.Add(new SqlParameter("@Codigo_Barra", oBeTms_ticket_pol.Codigo_Barra));

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

    public int Eliminar(IConfiguration config, clsBeTms_ticket_pol oBeTms_ticket_pol, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Tms_ticket_pol" +
             "  Where(IdTicket = @IdTicket)" +
             "  And (IdOrdenTmsEnc = @IdOrdenTmsEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTicket", oBeTms_ticket_pol.IdTicket));

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
            const string sp = "Select * FROM Tms_ticket_pol";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTms_ticket_pol pBeTms_ticket_pol)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Tms_ticket_pol" +
            " Where(IdTicket = @IdTicket)" +
            " And (IdOrdenTmsEnc = @IdOrdenTmsEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTicket", pBeTms_ticket_pol.IdTicket));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOrdenTmsEnc", pBeTms_ticket_pol.IdOrdenTmsEnc));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTms_ticket_pol, r);
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

    public static List<clsBeTms_ticket_pol> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTms_ticket_pol> lreturnList = new List<clsBeTms_ticket_pol>();

        try
        {
            const string sp = "Select * FROM Tms_ticket_pol";

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

                        clsBeTms_ticket_pol vBeTms_ticket_pol = new clsBeTms_ticket_pol();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTms_ticket_pol = new clsBeTms_ticket_pol();
                            Cargar(ref vBeTms_ticket_pol, dr);
                            lreturnList.Add(vBeTms_ticket_pol);
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

            const string sp = "Select ISNULL(Max(IdTicket),0) FROM Tms_ticket_pol";

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
            const string sp = "Select ISNULL(Max(IdTicket),0) FROM Tms_ticket_pol";

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

    public static clsBeTms_ticket_pol? GetSingle(int pIdTicket, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        clsBeTms_ticket_pol? result = null;

        try
        {
            string vSQL = "SELECT TOP 1 * FROM tms_ticket_pol WHERE IdTicket=@IdTicket";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeTms_ticket_pol Obj = new clsBeTms_ticket_pol();
                    Cargar(ref Obj, lRow);
                    result = Obj;
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