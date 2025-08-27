#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ConfiguracionBarraPallet
    {
        public int IdConfiguracionPallet { get; set; }
        public int? LongCodBodegaOrigen { get; set; }
        public int? LongCodProducto { get; set; }
        public int? LongLp { get; set; }
        public bool? CodigoNumerico { get; set; }
        public string IdentificadorInicio { get; set; }
    }
}
