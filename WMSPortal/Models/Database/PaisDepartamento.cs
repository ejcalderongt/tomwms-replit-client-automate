using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PaisDepartamento
    {
        public PaisDepartamento()
        {
            PaisMunicipios = new HashSet<PaisMunicipio>();
        }

        public int IdDepartamento { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public int? UserAgr { get; set; }
        public int? UserMod { get; set; }

        public virtual Paise IdPaisNavigation { get; set; }
        public virtual ICollection<PaisMunicipio> PaisMunicipios { get; set; }
    }
}
