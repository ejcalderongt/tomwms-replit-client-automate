using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeI_nav_ped_compra_det_lote : ICloneable
    {
        [Column("NoEnc")]
        [DisplayName("NoEnc")]
        public string NoEnc { get; set; } = "";

        [Column("source_ID")]
        [DisplayName("source_ID")]
        public string Source_ID { get; set; } = "";

        [Column("Source_Prod_Order_Line")]
        [DisplayName("Source_Prod_Order_Line")]
        public int Source_Prod_Order_Line { get; set; } = 0;

        [Column("Item_No")]
        [DisplayName("Item_No")]
        public string Item_No { get; set; } = "";

        [Column("Lot_No")]
        [DisplayName("Lot_No")]
        public string Lot_No { get; set; } = "";

        [Column("Expiration_Date")]
        [DisplayName("Expiration_Date")]
        public DateTime Expiration_Date { get; set; } = new DateTime(1900,1,1);

        [Column("Entry_No")]
        [DisplayName("Entry_No")]
        public string Entry_No { get; set; } = "";

        [Column("Source_Type")]
        [DisplayName("Source_Type")]
        public int Source_Type { get; set; } = 0;

        [Column("Quantity_Base")]
        [DisplayName("Quantity_Base")]
        public double Quantity_Base { get; set; } = 0;

        [Column("Variant_Code")]
        [DisplayName("Variant_Code")]
        public string Variant_Code { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public Double Pallet_Weight { get; set; } = 0;
        public string Pallet_License_No { get; set; } = "";

        public clsBeI_nav_ped_compra_det_lote() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
