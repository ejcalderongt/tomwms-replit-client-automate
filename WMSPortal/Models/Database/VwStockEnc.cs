#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockEnc
    {
        public int? IdBodega { get; set; }
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdProductoBodega { get; set; }
        public int IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public string Propietario { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Lote { get; set; }
        public string Serial { get; set; }
        public double? CantidadSf { get; set; }
        public double? Factor { get; set; }
        public double? Cantidad { get; set; }
        public string NomEstado { get; set; }
        public bool? EstadoUtilizable { get; set; }
        public bool? Dañado { get; set; }
        public string LicPlate { get; set; }
        public double? Peso { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? CantidadReservada { get; set; }
        public string IndiceRotacion { get; set; }
        public double? ExistenciaMinUmbas { get; set; }
        public double? ExistenciaMaxUmbas { get; set; }
        public string CodigoBarra { get; set; }
        public double? Costo { get; set; }
        public double? ExistenciaMinPres { get; set; }
        public double? ExistenciaMaxPres { get; set; }
    }
}
