using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Contenedor
    {
        public int IdContenedor { get; set; }
        public int? IdTipoContenedor { get; set; }
        public string Codigo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
    }
}
