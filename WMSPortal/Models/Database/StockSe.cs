using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockSe
    {
        public int IdStockSe { get; set; }
        public int IdStock { get; set; }
        public int? IdProductoBodega { get; set; }
        public string NoSerie { get; set; }
        public string NoSerieInicial { get; set; }
        public string NoSerieFinal { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }

        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual Stock IdStockNavigation { get; set; }
    }
}
