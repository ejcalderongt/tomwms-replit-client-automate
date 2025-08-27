using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockSerieParametro
    {
        public int StockId { get; set; }
        public string Propietario { get; set; }
        public string Producto { get; set; }
        public string CódigoProducto { get; set; }
        public string CódigoDeBarra { get; set; }
        public string Estado { get; set; }
        public string Presentación { get; set; }
        public string UMBas { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public int? Recepción { get; set; }
    }
}
