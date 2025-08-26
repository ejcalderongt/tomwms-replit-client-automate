using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwDespachoDetalle
    {
        public int IdDespachoEnc { get; set; }
        public int IdDespachoDet { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public string Presentacion { get; set; }
        public string Estado { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicacion { get; set; }
        public bool? Activo { get; set; }
        public int IdPickingUbic { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public string FecMod { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPedidoEnc { get; set; }
        public int IdPedidoDet { get; set; }
    }
}
