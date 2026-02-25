using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeI_nav_ped_compra_det : ICloneable
    {
        [Column("NoEnc")]
        [DisplayName("NoEnc")]
        public string NoEnc { get; set; } = "";

        [Column("No")]
        [DisplayName("No")]
        public string No { get; set; } = "";

        [Column("Line_No")]
        [DisplayName("Line_No")]
        public int Line_No { get; set; } = 0;

        [Column("Type")]
        [DisplayName("Type")]
        public string Type { get; set; } = "";

        [Column("Description")]
        [DisplayName("Description")]
        public string Description { get; set; } = "";

        [Column("Description2")]
        [DisplayName("Description2")]
        public string Description2 { get; set; } = "";

        [Column("Location_Code")]
        [DisplayName("Location_Code")]
        public string Location_Code { get; set; } = "";

        [Column("Quantity")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; } = 0;

        [Column("Unit_Of_Measure_Code")]
        [DisplayName("Unit_Of_Measure_Code")]
        public string Unit_Of_Measure_Code { get; set; } = "";

        [Column("Direct_Unit_Cost")]
        [DisplayName("Direct_Unit_Cost")]
        public double Direct_Unit_Cost { get; set; } = 0;

        [Column("Line_Amount")]
        [DisplayName("Line_Amount")]
        public double Line_Amount { get; set; } = 0;

        [Column("Quantity_Received")]
        [DisplayName("Quantity_Received")]
        public double Quantity_Received { get; set; } = 0;

        [Column("Planed_Receipt_Date")]
        [DisplayName("Planed_Receipt_Date")]
        public DateTime Planed_Receipt_Date { get; set; } = DateTime.Now;

        [Column("Variant_Code")]
        [DisplayName("Variant_Code")]
        public string Variant_Code { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string Barcode { get; set; } = "";
        public string Size { get; set; } = "";
        public string Color { get; set; } = "";
        public  int LayersPallet { get; set; } = 0;
        public int BoxesLayer { get; set; } = 0;    
        public clsBeI_nav_ped_compra_det() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
