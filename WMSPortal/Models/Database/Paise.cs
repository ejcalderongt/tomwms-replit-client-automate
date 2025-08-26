using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Paise
    {
        public Paise()
        {
            Bodegas = new HashSet<Bodega>();
            PaisDepartamentos = new HashSet<PaisDepartamento>();
            PaisRegions = new HashSet<PaisRegion>();
        }

        public int IdPais { get; set; }
        public int? Isonum { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Bodega> Bodegas { get; set; }
        public virtual ICollection<PaisDepartamento> PaisDepartamentos { get; set; }
        public virtual ICollection<PaisRegion> PaisRegions { get; set; }
    }
}
