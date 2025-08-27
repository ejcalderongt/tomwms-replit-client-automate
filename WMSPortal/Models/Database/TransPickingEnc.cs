using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPickingEnc
    {
        public TransPickingEnc()
        {
            TransPickingDets = new HashSet<TransPickingDet>();
            TransPickingOps = new HashSet<TransPickingOp>();
        }

        public int IdPickingEnc { get; set; }
        public int IdBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdUbicacionPicking { get; set; }
        public DateTime? FechaPicking { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Estado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? DetalleOperador { get; set; }
        public bool? Activo { get; set; }
        public bool? VerificaAuto { get; set; }
        public bool? ProcesadoBof { get; set; }
        public bool? RequierePreparacion { get; set; }
        public string TipoPreparacion { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual ICollection<TransPickingDet> TransPickingDets { get; set; }
        public virtual ICollection<TransPickingOp> TransPickingOps { get; set; }
    }
}
