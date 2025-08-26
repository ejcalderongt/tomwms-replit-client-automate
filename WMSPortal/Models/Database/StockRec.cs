using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockRec
    {
        public StockRec()
        {
            StockSeRecs = new HashSet<StockSeRec>();
        }

        public int IdStockRec { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int IdUbicacion { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? IdRecepcionDet { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int? IdPickingEnc { get; set; }
        public int? IdDespachoEnc { get; set; }
        public string Lote { get; set; }
        public string LicPlate { get; set; }
        public string Serial { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? UdsLicPlate { get; set; }
        public int? NoBulto { get; set; }
        public DateTime? FechaManufactura { get; set; }
        public int? Añada { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }
        public double? Peso { get; set; }
        public double? Temperatura { get; set; }
        public bool? Regularizado { get; set; }
        public DateTime? FechaRegularizacion { get; set; }
        public int? NoLinea { get; set; }
        public string AtributoVariante1 { get; set; }
        public bool? Impreso { get; set; }
        public int? IdBodega { get; set; }
        public bool? PalletNoEstandar { get; set; }

        public virtual BodegaUbicacion Id { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual ProductoEstado IdProductoEstadoNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual TransReDet IdRecepcion { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<StockSeRec> StockSeRecs { get; set; }
    }
}
