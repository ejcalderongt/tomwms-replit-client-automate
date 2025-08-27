using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwDespachoRepRe
    {
        public int? IdPropietarioBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int IdDespachoEnc { get; set; }
        public int? IdPedidoEnc { get; set; }
        public string Propietario { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Um { get; set; }
        public string Presentacion { get; set; }
        public double? Factor { get; set; }
        public string Estado { get; set; }
        public string Lote { get; set; }
        public DateTime? Vence { get; set; }
        public double? CantidadPickeada { get; set; }
        public double? CantidadVerificada { get; set; }
        public double? PesoPickeado { get; set; }
        public double? PesoVerificado { get; set; }
        public double? CantidadDespachada { get; set; }
        public double? PesoDespachado { get; set; }
        public bool Encontrado { get; set; }
        public bool Acepto { get; set; }
        public long? NoDocumentoWms { get; set; }
        public string NoReferencia { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int? Idubicacionvirtual { get; set; }
        public bool? EsBodegaRecepcion { get; set; }
        public bool? EsBodegaTraslado { get; set; }
        public int? NoPase { get; set; }
        public string Observacion { get; set; }
        public int? Numero { get; set; }
        public string Marchamo { get; set; }
        public string CodigoRuta { get; set; }
        public string NombreRuta { get; set; }
        public string PlacaVehiculo { get; set; }
        public string PlacaComercial { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string NombrePiloto { get; set; }
        public string ApellidoPiloto { get; set; }
        public string NoCarnetPiloto { get; set; }
        public string NoLicenciaPiloto { get; set; }
        public DateTime? FechaDespacho { get; set; }
    }
}
