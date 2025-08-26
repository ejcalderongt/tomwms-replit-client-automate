#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwHorarioLaboralEnc
    {
        public int Código { get; set; }
        public int? IdBodega { get; set; }
        public int? IdJornada { get; set; }
        public int? IdTurno { get; set; }
        public string Jornada { get; set; }
        public string Turno { get; set; }
        public bool? Activo { get; set; }
    }
}
