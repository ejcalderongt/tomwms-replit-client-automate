using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransUbicHhDet
    {
        public int IdTareaUbicacionEnc { get; set; }
        public int IdTareaUbicacionDet { get; set; }
        public int? IdStock { get; set; }
        public int? IdUbicacionOrigen { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public int? IdEstadoOrigen { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdOperadorBodega { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public bool? Realizado { get; set; }
        public double? Cantidad { get; set; }
        public bool? Activo { get; set; }
        public double? Recibido { get; set; }
        public string Estado { get; set; }
        public string AtributoVariante1 { get; set; }
        public int? IdBodega { get; set; }

        public virtual BodegaUbicacion Id { get; set; }
        public virtual ProductoEstado IdEstadoDestinoNavigation { get; set; }
        public virtual ProductoEstado IdEstadoOrigenNavigation { get; set; }
        public virtual BodegaUbicacion IdNavigation { get; set; }
        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual TransUbicHhEnc IdTareaUbicacionEncNavigation { get; set; }
    }
}
