using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class UnidadMedidum
    {
        public UnidadMedidum()
        {
            Productos = new HashSet<Producto>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransOcDets = new HashSet<TransOcDet>();
            TransPeDets = new HashSet<TransPeDet>();
            TransReDets = new HashSet<TransReDet>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdUnidadMedida { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserAgr { get; set; }
        public string Codigo { get; set; }
        public bool? EsUmCobro { get; set; }
        public double? Factor { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
        public virtual ICollection<TransPeDet> TransPeDets { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
