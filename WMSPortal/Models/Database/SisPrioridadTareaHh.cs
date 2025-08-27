using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SisPrioridadTareaHh
    {
        public SisPrioridadTareaHh()
        {
            TareaHhs = new HashSet<TareaHh>();
        }

        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TareaHh> TareaHhs { get; set; }
    }
}
