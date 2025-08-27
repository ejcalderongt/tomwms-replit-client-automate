using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRptProductosProximosVencimiento
    {
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdProducto { get; set; }
        public int IdStock { get; set; }
        public int IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public string Propietario { get; set; }
        public string UnidadMedida { get; set; }
        public bool? ControlVencimiento { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public double? Cantidad { get; set; }
        public double? ExistenciaMin { get; set; }
        public double? ExistenciaMax { get; set; }
        public string CodigoBarra { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaProyectada { get; set; }
        public int? Tolerancia { get; set; }
        public int? ToleranciaProyectada { get; set; }
        public string Presentacion { get; set; }
        public int IdEstado { get; set; }
        public string NomEstado { get; set; }
        public string Serial { get; set; }
        public int IdUbicacion { get; set; }
        public double? CantidadPresentacion { get; set; }
    }
}
