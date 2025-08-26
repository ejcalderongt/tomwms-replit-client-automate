using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class AjusteTipo
    {
        public int Idtipoajuste { get; set; }
        public string Nombre { get; set; }
        public bool? ModificaLote { get; set; }
        public bool? MomdificaVencimiento { get; set; }
        public bool? ModificaCantidad { get; set; }
        public bool? ModificaPeso { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }
    }
}
