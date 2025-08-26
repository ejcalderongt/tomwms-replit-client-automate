#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwConfiguracioninv
    {
        public int Correlativo { get; set; }
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Bodega { get; set; }
        public int Idempresa { get; set; }
        public int Idbodega { get; set; }
        public string Propietario { get; set; }
        public int? IdPropietario { get; set; }
    }
}
