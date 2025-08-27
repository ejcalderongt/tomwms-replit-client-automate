using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PerfilSerializado
    {
        public PerfilSerializado()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdPerfilSerializado { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
