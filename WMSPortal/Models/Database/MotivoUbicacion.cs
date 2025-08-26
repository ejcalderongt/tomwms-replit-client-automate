using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MotivoUbicacion
    {
        public int IdEmpresa { get; set; }
        public int IdMotivoUbicacion { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
    }
}
