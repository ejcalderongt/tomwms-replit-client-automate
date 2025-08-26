using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TempComparacionInventario
    {
        public int? IdInventario { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdUnidadMedida { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public double? CantidadStock { get; set; }
        public double? Cantidad { get; set; }
        public double? PesoStock { get; set; }
        public double? Peso { get; set; }
        public double? EntradasSalidas { get; set; }
        public double? Entradas { get; set; }
        public double? Salidas { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
    }
}
