using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MontacargaBodega
    {
        public int IdMontacargaBodega { get; set; }
        public int? IdMontacarga { get; set; }
        public int? IdBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
