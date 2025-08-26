using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwComparativoNavWmsConCosto
    {
        public int IdInventario { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Umbas { get; set; }
        public double? StockWms { get; set; }
        public double? TeoricoErp { get; set; }
        public double? DifErp { get; set; }
        public double? Conteo { get; set; }
        public double? DifConteo { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? CostoNav { get; set; }
        public double? CostoConteo { get; set; }
        public double? DifCosto { get; set; }
        public double? Costo { get; set; }
    }
}
