using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MenuRolOp
    {
        public string IdMenuSistemaOp { get; set; }
        public int IdRolOperador { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Visible { get; set; }
        public bool? Activo { get; set; }

        public virtual MenuSistemaOp IdMenuSistemaOpNavigation { get; set; }
        public virtual RolOperador IdRolOperadorNavigation { get; set; }
    }
}
