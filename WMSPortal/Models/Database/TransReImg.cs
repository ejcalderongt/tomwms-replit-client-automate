using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReImg
    {
        public int IdImagen { get; set; }
        public int IdRecepcionEnc { get; set; }
        public byte[] Imagen { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string Observacion { get; set; }

        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
    }
}
