using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ClienteTipo
    {
        public ClienteTipo()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int IdTipoCliente { get; set; }
        public int IdPropietario { get; set; }
        public string NombreTipoCliente { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
