using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TProductoBodega
    {
        public int IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public bool? Sistema { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
    }
}
