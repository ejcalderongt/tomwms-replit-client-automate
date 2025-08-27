using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwEstadoEnviosNav
    {
        public DateTime? Fecha { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int? Cantidad { get; set; }
    }
}
