using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicDetTp
    {
        public int IdReglaUbicacoinTp { get; set; }
        public int? IdReglaUbicacionEnc { get; set; }
        public int? IdTipoProducto { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual ReglaUbicEnc IdReglaUbicacionEncNavigation { get; set; }
        public virtual ProductoTipo IdTipoProductoNavigation { get; set; }
    }
}
