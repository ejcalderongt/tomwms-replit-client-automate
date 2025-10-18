namespace WMS.EntityCore.Pedido
{
    public class clsBeI_nav_ped_traslado_det_lote : ICloneable
    {
        public string NoEnc { get; set; } = string.Empty;
        public int Line_No { get; set; } = 0;
        public string No { get; set; } = string.Empty;
        public string Batch_No { get; set; } = string.Empty;
        public string Serial_No { get; set; } = string.Empty;
        public DateTime Expiration_Date { get; set; } = DateTime.Now;
        public double Quantity_Base { get; set; } = 0.0;
        public string Variant_Code { get; set; } = string.Empty;
        public string WhsFrom { get; set; } = string.Empty;
        public string WhsTo { get; set; } = string.Empty;
        public DateTime Fec_Agr { get; set; } = DateTime.Now;

        public clsBeI_nav_ped_traslado_det_lote()
        {
            // Constructor vacío
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}