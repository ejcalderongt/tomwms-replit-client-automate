using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicDetPe
    {
        public int IdReglaUbicacionDetPe { get; set; }
        public int? IdReglaUbicacionEnc { get; set; }
        public int? IdEstado { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual ProductoEstado IdEstadoNavigation { get; set; }
        public virtual ReglaUbicEnc IdReglaUbicacionEncNavigation { get; set; }
    }
}
