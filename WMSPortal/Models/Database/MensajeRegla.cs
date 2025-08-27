using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MensajeRegla
    {
        public MensajeRegla()
        {
            PropietarioReglasEncs = new HashSet<PropietarioReglasEnc>();
        }

        public int IdMensajeRegla { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<PropietarioReglasEnc> PropietarioReglasEncs { get; set; }
    }
}
