#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavServicio
    {
        public int IdServicio { get; set; }
        public string CodigoServicio { get; set; }
        public string Descripcion { get; set; }
        public string Nemonico { get; set; }
        public int? CodigoRubro { get; set; }
        public bool? Activo { get; set; }
        public bool ProcesadoWms { get; set; }
    }
}
