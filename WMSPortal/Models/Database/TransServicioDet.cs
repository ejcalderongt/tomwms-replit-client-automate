using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransServicioDet
    {
        public int IdServicioEnc { get; set; }
        public int IdServicioDet { get; set; }
        public string Observacion { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreServicio { get; set; }
        public int? UnidMedida { get; set; }
        public string NombreUnidad { get; set; }
        public int? CorreDetalleacuerdo { get; set; }
        public int? CorreCatalogoproductos { get; set; }
        public int? Cantidad { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public int? IdPropietario { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual TransServicioEnc IdServicioEncNavigation { get; set; }
    }
}
