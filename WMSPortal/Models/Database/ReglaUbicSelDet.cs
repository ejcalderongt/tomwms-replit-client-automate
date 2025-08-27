#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicSelDet
    {
        public int IdReglaUbicOrd { get; set; }
        public int IdRegla { get; set; }
        public int? Orden { get; set; }
        public bool? Activo { get; set; }

        public virtual ReglaUbicSelEnc IdReglaUbicOrdNavigation { get; set; }
    }
}
