using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwCambiosUbicacion
    {
        public int IdBodegaOrigen { get; set; }
        public string TipoTarea { get; set; }
        public string Código { get; set; }
        public string Producto { get; set; }
        public double? Cantidad { get; set; }
        public string Umbas { get; set; }
        public double? Peso { get; set; }
        public string Lote { get; set; }
        public string Lp { get; set; }
        public DateTime? Vence { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Propietario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Poliza { get; set; }
        public int? IdPresentacion { get; set; }
        public string Presentacion { get; set; }
        public double? Factor { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string UbicacionOrigen { get; set; }
        public string UbicacionDestino { get; set; }
    }
}
