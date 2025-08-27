using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwOperadorHorario
    {
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public int IdOperador { get; set; }
        public int IdJornada { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdHorarioLaboralEnc { get; set; }
        public int? Dia { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public int? MinimoMinHoraIngreso { get; set; }
        public int? MaximoMinHoraIngreso { get; set; }
        public int? MinimoMinHoraSalida { get; set; }
        public int? MaximoMinHoraSalida { get; set; }
        public int? TiempoRetrasoPermitido { get; set; }
        public bool? HorasExtras { get; set; }
        public bool? OperadorActivo { get; set; }
        public bool? OperadorBodegaActivo { get; set; }
        public bool? JornadaLaboralActivo { get; set; }
        public bool? HorarioActivo { get; set; }
        public bool? HorarioLaboralDetActivo { get; set; }
        public bool TurnoActivo { get; set; }
    }
}
