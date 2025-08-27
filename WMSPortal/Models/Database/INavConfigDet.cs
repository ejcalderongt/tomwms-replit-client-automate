using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavConfigDet
    {
        public int Idnavconfigdet { get; set; }
        public int? Idnavent { get; set; }
        public int Idnavconfigenc { get; set; }
        public int? Dia { get; set; }
        public DateTime? Horainicio { get; set; }
        public DateTime? Horafin { get; set; }
        public int? Frecuencia { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }

        public virtual INavConfigEnc IdnavconfigencNavigation { get; set; }
        public virtual INavEnt IdnaventNavigation { get; set; }
    }
}
