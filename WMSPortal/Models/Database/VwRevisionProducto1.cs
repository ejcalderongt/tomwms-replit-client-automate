#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRevisionProducto1
    {
        public string Producto { get; set; }
        public string Presentación { get; set; }
        public string Estado { get; set; }
        public string Ubicación { get; set; }
        public double? Mínimo { get; set; }
        public double? Máximo { get; set; }
        public double? Disponible { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdPropietario { get; set; }
        public int? IdBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdPresentacion { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdUnidadMedida { get; set; }
        public double Factor { get; set; }
    }
}
