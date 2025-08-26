using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPickingUbicacion
    {
        public int Pedido { get; set; }
        public int IdPicking { get; set; }
        public string Código { get; set; }
        public string Producto { get; set; }
        public string Presentación { get; set; }
        public string Estado { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicación { get; set; }
        public double? Solicitado { get; set; }
        public double? Picking { get; set; }
        public double? Verificado { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? FechaPicking { get; set; }
        public DateTime? Vence { get; set; }
        public bool? Activo { get; set; }
        public int IdPickingUbic { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int IdPedidoDet { get; set; }
    }
}
