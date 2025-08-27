using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwUbicacionPicking
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdPedidoEnc { get; set; }
        public int IdPickingEnc { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? FechaPicking { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public DateTime? Vence { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public string Presentación { get; set; }
        public string Operador { get; set; }
        public string Lote { get; set; }
        public double? Solicitado { get; set; }
        public double? Picking { get; set; }
        public double? Verificado { get; set; }
    }
}
