using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglasRecepcion
    {
        public ReglasRecepcion()
        {
            PropietarioReglasEncs = new HashSet<PropietarioReglasEnc>();
        }

        public int IdReglaRecepcion { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Rechazar { get; set; }
        public bool? StockNoDisponible { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<PropietarioReglasEnc> PropietarioReglasEncs { get; set; }
    }
}
