using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMotivoAnulacion
    {
        public string Empresa { get; set; }
        public int IdMotivoAnulacion { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
    }
}
