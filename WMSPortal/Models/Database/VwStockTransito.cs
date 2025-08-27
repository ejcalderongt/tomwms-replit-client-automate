#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockTransito
    {
        public int OrdenDeCompra { get; set; }
        public string Propietario { get; set; }
        public string Producto { get; set; }
        public string Presentaciòn { get; set; }
        public string Proveedor { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? CantidadPendiente { get; set; }
        public double? Costo { get; set; }
        public double? Total { get; set; }
    }
}
