using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoFamilium
    {
        public ProductoFamilium()
        {
            ClienteTiempos = new HashSet<ClienteTiempo>();
            Productos = new HashSet<Producto>();
        }

        public int IdFamilia { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<ClienteTiempo> ClienteTiempos { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
