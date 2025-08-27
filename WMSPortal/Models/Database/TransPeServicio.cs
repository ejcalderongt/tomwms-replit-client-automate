using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPeServicio
    {
        public int IdOrdenPedidoServicio { get; set; }
        public int IdOrdenPedidoEnc { get; set; }
        public int IdServicio { get; set; }
        public int? Cantidad { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Observacion { get; set; }
    }
}
