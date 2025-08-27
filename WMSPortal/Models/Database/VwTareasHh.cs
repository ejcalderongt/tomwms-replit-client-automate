using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTareasHh
    {
        public int IdTareahh { get; set; }
        public int? IdPropietario { get; set; }
        public string Propietario { get; set; }
        public int? IdBodega { get; set; }
        public string Bodega { get; set; }
        public int? IdMuelle { get; set; }
        public string Muelle { get; set; }
        public int? IdEstado { get; set; }
        public string Estado { get; set; }
        public int? IdPrioridad { get; set; }
        public string Prioridad { get; set; }
        public int? IdTipoTarea { get; set; }
        public string TipoTarea { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
