using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoPresentacionesConversione
    {
        public int IdConversion { get; set; }
        public int? IdPresentacionOrigen { get; set; }
        public int? IdPresentacionDestino { get; set; }
        public double? Factor { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserAgr { get; set; }
        public bool? Inverso { get; set; }

        public virtual ProductoPresentacion IdPresentacionOrigenNavigation { get; set; }
    }
}
