using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoRotacion
    {
        public TipoRotacion()
        {
            BodegaUbicacions = new HashSet<BodegaUbicacion>();
            Productos = new HashSet<Producto>();
            ReglaUbicDetTrs = new HashSet<ReglaUbicDetTr>();
        }

        public int IdTipoRotacion { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<BodegaUbicacion> BodegaUbicacions { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<ReglaUbicDetTr> ReglaUbicDetTrs { get; set; }
    }
}
