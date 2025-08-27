#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicPrioParam
    {
        public int IdReglaUbicPrioParam { get; set; }
        public string Nombre { get; set; }
        public int? Activo { get; set; }
        public int? Orden { get; set; }
        public int? Tipo { get; set; }
    }
}
