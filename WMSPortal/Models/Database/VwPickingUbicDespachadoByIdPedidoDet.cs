using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPickingUbicDespachadoByIdPedidoDet
    {
        public int IdDespachoEnc { get; set; }
        public int IdPedidoEnc { get; set; }
        public int? IdPedidoDet { get; set; }
        public int IdPickingEnc { get; set; }
        public int IdPickingUbic { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public string NomEstado { get; set; }
        public string UnidadMedida { get; set; }
        public string Propietario { get; set; }
        public double? CantidadSolicitada { get; set; }
        public int? IdStock { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string LicPlate { get; set; }
        public string Serial { get; set; }
        public double? PesoSolicitado { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? CantidadVerificada { get; set; }
        public double? CantidadDespachada { get; set; }
        public double? PesoRecibido { get; set; }
        public double? PesoVerificado { get; set; }
        public double? PesoDespachado { get; set; }
        public bool? Acepto { get; set; }
        public bool? Encontrado { get; set; }
        public bool? DañadoVerificacion { get; set; }
        public int IdPickingDet { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public long? IdRecepcion { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaRealVence { get; set; }
        public string NoPacking { get; set; }
        public DateTime? FechaPicking { get; set; }
        public DateTime? FechaVerificado { get; set; }
        public DateTime? FechaPacking { get; set; }
        public DateTime? FechaDespachado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? DañadoPicking { get; set; }
        public string NombreUbicacion { get; set; }
    }
}
