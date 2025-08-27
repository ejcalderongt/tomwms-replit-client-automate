using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Rol
    {
        public Rol()
        {
            MenuRols = new HashSet<MenuRol>();
            RolBodegas = new HashSet<RolBodega>();
            RolMenus = new HashSet<RolMenu>();
        }

        public int IdRol { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? RegistrarClaveAutorizacion { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<MenuRol> MenuRols { get; set; }
        public virtual ICollection<RolBodega> RolBodegas { get; set; }
        public virtual ICollection<RolMenu> RolMenus { get; set; }
    }
}
