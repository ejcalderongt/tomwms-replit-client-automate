using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavEjecucionDetError
    {
        public int Idejecuciondet { get; set; }
        public int? Idejecucionenc { get; set; }
        public int? Idnavconfigdet { get; set; }
        public string Error { get; set; }
        public DateTime? Fecha { get; set; }
        public string Referencia { get; set; }
        public bool? EsBodegaRecepcion { get; set; }
        public DateTime? FecAgr { get; set; }
    }
}
