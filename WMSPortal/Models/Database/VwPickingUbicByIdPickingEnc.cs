using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPickingUbicByIdPickingEnc
    {
        public int IdPickingEnc { get; set; }
        public int IdPickingUbic { get; set; }
        public int IdPickingDet { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdStock { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public long? IdRecepcion { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaMinima { get; set; }
        public string Serial { get; set; }
        public string LicPlate { get; set; }
        public bool? Acepto { get; set; }
        public double? PesoSolicitado { get; set; }
        public double? PesoRecibido { get; set; }
        public double? PesoVerificado { get; set; }
        public double? PesoDespachado { get; set; }
        public double? CantidadSolicitada { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? CantidadVerificada { get; set; }
        public bool? Encontrado { get; set; }
        public bool? DañadoVerificacion { get; set; }
        public DateTime? FechaRealVence { get; set; }
        public string NoPacking { get; set; }
        public DateTime? FechaPicking { get; set; }
        public DateTime? FechaVerificado { get; set; }
        public DateTime? FechaPacking { get; set; }
        public DateTime? FechaDespachado { get; set; }
        public double? CantidadDespachada { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public int? IdPedidoDet { get; set; }
        public bool? DañadoPicking { get; set; }
        public int IdStockRes { get; set; }
        public string LicPlateReemplazo { get; set; }
        public int? IdUbicacionReemplazo { get; set; }
        public int? IdStockReemplazo { get; set; }
        public int IdPedidoEnc { get; set; }
        public int Expr1 { get; set; }
        public int? Expr2 { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int Expr3 { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NomPresentacion { get; set; }
        public string NomUnidMed { get; set; }
        public string NomEstado { get; set; }
        public int IdEstado { get; set; }
        public double? Peso { get; set; }
        public double? Precio { get; set; }
        public int SrIdStockRes { get; set; }
        public int SrIdStock { get; set; }
        public int? IdBodega { get; set; }
        public int? IdOperadorBodegaPickeo { get; set; }
        public int? IdOperadorBodegaVerifico { get; set; }
        public string NombreUbicacion { get; set; }
    }
}
