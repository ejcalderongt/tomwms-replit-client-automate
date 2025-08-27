using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRptMinimosMaximo
    {
        public int? IdBodega { get; set; }
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProducto { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdStock { get; set; }
        public int? IdUbicacionActual { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public int IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public string Propietario { get; set; }
        public string UbicacionAnterior { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Serial { get; set; }
        public int? Añada { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaVence { get; set; }
        public string NomEstado { get; set; }
        public bool? EstadoUtilizable { get; set; }
        public bool? Dañado { get; set; }
        public int IdUbicacion { get; set; }
        public string LicPlate { get; set; }
        public double? Peso { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public double MinimoExistencia { get; set; }
        public double MaximoExistencia { get; set; }
        public string CodigoBarra { get; set; }
        public string IndiceRotacion { get; set; }
        public double? Costo { get; set; }
        public double? TotalLinea { get; set; }
        public double? CantidadPresentacion { get; set; }
    }
}
