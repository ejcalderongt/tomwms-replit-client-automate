#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicPrioProducto
    {
        public int IdReglaUbicPrioProd { get; set; }
        public int? IdReglaUbicPrioEnc { get; set; }
        public int? IdProducto { get; set; }
    }
}
