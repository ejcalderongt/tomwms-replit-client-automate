using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMovimientosN
    {
        public string Propietario { get; set; }
        public string Producto { get; set; }
        public string Poliza { get; set; }
        public string Presentación { get; set; }
        public string EstadoOrigen { get; set; }
        public string EstadoDestino { get; set; }
        public string Umbas { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public string Lote { get; set; }
        public string UbicOrigen { get; set; }
        public string UbicDestino { get; set; }
        public string TipoTarea { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public int? IdTipoTarea { get; set; }
        public bool? Contabilizar { get; set; }
        public DateTime? FechaVence { get; set; }
        public int? IdTipoActualizacionCosto { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdEstadoOrigen { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdBodega { get; set; }
        public string BarraPallet { get; set; }
        public string Clasificacion { get; set; }
        public string Familia { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int? IdBodegaDestino { get; set; }
        public string CodigoBodegaDestino { get; set; }
        public string NombreBodegaDestino { get; set; }
        public int IdMovimiento { get; set; }
        public string CodigoBodegaOrigen { get; set; }
        public string NombreBodegaOrigen { get; set; }
    }
}
