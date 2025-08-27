using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTarimasUsadasEnTransaccion
    {
        public int IdTarimaTareaUbic { get; set; }
        public int? IdTareaUbicacionEnc { get; set; }
        public int? IdTarima { get; set; }
        public string CodigoTarima { get; set; }
        public string NombreTipoTarima { get; set; }
        public string Codigo { get; set; }
        public bool? Utilizada { get; set; }
        public DateTime? FechaUtilizacion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
    }
}
