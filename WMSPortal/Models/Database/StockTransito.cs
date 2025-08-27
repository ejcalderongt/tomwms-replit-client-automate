using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockTransito
    {
        public int IdStockTransito { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodegaOrigen { get; set; }
        public int? IdBodegaDestino { get; set; }
        public int? IdStock { get; set; }
        public int? IdProductoBodegaDestino { get; set; }
        public int? IdProductoBodegaOrigen { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? IdRecepcionDet { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int? IdPickingEnc { get; set; }
        public int? IdDespachoEnc { get; set; }
        public int? IdPickingUbic { get; set; }
        public int? IdPedidoDet { get; set; }
        public string Lote { get; set; }
        public string LicPlate { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaManufactura { get; set; }
        public double? CantidadRecibida { get; set; }
        public DateTime? FechaAgregado { get; set; }
        public int? IdOrdenCompraEncBodDest { get; set; }
        public int? IdRecepcionEncBodDest { get; set; }
    }
}
