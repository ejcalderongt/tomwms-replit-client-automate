#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwUbicacionesPicking
    {
        public int IdBodega { get; set; }
        public int IdUbicacion { get; set; }
        public string Descripcion { get; set; }
        public string Tramo { get; set; }
        public string Sector { get; set; }
        public string Area { get; set; }
        public int? Nivel { get; set; }
        public int? IndiceX { get; set; }
        public string CodigoBarra { get; set; }
        public bool? Activo { get; set; }
        public bool? Bloqueada { get; set; }
        public bool? AceptaPallet { get; set; }
    }
}
