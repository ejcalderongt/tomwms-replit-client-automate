using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockResPedido
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public string NomEstado { get; set; }
        public string Unidadmedida { get; set; }
        public string Propietario { get; set; }
        public string Bodegaubicacion { get; set; }
        public double Cantidadfisica { get; set; }
        public double? Factor { get; set; }
        public int IdStockRes { get; set; }
        public int IdTransaccion { get; set; }
        public string Indicador { get; set; }
        public int IdPedidoDet { get; set; }
        public int IdStock { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdUbicacion { get; set; }
        public string Estado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public string Lote { get; set; }
        public string LicPlate { get; set; }
        public string Serial { get; set; }
        public double Cantidad { get; set; }
        public double? Peso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? UdsLicPlate { get; set; }
        public string UbicacionAnt { get; set; }
        public int? NoBulto { get; set; }
        public long? IdRecepcion { get; set; }
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
        public string Referencia { get; set; }
        public int? IdBodega { get; set; }
    }
}
