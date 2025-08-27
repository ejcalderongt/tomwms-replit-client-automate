using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PaisMunicipio
    {
        public PaisMunicipio()
        {
            ClienteDireccions = new HashSet<ClienteDireccion>();
        }

        public int IdMunicipio { get; set; }
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public int? UserAgr { get; set; }
        public int? UserMod { get; set; }

        public virtual PaisDepartamento IdDepartamentoNavigation { get; set; }
        public virtual ICollection<ClienteDireccion> ClienteDireccions { get; set; }
    }
}
