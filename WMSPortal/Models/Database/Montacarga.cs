using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Montacarga
    {
        public int IdMontacarga { get; set; }
        public int? IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public double? CapacidadBasica { get; set; }
        public double? DesplazamientoMotor { get; set; }
        public string TipoCombustible { get; set; }
        public string TipoMontacarga { get; set; }
        public DateTime? FechaCompra { get; set; }
        public DateTime? FechaInicioOperaciones { get; set; }
        public DateTime? ProximoMantenimiento { get; set; }
        public double? CostoHora { get; set; }
    }
}
