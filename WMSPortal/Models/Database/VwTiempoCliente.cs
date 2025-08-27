using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTiempoCliente
    {
        public string Familia { get; set; }
        public string Clasificación { get; set; }
        public int IdTiempoCliente { get; set; }
        public int IdCliente { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdClasificacion { get; set; }
        public int? DiasLocal { get; set; }
        public int? DiasExterior { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
