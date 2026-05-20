namespace WMS.EntityCore.Prefactura
{
    public class clsBeTrans_prefactura_erp : ICloneable
    {
        public int IdPrefacturaEnc { get; set; }
        public string Nit { get; set; } = "";
        public int IdCliente_facturar { get; set; }
        public int Codigo_acuerdo { get; set; }
        public int IdCliente { get; set; }
        public string Moneda { get; set; } = "";
        public string Periodo { get; set; } = "";
        public string Mercaderia { get; set; } = "";
        public double TipoCambio { get; set; }
        public string Observaciones { get; set; } = "";
        public List<clsBeTrans_prefactura_erp_det> Detalle { get; set; } = new List<clsBeTrans_prefactura_erp_det>();

        public clsBeTrans_prefactura_erp()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}