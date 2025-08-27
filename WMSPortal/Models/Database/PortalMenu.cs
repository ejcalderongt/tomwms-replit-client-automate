using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PortalMenu
    {
        public PortalMenu()
        {
            InversePadreNavigation = new HashSet<PortalMenu>();
            PortalMenuRols = new HashSet<PortalMenuRol>();
        }

        public int IdPortalMenu { get; set; }
        public string Nombre { get; set; }
        public string Enlace { get; set; }
        public string Icono { get; set; }
        public int? Padre { get; set; }
        public bool? Activo { get; set; }

        public virtual PortalMenu PadreNavigation { get; set; }
        public virtual ICollection<PortalMenu> InversePadreNavigation { get; set; }
        public virtual ICollection<PortalMenuRol> PortalMenuRols { get; set; }
    }
}
