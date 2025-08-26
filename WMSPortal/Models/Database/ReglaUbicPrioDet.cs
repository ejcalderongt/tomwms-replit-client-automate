#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicPrioDet
    {
        public int IdReglaUbicPrioDet { get; set; }
        public int? IdReglaUbicPrioParam { get; set; }
        public int? IdReglaUbicPrioEnc { get; set; }
        public int? Orden { get; set; }
        public bool? Activo { get; set; }
    }
}
