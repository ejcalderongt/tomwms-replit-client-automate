using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransUbicHhSe
    {
        public int IdTareaUbicacionDetSe { get; set; }
        public int IdTareaUbicacionEnc { get; set; }
        public int? IdStockSe { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdOperador { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public bool? Realizado { get; set; }
        public double? Cantidad { get; set; }
        public bool? Activo { get; set; }
    }
}
