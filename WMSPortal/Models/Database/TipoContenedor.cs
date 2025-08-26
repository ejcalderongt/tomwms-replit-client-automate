using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoContenedor
    {
        public int IdTipoContenedor { get; set; }
        public string Nombre { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? Alto { get; set; }
        public double? Pies { get; set; }
        public double? Tonealadas { get; set; }
        public double? VolumenUtil { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }
        public double? Tara { get; set; }
    }
}
