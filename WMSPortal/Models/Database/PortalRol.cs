using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PortalRol
    {
        public PortalRol()
        {
            PortalMenuRols = new HashSet<PortalMenuRol>();
        }

        public int IdPortalRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<PortalMenuRol> PortalMenuRols { get; set; }
    }
}
