#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavAcuerdo
    {
        public int IdAcuerdo { get; set; }
        public int? IdCliente { get; set; }
        public string CodigoAcuerdo { get; set; }
        public string Descripcion { get; set; }
        public string TipoCobro { get; set; }
        public int CodMoneda { get; set; }
        public string NomMoneda { get; set; }
    }
}
