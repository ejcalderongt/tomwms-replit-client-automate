using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoBodega
    {
        public ProductoBodega()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            StockSeRecs = new HashSet<StockSeRec>();
            StockSes = new HashSet<StockSe>();
            Stocks = new HashSet<Stock>();
            TransInvCiclicos = new HashSet<TransInvCiclico>();
            TransInvReconteos = new HashSet<TransInvReconteo>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransOcDets = new HashSet<TransOcDet>();
            TransPeDets = new HashSet<TransPeDet>();
            TransReDetInfraccions = new HashSet<TransReDetInfraccion>();
            TransReDets = new HashSet<TransReDet>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public bool? Sistema { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<StockSeRec> StockSeRecs { get; set; }
        public virtual ICollection<StockSe> StockSes { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransInvCiclico> TransInvCiclicos { get; set; }
        public virtual ICollection<TransInvReconteo> TransInvReconteos { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
        public virtual ICollection<TransPeDet> TransPeDets { get; set; }
        public virtual ICollection<TransReDetInfraccion> TransReDetInfraccions { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
