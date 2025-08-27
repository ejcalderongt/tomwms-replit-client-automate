using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TmpHouseDatum
    {
        public string CodigoCliente { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string CodigoProducto { get; set; }
        public double? Cantidad { get; set; }
    }
}
