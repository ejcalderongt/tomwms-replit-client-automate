#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwBodegaMuelle
    {
        public int Código { get; set; }
        public string Bodega { get; set; }
        public string Muelle { get; set; }
        public bool? Activo { get; set; }
    }
}
