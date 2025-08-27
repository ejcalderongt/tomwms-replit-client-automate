using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwGetAllStockDetalleResuman
    {
        public int IdStock { get; set; }
        public int IdProducto { get; set; }
        public int IdProductoBodega { get; set; }
        public string Codigo { get; set; }
        public string Propietario { get; set; }
        public string Producto { get; set; }
        public string Barra { get; set; }
        public string Estado { get; set; }
        public string Presentación { get; set; }
        public string Umbas { get; set; }
        public string Serial { get; set; }
        public double? CantPresentacion { get; set; }
        public double? CantUmbas { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public string Lote { get; set; }
        public int? NoRecepcion { get; set; }
        public int IdUbicacion { get; set; }
        public string Tramo { get; set; }
        public string Ubicacion { get; set; }
        public double? Largo { get; set; }
    }
}
