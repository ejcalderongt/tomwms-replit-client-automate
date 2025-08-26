using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcEstado
    {
        public TransOcEstado()
        {
            TransDiEncs = new HashSet<TransDiEnc>();
            TransOcEncs = new HashSet<TransOcEnc>();
        }

        public int IdEstadoOc { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TransDiEnc> TransDiEncs { get; set; }
        public virtual ICollection<TransOcEnc> TransOcEncs { get; set; }
    }
}
