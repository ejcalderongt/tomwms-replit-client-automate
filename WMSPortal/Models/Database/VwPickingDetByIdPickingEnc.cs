using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPickingDetByIdPickingEnc
    {
        public string Bodega { get; set; }
        public string Cliente { get; set; }
        public string Propietario { get; set; }
        public DateTime? FechaPedido { get; set; }
        public int IdPickingDet { get; set; }
        public int IdPickingEnc { get; set; }
        public int IdPedidoDet { get; set; }
        public int? IdOperadorBodega { get; set; }
        public double? Cantidad { get; set; }
        public int? ClienteDias { get; set; }
        public double? CantidadRecibida { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int IdProducto { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public string NombrePresentacion { get; set; }
        public string NombreUnidadMedida { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
}
