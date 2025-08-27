using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwNavdetalleconfiguracion
    {
        public int Correlativo { get; set; }
        public int? Dia { get; set; }
        public DateTime? Horainicio { get; set; }
        public int? Frecuencia { get; set; }
        public string Entidad { get; set; }
        public int Idnavconfigenc { get; set; }
        public string NombreDia { get; set; }
        public DateTime? Horafin { get; set; }
    }
}
