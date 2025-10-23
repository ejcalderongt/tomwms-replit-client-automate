using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeI_nav_ped_compra_enc : ICloneable
    {
        [Column("No")]
        [DisplayName("No")]
        public string No { get; set; } = "";

        [Column("Buy_From_Vendor_No")]
        [DisplayName("Buy_From_Vendor_No")]
        public string Buy_From_Vendor_No { get; set; } = "";

        [Column("Buy_From_Vendor_Name")]
        [DisplayName("Buy_From_Vendor_Name")]
        public string Buy_From_Vendor_Name { get; set; } = "";

        [Column("Posting_Description")]
        [DisplayName("Posting_Description")]
        public string Posting_Description { get; set; } = "";

        [Column("Posting_Date")]
        [DisplayName("Posting_Date")]
        public DateTime Posting_Date { get; set; } = DateTime.Now;

        [Column("Order_Date")]
        [DisplayName("Order_Date")]
        public DateTime Order_Date { get; set; } = DateTime.Now;

        [Column("Document_Date")]
        [DisplayName("Document_Date")]
        public DateTime Document_Date { get; set; } = DateTime.Now;

        [Column("Vendor_Invoice_No")]
        [DisplayName("Vendor_Invoice_No")]
        public string Vendor_Invoice_No { get; set; } = "";

        [Column("Status")]
        [DisplayName("Status")]
        public string Status { get; set; } = "";

        [Column("Payment_Terms_Code")]
        [DisplayName("Payment_Terms_Code")]
        public string Payment_Terms_Code { get; set; } = "";

        [Column("Ship_To_Name")]
        [DisplayName("Ship_To_Name")]
        public string Ship_To_Name { get; set; } = "";

        [Column("Location_Code")]
        [DisplayName("Location_Code")]
        public string Location_Code { get; set; } = "";

        [Column("Ship_To_Contact")]
        [DisplayName("Ship_To_Contact")]
        public string Ship_To_Contact { get; set; } = "";

        [Column("Expected_Receipt_Date")]
        [DisplayName("Expected_Receipt_Date")]
        public DateTime Expected_Receipt_Date { get; set; } = DateTime.Now;

        [Column("Is_Internal_Transfer")]
        [DisplayName("Is_Internal_Transfer")]
        public bool Is_Internal_Transfer { get; set; } = false;

        [Column("Product_Owner_Code")]
        [DisplayName("Product_Owner_Code")]
        public string Product_Owner_Code { get; set; } = "";

        [Column("Internal_Transfer_Document_No")]
        [DisplayName("Internal_Transfer_Document_No")]
        public string Internal_Transfer_Document_No { get; set; } = "";

        [Column("Document_Type")]
        [DisplayName("Document_Type")]
        public int Document_Type { get; set; } = 0;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("IsImport")]
        [DisplayName("IsImport")]
        public bool IsImport { get; set; } = false;

        [Column("Company_Code")]
        [DisplayName("Company_Code")]
        public string Company_Code { get; set; } = "";

        public List<clsBeI_nav_ped_compra_det> Lineas_Detalle { get; set; } = new List<clsBeI_nav_ped_compra_det>();
        public List<clsBeI_nav_ped_compra_det_lote> Lineas_Detalle_Lotes { get; set; } = new();
        public List<clsBeProducto_talla_color> Lineas_Detalle_Talla_Color { get; set; } = new();

        public clsBeCampaña Campaña { get; set; } = new clsBeCampaña();

        public string Series { get; set; } = ""; // Initialize to avoid CS8618

        public int Campaign_No { get; set; } = 0;
        public string User_Document { get; set; } = "";
        public string Comments { get; set; } = "";

        public clsBeI_nav_ped_compra_enc() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
