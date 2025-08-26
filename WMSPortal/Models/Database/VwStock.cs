#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStock
    {
        public int IdProductoBodega { get; set; }
        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public string NombrePropietario { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string NombreUnidadMedida { get; set; }
        public string NombrePresentacion { get; set; }
        public string CodigoBarraPresentacion { get; set; }
        public string NombreEstadoProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public double? Precio { get; set; }
    }
}
