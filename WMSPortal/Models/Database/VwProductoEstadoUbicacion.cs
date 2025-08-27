using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoEstadoUbicacion
    {
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public int IdEstado { get; set; }
        public int IdProductoEstadUbic { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public int? IdBodega { get; set; }
        public string Bodega { get; set; }
    }
}
