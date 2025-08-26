using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReFact
    {
        public int IdFacturaRecepcion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? Orden { get; set; }
        public string NoFactura { get; set; }
        public string Observacion { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Completa { get; set; }

        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
    }
}
