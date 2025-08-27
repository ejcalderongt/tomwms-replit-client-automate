using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoParametro
    {
        public ProductoParametro()
        {
            StockParametros = new HashSet<StockParametro>();
            TransPickingDetParametros = new HashSet<TransPickingDetParametro>();
            TransReDetParametros = new HashSet<TransReDetParametro>();
        }

        public int IdProductoParametro { get; set; }
        public int IdParametro { get; set; }
        public int IdProducto { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLogico { get; set; }
        public bool CapturarSiempre { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }

        public virtual PParametro IdParametroNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual ICollection<StockParametro> StockParametros { get; set; }
        public virtual ICollection<TransPickingDetParametro> TransPickingDetParametros { get; set; }
        public virtual ICollection<TransReDetParametro> TransReDetParametros { get; set; }
    }
}
