using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockParametro
    {
        public int IdStockParametro { get; set; }
        public int IdStock { get; set; }
        public int IdProductoParametro { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLogico { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public bool? Activo { get; set; }

        public virtual ProductoParametro IdProductoParametroNavigation { get; set; }
        public virtual Stock IdStockNavigation { get; set; }
    }
}
