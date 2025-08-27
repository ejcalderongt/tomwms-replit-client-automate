#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwInventarioPrgPorTipo
    {
        public string NombreTipoProducto { get; set; }
        public int Idinventarioenc { get; set; }
        public double? Teórico { get; set; }
        public double? Contado { get; set; }
    }
}
