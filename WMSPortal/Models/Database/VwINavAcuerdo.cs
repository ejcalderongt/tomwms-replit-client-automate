#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwINavAcuerdo
    {
        public int IdCliente { get; set; }
        public int IdContrato { get; set; }
        public int IdAcuerdo { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoAcuerdo { get; set; }
        public string Descripcion { get; set; }
    }
}
