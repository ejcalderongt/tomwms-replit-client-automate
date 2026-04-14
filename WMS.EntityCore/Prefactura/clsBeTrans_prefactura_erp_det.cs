namespace WMS.EntityCore.Prefactura
{
    public class clsBeTrans_prefactura_erp_det : ICloneable
    {
        public int corre_cbdetacuerdosservicios { get; set; } = 0;
        public string codigoproducto { get; set; } = "";
        public int dias { get; set; } = 0;
        public double monto { get; set; } = 0.00;

        public clsBeTrans_prefactura_erp_det()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}