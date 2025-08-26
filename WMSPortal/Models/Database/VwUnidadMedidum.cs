using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwUnidadMedidum
    {
        public string Propietario { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserAgr { get; set; }
        public string Codigo { get; set; }
        public bool? EsUmCobro { get; set; }
        public double? Factor { get; set; }
    }
}
