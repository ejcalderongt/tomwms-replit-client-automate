using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoCodigosBarra
    {
        public int IdProductoCodigoBarra { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public string CodigoBarra { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserAgr { get; set; }
        public bool? Activo { get; set; }
    }
}
