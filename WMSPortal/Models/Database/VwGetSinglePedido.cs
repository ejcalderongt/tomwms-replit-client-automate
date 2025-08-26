using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwGetSinglePedido
    {
        public int IdPedidoEnc { get; set; }
        public int? IdBodega { get; set; }
        public int? IdCliente { get; set; }
        public int? IdMuelle { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdTipoPedido { get; set; }
        public int? IdPickingEnc { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public long? NoDespacho { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public long? NoDocumento { get; set; }
        public bool? Local { get; set; }
        public bool? PalletPrimero { get; set; }
        public double? DiasCliente { get; set; }
        public bool? Anulado { get; set; }
        public double? RoadKilometraje { get; set; }
        public DateTime? RoadFechaEntr { get; set; }
        public string RoadDirEntrega { get; set; }
        public double? RoadTotal { get; set; }
        public double? RoadDesMonto { get; set; }
        public double? RoadImpMonto { get; set; }
        public double? RoadPeso { get; set; }
        public string RoadBandera { get; set; }
        public string RoadStatCom { get; set; }
        public string RoadCalcoBj { get; set; }
        public int? RoadImpres { get; set; }
        public string RoadAdd1 { get; set; }
        public string RoadAdd2 { get; set; }
        public string RoadAdd3 { get; set; }
        public string RoadStatProc { get; set; }
        public bool? RoadRechazado { get; set; }
        public string RoadRazonRechazado { get; set; }
        public bool? RoadInformado { get; set; }
        public string RoadSucursal { get; set; }
        public int? RoadIdDespacho { get; set; }
        public int? RoadIdFacturacion { get; set; }
        public int? RoadIdRuta { get; set; }
        public int? RoadIdVendedor { get; set; }
        public int? RoadIdRutaDespacho { get; set; }
        public int? RoadIdVendedorDespacho { get; set; }
        public string Observacion { get; set; }
        public bool? PedidoRoad { get; set; }
        public DateTime? HoraEntregaDesde { get; set; }
        public DateTime? HoraEntregaHasta { get; set; }
        public string Referencia { get; set; }
        public int? IdMotivoAnulacionBodega { get; set; }
        public bool? EnviadoAErp { get; set; }
        public bool? ControlUltimoLote { get; set; }
        public string Serie { get; set; }
        public int? Correlativo { get; set; }
        public string ReferenciaDocumentoIngresoBodegaDestino { get; set; }
        public bool? SyncMi3 { get; set; }
        public string NombreTipoPedido { get; set; }
        public string NombreCliente { get; set; }
        public string NombrePropietario { get; set; }
        public int? IdPropietario { get; set; }
        public string CodigoCliente { get; set; }
        public bool? EsBodegaRecepcion { get; set; }
        public bool? EsBodegaTraslado { get; set; }
        public int? Idubicacionvirtual { get; set; }
        public bool? ControlUltimoLoteCliente { get; set; }
    }
}
