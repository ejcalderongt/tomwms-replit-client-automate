using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PropietarioDestinatario
    {
        public PropietarioDestinatario()
        {
            PropietarioReglasDets = new HashSet<PropietarioReglasDet>();
        }

        public int IdDestinatarioPropietario { get; set; }
        public int? IdPropietario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Telefono1 { get; set; }
        public string Cargo { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<PropietarioReglasDet> PropietarioReglasDets { get; set; }
    }
}
