using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockRe
    {
        public int? IdBodega { get; set; }
        public string Bodega { get; set; }
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdProductoBodega { get; set; }
        public int IdStock { get; set; }
        public bool? PalletNoEstandar { get; set; }
        public int Posiciones { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public int IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public string Propietario { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public double CantidadSf { get; set; }
        public double? Peso { get; set; }
        public double? Cantidad { get; set; }
        public string NomEstado { get; set; }
        public bool? Dañado { get; set; }
        public double? Factor { get; set; }
        public bool? EstadoUtilizable { get; set; }
        public int IdUbicacion { get; set; }
        public string LicPlate { get; set; }
        public string Serial { get; set; }
        public int? Añada { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? CantidadReservada { get; set; }
        public int? IdTramo { get; set; }
        public double? AnchoUbicacion { get; set; }
        public double? LargoUbicacion { get; set; }
        public double? AltoUbicacion { get; set; }
        public string IndiceRotacion { get; set; }
        public double? ExistenciaMinUmbas { get; set; }
        public double? ExistenciaMaxUmbas { get; set; }
        public string CodigoBarra { get; set; }
        public double? Costo { get; set; }
        public double? ExistenciaMinPres { get; set; }
        public double? ExistenciaMaxPres { get; set; }
        public string AtributoVariante1 { get; set; }
        public int? IdUbicacionActual { get; set; }
        public int? UbicacionNivel { get; set; }
        public int? UbicacionIndiceX { get; set; }
        public string UbicacionNombre { get; set; }
        public string UbicacionTramo { get; set; }
        public string NombreCompleto { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public string NumeroOrden { get; set; }
        public string CodigoPoliza { get; set; }
        public int DocumentoIngreso { get; set; }
    }
}
