using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ResolucionLpOperador
    {
        public int Idresolucionlp { get; set; }
        public int Idoperador { get; set; }
        public int? Idbodega { get; set; }
        public string Serie { get; set; }
        public int? CorrelativoInicial { get; set; }
        public int? CorrelativoFinal { get; set; }
        public int? CorrelativoActual { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
    }
}
