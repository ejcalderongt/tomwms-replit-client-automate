using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class AjusteMotivo
    {
        public int Idmotivoajuste { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }
        public bool? Sistema { get; set; }
    }
}
