using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoTipo
    {
        public int IdTipoProducto { get; set; }
        public int IdPropietario { get; set; }
        public string Propietario { get; set; }
        public string NombreTipoProducto { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
    }
}
