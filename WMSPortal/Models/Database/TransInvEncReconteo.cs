using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvEncReconteo
    {
        public int Idinvencreconteo { get; set; }
        public int Idinventarioenc { get; set; }
        public int? Reconteo { get; set; }
        public string Estado { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual TransInvEnc IdinventarioencNavigation { get; set; }
    }
}
