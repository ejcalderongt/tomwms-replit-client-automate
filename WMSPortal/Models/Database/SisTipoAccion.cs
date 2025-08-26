using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SisTipoAccion
    {
        public SisTipoAccion()
        {
            ProductoRellenados = new HashSet<ProductoRellenado>();
        }

        public int IdTipoAccion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ProductoRellenado> ProductoRellenados { get; set; }
    }
}
