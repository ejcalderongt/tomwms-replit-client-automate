using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoEstadoUbic
    {
        public int IdProductoEstadUbic { get; set; }
        public int IdEstado { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }
        public int? IdBodega { get; set; }

        public virtual ProductoEstado IdEstadoNavigation { get; set; }
    }
}
