using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransaccionesLog
    {
        public int IdTransaccionLog { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdObservacion { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUbicacion { get; set; }
        public double? CantidadReabasto { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual SisObsLog IdObservacionNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual ProductoEstado IdProductoEstadoNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
    }
}
