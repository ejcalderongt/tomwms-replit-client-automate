using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class IndiceRotacion
    {
        public IndiceRotacion()
        {
            Productos = new HashSet<Producto>();
            ReglaUbicDetIrs = new HashSet<ReglaUbicDetIr>();
        }

        public int IdIndiceRotacion { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int? IndicePrioridad { get; set; }
        public int? Grupo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<ReglaUbicDetIr> ReglaUbicDetIrs { get; set; }
    }
}
