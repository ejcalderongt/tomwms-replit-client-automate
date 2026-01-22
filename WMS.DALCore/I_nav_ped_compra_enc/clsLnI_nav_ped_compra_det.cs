using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnI_nav_ped_compra_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();
    public static void Cargar(ref clsBeI_nav_ped_compra_det oBeI_nav_ped_compra_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }               
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeI_nav_ped_compra_det.NoEnc = GetString("NoEnc");
            oBeI_nav_ped_compra_det.No = GetString("No");
            oBeI_nav_ped_compra_det.Line_No = GetInt("Line_No");
            oBeI_nav_ped_compra_det.Type = GetString("Type");
            oBeI_nav_ped_compra_det.Description = GetString("Description");
            oBeI_nav_ped_compra_det.Description2 = GetString("Description2");
            oBeI_nav_ped_compra_det.Location_Code = GetString("Location_Code");
            oBeI_nav_ped_compra_det.Quantity = GetDouble("Quantity");
            oBeI_nav_ped_compra_det.Unit_Of_Measure_Code = GetString("Unit_Of_Measure_Code");
            oBeI_nav_ped_compra_det.Direct_Unit_Cost = GetDouble("Direct_Unit_Cost");
            oBeI_nav_ped_compra_det.Line_Amount = GetDouble("Line_Amount");
            oBeI_nav_ped_compra_det.Quantity_Received = GetDouble("Quantity_Received");
            oBeI_nav_ped_compra_det.Planed_Receipt_Date = GetDate("Planed_Receipt_Date");
            oBeI_nav_ped_compra_det.Variant_Code = GetString("Variant_Code");
            oBeI_nav_ped_compra_det.Fec_agr = GetDate("fec_agr");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);            
            throw new Exception(vMsgError);
        }
    }
    public static int Insertar(IConfiguration config, clsBeI_nav_ped_compra_det oBeI_nav_ped_compra_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_ped_compra_det");
            Ins.Add("noenc", "@noenc", "F");
            Ins.Add("no", "@no", "F");
            Ins.Add("line_no", "@line_no", "F");
            Ins.Add("type", "@type", "F");
            Ins.Add("description", "@description", "F");
            Ins.Add("description2", "@description2", "F");
            Ins.Add("location_code", "@location_code", "F");
            Ins.Add("quantity", "@quantity", "F");
            Ins.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
            Ins.Add("direct_unit_cost", "@direct_unit_cost", "F");
            Ins.Add("line_amount", "@line_amount", "F");
            Ins.Add("quantity_received", "@quantity_received", "F");
            Ins.Add("planed_receipt_date", "@planed_receipt_date", "F");
            Ins.Add("variant_code", "@variant_code", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");

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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_det.No));
            cmd.Parameters.Add(new SqlParameter("@Line_No", oBeI_nav_ped_compra_det.Line_No));
            cmd.Parameters.Add(new SqlParameter("@Type", oBeI_nav_ped_compra_det.Type));
            cmd.Parameters.Add(new SqlParameter("@Description", oBeI_nav_ped_compra_det.Description));
            cmd.Parameters.Add(new SqlParameter("@Description2", oBeI_nav_ped_compra_det.Description2));
            cmd.Parameters.Add(new SqlParameter("@Location_Code", oBeI_nav_ped_compra_det.Location_Code));
            cmd.Parameters.Add(new SqlParameter("@Quantity", oBeI_nav_ped_compra_det.Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit_Of_Measure_Code", oBeI_nav_ped_compra_det.Unit_Of_Measure_Code));
            cmd.Parameters.Add(new SqlParameter("@Direct_Unit_Cost", oBeI_nav_ped_compra_det.Direct_Unit_Cost));
            cmd.Parameters.Add(new SqlParameter("@Line_Amount", oBeI_nav_ped_compra_det.Line_Amount));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Received", oBeI_nav_ped_compra_det.Quantity_Received));
            cmd.Parameters.Add(new SqlParameter("@Planed_Receipt_Date", oBeI_nav_ped_compra_det.Planed_Receipt_Date));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det.Fec_agr));

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
    public static int Insertar(IConfiguration config, clsBeI_nav_ped_compra_det oBeI_nav_ped_compra_det)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_ped_compra_det");
            Ins.Add("noenc", "@noenc", "F");
            Ins.Add("no", "@no", "F");
            Ins.Add("line_no", "@line_no", "F");
            Ins.Add("type", "@type", "F");
            Ins.Add("description", "@description", "F");
            Ins.Add("description2", "@description2", "F");
            Ins.Add("location_code", "@location_code", "F");
            Ins.Add("quantity", "@quantity", "F");
            Ins.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
            Ins.Add("direct_unit_cost", "@direct_unit_cost", "F");
            Ins.Add("line_amount", "@line_amount", "F");
            Ins.Add("quantity_received", "@quantity_received", "F");
            Ins.Add("planed_receipt_date", "@planed_receipt_date", "F");
            Ins.Add("variant_code", "@variant_code", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_det.No));
            cmd.Parameters.Add(new SqlParameter("@Line_No", oBeI_nav_ped_compra_det.Line_No));
            cmd.Parameters.Add(new SqlParameter("@Type", oBeI_nav_ped_compra_det.Type));
            cmd.Parameters.Add(new SqlParameter("@Description", oBeI_nav_ped_compra_det.Description));
            cmd.Parameters.Add(new SqlParameter("@Description2", oBeI_nav_ped_compra_det.Description2));
            cmd.Parameters.Add(new SqlParameter("@Location_Code", oBeI_nav_ped_compra_det.Location_Code));
            cmd.Parameters.Add(new SqlParameter("@Quantity", oBeI_nav_ped_compra_det.Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit_Of_Measure_Code", oBeI_nav_ped_compra_det.Unit_Of_Measure_Code));
            cmd.Parameters.Add(new SqlParameter("@Direct_Unit_Cost", oBeI_nav_ped_compra_det.Direct_Unit_Cost));
            cmd.Parameters.Add(new SqlParameter("@Line_Amount", oBeI_nav_ped_compra_det.Line_Amount));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Received", oBeI_nav_ped_compra_det.Quantity_Received));
            cmd.Parameters.Add(new SqlParameter("@Planed_Receipt_Date", oBeI_nav_ped_compra_det.Planed_Receipt_Date));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det.Fec_agr));

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
    public static int Actualizar(IConfiguration config, clsBeI_nav_ped_compra_det oBeI_nav_ped_compra_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("i_nav_ped_compra_det");
            Upd.Add("noenc", "@noenc", "F");
            Upd.Add("no", "@no", "F");
            Upd.Add("line_no", "@line_no", "F");
            Upd.Add("type", "@type", "F");
            Upd.Add("description", "@description", "F");
            Upd.Add("description2", "@description2", "F");
            Upd.Add("location_code", "@location_code", "F");
            Upd.Add("quantity", "@quantity", "F");
            Upd.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
            Upd.Add("direct_unit_cost", "@direct_unit_cost", "F");
            Upd.Add("line_amount", "@line_amount", "F");
            Upd.Add("quantity_received", "@quantity_received", "F");
            Upd.Add("planed_receipt_date", "@planed_receipt_date", "F");
            Upd.Add("variant_code", "@variant_code", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Where("NoEnc = @NoEnc " +
                " AND No = @No" +
                " AND Line_No = @Line_No");

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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_det.No));
            cmd.Parameters.Add(new SqlParameter("@Line_No", oBeI_nav_ped_compra_det.Line_No));
            cmd.Parameters.Add(new SqlParameter("@Type", oBeI_nav_ped_compra_det.Type));
            cmd.Parameters.Add(new SqlParameter("@Description", oBeI_nav_ped_compra_det.Description));
            cmd.Parameters.Add(new SqlParameter("@Description2", oBeI_nav_ped_compra_det.Description2));
            cmd.Parameters.Add(new SqlParameter("@Location_Code", oBeI_nav_ped_compra_det.Location_Code));
            cmd.Parameters.Add(new SqlParameter("@Quantity", oBeI_nav_ped_compra_det.Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit_Of_Measure_Code", oBeI_nav_ped_compra_det.Unit_Of_Measure_Code));
            cmd.Parameters.Add(new SqlParameter("@Direct_Unit_Cost", oBeI_nav_ped_compra_det.Direct_Unit_Cost));
            cmd.Parameters.Add(new SqlParameter("@Line_Amount", oBeI_nav_ped_compra_det.Line_Amount));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Received", oBeI_nav_ped_compra_det.Quantity_Received));
            cmd.Parameters.Add(new SqlParameter("@Planed_Receipt_Date", oBeI_nav_ped_compra_det.Planed_Receipt_Date));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det.Fec_agr));

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
    public int Eliminar(IConfiguration config, clsBeI_nav_ped_compra_det oBeI_nav_ped_compra_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from I_nav_ped_compra_det" +
             "  Where(NoEnc = @NoEnc)" +
             "  And (No = @No)" +
             "  And (Line_No = @Line_No)");

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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det.NoEnc));

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
            const string sp = "Select * FROM I_nav_ped_compra_det";
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
    public static bool GetSingle(IConfiguration config, ref clsBeI_nav_ped_compra_det pBeI_nav_ped_compra_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM I_nav_ped_compra_det" +
            " Where(NoEnc = @NoEnc)" +
            " And (No = @No)" +
            " And (Line_No = @Line_No)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@NoEnc", pBeI_nav_ped_compra_det.NoEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@No", pBeI_nav_ped_compra_det.No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Line_No", pBeI_nav_ped_compra_det.Line_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Type", pBeI_nav_ped_compra_det.Type));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Description", pBeI_nav_ped_compra_det.Description));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Description2", pBeI_nav_ped_compra_det.Description2));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Location_Code", pBeI_nav_ped_compra_det.Location_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Quantity", pBeI_nav_ped_compra_det.Quantity));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Unit_Of_Measure_Code", pBeI_nav_ped_compra_det.Unit_Of_Measure_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Direct_Unit_Cost", pBeI_nav_ped_compra_det.Direct_Unit_Cost));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Line_Amount", pBeI_nav_ped_compra_det.Line_Amount));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Quantity_Received", pBeI_nav_ped_compra_det.Quantity_Received));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Planed_Receipt_Date", pBeI_nav_ped_compra_det.Planed_Receipt_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Variant_Code", pBeI_nav_ped_compra_det.Variant_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeI_nav_ped_compra_det.Fec_agr));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeI_nav_ped_compra_det, r);
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
    public static List<clsBeI_nav_ped_compra_det> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeI_nav_ped_compra_det> lreturnList = new List<clsBeI_nav_ped_compra_det>();

        try
        {
            const string sp = "Select * FROM I_nav_ped_compra_det";

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

                        clsBeI_nav_ped_compra_det vBeI_nav_ped_compra_det = new clsBeI_nav_ped_compra_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeI_nav_ped_compra_det = new clsBeI_nav_ped_compra_det();
                            Cargar(ref vBeI_nav_ped_compra_det, dr);
                            lreturnList.Add(vBeI_nav_ped_compra_det);
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

            const string sp = "Select ISNULL(Max(NoEnc),0) FROM I_nav_ped_compra_det";

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
    public static int MaxID(IConfiguration config,  SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(NoEnc),0) FROM I_nav_ped_compra_det";

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

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
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

    public static bool Exist(clsBeI_nav_ped_compra_det pBeI_nav_ped_compra_det,
                             SqlConnection pConnection,
                             SqlTransaction pTransaction)
    {
        bool exist = false;

        try
        {
            if (pBeI_nav_ped_compra_det.No != null)
            {                
                string vSQL = @"SELECT No 
                                FROM I_nav_ped_compra_det 
                                WHERE (NoEnc = @NoEnc AND Line_No = @Line_No AND No = @No)";

                using (var lDTA = new SqlDataAdapter(vSQL, pConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@NoEnc", pBeI_nav_ped_compra_det.NoEnc));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@Line_No", pBeI_nav_ped_compra_det.Line_No));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@No", pBeI_nav_ped_compra_det.No));
                    lDTA.SelectCommand.Transaction = pTransaction;

                    var lDT = new DataTable();
                    lDTA.Fill(lDT);

                    exist = lDT.Rows.Count > 0;
                }
            }
            else
            {
                throw new Exception(string.Format(
                    "La línea de detalle para la orden de compra: {0}, no tiene # de línea",
                    pBeI_nav_ped_compra_det.NoEnc));
            }
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }

        return exist;
    }
}