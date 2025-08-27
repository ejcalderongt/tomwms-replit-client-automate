using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransTrasOp
    {
        public int IdOperadorTras { get; set; }
        public int IdTrasladoEnc { get; set; }
        public int? IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual TransTrasEnc IdTrasladoEncNavigation { get; set; }
    }
}
