using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwGetDetalleByIdRecepcionEnc
    {
        public int? IdBodega { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int? IdUbicacionRecepcion { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProducto { get; set; }
        public bool? ControlPeso { get; set; }
        public int IdRecepcionDet { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdOperadorBodega { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? NoLinea { get; set; }
        public double? CantidadRecibida { get; set; }
        public string NombreProducto { get; set; }
        public string NombrePresentacion { get; set; }
        public string NombreUnidadMedida { get; set; }
        public string NombreProductoEstado { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public double? Peso { get; set; }
        public double? PesoEstadistico { get; set; }
        public double? PesoMinimo { get; set; }
        public double? PesoMaximo { get; set; }
        public double? PesoUnitario { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string Observacion { get; set; }
        public int? Añada { get; set; }
        public double? Costo { get; set; }
        public double? CostoOc { get; set; }
        public double? CostoEstadistico { get; set; }
        public string AtributoVariante1 { get; set; }
        public string CodigoProducto { get; set; }
        public string LicPlate { get; set; }
        public bool? PalletNoEstandar { get; set; }
    }
}
