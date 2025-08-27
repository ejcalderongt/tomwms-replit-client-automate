using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReOc
    {
        public int IdRecepcionOc { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public bool? RecepcionCiega { get; set; }
        public bool? RecepcionManual { get; set; }
        public string NoDocto { get; set; }
        public DateTime? HoraIniHh { get; set; }
        public DateTime? HoraFinHh { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public byte[] FirmaOperador { get; set; }

        public virtual TransOcEnc IdOrdenCompraEncNavigation { get; set; }
        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
    }
}
