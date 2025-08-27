using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransUbicHhStock
    {
        public int IdStockTransUbicHhdet { get; set; }
        public int IdTareaUbicacionEnc { get; set; }
        public int IdTareaUbicacionDet { get; set; }
        public int IdStock { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUbicacion { get; set; }
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
        public string UdsLicPlate { get; set; }
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
        public DateTime? FechaMovHist { get; set; }
        public string AtributoVariante1 { get; set; }

        public virtual TransPeEnc IdPedidoEncNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual ProductoEstado IdProductoEstadoNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual TransReDet IdRecepcion { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
    }
}
