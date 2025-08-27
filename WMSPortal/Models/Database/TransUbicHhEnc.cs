using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransUbicHhEnc
    {
        public TransUbicHhEnc()
        {
            TransUbicHhDets = new HashSet<TransUbicHhDet>();
            TransUbicTarimas = new HashSet<TransUbicTarima>();
        }

        public int IdTareaUbicacionEnc { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdMotivoUbicacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? HoraFin { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Observacion { get; set; }
        public bool? Activo { get; set; }
        public bool? OperadorPorLinea { get; set; }
        public bool? UbicacionConHh { get; set; }
        public string Estado { get; set; }
        public bool? CambioEstado { get; set; }
        public int? IdReabastecimientoLog { get; set; }

        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDets { get; set; }
        public virtual ICollection<TransUbicTarima> TransUbicTarimas { get; set; }
    }
}
