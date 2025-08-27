using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReDetParametro
    {
        public int IdParametroDet { get; set; }
        public int IdRecepcionDet { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdProductoParametro { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLogico { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }

        public virtual ProductoParametro IdProductoParametroNavigation { get; set; }
    }
}
