using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class StockJornadum
    {
        public int IdStockJornada { get; set; }
        public int? IdJornadaSistema { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdBodega { get; set; }
        public int? IdStock { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdProductoBodega { get; set; }
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
        public double? UdsLicPlate { get; set; }
        public int? NoBulto { get; set; }
        public DateTime? FechaManufactura { get; set; }
        public int? Añada { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public double? Peso { get; set; }
        public double? Temperatura { get; set; }
        public string AtributoVariante1 { get; set; }
        public bool? PalletNoEstandar { get; set; }
        public string Propietario { get; set; }
        public string Proveedor { get; set; }
        public string Bodega { get; set; }
        public int? IdOrdenCompraEnc { get; set; }
        public string NoDocumentoOc { get; set; }
        public string NoDocumentoRec { get; set; }
        public string ReferenciaOc { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string TipoTrans { get; set; }
        public DateTime? FechaAgrego { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoBarraProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Existencia { get; set; }
        public string NomUmBas { get; set; }
        public string NomEstadoProducto { get; set; }
        public string NomPresentacionProducto { get; set; }
        public string UbicacionOrigen { get; set; }
        public string NoPoliza { get; set; }
        public double? ValorAduana { get; set; }
        public double? ValorFob { get; set; }
        public double? ValorIva { get; set; }
        public double? ValorDai { get; set; }
        public double? ValorSeguro { get; set; }
        public double? ValorFlete { get; set; }
        public double? PesoNeto { get; set; }
        public string NumeroOrden { get; set; }
        public string CodigoRegimen { get; set; }
        public string NombreRegimen { get; set; }
        public int? DiasVencimientoRegimen { get; set; }
        public DateTime? FechaIngresoTicketTms { get; set; }
        public bool? EsRetroactivo { get; set; }
        public double? Factor { get; set; }
        public double? CamasPorTarima { get; set; }
        public double? CajasPorCama { get; set; }
        public double? CantidadIngresoAfectaASalida { get; set; }
        public string StockJornadaHash { get; set; }
        public int? IdTicketTms { get; set; }
        public DateTime? FechaProcesadoStockJornada { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdClasificacion { get; set; }
        public string Clasificacion { get; set; }
        public string Regimen { get; set; }
        public int Posiciones { get; set; }
        public double? CostoUnitario { get; set; }
    }
}
