using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavConfigEnt
    {
        public int Idnavconfigent { get; set; }
        public int? Idnavent { get; set; }
        public string Endpoint { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public int? UserMod { get; set; }

        public virtual INavEnt IdnaventNavigation { get; set; }
    }
}
