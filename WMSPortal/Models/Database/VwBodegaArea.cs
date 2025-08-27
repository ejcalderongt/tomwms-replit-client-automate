using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwBodegaArea
    {
        public int IdArea { get; set; }
        public int IdBodega { get; set; }
        public string Descripcion { get; set; }
        public bool? Sistema { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Codigo { get; set; }
    }
}
