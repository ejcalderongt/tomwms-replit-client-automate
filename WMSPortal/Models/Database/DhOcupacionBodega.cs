using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class DhOcupacionBodega
    {
        public int IdOcupacionBodega { get; set; }
        public int? IdBodega { get; set; }
        public double? CantUbicacionesVacias { get; set; }
        public double? CantUbicacionesOcupadas { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
