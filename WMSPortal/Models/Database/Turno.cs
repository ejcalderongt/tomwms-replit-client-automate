using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Turno
    {
        public Turno()
        {
            HorarioLaboralEncs = new HashSet<HorarioLaboralEnc>();
        }

        public int IdTurno { get; set; }
        public int IdBodega { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual ICollection<HorarioLaboralEnc> HorarioLaboralEncs { get; set; }
    }
}
