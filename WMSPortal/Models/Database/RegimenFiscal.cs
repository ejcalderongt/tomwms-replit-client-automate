#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RegimenFiscal
    {
        public int IdRegimen { get; set; }
        public string CodigoRegimen { get; set; }
        public string Descripcion { get; set; }
        public int? DiasVencimiento { get; set; }
    }
}
