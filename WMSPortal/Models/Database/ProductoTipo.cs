using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoTipo
    {
        public ProductoTipo()
        {
            Productos = new HashSet<Producto>();
            ReglaUbicDetTps = new HashSet<ReglaUbicDetTp>();
        }

        public int IdTipoProducto { get; set; }
        public int IdPropietario { get; set; }
        public string NombreTipoProducto { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<ReglaUbicDetTp> ReglaUbicDetTps { get; set; }
    }
}
