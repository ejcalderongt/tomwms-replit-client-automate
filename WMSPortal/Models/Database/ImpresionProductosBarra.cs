using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ImpresionProductosBarra
    {
        public int IdProductoBarra { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public int? CantidadImpresiones { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public int? CamasPorTarima { get; set; }
        public int? CajasPorCama { get; set; }
        public double? CantidadPresentacion { get; set; }
        public double? Factor { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaAgr { get; set; }
        public string UserAgr { get; set; }
        public bool? Impreso { get; set; }
        public bool? Activo { get; set; }
        public int? IdImpresora { get; set; }
    }
}
