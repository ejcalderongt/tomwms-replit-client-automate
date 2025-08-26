#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicacion
    {
        public int IdUbicacion { get; set; }
        public int IdReglaUbicacionEnc { get; set; }
        public int? IdBodega { get; set; }

        public virtual ReglaUbicEnc IdReglaUbicacionEncNavigation { get; set; }
    }
}
