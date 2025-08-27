using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReabastecimientoLog
    {
        public int IdReabastecimientoLog { get; set; }
        public int? IdRellenado { get; set; }
        public int? IdBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string NombreUmBas { get; set; }
        public int? IdPresentacion { get; set; }
        public string Presentacion { get; set; }
        public double? Minimo { get; set; }
        public double? Maximo { get; set; }
        public int? IdProductoEstado { get; set; }
        public string Estado { get; set; }
        public double? StockUmbas { get; set; }
        public double? ReservadoUmBas { get; set; }
        public double? DisponibleUmbas { get; set; }
        public double? Factor { get; set; }
        public double? StockPres { get; set; }
        public double? ReservadoPres { get; set; }
        public double? DisponiblePres { get; set; }
        public string Ubicacion { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdTipoAccion { get; set; }
        public bool? Activo { get; set; }
        public int? IdPropietario { get; set; }
        public string NombrePropietario { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public int? IdUmBasAbastercerCon { get; set; }
        public int? IdPresentacionAbastercerCon { get; set; }
        public string NombrePresentacionAbastecerCon { get; set; }
        public bool? Enviado { get; set; }
        public bool? Cancelado { get; set; }
        public DateTime? FechaGeneracionInexistencia { get; set; }
        public DateTime? HoraGeneracionInexistencia { get; set; }
        public DateTime? FechaProcesamientoBof { get; set; }
        public DateTime? HoraProcesamientoBof { get; set; }
        public int? IdOperadorBodega { get; set; }
        public bool? ProcesadoHh { get; set; }
        public DateTime? FechaProcesamientoHh { get; set; }
        public double? StockUbicacion { get; set; }
        public double? CantidadAUbicar { get; set; }
        public double? StockInferior { get; set; }
        public double? StockDisponible { get; set; }
        public int? IdTareaUbicacionEnc { get; set; }
    }
}
