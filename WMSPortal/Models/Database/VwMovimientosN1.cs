using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMovimientosN1
    {
        public int IdEmpresa { get; set; }
        public int Idmovimiento { get; set; }
        public string Propietario { get; set; }
        public string Producto { get; set; }
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
        public int IdBodegaOrigen { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public int? IdTipoTarea { get; set; }
        public bool? Contabilizar { get; set; }
        public string NoDocIngreso { get; set; }
        public string NoRefIngreso { get; set; }
        public string NoDocSalida { get; set; }
        public string NoRefSalida { get; set; }
        public DateTime? FechaVence { get; set; }
        public int? IdTipoActualizacionCosto { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdEstadoOrigen { get; set; }
        public int? IdProductoBodega { get; set; }
        public string BarraPallet { get; set; }
        public string CodigoBodega { get; set; }
    }
}
