#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoEstadoUbicDefecto
    {
        public int IdUbicacion { get; set; }
        public int IdEstado { get; set; }
        public int IdBodega { get; set; }
        public string NombreUbic { get; set; }
    }
}
