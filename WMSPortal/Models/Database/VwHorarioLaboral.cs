#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwHorarioLaboral
    {
        public int? IdBodega { get; set; }
        public int? Dia { get; set; }
        public int? IdJornada { get; set; }
        public int Código { get; set; }
        public string Día { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool? Activo { get; set; }
    }
}
