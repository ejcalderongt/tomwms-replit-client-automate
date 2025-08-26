using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RolBodega
    {
        public int IdRolBodega { get; set; }
        public int? IdRol { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
