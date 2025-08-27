using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReTr
    {
        public TransReTr()
        {
            TransReEncs = new HashSet<TransReEnc>();
        }

        public string IdTipoTransaccion { get; set; }
        public string Descripcion { get; set; }
        public string Funcionalidad { get; set; }
        public bool? UsaHh { get; set; }
        public string DescDev { get; set; }
        public string TipoTrans { get; set; }
        public bool? ConRef { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<TransReEnc> TransReEncs { get; set; }
    }
}
