using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SisObsLog
    {
        public SisObsLog()
        {
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdObservacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
