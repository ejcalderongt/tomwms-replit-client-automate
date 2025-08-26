#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwOperador
    {
        public int Código { get; set; }
        public string Empresa { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public double? CostoHora { get; set; }
        public bool? Activo { get; set; }
        public bool? UsaHh { get; set; }
        public byte[] Foto { get; set; }
        public string NombreJornada { get; set; }
        public string Turno { get; set; }
        public string Horario { get; set; }
        public int? HorasJornada { get; set; }
        public bool? Recibe { get; set; }
        public bool? Ubica { get; set; }
        public bool? Transporta { get; set; }
        public bool? Pickea { get; set; }
        public bool? Verifica { get; set; }
    }
}
