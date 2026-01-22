using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class clsLnI_nav_ped_compra_det_lote
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeI_nav_ped_compra_det_lote oBeI_nav_ped_compra_det_lote, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col)
        {
            return dr[col] is DBNull ? 0d : Convert.ToDouble(dr[col]);
        }


        try
        {
            oBeI_nav_ped_compra_det_lote.NoEnc = GetString("NoEnc");
            oBeI_nav_ped_compra_det_lote.Source_ID = GetString("source_ID");
            oBeI_nav_ped_compra_det_lote.Source_Prod_Order_Line = GetInt("Source_Prod_Order_Line");
            oBeI_nav_ped_compra_det_lote.Item_No = GetString("Item_No");
            oBeI_nav_ped_compra_det_lote.Lot_No = GetString("Lot_No");
            oBeI_nav_ped_compra_det_lote.Expiration_Date = GetDate("Expiration_Date");
            oBeI_nav_ped_compra_det_lote.Entry_No = GetString("Entry_No");
            oBeI_nav_ped_compra_det_lote.Source_Type = GetInt("Source_Type");
            oBeI_nav_ped_compra_det_lote.Quantity_Base = GetDouble("Quantity_Base");
            oBeI_nav_ped_compra_det_lote.Variant_Code = GetString("Variant_Code");
            oBeI_nav_ped_compra_det_lote.Fec_agr = GetDate("fec_agr");
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

    public static int Insertar(IConfiguration config, clsBeI_nav_ped_compra_det_lote oBeI_nav_ped_compra_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_ped_compra_det_lote");
            Ins.Add("noenc", "@noenc", "F");
            Ins.Add("source_id", "@source_id", "F");
            Ins.Add("source_prod_order_line", "@source_prod_order_line", "F");
            Ins.Add("item_no", "@item_no", "F");
            Ins.Add("lot_no", "@lot_no", "F");
            Ins.Add("expiration_date", "@expiration_date", "F");
            Ins.Add("entry_no", "@entry_no", "F");
            Ins.Add("source_type", "@source_type", "F");
            Ins.Add("quantity_base", "@quantity_base", "F");
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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det_lote.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@source_ID", oBeI_nav_ped_compra_det_lote.Source_ID));
            cmd.Parameters.Add(new SqlParameter("@Source_Prod_Order_Line", oBeI_nav_ped_compra_det_lote.Source_Prod_Order_Line));
            cmd.Parameters.Add(new SqlParameter("@Item_No", oBeI_nav_ped_compra_det_lote.Item_No));
            cmd.Parameters.Add(new SqlParameter("@Lot_No", oBeI_nav_ped_compra_det_lote.Lot_No));
            cmd.Parameters.Add(new SqlParameter("@Expiration_Date", oBeI_nav_ped_compra_det_lote.Expiration_Date));
            cmd.Parameters.Add(new SqlParameter("@Entry_No", oBeI_nav_ped_compra_det_lote.Entry_No));
            cmd.Parameters.Add(new SqlParameter("@Source_Type", oBeI_nav_ped_compra_det_lote.Source_Type));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Base", oBeI_nav_ped_compra_det_lote.Quantity_Base));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det_lote.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det_lote.Fec_agr));

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

    public static int Insertar(IConfiguration config, clsBeI_nav_ped_compra_det_lote oBeI_nav_ped_compra_det_lote)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_ped_compra_det_lote");
            Ins.Add("noenc", "@noenc", "F");
            Ins.Add("source_id", "@source_id", "F");
            Ins.Add("source_prod_order_line", "@source_prod_order_line", "F");
            Ins.Add("item_no", "@item_no", "F");
            Ins.Add("lot_no", "@lot_no", "F");
            Ins.Add("expiration_date", "@expiration_date", "F");
            Ins.Add("entry_no", "@entry_no", "F");
            Ins.Add("source_type", "@source_type", "F");
            Ins.Add("quantity_base", "@quantity_base", "F");
            Ins.Add("variant_code", "@variant_code", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det_lote.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@source_ID", oBeI_nav_ped_compra_det_lote.Source_ID));
            cmd.Parameters.Add(new SqlParameter("@Source_Prod_Order_Line", oBeI_nav_ped_compra_det_lote.Source_Prod_Order_Line));
            cmd.Parameters.Add(new SqlParameter("@Item_No", oBeI_nav_ped_compra_det_lote.Item_No));
            cmd.Parameters.Add(new SqlParameter("@Lot_No", oBeI_nav_ped_compra_det_lote.Lot_No));
            cmd.Parameters.Add(new SqlParameter("@Expiration_Date", oBeI_nav_ped_compra_det_lote.Expiration_Date));
            cmd.Parameters.Add(new SqlParameter("@Entry_No", oBeI_nav_ped_compra_det_lote.Entry_No));
            cmd.Parameters.Add(new SqlParameter("@Source_Type", oBeI_nav_ped_compra_det_lote.Source_Type));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Base", oBeI_nav_ped_compra_det_lote.Quantity_Base));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det_lote.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det_lote.Fec_agr));

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

    public static int Actualizar(IConfiguration config, clsBeI_nav_ped_compra_det_lote oBeI_nav_ped_compra_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("i_nav_ped_compra_det_lote");
            Upd.Add("noenc", "@noenc", "F");
            Upd.Add("source_id", "@source_id", "F");
            Upd.Add("source_prod_order_line", "@source_prod_order_line", "F");
            Upd.Add("item_no", "@item_no", "F");
            Upd.Add("lot_no", "@lot_no", "F");
            Upd.Add("expiration_date", "@expiration_date", "F");
            Upd.Add("entry_no", "@entry_no", "F");
            Upd.Add("source_type", "@source_type", "F");
            Upd.Add("quantity_base", "@quantity_base", "F");
            Upd.Add("variant_code", "@variant_code", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Where("NoEnc = @NoEnc" +
                " AND Source_Prod_Order_Line = @Source_Prod_Order_Line " +
                " AND Item_No = @Item_No");

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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det_lote.NoEnc));
            cmd.Parameters.Add(new SqlParameter("@source_ID", oBeI_nav_ped_compra_det_lote.Source_ID));
            cmd.Parameters.Add(new SqlParameter("@Source_Prod_Order_Line", oBeI_nav_ped_compra_det_lote.Source_Prod_Order_Line));
            cmd.Parameters.Add(new SqlParameter("@Item_No", oBeI_nav_ped_compra_det_lote.Item_No));
            cmd.Parameters.Add(new SqlParameter("@Lot_No", oBeI_nav_ped_compra_det_lote.Lot_No));
            cmd.Parameters.Add(new SqlParameter("@Expiration_Date", oBeI_nav_ped_compra_det_lote.Expiration_Date));
            cmd.Parameters.Add(new SqlParameter("@Entry_No", oBeI_nav_ped_compra_det_lote.Entry_No));
            cmd.Parameters.Add(new SqlParameter("@Source_Type", oBeI_nav_ped_compra_det_lote.Source_Type));
            cmd.Parameters.Add(new SqlParameter("@Quantity_Base", oBeI_nav_ped_compra_det_lote.Quantity_Base));
            cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det_lote.Variant_Code));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_det_lote.Fec_agr));

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

    public int Eliminar(IConfiguration config, clsBeI_nav_ped_compra_det_lote oBeI_nav_ped_compra_det_lote, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from I_nav_ped_compra_det_lote" +
             "  Where(NoEnc = @NoEnc)" +
             "  And (Source_Prod_Order_Line = @Source_Prod_Order_Line)" +
             "  And (Item_No = @Item_No)");

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

            cmd.Parameters.Add(new SqlParameter("@NoEnc", oBeI_nav_ped_compra_det_lote.NoEnc));

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
            const string sp = "Select * FROM I_nav_ped_compra_det_lote";
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

    public static bool GetSingle (IConfiguration config, ref clsBeI_nav_ped_compra_det_lote pBeI_nav_ped_compra_det_lote)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM I_nav_ped_compra_det_lote" +
            " Where(NoEnc = @NoEnc)" +
            " And (Source_Prod_Order_Line = @Source_Prod_Order_Line)" +
            " And (Item_No = @Item_No)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@NoEnc", pBeI_nav_ped_compra_det_lote.NoEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@source_ID", pBeI_nav_ped_compra_det_lote.Source_ID));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Source_Prod_Order_Line", pBeI_nav_ped_compra_det_lote.Source_Prod_Order_Line));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Item_No", pBeI_nav_ped_compra_det_lote.Item_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Lot_No", pBeI_nav_ped_compra_det_lote.Lot_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Expiration_Date", pBeI_nav_ped_compra_det_lote.Expiration_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Entry_No", pBeI_nav_ped_compra_det_lote.Entry_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Source_Type", pBeI_nav_ped_compra_det_lote.Source_Type));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Quantity_Base", pBeI_nav_ped_compra_det_lote.Quantity_Base));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Variant_Code", pBeI_nav_ped_compra_det_lote.Variant_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeI_nav_ped_compra_det_lote.Fec_agr));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeI_nav_ped_compra_det_lote, r);
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

    public static List<clsBeI_nav_ped_compra_det_lote> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeI_nav_ped_compra_det_lote> lreturnList = new List<clsBeI_nav_ped_compra_det_lote>();

        try
        {
            const string sp = "Select * FROM I_nav_ped_compra_det_lote";

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

                        clsBeI_nav_ped_compra_det_lote vBeI_nav_ped_compra_det_lote = new clsBeI_nav_ped_compra_det_lote();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeI_nav_ped_compra_det_lote = new clsBeI_nav_ped_compra_det_lote();
                            Cargar(ref vBeI_nav_ped_compra_det_lote, dr);
                            lreturnList.Add(vBeI_nav_ped_compra_det_lote);
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

            const string sp = "Select ISNULL(Max(NoEnc),0) FROM I_nav_ped_compra_det_lote";

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
    public static int MaxID( IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(NoEnc),0) FROM I_nav_ped_compra_det_lote";

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

    public static bool Exist(clsBeI_nav_ped_compra_det_lote pBeI_nav_ped_transf_det_lote,
                             SqlConnection pConnection,
                             SqlTransaction pTransaction)
    {
        bool exist = false;

        try
        {
            if (pBeI_nav_ped_transf_det_lote.Lot_No != null)
            {
                string vSQL = @"
                    SELECT Lot_No
                    FROM I_nav_ped_compra_det_lote
                    WHERE NoEnc = @NoEnc
                      AND Source_Prod_Order_Line = @Line_No
                      AND Item_No = @Item_No
                      AND Lot_No = @Lote_No";

                using (var lDTA = new SqlDataAdapter(vSQL, pConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@NoEnc", pBeI_nav_ped_transf_det_lote.NoEnc));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@Line_No", pBeI_nav_ped_transf_det_lote.Source_Prod_Order_Line));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@Item_No", pBeI_nav_ped_transf_det_lote.Item_No));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@Lote_No", pBeI_nav_ped_transf_det_lote.Lot_No));
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
                    pBeI_nav_ped_transf_det_lote.NoEnc));
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