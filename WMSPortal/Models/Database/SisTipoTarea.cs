using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SisTipoTarea
    {
        public SisTipoTarea()
        {
            MenuSistemaOps = new HashSet<MenuSistemaOp>();
            TareaHhs = new HashSet<TareaHh>();
            TransMovimientos = new HashSet<TransMovimiento>();
        }

        public int IdTipoTarea { get; set; }
        public string Nombre { get; set; }
        public bool? Contabilizar { get; set; }

        public virtual ICollection<MenuSistemaOp> MenuSistemaOps { get; set; }
        public virtual ICollection<TareaHh> TareaHhs { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
    }
}
