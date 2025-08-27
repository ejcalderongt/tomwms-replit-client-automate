#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EstructuraGrupo
    {
        public int IdGrupo { get; set; }
        public int? IdTramo { get; set; }
        public int? Pos { get; set; }
        public int? Cant { get; set; }
        public int? Tamano { get; set; }
        public int? Offset { get; set; }
        public double? Ancho { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public int Palet { get; set; }
        public int? Orient { get; set; }
        public int? Agrupacion { get; set; }

        public virtual EstructuraTramo IdTramoNavigation { get; set; }
    }
}
