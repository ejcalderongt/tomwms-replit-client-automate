using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPedidosDm
    {
        public string Codigo { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string CodigoProducto { get; set; }
        public double? Cantidad { get; set; }
    }
}
