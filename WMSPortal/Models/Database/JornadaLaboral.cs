using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class JornadaLaboral
    {
        public JornadaLaboral()
        {
            HorarioLaboralEncs = new HashSet<HorarioLaboralEnc>();
            Operadors = new HashSet<Operador>();
        }

        public int IdJornada { get; set; }
        public int? IdBodega { get; set; }
        public string NombreJornada { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? HorasTrabajadas { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public DateTime? FechaBaja { get; set; }
        public bool? Activo { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual ICollection<HorarioLaboralEnc> HorarioLaboralEncs { get; set; }
        public virtual ICollection<Operador> Operadors { get; set; }
    }
}
