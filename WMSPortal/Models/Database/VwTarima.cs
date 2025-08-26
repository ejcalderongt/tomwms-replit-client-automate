using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTarima
    {
        public int IdTarima { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoTarima { get; set; }
        public string NombreTipoTarima { get; set; }
        public string Codigo { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? Disponible { get; set; }
    }
}
