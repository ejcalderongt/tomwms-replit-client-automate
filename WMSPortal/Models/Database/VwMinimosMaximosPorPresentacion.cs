#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMinimosMaximosPorPresentacion
    {
        public int IdPresentacion { get; set; }
        public int IdProducto { get; set; }
        public int? IdBodega { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string NombreProductoPresentacion { get; set; }
        public double? MinimoExistencia { get; set; }
        public double? MaximoExistencia { get; set; }
        public double? Cantidad { get; set; }
    }
}
