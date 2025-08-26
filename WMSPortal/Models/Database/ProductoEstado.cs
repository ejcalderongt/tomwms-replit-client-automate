using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoEstado
    {
        public ProductoEstado()
        {
            ProductoEstadoUbics = new HashSet<ProductoEstadoUbic>();
            ProductoRellenados = new HashSet<ProductoRellenado>();
            ReglaUbicDetPes = new HashSet<ReglaUbicDetPe>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransReDets = new HashSet<TransReDet>();
            TransTrasDets = new HashSet<TransTrasDet>();
            TransUbicHhDetIdEstadoDestinoNavigations = new HashSet<TransUbicHhDet>();
            TransUbicHhDetIdEstadoOrigenNavigations = new HashSet<TransUbicHhDet>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdEstado { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public bool? Utilizable { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Dañado { get; set; }
        public string CodigoBodegaErp { get; set; }
        public bool? Sistema { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<ProductoEstadoUbic> ProductoEstadoUbics { get; set; }
        public virtual ICollection<ProductoRellenado> ProductoRellenados { get; set; }
        public virtual ICollection<ReglaUbicDetPe> ReglaUbicDetPes { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransTrasDet> TransTrasDets { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDetIdEstadoDestinoNavigations { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDetIdEstadoOrigenNavigations { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
