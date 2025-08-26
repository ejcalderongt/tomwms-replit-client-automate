using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavEjecucionEnc
    {
        public int Idejecucionenc { get; set; }
        public int? Idnavconfigenc { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Exitosa { get; set; }
    }
}
