using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcTi
    {
        public TransOcTi()
        {
            TransDiEncs = new HashSet<TransDiEnc>();
            TransOcEncs = new HashSet<TransOcEnc>();
        }

        public int IdTipoIngresoOc { get; set; }
        public string Nombre { get; set; }
        public bool? EsDevolucion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? ControlPoliza { get; set; }
        public bool? RequerirDocumentoRef { get; set; }
        public bool? EsPolizaConsolidada { get; set; }
        public bool? GeneraTareaIngreso { get; set; }
        public bool? RequerirProveedorEsBodegaWms { get; set; }
        public bool? RequerirDocumentoRefWms { get; set; }

        public virtual ICollection<TransDiEnc> TransDiEncs { get; set; }
        public virtual ICollection<TransOcEnc> TransOcEncs { get; set; }
    }
}
