#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwCuadrilla
    {
        public int IdCuadrillaEnc { get; set; }
        public string Bodega { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }
    }
}
