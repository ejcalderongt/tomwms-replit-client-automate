using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReOp
    {
        public int IdOperadorRec { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int? IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
    }
}
