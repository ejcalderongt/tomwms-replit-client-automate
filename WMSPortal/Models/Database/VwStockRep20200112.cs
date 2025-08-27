#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockRep20200112
    {
        public string Tipo { get; set; }
        public double? CantidadSf { get; set; }
        public string UbicacionTramo { get; set; }
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public string Propietario { get; set; }
        public double? Cantidad { get; set; }
        public int? IdBodega { get; set; }
    }
}
