using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoPresentacion
    {
        public ProductoPresentacion()
        {
            InverseIdPresentacionPalletNavigation = new HashSet<ProductoPresentacion>();
            ProductoPallets = new HashSet<ProductoPallet>();
            ProductoPresentacionTarimas = new HashSet<ProductoPresentacionTarima>();
            ProductoPresentacionesConversiones = new HashSet<ProductoPresentacionesConversione>();
            ProductoRellenados = new HashSet<ProductoRellenado>();
            ProductoSustitutos = new HashSet<ProductoSustituto>();
            ReglaUbicDetPps = new HashSet<ReglaUbicDetPp>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransOcDets = new HashSet<TransOcDet>();
            TransPeDets = new HashSet<TransPeDet>();
            TransReDetInfraccions = new HashSet<TransReDetInfraccion>();
            TransReDets = new HashSet<TransReDet>();
            TransTrasDets = new HashSet<TransTrasDet>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdPresentacion { get; set; }
        public int IdProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public bool? ImprimeBarra { get; set; }
        public double? Peso { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double Factor { get; set; }
        public double? MinimoExistencia { get; set; }
        public double? MaximoExistencia { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? EsPallet { get; set; }
        public double? Precio { get; set; }
        public double? MinimoPeso { get; set; }
        public double? MaximoPeso { get; set; }
        public double? Costo { get; set; }
        public double? CamasPorTarima { get; set; }
        public double? CajasPorCama { get; set; }
        public bool? GeneraLpAuto { get; set; }
        public bool? PermitirPaletizar { get; set; }
        public bool? Sistema { get; set; }
        public int? IdPresentacionPallet { get; set; }

        public virtual ProductoPresentacion IdPresentacionPalletNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual ICollection<ProductoPresentacion> InverseIdPresentacionPalletNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<ProductoPresentacionTarima> ProductoPresentacionTarimas { get; set; }
        public virtual ICollection<ProductoPresentacionesConversione> ProductoPresentacionesConversiones { get; set; }
        public virtual ICollection<ProductoRellenado> ProductoRellenados { get; set; }
        public virtual ICollection<ProductoSustituto> ProductoSustitutos { get; set; }
        public virtual ICollection<ReglaUbicDetPp> ReglaUbicDetPps { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
        public virtual ICollection<TransPeDet> TransPeDets { get; set; }
        public virtual ICollection<TransReDetInfraccion> TransReDetInfraccions { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransTrasDet> TransTrasDets { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
