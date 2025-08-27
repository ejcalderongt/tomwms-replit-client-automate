#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoDimension
    {
        public int IdStock { get; set; }
        public int IdUbicacion { get; set; }
        public int? IdProducto { get; set; }
        public string Nombre { get; set; }
        public double? Cantidad { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? Dimension { get; set; }
        public string Umbas { get; set; }
        public string Presentacion { get; set; }
        public double? Factor { get; set; }
    }
}
