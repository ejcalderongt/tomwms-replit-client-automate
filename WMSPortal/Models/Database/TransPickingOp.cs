using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPickingOp
    {
        public int IdOperadorPicking { get; set; }
        public int IdPickingEnc { get; set; }
        public int IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual TransPickingEnc IdPickingEncNavigation { get; set; }
    }
}
