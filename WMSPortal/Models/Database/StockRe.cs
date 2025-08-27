using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockRe
    {
        public int IdStockRes { get; set; }
        public int IdTransaccion { get; set; }
        public string Indicador { get; set; }
        public int IdPedidoDet { get; set; }
        public int IdStock { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int IdUbicacion { get; set; }
        public string UbicacionAnt { get; set; }
        public long? IdRecepcion { get; set; }
        public string Lote { get; set; }
        public string LicPlate { get; set; }
        public string Serial { get; set; }
        public double Cantidad { get; set; }
        public double? Peso { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? UdsLicPlate { get; set; }
        public int? NoBulto { get; set; }
        public long? IdPicking { get; set; }
        public long? IdPedido { get; set; }
        public long? IdDespacho { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Host { get; set; }
        public int? Añada { get; set; }
        public DateTime? FechaManufactura { get; set; }
        public int IdBodega { get; set; }
        public bool? PalletNoEstandar { get; set; }
    }
}
