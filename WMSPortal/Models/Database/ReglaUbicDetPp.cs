using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicDetPp
    {
        public int IdReglaUbicacionDetPp { get; set; }
        public int? IdReglaUbicacionEnc { get; set; }
        public int? IdPresentacion { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ReglaUbicEnc IdReglaUbicacionEncNavigation { get; set; }
    }
}
