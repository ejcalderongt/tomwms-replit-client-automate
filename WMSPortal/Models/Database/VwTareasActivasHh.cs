using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTareasActivasHh
    {
        public int? Correlativo { get; set; }
        public string Tarea { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime UltRevision { get; set; }
        public int? Ttm { get; set; }
        public string Propietario { get; set; }
        public string Estado { get; set; }
        public int IdTareahh { get; set; }
        public int? IdBodega { get; set; }
    }
}
