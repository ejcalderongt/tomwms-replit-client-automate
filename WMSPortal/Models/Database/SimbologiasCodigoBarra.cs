using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SimbologiasCodigoBarra
    {
        public SimbologiasCodigoBarra()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdSimbologia { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
