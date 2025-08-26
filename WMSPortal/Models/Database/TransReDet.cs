using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReDet
    {
        public TransReDet()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
        }

        public int IdRecepcionDet { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdOperadorBodega { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? NoLinea { get; set; }
        public double? CantidadRecibida { get; set; }
        public string NombreProducto { get; set; }
        public string NombrePresentacion { get; set; }
        public string NombreUnidadMedida { get; set; }
        public string NombreProductoEstado { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public double? Peso { get; set; }
        public double? PesoEstadistico { get; set; }
        public double? PesoMinimo { get; set; }
        public double? PesoMaximo { get; set; }
        public double? PesoUnitario { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string Observacion { get; set; }
        public int? Añada { get; set; }
        public double? Costo { get; set; }
        public double? CostoOc { get; set; }
        public double? CostoEstadistico { get; set; }
        public string AtributoVariante1 { get; set; }
        public string CodigoProducto { get; set; }
        public string LicPlate { get; set; }
        public bool? PalletNoEstandar { get; set; }

        public virtual MotivoDevolucion IdMotivoDevolucionNavigation { get; set; }
        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual ProductoEstado IdProductoEstadoNavigation { get; set; }
        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
    }
}
