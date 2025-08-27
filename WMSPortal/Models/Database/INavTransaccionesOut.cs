using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavTransaccionesOut
    {
        public int Idtransaccion { get; set; }
        public int? Idempresa { get; set; }
        public int Idbodega { get; set; }
        public int? Idpropietario { get; set; }
        public int? Idpropietariobodega { get; set; }
        public int? Idordencompra { get; set; }
        public int? Idrecepcionenc { get; set; }
        public int? Idpedidoenc { get; set; }
        public int? Iddespachoenc { get; set; }
        public int Idproductobodega { get; set; }
        public int? Idproducto { get; set; }
        public int? Idunidadmedida { get; set; }
        public int? Idpresentacion { get; set; }
        public int? Idproductoestado { get; set; }
        public double Cantidad { get; set; }
        public double? Peso { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string NoPedido { get; set; }
        public string NoLinea { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoVariante { get; set; }
        public string UnidadMedida { get; set; }
        public string TipoTransaccion { get; set; }
        public int? Enviado { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public double? CantidadEsperada { get; set; }
        public string LicPlate { get; set; }
        public double? UdsLicPlate { get; set; }
        public double? CantidadPresentacion { get; set; }
        public int? IdTipoDocumento { get; set; }
        public bool? EsServicio { get; set; }
        public string CodigoBarra { get; set; }
        public double? ValorAduana { get; set; }
        public double? ValorFob { get; set; }
        public double? ValorIva { get; set; }
        public double? ValorDai { get; set; }
        public double? ValorSeguro { get; set; }
        public double? ValorFlete { get; set; }
        public double? PesoNeto { get; set; }
        public double? PesoBruto { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public string NoDocumentoSalidaRefDevol { get; set; }
        public int? IdPedidoEncDevol { get; set; }
    }
}
