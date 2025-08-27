using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockRecep
    {
        public int IdStock { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int IdUbicacion { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? IdRecepcionDet { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int? IdPickingEnc { get; set; }
        public int? IdDespachoEnc { get; set; }
        public string Lote { get; set; }
        public double? Sumcant { get; set; }
        public DateTime? FechaVence { get; set; }
        public int? Añada { get; set; }
        public bool Activo { get; set; }
        public double? Sumpeso { get; set; }
        public double? Temperatura { get; set; }
        public int? IdBodega { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Serial { get; set; }
        public string LicPlate { get; set; }
        public double? Cantidad { get; set; }
        public string NombrePropietario { get; set; }
        public string NombrePresentacion { get; set; }
        public string NombreEstado { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public string Codigo { get; set; }
    }
}
