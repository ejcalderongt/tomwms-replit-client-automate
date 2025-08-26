using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProximosVencimiento
    {
        public string Propietario { get; set; }
        public string Codigo { get; set; }
        public string Barra { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public string Lote { get; set; }
        public string LicPlate { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaProyectada { get; set; }
        public int? ToleranciaDias { get; set; }
        public double? CantidadSf { get; set; }
        public string NomEstado { get; set; }
        public int IdUbicacion { get; set; }
        public double? Cantidad { get; set; }
        public int? IdProducto { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdBodega { get; set; }
        public string UbicacionCompleta { get; set; }
    }
}
