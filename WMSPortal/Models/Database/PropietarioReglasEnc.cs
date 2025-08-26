using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PropietarioReglasEnc
    {
        public PropietarioReglasEnc()
        {
            PropietarioReglasDets = new HashSet<PropietarioReglasDet>();
            TransReDetInfraccions = new HashSet<TransReDetInfraccion>();
        }

        public int IdReglaPropietarioEnc { get; set; }
        public int? IdReglaRecepcion { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdMensajeRegla { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual MensajeRegla IdMensajeReglaNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ReglasRecepcion IdReglaRecepcionNavigation { get; set; }
        public virtual ICollection<PropietarioReglasDet> PropietarioReglasDets { get; set; }
        public virtual ICollection<TransReDetInfraccion> TransReDetInfraccions { get; set; }
    }
}
