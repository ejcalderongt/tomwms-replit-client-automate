using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RolMenu
    {
        public int IdMenuRol { get; set; }
        public int IdRol { get; set; }
        public string IdMenu { get; set; }
        public int Visible { get; set; }
        public string UsuarioAgr { get; set; }
        public DateTime? FecAgr { get; set; }

        public virtual MenuSistema IdMenuNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
