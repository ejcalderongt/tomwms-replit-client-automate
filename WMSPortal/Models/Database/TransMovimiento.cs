using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransMovimiento
    {
        public int IdMovimiento { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int IdTransaccion { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdUbicacionOrigen { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdEstadoOrigen { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdTipoTarea { get; set; }
        public int? IdBodegaDestino { get; set; }
        public int? IdRecepcion { get; set; }
        public double? Cantidad { get; set; }
        public string Serie { get; set; }
        public double? Peso { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? Fecha { get; set; }
        public string BarraPallet { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public DateTime? FechaAgr { get; set; }
        public string UsuarioAgr { get; set; }
        public double? CantidadHist { get; set; }
        public double? PesoHist { get; set; }
        public string LicPlate { get; set; }

        public virtual Bodega IdBodegaDestinoNavigation { get; set; }
        public virtual ProductoEstado IdEstadoOrigenNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual SisTipoTarea IdTipoTareaNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
    }
}
