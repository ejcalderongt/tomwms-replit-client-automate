#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MontacargaTipoFalla
    {
        public string IdTipoFalla { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public int? Activo { get; set; }
    }
}
