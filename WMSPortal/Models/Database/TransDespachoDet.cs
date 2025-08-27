using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransDespachoDet
    {
        public int IdDespachoDet { get; set; }
        public int IdDespachoEnc { get; set; }
        public int IdPickingUbic { get; set; }
        public DateTime Fecha { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int? IdPedidoDet { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int? IdPresentacion { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string NombreEstado { get; set; }
        public double? CantidadDespachada { get; set; }
        public double? PesoDespachado { get; set; }
        public int? IdProductoEstado { get; set; }

        public virtual TransDespachoEnc IdDespachoEncNavigation { get; set; }
        public virtual TransPickingUbic IdPickingUbicNavigation { get; set; }
    }
}
