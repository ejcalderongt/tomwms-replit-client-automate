using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockResSe
    {
        public int IdStockResSe { get; set; }
        public int? IdTransaccion { get; set; }
        public string Indicador { get; set; }
        public int? IdStockSe { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }
    }
}
