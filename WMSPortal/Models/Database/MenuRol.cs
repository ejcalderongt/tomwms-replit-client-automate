using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MenuRol
    {
        public int IdMenuRol { get; set; }
        public string IdMenu { get; set; }
        public int IdRol { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
        public bool Visible { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
