using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class HorarioLaboralEnc
    {
        public HorarioLaboralEnc()
        {
            HorarioLaboralDets = new HashSet<HorarioLaboralDet>();
        }

        public int IdHorarioLaboralEnc { get; set; }
        public int? IdBodega { get; set; }
        public int? IdJornada { get; set; }
        public int? IdTurno { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual JornadaLaboral IdJornadaNavigation { get; set; }
        public virtual Turno IdTurnoNavigation { get; set; }
        public virtual ICollection<HorarioLaboralDet> HorarioLaboralDets { get; set; }
    }
}
