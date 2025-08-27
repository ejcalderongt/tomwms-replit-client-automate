using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MenuSistema
    {
        public MenuSistema()
        {
            RolMenus = new HashSet<RolMenu>();
        }

        public string IdMenu { get; set; }
        public string Titulo { get; set; }
        public string NombreLgco { get; set; }
        public int? Nivel { get; set; }
        public string Padre { get; set; }
        public bool? SolicitarClaveAutorizacion { get; set; }

        public virtual ICollection<RolMenu> RolMenus { get; set; }
    }
}
