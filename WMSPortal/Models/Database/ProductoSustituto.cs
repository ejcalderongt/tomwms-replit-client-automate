using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoSustituto
    {
        public int IdProductoSustituto { get; set; }
        public int IdProductoOriginal { get; set; }
        public int? IdProductoPresentacionOriginal { get; set; }
        public int IdProductoReemplazo { get; set; }
        public int? IdProductoPresentacionReemplazo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Producto IdProductoOriginalNavigation { get; set; }
        public virtual ProductoPresentacion IdProductoPresentacionOriginalNavigation { get; set; }
        public virtual Producto IdProductoReemplazoNavigation { get; set; }
    }
}
