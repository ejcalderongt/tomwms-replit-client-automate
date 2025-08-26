using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TmpLicenciaItem
    {
        public string IdDisp { get; set; }
        public string Identificacion { get; set; }
        public int? Tipo { get; set; }
        public int? Bandera { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaSistema { get; set; }
    }
}
