using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class CuadrillaDetMontacarga
    {
        public int IdCuadrillaDetMontaCarga { get; set; }
        public int? IdCuadrillaEnc { get; set; }
        public int? IdMontacargaBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
    }
}
