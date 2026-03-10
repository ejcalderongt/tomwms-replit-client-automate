namespace WMS.EntityCore.Pedido
{
    using System;

    public class clsBeI_nav_ped_traslado_det : ICloneable
    {
        public string NoEnc { get; set; } = string.Empty;
        public int Line_No { get; set; } = 0;
        public string Variant_Code { get; set; } = string.Empty;
        public string No { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Item_No { get; set; } = string.Empty;
        public double Qty_to_Receive { get; set; } = 0;
        public double Qty_to_Ship { get; set; } = 0;
        public double Quantity { get; set; } = 0;
        public decimal Quantity_Shipped { get; set; } = 0;
        public string Transfer_to_CodeField { get; set; } = string.Empty;
        public string Transfer_From_CodeField { get; set; } = string.Empty;
        public DateTime? Shipment_Date { get; set; } = new DateTime(1900, 1, 1);
        public string Unit_of_Measure_Code { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
        public string Process_Result { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public string Lote_No { get; set; } = string.Empty;
        public DateTime? Expiration_Date { get; set; } = new DateTime(1900, 1, 1);
        public string Source_ID { get; set; } = string.Empty;
        public int IdPedidoDet { get; set; } = 0;
        public decimal Quantity_In_UMBas { get; set; } = 0.0m;
        public bool Is_Partially_Processed { get; set; } = false;
        public double Quantity_Reserved_WMS { get; set; } = 0;
        public string Scan_Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public List<clsBeI_nav_ped_traslado_det_lote> Lotes_Detalle { get; set; } = new List<clsBeI_nav_ped_traslado_det_lote>();

        public clsBeI_nav_ped_traslado_det() { }

        public clsBeI_nav_ped_traslado_det(
            ref string NoEnc,
            string No,
            string Description,
            string Item_No,
            double Qty_to_Receive,
            double Qty_to_Ship,
            double Quantity,
            string transfer_to_CodeField,
            DateTime Shipment_Date,
            string Unit_of_Measure_Code)
        {
            this.NoEnc = NoEnc;
            this.No = No;
            this.Description = Description;
            this.Item_No = Item_No;
            this.Qty_to_Receive = Qty_to_Receive;
            this.Qty_to_Ship = Qty_to_Ship;
            this.Quantity = Quantity;
            this.Transfer_to_CodeField = transfer_to_CodeField;
            this.Shipment_Date = Shipment_Date;
            this.Unit_of_Measure_Code = Unit_of_Measure_Code;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}