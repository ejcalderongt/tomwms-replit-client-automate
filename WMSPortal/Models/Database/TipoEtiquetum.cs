using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoEtiquetum
    {
        public int IdTipoEtiqueta { get; set; }
        public string Nombre { get; set; }
        public double? Alto { get; set; }
        public double? Ancho { get; set; }
        public double? MargenIzq { get; set; }
        public double? MagenDer { get; set; }
        public double? MargenSup { get; set; }
        public double? MargenInf { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
