#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoRack
    {
        public int IdTipoRack { get; set; }
        public string Nombre { get; set; }
        public int? CantidadPosicionesAncho { get; set; }
        public int? CantidadPosicionesProfundo { get; set; }
        public string Descripcion { get; set; }
    }
}
