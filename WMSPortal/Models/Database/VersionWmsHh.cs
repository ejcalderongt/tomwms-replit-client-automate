using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VersionWmsHh
    {
        public int IdEmpresaVersion { get; set; }
        public int IdEmpresa { get; set; }
        public string Version { get; set; }
        public string Notas { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
