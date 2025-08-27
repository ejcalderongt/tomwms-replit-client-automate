using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class HorarioLaboralDet
    {
        public int IdHorarioLaboralDet { get; set; }
        public int? IdHorarioLaboralEnc { get; set; }
        public int? Dia { get; set; }
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

        public virtual HorarioLaboralEnc IdHorarioLaboralEncNavigation { get; set; }
    }
}
