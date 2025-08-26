using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwHorarioLaboralDet
    {
        public int IdHorarioLaboralDet { get; set; }
        public int? IdHorarioLaboralEnc { get; set; }
        public int? Dia { get; set; }
        public string NombreDia { get; set; }
        public string NhoraInicio { get; set; }
        public string NhoraFin { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public int? MinimoMinHoraIngreso { get; set; }
        public int? MaximoMinHoraIngreso { get; set; }
        public int? MinimoMinHoraSalida { get; set; }
        public int? MaximoMinHoraSalida { get; set; }
        public int? TiempoRetrasoPermitido { get; set; }
        public bool? HorasExtras { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public DateTime? FechaBaja { get; set; }
        public bool? Activo { get; set; }
    }
}
