using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MotivoDevolucion
    {
        public MotivoDevolucion()
        {
            MotivoDevolucionBodegas = new HashSet<MotivoDevolucionBodega>();
            TransOcDets = new HashSet<TransOcDet>();
            TransOcEncs = new HashSet<TransOcEnc>();
            TransReDets = new HashSet<TransReDet>();
        }

        public int IdMotivoDevolucion { get; set; }
        public int IdEmpresa { get; set; }
        public int? IdPropietario { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
        public bool EsDetalle { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<MotivoDevolucionBodega> MotivoDevolucionBodegas { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
        public virtual ICollection<TransOcEnc> TransOcEncs { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
    }
}
