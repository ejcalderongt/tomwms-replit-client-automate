using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransUbicHhOp
    {
        public int IdTransUbicHhOp { get; set; }
        public int IdTareaUbicacionEnc { get; set; }
        public int IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
    }
}
