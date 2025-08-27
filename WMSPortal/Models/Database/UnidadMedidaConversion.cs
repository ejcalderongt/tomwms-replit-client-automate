#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class UnidadMedidaConversion
    {
        public int IdConversion { get; set; }
        public int? IdUnidadMedidaOrigen { get; set; }
        public int? IdUnidadMedidaDestino { get; set; }
        public double? Factor { get; set; }
        public bool? Activo { get; set; }
    }
}
