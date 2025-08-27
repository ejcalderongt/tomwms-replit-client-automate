using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PortalMenuRol
    {
        public int IdPortalMenuRol { get; set; }
        public int IdPortalMenu { get; set; }
        public int IdPortalRol { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }

        public virtual PortalMenu IdPortalMenuNavigation { get; set; }
        public virtual PortalRol IdPortalRolNavigation { get; set; }
    }
}
