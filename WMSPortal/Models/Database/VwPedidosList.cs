using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPedidosList
    {
        public int Correlativo { get; set; }
        public int? IdBodega { get; set; }
        public long? NoDocumento { get; set; }
        public string Referencia { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string Cliente { get; set; }
        public string Estado { get; set; }
        public string Bodega { get; set; }
        public string Muelle { get; set; }
        public string Propietario { get; set; }
        public string RoadVendedor { get; set; }
        public string RoadRuta { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Anulado { get; set; }
        public bool? Activo { get; set; }
        public bool? EnviadoAErp { get; set; }
        public DateTime? FecAgr { get; set; }
    }
}
