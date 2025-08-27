using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoEstado
    {
        public string Propietario { get; set; }
        public int IdEstado { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public bool? Utilizable { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Dañado { get; set; }
    }
}
