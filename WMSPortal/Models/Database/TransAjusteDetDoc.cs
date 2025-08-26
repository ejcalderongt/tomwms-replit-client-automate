#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransAjusteDetDoc
    {
        public int Idajustedoc { get; set; }
        public int Idajusteenc { get; set; }
        public string Documento { get; set; }

        public virtual TransAjusteEnc IdajusteencNavigation { get; set; }
    }
}
