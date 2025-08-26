using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MenuSistemaOp
    {
        public MenuSistemaOp()
        {
            MenuRolOps = new HashSet<MenuRolOp>();
        }

        public string IdMenuSistemaOp { get; set; }
        public int? IdTipoTarea { get; set; }
        public string Nombre { get; set; }
        public int? Nivel { get; set; }
        public string Padre { get; set; }
        public int? Posicion { get; set; }

        public virtual SisTipoTarea IdTipoTareaNavigation { get; set; }
        public virtual ICollection<MenuRolOp> MenuRolOps { get; set; }
    }
}
