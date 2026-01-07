using System.Data;
using System.Diagnostics;
using System.Reflection;
using AppGlobal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.DALCore.I_nav_barras_pallet;
using WMS.EntityCore;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.I_nav_Ped_Compra;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Proveedor;
using WMS.EntityCore.Stock;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;

public class clsLnI_nav_ped_compra_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeI_nav_ped_compra_enc.No = GetString("No");
            oBeI_nav_ped_compra_enc.Buy_From_Vendor_No = GetString("Buy_From_Vendor_No");
            oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name = GetString("Buy_From_Vendor_Name");
            oBeI_nav_ped_compra_enc.Posting_Description = GetString("Posting_Description");
            oBeI_nav_ped_compra_enc.Posting_Date = GetDate("Posting_Date");
            oBeI_nav_ped_compra_enc.Order_Date = GetDate("Order_Date");
            oBeI_nav_ped_compra_enc.Document_Date = GetDate("Document_Date");
            oBeI_nav_ped_compra_enc.Vendor_Invoice_No = GetString("Vendor_Invoice_No");
            oBeI_nav_ped_compra_enc.Status = GetString("Status");
            oBeI_nav_ped_compra_enc.Payment_Terms_Code = GetString("Payment_Terms_Code");
            oBeI_nav_ped_compra_enc.Ship_To_Name = GetString("Ship_To_Name");
            oBeI_nav_ped_compra_enc.Location_Code = GetString("Location_Code");
            oBeI_nav_ped_compra_enc.Ship_To_Contact = GetString("Ship_To_Contact");
            oBeI_nav_ped_compra_enc.Expected_Receipt_Date = GetDate("Expected_Receipt_Date");
            oBeI_nav_ped_compra_enc.Is_Internal_Transfer = GetBool("Is_Internal_Transfer");
            oBeI_nav_ped_compra_enc.Product_Owner_Code = GetString("Product_Owner_Code");
            oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No = GetString("Internal_Transfer_Document_No");
            oBeI_nav_ped_compra_enc.Document_Type = GetInt("Document_Type");
            oBeI_nav_ped_compra_enc.Fec_agr = GetDate("fec_agr");
            oBeI_nav_ped_compra_enc.IsImport = GetBool("IsImport");
            oBeI_nav_ped_compra_enc.Company_Code = GetString("Company_Code");
            //oBeI_nav_ped_compra_enc.No_Document_Wms = GetInt("No_Document_Wms");
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
    public static int Insertar(IConfiguration config, clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_ped_compra_enc");
            Ins.Add("no", "@no", "F");
            Ins.Add("buy_from_vendor_no", "@buy_from_vendor_no", "F");
            Ins.Add("buy_from_vendor_name", "@buy_from_vendor_name", "F");
            Ins.Add("posting_description", "@posting_description", "F");
            Ins.Add("posting_date", "@posting_date", "F");
            Ins.Add("order_date", "@order_date", "F");
            Ins.Add("document_date", "@document_date", "F");
            Ins.Add("vendor_invoice_no", "@vendor_invoice_no", "F");
            Ins.Add("status", "@status", "F");
            Ins.Add("payment_terms_code", "@payment_terms_code", "F");
            Ins.Add("ship_to_name", "@ship_to_name", "F");
            Ins.Add("location_code", "@location_code", "F");
            Ins.Add("ship_to_contact", "@ship_to_contact", "F");
            Ins.Add("expected_receipt_date", "@expected_receipt_date", "F");
            Ins.Add("is_internal_transfer", "@is_internal_transfer", "F");
            Ins.Add("product_owner_code", "@product_owner_code", "F");
            Ins.Add("internal_transfer_document_no", "@internal_transfer_document_no", "F");
            Ins.Add("document_type", "@document_type", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("isimport", "@isimport", "F");
            Ins.Add("company_code", "@company_code", "F");
            //Ins.Add("No_Document_Wms", "@No_Document_Wms", "F");

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

            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_enc.No));
            cmd.Parameters.Add(new SqlParameter("@Buy_From_Vendor_No", oBeI_nav_ped_compra_enc.Buy_From_Vendor_No));
            cmd.Parameters.Add(new SqlParameter("@Buy_From_Vendor_Name", oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name));
            cmd.Parameters.Add(new SqlParameter("@Posting_Description", oBeI_nav_ped_compra_enc.Posting_Description));
            cmd.Parameters.Add(new SqlParameter("@Posting_Date", oBeI_nav_ped_compra_enc.Posting_Date));
            cmd.Parameters.Add(new SqlParameter("@Order_Date", oBeI_nav_ped_compra_enc.Order_Date));
            cmd.Parameters.Add(new SqlParameter("@Document_Date", oBeI_nav_ped_compra_enc.Document_Date));
            cmd.Parameters.Add(new SqlParameter("@Vendor_Invoice_No", oBeI_nav_ped_compra_enc.Vendor_Invoice_No));
            cmd.Parameters.Add(new SqlParameter("@Status", oBeI_nav_ped_compra_enc.Status));
            cmd.Parameters.Add(new SqlParameter("@Payment_Terms_Code", oBeI_nav_ped_compra_enc.Payment_Terms_Code));
            cmd.Parameters.Add(new SqlParameter("@Ship_To_Name", oBeI_nav_ped_compra_enc.Ship_To_Name));
            cmd.Parameters.Add(new SqlParameter("@Location_Code", oBeI_nav_ped_compra_enc.Location_Code));
            cmd.Parameters.Add(new SqlParameter("@Ship_To_Contact", oBeI_nav_ped_compra_enc.Ship_To_Contact));
            cmd.Parameters.Add(new SqlParameter("@Expected_Receipt_Date", oBeI_nav_ped_compra_enc.Expected_Receipt_Date));
            cmd.Parameters.Add(new SqlParameter("@Is_Internal_Transfer", oBeI_nav_ped_compra_enc.Is_Internal_Transfer));
            cmd.Parameters.Add(new SqlParameter("@Product_Owner_Code", oBeI_nav_ped_compra_enc.Product_Owner_Code));
            cmd.Parameters.Add(new SqlParameter("@Internal_Transfer_Document_No", oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No));
            cmd.Parameters.Add(new SqlParameter("@Document_Type", oBeI_nav_ped_compra_enc.Document_Type));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@IsImport", oBeI_nav_ped_compra_enc.IsImport));
            cmd.Parameters.Add(new SqlParameter("@Company_Code", oBeI_nav_ped_compra_enc.Company_Code));
            //cmd.Parameters.Add(new SqlParameter("@No_Document_Wms", oBeI_nav_ped_compra_enc.No_Document_Wms));

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
    public static int Actualizar(IConfiguration config, clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("i_nav_ped_compra_enc");
            Upd.Add("no", "@no", "F");
            Upd.Add("buy_from_vendor_no", "@buy_from_vendor_no", "F");
            Upd.Add("buy_from_vendor_name", "@buy_from_vendor_name", "F");
            Upd.Add("posting_description", "@posting_description", "F");
            Upd.Add("posting_date", "@posting_date", "F");
            Upd.Add("order_date", "@order_date", "F");
            Upd.Add("document_date", "@document_date", "F");
            Upd.Add("vendor_invoice_no", "@vendor_invoice_no", "F");
            Upd.Add("status", "@status", "F");
            Upd.Add("payment_terms_code", "@payment_terms_code", "F");
            Upd.Add("ship_to_name", "@ship_to_name", "F");
            Upd.Add("location_code", "@location_code", "F");
            Upd.Add("ship_to_contact", "@ship_to_contact", "F");
            Upd.Add("expected_receipt_date", "@expected_receipt_date", "F");
            Upd.Add("is_internal_transfer", "@is_internal_transfer", "F");
            Upd.Add("product_owner_code", "@product_owner_code", "F");
            Upd.Add("internal_transfer_document_no", "@internal_transfer_document_no", "F");
            Upd.Add("document_type", "@document_type", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("isimport", "@isimport", "F");
            Upd.Add("company_code", "@company_code", "F");
            //Upd.Add("No_Document_Wms", "@No_Document_Wms", "F");


            Upd.Where("No = @No" +
                " AND Document_Type = @Document_Type");

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

            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_enc.No));
            cmd.Parameters.Add(new SqlParameter("@Buy_From_Vendor_No", oBeI_nav_ped_compra_enc.Buy_From_Vendor_No));
            cmd.Parameters.Add(new SqlParameter("@Buy_From_Vendor_Name", oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name));
            cmd.Parameters.Add(new SqlParameter("@Posting_Description", oBeI_nav_ped_compra_enc.Posting_Description));
            cmd.Parameters.Add(new SqlParameter("@Posting_Date", oBeI_nav_ped_compra_enc.Posting_Date));
            cmd.Parameters.Add(new SqlParameter("@Order_Date", oBeI_nav_ped_compra_enc.Order_Date));
            cmd.Parameters.Add(new SqlParameter("@Document_Date", oBeI_nav_ped_compra_enc.Document_Date));
            cmd.Parameters.Add(new SqlParameter("@Vendor_Invoice_No", oBeI_nav_ped_compra_enc.Vendor_Invoice_No));
            cmd.Parameters.Add(new SqlParameter("@Status", oBeI_nav_ped_compra_enc.Status));
            cmd.Parameters.Add(new SqlParameter("@Payment_Terms_Code", oBeI_nav_ped_compra_enc.Payment_Terms_Code));
            cmd.Parameters.Add(new SqlParameter("@Ship_To_Name", oBeI_nav_ped_compra_enc.Ship_To_Name));
            cmd.Parameters.Add(new SqlParameter("@Location_Code", oBeI_nav_ped_compra_enc.Location_Code));
            cmd.Parameters.Add(new SqlParameter("@Ship_To_Contact", oBeI_nav_ped_compra_enc.Ship_To_Contact));
            cmd.Parameters.Add(new SqlParameter("@Expected_Receipt_Date", oBeI_nav_ped_compra_enc.Expected_Receipt_Date));
            cmd.Parameters.Add(new SqlParameter("@Is_Internal_Transfer", oBeI_nav_ped_compra_enc.Is_Internal_Transfer));
            cmd.Parameters.Add(new SqlParameter("@Product_Owner_Code", oBeI_nav_ped_compra_enc.Product_Owner_Code));
            cmd.Parameters.Add(new SqlParameter("@Internal_Transfer_Document_No", oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No));
            cmd.Parameters.Add(new SqlParameter("@Document_Type", oBeI_nav_ped_compra_enc.Document_Type));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeI_nav_ped_compra_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@IsImport", oBeI_nav_ped_compra_enc.IsImport));
            cmd.Parameters.Add(new SqlParameter("@Company_Code", oBeI_nav_ped_compra_enc.Company_Code));
            //cmd.Parameters.Add(new SqlParameter("@No_Document_Wms", oBeI_nav_ped_compra_enc.No_Document_Wms));
            
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
    public int Eliminar(IConfiguration config, clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from I_nav_ped_compra_enc" +
             "  Where(No = @No)" +
             "  And (Document_Type = @Document_Type)");

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

            cmd.Parameters.Add(new SqlParameter("@No", oBeI_nav_ped_compra_enc.No));

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
            const string sp = "Select * FROM I_nav_ped_compra_enc";
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
    public static bool GetSingle(IConfiguration config, ref clsBeI_nav_ped_compra_enc pBeI_nav_ped_compra_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM I_nav_ped_compra_enc" +
            " Where(No = @No)" +
            " And (Document_Type = @Document_Type)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@No", pBeI_nav_ped_compra_enc.No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Buy_From_Vendor_No", pBeI_nav_ped_compra_enc.Buy_From_Vendor_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Buy_From_Vendor_Name", pBeI_nav_ped_compra_enc.Buy_From_Vendor_Name));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Posting_Description", pBeI_nav_ped_compra_enc.Posting_Description));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Posting_Date", pBeI_nav_ped_compra_enc.Posting_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Order_Date", pBeI_nav_ped_compra_enc.Order_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Document_Date", pBeI_nav_ped_compra_enc.Document_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Vendor_Invoice_No", pBeI_nav_ped_compra_enc.Vendor_Invoice_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Status", pBeI_nav_ped_compra_enc.Status));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Payment_Terms_Code", pBeI_nav_ped_compra_enc.Payment_Terms_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Ship_To_Name", pBeI_nav_ped_compra_enc.Ship_To_Name));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Location_Code", pBeI_nav_ped_compra_enc.Location_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Ship_To_Contact", pBeI_nav_ped_compra_enc.Ship_To_Contact));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Expected_Receipt_Date", pBeI_nav_ped_compra_enc.Expected_Receipt_Date));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Is_Internal_Transfer", pBeI_nav_ped_compra_enc.Is_Internal_Transfer));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Product_Owner_Code", pBeI_nav_ped_compra_enc.Product_Owner_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Internal_Transfer_Document_No", pBeI_nav_ped_compra_enc.Internal_Transfer_Document_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Document_Type", pBeI_nav_ped_compra_enc.Document_Type));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeI_nav_ped_compra_enc.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IsImport", pBeI_nav_ped_compra_enc.IsImport));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Company_Code", pBeI_nav_ped_compra_enc.Company_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@No_Document_Wms", pBeI_nav_ped_compra_enc.No_Document_Wms));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeI_nav_ped_compra_enc, r);
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
    public static List<clsBeI_nav_ped_compra_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeI_nav_ped_compra_enc> lreturnList = new List<clsBeI_nav_ped_compra_enc>();

        try
        {
            const string sp = "Select * FROM I_nav_ped_compra_enc";

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

                        clsBeI_nav_ped_compra_enc vBeI_nav_ped_compra_enc = new clsBeI_nav_ped_compra_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeI_nav_ped_compra_enc = new clsBeI_nav_ped_compra_enc();
                            Cargar(ref vBeI_nav_ped_compra_enc, dr);
                            lreturnList.Add(vBeI_nav_ped_compra_enc);
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

            const string sp = "Select ISNULL(Max(No),0) FROM I_nav_ped_compra_enc";

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


            const string sp = "Select ISNULL(Max(No),0) FROM I_nav_ped_compra_enc";

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
    public static bool Datos_Validos(clsBeI_nav_ped_compra_enc beINavPedCompraEnc)
    {
        bool datosValidos = false;

        try
        {
            // #EJC20180810_0145AM: Validar que tenga detalle el documento.
            if (beINavPedCompraEnc.Lineas_Detalle == null)
            {
                throw new Exception("No se proporcionó el detalle del documento");
            }
            else if (beINavPedCompraEnc.Lineas_Detalle.Count == 0)
            {
                throw new Exception("No se proporcionó el detalle del documento");
            }
            else if (string.IsNullOrWhiteSpace(beINavPedCompraEnc.No))
            {
                throw new Exception("El número de documento no puede ser vacío");
            }
            else if (string.IsNullOrWhiteSpace(beINavPedCompraEnc.Product_Owner_Code))
            {
                throw new Exception("El campo Producto_Owner_Code no puede ser vacío, éste valor corresponde al código de propietario tabla -> propietarios");
            }
            else
            {
                datosValidos = true;
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

        return datosValidos;
    }
    //public static int Insert_Single_Pedido_From_ERP(IConfiguration config, clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc)
    //{
    //    int result = 0;

    //    var connectionString = config.GetConnectionString("CST");
    //    using var lConnection = new SqlConnection(connectionString);
    //    SqlTransaction? lTransaction = null;

    //    try
    //    {
    //        var BeProductoBodega = new clsBeProducto_bodega();
    //        int vContador = 0;
    //        int RegistrosAfectados = 0;
    //        bool Bodega_Es_Valida_Para_Recepcion = false;
    //        int vIdBodega = 0;

    //        lConnection.Open();
    //        lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

    //        try
    //        {
    //            // Insertar/Actualizar encabezado
    //            if (!Exist(oBeI_nav_ped_compra_enc.No, lConnection, lTransaction))
    //            {
    //                RegistrosAfectados += Insertar(config,oBeI_nav_ped_compra_enc, lConnection, lTransaction);
    //            }
    //            else
    //            {
    //                RegistrosAfectados += Actualizar(config,oBeI_nav_ped_compra_enc, lConnection, lTransaction);
    //            }

    //            vContador += 1;

    //            lTransaction.Save("Encabezado");

    //            // Insertar detalle
    //            if (oBeI_nav_ped_compra_enc.Lineas_Detalle != null)
    //            {
    //                foreach (var Det in oBeI_nav_ped_compra_enc.Lineas_Detalle)
    //                {
    //                    if (Det != null) {
    //                        try
    //                        {
    //                            // Es Producto
    //                            if ((Det.Type?.ToString()) == "2")
    //                            {
    //                                if (Det.Location_Code != null)
    //                                {
    //                                    // Validaciones para transferencias interbodega
    //                                    if (!oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
    //                                    {
    //                                        // ¿El código de cliente es válido?
    //                                        Bodega_Es_Valida_Para_Recepcion = clsLnCliente.Bodega_Es_Valida_Para_Recepcion(Det.Location_Code, lConnection, lTransaction);

    //                                        if (!Bodega_Es_Valida_Para_Recepcion)
    //                                        {
    //                                            throw new Exception(string.Format(
    //                                                "La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
    //                                                "Mantenimientos->Cliene: Verifique que exista un cliente con el código: {0}",
    //                                                Det.Location_Code, Det.No));
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        // ¿El código de bodega es válido?
    //                                        Bodega_Es_Valida_Para_Recepcion = clsLnBodega.Exists_By_Codigo(Det.Location_Code, lConnection, lTransaction);

    //                                        if (!Bodega_Es_Valida_Para_Recepcion)
    //                                        {
    //                                            throw new Exception(string.Format(
    //                                                "La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
    //                                                "Mantenimientos->Bodega: Verifique que exista una bodega con el código: {0}",
    //                                                Det.Location_Code, Det.No));
    //                                        }
    //                                    }

    //                                    if (Bodega_Es_Valida_Para_Recepcion)
    //                                    {
    //                                        vIdBodega = clsLnBodega.Get_IdBodega_By_Codigo(Det.Location_Code, lConnection, lTransaction);

    //                                        if (vIdBodega == 0)
    //                                            throw new Exception("No se pudo obtener el identificador para la bodega: " + Det.Location_Code);

    //                                        BeProductoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(Det.No ?? string.Empty,
    //                                                              vIdBodega,
    //                                                              lConnection,
    //                                                              lTransaction);

    //                                        // ¿Existe el producto en el maestro?
    //                                        if (BeProductoBodega != null)
    //                                        {
    //                                            if (Det.Quantity != Det.Quantity_Received)
    //                                            {
    //                                                if (clsLnI_nav_ped_compra_det.Exist(Det, lConnection, lTransaction))
    //                                                {
    //                                                    RegistrosAfectados += clsLnI_nav_ped_compra_det.Actualizar(config, Det, lConnection, lTransaction);
    //                                                }
    //                                                else
    //                                                {
    //                                                    RegistrosAfectados += clsLnI_nav_ped_compra_det.Insertar(config, Det, lConnection, lTransaction);
    //                                                }
    //                                            }

    //                                            // Lotes
    //                                            if (oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes != null)
    //                                            {
    //                                                if (oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes.Count > 0)
    //                                                {
    //                                                    foreach (var Lote in oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes)
    //                                                    {
    //                                                        if (!clsLnI_nav_ped_compra_det_lote.Exist(Lote, lConnection, lTransaction))
    //                                                        {
    //                                                            RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Insertar(config, Lote, lConnection, lTransaction);
    //                                                        }
    //                                                        else
    //                                                        {
    //                                                            RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Actualizar(config, Lote, lConnection, lTransaction);
    //                                                        }
    //                                                    }
    //                                                }
    //                                                else
    //                                                {
    //                                                    if (oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
    //                                                    {
    //                                                        throw new Exception("Error_202301191027A: El documento de transferencia interna no tiene definidos los lotes y fechas de vencimiento count(0). " +
    //                                                            "Is_Internal_Transfer = " + oBeI_nav_ped_compra_enc.Is_Internal_Transfer);
    //                                                    }
    //                                                }
    //                                            }
    //                                            else
    //                                            {
    //                                                if (oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes == null &&
    //                                                    oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
    //                                                {
    //                                                    throw new Exception(string.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento",
    //                                                        Det.No));
    //                                                }
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            throw new Exception(string.Format("El código de producto: {0} no existe en maestro o no está asociado a la bodega: {1}",
    //                                                Det.No, Det.Location_Code));
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        lTransaction?.Rollback("Encabezado");

    //                                        throw new Exception(string.Format("La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
    //                                            "Si es una transferencia interna: la bodega debe existir en el maestro de bodegas. " +
    //                                            "Si no es T.I. el código de bodega debe existir en maestro de clientes",
    //                                            Det.Location_Code, Det.No));
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    if (Det.No != null)
    //                                    {
    //                                        throw new Exception(string.Format(
    //                                            "No está definida bodega para producto: {0}, no se importará",
    //                                            Det.No));
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            var st = new StackTrace();
    //                            var sf = st.GetFrame(0);
    //                            MethodBase? currentMethodName = null;
    //                            if (sf != null) { currentMethodName = sf.GetMethod(); }
    //                            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);

    //                            throw new Exception(vMsgError);
    //                        }
    //                    }                        
    //                }

    //                lTransaction.Commit();
    //                result = RegistrosAfectados;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Pedido de compra sin lineas de detalle?");
    //                throw new Exception(string.Format(
    //                    "Pedido de compra No: {0} sin lineas de detalle",
    //                    oBeI_nav_ped_compra_enc.No));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            var st = new StackTrace();
    //            var sf = st.GetFrame(0);
    //            MethodBase? currentMethodName = null;
    //            if (sf != null) { currentMethodName = sf.GetMethod(); }
    //            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);

    //            throw new Exception(vMsgError);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lTransaction?.Rollback();
    //        var st = new StackTrace();
    //        var sf = st.GetFrame(0);
    //        MethodBase? currentMethodName = null;
    //        if (sf != null) { currentMethodName = sf.GetMethod(); }
    //        string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);

    //        throw new Exception(vMsgError);
    //    }
    //    finally
    //    {
    //        if (lConnection.State == ConnectionState.Open)
    //            lConnection.Close();
    //    }

    //    return result;
    //}

    public static int Insert_Single_Pedido_From_ERP(IConfiguration config,clsBeI_nav_ped_compra_enc oBeI_nav_ped_compra_enc)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(oBeI_nav_ped_compra_enc);

        int result = 0;

        var connectionString = config.GetConnectionString("CST")
            ?? throw new InvalidOperationException("Missing connection string 'CST'.");

        using var lConnection = new SqlConnection(connectionString);
        lConnection.Open();

        // Transacción con ReadCommitted y con using para asegurar Dispose()
        using var lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

        try
        {
            var BeProductoBodega = new clsBeProducto_bodega();
            int vContador = 0;
            int RegistrosAfectados = 0;
            bool Bodega_Es_Valida_Para_Recepcion = false;
            int vIdBodega = 0;

            if (!Exist(oBeI_nav_ped_compra_enc.No, lConnection, lTransaction))
            {
                RegistrosAfectados += Insertar(config, oBeI_nav_ped_compra_enc, lConnection, lTransaction);
            }
            else
            {
                RegistrosAfectados += Actualizar(config, oBeI_nav_ped_compra_enc, lConnection, lTransaction);
            }

            vContador += 1;

            lTransaction.Save("Encabezado");

            if (oBeI_nav_ped_compra_enc.Lineas_Detalle != null)
            {
                foreach (var Det in oBeI_nav_ped_compra_enc.Lineas_Detalle)
                {
                    if (Det == null) continue;

                    string locationToUse = string.IsNullOrWhiteSpace(Det.Location_Code)
                                            ? oBeI_nav_ped_compra_enc.Location_Code
                                            : Det.Location_Code;

                    try
                    {
                     
                        //tipo 2 es producto, tipo 1 es servicio, y se omite
                        if ((Det.Type?.ToString()) == "2")
                        {
                            if (Det.Location_Code != null)
                            {
                                if (!oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
                                {
                                    Bodega_Es_Valida_Para_Recepcion =
                                        clsLnCliente.Bodega_Es_Valida_Para_Recepcion(locationToUse, lConnection, lTransaction);                                    

                                    if (!Bodega_Es_Valida_Para_Recepcion)
                                    {
                                        throw new Exception(string.Format(
                                            "La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
                                            "Mantenimientos->Cliene: Verifique que exista un cliente con el código: {0}",
                                            Det.Location_Code, Det.No));
                                    }
                                }
                                else
                                {
                                    Bodega_Es_Valida_Para_Recepcion =
                                        clsLnBodega.Exists_By_Codigo(locationToUse, lConnection, lTransaction);

                                    if (!Bodega_Es_Valida_Para_Recepcion)
                                    {
                                        throw new Exception(string.Format(
                                            "La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
                                            "Mantenimientos->Bodega: Verifique que exista una bodega con el código: {0}",
                                            Det.Location_Code, Det.No));
                                    }
                                }

                                if (Bodega_Es_Valida_Para_Recepcion)
                                {
                                    vIdBodega = clsLnBodega.Get_IdBodega_By_Codigo(locationToUse, lConnection, lTransaction);
                                    if (vIdBodega == 0)
                                        throw new Exception("No se pudo obtener el identificador para la bodega: " + locationToUse);

                                    BeProductoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(
                                        Det.No ?? string.Empty, vIdBodega, lConnection, lTransaction);

                                    if (BeProductoBodega != null)
                                    {
                                        if (Det.Quantity != Det.Quantity_Received)
                                        {
                                            if (clsLnI_nav_ped_compra_det.Exist(Det, lConnection, lTransaction))
                                                RegistrosAfectados += clsLnI_nav_ped_compra_det.Actualizar(config, Det, lConnection, lTransaction);
                                            else
                                                RegistrosAfectados += clsLnI_nav_ped_compra_det.Insertar(config, Det, lConnection, lTransaction);
                                        }

                                        if (oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes != null)
                                        {
                                            if (oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes.Count > 0)
                                            {
                                                foreach (var Lote in oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes)
                                                {
                                                    if (!clsLnI_nav_ped_compra_det_lote.Exist(Lote, lConnection, lTransaction))
                                                        RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Insertar(config, Lote, lConnection, lTransaction);
                                                    else
                                                        RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Actualizar(config, Lote, lConnection, lTransaction);
                                                }
                                            }
                                            else
                                            {
                                                if (oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
                                                {
                                                    throw new Exception("Error_202301191027A: El documento de transferencia interna no tiene definidos los lotes y fechas de vencimiento count(0). " +
                                                        "Is_Internal_Transfer = " + oBeI_nav_ped_compra_enc.Is_Internal_Transfer);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
                                            {
                                                throw new Exception(string.Format(
                                                    "El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento",
                                                    Det.No));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(
                                            "El código de producto: {0} no existe en maestro o no está asociado a la bodega: {1}",
                                            Det.No, Det.Location_Code));
                                    }
                                }
                                else
                                {
                                    // Rollback parcial al savepoint y re-lanzar
                                    lTransaction.Rollback("Encabezado");

                                    throw new Exception(string.Format(
                                        "La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. " +
                                        "Si es una transferencia interna: la bodega debe existir en el maestro de bodegas. " +
                                        "Si no es T.I. el código de bodega debe existir en maestro de clientes",
                                        Det.Location_Code, Det.No));
                                }
                            }
                            else if (Det.No != null)
                            {
                                throw new Exception($"No está definida bodega para producto: {Det.No}, no se importará");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Re-lanzar con contexto sin perder stack original
                        throw new Exception("Error procesando línea de detalle.", ex);
                    }
                }

                result = RegistrosAfectados;
            }
            else
            {
                Console.WriteLine("Pedido de compra sin lineas de detalle?");
                throw new Exception($"Pedido de compra No: {oBeI_nav_ped_compra_enc.No} sin lineas de detalle");
            }

            // Si todo fue bien, commit al final
            lTransaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            // Rollback global; si ya se hizo uno parcial, esto garantiza consistencia
            try { lTransaction.Rollback(); } catch { /* opcional: log del fallo de rollback */ }
            throw new Exception($"{nameof(Insert_Single_Pedido_From_ERP)} failed.", ex);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open)
                lConnection.Close();
        }
    }


    public static bool Exist(string pNo, SqlConnection lConnection, SqlTransaction lTrans)
    {
        bool exist = false;

        try
        {
            const string sp = @"SELECT No 
                                FROM I_nav_ped_compra_enc
                                WHERE No = @No";

            using (var cmd = new SqlCommand(sp, lConnection, lTrans))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@No", pNo));

                using (var dad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    dad.Fill(dt);
                    exist = dt.Rows.Count > 0;
                }
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
    public static void Generar_Tarea_Recepcion(clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                               clsBeI_nav_config_enc? BeConfigEnc,
                                               clsBeTrans_oc_ti BeTipoDocumento,
                                               clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                               ref string lblprg,
                                               clsBeTrans_pe_enc? BePedidoEnc = null,
                                               SqlConnection? lConnection = null,
                                               SqlTransaction? lTransInterface = null)
    {
        try
        {
            clsBeTrans_re_enc? OutBeRecepcionEnc = new clsBeTrans_re_enc();

            if (lConnection != null && lTransInterface != null) {
                if (BeConfigEnc !=null)
                if (BeConfigEnc.Crear_Recepcion_De_Compra_NAV || BeTipoDocumento.Genera_tarea_ingreso)
                {
                    if (BeConfigEnc.Interface_SAP && BePedidoEnc != null)
                    {
                        clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido(ref gBeOrdenCompraEnc,
                                                                                               ref lblprg,
                                                                                               true,
                                                                                               BeConfigEnc,
                                                                                               ref OutBeRecepcionEnc,
                                                                                               ref BePedidoEnc,
                                                                                               lConnection,
                                                                                               lTransInterface);
                    }
                    else
                    {
                        clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc(ref gBeOrdenCompraEnc,
                                                                                    ref lblprg,
                                                                                    true,
                                                                                    BeConfigEnc,
                                                                                    ref OutBeRecepcionEnc,
                                                                                    lConnection,
                                                                                    lTransInterface);
                    }

                    if (OutBeRecepcionEnc != null)
                        lblprg += Environment.NewLine + "\t" + string.Format("Se creó la tarea de recepción: {0} para el documento de ingreso: {1} {2}",
                                                                              OutBeRecepcionEnc.IdRecepcionEnc,
                                                                              navPedidoCompraEnc.No,
                                                                              Environment.NewLine);
                }
                else {

                    lblprg += Environment.NewLine + "\t" + string.Format("La parametrización no habilita la tarea de recepción: {0} para el documento de ingreso: {1} {2}",
                                                                              OutBeRecepcionEnc.IdRecepcionEnc,
                                                                              navPedidoCompraEnc.No,
                                                                              Environment.NewLine);

                }
            }            
        }
        catch (Exception ex)
        {
            throw new Exception("Error en Generar_Tarea_Recepcion: " + ex.Message);
        }
    }

    public static bool InsertarDetalleOrdenCompra(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                  clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                                  clsBeProducto_bodega BeProductoBodega,
                                                  clsBeProducto_presentacion? BePresentacion,
                                                  clsBeI_nav_config_enc BeConfigEnc,
                                                  clsBeUnidad_medida? BeUnidadMedidaPedCompra,
                                                  double vCantidadEnteraPres,
                                                  double vCantidadDecimalUMBas,
                                                  ref int vContadorLineasDetInsertadas,
                                                  ref string lblprg,
                                                  ref clsBeTrans_oc_det_lote BeOcDetLote,
                                                  ref List<clsBeTrans_oc_det_lote> LotesExistentes,
                                                  clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                                  List<clsBeTrans_picking_ubic>? pDetallePickingUbic,
                                                  SqlConnection lConnection,
                                                  SqlTransaction lTransInterface)
    {
        try
        {
            clsBeTrans_oc_det BePedidoCompraDet = new clsBeTrans_oc_det()
            {
                IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc,
                IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
            };

            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega;
            BePedidoCompraDet.Codigo_producto = string.IsNullOrEmpty(navPedidoCompraDet.Barcode) ? BeProductoBodega.Producto.codigo : navPedidoCompraDet.Barcode;
            BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description;

            if (!(BeConfigEnc.Convertir_decimales_a_umbas == 1 || BeConfigEnc.Interface_SAP) && vCantidadEnteraPres > 0 && BePresentacion !=null)
            {
                BePedidoCompraDet.Cantidad = Math.Round(vCantidadEnteraPres / BePresentacion.Factor, 6);
                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion;
            }
            else
            {
                BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity;
            }

            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received;
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost;
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount;
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No;
            BePedidoCompraDet.Activo = true;
            BePedidoCompraDet.Porcentaje_arancel = 0;
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code;
            BePedidoCompraDet.IdPresentacion = (BePresentacion != null) ? BePresentacion.IdPresentacion : 0;
            BePedidoCompraDet.Presentacion.IdPresentacion = BePedidoCompraDet.IdPresentacion;

            clsBeBodega? Bebodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, lConnection, lTransInterface);

            if (Bebodega != null) {
                if (Bebodega.Control_Talla_Color)
                {
                    if (!string.IsNullOrEmpty(navPedidoCompraDet.Barcode))
                    {
                        clsBeProducto_talla_color? BeProductoTallaColor = new clsBeProducto_talla_color();
                        BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_Params(BeProductoBodega.IdProducto, navPedidoCompraDet.Size, navPedidoCompraDet.Color, lConnection, lTransInterface);

                        if (BeProductoTallaColor != null)
                        {
                            BePedidoCompraDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor;
                        }
                        else
                        {
                            lblprg += "No existe la Talla/Color definidas para el código " + navPedidoCompraDet.No + Environment.NewLine;
                            return false;
                        }
                    }
                }
            }

            if (BeUnidadMedidaPedCompra != null)
            if (Asigna_Unidad_De_Medida(BePedidoCompraDet, navPedidoCompraDet, BeUnidadMedidaPedCompra, BeProductoBodega, lConnection, lTransInterface))
            {
                if (!(BeConfigEnc.Convertir_decimales_a_umbas == 1 || BeConfigEnc.Interface_SAP) && vCantidadDecimalUMBas > 0)
                {
                    clsBeTrans_oc_det BePedidoCompraDetUmBas = new clsBeTrans_oc_det();
                    clsPublic.CopyObject(BePedidoCompraDet, ref BePedidoCompraDetUmBas);
                    BePedidoCompraDetUmBas.IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc;
                    BePedidoCompraDetUmBas.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1;
                    BePedidoCompraDetUmBas.Cantidad = vCantidadDecimalUMBas;
                    BePedidoCompraDetUmBas.IdPresentacion = 0;
                    BePedidoCompraDetUmBas.Presentacion.IdPresentacion = 0;
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDetUmBas, lConnection, lTransInterface);
                }
                else
                {
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransInterface);
                }

                if (!string.IsNullOrEmpty(navPedidoCompraEnc.Internal_Transfer_Document_No))
                {
                    if (pDetallePickingUbic != null && pDetallePickingUbic.Count > 0)
                    {
                        int lMaxIdLoteDet = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransInterface) + 1;
                        int lMaxIdPallet = clsLnI_nav_barras_pallet.MaxID(lConnection, lTransInterface) + 1;
                        clsBeTrans_re_det BeTransReDet = new clsBeTrans_re_det();
                        clsBeI_nav_barras_pallet BeINavBarraPallet = new clsBeI_nav_barras_pallet();
                        clsBeI_nav_barras_pallet BeINavBarraPalletOriginal = new clsBeI_nav_barras_pallet();
                        clsBeStock BeStock = new clsBeStock();                        

                        var lFiltroPickingUbic = pDetallePickingUbic.Where(x => x.CodigoProducto == navPedidoCompraDet.No && x.No_Linea == navPedidoCompraDet.Line_No).ToList();

                        foreach (clsBeTrans_picking_ubic BePickingUbic in lFiltroPickingUbic)
                        {
                            BeOcDetLote = new clsBeTrans_oc_det_lote();
                            BeOcDetLote.IdOrdenCompraDetLote = lMaxIdLoteDet;
                            BeOcDetLote.IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc;
                            BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet;
                            BeOcDetLote.Cantidad = BePickingUbic.Cantidad_despachada;
                            BeOcDetLote.No_linea = BePedidoCompraDet.No_Linea;
                            BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega;
                            BeOcDetLote.Lote = BePickingUbic.Lote;
                            BeOcDetLote.Lic_plate = BePickingUbic.Lic_plate;
                            BeOcDetLote.Cantidad_recibida = 0;
                            BeOcDetLote.Codigo_producto = BePedidoCompraDet.Codigo_producto;
                            BeOcDetLote.Fecha_vence = BePickingUbic.Fecha_vence;
                            BeOcDetLote.IdPresentacion = BePickingUbic.IdPresentacion;
                            BeOcDetLote.Presentacion.IdPresentacion = BePickingUbic.IdPresentacion;
                            BeOcDetLote.IdUnidadMedidaBasica = BePickingUbic.IdUnidadMedida;
                            BeOcDetLote.UnidadMedida.IdUnidadMedida = BePickingUbic.IdUnidadMedida;
                            BeOcDetLote.IdProductoTallaColor = BePickingUbic.IdProductoTallaColor;
                            BeOcDetLote.Talla = BePickingUbic.Codigo_Talla;
                            BeOcDetLote.Color = BePickingUbic.Codigo_Color;
                            BeOcDetLote.Activo = true;
                            BeOcDetLote.User_agr = BePedidoCompraDet.User_agr;
                            BeOcDetLote.User_mod = BePedidoCompraDet.User_mod;
                            clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransInterface);

                            lMaxIdLoteDet++;
                        }
                    }
                }

                vContadorLineasDetInsertadas++;

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            string vMsgEx3 = string.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", ex.Message, ex.Source, Environment.NewLine);            
            lblprg += vMsgEx3 + Environment.NewLine;
            throw new Exception(vMsgEx3);
        }
    }

    public static bool ValidarYCalcularUMBas(clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                        ref clsBeUnidad_medida? BeUnidadMedidaPedCompra,
                                        ref clsBeProducto_presentacion? BePresentacion,
                                        clsBeProducto_bodega BeProductoBodega,
                                        clsBeI_nav_config_enc BeConfigEnc,
                                        ref double vCantidadSolicitadaPedido,
                                        ref double vCantidadEnteraPres,
                                        ref double vCantidadDecimalUMBas,
                                        ref string lblprg,
                                        SqlConnection lConnection,
                                        SqlTransaction lTransInterface)
    {
        try
        {
            // Buscar UM básica por código y propietario
            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_Of_Measure_Code,
                                                                                           BeConfigEnc.IdPropietario,
                                                                                           lConnection,
                                                                                           lTransInterface);

            if (BeUnidadMedidaPedCompra != null)
            {
                // Confirmar que el producto existe con la unidad medida
                if (!clsLnProducto.Existe(navPedidoCompraDet.No, BeUnidadMedidaPedCompra.IdUnidadMedida, lConnection, lTransInterface))
                {
                    // Buscar presentación por código variante si existe
                    if (!string.IsNullOrEmpty(navPedidoCompraDet.Variant_Code))
                    {
                        BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                                 navPedidoCompraDet.Variant_Code,
                                                                                                 lConnection,
                                                                                                 lTransInterface);
                        if (BePresentacion != null)
                        {
                            BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida;
                        }
                        else
                        {
                            throw new Exception("ERROR_20220727_1228A: No se encontró la presentación asociada al código: " + navPedidoCompraDet.No +
                                            " Con código de variante: " + navPedidoCompraDet.Variant_Code + " para el IdProducto: " +
                                            BeProductoBodega.IdProducto + " en la línea " + navPedidoCompraDet.Line_No);
                        }
                    }
                }
            }
            else
            {
                if (BeProductoBodega.Producto.UnidadMedida == null)
                {
                    throw new Exception($"Producto: {navPedidoCompraDet.No} UnidMedBas No definida");
                }

                // Buscar presentación por nombre
                BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                       navPedidoCompraDet.Unit_Of_Measure_Code,
                                                                                       lConnection,
                                                                                       lTransInterface);

                if (BePresentacion != null)
                {
                    BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida;
                }
                else
                {
                    throw new Exception($"La unidad de medida: {navPedidoCompraDet.Unit_Of_Measure_Code} no está definida para el código de producto:{navPedidoCompraDet.No} en la tabla unidad_medida.");
                }
            }

            // Evaluar si se requiere conversión a UM básica
            if (BeConfigEnc.Convertir_decimales_a_umbas == 1 && BeConfigEnc.Interface_SAP)
            {
                BePresentacion = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(BeProductoBodega.IdProducto,
                                                                                                   lConnection,
                                                                                                   lTransInterface);

                if (BePresentacion != null)
                {
                    if (BePresentacion.Factor <= 0)
                    {
                        throw new Exception("ERROR_202210251745: El factor es 0 para la presentación NO se puede inferir la conversión.");
                    }

                    double v1 = Convert.ToDouble(vCantidadEnteraPres);
                    double v2 = Convert.ToDouble(vCantidadDecimalUMBas);

                    clsPublic.Split_Decimal(Convert.ToDouble(navPedidoCompraDet.Quantity / BePresentacion.Factor),
                                            ref v1,
                                            ref v2);

                    vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * BePresentacion.Factor);
                    vCantidadEnteraPres = vCantidadEnteraPres * BePresentacion.Factor;

                    vCantidadSolicitadaPedido = (vCantidadEnteraPres > 0) ? vCantidadEnteraPres : vCantidadDecimalUMBas;
                }
                else
                {
                    vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity;
                }
            }
            else
            {
                vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity;
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool InicializarEncabezadoNuevaOC(
    clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
    clsBeI_nav_config_enc? BeConfigEnc,
    ref clsBeTrans_oc_ti BeTipoDocumento,
    int IdNavConfigDet,
    ref string lblprg,
    ref clsBeTrans_oc_enc gBeOrdenCompraEnc,
    int IdBodegaDestino,
    SqlConnection lConnection,
    SqlTransaction lTransInterface)
    {
        try
        {
            if (BeConfigEnc == null)
            {
                throw new Exception("No se recibió la configuración de interfaz (BeConfigEnc es null).");
            }

            gBeOrdenCompraEnc = new clsBeTrans_oc_enc();
            gBeOrdenCompraEnc.IdBodega = IdBodegaDestino;

            gBeOrdenCompraEnc.PropietarioBodega = new clsBePropietario_bodega
            {
                IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(
                                          IdBodegaDestino,
                                          BeConfigEnc.IdPropietario,
                                          lConnection,
                                          lTransInterface)
            };

            if (gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega == 0)
            {
                throw new Exception("No se pudo obtener el IdPropietarioBodega");
            }

            gBeOrdenCompraEnc.IdPropietarioBodega = gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega;
            gBeOrdenCompraEnc.IdEstadoOC = Convert.ToInt32(clsDataContractDI.tEstadoOC.NUEVA);
            gBeOrdenCompraEnc.Hora_Creacion = DateTime.Now;
            gBeOrdenCompraEnc.User_Agr = BeConfigEnc.IdUsuario.ToString();
            gBeOrdenCompraEnc.Fec_Agr = DateTime.Now;
            gBeOrdenCompraEnc.Fecha_Creacion = DateTime.Now;
            gBeOrdenCompraEnc.Activo = true;
            gBeOrdenCompraEnc.IdCampaña = Convert.ToInt32(navPedidoCompraEnc.Campaign_No);
            gBeOrdenCompraEnc.Usr_Documento = navPedidoCompraEnc.User_Document;
            gBeOrdenCompraEnc.Serie = navPedidoCompraEnc.Series;
            gBeOrdenCompraEnc.Comentarios = navPedidoCompraEnc.Comments;

            clsBeProveedor_bodega? BeProveedorBodega =
                clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(
                    navPedidoCompraEnc.Buy_From_Vendor_No,
                    IdBodegaDestino,
                    BeConfigEnc,
                    lConnection,
                    lTransInterface);

            if (BeProveedorBodega == null)
            {
                if (!InsertaProveedor(navPedidoCompraEnc, gBeOrdenCompraEnc, BeConfigEnc,
                                      ref BeProveedorBodega, lConnection, lTransInterface))
                {
                    throw new Exception(
                        $"El proveedor: {navPedidoCompraEnc.Buy_From_Vendor_No} no existe, " +
                        $"no se puede importar el pedido de compra: {navPedidoCompraEnc.No}");
                }
            }

            if (BeProveedorBodega != null)
                gBeOrdenCompraEnc.IdProveedorBodega = BeProveedorBodega.IdAsignacion;

            BeTipoDocumento = AsignarTipoDocumentoIngreso(
                                  navPedidoCompraEnc,
                                  ref gBeOrdenCompraEnc,
                                  ref BeTipoDocumento,
                                  lConnection,
                                  lTransInterface);

            // Tu validación de Vendor_Invoice_No / No
            gBeOrdenCompraEnc.No_Documento =
                string.IsNullOrWhiteSpace(navPedidoCompraEnc.Vendor_Invoice_No)
                ? navPedidoCompraEnc.No
                : navPedidoCompraEnc.Vendor_Invoice_No;

            if (string.IsNullOrWhiteSpace(gBeOrdenCompraEnc.No_Documento))
            {
                throw new Exception("No se definió el número de documento (Vendor_Invoice_No / No) para la cabecera.");
            }

            gBeOrdenCompraEnc.User_Mod = BeConfigEnc.IdUsuario.ToString();
            gBeOrdenCompraEnc.Fec_Mod = DateTime.Now;

            gBeOrdenCompraEnc.Procedencia = BeConfigEnc.Interface_SAP
                ? navPedidoCompraEnc.Buy_From_Vendor_No + " " + navPedidoCompraEnc.Buy_From_Vendor_Name
                : navPedidoCompraEnc.Ship_To_Contact;

            gBeOrdenCompraEnc.No_Marchamo = "";
            gBeOrdenCompraEnc.Referencia = navPedidoCompraEnc.No;

            gBeOrdenCompraEnc.No_Documento_Recepcion_ERP = BeConfigEnc.Interface_SAP
                ? (!string.IsNullOrWhiteSpace(navPedidoCompraEnc.Internal_Transfer_Document_No)
                    ? navPedidoCompraEnc.Internal_Transfer_Document_No
                    : navPedidoCompraEnc.Ship_To_Contact)
                : "";

            gBeOrdenCompraEnc.Observacion = navPedidoCompraEnc.Posting_Description;
            gBeOrdenCompraEnc.Control_Poliza = false;

            if (gBeOrdenCompraEnc.IsNew)
                gBeOrdenCompraEnc.ObjPoliza = null;

            gBeOrdenCompraEnc.Enviado_A_ERP = false;
            gBeOrdenCompraEnc.Codigo_Empresa_ERP = navPedidoCompraEnc.Company_Code;
            gBeOrdenCompraEnc.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(lConnection, lTransInterface) + 1;

            clsLnTrans_oc_enc.Insertar(gBeOrdenCompraEnc, lConnection, lTransInterface);

            return true;
        }
        catch (Exception ex)
        {
            // si quieres propagar: throw;  (pero entonces la firma bool pierde sentido)
            lblprg = $"Error al inicializar encabezado de OC: {ex.Message}";
            return false;
        }
    }

    public static bool CrearYGuardarDetalleOC(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                              clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                              ref clsBeTrans_oc_det? BePedidoCompraDet,
                                              clsBeProducto_bodega? BeProductoBodega,
                                              clsBeUnidad_medida? BeUnidadMedidaPedCompra,
                                              clsBeProducto_presentacion BePresentacion,
                                              clsBeTrans_oc_enc PedidoCompraExistente,
                                              ref int VContadorBitacoraTOMWMS,
                                              int IdNavConfigDet,
                                              clsBeI_nav_config_enc BeConfigEnc,
                                              ref string lblprg,
                                              List<clsBeTrans_oc_det_lote> LotesExistentes,
                                              ref clsBeTrans_oc_det_lote BeOcDetLote,
                                              SqlConnection lConnection,
                                              SqlTransaction lTransInterface)
    {
        try
        {

            if (BeProductoBodega == null)
            {
                throw new Exception("El producto bodega es nulo");
            }

            BePedidoCompraDet = new clsBeTrans_oc_det
            {
                IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc,
                IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(PedidoCompraExistente.IdOrdenCompraEnc, lConnection, lTransInterface) + 1,
                IdProductoBodega = BeProductoBodega.IdProductoBodega
            };

            if (BePresentacion != null && BePresentacion.IdPresentacion !=0)
            {
                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion;                
                BePedidoCompraDet.Presentacion ??= new clsBeProducto_presentacion();
                BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion;
            }
            else
            {
                BePedidoCompraDet.IdPresentacion = 0;
            }

            BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description;

            BePedidoCompraDet.Nombre_unidad_medida_basica =
                string.IsNullOrEmpty(navPedidoCompraDet.Variant_Code)
                    ? navPedidoCompraDet.Unit_Of_Measure_Code
                    : (BeProductoBodega?.Producto?.UnidadMedida?.Nombre ?? navPedidoCompraDet.Unit_Of_Measure_Code);

            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity;
            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received;
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost;
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount;
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No;
            BePedidoCompraDet.Activo = true;
            BePedidoCompraDet.Porcentaje_arancel = 0;
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code;


            clsBeBodega? BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega,lConnection,lTransInterface);

            if (BeBodega != null) {
                if (BeBodega.Control_Talla_Color) {
                    if (!string.IsNullOrEmpty(navPedidoCompraDet.Barcode))
                    {
                        clsBeProducto_talla_color? BeProductoTallaColor = new clsBeProducto_talla_color();
                        if (BeProductoBodega != null && BeProductoBodega.IdProducto > 0)
                        {
                            BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_Params(BeProductoBodega.IdProducto, navPedidoCompraDet.Size, navPedidoCompraDet.Color, lConnection, lTransInterface);
                            if (BeProductoTallaColor != null)
                            {
                                BePedidoCompraDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor;
                            }
                            else
                            {
                                lblprg += "No existe la Talla/Color definidas para el código " + navPedidoCompraDet.No + Environment.NewLine;
                                return false;
                            }
                        }
                    }
                }
            }            

            if (BeProductoBodega !=null)
            if (Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                        navPedidoCompraDet,
                                        BeUnidadMedidaPedCompra,
                                        BeProductoBodega,
                                        lConnection,
                                        lTransInterface))
            {
                clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransInterface);

                ProcesarLotes(navPedidoCompraEnc,
                              navPedidoCompraDet,
                              ref BeOcDetLote,
                              BePedidoCompraDet,
                              LotesExistentes,
                              lConnection,
                              lTransInterface);

                VContadorBitacoraTOMWMS += 1;

                return true;
            }
        }
        catch (Exception ex)
        {            
            lblprg += string.Format("Error al insertar Detalle en : {0}{1}", ex.Message, Environment.NewLine);
            lblprg += Environment.NewLine;
        }

        return false;
    }

        public static bool Asigna_Unidad_De_Medida(clsBeTrans_oc_det BePedidoCompraDet,
                                                   clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                                   clsBeUnidad_medida? BeUnidadMedidaPedCompra,
                                                   clsBeProducto_bodega BeProductoBodega,
                                                   SqlConnection lConnection,
                                                   SqlTransaction lTransaction)
    {
        try
        {

            if (BeUnidadMedidaPedCompra == null) throw new Exception("Error: El objeto Unidad de Medida esta vacio.");

            if (!clsLnProducto.Existe(navPedidoCompraDet.No,
                                      BeUnidadMedidaPedCompra.IdUnidadMedida,
                                      lConnection,
                                      lTransaction))
            {
                clsBeProducto_presentacion? BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                                                navPedidoCompraDet.Variant_Code,
                                                                                                                lConnection,
                                                                                                                lTransaction);

                if (BePresentacion != null)
                {
                    BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion;
                    BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion;
                    BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica;
                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica;
                    BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre;
                }
                else
                {
                    double vFactorConv = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                                  BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                                  lConnection,
                                                                                  lTransaction);

                    if (vFactorConv > 0)
                    {
                        // Se desactiva la creación automática de presentaciones. 
                        throw new Exception("ERROR_20220727_1228C: No se encontró la presentación asociada al código: " +
                                        navPedidoCompraDet.No + " Con código variante: " + navPedidoCompraDet.Variant_Code);
                    }
                    else
                    {
                        throw new Exception(string.Format("Error: No existe factor en unidad_medida_conversion para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                                      navPedidoCompraDet.No,
                                                      BeProductoBodega.Producto.UnidadMedida.Nombre,
                                                      navPedidoCompraDet.Unit_Of_Measure_Code));
                    }
                }
            }
            else
            {
                BePedidoCompraDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida;
                BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida;
                BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_Of_Measure_Code;
            }

            return true;
        }
        catch (Exception)
        {         
            throw;
        }
    }

    public static void ValidarPresentaciones(clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                             clsBeProducto_bodega BeProductoBodega,
                                             ref clsBeProducto_presentacion BePresentacion,
                                             SqlConnection lConnection,
                                             SqlTransaction lTransInterface)
    {
        try
        {
            // Validación por código de unidad de medida (nombre)
            var presentacionPorNombre = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                                 navPedidoCompraDet.Unit_Of_Measure_Code,
                                                                                                 lConnection,
                                                                                                 lTransInterface);

            if (presentacionPorNombre != null)
            {
                BePresentacion = presentacionPorNombre;
            }

            // Validación por variant_code si aplica
            if (!string.IsNullOrWhiteSpace(navPedidoCompraDet.Variant_Code))
            {
                var presentacionPorCodigo = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                                     navPedidoCompraDet.Variant_Code,
                                                                                                     lConnection,
                                                                                                     lTransInterface);

                if (presentacionPorCodigo != null)
                {
                    BePresentacion = presentacionPorCodigo;
                }
                else
                {
                    throw new Exception("ERROR_20220727_1228E: No se encontró la presentación asociada al código: " +
                                        navPedidoCompraDet.No + " Con código variante: " + navPedidoCompraDet.Variant_Code);
                }
            }

            // Verificar si BePresentacion sigue siendo null después de las validaciones
            if (BePresentacion == null)
            {
                throw new Exception("No se encontró una presentación válida para el producto.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeI_nav_config_enc ObtenerConfiguracionDeBodega(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                                     ref clsBeI_nav_config_enc? BeConfigEnc,
                                                                     ref clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                                                     int IdBodegaDestino,
                                                                     int vIdEmpresa,
                                                                     SqlConnection? lConnection,
                                                                     SqlTransaction? lTransInterface)
    {
        try
        {
            if (lConnection != null && lTransInterface != null) {

                BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(gBeOrdenCompraEnc.IdBodega,
                                                                                         vIdEmpresa,
                                                                                         lConnection,
                                                                                         lTransInterface);

                if (BeConfigEnc == null)
                    throw new Exception("20251202: La configuración de interface para la bodega no fue definida");

                BeConfigEnc.IdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(navPedidoCompraEnc.Product_Owner_Code,
                                                                                      lConnection,
                                                                                      lTransInterface);

                if (BeConfigEnc.IdPropietario == 0)
                    throw new Exception($"No se pudo obtener el Identificador de propietario para: {navPedidoCompraEnc.Product_Owner_Code}");

                gBeOrdenCompraEnc.PropietarioBodega = new clsBePropietario_bodega
                {
                    IdPropietario = BeConfigEnc.IdPropietario,
                    IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(IdBodegaDestino,
                                                                                                              BeConfigEnc.IdPropietario,
                                                                                                              lConnection,
                                                                                                              lTransInterface)
                };

                if (gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega == 0)
                    throw new Exception("No se pudo obtener el IdPropietarioBodega");

                gBeOrdenCompraEnc.IdPropietarioBodega = gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega;
            }                

            return BeConfigEnc;
        }
        catch
        {
            throw;
        }
    }

    public static int ObtenerIdBodegaDestino(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                            ref clsBeI_nav_config_enc BeConfigEnc,
                                            ref clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                            SqlConnection lConnection,
                                            SqlTransaction lTransInterface)
    {
        try
        {
            int IdBodegaDestino = 0;

            if (!navPedidoCompraEnc.Is_Internal_Transfer)
            {
                IdBodegaDestino = clsLnCliente.Get_IdUbicacionVirtual_By_Codigo(navPedidoCompraEnc.Location_Code, 
                                                                                lConnection, 
                                                                                lTransInterface);

                if (IdBodegaDestino == 0)
                {
                    IdBodegaDestino = clsLnBodega.Get_IdBodega_By_Codigo(navPedidoCompraEnc.Location_Code, 
                                                                         lConnection, 
                                                                         lTransInterface);

                    if (IdBodegaDestino == 0)
                    {
                        var beBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(navPedidoCompraEnc.Location_Code, 
                                                                                        lConnection, 
                                                                                        lTransInterface);

                        if (beBodegaArea != null)
                        {
                            IdBodegaDestino = beBodegaArea.IdBodega;
                        }
                        else if (int.TryParse(navPedidoCompraEnc.Location_Code, out int idNum) &&
                                 clsLnBodega.Exists_By_IdBodega(idNum, lConnection, lTransInterface))
                        {
                            IdBodegaDestino = idNum;
                        }
                    }
                }
            }
            else
            {
                IdBodegaDestino = clsLnBodega.Get_IdBodega_By_Codigo(navPedidoCompraEnc.Location_Code, 
                                                                     lConnection, 
                                                                     lTransInterface);

                if (IdBodegaDestino == 0 &&
                    int.TryParse(navPedidoCompraEnc.Location_Code, out int idNum) &&
                    clsLnBodega.Exists_By_IdBodega(idNum, lConnection, lTransInterface))
                {
                    IdBodegaDestino = idNum;
                }
            }

            if (IdBodegaDestino == 0)
            {
                string mensaje = navPedidoCompraEnc.Is_Internal_Transfer
                    ? $"No se ha configurado la ubicación virtual para el cliente/bodega: {navPedidoCompraEnc.Location_Code} IsInternalTransfer: {navPedidoCompraEnc.Is_Internal_Transfer}"
                    : $"#ERROR_202309131539: No se ha configurado la ubicación virtual para la bodega destino: {navPedidoCompraEnc.Location_Code} IsInternalTransfer: {navPedidoCompraEnc.Is_Internal_Transfer}, revise que el material está asociado a la bodega.";

                throw new Exception(mensaje);
            }

            BeConfigEnc.Idbodega = IdBodegaDestino;
            gBeOrdenCompraEnc.IdBodega = IdBodegaDestino;

            return IdBodegaDestino;
        }
        catch
        {
            throw;
        }
    }
    public static clsBeTrans_oc_ti AsignarTipoDocumentoIngreso(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                               ref clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                                               ref clsBeTrans_oc_ti BeTipoDocumento,
                                                               SqlConnection lConnection,
                                                               SqlTransaction lTransInterface)
    {
        try
        {
            BeTipoDocumento ??= new clsBeTrans_oc_ti();

            if (navPedidoCompraEnc.Document_Type == 0)
            {
                if (navPedidoCompraEnc.Is_Internal_Transfer)
                {
                    gBeOrdenCompraEnc.IdTipoIngresoOC = (int)clsDataContractDI.tTipoDocumentoIngreso.Transferencia;
                    BeTipoDocumento.Genera_tarea_ingreso = true;
                }
                else
                {
                    gBeOrdenCompraEnc.IdTipoIngresoOC = (int)clsDataContractDI.tTipoDocumentoIngreso.Ingreso;
                }
            }
            else
            {
                gBeOrdenCompraEnc.IdTipoIngresoOC = navPedidoCompraEnc.Document_Type;

                BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(gBeOrdenCompraEnc.IdTipoIngresoOC, 
                                                             lConnection, 
                                                             lTransInterface
                ) ?? new clsBeTrans_oc_ti { Genera_tarea_ingreso = false };
            }

            return BeTipoDocumento;
        }
        catch
        {
            throw;
        }
    }
    public static void ConfigurarEncabezadoOrdenCompra(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                       clsBeI_nav_config_enc BeConfigEnc,
                                                       clsBeProveedor_bodega BeProveedorBodega,
                                                       ref clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                                       SqlConnection lConnection,
                                                       SqlTransaction lTransInterface)
    {
        try
        {
            gBeOrdenCompraEnc.IdProveedorBodega = BeProveedorBodega.IdAsignacion;
            gBeOrdenCompraEnc.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No;

            if (BeConfigEnc.Interface_SAP)
            {
                gBeOrdenCompraEnc.No_Documento_Recepcion_ERP = navPedidoCompraEnc.Ship_To_Contact;
            }

            gBeOrdenCompraEnc.User_Mod = BeConfigEnc.IdUsuario.ToString();
            gBeOrdenCompraEnc.Fec_Mod = DateTime.Now;

            if (BeConfigEnc.Interface_SAP)
            {
                gBeOrdenCompraEnc.Procedencia = $"{navPedidoCompraEnc.Buy_From_Vendor_No} {navPedidoCompraEnc.Buy_From_Vendor_Name}";
            }
            else
            {
                gBeOrdenCompraEnc.Procedencia = navPedidoCompraEnc.Ship_To_Contact;
            }

            gBeOrdenCompraEnc.No_Marchamo = string.Empty;
            gBeOrdenCompraEnc.Referencia = navPedidoCompraEnc.No;
            gBeOrdenCompraEnc.Observacion = navPedidoCompraEnc.Posting_Description;
            gBeOrdenCompraEnc.Control_Poliza = false;
            gBeOrdenCompraEnc.Codigo_Empresa_ERP = navPedidoCompraEnc.Company_Code;

            if (gBeOrdenCompraEnc.IsNew)
            {
                gBeOrdenCompraEnc.ObjPoliza = null;
            }

            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompraEnc, lConnection, lTransInterface);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static clsBeProducto_bodega BuscarProductoBodega(clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                                            int IdBodegaDestino,
                                                            clsBeI_nav_config_enc BeConfigEnc,
                                                            SqlConnection lConnection,
                                                            SqlTransaction lTransInterface)
    {
        try
        {
            clsBeProducto_bodega? productoBodega = null;

            if (BeConfigEnc.Equiparar_Productos)
            {
                // 1️Buscar por código
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(
                    navPedidoCompraDet.No,
                    IdBodegaDestino,
                    lConnection,
                    lTransInterface);

                // 2️Si no existe, buscar por parte
                if (productoBodega == null)
                {
                    productoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(
                        navPedidoCompraDet.No,
                        IdBodegaDestino,
                        lConnection,
                        lTransInterface);
                }

                // 3️⃣ Si tampoco, buscar por número de serie
                if (productoBodega == null)
                {
                    productoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(
                        navPedidoCompraDet.No,
                        IdBodegaDestino,
                        lConnection,
                        lTransInterface);
                }
            }
            else
            {
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(
                    navPedidoCompraDet.No,
                    IdBodegaDestino,
                    lConnection,
                    lTransInterface);
            }

            return productoBodega!;
        }
        catch
        {
            throw;
        }
    }
    public static bool ActualizarDetalleOrdenCompra(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                   clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                                   ref clsBeTrans_oc_det BePedidoCompraDet,
                                                   ref clsBeTrans_oc_det_lote BeOcDetLote,
                                                   ref List<clsBeTrans_oc_det_lote> LotesExistentes,
                                                   clsBeProducto_bodega BeProductoBodega,
                                                   ref int VContadorBitacoraTOMWMS,
                                                   ref int vContadorLineasDetInsertadas,
                                                   ref string lblprg,
                                                   clsBeI_nav_config_enc BeConfigEnc,
                                                   int IdNavConfigDet,
                                                   clsBeTrans_oc_enc PedidoCompraExistente,
                                                   SqlConnection lConnection,
                                                   SqlTransaction lTransInterface)
    {
        try
        {
            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega;
            BePedidoCompraDet.Codigo_producto = BeProductoBodega.Producto?.codigo ?? string.Empty;
            BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description);

            BePedidoCompraDet.Nombre_unidad_medida_basica =
                string.IsNullOrEmpty(navPedidoCompraDet.Variant_Code)
                    ? navPedidoCompraDet.Unit_Of_Measure_Code
                    : (BeProductoBodega.Producto?.UnidadMedida?.Nombre ?? navPedidoCompraDet.Unit_Of_Measure_Code);

            double DifCant = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad;

            if (BePedidoCompraDet.Cantidad != 0)
            {
                lblprg += Environment.NewLine;
                string msg = (DifCant == 0)
                    ? $"La cantidad no se modificó para pedido {navPedidoCompraEnc.No} producto {navPedidoCompraDet.No} "
                    : (DifCant > 0)
                        ? $"La cantidad incrementó respecto a TOM para pedido {navPedidoCompraEnc.No} producto {navPedidoCompraDet.No} "
                        : $"La cantidad disminuyó respecto al original en WMS  para pedido {navPedidoCompraEnc.No} producto {navPedidoCompraDet.No} ";
                lblprg += msg;
            }

            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity;
            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received;
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost;
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount;
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No;
            BePedidoCompraDet.Activo = true;
            BePedidoCompraDet.Porcentaje_arancel = 0;
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario.ToString();
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code;

            clsLnTrans_oc_det.Actualizar_Desde_Interface(ref BePedidoCompraDet, lConnection, lTransInterface);

            LotesExistentes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                             lConnection,
                                                                             lTransInterface);

            ProcesarLotes(navPedidoCompraEnc,
                          navPedidoCompraDet,
                          ref BeOcDetLote,
                          BePedidoCompraDet,
                          LotesExistentes,
                          lConnection,
                          lTransInterface);

            VContadorBitacoraTOMWMS += 1;
            vContadorLineasDetInsertadas += 1;

            return true;
        }
        catch (Exception ex)
        {            
            lblprg += $"Pedido Sin Detalle: {ex.Message}{Environment.NewLine}";
            return false;
        }
    }
    public static bool ProcesarLotes(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                    clsBeI_nav_ped_compra_det navPedidoCompraDet,
                                    ref clsBeTrans_oc_det_lote BeOcDetLote,
                                    clsBeTrans_oc_det BePedidoCompraDet,
                                    List<clsBeTrans_oc_det_lote> LotesExistentes,
                                    SqlConnection lConnection,
                                    SqlTransaction lTransInterface)
    {
        try
        {
            if (navPedidoCompraEnc.Lineas_Detalle_Lotes == null || navPedidoCompraEnc.Lineas_Detalle_Lotes.Count == 0)
            {
                return false;
            }

            var lotesFiltrados = navPedidoCompraEnc.Lineas_Detalle_Lotes.Where(x => x.NoEnc == navPedidoCompraDet.NoEnc &&
                                                                                   x.Item_No == navPedidoCompraDet.No &&
                                                                                   x.Source_Prod_Order_Line == navPedidoCompraDet.Line_No);

            foreach (var Lote in lotesFiltrados)
            {
                var LoteExistente = LotesExistentes.Find(x => x.No_linea == Lote.Source_Prod_Order_Line && x.Lote == Lote.Lot_No);

                BeOcDetLote = new clsBeTrans_oc_det_lote
                {
                    IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc,
                    IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet,
                    Cantidad = Lote.Quantity_Base,
                    No_linea = Lote.Source_Prod_Order_Line,
                    IdProductoBodega = BePedidoCompraDet.IdProductoBodega,
                    Lote = Lote.Lot_No,
                    Cantidad_recibida = 0,
                    Codigo_producto = Lote.Item_No
                };

                if (LoteExistente == null)
                {
                    BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransInterface) + 1;
                    if (BeOcDetLote != null)
                        clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransInterface);
                }
                else
                {
                    clsLnTrans_oc_det_lote.Actualizar(BeOcDetLote, lConnection, lTransInterface);
                }
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static bool InsertaProveedor(clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                        clsBeTrans_oc_enc gBeOrdenCompraEnc,
                                        clsBeI_nav_config_enc BeConfigEnc,
                                        ref clsBeProveedor_bodega? BeProveedorBodega,
                                        SqlConnection lConnection,
                                        SqlTransaction lTransaction)
    {
        bool result = false;
        try
        {
            if (!clsLnProveedor.Existe_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No, lConnection, lTransaction))
            {
                clsBeProveedor BeProveedorIngresoDefecto = new clsBeProveedor
                {
                    IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1,
                    IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(gBeOrdenCompraEnc.IdBodega, lConnection, lTransaction),
                    IdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(gBeOrdenCompraEnc.IdBodega, gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction),
                    Codigo = navPedidoCompraEnc.Buy_From_Vendor_No,
                    Nombre = navPedidoCompraEnc.Buy_From_Vendor_Name,
                    Activo = true,
                    User_agr = BeConfigEnc.User_agr,
                    User_mod = BeConfigEnc.User_mod,
                    Fec_agr = DateTime.Now,
                    Fec_mod = DateTime.Now
                };
                clsLnProveedor.Insertar(BeProveedorIngresoDefecto, lConnection, lTransaction);

                BeProveedorBodega = new clsBeProveedor_bodega
                {
                    IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1,
                    IdProveedor = BeProveedorIngresoDefecto.IdProveedor,
                    IdBodega = gBeOrdenCompraEnc.IdBodega,
                    Activo = true,
                    User_agr = BeConfigEnc.IdUsuario.ToString(),
                    User_mod = BeConfigEnc.IdUsuario.ToString(),
                    Fec_agr = DateTime.Now,
                    Fec_mod = DateTime.Now
                };
                clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConnection, lTransaction);
                result = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error en InsertarProveedorDefecto: " + ex.Message);
        }
        return result;
    }
    public static bool Procesar_Pedido_Compra_MI3(IConfiguration config,
                                                ref clsBeI_nav_ped_compra_enc navPedidoCompraEnc,
                                                ref clsBeTrans_oc_enc OutBeOrdenCompra,
                                                ref string lblprg,
                                                clsBeTrans_pe_enc? BePedidoEnc)
    {
        bool result = false;

        clsBeI_nav_config_enc? BeConfigEnc = new clsBeI_nav_config_enc();
        int IdNavConfigDet = 101;
        int VContadorBitacoraTOMWMS = 0;
        clsBeTrans_oc_enc? gBeOrdenCompraEnc = null;
        clsBeTrans_oc_enc? PedidoCompraExistente = null;
        int vContadorLineasDetInsertadas = 0;
        bool InsertoEncabezado = false;
        clsBeTrans_oc_det_lote BeOcDetLote = new clsBeTrans_oc_det_lote();
        clsBeTrans_oc_ti BeTipoDocumento = new clsBeTrans_oc_ti();
        List<clsBeTrans_oc_det_lote> LotesExistentes = new List<clsBeTrans_oc_det_lote>();
        double vCantidadDecimalUMBas = 0;
        double vCantidadEnteraPres = 0;
        int vProductoNoExiste = 0;

        SqlConnection? lConnection = null;
        SqlTransaction? lTransInterface = null;

        try
        {
            lConnection = new SqlConnection(config.GetConnectionString("CST"));
            lConnection.Open();
            lTransInterface = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            if (navPedidoCompraEnc.Status != "0")
            {
                lblprg += Environment.NewLine + $"Procesando Documento: {navPedidoCompraEnc.No}" + Environment.NewLine;

                gBeOrdenCompraEnc = new clsBeTrans_oc_enc()
                {
                    Referencia = navPedidoCompraEnc.No,
                    IdTipoIngresoOC = navPedidoCompraEnc.Document_Type
                };

                PedidoCompraExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(ref gBeOrdenCompraEnc, lConnection, lTransInterface);

                int IdBodegaDestino = ObtenerIdBodegaDestino(navPedidoCompraEnc, ref BeConfigEnc, ref gBeOrdenCompraEnc, lConnection, lTransInterface);
                lblprg += Environment.NewLine + $"Procesando Bodega Destino: {IdBodegaDestino}" + Environment.NewLine;

                int vIdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(gBeOrdenCompraEnc.IdBodega, lConnection, lTransInterface);

                lblprg += Environment.NewLine + $"Procesando empresa: {vIdEmpresa}" + Environment.NewLine;

                ObtenerConfiguracionDeBodega(navPedidoCompraEnc, ref BeConfigEnc, ref gBeOrdenCompraEnc, IdBodegaDestino, vIdEmpresa, lConnection, lTransInterface);

                if(BeConfigEnc !=null)
                if (BeConfigEnc.Idnavconfigenc == 0) throw new Exception("Error al cargar i_nav_config_enc en Procesar_Pedido_Compra_MI3");

                if (PedidoCompraExistente != null)
                {
                    lblprg += "El documento ya existe, se actualizará." + Environment.NewLine;

                    gBeOrdenCompraEnc.Activo = true;
                    gBeOrdenCompraEnc.IdCampaña = navPedidoCompraEnc.Campaign_No;

                    gBeOrdenCompraEnc.Serie = navPedidoCompraEnc.Series;
                    gBeOrdenCompraEnc.Usr_Documento = navPedidoCompraEnc.User_Document;
                    gBeOrdenCompraEnc.Comentarios = navPedidoCompraEnc.Comments;

                    clsBeProveedor_bodega BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No, BeConfigEnc.Idbodega, lConnection, lTransInterface);
                    BeTipoDocumento = AsignarTipoDocumentoIngreso(navPedidoCompraEnc, ref gBeOrdenCompraEnc, ref BeTipoDocumento, lConnection, lTransInterface);
                    ConfigurarEncabezadoOrdenCompra(navPedidoCompraEnc,
                                                    BeConfigEnc,
                                                    BeProveedorBodega,
                                                    ref gBeOrdenCompraEnc,
                                                    lConnection,
                                                    lTransInterface);
                    lTransInterface.Save("oc_enc");

                    lblprg += $"Procesando# : {navPedidoCompraEnc.No}" + Environment.NewLine;
                    VContadorBitacoraTOMWMS += 1;

                    foreach (var navPedidoCompraDet in navPedidoCompraEnc.Lineas_Detalle)
                    {
                        try
                        {
                            clsBeProducto_presentacion BePresentacion = new clsBeProducto_presentacion();
                            clsBeUnidad_medida? BeUnidadMedidaPedCompra = null;
                            clsBeProducto_bodega BeProductoBodega = BuscarProductoBodega(navPedidoCompraDet, IdBodegaDestino, BeConfigEnc, lConnection, lTransInterface);

                            if (BeProductoBodega != null)
                            {
                                clsBeTrans_oc_det? BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc, navPedidoCompraDet.Line_No, lConnection, lTransInterface);
                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_Of_Measure_Code, BeConfigEnc.IdPropietario, lConnection, lTransInterface);
                                ValidarPresentaciones(navPedidoCompraDet, BeProductoBodega, ref BePresentacion, lConnection, lTransInterface);
                                if (BePedidoCompraDet == null)
                                {
                                    CrearYGuardarDetalleOC(navPedidoCompraEnc,
                                                         navPedidoCompraDet,
                                                         ref BePedidoCompraDet,
                                                         BeProductoBodega,
                                                         BeUnidadMedidaPedCompra,
                                                         BePresentacion,
                                                         PedidoCompraExistente,
                                                         ref VContadorBitacoraTOMWMS,
                                                         IdNavConfigDet,
                                                         BeConfigEnc,
                                                         ref lblprg,
                                                         LotesExistentes,
                                                         ref BeOcDetLote,
                                                         lConnection,
                                                         lTransInterface);
                                }
                                else
                                {
                                    ActualizarDetalleOrdenCompra(navPedidoCompraEnc,
                                                               navPedidoCompraDet,
                                                               ref BePedidoCompraDet,
                                                               ref BeOcDetLote,
                                                               ref LotesExistentes,
                                                               BeProductoBodega,
                                                               ref VContadorBitacoraTOMWMS,
                                                               ref vContadorLineasDetInsertadas,
                                                               ref lblprg,
                                                               BeConfigEnc,
                                                               IdNavConfigDet,
                                                               PedidoCompraExistente,
                                                               lConnection,
                                                               lTransInterface);
                                }
                            }
                            else if (BeConfigEnc.Interface_SAP)
                            {
                                lblprg += $"Producto no existe en WMS: {navPedidoCompraDet.No}" + Environment.NewLine;
                                vProductoNoExiste += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblprg += $"Pedido sin detalle: {ex.Message}" + Environment.NewLine;
                        }
                    }
                }
                else if (navPedidoCompraEnc.Lineas_Detalle.Count > 0)
                {
                    try
                    {
                        List<clsBeTrans_picking_ubic>? DetallePickingUbic = new List<clsBeTrans_picking_ubic>();

                        bool creada = InicializarEncabezadoNuevaOC(navPedidoCompraEnc,
                                                                  BeConfigEnc,
                                                                  ref BeTipoDocumento,
                                                                  IdNavConfigDet,
                                                                  ref lblprg,
                                                                  ref gBeOrdenCompraEnc,
                                                                  IdBodegaDestino,
                                                                  lConnection,
                                                                  lTransInterface);
                        lTransInterface.Save("oc_enc");
                        InsertoEncabezado = true;
                        VContadorBitacoraTOMWMS += 1;

                        clsBeUnidad_medida? BeUnidadMedidaPedCompra = new clsBeUnidad_medida();
                        clsBeProducto_presentacion? BePresentacion = new clsBeProducto_presentacion();
                        double vCantidadSolicitadaPedido = 0;

                        if (!string.IsNullOrEmpty(navPedidoCompraEnc.Internal_Transfer_Document_No) && IsNumeric(navPedidoCompraEnc.Internal_Transfer_Document_No))
                        {
                            DetallePickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_Despachado_By_IdDespachoEnc(int.Parse(navPedidoCompraEnc.Internal_Transfer_Document_No), lConnection, lTransInterface);
                        }

                        foreach (var navPedidoCompraDet in navPedidoCompraEnc.Lineas_Detalle)
                        {
                            clsBeProducto_bodega BeProductoBodega = BuscarProductoBodega(navPedidoCompraDet, IdBodegaDestino, BeConfigEnc, lConnection, lTransInterface);
                            if (BeProductoBodega != null)
                            {
                                ValidarYCalcularUMBas(navPedidoCompraDet,
                                                    ref BeUnidadMedidaPedCompra,
                                                    ref BePresentacion,
                                                    BeProductoBodega,
                                                    BeConfigEnc,
                                                    ref vCantidadSolicitadaPedido,
                                                    ref vCantidadEnteraPres,
                                                    ref vCantidadDecimalUMBas,
                                                    ref lblprg,
                                                    lConnection,
                                                    lTransInterface);

                                InsertarDetalleOrdenCompra(navPedidoCompraEnc,
                                                         navPedidoCompraDet,
                                                         BeProductoBodega,
                                                         BePresentacion,
                                                         BeConfigEnc,
                                                         BeUnidadMedidaPedCompra,
                                                         vCantidadEnteraPres,
                                                         vCantidadDecimalUMBas,
                                                         ref vContadorLineasDetInsertadas,
                                                         ref lblprg,
                                                         ref BeOcDetLote,
                                                         ref LotesExistentes,
                                                         gBeOrdenCompraEnc,
                                                         DetallePickingUbic,
                                                         lConnection,
                                                         lTransInterface);
                            }
                            else
                            {
                                clsBeProducto? beProducto = new clsBeProducto();
                                beProducto = clsLnProducto.Get_Single_By_Codigo(navPedidoCompraDet.No, lConnection, lTransInterface);
                                if (beProducto != null)
                                {
                                    BeProductoBodega = new clsBeProducto_bodega
                                    {
                                        IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransInterface) + 1,
                                        IdProducto = beProducto.IdProducto,
                                        IdBodega = BeConfigEnc.Idbodega,
                                        Activo = true,
                                        User_agr = BeConfigEnc.IdUsuario.ToString(),
                                        User_mod = BeConfigEnc.IdUsuario.ToString(),
                                        Fec_agr = DateTime.Now,
                                        Fec_mod = DateTime.Now
                                    };

                                    clsLnProducto_bodega.Insertar(BeProductoBodega, lConnection, lTransInterface);

                                    lblprg += $"El código de producto:{navPedidoCompraDet.No} no estaba asociado a la bodega y ya lo asociamos:{navPedidoCompraDet.Location_Code}" + Environment.NewLine;
                                    ValidarYCalcularUMBas(navPedidoCompraDet,
                                                        ref BeUnidadMedidaPedCompra,
                                                        ref BePresentacion,
                                                        BeProductoBodega,
                                                        BeConfigEnc,
                                                        ref vCantidadSolicitadaPedido,
                                                        ref vCantidadEnteraPres,
                                                        ref vCantidadDecimalUMBas,
                                                        ref lblprg,
                                                        lConnection,
                                                        lTransInterface);

                                    InsertarDetalleOrdenCompra(navPedidoCompraEnc,
                                                             navPedidoCompraDet,
                                                             BeProductoBodega,
                                                             BePresentacion,
                                                             BeConfigEnc,
                                                             BeUnidadMedidaPedCompra,
                                                             vCantidadEnteraPres,
                                                             vCantidadDecimalUMBas,
                                                             ref vContadorLineasDetInsertadas,
                                                             ref lblprg,
                                                             ref BeOcDetLote,
                                                             ref LotesExistentes,
                                                             gBeOrdenCompraEnc,
                                                             DetallePickingUbic,
                                                             lConnection,
                                                             lTransInterface);
                                }
                                else
                                {
                                    lblprg += $"El código de producto:{navPedidoCompraDet.No} no existe:{navPedidoCompraDet.Location_Code}" + Environment.NewLine;
                                    vProductoNoExiste += 1;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string vMsgEx4 = Environment.NewLine + $"Error al insertar el documento de ingreso con Referencia: {navPedidoCompraEnc.No}{Environment.NewLine}{ex.Message}";
                        lblprg += vMsgEx4 + Environment.NewLine;
                        throw new Exception(vMsgEx4);
                    }
                }
                else
                {
                    lblprg += $"Pedido #:{navPedidoCompraEnc.No} Sin Detalle" + Environment.NewLine;
                }

                if (InsertoEncabezado)
                {
                    if (vContadorLineasDetInsertadas > 0 && vProductoNoExiste == 0)
                    {
                        if (OutBeOrdenCompra != null)
                        {
                            Generar_Tarea_Recepcion(gBeOrdenCompraEnc,
                                                    BeConfigEnc,
                                                    BeTipoDocumento,
                                                    navPedidoCompraEnc,
                                                    ref lblprg,
                                                    BePedidoEnc,
                                                    lConnection,
                                                    lTransInterface);
                        }

                        result = true;
                        lTransInterface?.Commit();
                        OutBeOrdenCompra = gBeOrdenCompraEnc;
                        lblprg += Environment.NewLine + $"Documento de ingreso procesados correctamente - IdWMS: {OutBeOrdenCompra.IdOrdenCompraEnc}" + Environment.NewLine;
                    }
                    else
                    {
                        lTransInterface?.Rollback("oc_enc");
                    }
                }
            }
            else
            {
                lblprg += Environment.NewLine + $"OC Inactiva {navPedidoCompraEnc.No}" + Environment.NewLine;
            }
        }
        catch (Exception ex)
        {
            lTransInterface?.Rollback();
            lblprg += Environment.NewLine + $"Error al insertar pedido de compra a tabla de TOMWMS : {ex.Message}" + Environment.NewLine;
            throw;
        }
        finally
        {
            if (lConnection != null && lConnection.State == ConnectionState.Open)
                lConnection.Close();
            lConnection?.Dispose();
            lTransInterface?.Dispose();
        }

        return result;
    }

    private static bool IsNumeric(string value)
    {
        return double.TryParse(value, out _);
    } 
}