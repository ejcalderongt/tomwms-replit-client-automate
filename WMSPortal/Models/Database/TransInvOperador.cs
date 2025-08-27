#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvOperador
    {
        public int Idinvoperador { get; set; }
        public int Idinventarioenc { get; set; }
        public int Idinvencreconteo { get; set; }
        public int Idubic { get; set; }
        public int Idoperador { get; set; }

        public virtual TransInvEnc IdinventarioencNavigation { get; set; }
    }
}
