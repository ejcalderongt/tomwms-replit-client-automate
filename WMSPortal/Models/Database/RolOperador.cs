using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RolOperador
    {
        public RolOperador()
        {
            MenuRolOps = new HashSet<MenuRolOp>();
        }

        public int IdRolOperador { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<MenuRolOp> MenuRolOps { get; set; }
    }
}
