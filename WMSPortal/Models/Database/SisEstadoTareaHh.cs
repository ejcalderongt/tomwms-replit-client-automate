using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SisEstadoTareaHh
    {
        public SisEstadoTareaHh()
        {
            TareaHhs = new HashSet<TareaHh>();
        }

        public int IdEstado { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TareaHh> TareaHhs { get; set; }
    }
}
