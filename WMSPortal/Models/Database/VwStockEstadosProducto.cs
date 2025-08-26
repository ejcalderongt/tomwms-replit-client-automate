#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockEstadosProducto
    {
        public int? IdProductoEstado { get; set; }
        public string Nombre { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdProducto { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public bool? Utilizable { get; set; }
        public bool? Activo { get; set; }
        public bool? Dañado { get; set; }
    }
}
