using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Marcaje
    {
        public int IdMarcaje { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBodega { get; set; }
        public int IdOperador { get; set; }
        public string IdDispositivo { get; set; }
        public int IdHorarioLaboral { get; set; }
        public DateTime? FecLectura { get; set; }
        public DateTime? HoraInicioHorario { get; set; }
        public DateTime? HoraFinHorario { get; set; }
        public bool? IngresoAnticipado { get; set; }
        public bool? SalidaAnticipada { get; set; }
        public bool? IngresoTardio { get; set; }
        public bool? SalidaTardia { get; set; }
        public DateTime? HoraLectura { get; set; }
        public bool Entro { get; set; }
        public bool Salio { get; set; }
        public DateTime? HoraEntro { get; set; }
        public DateTime? HoraSalio { get; set; }
        public bool? MarcajeManual { get; set; }
        public int? PrimerMarcaje { get; set; }
        public bool? MarcajeContabilizado { get; set; }
        public bool? MarcajeAproximado { get; set; }
        public bool? MarcajeFueraDeSucursal { get; set; }
        public bool? EsBitacora { get; set; }
    }
}
