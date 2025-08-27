using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VTransPedido
    {
        public long IdPedidoEnc { get; set; }
        public long? NoDocumento { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string Cliente { get; set; }
        public string Estado { get; set; }
        public string Bodega { get; set; }
        public string Muelle { get; set; }
        public string Propietario { get; set; }
        public string RoadVendedor { get; set; }
        public string RoadRuta { get; set; }
        public bool? Anualdo { get; set; }
        public bool? Activo { get; set; }
    }
}
